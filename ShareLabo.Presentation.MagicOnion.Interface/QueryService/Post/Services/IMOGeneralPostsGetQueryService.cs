using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;
using System.Collections.Immutable;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOGeneralPostsGetQueryService
        : IMOQueryService<IMOGeneralPostsGetQueryService, IMOGeneralPostsGetQueryService.Req, IGeneralPostsGetQueryService.Req, IMOGeneralPostsGetQueryService.Res, IGeneralPostsGetQueryService.Res>
    {
        [MessagePackObject]
        sealed record Req : IMPDTO<IGeneralPostsGetQueryService.Req, Req>
        {
            public static Req FromDTO(IGeneralPostsGetQueryService.Req dto)
            {
                return new Req()
                {
                    Length = dto.Length,
                    StartPostSequenceId = dto.StartPostSequenceId,
                    ToBefore = dto.ToBefore,
                    UserId = dto.UserId,
                };
            }

            public IGeneralPostsGetQueryService.Req ToDTO()
            {
                return new IGeneralPostsGetQueryService.Req()
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
            public bool ToBefore { get; init; }

            [Key(3)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        sealed record Res : IMPDTO<IGeneralPostsGetQueryService.Res, Res>
        {
            public static Res FromDTO(IGeneralPostsGetQueryService.Res dto)
            {
                return new Res()
                {
                    PostSummaries = dto.PostSummaries.Select(x => MPPostSummaryReadModel.FromDTO(x)).ToList(),
                };
            }
            public IGeneralPostsGetQueryService.Res ToDTO()
            {
                return new IGeneralPostsGetQueryService.Res()
                {
                    PostSummaries = PostSummaries.Select(x => x.ToDTO()).ToImmutableList(),
                };
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
