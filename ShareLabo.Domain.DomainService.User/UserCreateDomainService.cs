using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserCreateDomainService<TUserSession>
        : IDomainService<UserCreateDomainService<TUserSession>.Req>
        where TUserSession : IDisposable
    {
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public UserCreateDomainService(UserAggregateService<TUserSession> userAggregateService)
        {
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _userAggregateService.CreateAsync(
                new UserAggregateService<TUserSession>.CreateReq()
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

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserAccountId UserAccountId { get; init; }

            public required UserId UserId { get; init; }

            public required UserName UserName { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
