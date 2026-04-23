using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class GroupMemberRepository : Repository<GroupMember>, IGroupMemberRepository
    {
        public GroupMemberRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> IsMemberAsync(string userId, string groupId)
            => await _dbSet.AnyAsync(gm => gm.UserId == userId && gm.GroupId == groupId);
    }
}
