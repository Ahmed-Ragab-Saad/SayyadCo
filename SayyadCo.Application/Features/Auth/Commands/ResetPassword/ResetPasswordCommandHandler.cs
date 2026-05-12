using MediatR;
using SayyadCo.Application.Common.Results;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces;

namespace SayyadCo.Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public ResetPasswordCommandHandler(IUnitOfWork unitOfWork, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<Result<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var otp = await _unitOfWork.Otps.GetByTokenAndTypeAsync(request.PasswordResetToken, OtpType.ForgotPassword);
            if (otp is null)
                return Result<bool>.NotFound("Invalid or expired reset token");

            if (!otp.IsUsed)
                return Result<bool>.Failure("Please verify your OTP first");

            var success = await _authService.ResetPasswordAsync(otp.UserId, request.NewPassword);
            if (!success)
                return Result<bool>.Failure("Failed to reset password");

            otp.Token = string.Empty;
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
    }
}
