using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommanService.TimeLine
{
    public interface ITimeLineDeleteCommandService : ICommandService<ITimeLineDeleteCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }
        }
    }
}
