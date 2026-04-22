using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        public GroupRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Group?> GetTeacherGroupAsync(string userId, string sectionId, string gameId)
            => await _dbSet
                .AsNoTracking()
                .FirstOrDefaultAsync(g =>
                    g.CreatedByUserId == userId &&
                    g.SectionId == sectionId &&
                    g.GameId == gameId);
    }
}
