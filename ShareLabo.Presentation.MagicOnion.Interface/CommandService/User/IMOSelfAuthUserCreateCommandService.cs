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
                    AccountPassword = dto.AccountPassword.Value,
                    UserAccountId = dto.UserAccountId.Value,
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    UserId = dto.UserId.Value,
                    UserName = dto.UserName.Value,
                };
            }

            public ISelfAuthUserCreateCommandService.Req ToDTO()
            {
                return new ISelfAuthUserCreateCommandService.Req()
                {
                    AccountPassword = Application.Authentication.AccountPassword.Reconstruct(AccountPassword),
                    UserAccountId = Domain.ValueObject.UserAccountId.Reconstruct(UserAccountId),
                    OperateInfo = OperateInfo.ToDTO(),
                    UserId = Domain.ValueObject.UserId.Reconstruct(UserId),
                    UserName = Domain.ValueObject.UserName.Reconstruct(UserName),
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
