using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class SelfAuthUserPasswordUpdateCommandService<TAuthSession>
        : ISelfAuthUserPasswordUpdateCommandService
        where TAuthSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountPasswordUpdateService<TAuthSession> _userAccountPasswordUpdateService;

        public SelfAuthUserPasswordUpdateCommandService(
            ITransactionManager transactionManager,
            UserAccountPasswordUpdateService<TAuthSession> userAccountPasswordUpdateService)
        {
            _transactionManager = transactionManager;
            _userAccountPasswordUpdateService = userAccountPasswordUpdateService;
        }

        public async ValueTask ExecuteAsync(
            ISelfAuthUserPasswordUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TAuthSession) ],
                async sessions => await _userAccountPasswordUpdateService.ExecuteAsync(
                    new UserAccountPasswordUpdateService<TAuthSession>.Req()
                    {
                        CurrentPassword = AccountPassword.Reconstruct(req.CurrentPassword),
                        NewPassword = AccountPassword.Create(req.NewPassword),
                        OperateInfo = req.OperateInfo.ToOperateInfo(),
                        Session = sessions.GetSession<TAuthSession>(),
                        TargetUserId = UserId.Reconstruct(req.TargetUserId),
                    }));
        }
    }
}
