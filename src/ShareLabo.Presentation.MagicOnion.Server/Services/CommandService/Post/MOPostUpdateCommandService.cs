using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Post
{
    [ExceptionFilter]
    public class MOPostUpdateCommandService
        : MOCommandServiceBase<IMOPostUpdateCommandService,
        IPostUpdateCommandService,
        IMOPostUpdateCommandService.Req,
        IPostUpdateCommandService.Req>
        , IMOPostUpdateCommandService
    {
        public MOPostUpdateCommandService(IPostUpdateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOPostUpdateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
