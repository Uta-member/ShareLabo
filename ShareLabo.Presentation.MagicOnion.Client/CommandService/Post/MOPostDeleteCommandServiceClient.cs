using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOPostDeleteCommandServiceClient
        : MOSHCommandServiceClientBase<IMOPostDeleteCommandService, IMOPostDeleteCommandService.Req, IPostDeleteCommandService.Req>
        , IPostDeleteCommandService
    {
        public MOPostDeleteCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
