using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineUpdateDomainService<TTimeLineSession, TUserSession>
        : ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly ITimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public TimeLineUpdateDomainService(
            ITimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            IUserAggregateService<TUserSession> userAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>.Req req,
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
                    if(!targetMemberOptional.TryGetValue(out var targetMember))
                    {
                        throw new ObjectNotFoundException();
                    }
                }
            }

            await _timeLineAggregateService.UpdateAsync(
                new ITimeLineAggregateService<TTimeLineSession>.UpdateReq()
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
    }
}
