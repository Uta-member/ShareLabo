using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class FollowCreateCommandService
        : MOSHCommandServiceClientBase<IMOFollowCreateCommandService, IMOFollowCreateCommandService.Req, IFollowCreateCommandService.Req>
        , IFollowCreateCommandService
    {
        public FollowCreateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
