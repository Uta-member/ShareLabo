using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserStatusUpdateDomainService<TUserSession> : IUserStatusUpdateDomainService<TUserSession>
        where TUserSession : IDisposable
    {
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public UserStatusUpdateDomainService(IUserAggregateService<TUserSession> userAggregateService)
        {
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IUserStatusUpdateDomainService<TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            if(req.ToEnabled)
            {
                await _userAggregateService.EnableAsync(
                    new IUserAggregateService<TUserSession>.StatusUpdateReq()
                    {
                        OperateInfo = req.OperateInfo,
                        Session = req.UserSession,
                        TargetId = req.TargetUserId,
                    });
            }
            else
            {
                await _userAggregateService.DisableAsync(
                    new IUserAggregateService<TUserSession>.StatusUpdateReq()
                    {
                        OperateInfo = req.OperateInfo,
                        Session = req.UserSession,
                        TargetId = req.TargetUserId,
                    });
            }
        }
    }
}
