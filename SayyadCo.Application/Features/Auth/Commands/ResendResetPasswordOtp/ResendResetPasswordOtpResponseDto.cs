namespace SayyadCo.Application.Features.Auth.Commands.ResendResetPasswordOtp
{
    public class ResendResetPasswordOtpResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public string ResetToken { get; set; } = string.Empty;
    }
}