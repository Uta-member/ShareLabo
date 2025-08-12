using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    [ExceptionFilter]
    public sealed class MOSelfAuthUserLoginCommandService
        : MOCommandServiceWithResBase<IMOSelfAuthUserLoginCommandService, ISelfAuthUserLoginCommandService, IMOSelfAuthUserLoginCommandService.Req, ISelfAuthUserLoginCommandService.Req, IMOSelfAuthUserLoginCommandService.Res, ISelfAuthUserLoginCommandService.Res>
        , IMOSelfAuthUserLoginCommandService
    {
        public MOSelfAuthUserLoginCommandService(ISelfAuthUserLoginCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult<IMOSelfAuthUserLoginCommandService.Res> Execute(
            IMOSelfAuthUserLoginCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
