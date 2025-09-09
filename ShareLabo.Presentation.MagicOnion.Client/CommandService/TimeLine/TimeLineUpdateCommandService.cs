using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class TimeLineUpdateCommandService
        : MOSHCommandServiceClientBase<IMOTimeLineUpdateCommandService, IMOTimeLineUpdateCommandService.Req, ITimeLineUpdateCommandService.Req>
        , ITimeLineUpdateCommandService
    {
        public TimeLineUpdateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
