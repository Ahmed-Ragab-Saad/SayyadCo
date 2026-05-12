using MediatR;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Common.Templates;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;
using System.Security.Cryptography;

namespace SayyadCo.Application.Features.Auth.Commands.ResendResetPasswordOtp
{
    public class ResendResetPasswordOtpCommandHandler : IRequestHandler<ResendResetPasswordOtpCommand, Result<ResendResetPasswordOtpResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public ResendResetPasswordOtpCommandHandler(IUnitOfWork unitOfWork, IAuthService authService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<Result<ResendResetPasswordOtpResponseDto>> Handle(ResendResetPasswordOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Otps.GetByTokenAndTypeAsync(request.ResetToken, OtpType.ForgotPassword);
            if (otp is null)
                return Result<ResendResetPasswordOtpResponseDto>.NotFound("Invalid reset token");

            if (otp.IsLocked)
                return Result<ResendResetPasswordOtpResponseDto>.Failure(
                    $"Too many attempts. Try again after {otp.LockedUntil!.Value:HH:mm:ss} UTC");

            if (!otp.CanResend)
                return Result<ResendResetPasswordOtpResponseDto>.Failure("Please wait 1 minute before requesting a new code");

            var user = await _authService.FindByIdAsync(otp.UserId);
            if (user is null)
                return Result<ResendResetPasswordOtpResponseDto>.NotFound("User not found");

            await _unitOfWork.Otps.InvalidateUserOtpsByTypeAsync(otp.UserId, OtpType.ForgotPassword);

            var newOtpCode = GenerateOtp();
            var newOtpEntity = new OtpCode
            {
                UserId = otp.UserId,
                Code = newOtpCode,
                Type = OtpType.ForgotPassword,
                ExpiresAt = DateTime.UtcNow.AddMinutes(10),
                LastResendAt = DateTime.UtcNow
            };

            await _unitOfWork.Otps.AddAsync(newOtpEntity);
            await _unitOfWork.SaveChangesAsync();

            await _emailService.SendAsync(user.Email, EmailTemplates.ForgotPasswordOtpTemplate(newOtpCode, user.FirstName));

            return Result<ResendResetPasswordOtpResponseDto>.Success(new ResendResetPasswordOtpResponseDto
            {
                Message = "A new verification code has been sent to your email.",
                ResetToken = newOtpEntity.Token
            });
        }

        private static string GenerateOtp()
        {
            var number = RandomNumberGenerator.GetInt32(100000, 1000000);
            return number.ToString();
        }
    }
}
