using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public interface IFollowDeleteCommandService : ICommandService<IFollowDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required OperateInfo OperateInfo { get; init; }
        }
    }
}
