using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.CommandService.TimeLine;

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
                return dto.Adapt<Req>();
            }

            public ITimeLineDeleteCommandService.Req ToDTO()
            {
                return this.Adapt<ITimeLineDeleteCommandService.Req>();
            }

            [Key(0)]
            public required MPOperateInfo OperateInfo { get; init; }

            [Key(1)]
            public required string TargetId { get; init; }
        }
    }
}
