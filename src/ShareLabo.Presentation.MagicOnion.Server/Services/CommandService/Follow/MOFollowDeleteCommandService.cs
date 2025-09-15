using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Follow
{
    [ExceptionFilter]
    public sealed class MOFollowDeleteCommandService
        : MOCommandServiceBase<IMOFollowDeleteCommandService, IFollowDeleteCommandService, IMOFollowDeleteCommandService.Req, IFollowDeleteCommandService.Req>
        , IMOFollowDeleteCommandService
    {
        public MOFollowDeleteCommandService(IFollowDeleteCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOFollowDeleteCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
