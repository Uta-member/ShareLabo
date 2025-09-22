using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

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
                return dto.Adapt<Req>();
            }

            public IGeneralPostsGetQueryService.Req ToDTO()
            {
                return this.Adapt<IGeneralPostsGetQueryService.Req>();
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
                return dto.Adapt<Res>();
            }
            public IGeneralPostsGetQueryService.Res ToDTO()
            {
                return this.Adapt<IGeneralPostsGetQueryService.Res>();
            }

            [Key(0)]
            public required List<MPPostSummaryReadModel> PostSummaries { get; init; }
        }
    }
}
