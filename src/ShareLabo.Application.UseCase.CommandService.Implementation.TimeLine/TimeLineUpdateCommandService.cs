using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Domain.DomainService.TimeLine;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.TimeLine
{
    public sealed class TimeLineUpdateCommandService<TTimeLineSession, TUserSession> : ITimeLineUpdateCommandService
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly TimeLineUpdateDomainService<TTimeLineSession, TUserSession> _timeLineUpdateDomainService;
        private readonly ITransactionManager _transactionManager;

        public TimeLineUpdateCommandService(
            ITransactionManager transactionManager,
            TimeLineUpdateDomainService<TTimeLineSession, TUserSession> timeLineUpdateDomainService)
        {
            _transactionManager = transactionManager;
            _timeLineUpdateDomainService = timeLineUpdateDomainService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TTimeLineSession), typeof(TUserSession)],
                async sessions => await _timeLineUpdateDomainService.ExecuteAsync(
                    new ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>.Req()
                {
                    FilterMembersOptional = req.FilterMembersOptional,
                    NameOptional = req.NameOptional,
                    OperateInfo = req.OperateInfo,
                    TargetId = req.TargetId,
                    TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                    UserSession = sessions.GetSession<TUserSession>(),
                }));
        }
    }
}
