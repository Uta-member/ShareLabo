using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserDetailFindByAccountIdQueryService
        : MOSHQueryServiceClientBase<IMOUserDetailFindByAccountIdQueryService, IMOUserDetailFindByAccountIdQueryService.Req, IUserDetailFindByAccountIdQueryService.Req, IMOUserDetailFindByAccountIdQueryService.Res, IUserDetailFindByAccountIdQueryService.Res>
        , IUserDetailFindByAccountIdQueryService
    {
        public UserDetailFindByAccountIdQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
