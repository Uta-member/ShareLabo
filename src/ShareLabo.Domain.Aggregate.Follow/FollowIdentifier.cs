using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Follow
{
    public sealed record FollowIdentifier
    {
        public required UserId FollowFromId { get; init; }

        public required UserId FollowToId { get; init; }
    }
}
