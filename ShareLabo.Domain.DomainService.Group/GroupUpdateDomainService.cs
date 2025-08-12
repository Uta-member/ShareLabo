using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.Group
{
    public sealed class GroupUpdateDomainService<TGroupSession, TUserSession>
        : IDomainService<GroupUpdateDomainService<TGroupSession, TUserSession>.Req>
        where TGroupSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly GroupAggregateService<TGroupSession> _groupAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public GroupUpdateDomainService(
            GroupAggregateService<TGroupSession> groupAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _groupAggregateService = groupAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            if(req.MembersOptional.TryGetValue(out var members))
            {
                foreach(var member in members)
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
            }
            await _groupAggregateService.UpdateAsync(
                new GroupAggregateService<TGroupSession>.UpdateReq()
                {
                    Command =
                        new GroupEntity.UpdateCommand()
                            {
                                NameOptional = req.GroupNameOptional,
                                MembersOptional = req.MembersOptional,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.GroupSession,
                    TargetId = req.TargetId,
                },
                cancellationToken);
        }

        public sealed record Req : IDomainServiceDTO
        {
            public Optional<GroupName> GroupNameOptional { get; init; } = Optional<GroupName>.Empty;

            public required TGroupSession GroupSession { get; init; }

            public Optional<ImmutableList<UserId>> MembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public required OperateInfo OperateInfo { get; init; }

            public required GroupId TargetId { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
