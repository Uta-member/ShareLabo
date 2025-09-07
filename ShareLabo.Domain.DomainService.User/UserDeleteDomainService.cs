using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession, TFollowSession>
        : IDomainService<UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession, TFollowSession>.Req>
        where TUserSession : IDisposable
        where TGroupSession : IDisposable
        where TTimeLineSession : IDisposable
        where TFollowSession : IDisposable
    {
        private readonly FollowAggregateService<TFollowSession> _followAggregateService;
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public UserDeleteDomainService(
            UserAggregateService<TUserSession> userAggregateService,
            GroupAggregateService<TGroupSession> groupAggregateService,
            TimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            FollowAggregateService<TFollowSession> followAggregateService)
        {
            _userAggregateService = userAggregateService;
            _groupAggregateService = groupAggregateService;
            _timeLineAggregateService = timeLineAggregateService;
            _followAggregateService = followAggregateService;
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

            var targetFollows = await _followAggregateService.FindByFollowingUserIdAsync(
                req.FollowSession,
                req.TargetId,
                cancellationToken);

            foreach(var targetFollow in targetFollows)
            {
                await _followAggregateService.DeleteAsync(
                    new FollowAggregateService<TFollowSession>.DeleteReq()
                    {
                        FollowId = targetFollow.Identifier,
                        OperateInfo = req.OperateInfo,
                        Session = req.FollowSession,
                    },
                    cancellationToken);
            }
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required TFollowSession FollowSession { get; init; }

            public required TGroupSession GroupSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
