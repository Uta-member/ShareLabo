using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.User;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public sealed class TimeLineCreateDomainService<TTimeLineSession, TUserSession>
        : ITimeLineCreateDomainService<TTimeLineSession, TUserSession>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        private readonly ITimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly IUserAggregateService<TUserSession> _userAggregateService;

        public TimeLineCreateDomainService(
            ITimeLineAggregateService<TTimeLineSession> timeLineAggregateService,
            IUserAggregateService<TUserSession> userAggregateService)
        {
            _timeLineAggregateService = timeLineAggregateService;
            _userAggregateService = userAggregateService;
        }

        public async ValueTask ExecuteAsync(
            ITimeLineCreateDomainService<TTimeLineSession, TUserSession>.Req req,
            CancellationToken cancellationToken = default)
        {
            foreach(var filterMember in req.FilterMembers)
            {
                var targetMemberOptional = await _userAggregateService.GetEntityByIdentifierAsync(
                    req.UserSession,
                    filterMember,
                    cancellationToken);
                if(!targetMemberOptional.TryGetValue(out var targetMember) ||
                    targetMember.Status != UserEntity.StatusEnum.Enabled)
                {
                    throw new ObjectNotFoundException();
                }
            }

            await _timeLineAggregateService.CreateAsync(
                new ITimeLineAggregateService<TTimeLineSession>.CreateReq()
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
    }
}
