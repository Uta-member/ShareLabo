using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class SelfAuthUserCreateCommandService
        : MOSHCommandServiceClientBase<IMOSelfAuthUserCreateCommandService, IMOSelfAuthUserCreateCommandService.Req, ISelfAuthUserCreateCommandService.Req>
        , ISelfAuthUserCreateCommandService
    {
        public SelfAuthUserCreateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
