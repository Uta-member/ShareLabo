using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.TimeLine
{
    public interface IUserTimeLinesGetQueryService
        : IQueryService<IUserTimeLinesGetQueryService.Req, IUserTimeLinesGetQueryService.Res>
    {
        sealed record Req : IQueryServiceDTO
        {
            public required string UserId { get; init; }
        }

        sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<TimeLineSummaryReadModel> TimeLineSummaries { get; init; }
        }
    }
}
