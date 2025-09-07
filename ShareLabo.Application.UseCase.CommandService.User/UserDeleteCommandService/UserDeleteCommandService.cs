using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public sealed class UserDeleteCommandService<TUserSession, TGroupSession, TAuthSession, TTimeLineSession, TFollowSession>
        : IUserDeleteCommandService
        where TUserSession : IDisposable
        where TGroupSession : IDisposable
        where TAuthSession : IDisposable
        where TTimeLineSession : IDisposable
        where TFollowSession : IDisposable
    {
        private readonly ITransactionManager _transactionManager;
        private readonly UserAccountDeleteService<TAuthSession> _userAccountDeleteService;
        private readonly UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession, TFollowSession> _userDeleteDomainService;

        public UserDeleteCommandService(
            ITransactionManager transactionManager,
            UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession, TFollowSession> userDeleteDomainService,
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
                [typeof(TUserSession), typeof(TGroupSession), typeof(TAuthSession), typeof(TTimeLineSession), typeof(TFollowSession)],
                async sessions =>
                {
                    await _userDeleteDomainService.ExecuteAsync(
                        new UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession, TFollowSession>.Req()
                        {
                            GroupSession = sessions.GetSession<TGroupSession>(),
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
