
using CSStack.TADA;
using ShareLabo.Domain.DomainService.Group;

namespace ShareLabo.Application.UseCase.CommandService.Group
{
    public sealed class GroupCreateCommandService<TGroupSession, TUserSesison> : IGroupCreateCommandService
        where TGroupSession : IDisposable
        where TUserSesison : IDisposable
    {
        private readonly GroupCreateDomainService<TGroupSession, TUserSesison> _groupCreateDomainService;
        private readonly ITransactionManager _transactionManager;

        public GroupCreateCommandService(
            ITransactionManager transactionManager,
            GroupCreateDomainService<TGroupSession, TUserSesison> groupCreateDomainService)
        {
            _transactionManager = transactionManager;
            _groupCreateDomainService = groupCreateDomainService;
        }

        public async ValueTask ExecuteAsync(
            IGroupCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TGroupSession), typeof(TUserSesison)],
                async sessions => await _groupCreateDomainService.ExecuteAsync(
                    new GroupCreateDomainService<TGroupSession, TUserSesison>.Req()
                {
                    GroupId = req.GroupId,
                    GroupName = req.GroupName,
                    GroupSession = sessions.GetSession<TGroupSession>(),
                    Members = req.Members,
                    OperateInfo = req.OperateInfo,
                    UserSession = sessions.GetSession<TUserSesison>(),
                }));
        }
    }
}
