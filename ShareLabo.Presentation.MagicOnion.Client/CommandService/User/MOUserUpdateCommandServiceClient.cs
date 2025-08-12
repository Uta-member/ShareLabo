using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOUserUpdateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOUserUpdateCommandService, IMOUserUpdateCommandService.Req, IUserUpdateCommandService.Req>
        , IUserUpdateCommandService
    {
        public MOUserUpdateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
