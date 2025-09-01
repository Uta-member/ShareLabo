using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public interface IPostSummariesSearchQueryService
        : IQueryService<IPostSummariesSearchQueryService.Req, IPostSummariesSearchQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required int Length { get; init; }

            public string? StartPostId { get; init; }

            public bool ToBefore { get; init; } = true;
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required ImmutableList<PostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
