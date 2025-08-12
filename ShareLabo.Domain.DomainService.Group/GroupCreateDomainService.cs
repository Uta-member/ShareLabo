using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.Group
{
    public sealed class GroupCreateDomainService<TGroupSession, TUserSession>
        : IDomainService<GroupCreateDomainService<TGroupSession, TUserSession>.Req>
        where TGroupSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public GroupCreateDomainService(
            GroupAggregateService<TGroupSession> groupAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _groupAggregateService = groupAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            foreach(var member in req.Members)
            {
                var targetUserOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                    req.UserSession,
                    member,
                    cancellationToken);
                if(!targetUserOptional.HasValue)
                {
                    throw new ObjectNotFoundException("メンバーに追加しようとしたユーザは存在しません");
                }
            }

            await _groupAggregateService.CreateAsync(
                new GroupAggregateService<TGroupSession>.CreateReq()
                {
                    Command =
                        new GroupEntity.CreateCommand()
                            {
                                Id = req.GroupId,
                                Members = req.Members,
                                Name = req.GroupName,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.GroupSession,
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required GroupId GroupId { get; init; }

            public required GroupName GroupName { get; init; }

            public required TGroupSession GroupSession { get; init; }

            public required ImmutableList<UserId> Members { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
