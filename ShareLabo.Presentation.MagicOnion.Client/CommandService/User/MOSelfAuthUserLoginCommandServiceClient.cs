using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOSelfAuthUserLoginCommandServiceClient
        : MOSHCommandServiceWithResClientBase<IMOSelfAuthUserLoginCommandService, IMOSelfAuthUserLoginCommandService.Req, ISelfAuthUserLoginCommandService.Req, IMOSelfAuthUserLoginCommandService.Res, ISelfAuthUserLoginCommandService.Res>
        , ISelfAuthUserLoginCommandService
    {
        public MOSelfAuthUserLoginCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
