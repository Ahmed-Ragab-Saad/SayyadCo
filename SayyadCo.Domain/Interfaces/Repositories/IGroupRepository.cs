using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<Group?> GetTeacherGroupAsync(string userId, string sectionId, string gameId);
    }
}
