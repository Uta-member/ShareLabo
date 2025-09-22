using CSStack.TADA;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.DomainService.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class OAuthUserCreateCommandService<TUserSession, TOAuthSession> : IOAuthUserCreateCommandService
        where TUserSession : IDisposable
        where TOAuthSession : IDisposable
    {
        private readonly IOAuthIntegrationService<TOAuthSession> _oauthIntegrationService;
        private readonly ITransactionManager _transactionManager;
        private readonly IUserCreateDomainService<TUserSession> _userCreateDomainService;

        public OAuthUserCreateCommandService(
            ITransactionManager transactionManager,
            IUserCreateDomainService<TUserSession> userCreateDomainService,
            IOAuthIntegrationService<TOAuthSession> oauthIntegrationService)
        {
            _transactionManager = transactionManager;
            _userCreateDomainService = userCreateDomainService;
            _oauthIntegrationService = oauthIntegrationService;
        }

        public async ValueTask ExecuteAsync(
            IOAuthUserCreateCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [ typeof(TUserSession), typeof(TOAuthSession) ],
                async sessions =>
                {
                    await _userCreateDomainService.ExecuteAsync(
                        new IUserCreateDomainService<TUserSession>.Req()
                            {
                                UserAccountId = UserAccountId.Create(req.UserAccountId),
                                OperateInfo = req.OperateInfo.ToOperateInfo(),
                                UserId = UserId.Reconstruct(req.UserId),
                                UserName = UserName.Create(req.UserName),
                                UserSession = sessions.GetSession<TUserSession>(),
                            },
                        cancellationToken: cancellationToken);

                    await _oauthIntegrationService.CreateAsync(
                        new IOAuthIntegrationService<TOAuthSession>.CreateReq()
                            {
                                OAuthInfos =
                                    [ new OAuthInfo()
                                            {
                                                OAuthIdentifier = req.OAuthIdentifier,
                                                OAuthType = req.OAuthType,
                                            } ],
                                Session = sessions.GetSession<TOAuthSession>(),
                                UserId = req.UserId,
                            });
                });
        }
    }
}
