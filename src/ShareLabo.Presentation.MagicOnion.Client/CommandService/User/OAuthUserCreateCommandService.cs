using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class OAuthUserCreateCommandService
        : MOSHCommandServiceClientBase<IMOOAuthUserCreateCommandService, IMOOAuthUserCreateCommandService.Req, IOAuthUserCreateCommandService.Req>
        , IOAuthUserCreateCommandService
    {
        public OAuthUserCreateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
