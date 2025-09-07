using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommanService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOTimeLineCreateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOTimeLineCreateCommandService, IMOTimeLineCreateCommandService.Req, ITimeLineCreateCommandService.Req>
        , ITimeLineCreateCommandService
    {
        public MOTimeLineCreateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
