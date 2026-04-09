using SayyadCo.Domain.Entities;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IOtpRepository
    {
        Task AddAsync(OtpCode otp);
        Task<OtpCode?> GetActiveOtpAsync(string userId);
        Task<OtpCode?> GetByTokenAsync(string token);
        Task InvalidateUserOtpsAsync(string userId);
    }
}
