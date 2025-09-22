using CSStack.TADA;
using ShareLabo.Application.Toolkit;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.TimeLine
{
    public interface ITimeLineCreateCommandService : ICommandService<ITimeLineCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required ImmutableList<string> FilterMembers { get; init; }

            public required string Id { get; init; }

            public required string Name { get; init; }

            public required OperateInfoWriteModel OperateInfo { get; init; }

            public required string OwnerId { get; init; }
        }
    }
}
