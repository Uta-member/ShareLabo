using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.TimeLine
{
    public sealed class TimeLineAggregateService<TSession>
        : AggregateServiceBase<TimeLineEntity, TimeLineId, ITimeLineRepository<TSession>, OperateInfo, TSession>
        , ITimeLineAggregateService<TSession>
        where TSession : IDisposable
    {
        public TimeLineAggregateService(ITimeLineRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(
            ITimeLineAggregateService<TSession>.CreateReq req,
            CancellationToken cancellationToken = default)
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

        public async ValueTask DeleteAsync(
            ITimeLineAggregateService<TSession>.DeleteReq req,
            CancellationToken cancellationToken = default)
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

        public async ValueTask DeleteByOwnerAsync(
            ITimeLineAggregateService<TSession>.DeleteByOwnerReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntities = await Repository.GetByOwnerIdAsync(req.Session, req.OwnerId, cancellationToken);
            foreach(var targetEntity in targetEntities)
            {
                await Repository.DeleteAsync(req.Session, targetEntity, req.OperateInfo, cancellationToken);
            }
        }

        public async ValueTask UpdateAsync(
            ITimeLineAggregateService<TSession>.UpdateReq req,
            CancellationToken cancellationToken = default)
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
    }
}
