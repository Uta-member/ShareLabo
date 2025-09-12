using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.TimeLine
{
    public interface ITimeLineAggregateService<TSession>
        : IAggregateService<TimeLineEntity, TimeLineId, ITimeLineRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(DeleteReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteByOwnerAsync(DeleteByOwnerReq req, CancellationToken cancellationToken = default);

        ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default);

        public sealed record DeleteByOwnerReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId OwnerId { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record CreateReq
        {
            public required TimeLineEntity.CreateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record UpdateReq
        {
            public required TimeLineEntity.UpdateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required TimeLineId TargetId { get; init; }
        }

        public sealed record DeleteReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required TimeLineId TargetId { get; init; }
        }
    }
}
