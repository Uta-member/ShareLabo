using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserUpdateCommandService
        : MOSHCommandServiceClientBase<IMOUserUpdateCommandService, IMOUserUpdateCommandService.Req, IUserUpdateCommandService.Req>
        , IUserUpdateCommandService
    {
        public UserUpdateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
