using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineCreateDomainService<TTimeLineSession, TUserSession>
        : IDomainService<TimeLineCreateDomainService<TTimeLineSession, TUserSession>.Req>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public TimeLineCreateDomainService(
            TimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            UserAggregateService<TUserSession> userAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            foreach(var filterMember in req.FilterMembers)
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

            await _timeLineAggregateService.CreateAsync(
                new TimeLineAggregateService<TTimeLineSession>.CreateReq()
                {
                    Command =
                        new TimeLineEntity.CreateCommand()
                            {
                                FilterMembers = req.FilterMembers,
                                Id = req.Id,
                                Name = req.Name,
                                OwnerId = req.OwnerId,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required ImmutableList<UserId> FilterMembers { get; init; }

            public required TimeLineId Id { get; init; }

            public required TimeLineName Name { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId OwnerId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
