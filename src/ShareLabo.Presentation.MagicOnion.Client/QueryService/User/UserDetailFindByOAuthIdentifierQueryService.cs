using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserDetailFindByOAuthIdentifierQueryService
        : MOSHQueryServiceClientBase<IMOUserDetailFindByOAuthIdentifierQueryService, IMOUserDetailFindByOAuthIdentifierQueryService.Req, IUserDetailFindByOAuthIdentifierQueryService.Req, IMOUserDetailFindByOAuthIdentifierQueryService.Res, IUserDetailFindByOAuthIdentifierQueryService.Res>
        , IUserDetailFindByOAuthIdentifierQueryService
    {
        public UserDetailFindByOAuthIdentifierQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
