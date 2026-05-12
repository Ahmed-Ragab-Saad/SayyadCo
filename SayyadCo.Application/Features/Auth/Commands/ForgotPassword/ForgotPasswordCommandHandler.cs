using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Common.Templates;
using SayyadCo.Application.Features.Auth.Commands.ForgetPassword;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Auth.Commands.ForgotPassword
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result<ForgotPasswordResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public ForgotPasswordCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<Result<ForgotPasswordResponseDto>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _authService.FindByEmailAsync(request.Email);
            if (user is null)
                return Result<ForgotPasswordResponseDto>.NotFound("Email does not exists");

            if (user.OtpLockedUntil.HasValue && user.OtpLockedUntil.Value > DateTime.UtcNow)
            {
                var remaining = user.OtpLockedUntil.Value - DateTime.UtcNow;
                return Result<ForgotPasswordResponseDto>
                    .Failure($"Too many failed attempts. Try again after {(int)remaining.TotalMinutes} minutes");
            }

            var otps = await _unitOfWork.Otps.GetByUserIdAndType(user.Id, OtpType.ForgotPassword);
            foreach (var otp in otps)
            {
                otp.IsUsed = true;
            }

            var otpCode = GenerateOtp();

            var otpEntity = new OtpCode()
            {
                UserId = user.Id,
                Code = otpCode,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                LastResendAt = DateTime.UtcNow,
                Type = OtpType.ForgotPassword
            };
            await _unitOfWork.Otps.AddAsync(otpEntity);

            var emailBody = EmailTemplates.ForgotPasswordOtpTemplate(otpCode, user.FirstName.Trim());
            await _emailService.SendAsync(request.Email, emailBody);
            await _unitOfWork.SaveChangesAsync();

            return Result<ForgotPasswordResponseDto>.Success(new ForgotPasswordResponseDto()
            {
                ResetToken = otpEntity.Token
            });
        }

        private static string GenerateOtp()
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000);
            return number.ToString();
        }
    }
}
