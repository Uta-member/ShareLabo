using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.Group;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOGroupDeleteCommandService
        : IMOCommandService<IMOGroupDeleteCommandService, IMOGroupDeleteCommandService.Req, IGroupDeleteCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<IGroupDeleteCommandService.Req, Req>
        {
            public static Req FromDTO(IGroupDeleteCommandService.Req dto)
            {
                return new Req()
                {
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetId = dto.TargetId.Value,
                };
            }

            public IGroupDeleteCommandService.Req ToDTO()
            {
                return new IGroupDeleteCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = GroupId.Reconstruct(TargetId),
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetId { get; init; }
        }
    }
}
