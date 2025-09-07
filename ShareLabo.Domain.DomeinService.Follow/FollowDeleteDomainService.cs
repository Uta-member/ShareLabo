using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;

namespace ShareLabo.Domain.DomeinService.Follow
{
    public sealed class FollowDeleteDomainService<TFollowSession>
        : IDomainService<FollowDeleteDomainService<TFollowSession>.Req>
        where TFollowSession : IDisposable
    {
        private readonly FollowAggregateService<TFollowSession> _followAggregateService;

        public FollowDeleteDomainService(FollowAggregateService<TFollowSession> followAggregateService)
        {
            _followAggregateService = followAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _followAggregateService.DeleteAsync(
                new FollowAggregateService<TFollowSession>.DeleteReq()
                {
                    FollowId = req.FollowId,
                    OperateInfo = req.OperateInfo,
                    Session = req.FollowSession,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required TFollowSession FollowSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }
        }
    }
}
