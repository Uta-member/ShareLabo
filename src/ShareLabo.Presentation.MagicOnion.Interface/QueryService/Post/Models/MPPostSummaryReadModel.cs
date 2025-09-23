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
            return new MPPostSummaryReadModel
            {
                PostDateTime = dto.PostDateTime,
                PostId = dto.PostId,
                PostSequenceId = dto.PostSequenceId,
                PostUser = MPPostUserReadModel.FromDTO(dto.PostUser),
                Title = dto.Title,
            };
        }

        public PostSummaryReadModel ToDTO()
        {
            return new PostSummaryReadModel
            {
                PostDateTime = PostDateTime,
                PostId = PostId,
                PostSequenceId = PostSequenceId,
                PostUser = PostUser.ToDTO(),
                Title = Title,
            };
        }

        [Key(0)]
        public required DateTime PostDateTime { get; init; }

        [Key(1)]
        public required string PostId { get; init; }

        [Key(4)]
        public required long PostSequenceId { get; init; }

        [Key(2)]
        public required MPPostUserReadModel PostUser { get; init; }

        [Key(3)]
        public required string Title { get; init; }
    }
}
