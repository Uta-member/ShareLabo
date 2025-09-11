using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOSelfAuthUserPasswordUpdateCommandService
        : IMOCommandService<IMOSelfAuthUserPasswordUpdateCommandService, IMOSelfAuthUserPasswordUpdateCommandService.Req, ISelfAuthUserPasswordUpdateCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ISelfAuthUserPasswordUpdateCommandService.Req, Req>
        {
            public static Req FromDTO(ISelfAuthUserPasswordUpdateCommandService.Req dto)
            {
                return new Req()
                {
                    CurrentPassword = dto.CurrentPassword.Value,
                    NewPassword = dto.NewPassword.Value,
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetUserId = dto.TargetUserId.Value,
                };
            }

            public ISelfAuthUserPasswordUpdateCommandService.Req ToDTO()
            {
                return new ISelfAuthUserPasswordUpdateCommandService.Req()
                {
                    CurrentPassword = AccountPassword.Reconstruct(CurrentPassword),
                    NewPassword = AccountPassword.Reconstruct(NewPassword),
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetUserId = UserId.Reconstruct(TargetUserId),
                };
            }

            [Key(0)]
            public required string CurrentPassword { get; init; }

            [Key(1)]
            public required string NewPassword { get; init; }

            [Key(2)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(3)]
            public required string TargetUserId { get; init; }
        }
    }
}
