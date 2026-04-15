using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ISectionGameRepository
    {
        Task AddAsync(SectionGame sectionGame);
        Task<SectionGame?> GetAsync(string sectionId, string gameId);
        Task<PagedResult<SectionGame>> GetBySectionIdAsync(string sectionId, QueryParameters parameters);
        Task<PagedResult<SectionGame>> GetFunnyGames(QueryParameters parameters);
        void Remove(SectionGame sectionGame);
    }
}
