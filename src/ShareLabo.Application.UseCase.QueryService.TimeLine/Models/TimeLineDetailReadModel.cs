using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.QueryService.TimeLine
{
    public sealed record TimeLineDetailReadModel
    {
        public required string OwnerId { get; init; }

        public required string OwnerName { get; init; }

        public required ImmutableList<TimeLineFilterReadModel> TimeLineFilters { get; init; }

        public required string TimeLineId { get; init; }

        public required string TimeLineName { get; init; }
    }
}
