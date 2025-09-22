using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineDetailReadModel : IMPDTO<TimeLineDetailReadModel, MPTimeLineDetailReadModel>
    {
        public static MPTimeLineDetailReadModel FromDTO(TimeLineDetailReadModel dto)
        {
            return dto.Adapt<MPTimeLineDetailReadModel>();
        }

        public TimeLineDetailReadModel ToDTO()
        {
            return this.Adapt<TimeLineDetailReadModel>();
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
