using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group?> GetTeacherGroupAsync(string userId, string sectionId, string gameId, string academicYearId, Semester semester);
    }
}
