using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserDetailFindByUserIdQueryService
        : IMOQueryService<IMOUserDetailFindByUserIdQueryService, IMOUserDetailFindByUserIdQueryService.Req, IUserDetailFindByUserIdQueryService.Req, IMOUserDetailFindByUserIdQueryService.Res, IUserDetailFindByUserIdQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserDetailFindByUserIdQueryService.Req, Req>
        {
            public static Req FromDTO(IUserDetailFindByUserIdQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserDetailFindByUserIdQueryService.Req ToDTO()
            {
                return this.Adapt<IUserDetailFindByUserIdQueryService.Req>();
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserDetailFindByUserIdQueryService.Res, Res>
        {
            public static Res FromDTO(IUserDetailFindByUserIdQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserDetailFindByUserIdQueryService.Res ToDTO()
            {
                return this.Adapt<IUserDetailFindByUserIdQueryService.Res>();
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> User { get; init; }
        }
    }
}
