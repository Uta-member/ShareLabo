using CSStack.TADA;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.Toolkit;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.Blazor.Client.Services;
using System.Security.Claims;

namespace ShareLabo.Presentation.Blazor.Client.Pages.User
{
    public partial class UserRegisterPage
    {
        private bool _isProcessing = false;
        private string _message = string.Empty;
        private UserRegisterViewModel _userRegisterViewModel = new();

        private async Task RegisterAsync()
        {
            _isProcessing = true;
            _message = string.Empty;
            await InvokeAsync(StateHasChanged);

            try
            {
                var userIdStr = Guid.NewGuid().ToString();
                var authenticationState = await AuthenticationStateTask;
                var isAuthenticated = authenticationState.User?.Identity?.IsAuthenticated ?? false;
                var oauthIdentifier = authenticationState.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(!isAuthenticated || string.IsNullOrWhiteSpace(oauthIdentifier))
                {
                    NotificationService.Notify("ユーザ登録", "認証されていないユーザです");
                    await Task.Delay(1000);
                    NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Home(), true);
                    return;
                }

                var userFindRes = await UserDetailFindByOAuthIdentifierQueryService.ExecuteAsync(
                    new IUserDetailFindByOAuthIdentifierQueryService.Req()
                    {
                        OAuthIdentifier = oauthIdentifier,
                        OAuthType = OAuthType.Google,
                    });

                if(userFindRes.UserOptional.HasValue)
                {
                    NotificationService.Notify("ユーザ登録", "登録済みユーザです");
                    await Task.Delay(1000);
                    NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Home(), true);
                    return;
                }

                await OAuthUserCreateCommandService.ExecuteAsync(
                    new IOAuthUserCreateCommandService.Req()
                    {
                        OAuthIdentifier = oauthIdentifier,
                        OAuthType = OAuthType.Google,
                        UserAccountId = _userRegisterViewModel.AccountId,
                        OperateInfo =
                            new OperateInfoDTO()
                                {
                                    OperatedDateTime = DateTime.Now,
                                    Operator = userIdStr,
                                },
                        UserId = userIdStr,
                        UserName = _userRegisterViewModel.Name,
                    });

                await HttpClient.PostAsync("/Account/RefreshSignIn", null);

                if(AuthenticationStateProvider is ApiAuthenticationStateProvider apiAuthProvider)
                {
                    apiAuthProvider.NotifyAuthenticationStateChanged();
                }

                NotificationService.Notify("ユーザ登録", "ユーザ登録が完了しました");
                await Task.Delay(1000);
                NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Home(), forceLoad: true);
            }
            catch(ObjectAlreadyExistException)
            {
                _message = "すでに存在するアカウントIDです";
            }
            catch
            {
                _message = "ユーザ登録に失敗しました";
            }
            finally
            {
                _isProcessing = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if(firstRender)
            {
                var authenticationState = await AuthenticationStateTask;
                var isAuthenticated = authenticationState.User?.Identity?.IsAuthenticated ?? false;
                var oauthIdentifier = authenticationState.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(!isAuthenticated || string.IsNullOrWhiteSpace(oauthIdentifier))
                {
                    NotificationService.Notify("ユーザ登録", "認証されていないユーザです");
                    await Task.Delay(1000);
                    NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Home(), true);
                    return;
                }

                var userFindRes = await UserDetailFindByOAuthIdentifierQueryService.ExecuteAsync(
                    new IUserDetailFindByOAuthIdentifierQueryService.Req()
                    {
                        OAuthIdentifier = oauthIdentifier,
                        OAuthType = OAuthType.Google,
                    });

                if(userFindRes.UserOptional.HasValue)
                {
                    NotificationService.Notify("ユーザ登録", "登録済みユーザです");
                    await Task.Delay(1000);
                    NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Home(), true);
                    return;
                }
            }
        }

        protected override string PageTitleCore => "ユーザ登録";

        [Inject]
        public required HttpClient HttpClient { get; set; }

        [Inject]
        public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [CascadingParameter]
        public required Task<AuthenticationState> AuthenticationStateTask { get; set; }

        [Inject]
        public required IOAuthUserCreateCommandService OAuthUserCreateCommandService { get; set; }

        [Inject]
        public required IUserDetailFindByOAuthIdentifierQueryService UserDetailFindByOAuthIdentifierQueryService
        {
            get;
            set;
        }

        private sealed record UserRegisterViewModel
        {
            public string AccountId { get; set; } = string.Empty;

            public string Name { get; set; } = string.Empty;
        }
    }
}
