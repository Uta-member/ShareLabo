using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOSearchUserSummariesQueryServiceClient
        : MOSHQueryServiceClientBase<IMOSearchUserSummariesQueryService, IMOSearchUserSummariesQueryService.Req, ISearchUserSummariesQueryService.Req, IMOSearchUserSummariesQueryService.Res, ISearchUserSummariesQueryService.Res>
        ,
        ISearchUserSummariesQueryService
    {
        public MOSearchUserSummariesQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
