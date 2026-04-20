using SayyadCo.Application.Interfaces;

namespace SayyadCo.Application.Common.Models
{
    public interface IGameAccessService
    {
        Task<GameAccessResult> CheckAccessAsync(string userId, string sectionId, string gameId);
    }
}
