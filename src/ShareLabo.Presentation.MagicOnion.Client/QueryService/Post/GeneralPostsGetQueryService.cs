using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class GeneralPostsGetQueryService
        : MOSHQueryServiceClientBase<IMOGeneralPostsGetQueryService, IMOGeneralPostsGetQueryService.Req, IGeneralPostsGetQueryService.Req, IMOGeneralPostsGetQueryService.Res, IGeneralPostsGetQueryService.Res>
        , IGeneralPostsGetQueryService
    {
        public GeneralPostsGetQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
