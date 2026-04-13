using SayyadCo.Domain.Common;

namespace SayyadCo.Domain.Entities
{
    public class GroupMember : BaseEntity
    {
        public string GroupId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;

        // Navigations
        public Group Group { get; set; } = null!;
    }
}
