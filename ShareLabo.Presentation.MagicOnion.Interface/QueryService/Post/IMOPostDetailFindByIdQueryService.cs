using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    PostId = dto.PostId,
                };
            }

            public IPostDetailFindByIdQueryService.Req ToDTO()
            {
                return new IPostDetailFindByIdQueryService.Req()
                {
                    PostId = PostId,
                };
            }

            [Key(0)]
            public required string PostId { get; init; }
        }

        [MessagePackObject]
        sealed record Res : IMPDTO<IPostDetailFindByIdQueryService.Res, Res>
        {
            public static Res FromDTO(IPostDetailFindByIdQueryService.Res dto)
            {
                return new Res()
                {
                    PostDetailOptional =
                        MPOptional<MPPostDetailReadModel>.FromOptional(
                            dto.PostDetailOptional,
                            x => MPPostDetailReadModel.FromDTO(x)),
                };
            }
            public IPostDetailFindByIdQueryService.Res ToDTO()
            {
                return new IPostDetailFindByIdQueryService.Res()
                {
                    PostDetailOptional = PostDetailOptional.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPPostDetailReadModel> PostDetailOptional { get; init; }
        }
    }
}
