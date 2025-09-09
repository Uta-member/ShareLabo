using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class SelfAuthUserPasswordUpdateCommandService
        : MOSHCommandServiceClientBase<IMOSelfAuthUserPasswordUpdateCommandService, IMOSelfAuthUserPasswordUpdateCommandService.Req, ISelfAuthUserPasswordUpdateCommandService.Req>
        , ISelfAuthUserPasswordUpdateCommandService
    {
        public SelfAuthUserPasswordUpdateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
