using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOUserSummariesSearchQueryServiceClient
        : MOSHQueryServiceClientBase<IMOUserSummariesSearchQueryService, IMOUserSummariesSearchQueryService.Req, IUserSummariesSearchQueryService.Req, IMOUserSummariesSearchQueryService.Res, IUserSummariesSearchQueryService.Res>
        , IUserSummariesSearchQueryService
    {
        public MOUserSummariesSearchQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
