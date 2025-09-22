using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class SelfAuthUserCreateCommandService<TUserSession, TAuthSession>
        : ISelfAuthUserCreateCommandService
        where TUserSession : IDisposable
        where TAuthSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountCreateService<TAuthSession> _userAccountCreateService;
        private readonly IUserCreateDomainService<TUserSession> _userCreateDomainService;

        public SelfAuthUserCreateCommandService(
            ITransactionManager transactionManager,
            IUserCreateDomainService<TUserSession> userCreateDomainService,
            UserAccountCreateService<TAuthSession> userAccountCreateService)
        {
            _transactionManager = transactionManager;
            _userCreateDomainService = userCreateDomainService;
            _userAccountCreateService = userAccountCreateService;
        }

        public async ValueTask ExecuteAsync(
            ISelfAuthUserCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TUserSession), typeof(TAuthSession) ],
                async sessions =>
                {
                    await _userCreateDomainService.ExecuteAsync(
                        new IUserCreateDomainService<TUserSession>.Req()
                            {
                                UserAccountId = UserAccountId.Create(req.UserAccountId),
                                OperateInfo = req.OperateInfo.ToOperateInfo(),
                                UserId = UserId.Create(req.UserId),
                                UserName = UserName.Create(req.UserName),
                                UserSession = sessions.GetSession<TUserSession>(),
                            },
                        cancellationToken);

                    await _userAccountCreateService.ExecuteAsync(
                        new UserAccountCreateService<TAuthSession>.Req()
                            {
                                AccountPassword = AccountPassword.Create(req.AccountPassword),
                                UserAccountId = UserAccountId.Create(req.UserAccountId),
                                OperateInfo = req.OperateInfo.ToOperateInfo(),
                                Session = sessions.GetSession<TAuthSession>(),
                                UserId = UserId.Create(req.UserId),
                            },
                        cancellationToken);
                });
        }
    }
}
