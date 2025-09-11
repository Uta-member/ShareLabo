using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOUserSummariesSearchQueryService
        : MOQueryServiceBase<IMOUserSummariesSearchQueryService, IUserSummariesSearchQueryService, IMOUserSummariesSearchQueryService.Req, IUserSummariesSearchQueryService.Req, IMOUserSummariesSearchQueryService.Res, IUserSummariesSearchQueryService.Res>
        , IMOUserSummariesSearchQueryService
    {
        public MOUserSummariesSearchQueryService(IUserSummariesSearchQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserSummariesSearchQueryService.Res> Execute(
            IMOUserSummariesSearchQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
