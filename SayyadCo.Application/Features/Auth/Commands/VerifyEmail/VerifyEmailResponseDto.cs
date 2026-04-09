namespace SayyadCo.Application.Features.Auth.Commands.VerifyEmail
{
    public class VerifyEmailResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiry { get; set; }
    }
}