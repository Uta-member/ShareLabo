using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface IUserDeleteCommandService : ICommandService<IUserDeleteCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }
        }
    }
}
