namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public sealed record PostDetailReadModel
    {
        public required string Content { get; init; }

        public required DateTime PostDateTime { get; init; }

        public required string PostId { get; init; }

        public required PostUserReadModel PostUser { get; init; }

        public required string Title { get; init; }

        public required DateTime UpdateTimeStamp { get; init; }
    }
}
