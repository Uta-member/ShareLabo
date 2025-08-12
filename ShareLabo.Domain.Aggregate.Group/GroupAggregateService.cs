using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.Aggregate.Group
{
    public sealed class GroupAggregateService<TSession>
        : AggregateServiceBase<GroupEntity, GroupId, IGroupRepository<TSession>, OperateInfo, TSession>
        where TSession : IDisposable
    {
        public GroupAggregateService(IGroupRepository<TSession> repository)
            : base(repository)
        {
        }

        public async ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default)
        {
            var entity = GroupEntity.Create(req.Command);
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
            if(!targetEntityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            await Repository.RemoveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public async ValueTask DisconnectMemberAsync(
            MemberDisconnectReq req,
            CancellationToken cancellationToken = default)
        {
            var targetEntities = await Repository.GetByMemberAsync(
                req.Session,
                req.TargetUserId,
                cancellationToken);
            foreach(var entity in targetEntities)
            {
                var disconnectedEntity = entity.Update(
                    new GroupEntity.UpdateCommand()
                    {
                        MembersOptional = entity.Members.Remove(req.TargetUserId),
                    });
                await Repository.SaveAsync(
                    req.Session,
                    disconnectedEntity,
                    req.OperateInfo,
                    cancellationToken);
            }
        }

        public async ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default)
        {
            var targetEntityOptional = await Repository.FindByIdentifierAsync(
                req.Session,
                req.TargetId,
                cancellationToken);
            if(!targetEntityOptional.TryGetValue(out var entity))
            {
                throw new ObjectNotFoundException();
            }
            entity = entity.Update(req.Command);
            await Repository.SaveAsync(req.Session, entity, req.OperateInfo, cancellationToken);
        }

        public sealed record MemberDisconnectReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required UserId TargetUserId { get; init; }
        }

        public sealed record CreateReq
        {
            public required GroupEntity.CreateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }
        }

        public sealed record UpdateReq
        {
            public required GroupEntity.UpdateCommand Command { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required GroupId TargetId { get; init; }
        }

        public sealed record DeleteReq
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TSession Session { get; init; }

            public required GroupId TargetId { get; init; }
        }
    }
}
