using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPPostDetailReadModel : IMPDTO<PostDetailReadModel, MPPostDetailReadModel>
    {
        public static MPPostDetailReadModel FromDTO(PostDetailReadModel dto)
        {
            return new MPPostDetailReadModel()
            {
                Content = dto.Content,
                PostDateTime = dto.PostDateTime,
                PostId = dto.PostId,
                PostSequenceId = dto.PostSequenceId,
                PostUser = MPPostUserReadModel.FromDTO(dto.PostUser),
                Title = dto.Title,
                UpdateTimeStamp = dto.UpdateTimeStamp,
            };
        }

        public PostDetailReadModel ToDTO()
        {
            return new PostDetailReadModel()
            {
                Content = Content,
                PostDateTime = PostDateTime,
                PostId = PostId,
                PostSequenceId = PostSequenceId,
                PostUser = PostUser.ToDTO(),
                Title = Title,
                UpdateTimeStamp = UpdateTimeStamp,
            };
        }

        [Key(0)]
        public required string Content { get; init; }

        [Key(1)]
        public required DateTime PostDateTime { get; init; }

        [Key(2)]
        public required string PostId { get; init; }

        [Key(6)]
        public required long PostSequenceId { get; init; }

        [Key(3)]
        public required MPPostUserReadModel PostUser { get; init; }

        [Key(4)]
        public required string Title { get; init; }

        [Key(5)]
        public required DateTime UpdateTimeStamp { get; init; }
    }
}
