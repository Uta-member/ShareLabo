
using CSStack.TADA;
using ShareLabo.Domain.DomainService.Group;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public sealed class GroupUpdateCommandService<TGroupSession, TUserSession> : IGroupUpdateCommandService
        where TGroupSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly GroupUpdateDomainService<TGroupSession, TUserSession> _groupUpdateDomainService;
        private readonly ITransactionManager _transactionManager;

        public GroupUpdateCommandService(
            ITransactionManager transactionManager,
            GroupUpdateDomainService<TGroupSession, TUserSession> groupUpdateDomainService)
        {
            _transactionManager = transactionManager;
            _groupUpdateDomainService = groupUpdateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IGroupUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TGroupSession), typeof(TUserSession)],
                async sessions => await _groupUpdateDomainService.ExecuteAsync(
                    new GroupUpdateDomainService<TGroupSession, TUserSession>.Req()
                {
                    GroupNameOptional = req.GroupNameOptional,
                    GroupSession = sessions.GetSession<TGroupSession>(),
                    MembersOptional = req.MembersOptional,
                    OperateInfo = req.OperateInfo,
                    TargetId = req.TargetId,
                    UserSession = sessions.GetSession<TUserSession>(),
                }));
        }
    }
}
