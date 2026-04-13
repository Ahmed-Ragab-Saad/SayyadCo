using SayyadCo.Application.Common.Models;

namespace SayyadCo.Application.Common.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<ExternalUserInfo?> VerifyTokenAsync(string accessToken);
    }
}
