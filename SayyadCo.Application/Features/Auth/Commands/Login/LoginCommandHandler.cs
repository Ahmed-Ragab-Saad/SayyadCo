using AutoMapper;
using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Common.Templates;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Auth.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public LoginCommandHandler(IAuthService authService, IJwtGenerator jwtGenerator, IUnitOfWork unitOfWork,
            IMapper mapper, IEmailService emailService)
        {
            _authService = authService;
            _jwtGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<Result<LoginResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.FindByEmailAsync(request.Email);
            if (user is null)
                return Result<LoginResponseDto>.Failure("Invalid email or password");

            if (await _authService.IsLockedOutAsync(user.Id))
                return Result<LoginResponseDto>.Failure("Account is locked please try again later");

            var isPasswordValid = await _authService.CheckPasswordAsync(user.Id, request.Password);
            if (!isPasswordValid)
                return Result<LoginResponseDto>.Failure("Invalid email or password");

            if (!await _authService.IsEmailConfirmedAsync(user.Id))
            {
                await _unitOfWork.Otps.InvalidateUserOtpsAsync(user.Id);

                var otpCode = GenerateOtp();
                var otpEntity = new OtpCode
                {
                    UserId = user.Id,
                    Code = otpCode,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                    LastResendAt = DateTime.UtcNow
                };

                await _unitOfWork.Otps.AddAsync(otpEntity);
                await _unitOfWork.SaveChangesAsync();

                await _emailService.SendAsync(user.Email, EmailTemplates.VerifyEmailOtpTemplate(otpCode, user.FirstName));

                return Result<LoginResponseDto>.UnverifiedEmail(otpEntity.Token);
            }

            var roles = await _authService.GetRolesAsync(user.Id);
            var userModel = new UserTokenModel()
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
                UserId = user.Id,
                ExpiresAt = tokens.RefreshTokenExpiry
            });

            await _unitOfWork.SaveChangesAsync();

            return Result<LoginResponseDto>.Success(_mapper.Map<LoginResponseDto>(tokens));
        }

        private static string GenerateOtp()
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000);
            return number.ToString();
        }
    }
}
