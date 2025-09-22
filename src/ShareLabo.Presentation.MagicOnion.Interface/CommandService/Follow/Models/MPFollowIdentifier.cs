using MessagePack;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPFollowIdentifier
    {
        [Key(0)]
        public required string FollowFromId { get; init; }

        [Key(1)]
        public required string FollowToId { get; init; }
    }
}
