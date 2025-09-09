using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserFollowersGetQueryService
        : MOSHQueryServiceClientBase<IMOUserFollowersGetQueryService, IMOUserFollowersGetQueryService.Req, IUserFollowersGetQueryService.Req, IMOUserFollowersGetQueryService.Res, IUserFollowersGetQueryService.Res>
        , IUserFollowersGetQueryService
    {
        public UserFollowersGetQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
