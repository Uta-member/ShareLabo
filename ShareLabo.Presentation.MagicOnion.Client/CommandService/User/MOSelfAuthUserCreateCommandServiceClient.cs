using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOSelfAuthUserCreateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOSelfAuthUserCreateCommandService, IMOSelfAuthUserCreateCommandService.Req, ISelfAuthUserCreateCommandService.Req>
        , ISelfAuthUserCreateCommandService
    {
        public MOSelfAuthUserCreateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
