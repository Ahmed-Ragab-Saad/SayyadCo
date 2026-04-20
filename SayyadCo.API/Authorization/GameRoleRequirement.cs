using Microsoft.AspNetCore.Authorization;

namespace SayyadCo.API.Authorization
{
    public class GameRoleRequirement : IAuthorizationRequirement
    {
        public string[] AllowedRoles { get; }

        public GameRoleRequirement(params string[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
    }
}
