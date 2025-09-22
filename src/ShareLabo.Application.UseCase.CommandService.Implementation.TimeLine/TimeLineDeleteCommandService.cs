using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Domain.DomainService.TimeLine;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.TimeLine
{
    public sealed class TimeLineDeleteCommandService<TTimeLineSession> : ITimeLineDeleteCommandService
        where TTimeLineSession : IDisposable
    {
        private readonly ITimeLineDeleteDomainService<TTimeLineSession> _timeLineDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public TimeLineDeleteCommandService(
            ITransactionManager transactionManager,
            ITimeLineDeleteDomainService<TTimeLineSession> timeLineDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _timeLineDeleteDomainService = timeLineDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TTimeLineSession) ],
                async sessions => await _timeLineDeleteDomainService.ExecuteAsync(
                    new ITimeLineDeleteDomainService<TTimeLineSession>.Req()
                    {
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                        TargetId = TimeLineId.Reconstruct(req.TargetId),
                        TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                    }));
        }
    }
}
