using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ISectionGamePlanRepository
    {
        Task AddAsync(SectionGamePlan plan);
        Task<SectionGamePlan?> GetAsync(string planId, string sectionId, string gameId, string gameRoleId);
        Task<IEnumerable<SectionGamePlan>> GetBySectionGameAsync(string sectionId, string gameId);
        void Remove(SectionGamePlan plan);
        Task<SectionGamePlan?> GetByIdWithPlanAsync(string id);
    }
}
