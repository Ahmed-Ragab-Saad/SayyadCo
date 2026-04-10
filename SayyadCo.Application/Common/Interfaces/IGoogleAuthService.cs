using SayyadCo.Application.Common.Models;

namespace SayyadCo.Application.Common.Interfaces
{
    public interface IGoogleAuthService
    {
        Task<ExternalUserInfo?> VerifyTokenAsync(string idToken);
    }
}
