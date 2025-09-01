using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession>
        : IDomainService<UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession>.Req>
        where TUserSession : IDisposable
        where TGroupSession : IDisposable
        where TTimeLineSession : IDisposable
    {
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public UserDeleteDomainService(
            UserAggregateService<TUserSession> userAggregateService,
            GroupAggregateService<TGroupSession> groupAggregateService,
            TimeLineAggregateService<TTimeLineSession> timeLineAggregateService)
        {
            _userAggregateService = userAggregateService;
            _groupAggregateService = groupAggregateService;
            _timeLineAggregateService = timeLineAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _userAggregateService.DeleteAsync(
                new UserAggregateService<TUserSession>.StatusUpdateReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.UserSession,
                    TargetId = req.TargetId,
                },
                cancellationToken);
            await _groupAggregateService.DisconnectMemberAsync(
                new GroupAggregateService<TGroupSession>.MemberDisconnectReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.GroupSession,
                    TargetUserId = req.TargetId,
                });
            await _timeLineAggregateService.DeleteByOwnerAsync(
                new TimeLineAggregateService<TTimeLineSession>.DeleteByOwnerReq()
                {
                    OwnerId = req.TargetId,
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required TGroupSession GroupSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
