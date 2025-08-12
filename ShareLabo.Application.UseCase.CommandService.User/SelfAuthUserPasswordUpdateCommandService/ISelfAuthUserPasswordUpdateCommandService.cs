using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserPasswordUpdateCommandService
        : ICommandService<ISelfAuthUserPasswordUpdateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required AccountPassword CurrentPassword { get; init; }

            public required AccountPassword NewPassword { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetUserId { get; init; }
        }
    }
}
