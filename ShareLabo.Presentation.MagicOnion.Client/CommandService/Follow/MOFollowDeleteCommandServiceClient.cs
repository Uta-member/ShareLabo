using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOFollowDeleteCommandServiceClient
        : MOSHCommandServiceClientBase<IMOFollowDeleteCommandService, IMOFollowDeleteCommandService.Req, IFollowDeleteCommandService.Req>
        , IFollowDeleteCommandService
    {
        public MOFollowDeleteCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
