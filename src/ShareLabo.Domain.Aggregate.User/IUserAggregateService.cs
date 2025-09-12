using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User
{
    public interface IUserAggregateService<TSession>
        : IAggregateService<UserEntity, UserId, IUserRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(StatusUpdateReq req, CancellationToken cancellationToken = default);

        ValueTask DisableAsync(StatusUpdateReq req, CancellationToken cancellationToken = default);

        ValueTask EnableAsync(StatusUpdateReq req, CancellationToken cancellationToken = default);

        ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default);

        public sealed record CreateReq
        {
            public required UserEntity.CreateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record UpdateReq
        {
            public required UserEntity.UpdateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserId TargetId { get; init; }
        }

        public sealed record StatusUpdateReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserId TargetId { get; init; }
        }
    }
}
