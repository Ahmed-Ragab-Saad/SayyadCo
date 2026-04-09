namespace SayyadCo.Application.Features.Auth.Commands.ResendOtp
{
    public class ResendOtpResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public string VerificationToken { get; set; } = string.Empty;
    }
}