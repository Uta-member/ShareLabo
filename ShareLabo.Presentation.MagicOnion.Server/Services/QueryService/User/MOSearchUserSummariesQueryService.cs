using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.User
{
    [ExceptionFilter]
    public sealed class MOSearchUserSummariesQueryService
        : MOQueryServiceBase<IMOSearchUserSummariesQueryService, ISearchUserSummariesQueryService, IMOSearchUserSummariesQueryService.Req, ISearchUserSummariesQueryService.Req, IMOSearchUserSummariesQueryService.Res, ISearchUserSummariesQueryService.Res>
        ,
        IMOSearchUserSummariesQueryService
    {
        public MOSearchUserSummariesQueryService(ISearchUserSummariesQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOSearchUserSummariesQueryService.Res> Execute(
            IMOSearchUserSummariesQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
