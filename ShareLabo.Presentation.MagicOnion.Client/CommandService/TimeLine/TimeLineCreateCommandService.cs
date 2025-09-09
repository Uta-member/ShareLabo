using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class TimeLineCreateCommandService
        : MOSHCommandServiceClientBase<IMOTimeLineCreateCommandService, IMOTimeLineCreateCommandService.Req, ITimeLineCreateCommandService.Req>
        , ITimeLineCreateCommandService
    {
        public TimeLineCreateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
