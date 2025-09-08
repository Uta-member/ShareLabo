using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public interface IGroupUpdateCommandService : ICommandService<IGroupUpdateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public Optional<GroupName> GroupNameOptional { get; init; } = Optional<GroupName>.Empty;

            public Optional<ImmutableList<UserId>> MembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public required OperateInfo OperateInfo { get; init; }

            public required GroupId TargetId { get; init; }
        }
    }
}
