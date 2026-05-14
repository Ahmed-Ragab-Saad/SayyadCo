using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IExamUpdateRequestRepository : IRepository<ExamUpdateRequest>
    {
        Task<ExamUpdateRequest?> GetPendingByExamIdAsync(string examId);
        Task<PagedResult<ExamUpdateRequest>> GetPendingUpdatesAsync(QueryParameters parameters);
    }
}
