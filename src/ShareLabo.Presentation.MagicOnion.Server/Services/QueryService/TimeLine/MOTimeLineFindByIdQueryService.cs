using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.QueryService.TimeLine
{
    [ExceptionFilter]
    public sealed class MOTimeLineFindByIdQueryService
        : MOQueryServiceBase<IMOTimeLineFindByIdQueryService, ITimeLineFindByIdQueryService, IMOTimeLineFindByIdQueryService.Req, ITimeLineFindByIdQueryService.Req, IMOTimeLineFindByIdQueryService.Res, ITimeLineFindByIdQueryService.Res>
        , IMOTimeLineFindByIdQueryService
    {
        public MOTimeLineFindByIdQueryService(ITimeLineFindByIdQueryService queryService)
            : base(queryService)
        {
        }

        public override UnaryResult<IMOTimeLineFindByIdQueryService.Res> Execute(
            IMOTimeLineFindByIdQueryService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
