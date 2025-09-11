using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineDeleteDomainService<TTimeLineSession>
        : IDomainService<TimeLineDeleteDomainService<TTimeLineSession>.Req>
        where TTimeLineSession : IDisposable
    {
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;

        public TimeLineDeleteDomainService(TimeLineAggregateService<TTimeLineSession> timeLineAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _timeLineAggregateService.DeleteAsync(
                new TimeLineAggregateService<TTimeLineSession>.DeleteReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                    TargetId = req.TargetId,
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }
        }
    }
}
