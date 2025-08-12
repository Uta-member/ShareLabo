using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using ShareLabo.Presentation.Blazor.Client;
using ShareLabo.Presentation.Blazor.Client.Auth;
using System.Diagnostics;

namespace ShareLabo.Presentation.Blazor.Auth
{
    internal sealed class PersistingServerAuthenticationStateProvider : ServerAuthenticationStateProvider, IDisposable
    {
        private Task<AuthenticationState>? authenticationStateTask;
        private readonly PersistentComponentState state;

        private readonly PersistingComponentStateSubscription subscription;

        public PersistingServerAuthenticationStateProvider(
            PersistentComponentState persistentComponentState)
        {
            state = persistentComponentState;

            AuthenticationStateChanged += OnAuthenticationStateChanged;
            subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        private void OnAuthenticationStateChanged(Task<AuthenticationState> task)
        {
            authenticationStateTask = task;
        }

        private async Task OnPersistingAsync()
        {
            if(authenticationStateTask is null)
            {
                throw new UnreachableException($"Authentication state not set in {nameof(OnPersistingAsync)}().");
            }

            var authenticationState = await authenticationStateTask;
            var principal = authenticationState.User;

            if(principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(ShareLaboClaim.UserId)?.Value;
                var accountId = principal.FindFirst(ShareLaboClaim.AccountId)?.Value;
                var userName = principal.FindFirst(ShareLaboClaim.UserName)?.Value;

                if(userId != null && accountId != null && userName != null)
                {
                    state.PersistAsJson(
                        nameof(UserInfo),
                        new UserInfo
                        {
                            UserId = userId,
                            AccountId = accountId,
                            UserName = userName,
                        });
                }
            }
        }

        public void Dispose()
        {
            subscription.Dispose();
            AuthenticationStateChanged -= OnAuthenticationStateChanged;
        }
    }
}
