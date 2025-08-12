using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOFindUserDetailByUserIdQueryService
        : MOQueryServiceBase<IMOFindUserDetailByUserIdQueryService, IFindUserDetailByUserIdQueryService, IMOFindUserDetailByUserIdQueryService.Req, IFindUserDetailByUserIdQueryService.Req, IMOFindUserDetailByUserIdQueryService.Res, IFindUserDetailByUserIdQueryService.Res>
        ,
        IMOFindUserDetailByUserIdQueryService
    {
        public MOFindUserDetailByUserIdQueryService(IFindUserDetailByUserIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOFindUserDetailByUserIdQueryService.Res> Execute(
            IMOFindUserDetailByUserIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
