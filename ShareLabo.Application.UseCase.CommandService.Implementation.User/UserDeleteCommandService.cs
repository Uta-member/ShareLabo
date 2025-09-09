using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Application.UseCase.CommandService.Implementation.User
{
    public sealed class UserDeleteCommandService<TUserSession, TAuthSession, TTimeLineSession, TFollowSession>
        : IUserDeleteCommandService
        where TUserSession : IDisposable
        where TAuthSession : IDisposable
        where TTimeLineSession : IDisposable
        where TFollowSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountDeleteService<TAuthSession> _userAccountDeleteService;
        private readonly UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession> _userDeleteDomainService;

        public UserDeleteCommandService(
            ITransactionManager transactionManager,
            UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession> userDeleteDomainService,
            UserAccountDeleteService<TAuthSession> userAccountDeleteService)
        {
            _transactionManager = transactionManager;
            _userDeleteDomainService = userDeleteDomainService;
            _userAccountDeleteService = userAccountDeleteService;
        }

        public async ValueTask ExecuteAsync(
            IUserDeleteCommandService.Req req,
            CancellationToken cancellationToken = default)
        {
            await _transactionManager.ExecuteTransactionAsync(
                [typeof(TUserSession), typeof(TAuthSession), typeof(TTimeLineSession), typeof(TFollowSession)],
                async sessions =>
                {
                    await _userDeleteDomainService.ExecuteAsync(
                        new UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>.Req()
                        {
                            OperateInfo = req.OperateInfo,
                            TargetId = req.TargetId,
                            UserSession = sessions.GetSession<TUserSession>(),
                            TimeLineSession = sessions.GetSession<TTimeLineSession>(),
                            FollowSession = sessions.GetSession<TFollowSession>(),
                        });
                    await _userAccountDeleteService.ExecuteAsync(
                        new UserAccountDeleteService<TAuthSession>.Req()
                        {
                            OperateInfo = req.OperateInfo,
                            Session = sessions.GetSession<TAuthSession>(),
                            TargetId = req.TargetId,
                        });
                });
        }
    }
}
