using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MyPostsGetQueryService
        : MOSHQueryServiceClientBase<IMOMyPostsGetQueryService,
        IMOMyPostsGetQueryService.Req,
        IMyPostsGetQueryService.Req,
        IMOMyPostsGetQueryService.Res,
        IMyPostsGetQueryService.Res>
        ,
        IMyPostsGetQueryService
    {
        public MyPostsGetQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
