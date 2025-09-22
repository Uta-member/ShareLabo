using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.Post;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOPostDetailFindByIdQueryService
        : IMOQueryService<IMOPostDetailFindByIdQueryService, IMOPostDetailFindByIdQueryService.Req, IPostDetailFindByIdQueryService.Req, IMOPostDetailFindByIdQueryService.Res, IPostDetailFindByIdQueryService.Res>
    {
        [MessagePackObject]
        sealed record Req : IMPDTO<IPostDetailFindByIdQueryService.Req, Req>
        {
            public static Req FromDTO(IPostDetailFindByIdQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IPostDetailFindByIdQueryService.Req ToDTO()
            {
                return this.Adapt<IPostDetailFindByIdQueryService.Req>();
            }

            [Key(0)]
            public required string PostId { get; init; }
        }

        [MessagePackObject]
        sealed record Res : IMPDTO<IPostDetailFindByIdQueryService.Res, Res>
        {
            public static Res FromDTO(IPostDetailFindByIdQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }
            public IPostDetailFindByIdQueryService.Res ToDTO()
            {
                return this.Adapt<IPostDetailFindByIdQueryService.Res>();
            }

            [Key(0)]
            public required MPOptional<MPPostDetailReadModel> PostDetailOptional { get; init; }
        }
    }
}
