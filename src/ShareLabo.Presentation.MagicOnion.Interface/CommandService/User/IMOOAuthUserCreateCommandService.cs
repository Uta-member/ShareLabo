using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.CommandService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOOAuthUserCreateCommandService
        : IMOCommandService<IMOOAuthUserCreateCommandService, IMOOAuthUserCreateCommandService.Req, IOAuthUserCreateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IOAuthUserCreateCommandService.Req, Req>
        {
            public static Req FromDTO(IOAuthUserCreateCommandService.Req dto)
            {
                return new Req()
                {
                    OAuthIdentifier = dto.OAuthIdentifier,
                    OAuthType = dto.OAuthType,
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    UserAccountId = dto.UserAccountId,
                    UserId = dto.UserId,
                    UserName = dto.UserName,
                };
            }

            public IOAuthUserCreateCommandService.Req ToDTO()
            {
                return new IOAuthUserCreateCommandService.Req()
                {
                    OAuthIdentifier = OAuthIdentifier,
                    OAuthType = OAuthType,
                    OperateInfo = OperateInfo.ToDTO(),
                    UserAccountId = UserAccountId,
                    UserId = UserId,
                    UserName = UserName,
                };
            }

            [Key(0)]
            public required string OAuthIdentifier { get; init; }

            [Key(1)]
            public required OAuthType OAuthType { get; init; }

            [Key(2)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(3)]
            public required string UserAccountId { get; init; }

            [Key(4)]
            public required string UserId { get; init; }

            [Key(5)]
            public required string UserName { get; init; }
        }
    }
}
