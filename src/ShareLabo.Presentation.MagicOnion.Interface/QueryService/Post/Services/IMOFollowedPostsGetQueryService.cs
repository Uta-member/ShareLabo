using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOFollowedPostsGetQueryService
        : IMOQueryService<IMOFollowedPostsGetQueryService,
        IMOFollowedPostsGetQueryService.Req, IFollowedPostsGetQueryService.Req,
        IMOFollowedPostsGetQueryService.Res, IFollowedPostsGetQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IFollowedPostsGetQueryService.Req, Req>
        {
            public static Req FromDTO(IFollowedPostsGetQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IFollowedPostsGetQueryService.Req ToDTO()
            {
                return this.Adapt<IFollowedPostsGetQueryService.Req>();
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
        public sealed record Res : IMPDTO<IFollowedPostsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IFollowedPostsGetQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }
            public IFollowedPostsGetQueryService.Res ToDTO()
            {
                return this.Adapt<IFollowedPostsGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
