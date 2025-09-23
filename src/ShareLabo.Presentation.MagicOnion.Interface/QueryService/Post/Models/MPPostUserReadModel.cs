using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPPostUserReadModel : IMPDTO<PostUserReadModel, MPPostUserReadModel>
    {
        public static MPPostUserReadModel FromDTO(PostUserReadModel dto)
        {
            return new MPPostUserReadModel
            {
                PostUserId = dto.PostUserId,
                PostUserName = dto.PostUserName,
            };
        }

        public PostUserReadModel ToDTO()
        {
            return new PostUserReadModel
            {
                PostUserId = PostUserId,
                PostUserName = PostUserName,
            };
        }

        [Key(0)]
        public required string PostUserId { get; init; }

        [Key(1)]
        public required string PostUserName { get; init; }
    }
}
