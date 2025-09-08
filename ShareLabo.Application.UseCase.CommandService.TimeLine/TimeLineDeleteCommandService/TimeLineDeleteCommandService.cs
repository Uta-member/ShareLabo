
using CSStack.TADA;
using ShareLabo.Domain.DomainService.TimeLine;

namespace ShareLabo.Application.UseCase.CommandService.TimeLine
{
    public sealed class TimeLineDeleteCommandService<TTimeLineSession> : ITimeLineDeleteCommandService
        where TTimeLineSession : IDisposable
    {
        private readonly TimeLineDeleteDomainService<TTimeLineSession> _timeLineDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public TimeLineDeleteCommandService(
            ITransactionManager transactionManager,
            TimeLineDeleteDomainService<TTimeLineSession> timeLineDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _timeLineDeleteDomainService = timeLineDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TTimeLineSession)],
                async sessions => await _timeLineDeleteDomainService.ExecuteAsync(
                    new TimeLineDeleteDomainService<TTimeLineSession>.Req()
                {
                    OperateInfo = req.OperateInfo,
                    TargetId = req.TargetId,
                    TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                }));
        }
    }
}
