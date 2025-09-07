using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommanService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOTimeLineDeleteCommandServiceClient
        : MOSHCommandServiceClientBase<IMOTimeLineDeleteCommandService, IMOTimeLineDeleteCommandService.Req, ITimeLineDeleteCommandService.Req>
        , ITimeLineDeleteCommandService
    {
        public MOTimeLineDeleteCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
