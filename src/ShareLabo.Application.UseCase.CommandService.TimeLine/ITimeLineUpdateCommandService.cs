using CSStack.TADA;
using ShareLabo.Application.Toolkit;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.TimeLine
{
    public interface ITimeLineUpdateCommandService : ICommandService<ITimeLineUpdateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public Optional<ImmutableList<string>> FilterMembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<string>>.Empty;

            public Optional<string> NameOptional { get; init; } = Optional<string>.Empty;

            public required OperateInfoWriteModel OperateInfo { get; init; }

            public required string TargetId { get; init; }
        }
    }
}
