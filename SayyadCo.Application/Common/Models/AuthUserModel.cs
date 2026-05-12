namespace SayyadCo.Application.Common.Models
{
    public class AuthUserModel
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? OtpLockedUntil { get; set; }
    }
}
