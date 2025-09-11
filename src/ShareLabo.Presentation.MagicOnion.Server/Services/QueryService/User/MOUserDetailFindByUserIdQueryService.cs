using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOUserDetailFindByUserIdQueryService
        : MOQueryServiceBase<IMOUserDetailFindByUserIdQueryService, IUserDetailFindByUserIdQueryService, IMOUserDetailFindByUserIdQueryService.Req, IUserDetailFindByUserIdQueryService.Req, IMOUserDetailFindByUserIdQueryService.Res, IUserDetailFindByUserIdQueryService.Res>
        , IMOUserDetailFindByUserIdQueryService
    {
        public MOUserDetailFindByUserIdQueryService(IUserDetailFindByUserIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserDetailFindByUserIdQueryService.Res> Execute(
            IMOUserDetailFindByUserIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
