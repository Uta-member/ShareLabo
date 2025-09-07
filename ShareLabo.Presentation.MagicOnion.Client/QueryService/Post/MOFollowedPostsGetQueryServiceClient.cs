using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOFollowedPostsGetQueryServiceClient
        : MOSHQueryServiceClientBase<IMOFollowedPostsGetQueryService,
        IMOFollowedPostsGetQueryService.Req,
        IFollowedPostsGetQueryService.Req,
        IMOFollowedPostsGetQueryService.Res,
        IFollowedPostsGetQueryService.Res>
        ,
        IFollowedPostsGetQueryService
    {
        public MOFollowedPostsGetQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
