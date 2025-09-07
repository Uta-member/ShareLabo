using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOPostUpdateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOPostUpdateCommandService, IMOPostUpdateCommandService.Req, IPostUpdateCommandService.Req>
        , IPostUpdateCommandService
    {
        public MOPostUpdateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
