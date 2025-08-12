using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.Group
{
    public interface IGroupRepository<TSession>
        : IRepository<GroupEntity, GroupId, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask<ImmutableList<GroupEntity>> GetByMemberAsync(
            TSession session,
            UserId memberId,
            CancellationToken cancellationToken = default);

        ValueTask RemoveAsync(
            TSession session,
            GroupEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);
    }
}
