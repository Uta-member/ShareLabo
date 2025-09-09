using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using System.Collections.Immutable;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineDetailReadModel : IMPDTO<TimeLineDetailReadModel, MPTimeLineDetailReadModel>
    {
        public static MPTimeLineDetailReadModel FromDTO(TimeLineDetailReadModel dto)
        {
            return new MPTimeLineDetailReadModel()
            {
                OwnerId = dto.OwnerId,
                OwnerName = dto.OwnerName,
                TimeLineFilters = dto.TimeLineFilters.Select(x => MPTimeLineFilterReadModel.FromDTO(x)).ToList(),
                TimeLineId = dto.TimeLineId,
                TimeLineName = dto.TimeLineName,
            };
        }

        public TimeLineDetailReadModel ToDTO()
        {
            return new TimeLineDetailReadModel()
            {
                OwnerId = OwnerId,
                OwnerName = OwnerName,
                TimeLineFilters = TimeLineFilters.Select(x => x.ToDTO()).ToImmutableList(),
                TimeLineId = TimeLineId,
                TimeLineName = TimeLineName,
            };
        }

        [Key(0)]
        public required string OwnerId { get; init; }

        [Key(1)]
        public required string OwnerName { get; init; }

        [Key(2)]
        public required List<MPTimeLineFilterReadModel> TimeLineFilters { get; init; }

        [Key(3)]
        public required string TimeLineId { get; init; }

        [Key(4)]
        public required string TimeLineName { get; init; }
    }
}
