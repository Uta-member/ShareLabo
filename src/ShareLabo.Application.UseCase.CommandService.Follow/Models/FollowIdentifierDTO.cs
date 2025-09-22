namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public sealed record FollowIdentifierDTO
    {
        public required string FollowFromId { get; init; }

        public required string FollowToId { get; init; }
    }
}
