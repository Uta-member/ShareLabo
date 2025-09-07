using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;
using System.Collections.Immutable;

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
                return new Req()
                {
                    Length = dto.Length,
                    StartPostSequenceId = dto.StartPostSequenceId,
                    ToBefore = dto.ToBefore,
                    UserId = dto.UserId,
                };
            }

            public IMyPostsGetQueryService.Req ToDTO()
            {
                return new IMyPostsGetQueryService.Req()
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
        public sealed record Res : IMPDTO<IMyPostsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IMyPostsGetQueryService.Res dto)
            {
                return new Res()
                {
                    PostSummaries = dto.PostSummaries.Select(x => MPPostSummaryReadModel.FromDTO(x)).ToList(),
                };
            }

            public IMyPostsGetQueryService.Res ToDTO()
            {
                return new IMyPostsGetQueryService.Res()
                {
                    PostSummaries = PostSummaries.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
