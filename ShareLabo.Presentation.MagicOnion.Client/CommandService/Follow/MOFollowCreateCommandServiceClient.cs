using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOFollowCreateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOFollowCreateCommandService, IMOFollowCreateCommandService.Req, IFollowCreateCommandService.Req>
        , IFollowCreateCommandService
    {
        public MOFollowCreateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
