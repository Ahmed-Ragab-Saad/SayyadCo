using Microsoft.AspNetCore.Identity;
using SayyadCo.Application.Common.Models;
using SayyadCo.Application.Features.Auth.Commands.Register;
using SayyadCo.Application.Interfaces;
using SayyadCo.Domain.Common;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> CheckPasswordAsync(string userId, string password)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<AuthUserModel?> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return null;

            return new AuthUserModel
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<IList<string>> GetRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return [];

            return await _userManager.GetRolesAsync(user);
        }

        public async Task<RegisterResultModel> RegisterAsync(RegisterCommand request, IList<string> roles)
        {
            var user = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return new RegisterResultModel
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description)
                };

            var rolesResult = await _userManager.AddToRolesAsync(user, roles);
            if (!rolesResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new RegisterResultModel
                {
                    Succeeded = false,
                    Errors = rolesResult.Errors.Select(e => e.Description)
                };
            }

            return new RegisterResultModel
            {
                Succeeded = true,
                UserId = user.Id
            };
        }

        public async Task<bool> ConfirmEmailAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            user.EmailConfirmed = true;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<AuthUserModel?> FindByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return null;

            return new AuthUserModel
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<bool> IsEmailConfirmedAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            return user.EmailConfirmed;
        }

        public async Task<bool> IsLockedOutAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
                return false;

            return await _userManager.IsLockedOutAsync(user);
        }

        public async Task<RegisterResultModel> RegisterExternalAsync(string firstName, string lastName, string email)
        {
            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                UserName = email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return new RegisterResultModel
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            var rolesResult = await _userManager.AddToRolesAsync(user, [AppRoles.User]);
            if (!rolesResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new RegisterResultModel
                {
                    Succeeded = false,
                    Errors = rolesResult.Errors.Select(e => e.Description)
                };
            }

            return new RegisterResultModel
            {
                Succeeded = true,
                UserId = user.Id
            };
        }

        public Task<bool> IsExternalUserAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
