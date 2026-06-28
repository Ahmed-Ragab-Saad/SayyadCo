using Microsoft.AspNetCore.Identity;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime? OtpLockedUntil { get; set; }
        public bool IsExternalImage { get; set; } = true;

        public ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<OtpCode> OtpCodes { get; set; } = new List<OtpCode>();
    }
}
