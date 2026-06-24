using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ISectionGamePlanRepository
    {
        Task AddAsync(SectionGamePlan plan);
        Task<SectionGamePlan?> GetAsync(string planId, string sectionId, string gameId, PlanType planType);
        Task<IEnumerable<SectionGamePlan>> GetBySectionGameAsync(string sectionId, string gameId);
        void Remove(SectionGamePlan plan);
    }
}
