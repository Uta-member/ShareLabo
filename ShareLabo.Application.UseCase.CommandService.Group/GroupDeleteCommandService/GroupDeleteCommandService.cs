
using CSStack.TADA;
using ShareLabo.Domain.DomainService.Group;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public sealed class GroupDeleteCommandService<TGroupSession> : IGroupDeleteCommandService
        where TGroupSession : IDisposable
    {
        private readonly GroupDeleteDomainService<TGroupSession> _groupDeleteDomainService;
        private readonly ITransactionManager _transactionManager;

        public GroupDeleteCommandService(
            ITransactionManager transactionManager,
            GroupDeleteDomainService<TGroupSession> groupDeleteDomainService)
        {
            _transactionManager = transactionManager;
            _groupDeleteDomainService = groupDeleteDomainService;
        }

        public async ValueTask ExecuteAsync(
            IGroupDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TGroupSession)],
                async sessions => await _groupDeleteDomainService.ExecuteAsync(
                    new GroupDeleteDomainService<TGroupSession>.Req()
                {
                    GroupSession = sessions.GetSession<TGroupSession>(),
                    OperateInfo = req.OperateInfo,
                    TargetId = req.TargetId,
                }));
        }
    }
}
