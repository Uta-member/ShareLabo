using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Domain.DomainService.TimeLine;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.TimeLine
{
    public sealed class TimeLineCreateCommandService<TTimeLineSession, TUserSession> : ITimeLineCreateCommandService
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly ITimeLineCreateDomainService<TTimeLineSession, TUserSession> _timeLineCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public TimeLineCreateCommandService(
            ITransactionManager transactionManager,
            ITimeLineCreateDomainService<TTimeLineSession, TUserSession> timeLineCreateDomainService)
        {
            _transactionManager = transactionManager;
            _timeLineCreateDomainService = timeLineCreateDomainService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TTimeLineSession), typeof(TUserSession)],
                async sessions => await _timeLineCreateDomainService.ExecuteAsync(
                    new ITimeLineCreateDomainService<TTimeLineSession, TUserSession>.Req()
                {
                    FilterMembers = req.FilterMembers,
                    Id = req.Id,
                    Name = req.Name,
                    OperateInfo = req.OperateInfo,
                    OwnerId = req.OwnerId,
                    TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                    UserSession = sessions.GetSession<TUserSession>(),
                }));
        }
    }
}
