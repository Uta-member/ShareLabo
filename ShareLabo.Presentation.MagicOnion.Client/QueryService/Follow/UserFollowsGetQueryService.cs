using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserFollowsGetQueryService
        : MOSHQueryServiceClientBase<IMOUserFollowsGetQueryService, IMOUserFollowsGetQueryService.Req, IUserFollowsGetQueryService.Req, IMOUserFollowsGetQueryService.Res, IUserFollowsGetQueryService.Res>
        , IUserFollowsGetQueryService
    {
        public UserFollowsGetQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
