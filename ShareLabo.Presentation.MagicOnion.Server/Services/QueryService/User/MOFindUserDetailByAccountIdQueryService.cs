using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOFindUserDetailByAccountIdQueryService
        : MOQueryServiceBase<IMOFindUserDetailByAccountIdQueryService, IFindUserDetailByAccountIdQueryService, IMOFindUserDetailByAccountIdQueryService.Req, IFindUserDetailByAccountIdQueryService.Req, IMOFindUserDetailByAccountIdQueryService.Res, IFindUserDetailByAccountIdQueryService.Res>
        , IMOFindUserDetailByAccountIdQueryService
    {
        public MOFindUserDetailByAccountIdQueryService(IFindUserDetailByAccountIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOFindUserDetailByAccountIdQueryService.Res> Execute(
            IMOFindUserDetailByAccountIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
