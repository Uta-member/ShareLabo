using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOFindUserDetailByUserIdQueryService
        : IMOQueryService<IMOFindUserDetailByUserIdQueryService, IMOFindUserDetailByUserIdQueryService.Req, IFindUserDetailByUserIdQueryService.Req, IMOFindUserDetailByUserIdQueryService.Res, IFindUserDetailByUserIdQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IFindUserDetailByUserIdQueryService.Req, Req>
        {
            public static Req FromDTO(IFindUserDetailByUserIdQueryService.Req dto)
            {
                return new Req()
                {
                    UserId = dto.UserId,
                };
            }

            public IFindUserDetailByUserIdQueryService.Req ToDTO()
            {
                return new IFindUserDetailByUserIdQueryService.Req()
                {
                    UserId = UserId,
                };
            }

            [Key(0)]
            public required string UserId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IFindUserDetailByUserIdQueryService.Res, Res>
        {
            public static Res FromDTO(IFindUserDetailByUserIdQueryService.Res dto)
            {
                return new Res()
                {
                    User =
                        MPOptional<MPUserDetailReadModel>.FromOptional(dto.User, x => MPUserDetailReadModel.FromDTO(x)),
                };
            }

            public IFindUserDetailByUserIdQueryService.Res ToDTO()
            {
                return new IFindUserDetailByUserIdQueryService.Res()
                {
                    User = User.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> User { get; init; }
        }
    }
}
