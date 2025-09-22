using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOTimeLinePostsGetQueryService
        : IMOQueryService<IMOTimeLinePostsGetQueryService,
        IMOTimeLinePostsGetQueryService.Req, ITimeLinePostsGetQueryService.Req,
        IMOTimeLinePostsGetQueryService.Res, ITimeLinePostsGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ITimeLinePostsGetQueryService.Req, Req>
        {
            public static Req FromDTO(ITimeLinePostsGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public ITimeLinePostsGetQueryService.Req ToDTO()
            {
                return this.Adapt<ITimeLinePostsGetQueryService.Req>();
            }

            [Key(0)]
            public required int Length { get; init; }

            [Key(1)]
            public long? StartPostSequenceId { get; init; }

            [Key(2)]
            public required string TimeLineId { get; init; }

            [Key(3)]
            public required bool ToBefore { get; init; }

            [Key(4)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<ITimeLinePostsGetQueryService.Res, Res>
        {
            public static Res FromDTO(ITimeLinePostsGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public ITimeLinePostsGetQueryService.Res ToDTO()
            {
                return this.Adapt<ITimeLinePostsGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
