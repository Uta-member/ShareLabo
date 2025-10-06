using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.QueryService.User;
using System.Security.Claims;

namespace ShareLabo.Presentation.Blazor.Client.Services
{
    public sealed class UserProfileService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private UserDetailReadModel? _userDetail;
        private readonly IUserDetailFindByOAuthIdentifierQueryService _userDetailFindByOAuthIdentifierQueryService;

        public UserProfileService(
            AuthenticationStateProvider authenticationStateProvider,
            IUserDetailFindByOAuthIdentifierQueryService userDetailFindByOAuthIdentifierQueryService)
        {
            _authenticationStateProvider = authenticationStateProvider;
            _userDetailFindByOAuthIdentifierQueryService = userDetailFindByOAuthIdentifierQueryService;
        }

        public async ValueTask<UserDetailReadModel> GetUserDetailAsync(bool reload = false)
        {
            if(!reload && _userDetail is not null)
            {
                return _userDetail;
            }

            var authenticationState = await _authenticationStateProvider.GetAuthenticationStateAsync();

            var userDetailRes = await _userDetailFindByOAuthIdentifierQueryService.ExecuteAsync(
                new IUserDetailFindByOAuthIdentifierQueryService.Req()
                {
                    OAuthType = OAuthType.Google,
                    OAuthIdentifier = authenticationState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!,
                });

            if(!userDetailRes.UserOptional.TryGetValue(out var userDetail))
            {
                throw new InvalidOperationException("ユーザ情報の取得に失敗しました。");
            }

            _userDetail = userDetail;
            return userDetail;
        }
    }
}
