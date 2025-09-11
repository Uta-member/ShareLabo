using CSStack.TADA.MagicOnionHelper.Server;
using MagicOnion;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Presentation.MagicOnion.Interface;

namespace ShareLabo.Presentation.MagicOnion.Server.Services.CommandService.Follow
{
    public sealed class MOFollowCreateCommandService
        : MOCommandServiceBase<IMOFollowCreateCommandService, IFollowCreateCommandService, IMOFollowCreateCommandService.Req, IFollowCreateCommandService.Req>
        , IMOFollowCreateCommandService
    {
        public MOFollowCreateCommandService(IFollowCreateCommandService commandService)
            : base(commandService)
        {
        }

        public override UnaryResult Execute(IMOFollowCreateCommandService.Req req)
        {
            return ExecuteCore(req);
        }
    }
}
