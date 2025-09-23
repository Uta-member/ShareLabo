using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;

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
                    CurrentPassword = dto.CurrentPassword,
                    NewPassword = dto.NewPassword,
                    OperateInfo = dto.OperateInfo.ToMPDTO(),
                    TargetUserId = dto.TargetUserId,
                };
            }

            public ISelfAuthUserPasswordUpdateCommandService.Req ToDTO()
            {
                return new ISelfAuthUserPasswordUpdateCommandService.Req()
                {
                    CurrentPassword = CurrentPassword,
                    NewPassword = NewPassword,
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetUserId = TargetUserId,
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
