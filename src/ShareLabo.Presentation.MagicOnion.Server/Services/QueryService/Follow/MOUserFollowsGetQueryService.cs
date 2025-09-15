using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.Follow
{
    [ExceptionFilter]
    public sealed class MOUserFollowsGetQueryService
        : MOQueryServiceBase<IMOUserFollowsGetQueryService, IUserFollowsGetQueryService, IMOUserFollowsGetQueryService.Req, IUserFollowsGetQueryService.Req, IMOUserFollowsGetQueryService.Res, IUserFollowsGetQueryService.Res>
        , IMOUserFollowsGetQueryService
    {
        public MOUserFollowsGetQueryService(IUserFollowsGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserFollowsGetQueryService.Res> Execute(IMOUserFollowsGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
