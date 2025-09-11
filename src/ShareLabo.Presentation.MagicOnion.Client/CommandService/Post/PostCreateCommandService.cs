using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class PostCreateCommandService
        : MOSHCommandServiceClientBase<IMOPostCreateCommandService, IMOPostCreateCommandService.Req, IPostCreateCommandService.Req>
        , IPostCreateCommandService
    {
        public PostCreateCommandService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
