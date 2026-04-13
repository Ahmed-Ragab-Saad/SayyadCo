using SayyadCo.Domain.Interfaces.Repositories;

namespace SayyadCo.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRefreshTokenRepository RefreshTokens { get; }
        IOtpRepository Otps { get; }
        ISectoinRepository Sections { get; }
        Task<int> SaveChangesAsync();
    }
}
