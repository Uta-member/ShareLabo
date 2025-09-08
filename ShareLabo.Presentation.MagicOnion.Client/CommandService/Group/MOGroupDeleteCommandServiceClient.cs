using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOGroupDeleteCommandServiceClient
        : MOSHCommandServiceClientBase<IMOGroupDeleteCommandService, IMOGroupDeleteCommandService.Req, IGroupDeleteCommandService.Req>
        , IGroupDeleteCommandService
    {
        public MOGroupDeleteCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
