using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.TimeLine
{
    public interface ITimeLineCreateCommandService : ICommandService<ITimeLineCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required ImmutableList<UserId> FilterMembers { get; init; }

            public required TimeLineId Id { get; init; }

            public required TimeLineName Name { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId OwnerId { get; init; }
        }
    }
}
