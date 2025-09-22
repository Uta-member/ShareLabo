using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPPostUserReadModel : IMPDTO<PostUserReadModel, MPPostUserReadModel>
    {
        public static MPPostUserReadModel FromDTO(PostUserReadModel dto)
        {
            return dto.Adapt<MPPostUserReadModel>();
        }

        public PostUserReadModel ToDTO()
        {
            return this.Adapt<PostUserReadModel>();
        }

        [Key(0)]
        public required string PostUserId { get; init; }

        [Key(1)]
        public required string PostUserName { get; init; }
    }
}
