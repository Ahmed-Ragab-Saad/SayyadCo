using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Common.Templates;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Auth.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponseDto>>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;

        public RegisterCommandHandler(IAuthService authService, IUnitOfWork unitOfWork, IEmailService emailService)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
        }

        public async Task<Result<RegisterResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _authService.FindByEmailAsync(request.Email);
            if (existingUser is not null)
                return Result<RegisterResponseDto>.Failure("Email already exists");

            var roles = new List<string>() { AppRoles.User };
            var result = await _authService.RegisterAsync(request, roles);

            if (!result.Succeeded)
                return Result<RegisterResponseDto>.Failure(string.Join(", ", result.Errors));

            var otpCode = GenerateOtp();

            var otpEntity = new OtpCode()
            {
                UserId = result.UserId,
                Code = otpCode,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                LastResendAt = DateTime.UtcNow,
                Type = OtpType.EmailVerification
            };
            await _unitOfWork.Otps.AddAsync(otpEntity);

            await _unitOfWork.SaveChangesAsync();

            var emailBody = EmailTemplates.VerifyEmailOtpTemplate(otpCode, request.FirstName.Trim());
            await _emailService.SendAsync(request.Email, emailBody);

            return Result<RegisterResponseDto>.Success(new RegisterResponseDto
            {
                Message = "Registration successful. Please check your email for the verification code.",
                VerificationToken = otpEntity.Token
            });
        }

        private static string GenerateOtp()
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000);
            return number.ToString();
        }
    }
}
