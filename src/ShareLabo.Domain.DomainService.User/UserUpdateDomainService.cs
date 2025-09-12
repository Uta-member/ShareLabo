using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserUpdateDomainService<TUserSession>
        : IUserUpdateDomainService<TUserSession>
        where TUserSession : IDisposable
    {
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public UserUpdateDomainService(IUserAggregateService<TUserSession> userAggregateService)
        {
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IUserUpdateDomainService<TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _userAggregateService.UpdateAsync(
                new IUserAggregateService<TUserSession>.UpdateReq()
                {
                    TargetId = req.TargetId,
                    Command =
                        new UserEntity.UpdateCommand()
                            {
                                AccountIdOptional = req.UserAccountIdOptional,
                                NameOptional = req.UserNameOptional,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.UserSession,
                },
                cancellationToken);
        }
    }
}
