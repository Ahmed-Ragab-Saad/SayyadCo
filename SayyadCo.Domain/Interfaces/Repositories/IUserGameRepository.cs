using SayyadCo.Domain.Common;
using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IUserGameRepository : IRepository<UserGame>
    {
        Task<UserGame?> GetAsync(string userId, string sectionId, string gameId);
        Task<string?> GetUserRoleAsync(string userId, string sectionId, string gameId);
        Task<PagedResult<UserGame>> GetTeacherGamesAsync(string userId, QueryParameters parameters);
        Task<PagedResult<UserGame>> GetStudentGamesAsync(string userId, QueryParameters parameters);
        Task<PagedResult<FunnyGameDto>> GetFunnyGamesAsync(string userId, QueryParameters parameters);
    }
}
