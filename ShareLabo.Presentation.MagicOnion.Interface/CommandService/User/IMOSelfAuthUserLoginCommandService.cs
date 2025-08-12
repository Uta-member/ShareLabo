using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOSelfAuthUserLoginCommandService
        : IMOCommandServiceWithRes<IMOSelfAuthUserLoginCommandService, IMOSelfAuthUserLoginCommandService.Req, ISelfAuthUserLoginCommandService.Req, IMOSelfAuthUserLoginCommandService.Res, ISelfAuthUserLoginCommandService.Res>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ISelfAuthUserLoginCommandService.Req, Req>
        {
            public static Req FromDTO(ISelfAuthUserLoginCommandService.Req dto)
            {
                return new Req()
                {
                    AccountPassword = dto.AccountPassword.Value,
                    UserAccountId = dto.UserAccountId.Value,
                };
            }

            public ISelfAuthUserLoginCommandService.Req ToDTO()
            {
                return new ISelfAuthUserLoginCommandService.Req()
                {
                    AccountPassword = Application.Authentication.AccountPassword.Reconstruct(AccountPassword),
                    UserAccountId = Domain.ValueObject.UserAccountId.Reconstruct(UserAccountId),
                };
            }

            [Key(0)]
            public required string AccountPassword { get; init; }

            [Key(1)]
            public required string UserAccountId { get; init; }
        }


        [MessagePackObject]
        public sealed record Res : IMPDTO<ISelfAuthUserLoginCommandService.Res, Res>
        {
            public static Res FromDTO(ISelfAuthUserLoginCommandService.Res dto)
            {
                return new Res()
                {
                    IsAuthorized = dto.IsAuthorized,
                    LoginResultDetail = dto.LoginResultDetail,
                };
            }

            public ISelfAuthUserLoginCommandService.Res ToDTO()
            {
                return new ISelfAuthUserLoginCommandService.Res()
                {
                    IsAuthorized = IsAuthorized,
                    LoginResultDetail = LoginResultDetail,
                };
            }

            [Key(0)]
            public required bool IsAuthorized { get; init; }

            [Key(1)]
            public required LoginResultDetail LoginResultDetail { get; init; }
        }
    }
}
