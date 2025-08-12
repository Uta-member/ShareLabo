using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public sealed class UserUpdateCommandService<TUserSession, TAuthSession> : IUserUpdateCommandService
        where TUserSession : IDisposable
        where TAuthSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountUpdateService<TAuthSession> _userAccountUpdateService;
        private readonly UserUpdateDomainService<TUserSession> _userUpdateDomainService;

        public UserUpdateCommandService(
            ITransactionManager transactionManager,
            UserUpdateDomainService<TUserSession> userUpdateDomainService,
            UserAccountUpdateService<TAuthSession> userAccountUpdateService)
        {
            _transactionManager = transactionManager;
            _userUpdateDomainService = userUpdateDomainService;
            _userAccountUpdateService = userAccountUpdateService;
        }

        public async ValueTask ExecuteAsync(
            IUserUpdateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TUserSession), typeof(TAuthSession)],
                async sessions =>
                {
                    await _userUpdateDomainService.ExecuteAsync(
                        new UserUpdateDomainService<TUserSession>.Req()
                        {
                            UserAccountIdOptional = req.UserAccountIdOptional,
                            OperateInfo = req.OperateInfo,
                            TargetId = req.TargetUserId,
                            UserNameOptional = req.UserNameOptional,
                            UserSession = sessions.GetSession<TUserSession>(),
                        });

                    await _userAccountUpdateService.ExecuteAsync(
                        new UserAccountUpdateService<TAuthSession>.Req()
                        {
                            UserAccountIdOptional = req.UserAccountIdOptional,
                            OperateInfo = req.OperateInfo,
                            Session = sessions.GetSession<TAuthSession>(),
                            TargetUserId = req.TargetUserId,
                        });
                });
        }
    }
}
