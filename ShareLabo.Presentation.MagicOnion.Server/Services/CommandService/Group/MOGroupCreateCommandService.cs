using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Group
{
    public sealed class MOGroupCreateCommandService
        : MOCommandServiceBase<IMOGroupCreateCommandService, IGroupCreateCommandService, IMOGroupCreateCommandService.Req, IGroupCreateCommandService.Req>
        , IMOGroupCreateCommandService
    {
        public MOGroupCreateCommandService(IGroupCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOGroupCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
