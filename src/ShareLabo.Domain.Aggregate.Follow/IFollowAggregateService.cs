using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.Follow
{
    public interface IFollowAggregateService<TSession>
        : IAggregateService<FollowEntity, FollowIdentifier, IFollowRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(DeleteReq req, CancellationToken cancellationToken = default);

        ValueTask<ImmutableList<FollowEntity>> FindByFollowingUserIdAsync(
            TSession session,
            UserId followingUserId,
            CancellationToken cancellationToken = default);

        public sealed record CreateReq
        {
            public required FollowEntity.CreateCommand CreateCommand { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record DeleteReq
        {
            public required FollowIdentifier FollowId { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }
    }
}
