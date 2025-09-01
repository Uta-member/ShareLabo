using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOUserDetailFindByAccountIdQueryService
        : MOQueryServiceBase<IMOUserDetailFindByAccountIdQueryService, IUserDetailFindByAccountIdQueryService, IMOUserDetailFindByAccountIdQueryService.Req, IUserDetailFindByAccountIdQueryService.Req, IMOUserDetailFindByAccountIdQueryService.Res, IUserDetailFindByAccountIdQueryService.Res>
        , IMOUserDetailFindByAccountIdQueryService
    {
        public MOUserDetailFindByAccountIdQueryService(IUserDetailFindByAccountIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserDetailFindByAccountIdQueryService.Res> Execute(
            IMOUserDetailFindByAccountIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
