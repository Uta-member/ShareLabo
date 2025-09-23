using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;
using System.Collections.Immutable;

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
                return new Req()
                {
                    Length = dto.Length,
                    StartPostSequenceId = dto.StartPostSequenceId,
                    TimeLineId = dto.TimeLineId,
                    ToBefore = dto.ToBefore,
                    UserId = dto.UserId,
                };
            }

            public ITimeLinePostsGetQueryService.Req ToDTO()
            {
                return new ITimeLinePostsGetQueryService.Req()
                {
                    Length = Length,
                    StartPostSequenceId = StartPostSequenceId,
                    TimeLineId = TimeLineId,
                    ToBefore = ToBefore,
                    UserId = UserId,
                };
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
                return new Res
                {
                    PostSummaries = dto.PostSummaries.Select(x => MPPostSummaryReadModel.FromDTO(x)).ToList(),
                };
            }

            public ITimeLinePostsGetQueryService.Res ToDTO()
            {
                return new ITimeLinePostsGetQueryService.Res
                {
                    PostSummaries = PostSummaries.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
