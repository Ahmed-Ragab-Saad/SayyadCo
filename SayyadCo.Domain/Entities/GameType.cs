using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class GameType : BaseEntity
    {
        public string NameAr { get; set; } = string.Empty;
        public string NameEn { get; set; } = string.Empty;

        // Navigation
        public ICollection<Game> Games { get; set; } = new List<Game>();
    }
}