using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class SelfAuthUserLoginCommandService<TAuthSession> : ISelfAuthUserLoginCommandService
        where TAuthSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserLoginService<TAuthSession> _userLoginService;

        public SelfAuthUserLoginCommandService(
            ITransactionManager transactionManager,
            UserLoginService<TAuthSession> userLoginService)
        {
            _transactionManager = transactionManager;
            _userLoginService = userLoginService;
        }

        public async ValueTask<ISelfAuthUserLoginCommandService.Res> ExecuteAsync(
            ISelfAuthUserLoginCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            ISelfAuthUserLoginCommandService.Res res = default!;

            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TAuthSession) ],
                async sessions =>
                {
                    var result = await _userLoginService.ExecuteAsync(
                        new UserLoginService<TAuthSession>.Req()
                            {
                                UserAccountId = UserAccountId.Reconstruct(req.UserAccountId),
                                Password = AccountPassword.Reconstruct(req.AccountPassword),
                                Session = sessions.GetSession<TAuthSession>(),
                            });
                    res = new ISelfAuthUserLoginCommandService.Res()
                    {
                        IsAuthorized = result.IsAuthorized,
                        LoginResultDetail = result.LoginResultDetail,
                    };
                });

            return res;
        }
    }
}
