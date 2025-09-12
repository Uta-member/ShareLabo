using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.Aggregate.Follow
{
    public sealed class FollowAggregateService<TSession>
        : AggregateServiceBase<FollowEntity, FollowIdentifier, IFollowRepository<TSession>, OperateInfo, TSession>
        , IFollowAggregateService<TSession>
        where TSession : IDisposable
    {
        public FollowAggregateService(IFollowRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(
            IFollowAggregateService<TSession> .CreateReq req,
            CancellationToken cancellationToken = default)
        {
            var entity = FollowEntity.Create(req.CreateCommand);

            var targetFoolowEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                entity.Identifier,
                cancellationToken);

            if(targetFoolowEntityOptional.HasValue)
            {
                throw new ObjectAlreadyExistException();
            }

            await Repository.SaveAsync(
                req.Session,
                entity,
                req.OperateInfo,
                cancellationToken);
        }

        public async ValueTask DeleteAsync(
            IFollowAggregateService<TSession> .DeleteReq req,
            CancellationToken cancellationToken = default)
        {
            var targetFollowEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                req.FollowId,
                cancellationToken);

            if(!targetFollowEntityOptional.TryGetValue(out var targetFollowEntity))
            {
                throw new ObjectNotFoundException();
            }

            await Repository.DeleteAsync(
                req.Session,
                targetFollowEntity,
                req.OperateInfo,
                cancellationToken);
        }

        public async ValueTask<ImmutableList<FollowEntity>> FindByFollowingUserIdAsync(
            TSession session,
            UserId followingUserId,
            CancellationToken cancellationToken = default)
        {
            return await Repository.FindByFollowingUserIdAsync(
                session,
                followingUserId,
                cancellationToken);
        }
    }
}
