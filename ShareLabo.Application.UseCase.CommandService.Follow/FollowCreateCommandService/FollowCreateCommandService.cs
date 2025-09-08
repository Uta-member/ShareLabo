
using CSStack.TADA;
using ShareLabo.Domain.DomainService.Follow;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public sealed class FollowCreateCommandService<TFollowSession, TUserSession> : IFollowCreateCommandService
        where TFollowSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly FollowCreateDomainService<TFollowSession, TUserSession> _followCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public FollowCreateCommandService(
            ITransactionManager transactionManager,
            FollowCreateDomainService<TFollowSession, TUserSession> followCreateDomainService)
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
                    new FollowCreateDomainService<TFollowSession, TUserSession>.Req()
                {
                    FollowId = req.FollowId,
                    FollowSession = sessions.GetSession<TFollowSession>(),
                    FollowStartDateTime = req.FollowStartDateTime,
                    OperateInfo = req.OperateInfo,
                    UserSession = sessions.GetSession<TUserSession>(),
                }));
        }
    }
}
