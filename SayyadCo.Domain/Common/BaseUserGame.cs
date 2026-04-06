using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Common
{
    public abstract class BaseUserGame : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SectionId { get; set; } = string.Empty;
        public string GameId { get; set; } = string.Empty;

        public SectionGame SectionGame { get; set; } = null!;
    }
}
