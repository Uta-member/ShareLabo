namespace ShareLabo.Application.UseCase.QueryService.Post
{
    public sealed record PostUserReadModel
    {
        public required string PostUserId { get; init; }

        public required string PostUserName { get; init; }
    }
}
