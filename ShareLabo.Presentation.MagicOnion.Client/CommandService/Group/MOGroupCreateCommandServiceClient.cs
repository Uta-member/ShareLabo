using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOGroupCreateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOGroupCreateCommandService, IMOGroupCreateCommandService.Req, IGroupCreateCommandService.Req>
        , IGroupCreateCommandService
    {
        public MOGroupCreateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
