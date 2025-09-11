using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Post
{
    [ExceptionFilter]
    public sealed class MOPostCreateCommandService
        : MOCommandServiceBase<IMOPostCreateCommandService, IPostCreateCommandService, IMOPostCreateCommandService.Req, IPostCreateCommandService.Req>
        , IMOPostCreateCommandService
    {
        public MOPostCreateCommandService(IPostCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOPostCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
