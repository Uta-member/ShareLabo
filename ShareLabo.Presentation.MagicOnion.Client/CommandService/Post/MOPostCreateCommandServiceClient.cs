using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOPostCreateCommandServiceClient
        : MOSHCommandServiceClientBase<IMOPostCreateCommandService, IMOPostCreateCommandService.Req, IPostCreateCommandService.Req>
        , IPostCreateCommandService
    {
        public MOPostCreateCommandServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
