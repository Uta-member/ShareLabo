using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserUpdateDomainService<TUserSession>
        : IDomainService<UserUpdateDomainService<TUserSession>.Req>
        where TUserSession : IDisposable
    {
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public UserUpdateDomainService(UserAggregateService<TUserSession> userAggregateService)
        {
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _userAggregateService.UpdateAsync(
                new UserAggregateService<TUserSession>.UpdateReq()
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

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }

            public required Optional<UserAccountId> UserAccountIdOptional
            {
                get;
                init;
            } = Optional<UserAccountId>.Empty;

            public required Optional<UserName> UserNameOptional { get; init; } = Optional<UserName>.Empty;

            public required TUserSession UserSession { get; init; }
        }
    }
}
