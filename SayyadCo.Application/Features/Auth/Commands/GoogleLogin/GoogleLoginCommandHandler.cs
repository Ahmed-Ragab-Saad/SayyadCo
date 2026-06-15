using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, Result<GoogleLoginResponseDto>>
    {
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IAuthService _authService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public GoogleLoginCommandHandler(IGoogleAuthService googleAuthService, IAuthService authService,
            IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _googleAuthService = googleAuthService;
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GoogleLoginResponseDto>> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            var googleUser = await _googleAuthService.VerifyTokenAsync(request.IdToken);
            if (googleUser is null)
                return Result<GoogleLoginResponseDto>.Failure("Invalid Google token");

            var existingUser = await _authService.FindByEmailAsync(googleUser.Email);
            var isNewUser = existingUser is null;

            string userId;

            if (isNewUser)
            {
                var result = await _authService.RegisterExternalAsync(
                    googleUser.FirstName,
                    googleUser.LastName,
                    googleUser.Email,
                    googleUser.Picture
                );

                if (!result.Succeeded)
                    return Result<GoogleLoginResponseDto>.Failure(string.Join(", ", result.Errors));

                userId = result.UserId;
            }
            else
            {
                userId = existingUser!.Id;
                if (existingUser.IsExternalImage)
                    await _authService.UpdateProfileImageAsync(userId, googleUser.Picture);
            }

            var roles = await _authService.GetRolesAsync(userId);
            var user = await _authService.FindByIdAsync(userId);

            var userModel = new UserTokenModel
            {
                Id = userId,
                Email = googleUser.Email,
                FirstName = user!.FirstName,
                LastName = user.LastName,
                Roles = roles,
            };

            var tokens = _jwtGenerator.GenerateTokens(userModel);

            await _unitOfWork.RefreshTokens.AddAsync(new Domain.Entities.RefreshToken
            {
                Token = tokens.RefreshToken,
                UserId = userId,
                ExpiresAt = tokens.RefreshTokenExpiry
            });

            await _unitOfWork.SaveChangesAsync();

            return Result<GoogleLoginResponseDto>.Success(new GoogleLoginResponseDto
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiry = tokens.AccessTokenExpiry,
                Image = user.Image,
                IsNewUser = isNewUser
            });
        }
    }
}
