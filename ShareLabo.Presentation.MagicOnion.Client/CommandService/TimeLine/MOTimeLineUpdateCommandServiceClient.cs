using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOTimeLineUpdateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOTimeLineUpdateCommandService, IMOTimeLineUpdateCommandService.Req, ITimeLineUpdateCommandService.Req>
        , ITimeLineUpdateCommandService
    {
        public MOTimeLineUpdateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
