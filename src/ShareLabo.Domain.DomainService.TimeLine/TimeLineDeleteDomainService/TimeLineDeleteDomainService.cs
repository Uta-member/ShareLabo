using ShareLabo.Domain.Aggregate.TimeLine;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineDeleteDomainService<TTimeLineSession>
        : ITimeLineDeleteDomainService<TTimeLineSession>
        where TTimeLineSession : IDisposable
    {
        private readonly ITimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;

        public TimeLineDeleteDomainService(ITimeLineAggregateService<TTimeLineSession> timeLineAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineDeleteDomainService<TTimeLineSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _timeLineAggregateService.DeleteAsync(
                new ITimeLineAggregateService<TTimeLineSession>.DeleteReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                    TargetId = req.TargetId,
                });
        }
    }
}
