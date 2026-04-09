using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken token);
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task RevokeAsync(string token);
        Task RevokeAllUserTokensAsync(string userId);
    }
}
