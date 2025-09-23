using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ShareLabo.Presentation.Blazor.Client.Services
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _http;

        public ApiAuthenticationStateProvider(HttpClient http)
        {
            _http = http;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userInfo = await _http.GetFromJsonAsync<UserInfoDto>("/Account/UserInfo");

            ClaimsIdentity identity;
            if(userInfo is not null && userInfo.IsAuthenticated)
            {
                identity = new ClaimsIdentity(
                    userInfo.Claims.Select(c => new Claim(c.Type, c.Value)),
                    "Server authentication");
            }
            else
            {
                identity = new ClaimsIdentity();
            }

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private class UserInfoDto
        {
            public List<ClaimDto> Claims { get; set; } = new();

            public bool IsAuthenticated { get; set; }

            public string? Name { get; set; }
        }

        private class ClaimDto
        {
            public string Type { get; set; } = string.Empty;

            public string Value { get; set; } = string.Empty;
        }
    }
}
