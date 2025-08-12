using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class MOFindUserDetailByUserIdQueryServiceClient
        : MOSHQueryServiceClientBase<IMOFindUserDetailByUserIdQueryService, IMOFindUserDetailByUserIdQueryService.Req, IFindUserDetailByUserIdQueryService.Req, IMOFindUserDetailByUserIdQueryService.Res, IFindUserDetailByUserIdQueryService.Res>
        ,
        IFindUserDetailByUserIdQueryService
    {
        public MOFindUserDetailByUserIdQueryServiceClient(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
