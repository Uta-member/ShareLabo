namespace ShareLabo.Application.UseCase.QueryService.Follow
{
    public sealed record FollowReadModel
    {
        public required string AccountId { get; init; }

        public required DateTime FollowStartDateTime { get; init; }

        public required string UserId { get; init; }

        public required string UserName { get; init; }
    }
}
