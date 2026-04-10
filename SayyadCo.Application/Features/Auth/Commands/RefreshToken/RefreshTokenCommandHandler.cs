using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResponseDto>>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RefreshTokenCommandHandler(IJwtGenerator jwtGenerator, IAuthService authService, IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _jwtGenerator = jwtGenerator;
            _authService = authService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<RefreshTokenResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _jwtGenerator.GetPrincipalFromExpiredToken(request.AccessToken);
            if (principal is null)
                return Result<RefreshTokenResponseDto>.Failure("Invalid access token");

            var userId = principal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                ?? principal.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId))
                return Result<RefreshTokenResponseDto>.Failure("Invalid access token");

            var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);
            if (refreshToken is null)
                return Result<RefreshTokenResponseDto>.NotFound("Refresh token not found");

            if (refreshToken.UserId != userId)
                return Result<RefreshTokenResponseDto>.Failure("Invalid refresh token");

            if (!refreshToken.IsActive)
                return Result<RefreshTokenResponseDto>.Failure(
                    refreshToken.IsExpired ? "Refresh token has expired" : "Refresh token has been revoked");

            var user = await _authService.FindByIdAsync(userId);
            if (user is null)
                return Result<RefreshTokenResponseDto>.NotFound("User not found");

            await _unitOfWork.RefreshTokens.RevokeAsync(request.RefreshToken);

            var roles = await _authService.GetRolesAsync(userId);
            var userModel = new UserTokenModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };

            var tokens = _jwtGenerator.GenerateTokens(userModel);

            await _unitOfWork.RefreshTokens.AddAsync(new Domain.Entities.RefreshToken()
            {
                Token = tokens.RefreshToken,
                UserId = userId,
                ExpiresAt = tokens.RefreshTokenExpiry
            });

            await _unitOfWork.SaveChangesAsync();

            return Result<RefreshTokenResponseDto>.Success(_mapper.Map<RefreshTokenResponseDto>(tokens));
        }
    }
}
