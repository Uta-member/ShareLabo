using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Domain.DomainService.TimeLine;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.TimeLine
{
    public sealed class TimeLineUpdateCommandService<TTimeLineSession, TUserSession> : ITimeLineUpdateCommandService
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly ITimeLineUpdateDomainService<TTimeLineSession, TUserSession> _timeLineUpdateDomainService;
        private readonly ITransactionManager _transactionManager;

        public TimeLineUpdateCommandService(
            ITransactionManager transactionManager,
            ITimeLineUpdateDomainService<TTimeLineSession, TUserSession> timeLineUpdateDomainService)
        {
            _transactionManager = transactionManager;
            _timeLineUpdateDomainService = timeLineUpdateDomainService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TTimeLineSession), typeof(TUserSession) ],
                async sessions => await _timeLineUpdateDomainService.ExecuteAsync(
                    new ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>.Req()
                    {
                        FilterMembersOptional =
                            req.FilterMembersOptional.TryGetValue(out var filterMembers)
                                        ? filterMembers.Select(x => UserId.Reconstruct(x)).ToImmutableList()
                                        : Optional<ImmutableList<UserId>>.Empty,
                        NameOptional =
                            req.NameOptional.TryGetValue(out var name)
                                        ? TimeLineName.Create(name)
                                        : Optional<TimeLineName>.Empty,
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                        TargetId = TimeLineId.Reconstruct(req.TargetId),
                        TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                        UserSession = sessions.GetSession<TUserSession>(),
                    }));
        }
    }
}
