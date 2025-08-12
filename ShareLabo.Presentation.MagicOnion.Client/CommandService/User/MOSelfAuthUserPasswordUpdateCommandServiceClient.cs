using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOSelfAuthUserPasswordUpdateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOSelfAuthUserPasswordUpdateCommandService, IMOSelfAuthUserPasswordUpdateCommandService.Req, ISelfAuthUserPasswordUpdateCommandService.Req>
        , ISelfAuthUserPasswordUpdateCommandService
    {
        public MOSelfAuthUserPasswordUpdateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
