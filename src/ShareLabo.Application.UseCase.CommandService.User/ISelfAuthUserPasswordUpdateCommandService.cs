using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserPasswordUpdateCommandService
        : ICommandService<ISelfAuthUserPasswordUpdateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required string CurrentPassword { get; init; }

            public required string NewPassword { get; init; }

            public required OperateInfoDTO OperateInfo { get; init; }

            public required string TargetUserId { get; init; }
        }
    }
}
