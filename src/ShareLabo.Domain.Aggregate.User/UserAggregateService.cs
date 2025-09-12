using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.User
{
    public sealed class UserAggregateService<TSession>
        : AggregateServiceBase<UserEntity, UserId, IUserRepository<TSession>, OperateInfo, TSession>
        , IUserAggregateService<TSession>
        where TSession : IDisposable
    {
        public UserAggregateService(IUserRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(
            IUserAggregateService<TSession>.CreateReq req,
            CancellationToken cancellationToken = default)
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

        public async ValueTask DeleteAsync(
            IUserAggregateService<TSession>.StatusUpdateReq req,
            CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Delete();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DisableAsync(
            IUserAggregateService<TSession>.StatusUpdateReq req,
            CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Disable();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask EnableAsync(
            IUserAggregateService<TSession>.StatusUpdateReq req,
            CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Enable();
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask UpdateAsync(
            IUserAggregateService<TSession>.UpdateReq req,
            CancellationToken cancellationToken = default)
        {
            var entityOptional = await Repository.FindByIdentifierAsync(req.Session, req.TargetId, cancellationToken);
            if(!entityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Update(req.Command);
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }
    }
}
