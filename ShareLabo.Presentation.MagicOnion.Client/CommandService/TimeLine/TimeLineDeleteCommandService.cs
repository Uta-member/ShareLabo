using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class TimeLineDeleteCommandService
        : MOSHCommandServiceClientBase<IMOTimeLineDeleteCommandService, IMOTimeLineDeleteCommandService.Req, ITimeLineDeleteCommandService.Req>
        , ITimeLineDeleteCommandService
    {
        public TimeLineDeleteCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
