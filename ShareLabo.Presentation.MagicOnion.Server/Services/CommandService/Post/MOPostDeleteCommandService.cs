using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Post
{
    public sealed class MOPostDeleteCommandService
        : MOCommandServiceBase<IMOPostDeleteCommandService, IPostDeleteCommandService, IMOPostDeleteCommandService.Req, IPostDeleteCommandService.Req>
    {
        public MOPostDeleteCommandService(IPostDeleteCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOPostDeleteCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
