using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.TimeLine
{
    [ExceptionFilter]
    public sealed class MOUserTimeLinesGetQueryService
        : MOQueryServiceBase<IMOUserTimeLinesGetQueryService, IUserTimeLinesGetQueryService, IMOUserTimeLinesGetQueryService.Req, IUserTimeLinesGetQueryService.Req, IMOUserTimeLinesGetQueryService.Res, IUserTimeLinesGetQueryService.Res>
        , IMOUserTimeLinesGetQueryService
    {
        public MOUserTimeLinesGetQueryService(IUserTimeLinesGetQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOUserTimeLinesGetQueryService.Res> Execute(
            IMOUserTimeLinesGetQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
