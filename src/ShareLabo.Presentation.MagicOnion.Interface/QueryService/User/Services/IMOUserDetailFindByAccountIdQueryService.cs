using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserDetailFindByAccountIdQueryService
        : IMOQueryService<IMOUserDetailFindByAccountIdQueryService, IMOUserDetailFindByAccountIdQueryService.Req, IUserDetailFindByAccountIdQueryService.Req, IMOUserDetailFindByAccountIdQueryService.Res, IUserDetailFindByAccountIdQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserDetailFindByAccountIdQueryService.Req, Req>
        {
            public static Req FromDTO(IUserDetailFindByAccountIdQueryService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserDetailFindByAccountIdQueryService.Req ToDTO()
            {
                return this.Adapt<IUserDetailFindByAccountIdQueryService.Req>();
            }

            [Key(0)]
            public required string AccountId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserDetailFindByAccountIdQueryService.Res, Res>
        {
            public static Res FromDTO(IUserDetailFindByAccountIdQueryService.Res dto)
            {
                return dto.Adapt<Res>();
            }

            public IUserDetailFindByAccountIdQueryService.Res ToDTO()
            {
                return this.Adapt<IUserDetailFindByAccountIdQueryService.Res>();
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> User { get; init; }
        }
    }
}
