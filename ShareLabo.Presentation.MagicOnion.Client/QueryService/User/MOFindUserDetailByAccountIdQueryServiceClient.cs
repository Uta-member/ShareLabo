using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOFindUserDetailByAccountIdQueryServiceClient
        : MOSHQueryServiceClientBase<IMOFindUserDetailByAccountIdQueryService, IMOFindUserDetailByAccountIdQueryService.Req, IFindUserDetailByAccountIdQueryService.Req, IMOFindUserDetailByAccountIdQueryService.Res, IFindUserDetailByAccountIdQueryService.Res>
        ,
        IFindUserDetailByAccountIdQueryService
    {
        public MOFindUserDetailByAccountIdQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
