using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPPostSummaryReadModel : IMPDTO<PostSummaryReadModel, MPPostSummaryReadModel>
    {
        public static MPPostSummaryReadModel FromDTO(PostSummaryReadModel dto)
        {
            return dto.Adapt<MPPostSummaryReadModel>();
        }

        public PostSummaryReadModel ToDTO()
        {
            return this.Adapt<PostSummaryReadModel>();
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
