using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    internal class CodeRepository : Repository<Code>, ICodeRepository
    {
        public CodeRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Code?> GetByValueAsync(string value)
            => await _dbSet.Include(c => c.GameRole)
                .Include(c => c.SectionGamePlan)
                    .ThenInclude(sgp => sgp.Plan)
                .Include(c => c.SectionGame)
                    .ThenInclude(sg => sg.Section)
                .Include(c => c.SectionGame)
                    .ThenInclude(sg => sg.Game)
                .FirstOrDefaultAsync(c => c.Value == value);

        public async Task<IEnumerable<Code>> GetBySectionGameAsync(string sectionId, string gameId)
            => await _dbSet
                .Include(c => c.GameRole)
                .AsNoTracking()
                .Where(c => c.SectionId == sectionId && c.GameId == gameId)
                .ToListAsync();

    }
}
