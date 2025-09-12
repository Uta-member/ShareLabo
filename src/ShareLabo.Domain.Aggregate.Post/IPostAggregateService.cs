using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post
{
    public interface IPostAggregateService<TSession>
        : IAggregateService<PostEntity, PostId, IPostRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(DeleteReq req, CancellationToken cancellationToken = default);

        ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default);

        public sealed record CreateReq
        {
            public required PostEntity.CreateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record UpdateReq
        {
            public required PostEntity.UpdateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required PostId TargetId { get; init; }
        }

        public sealed record DeleteReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required PostId TargetId { get; init; }
        }
    }
}
