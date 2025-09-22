using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
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
                return dto.Adapt<Req>();
            }

            public ISelfAuthUserPasswordUpdateCommandService.Req ToDTO()
            {
                return this.Adapt<ISelfAuthUserPasswordUpdateCommandService.Req>();
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
