using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.TimeLine
{
    public sealed class MOTimeLineUpdateCommandService
        : MOCommandServiceBase<IMOTimeLineUpdateCommandService, ITimeLineUpdateCommandService, IMOTimeLineUpdateCommandService.Req, ITimeLineUpdateCommandService.Req>
        , IMOTimeLineUpdateCommandService
    {
        public MOTimeLineUpdateCommandService(ITimeLineUpdateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOTimeLineUpdateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
