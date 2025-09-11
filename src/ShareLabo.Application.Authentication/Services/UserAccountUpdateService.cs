using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public sealed class UserAccountUpdateService<TSession>
        where TSession : IDisposable
    {
        private readonly IUserAccountRepository<TSession> _userAccountRepository;

        public UserAccountUpdateService(IUserAccountRepository<TSession> userAccountRepository)
        {
            _userAccountRepository = userAccountRepository;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            var targetAccountOptional = await _userAccountRepository.FindByUserIdAsync(
                req.Session,
                req.TargetUserId,
                cancellationToken);
            if(!targetAccountOptional.TryGetValue(out var targetAccount))
            {
                throw new ObjectNotFoundException();
            }

            if(targetAccount.Status == UserAccountModel.StatusEnum.Deleted)
            {
                throw new InvalidOperationException();
            }

            await _userAccountRepository.SaveAsync(
                req.Session,
                targetAccount with
                {
                    UserAccountId = req.UserAccountIdOptional.GetValue(targetAccount.UserAccountId),
                },
                req.OperateInfo,
                cancellationToken);
        }

        public sealed record Req
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required Optional<UserAccountId> UserAccountIdOptional { get; init; }

            public required UserId TargetUserId { get; init; }
        }
    }
}
