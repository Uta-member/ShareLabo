using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOFindUserDetailByAccountIdQueryService
        : IMOQueryService<IMOFindUserDetailByAccountIdQueryService, IMOFindUserDetailByAccountIdQueryService.Req, IFindUserDetailByAccountIdQueryService.Req, IMOFindUserDetailByAccountIdQueryService.Res, IFindUserDetailByAccountIdQueryService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IFindUserDetailByAccountIdQueryService.Req, Req>
        {
            public static Req FromDTO(IFindUserDetailByAccountIdQueryService.Req dto)
            {
                return new Req()
                {
                    AccountId = dto.AccountId,
                };
            }

            public IFindUserDetailByAccountIdQueryService.Req ToDTO()
            {
                return new IFindUserDetailByAccountIdQueryService.Req()
                {
                    AccountId = AccountId,
                };
            }

            [Key(0)]
            public required string AccountId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IFindUserDetailByAccountIdQueryService.Res, Res>
        {
            public static Res FromDTO(IFindUserDetailByAccountIdQueryService.Res dto)
            {
                return new Res()
                {
                    User =
                        MPOptional<MPUserDetailReadModel>.FromOptional(dto.User, x => MPUserDetailReadModel.FromDTO(x)),
                };
            }

            public IFindUserDetailByAccountIdQueryService.Res ToDTO()
            {
                return new IFindUserDetailByAccountIdQueryService.Res()
                {
                    User = User.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> User { get; init; }
        }
    }
}
