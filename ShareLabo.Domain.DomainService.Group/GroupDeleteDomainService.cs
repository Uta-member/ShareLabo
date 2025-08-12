using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Group
{
    public sealed class GroupDeleteDomainService<TGroupSession>
        : IDomainService<GroupDeleteDomainService<TGroupSession>.Req>
        where TGroupSession : IDisposable
    {
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;

        public GroupDeleteDomainService(GroupAggregateService<TGroupSession> groupAggregateService)
        {
            _groupAggregateService = groupAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _groupAggregateService.DeleteAsync(
                new GroupAggregateService<TGroupSession>.DeleteReq()
                {
                    OperateInfo = req.OperateInfo,
                    Session = req.GroupSession,
                    TargetId = req.TargetId
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required TGroupSession GroupSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required GroupId TargetId { get; init; }
        }
    }
}
