using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ICodeRepository : IRepository<Code>
    {
        Task<Code?> GetByValueAsync(string value);
        Task<IEnumerable<Code>> GetBySectionGameAsync(string sectionId, string gameId);
    }
}
