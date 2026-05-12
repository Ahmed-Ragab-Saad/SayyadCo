using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.VerifyResetOtp
{
    public class VerifyResetOtpCommandHandler : IRequestHandler<VerifyResetOtpCommand, Result<VerifyResetOtpResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public VerifyResetOtpCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<VerifyResetOtpResponseDto>> Handle(VerifyResetOtpCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Otps.GetByTokenAndTypeAsync(request.ResetToken, OtpType.ForgotPassword);
            if (otp is null)
                return Result<VerifyResetOtpResponseDto>.NotFound("Invalid reset token");

            if (otp.IsLocked)
                return Result<VerifyResetOtpResponseDto>.Failure(
                    $"Too many attempts. Try again after {otp.LockedUntil!.Value:HH:mm:ss} UTC");

            if (otp.IsExpired)
                return Result<VerifyResetOtpResponseDto>.Failure("OTP has expired");

            if (otp.Code != request.OtpCode)
            {
                otp.FailedAttempts++;

                if (otp.FailedAttempts >= 5)
                    otp.LockedUntil = DateTime.UtcNow.AddMinutes(5);

                await _unitOfWork.SaveChangesAsync();

                var remaining = 5 - otp.FailedAttempts;
                return remaining > 0
                    ? Result<VerifyResetOtpResponseDto>.Failure($"Invalid OTP. {remaining} attempts remaining")
                    : Result<VerifyResetOtpResponseDto>.Failure("Too many failed attempts. Try again in 5 minutes");
            }

            otp.IsUsed = true;
            otp.Token = Guid.NewGuid().ToString();
            await _unitOfWork.SaveChangesAsync();

            return Result<VerifyResetOtpResponseDto>.Success(new VerifyResetOtpResponseDto
            {
                PasswordResetToken = otp.Token
            });
        }
    }
}
