using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public interface IGroupCreateCommandService : ICommandService<IGroupCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required GroupId GroupId { get; init; }

            public required GroupName GroupName { get; init; }

            public required ImmutableList<UserId> Members { get; init; }

            public required OperateInfo OperateInfo { get; init; }
        }
    }
}
