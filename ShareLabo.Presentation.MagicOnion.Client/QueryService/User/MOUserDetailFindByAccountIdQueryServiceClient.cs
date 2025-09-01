using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOUserDetailFindByAccountIdQueryServiceClient
        : MOSHQueryServiceClientBase<IMOUserDetailFindByAccountIdQueryService, IMOUserDetailFindByAccountIdQueryService.Req, IUserDetailFindByAccountIdQueryService.Req, IMOUserDetailFindByAccountIdQueryService.Res, IUserDetailFindByAccountIdQueryService.Res>
        , IUserDetailFindByAccountIdQueryService
    {
        public MOUserDetailFindByAccountIdQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
