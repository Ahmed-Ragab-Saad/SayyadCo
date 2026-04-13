using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommand, Result<FacebookLoginResponseDto>>
    {
        private readonly IFacebookAuthService _facebookAuthService;
        private readonly IAuthService _authService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;

        public FacebookLoginCommandHandler(IFacebookAuthService facebookAuthService, IAuthService authService,
            IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork)
        {
            _facebookAuthService = facebookAuthService;
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<FacebookLoginResponseDto>> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
        {
            var facebookUser = await _facebookAuthService.VerifyTokenAsync(request.AccessToken);
            if (facebookUser is null)
                return Result<FacebookLoginResponseDto>.Failure("Invalid Facebook token");

            if (string.IsNullOrEmpty(facebookUser.Email))
                return Result<FacebookLoginResponseDto>.Failure("Facebook account must have an email address");

            var existingUser = await _authService.FindByEmailAsync(facebookUser.Email);
            var isNewUser = existingUser is null;
            string userId;

            if (isNewUser)
            {
                var result = await _authService.RegisterExternalAsync(
                    facebookUser.FirstName,
                    facebookUser.LastName,
                    facebookUser.Email
                );

                if (!result.Succeeded)
                    return Result<FacebookLoginResponseDto>.Failure(string.Join(", ", result.Errors));

                userId = result.UserId;
            }
            else
            {
                userId = existingUser!.Id;
            }

            var roles = await _authService.GetRolesAsync(userId);
            var user = await _authService.FindByIdAsync(userId);

            var userModel = new UserTokenModel()
            {
                Id = userId,
                Email = facebookUser.Email,
                FirstName = user!.FirstName,
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

            return Result<FacebookLoginResponseDto>.Success(new FacebookLoginResponseDto
            {
                AccessToken = tokens.AccessToken,
                RefreshToken = tokens.RefreshToken,
                AccessTokenExpiry = tokens.AccessTokenExpiry,
                IsNewUser = isNewUser
            });
        }
    }
}
