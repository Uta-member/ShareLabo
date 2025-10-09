using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.Follow
{
    public sealed class FollowCreateDomainService<TFollowSession, TUserSession>
        : IFollowCreateDomainService<TFollowSession, TUserSession>
        where TFollowSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly IFollowAggregateService<TFollowSession> _followAggregateService;
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public FollowCreateDomainService(
            IFollowAggregateService<TFollowSession> followAggregateService,
            IUserAggregateService<TUserSession> userAggregateService)
        {
            _followAggregateService = followAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IFollowCreateDomainService<TFollowSession, TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            var targetFromUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                req.UserSession,
                req.FollowId.FollowFromId,
                cancellationToken);
            if(!targetFromUserOptional.TryGetValue(out var targetFromUser) ||
                targetFromUser.Status != UserEntity.StatusEnum.Enabled)
            {
                throw new ObjectNotFoundException("フォロー元ユーザが存在しません");
            }

            var targetToUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                req.UserSession,
                req.FollowId.FollowToId,
                cancellationToken);
            if(!targetToUserOptional.TryGetValue(out var targetToUser) ||
                targetToUser.Status != UserEntity.StatusEnum.Enabled)
            {
                throw new ObjectNotFoundException("フォロー先ユーザが存在しません");
            }

            await _followAggregateService.CreateAsync(
                new IFollowAggregateService<TFollowSession>.CreateReq()
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
    }
}
