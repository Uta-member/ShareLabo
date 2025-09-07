using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;

namespace ShareLabo.Domain.DomeinService.Follow
{
    public sealed class FollowCreateDomainService<TFollowSession>
        : IDomainService<FollowCreateDomainService<TFollowSession>.Req>
        where TFollowSession : IDisposable
    {
        private readonly FollowAggregateService<TFollowSession> _followAggregateService;

        public FollowCreateDomainService(FollowAggregateService<TFollowSession> followAggregateService)
        {
            _followAggregateService = followAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _followAggregateService.CreateAsync(
                new FollowAggregateService<TFollowSession>.CreateReq()
                {
                    CreateCommand =
                        new FollowEntity.CreateCommand()
                            {
                                FollowId = req.FollowId,
                                FollowStartDateTime = req.FollowStartDateTime,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.Session,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TFollowSession Session { get; init; }
        }
    }
}
