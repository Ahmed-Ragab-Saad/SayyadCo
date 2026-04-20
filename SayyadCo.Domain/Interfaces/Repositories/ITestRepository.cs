using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface ITestRepository : IRepository<Test>
    {
        Task<Test?> GetByIdWithQuestionsAsync(string id);
        Task<PagedResult<Test>> GetBySectionGameAsync(string sectionId, string gameId, string academicYearId,
            Semester semester, QueryParameters parameters);
    }
}
