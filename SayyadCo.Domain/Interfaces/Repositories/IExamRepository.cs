using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task DeleteWithQuestionsAsync(string id);
        Task<PagedResult<Exam>> GetPendingExamsAsync(QueryParameters parameters);
    }
}
