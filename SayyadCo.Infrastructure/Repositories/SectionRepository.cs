using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class SectionRepository : Repository<Section>, ISectoinRepository
    {
        public SectionRepository(AppDbContext context) : base(context)
        {
        }

        protected override IQueryable<Section> GetAllQueryable()
            => _dbSet.Where(s => s.SectionType != SectionType.Funny);

        public async Task<Section?> GetFunnySection()
            => await _context.Sections.FirstOrDefaultAsync(s => s.SectionType == SectionType.Funny);
    }
}
