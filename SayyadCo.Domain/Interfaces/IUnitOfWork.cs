using SayyadCo.Domain.Interfaces.Repositories;

namespace SayyadCo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRefreshTokenRepository RefreshTokens { get; }
        IOtpRepository Otps { get; }
        ISectoinRepository Sections { get; }
        IGameRepository Games { get; }
        Task<int> SaveChangesAsync();
    }
}
