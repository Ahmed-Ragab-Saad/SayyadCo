using SayyadCo.Domain.Entities;
using SayyadCo.Domain.Enums;

namespace SayyadCo.Domain.Interfaces.Repositories
{
    public interface IOtpRepository
    {
        Task AddAsync(OtpCode otp);
        Task<OtpCode?> GetActiveOtpAsync(string userId);
        Task<OtpCode?> GetByTokenAsync(string token);
        Task<OtpCode?> GetByTokenAndTypeAsync(string token, OtpType type);
        Task InvalidateUserOtpsAsync(string userId);
        Task<IEnumerable<OtpCode>> GetByUserIdAndType(string userId, OtpType type);
        Task InvalidateUserOtpsByTypeAsync(string userId, OtpType type);
    }
}
