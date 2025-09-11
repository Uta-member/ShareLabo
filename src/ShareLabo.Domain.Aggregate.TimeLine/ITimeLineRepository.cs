using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.TimeLine
{
    public interface ITimeLineRepository<TSession> : IRepository<TimeLineEntity, TimeLineId, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask DeleteAsync(
            TSession session,
            TimeLineEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        ValueTask<ImmutableList<TimeLineEntity>> GetByOwnerIdAsync(
            TSession session,
            UserId ownerId,
            CancellationToken cancellationToken = default);
    }
}
