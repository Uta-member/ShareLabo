namespace ShareLabo.Application.UseCase.QueryService.TimeLine
{
    public sealed record TimeLineSummaryReadModel
    {
        public required string OwnerId { get; init; }

        public required string TimeLineId { get; init; }

        public required string TimeLineName { get; init; }
    }
}
