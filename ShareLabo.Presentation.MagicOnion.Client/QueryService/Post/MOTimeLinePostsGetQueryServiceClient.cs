using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOTimeLinePostsGetQueryServiceClient
        : MOSHQueryServiceClientBase<IMOTimeLinePostsGetQueryService,
        IMOTimeLinePostsGetQueryService.Req,
        ITimeLinePostsGetQueryService.Req,
        IMOTimeLinePostsGetQueryService.Res,
        ITimeLinePostsGetQueryService.Res>
        ,
        ITimeLinePostsGetQueryService
    {
        public MOTimeLinePostsGetQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
