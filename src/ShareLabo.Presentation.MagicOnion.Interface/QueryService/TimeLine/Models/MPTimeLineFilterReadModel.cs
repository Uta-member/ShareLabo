using CSStack.TADA.MagicOnionHelper.Abstractions;
using Mapster;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineFilterReadModel : IMPDTO<TimeLineFilterReadModel, MPTimeLineFilterReadModel>
    {
        public static MPTimeLineFilterReadModel FromDTO(TimeLineFilterReadModel dto)
        {
            return dto.Adapt<MPTimeLineFilterReadModel>();
        }

        public TimeLineFilterReadModel ToDTO()
        {
            return this.Adapt<TimeLineFilterReadModel>();
        }

        [Key(0)]
        public required string UserId { get; init; }

        [Key(1)]
        public required string UserName { get; init; }
    }
}
