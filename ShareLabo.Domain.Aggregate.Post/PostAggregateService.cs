using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Post
{
    public sealed class PostAggregateService<TSession>
        : AggregateServiceBase<PostEntity, PostId, IPostRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        public PostAggregateService(IPostRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default)
        {
            var entity = PostEntity.Create(req.Command);
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
            targetEntity = targetEntity.Update(req.Command);
            await Repository.SaveAsync(req.Session, targetEntity, req.OperateInfo, cancellationToken);
        }

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
