using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.TimeLine
{
    [ExceptionFilter]
    public sealed class MOTimeLineCreateCommandService
        : MOCommandServiceBase<IMOTimeLineCreateCommandService, ITimeLineCreateCommandService, IMOTimeLineCreateCommandService.Req, ITimeLineCreateCommandService.Req>
        , IMOTimeLineCreateCommandService
    {
        public MOTimeLineCreateCommandService(ITimeLineCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOTimeLineCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
