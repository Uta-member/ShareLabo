using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;
using System.Collections.Immutable;

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
                return new Req()
                {
                    Length = dto.Length,
                    StartPostSequenceId = dto.StartPostSequenceId,
                    ToBefore = dto.ToBefore,
                    UserId = dto.UserId,
                };
            }

            public IFollowedPostsGetQueryService.Req ToDTO()
            {
                return new IFollowedPostsGetQueryService.Req()
                {
                    Length = Length,
                    StartPostSequenceId = StartPostSequenceId,
                    ToBefore = ToBefore,
                    UserId = UserId,
                };
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
                return new Res()
                {
                    PostSummaries = dto.PostSummaries.Select(x => MPPostSummaryReadModel.FromDTO(x)).ToList(),
                };
            }
            public IFollowedPostsGetQueryService.Res ToDTO()
            {
                return new IFollowedPostsGetQueryService.Res()
                {
                    PostSummaries = PostSummaries.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
