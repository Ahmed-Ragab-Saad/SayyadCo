using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IGroupMemberRepository : IRepository<GroupMember>
    {
        Task<bool> IsMemberAsync(string userId, string groupId);
    }
}
