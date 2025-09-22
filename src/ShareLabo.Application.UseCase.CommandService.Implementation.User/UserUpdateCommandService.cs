using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class UserUpdateCommandService<TUserSession, TAuthSession> : IUserUpdateCommandService
        where TUserSession : IDisposable
        where TAuthSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountUpdateService<TAuthSession> _userAccountUpdateService;
        private readonly IUserUpdateDomainService<TUserSession> _userUpdateDomainService;

        public UserUpdateCommandService(
            ITransactionManager transactionManager,
            IUserUpdateDomainService<TUserSession> userUpdateDomainService,
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
                [ typeof(TUserSession), typeof(TAuthSession) ],
                async sessions =>
                {
                    await _userUpdateDomainService.ExecuteAsync(
                        new IUserUpdateDomainService<TUserSession>.Req()
                            {
                                UserAccountIdOptional =
                                    req.UserAccountIdOptional.TryGetValue(out var userAccountId)
                                                ? UserAccountId.Create(userAccountId)
                                                : Optional<UserAccountId>.Empty,
                                OperateInfo = req.OperateInfo.ToOperateInfo(),
                                TargetId = UserId.Reconstruct(req.TargetUserId),
                                UserNameOptional =
                                    req.UserNameOptional.TryGetValue(out var userName)
                                                ? UserName.Create(userName)
                                                : Optional<UserName>.Empty,
                                UserSession = sessions.GetSession<TUserSession>(),
                            });

                    await _userAccountUpdateService.ExecuteAsync(
                        new UserAccountUpdateService<TAuthSession>.Req()
                            {
                                UserAccountIdOptional =
                                    req.UserAccountIdOptional.TryGetValue(out userAccountId)
                                                ? UserAccountId.Create(userAccountId)
                                                : Optional<UserAccountId>.Empty,
                                OperateInfo = req.OperateInfo.ToOperateInfo(),
                                Session = sessions.GetSession<TAuthSession>(),
                                TargetUserId = UserId.Reconstruct(req.TargetUserId),
                            });
                });
        }
    }
}
