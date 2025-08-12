using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.Authentication
{
    public interface IUserAccountRepository<TSession>
        where TSession : IDisposable
    {
        ValueTask CreateAsync(
            TSession session,
            UserAccountModel userAccount,
            AccountPassword accountPassword,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        ValueTask<Optional<UserAccountModel>> FindByUserAccountIdAsync(
            TSession session,
            UserAccountId userAccountId,
            CancellationToken cancellationToken = default);

        ValueTask<Optional<UserAccountModel>> FindByUserIdAsync(
            TSession session,
            UserId userId,
            CancellationToken cancellationToken = default);

        ValueTask<Optional<(UserAccountModel, string)>> FindWithPasswordHashByUserIdAsync(
            TSession session,
            UserId userId,
            CancellationToken cancellationToken = default);

        ValueTask SaveAsync(
            TSession session,
            UserAccountModel userAccount,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        ValueTask SaveAsync(
            TSession session,
            UserAccountModel userAccount,
            AccountPassword accountPassword,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        bool VerifyPassword(
            string passwordHash,
            AccountPassword accountPassword);
    }
}
