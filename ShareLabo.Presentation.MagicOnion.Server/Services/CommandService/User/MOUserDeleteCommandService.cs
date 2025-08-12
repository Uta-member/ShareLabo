using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    [ExceptionFilter]
    public sealed class MOUserDeleteCommandService
        : MOCommandServiceBase<IMOUserDeleteCommandService, IUserDeleteCommandService, IMOUserDeleteCommandService.Req, IUserDeleteCommandService.Req>
        , IMOUserDeleteCommandService
    {
        public MOUserDeleteCommandService(IUserDeleteCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOUserDeleteCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
