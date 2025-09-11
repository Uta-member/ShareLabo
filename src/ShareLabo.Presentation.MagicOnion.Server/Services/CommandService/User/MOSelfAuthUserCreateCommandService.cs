using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    [ExceptionFilter]
    public sealed class MOSelfAuthUserCreateCommandService
        : MOCommandServiceBase<IMOSelfAuthUserCreateCommandService, ISelfAuthUserCreateCommandService, IMOSelfAuthUserCreateCommandService.Req, ISelfAuthUserCreateCommandService.Req>
        , IMOSelfAuthUserCreateCommandService
    {
        public MOSelfAuthUserCreateCommandService(ISelfAuthUserCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOSelfAuthUserCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
