using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPPostSummaryReadModel : IMPDTO<PostSummaryReadModel, MPPostSummaryReadModel>
    {
        public static MPPostSummaryReadModel FromDTO(PostSummaryReadModel dto)
        {
            return new MPPostSummaryReadModel()
            {
                PostDateTime = dto.PostDateTime,
                PostId = dto.PostId,
                PostUser = dto.PostUser,
                Title = dto.Title,
                PostSequenceId = dto.PostSequenceId,
            };
        }

        public PostSummaryReadModel ToDTO()
        {
            return new PostSummaryReadModel()
            {
                PostDateTime = PostDateTime,
                PostId = PostId,
                PostUser = PostUser,
                Title = Title,
                PostSequenceId = PostSequenceId,
            };
        }

        [Key(0)]
        public required DateTime PostDateTime { get; init; }

        [Key(1)]
        public required string PostId { get; init; }

        [Key(4)]
        public required long PostSequenceId { get; init; }

        [Key(2)]
        public required PostUserReadModel PostUser { get; init; }

        [Key(3)]
        public required string Title { get; init; }
    }
}
