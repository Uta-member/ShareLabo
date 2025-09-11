namespace ShareLabo.Domain.ValueObject
{
    public sealed record FollowIdentifier
    {
        public required UserId FollowFromId { get; init; }

        public required UserId FollowToId { get; init; }
    }
}
