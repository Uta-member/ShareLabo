using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineUpdateDomainService<TTimeLineSession, TUserSession>
        : IDomainService<TimeLineUpdateDomainService<TTimeLineSession, TUserSession>.Req>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public TimeLineUpdateDomainService(
            TimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            Req req,
            CancellationToken cancellationToken = default)
        {
            if(req.FilterMembersOptional.TryGetValue(out var filterMembers))
            {
                foreach(var filterMember in filterMembers)
                {
                    var targetMemberOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                        req.UserSession,
                        filterMember,
                        cancellationToken);
                    if(!targetMemberOptional.HasValue)
                    {
                        throw new ObjectNotFoundException();
                    }
                }
            }

            await _timeLineAggregateService.UpdateAsync(
                new TimeLineAggregateService<TTimeLineSession>.UpdateReq()
                {
                    Command =
                        new TimeLineEntity.UpdateCommand()
                            {
                                FilterMembersOptional = req.FilterMembersOptional,
                                NameOptional = req.NameOptional,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                    TargetId = req.TargetId,
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public Optional<ImmutableList<UserId>> FilterMembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public Optional<TimeLineName> NameOptional { get; init; } = Optional<TimeLineName>.Empty;

            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
