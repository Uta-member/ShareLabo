using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOMyPostsGetQueryService
        : IMOQueryService<IMOMyPostsGetQueryService,
        IMOMyPostsGetQueryService.Req, IMyPostsGetQueryService.Req,
        IMOMyPostsGetQueryService.Res, IMyPostsGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IMyPostsGetQueryService.Req, Req>
        {
            public static Req FromDTO(IMyPostsGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IMyPostsGetQueryService.Req ToDTO()
            {
                return this.Adapt<IMyPostsGetQueryService.Req>();
            }

            [Key(0)]
            public required int Length { get; init; }

            [Key(1)]
            public long? StartPostSequenceId { get; init; }

            [Key(2)]
            public required bool ToBefore { get; init; }

            [Key(3)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IMyPostsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IMyPostsGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IMyPostsGetQueryService.Res ToDTO()
            {
                return this.Adapt<IMyPostsGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
