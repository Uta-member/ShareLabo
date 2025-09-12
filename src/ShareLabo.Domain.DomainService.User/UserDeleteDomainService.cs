using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>
        : IUserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>
        where TUserSession : IDisposable
        where TTimeLineSession : IDisposable
        where TFollowSession : IDisposable
    {
        private readonly IFollowAggregateService<TFollowSession> _followAggregateService;
        private readonly ITimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public UserDeleteDomainService(
            IUserAggregateService<TUserSession> userAggregateService,
            ITimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            IFollowAggregateService<TFollowSession> followAggregateService)
        {
            _userAggregateService = userAggregateService;
            _timeLineAggregateService = timeLineAggregateService;
            _followAggregateService = followAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IUserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _userAggregateService.DeleteAsync(
                new IUserAggregateService<TUserSession>.StatusUpdateReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.UserSession,
                    TargetId = req.TargetId,
                },
                cancellationToken);
            await _timeLineAggregateService.DeleteByOwnerAsync(
                new ITimeLineAggregateService<TTimeLineSession>.DeleteByOwnerReq()
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
                    new IFollowAggregateService<TFollowSession>.DeleteReq()
                    {
                        FollowId = targetFollow.Identifier,
                        OperateInfo = req.OperateInfo,
                        Session = req.FollowSession,
                    },
                    cancellationToken);
            }
        }
    }
}
