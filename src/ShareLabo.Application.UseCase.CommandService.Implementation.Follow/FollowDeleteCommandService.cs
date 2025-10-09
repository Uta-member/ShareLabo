using CSStack.TADA;
using ShareLabo.Domain.DomainService.Follow;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Follow
{
    public sealed class FollowDeleteCommandService<TFollowSession> : IFollowDeleteCommandService
        where TFollowSession : IDisposable
    {
        private readonly IFollowDeleteDomainService<TFollowSession> _followDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public FollowDeleteCommandService(
            ITransactionManager transactionManager,
            IFollowDeleteDomainService<TFollowSession> followDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _followDeleteDomainService = followDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            IFollowDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TFollowSession) ],
                async sessions => await _followDeleteDomainService.ExecuteAsync(
                    new IFollowDeleteDomainService<TFollowSession>.Req()
                    {
                        FollowId =
                            FollowIdentifier.Reconstruct(
                                        UserId.Reconstruct(req.FollowId.FollowFromId),
                                        UserId.Reconstruct(req.FollowId.FollowToId)),
                        FollowSession = sessions.GetSession<TFollowSession>(),
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                    }));
        }
    }
}
