using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User
{
    public interface IUserRepository<TSession> : IRepository<UserEntity, UserId, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask<Optional<UserEntity>> FindByAccountIdAsync(
            TSession session,
            UserAccountId accountId,
            CancellationToken cancellationToken = default);
    }
}
