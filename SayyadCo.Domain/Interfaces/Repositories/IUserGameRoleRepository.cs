namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IUserGameRoleRepository
    {
        Task AssignRoleToUserAsync(string userId, string gameRoleId, string sectionId, string gameId);
        Task RemoveRoleFromUserAsync(string userId, string gameRoleId, string sectionId, string gameId);
        Task<bool> UserHasRoleAsync(string userId, string gameRoleId, string sectionId, string gameId);
        Task<string?> GetUserRoleAsync(string userId, string sectionId, string gameId);
    }
}
