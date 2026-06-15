namespace SayyadCo.Application.Features.Auth.Commands.FacebookLogin
{
    public class FacebookLoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public string? Image { get; set; }
        public DateTime AccessTokenExpiry { get; set; }
        public bool IsNewUser { get; set; }
    }
}