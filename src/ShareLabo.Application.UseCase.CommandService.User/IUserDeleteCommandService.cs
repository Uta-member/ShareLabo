using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface IUserDeleteCommandService : ICommandService<IUserDeleteCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfoDTO OperateInfo { get; init; }

            public required string TargetId { get; init; }
        }
    }
}
