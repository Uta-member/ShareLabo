namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public sealed record PostSummaryReadModel
    {
        public required DateTime PostDateTime { get; init; }

        public required string PostId { get; init; }

        public required long PostSequenceId { get; init; }

        public required PostUserReadModel PostUser { get; init; }

        public required string Title { get; init; }
    }
}
