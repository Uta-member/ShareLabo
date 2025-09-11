using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User
{
    public sealed class UserAggregateService<TSession>
        : AggregateServiceBase<UserEntity, UserId, IUserRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        public UserAggregateService(IUserRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default)
        {
            var entity = UserEntity.Create(req.Command);
            var targetEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                entity.Identifier,
                cancellationToken);
            if(targetEntityOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }
            targetEntityOptional = await Repository.FindByAccountIdAsync(
                req.Session,
                entity.AccountId,
                cancellationToken);
            if(targetEntityOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DeleteAsync(StatusUpdateReq req, CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Delete();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DisableAsync(StatusUpdateReq req, CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Disable();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask EnableAsync(StatusUpdateReq req, CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Enable();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Update(req.Command);
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

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
