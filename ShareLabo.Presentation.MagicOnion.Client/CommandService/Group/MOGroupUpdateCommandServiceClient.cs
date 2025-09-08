using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOGroupUpdateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOGroupUpdateCommandService, IMOGroupUpdateCommandService.Req, IGroupUpdateCommandService.Req>
        , IGroupUpdateCommandService
    {
        public MOGroupUpdateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
