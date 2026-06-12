using SayyadCo.Application.Common.Models;

namespace SayyadCo.Application.Common.Interfaces
{
    public interface IFacebookAuthService
    {
        Task<ExternalUserInfo?> LoginWithCodeAsync(string code, string redirectUri);
    }
}
