using Microsoft.JSInterop;
using System.Text.Json;

namespace ShareLabo.Presentation.Blazor.Client.Services
{
    public sealed class LoginedUserStorageService
    {
        private const string USER_MODEL_KEY = "LoginedUserModel";

        private IJSRuntime _jsRuntime;

        public LoginedUserStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public event Action<LoginedUserModel?>? OnLoginedUserChanged;

        public event Action<LoginedUserModel>? OnLoginedUserInfoUpdated;

        public async ValueTask<LoginedUserModel?> GetLoginedUser()
        {
            var loginedUserStr = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", USER_MODEL_KEY);
            var loginedUser = JsonSerializer.Deserialize<LoginedUserModel>(loginedUserStr);

            return loginedUser;
        }

        public async ValueTask ResetLoginedUserToStorage()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", USER_MODEL_KEY);
            OnLoginedUserChanged?.Invoke(null);
        }

        public async ValueTask SetLoginedUserToStorage(LoginedUserModel loginedUser)
        {
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                USER_MODEL_KEY,
                JsonSerializer.Serialize(loginedUser));
            OnLoginedUserChanged?.Invoke(loginedUser);
        }

        public async ValueTask UpdateLoginedUserInfoToStorage(LoginedUserModel loginedUser)
        {
            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                USER_MODEL_KEY,
                JsonSerializer.Serialize(loginedUser));
            OnLoginedUserInfoUpdated?.Invoke(loginedUser);
        }

        public sealed record LoginedUserModel
        {
            public required string UserAccountId { get; init; }

            public required string UserId { get; init; }

            public required string UserName { get; init; }
        }
    }
}
