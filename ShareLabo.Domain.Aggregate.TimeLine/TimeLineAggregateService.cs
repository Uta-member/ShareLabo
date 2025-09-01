using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.TimeLine
{
    public sealed class TimeLineAggregateService<TSession>
        : AggregateServiceBase<TimeLineEntity, TimeLineId, ITimeLineRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        public TimeLineAggregateService(ITimeLineRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default)
        {
            var entity = TimeLineEntity.Create(req.Command);
            var targetEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                entity.Identifier,
                cancellationToken);
            if(targetEntityOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DeleteAsync(DeleteReq req, CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                req.TargetId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            await Repository.DeleteAsync(req.Session, targetEntity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DeleteByOwnerAsync(DeleteByOwnerReq req, CancellationToken cancellationToken = default)
        {
            var targetEntities = await Repository.GetByOwnerIdAsync(req.Session, req.OwnerId, cancellationToken);
            foreach(var targetEntity in targetEntities)
            {
                await Repository.DeleteAsync(req.Session, targetEntity, req.OperateInfo, cancellationToken);
            }
        }

        public async ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                req.TargetId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var targetEntity))
            {
                throw new ObjectNotFoundException();
            }
            var newEntity = targetEntity.Update(req.Command);
            await Repository.SaveAsync(req.Session, newEntity, req.OperateInfo, cancellationToken);
        }

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
