using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserCreateDomainService<TUserSession>
        : IUserCreateDomainService<TUserSession>
        where TUserSession : IDisposable
    {
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public UserCreateDomainService(
            IUserAggregateService<TUserSession> userAggregateService)
        {
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            IUserCreateDomainService<TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            await _userAggregateService.CreateAsync(
                new IUserAggregateService<TUserSession>.CreateReq()
                {
                    Command =
                        new UserEntity.CreateCommand()
                            {
                                Id = req.UserId,
                                AccountId = req.UserAccountId,
                                Name = req.UserName,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.UserSession,
                },
                cancellationToken);
        }
    }
}
