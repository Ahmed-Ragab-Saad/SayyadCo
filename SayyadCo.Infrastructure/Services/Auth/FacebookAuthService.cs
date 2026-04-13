using Microsoft.Extensions.Options;
using SayyadCo.Application.Common.Interfaces;
using SayyadCo.Application.Common.Models;
using SayyadCo.Infrastructure.Identity;
using System.Text.Json;

namespace SayyadCo.Infrastructure.Services.Auth
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly FacebookAuthSettings _settings;
        private readonly HttpClient _httpClient;

        public FacebookAuthService(IOptions<FacebookAuthSettings> settings, HttpClient httpClient)
        {
            _settings = settings.Value;
            _httpClient = httpClient;
        }

        public async Task<ExternalUserInfo?> VerifyTokenAsync(string accessToken)
        {
            try
            {
                var appToken = $"{_settings.AppId}|{_settings.AppSecret}";
                var verifyUrl = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appToken}";

                var verifyResponse = await _httpClient.GetStringAsync(verifyUrl);
                var verifyData = JsonDocument.Parse(verifyResponse);
                var isValid = verifyData.RootElement
                    .GetProperty("data")
                    .GetProperty("is_valid")
                    .GetBoolean();

                if (!isValid)
                    return null;

                var userUrl = $"https://graph.facebook.com/me?fields=id,first_name,last_name,email,picture&access_token={accessToken}";
                var userResponse = await _httpClient.GetStringAsync(userUrl);
                var userData = JsonDocument.Parse(userResponse);

                return new ExternalUserInfo()
                {
                    Email = userData.RootElement.TryGetProperty("email", out var email)
                        ? email.GetString() ?? string.Empty
                        : string.Empty,
                    FirstName = userData.RootElement.TryGetProperty("first_name", out var firstName)
                        ? firstName.GetString() ?? string.Empty
                        : string.Empty,
                    LastName = userData.RootElement.TryGetProperty("last_name", out var lastName)
                        ? lastName.GetString() ?? string.Empty
                        : string.Empty,
                    Picture = userData.RootElement.TryGetProperty("picture", out var picture)
                        ? picture.GetProperty("data").GetProperty("url").GetString() ?? string.Empty
                        : string.Empty
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
