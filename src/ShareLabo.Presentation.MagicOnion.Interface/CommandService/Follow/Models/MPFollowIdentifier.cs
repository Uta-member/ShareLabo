using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Follow;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPFollowIdentifier : IMPDTO<FollowIdentifierDTO, MPFollowIdentifier>
    {
        public static MPFollowIdentifier FromDTO(FollowIdentifierDTO dto)
        {
            return new MPFollowIdentifier
            {
                FollowFromId = dto.FollowFromId,
                FollowToId = dto.FollowToId,
            };
        }

        public FollowIdentifierDTO ToDTO()
        {
            return new FollowIdentifierDTO
            {
                FollowFromId = FollowFromId,
                FollowToId = FollowToId,
            };
        }

        [Key(0)]
        public required string FollowFromId { get; init; }

        [Key(1)]
        public required string FollowToId { get; init; }
    }
}
