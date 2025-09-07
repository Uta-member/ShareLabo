using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.CommanService.TimeLine;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    public interface IMOTimeLineDeleteCommandService
        : IMOCommandService<IMOTimeLineDeleteCommandService, IMOTimeLineDeleteCommandService.Req, ITimeLineDeleteCommandService.Req>
    {
        [MessagePackObject]
        public sealed record Req : IMPDTO<ITimeLineDeleteCommandService.Req, Req>
        {
            public static Req FromDTO(ITimeLineDeleteCommandService.Req dto)
            {
                return new Req()
                {
                    OperateInfo = MPOperateInfo.FromDTO(dto.OperateInfo),
                    TargetId = dto.TargetId.Value,
                };
            }

            public ITimeLineDeleteCommandService.Req ToDTO()
            {
                return new ITimeLineDeleteCommandService.Req()
                {
                    OperateInfo = OperateInfo.ToDTO(),
                    TargetId = TimeLineId.Reconstruct(TargetId),
                };
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetId { get; init; }
        }
    }
}
