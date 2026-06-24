using Microsoft.EntityFrameworkCore;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;
using SayyadCo.Domain.Interfaces.Repositories;
using SayyadCo.Infrastructure.Data;

namespace SayyadCo.Infrastructure.Repositories
{
    public class SectionGamePlanRepository : ISectionGamePlanRepository
    {
        private readonly AppDbContext _context;

        public SectionGamePlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(SectionGamePlan plan)
            => await _context.SectionGamePlans.AddAsync(plan);

        public async Task<SectionGamePlan?> GetAsync(string planId, string sectionId, string gameId, PlanType planType)
            => await _context.SectionGamePlans
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.PlanId == planId &&
                    p.SectionId == sectionId &&
                    p.GameId == gameId &&
                    p.PlanType == planType);

        public async Task<IEnumerable<SectionGamePlan>> GetBySectionGameAsync(string sectionId, string gameId)
            => await _context.SectionGamePlans
                .Include(p => p.Plan)
                .AsNoTracking()
                .Where(p => p.SectionId == sectionId && p.GameId == gameId)
                .ToListAsync();

        public void Remove(SectionGamePlan plan)
            => _context.SectionGamePlans.Remove(plan);
    }
}
