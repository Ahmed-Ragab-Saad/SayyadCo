namespace SayyadCo.Application.Features.Auth.Commands.GoogleLogin
{
    public class GoogleLoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiry { get; set; }
        public bool IsNewUser { get; set; }
    }
}