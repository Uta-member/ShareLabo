using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post
{
    public interface IPostRepository<TSession> : IRepository<PostEntity, PostId, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask DeleteAsync(
            TSession session,
            PostEntity entity,
            OperateInfo operateInfo,
            CancellationToken cancellationToken = default);

        ValueTask<Optional<PostEntity>> FindLatestPostAsync(
            TSession session,
            CancellationToken cancellationToken = default);
    }
}
