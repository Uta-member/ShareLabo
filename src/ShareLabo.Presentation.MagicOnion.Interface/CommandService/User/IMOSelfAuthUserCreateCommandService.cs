using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOSelfAuthUserCreateCommandService
        : IMOCommandService<IMOSelfAuthUserCreateCommandService, IMOSelfAuthUserCreateCommandService.Req, ISelfAuthUserCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ISelfAuthUserCreateCommandService.Req, Req>
        {
            public static Req FromDTO(ISelfAuthUserCreateCommandService.Req dto)
            {
                return new Req()
                {
                    AccountPassword = dto.AccountPassword,
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    UserAccountId = dto.UserAccountId,
                    UserId = dto.UserId,
                    UserName = dto.UserName,
                };
            }

            public ISelfAuthUserCreateCommandService.Req ToDTO()
            {
                return new ISelfAuthUserCreateCommandService.Req()
                {
                    AccountPassword = AccountPassword,
                    OperateInfo = OperateInfo.ToDTO(),
                    UserAccountId = UserAccountId,
                    UserId = UserId,
                    UserName = UserName,
                };
            }

            [Key(0)]
            public required string AccountPassword { get; init; }

            [Key(1)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(2)]
            public required string UserAccountId { get; init; }

            [Key(3)]
            public required string UserId { get; init; }

            [Key(4)]
            public required string UserName { get; init; }
        }
    }
}
