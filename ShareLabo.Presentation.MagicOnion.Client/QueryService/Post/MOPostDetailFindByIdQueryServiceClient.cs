using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOPostDetailFindByIdQueryServiceClient
        : MOSHQueryServiceClientBase<IMOPostDetailFindByIdQueryService,
        IMOPostDetailFindByIdQueryService.Req,
        IPostDetailFindByIdQueryService.Req,
        IMOPostDetailFindByIdQueryService.Res,
        IPostDetailFindByIdQueryService.Res>
        , IPostDetailFindByIdQueryService
    {
        public MOPostDetailFindByIdQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
