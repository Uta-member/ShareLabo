using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Group
{
    public sealed class MOGroupDeleteCommandService
        : MOCommandServiceBase<IMOGroupDeleteCommandService, IGroupDeleteCommandService, IMOGroupDeleteCommandService.Req, IGroupDeleteCommandService.Req>
        , IMOGroupDeleteCommandService
    {
        public MOGroupDeleteCommandService(IGroupDeleteCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOGroupDeleteCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
