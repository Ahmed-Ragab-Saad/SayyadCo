using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class UserGame : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;
        public string GameRoleId { get; set; } = string.Empty;

        public SectionGame SectionGame { get; set; } = null!;
        public GameRole GameRole { get; set; } = null!;
    }
}
//VX4ETT9U
