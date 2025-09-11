using CSStack.TADA.MagicOnionHelper.Abstractions;
using MessagePack;
using ShareLabo.Application.UseCase.QueryService.TimeLine;

namespace ShareLabo.Presentation.MagicOnion.Interface
{
    [MessagePackObject]
    public sealed record MPTimeLineFilterReadModel : IMPDTO<TimeLineFilterReadModel, MPTimeLineFilterReadModel>
    {
        public static MPTimeLineFilterReadModel FromDTO(TimeLineFilterReadModel dto)
        {
            return new MPTimeLineFilterReadModel()
            {
                UserId = dto.UserId,
                UserName = dto.UserName,
            };
        }

        public TimeLineFilterReadModel ToDTO()
        {
            return new TimeLineFilterReadModel()
            {
                UserId = UserId,
                UserName = UserName,
            };
        }

        [Key(0)]
        public required string UserId { get; init; }

        [Key(1)]
        public required string UserName { get; init; }
    }
}
