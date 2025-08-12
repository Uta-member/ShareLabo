using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    [ExceptionFilter]
    public sealed class MOUserUpdateCommandService
        : MOCommandServiceBase<IMOUserUpdateCommandService, IUserUpdateCommandService, IMOUserUpdateCommandService.Req, IUserUpdateCommandService.Req>
        , IMOUserUpdateCommandService
    {
        public MOUserUpdateCommandService(IUserUpdateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOUserUpdateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
