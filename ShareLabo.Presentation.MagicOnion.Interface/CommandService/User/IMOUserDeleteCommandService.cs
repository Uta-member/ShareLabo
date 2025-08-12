using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.ValueObject;

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
                return new Req()
                {
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetId = dto.TargetId.Value,
                };
            }

            public IUserDeleteCommandService.Req ToDTO()
            {
                return new IUserDeleteCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = UserId.Reconstruct(TargetId),
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetId { get; init; }
        }
    }
}
