using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.User
{
    public sealed class MOOAuthUserCreateCommandService
        : MOCommandServiceBase<
        IMOOAuthUserCreateCommandService, IOAuthUserCreateCommandService, IMOOAuthUserCreateCommandService.Req, IOAuthUserCreateCommandService.Req>
        ,
        IMOOAuthUserCreateCommandService
    {
        public MOOAuthUserCreateCommandService(IOAuthUserCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOOAuthUserCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
