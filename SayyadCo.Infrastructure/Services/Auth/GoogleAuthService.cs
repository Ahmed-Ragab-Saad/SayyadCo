using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Infrastructure.Identity;

namespace SayyadCo.Infrastructure.Services.Auth
{
    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly GoogleAuthSettings _settings;

        public GoogleAuthService(IOptions<GoogleAuthSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<ExternalUserInfo?> VerifyTokenAsync(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { _settings.ClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

                return new ExternalUserInfo()
                {
                    Email = payload.Email,
                    FirstName = payload.GivenName ?? string.Empty,
                    LastName = payload.FamilyName ?? string.Empty,
                    Picture = payload.Picture ?? string.Empty
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
