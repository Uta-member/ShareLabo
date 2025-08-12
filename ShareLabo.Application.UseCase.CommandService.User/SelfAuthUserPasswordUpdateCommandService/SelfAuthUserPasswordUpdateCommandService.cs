using CSStack.TADA;
using ShareLabo.Application.Authentication;

namespace ShareLabo.Application.UseCase.CommandService.User
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
                [typeof(TAuthSession)],
                async sessions => await _userAccountPasswordUpdateService.ExecuteAsync(
                    new UserAccountPasswordUpdateService<TAuthSession>.Req()
                {
                    CurrentPassword = req.CurrentPassword,
                    NewPassword = req.NewPassword,
                    OperateInfo = req.OperateInfo,
                    Session = sessions.GetSession<TAuthSession>(),
                    TargetUserId = req.TargetUserId,
                }));
        }
    }
}
