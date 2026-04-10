using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Auth.Commands.Register;

namespace SayyadCo.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthUserModel?> FindByEmailAsync(string email);
        Task<RegisterResultModel> RegisterAsync(RegisterCommand request, IList<string> roles);
        Task<bool> CheckPasswordAsync(string userId, string password);
        Task<IList<string>> GetRolesAsync(string userId);
        Task<bool> ConfirmEmailAsync(string userId);
        Task<AuthUserModel?> FindByIdAsync(string userId);
        Task<bool> IsEmailConfirmedAsync(string userId);
        Task<bool> IsLockedOutAsync(string userId);
        Task<RegisterResultModel> RegisterExternalAsync(string firstName, string lastName, string email);
        Task<bool> IsExternalUserAsync(string userId);
    }
}
