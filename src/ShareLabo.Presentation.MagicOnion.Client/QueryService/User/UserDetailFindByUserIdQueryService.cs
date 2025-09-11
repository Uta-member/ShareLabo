using CSStack.TADA.MagicOnionHelper.Client;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Client
{
    public sealed class UserDetailFindByUserIdQueryService
        : MOSHQueryServiceClientBase<IMOUserDetailFindByUserIdQueryService, IMOUserDetailFindByUserIdQueryService.Req, IUserDetailFindByUserIdQueryService.Req, IMOUserDetailFindByUserIdQueryService.Res, IUserDetailFindByUserIdQueryService.Res>
        ,
        IUserDetailFindByUserIdQueryService
    {
        public UserDetailFindByUserIdQueryService(IMOClientChannelFactory channelFactory)
            : base(channelFactory)
        {
        }
    }
}
