using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Follow
{
    public sealed class MOUserFollowersGetQueryService
        : MOQueryServiceBase<IMOUserFollowersGetQueryService, IUserFollowersGetQueryService, IMOUserFollowersGetQueryService.Req, IUserFollowersGetQueryService.Req, IMOUserFollowersGetQueryService.Res, IUserFollowersGetQueryService.Res>
        , IMOUserFollowersGetQueryService
    {
        public MOUserFollowersGetQueryService(IUserFollowersGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserFollowersGetQueryService.Res> Execute(
            IMOUserFollowersGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
