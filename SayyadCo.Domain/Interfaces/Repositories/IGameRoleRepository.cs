using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IGameRoleRepository
    {
        Task<GameRole?> GetByIdAsync(string id);
        Task<string?> GetRoleNameAsync(string id);
        Task<bool> IsRoleNameExist(string roleName);
        Task AddAsync(GameRole gameRole);
    }
}
