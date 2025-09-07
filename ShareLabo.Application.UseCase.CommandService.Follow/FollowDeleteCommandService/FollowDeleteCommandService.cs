
using CSStack.TADA;
using ShareLabo.Domain.DomeinService.Follow;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public sealed class FollowDeleteCommandService<TFollowSession> : IFollowDeleteCommandService
        where TFollowSession : IDisposable
    {
        private readonly FollowDeleteDomainService<TFollowSession> _followDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public FollowDeleteCommandService(
            ITransactionManager transactionManager,
            FollowDeleteDomainService<TFollowSession> followDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _followDeleteDomainService = followDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            IFollowDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TFollowSession)],
                async sessions => await _followDeleteDomainService.ExecuteAsync(
                    new FollowDeleteDomainService<TFollowSession>.Req()
                {
                    FollowId = req.FollowId,
                    FollowSession = sessions.GetSession<TFollowSession>(),
                    OperateInfo = req.OperateInfo,
                }));
        }
    }
}
