using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public interface ITimeLinePostsGetQueryService
        : IQueryService<ITimeLinePostsGetQueryService.Req, ITimeLinePostsGetQueryService.Res>
    {
        sealed record Req : IQueryServiceDTO
        {
            public required int Length { get; init; }

            public long? StartPostSequenceId { get; init; }

            public required string TimeLineId { get; init; }

            public bool ToBefore { get; init; } = true;

            public required string UserId { get; init; }
        }

        sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<PostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
