using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineSummaryReadModel : IMPDTO<TimeLineSummaryReadModel, MPTimeLineSummaryReadModel>
    {
        public static MPTimeLineSummaryReadModel FromDTO(TimeLineSummaryReadModel dto)
        {
            return new MPTimeLineSummaryReadModel()
            {
                OwnerId = dto.OwnerId,
                TimeLineId = dto.TimeLineId,
                TimeLineName = dto.TimeLineName,
            };
        }

        public TimeLineSummaryReadModel ToDTO()
        {
            return new TimeLineSummaryReadModel()
            {
                OwnerId = OwnerId,
                TimeLineId = TimeLineId,
                TimeLineName = TimeLineName,
            };
        }

        [Key(0)]
        public required string OwnerId { get; init; }

        [Key(1)]
        public required string TimeLineId { get; init; }

        [Key(3)]
        public required string TimeLineName { get; init; }
    }
}
