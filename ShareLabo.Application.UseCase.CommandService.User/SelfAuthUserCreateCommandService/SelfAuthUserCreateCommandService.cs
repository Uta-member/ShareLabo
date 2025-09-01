using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public sealed class SelfAuthUserCreateCommandService<TUserSession, TAuthSession, TTimeLineSession>
        : ISelfAuthUserCreateCommandService
        where TUserSession : IDisposable
        where TAuthSession : IDisposable
        where TTimeLineSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountCreateService<TAuthSession> _userAccountCreateService;
        private readonly UserCreateDomainService<TUserSession, TTimeLineSession> _userCreateDomainService;

        public SelfAuthUserCreateCommandService(
            ITransactionManager transactionManager,
            UserCreateDomainService<TUserSession, TTimeLineSession> userCreateDomainService,
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
                [typeof(TUserSession), typeof(TAuthSession), typeof(TTimeLineSession)],
                async sessions =>
                {
                    await _userCreateDomainService.ExecuteAsync(
                        new UserCreateDomainService<TUserSession, TTimeLineSession>.Req()
                        {
                            UserAccountId = req.UserAccountId,
                            OperateInfo = req.OperateInfo,
                            UserId = req.UserId,
                            UserName = req.UserName,
                            UserSession = sessions.GetSession<TUserSession>(),
                            TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                        },
                        cancellationToken);

                    await _userAccountCreateService.ExecuteAsync(
                        new UserAccountCreateService<TAuthSession>.Req()
                        {
                            AccountPassword = req.AccountPassword,
                            UserAccountId = req.UserAccountId,
                            OperateInfo = req.OperateInfo,
                            Session = sessions.GetSession<TAuthSession>(),
                            UserId = req.UserId,
                        },
                        cancellationToken);
                });
        }
    }
}
