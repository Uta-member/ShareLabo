using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.TimeLine
{
    public sealed class MOTimeLineDeleteCommandService
        : MOCommandServiceBase<IMOTimeLineDeleteCommandService, ITimeLineDeleteCommandService, IMOTimeLineDeleteCommandService.Req, ITimeLineDeleteCommandService.Req>
        , IMOTimeLineDeleteCommandService
    {
        public MOTimeLineDeleteCommandService(ITimeLineDeleteCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOTimeLineDeleteCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
