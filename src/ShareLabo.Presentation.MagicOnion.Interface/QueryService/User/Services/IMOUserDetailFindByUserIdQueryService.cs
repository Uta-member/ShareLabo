using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    UserId = dto.UserId,
                };
            }

            public IUserDetailFindByUserIdQueryService.Req ToDTO()
            {
                return new IUserDetailFindByUserIdQueryService.Req()
                {
                    UserId = UserId,
                };
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserDetailFindByUserIdQueryService.Res, Res>
        {
            public static Res FromDTO(IUserDetailFindByUserIdQueryService.Res dto)
            {
                return new Res()
                {
                    UserOptional = dto.UserOptional.ToMPOptional(x => MPUserDetailReadModel.FromDTO(x)),
                };
            }

            public IUserDetailFindByUserIdQueryService.Res ToDTO()
            {
                return new IUserDetailFindByUserIdQueryService.Res()
                {
                    UserOptional = UserOptional.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> UserOptional { get; init; }
        }
    }
}
