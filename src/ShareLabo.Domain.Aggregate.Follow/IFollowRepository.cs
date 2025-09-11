using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.Follow
{
    public interface IFollowRepository<TSession> : IRepository<FollowEntity, FollowIdentifier, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask DeleteAsync(
            TSession session,
            FollowEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        ValueTask<ImmutableList<FollowEntity>> FindByFollowingUserIdAsync(
            TSession session,
            UserId followingUserId,
            CancellationToken cancellationToken = default);
    }
}
