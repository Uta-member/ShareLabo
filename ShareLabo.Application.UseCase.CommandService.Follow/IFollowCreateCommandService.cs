using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public interface IFollowCreateCommandService : ICommandService<IFollowCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required OperateInfo OperateInfo { get; init; }
        }
    }
}
