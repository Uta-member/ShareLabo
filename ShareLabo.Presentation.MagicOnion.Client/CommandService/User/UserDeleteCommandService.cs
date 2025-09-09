using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserDeleteCommandService
        : MOSHCommandServiceClientBase<IMOUserDeleteCommandService, IMOUserDeleteCommandService.Req, IUserDeleteCommandService.Req>
        , IUserDeleteCommandService
    {
        public UserDeleteCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
