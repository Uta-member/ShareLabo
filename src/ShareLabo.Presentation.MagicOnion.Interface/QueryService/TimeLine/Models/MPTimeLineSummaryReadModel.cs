using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineSummaryReadModel : IMPDTO<TimeLineSummaryReadModel, MPTimeLineSummaryReadModel>
    {
        public static MPTimeLineSummaryReadModel FromDTO(TimeLineSummaryReadModel dto)
        {
            return dto.Adapt<MPTimeLineSummaryReadModel>();
        }

        public TimeLineSummaryReadModel ToDTO()
        {
            return this.Adapt<TimeLineSummaryReadModel>();
        }

        [Key(0)]
        public required string OwnerId { get; init; }

        [Key(1)]
        public required string TimeLineId { get; init; }

        [Key(3)]
        public required string TimeLineName { get; init; }
    }
}
