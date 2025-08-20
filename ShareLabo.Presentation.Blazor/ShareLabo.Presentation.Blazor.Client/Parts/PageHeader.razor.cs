using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.Blazor.Client.Auth;

namespace ShareLabo.Presentation.Blazor.Client.Parts
{
    public partial class PageHeader
    {
        private UserDetailReadModel? _userDetail;

        private void Logout()
        {
            NavigationManager.NavigateTo(ShareLaboPagePath.Helper.Logout(), true);
        }

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
        {
            var authenticationState = await AuthenticationState;

            var userIdStr = authenticationState.User.FindFirst(x => x.Type == ShareLaboClaim.UserId)?.Value;
            if(string.IsNullOrWhiteSpace(userIdStr))
            {
                _userDetail = null;
                StateHasChanged();
                return;
            }

            var userFindRes = await FindUserDetailByUserIdQueryService.ExecuteAsync(
                new IFindUserDetailByUserIdQueryService.Req()
                {
                    UserId = userIdStr,
                });

            if(!userFindRes.User.TryGetValue(out var userDetail))
            {
                _userDetail = null;
                StateHasChanged();
                return;
            }

            _userDetail = userDetail;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            OnAuthenticationStateChanged(AuthenticationState);
            AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;
        }


        [CascadingParameter]
        public required Task<AuthenticationState> AuthenticationState { get; set; }

        [Inject]
        public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        public required IFindUserDetailByUserIdQueryService FindUserDetailByUserIdQueryService { get; set; }
    }
}
