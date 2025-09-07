using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommanService.TimeLine
{
    public interface ITimeLineUpdateCommandService : ICommandService<ITimeLineUpdateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public Optional<ImmutableList<UserId>> FilterMembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public Optional<TimeLineName> NameOptional { get; init; } = Optional<TimeLineName>.Empty;

            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }
        }
    }
}
