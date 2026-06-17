using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class PlanRepository : Repository<Plan>, IPlanRepository
    {
        public PlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistingByTitle(string title)
            => await _dbSet.AnyAsync(p => p.TitleAr == title || p.TitleEn == title);
    }
}
