using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow
{
    public sealed class FollowCreateDomainService<TFollowSession, TUserSession>
        : IDomainService<FollowCreateDomainService<TFollowSession, TUserSession>.Req>
        where TFollowSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly FollowAggregateService<TFollowSession> _followAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public FollowCreateDomainService(
            FollowAggregateService<TFollowSession> followAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _followAggregateService = followAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            var targetFromUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                req.UserSession,
                req.FollowId.FollowFromId,
                cancellationToken);
            if(!targetFromUserOptional.HasValue)
            {
                throw new ObjectNotFoundException("フォロー元ユーザが存在しません");
            }

            var targetToUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                req.UserSession,
                req.FollowId.FollowToId,
                cancellationToken);
            if(!targetToUserOptional.HasValue)
            {
                throw new ObjectNotFoundException("フォロー先ユーザが存在しません");
            }

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
                    Session = req.FollowSession,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required TFollowSession FollowSession { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
