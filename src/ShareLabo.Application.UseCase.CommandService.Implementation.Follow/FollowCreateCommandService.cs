using CSStack.TADA;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Domain.DomainService.Follow;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.Follow
{
    public sealed class FollowCreateCommandService<TFollowSession, TUserSession> : IFollowCreateCommandService
        where TFollowSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly IFollowCreateDomainService<TFollowSession, TUserSession> _followCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public FollowCreateCommandService(
            ITransactionManager transactionManager,
            IFollowCreateDomainService<TFollowSession, TUserSession> followCreateDomainService)
        {
            _transactionManager = transactionManager;
            _followCreateDomainService = followCreateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IFollowCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TFollowSession), typeof(TUserSession)],
                async sessions => await _followCreateDomainService.ExecuteAsync(
                    new IFollowCreateDomainService<TFollowSession, TUserSession>.Req()
                {
                    FollowId =
                        new FollowIdentifier()
                                {
                                    FollowFromId = req.FollowId.FollowFromId,
                                    FollowToId = req.FollowId.FollowToId
                                },
                    FollowSession = sessions.GetSession<TFollowSession>(),
                    FollowStartDateTime = req.FollowStartDateTime,
                    OperateInfo = req.OperateInfo,
                    UserSession = sessions.GetSession<TUserSession>(),
                }));
        }
    }
}
