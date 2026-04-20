using SayyadCo.Domain.Entities;

namespace SayyadCo.Infrastructure.Identity
{
    public class UserGameRole
    {
        public string UserId { get; set; } = string.Empty;
        public string GameRoleId { get; set; } = string.Empty;
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;

        public ApplicationUser User { get; set; } = null!;
        public GameRole GameRole { get; set; } = null!;
        public SectionGame SectionGame { get; set; } = null!;
    }
}
