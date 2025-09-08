using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Group
{
    public sealed class MOGroupUpdateCommandService
        : MOCommandServiceBase<IMOGroupUpdateCommandService, IGroupUpdateCommandService, IMOGroupUpdateCommandService.Req, IGroupUpdateCommandService.Req>
        , IMOGroupUpdateCommandService
    {
        public MOGroupUpdateCommandService(IGroupUpdateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOGroupUpdateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
