using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.ValueObject;
using ShareLabo.Presentation.Blazor.Client;
using ShareLabo.Presentation.Blazor.Client.Auth;
using System.Security.Claims;

namespace ShareLabo.Presentation.Blazor.Components.Pages.Accounts
{
    [Route(ShareLaboPagePath.Login)]
    public partial class LoginPage
    {
        private string _message = string.Empty;

        private async Task LoginAsync(EditContext editContext)
        {
            _ = HttpContext ?? throw new InvalidOperationException("Static SSR で実行してください。");

            _message = string.Empty;

            try
            {
                if(HttpContext.User?.Identity?.IsAuthenticated ?? false)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
                var authResult = await SelfAuthUserLoginCommandService.ExecuteAsync(
                    new ISelfAuthUserLoginCommandService.Req()
                    {
                        AccountPassword = AccountPassword.Reconstruct(_loginViewModel.Password),
                        UserAccountId = UserAccountId.Reconstruct(_loginViewModel.AccountId),
                    });

                if(!authResult.IsAuthorized)
                {
                    throw new Exception();
                }

                var userRes = await FindUserDetailByAccountIdQueryService.ExecuteAsync(
                    new IFindUserDetailByAccountIdQueryService.Req() { AccountId = _loginViewModel.AccountId });
                if(!userRes.User.TryGetValue(out var user))
                {
                    throw new Exception();
                }

                var claims = new List<Claim>
                {
                    new Claim(ShareLaboClaim.UserId, user.UserId),
                    new Claim(ShareLaboClaim.AccountId, user.UserAccountId),
                    new Claim(ShareLaboClaim.UserName, user.UserName),
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                NavigationManager.NavigateTo(ReturnUrl ?? ShareLaboPagePath.Helper.Home(), forceLoad: true);
            }
            catch
            {
                if(HttpContext.User?.Identity?.IsAuthenticated ?? false)
                {
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                }
                _message = "ログインに失敗しました";
            }
        }

        private EditContext _editContext { get; set; } = default!;

        [SupplyParameterFromForm]
        private LoginViewModel _loginViewModel { get; set; } = default!;

        [CascadingParameter]
        private HttpContext? HttpContext { get; set; }

        protected override void OnInitialized()
        {
            _loginViewModel ??= new();
            _editContext = new(_loginViewModel);
        }

        protected override string PageTitleCore => "ログイン";

        [Inject]
        public required IFindUserDetailByAccountIdQueryService FindUserDetailByAccountIdQueryService { get; set; }

        [SupplyParameterFromQuery(Name = "returnUrl")]
        public string? ReturnUrl { get; set; }

        [Inject]
        public required ISelfAuthUserLoginCommandService SelfAuthUserLoginCommandService { get; set; }

        public sealed class LoginViewModel
        {
            public string AccountId { get; set; } = string.Empty;

            public string Password { get; set; } = string.Empty;
        }
    }
}
