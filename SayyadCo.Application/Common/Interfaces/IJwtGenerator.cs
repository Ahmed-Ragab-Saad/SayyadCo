using SayyadCo.Application.Common.Models;
using System.Security.Claims;

namespace SayyadCo.Application.Common.Interfaces
{
    public interface IJwtGenerator
    {
        TokenResult GenerateTokens(UserTokenModel user);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
    }
}
