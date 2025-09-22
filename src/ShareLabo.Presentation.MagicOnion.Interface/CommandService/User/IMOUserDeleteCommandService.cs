using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOUserDeleteCommandService
        : IMOCommandService<IMOUserDeleteCommandService, IMOUserDeleteCommandService.Req, IUserDeleteCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IUserDeleteCommandService.Req, Req>
        {
            public static Req FromDTO(IUserDeleteCommandService.Req dto)
            {
                return dto.Adapt<Req>();
            }

            public IUserDeleteCommandService.Req ToDTO()
            {
                return this.Adapt<IUserDeleteCommandService.Req>();
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetId { get; init; }
        }
    }
}
