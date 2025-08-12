using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    [ExceptionFilter]
    public sealed class MOSelfAuthUserPasswordUpdateCommandService
        : MOCommandServiceBase<IMOSelfAuthUserPasswordUpdateCommandService, ISelfAuthUserPasswordUpdateCommandService, IMOSelfAuthUserPasswordUpdateCommandService.Req, ISelfAuthUserPasswordUpdateCommandService.Req>
        , IMOSelfAuthUserPasswordUpdateCommandService
    {
        public MOSelfAuthUserPasswordUpdateCommandService(ISelfAuthUserPasswordUpdateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOSelfAuthUserPasswordUpdateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
