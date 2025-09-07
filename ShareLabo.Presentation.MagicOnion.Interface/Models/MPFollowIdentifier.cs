using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPFollowIdentifier : IMPDTO<FollowIdentifier, MPFollowIdentifier>
    {
        public static MPFollowIdentifier FromDTO(FollowIdentifier dto)
        {
            return new MPFollowIdentifier()
            {
                FollowFromId = dto.FollowFromId.Value,
                FollowToId = dto.FollowToId.Value,
            };
        }

        public FollowIdentifier ToDTO()
        {
            return new FollowIdentifier()
            {
                FollowFromId = UserId.Reconstruct(FollowFromId),
                FollowToId = UserId.Reconstruct(FollowToId),
            };
        }

        [Key(0)]
        public required string FollowFromId { get; init; }

        [Key(1)]
        public required string FollowToId { get; init; }
    }
}
