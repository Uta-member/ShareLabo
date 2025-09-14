using ShareLabo.Domain.Aggregate.Follow;

namespace ShareLabo.Domain.DomainService.Follow
{
    public sealed class FollowDeleteDomainService<TFollowSession>
        : IFollowDeleteDomainService<TFollowSession>
        where TFollowSession : IDisposable
    {
        private readonly IFollowAggregateService<TFollowSession> _followAggregateService;

        public FollowDeleteDomainService(IFollowAggregateService<TFollowSession> followAggregateService)
        {
            _followAggregateService = followAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IFollowDeleteDomainService<TFollowSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _followAggregateService.DeleteAsync(
                new IFollowAggregateService<TFollowSession>.DeleteReq()
                {
                    FollowId = req.FollowId,
                    OperateInfo = req.OperateInfo,
                    Session = req.FollowSession,
                },
                cancellationToken);
        }
    }
}
