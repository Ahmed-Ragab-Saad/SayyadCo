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

        public async Task<bool> ExistingByTitle(string title, string? id = null)
        {
            title = title.ToUpper();
            return await _dbSet.AnyAsync(p => p.Id != id && (p.TitleAr.ToUpper() == title || p.TitleEn.ToUpper() == title));
        }
    }
}
