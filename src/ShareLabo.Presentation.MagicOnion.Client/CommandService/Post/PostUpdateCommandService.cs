using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class PostUpdateCommandService
        : MOSHCommandServiceClientBase<IMOPostUpdateCommandService, IMOPostUpdateCommandService.Req, IPostUpdateCommandService.Req>
        , IPostUpdateCommandService
    {
        public PostUpdateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
