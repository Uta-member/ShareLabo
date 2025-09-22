using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.Blazor.Client.Parts
{
    public partial class PageHeader
    {
        private UserDetailReadModel? _userDetail;

        private async void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
        {
            var authenticationState = await AuthenticationState;

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
        public required IUserDetailFindByUserIdQueryService FindUserDetailByUserIdQueryService { get; set; }
    }
}
