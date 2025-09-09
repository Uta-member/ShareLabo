using CSStack.TADA.MagicOnionHelper.Abstractions;
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
                return new Req()
                {
                    AccountId = dto.AccountId,
                };
            }

            public IUserDetailFindByAccountIdQueryService.Req ToDTO()
            {
                return new IUserDetailFindByAccountIdQueryService.Req()
                {
                    AccountId = AccountId,
                };
            }

            [Key(0)]
            public required string AccountId { get; init; }
        }

        [MessagePackObject]
        public sealed record Res : IMPDTO<IUserDetailFindByAccountIdQueryService.Res, Res>
        {
            public static Res FromDTO(IUserDetailFindByAccountIdQueryService.Res dto)
            {
                return new Res()
                {
                    User =
                        MPOptional<MPUserDetailReadModel>.FromOptional(dto.User, x => MPUserDetailReadModel.FromDTO(x)),
                };
            }

            public IUserDetailFindByAccountIdQueryService.Res ToDTO()
            {
                return new IUserDetailFindByAccountIdQueryService.Res()
                {
                    User = User.ToOptional(x => x.ToDTO()),
                };
            }

            [Key(0)]
            public required MPOptional<MPUserDetailReadModel> User { get; init; }
        }
    }
}
