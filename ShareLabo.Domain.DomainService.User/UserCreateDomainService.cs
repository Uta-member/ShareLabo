using CSStack.TADA;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public sealed class UserCreateDomainService<TUserSession, TTimeLineSession>
        : IDomainService<UserCreateDomainService<TUserSession, TTimeLineSession>.Req>
        where TUserSession : IDisposable
        where TTimeLineSession : IDisposable
    {
        private readonly TimeLineAggregateService<TTimeLineSession> _timeLineAggregateService;
        private readonly UserAggregateService<TUserSession> _userAggregateService;

        public UserCreateDomainService(
            UserAggregateService<TUserSession> userAggregateService,
            TimeLineAggregateService<TTimeLineSession> timeLineAggregateService)
        {
            _userAggregateService = userAggregateService;
            _timeLineAggregateService = timeLineAggregateService;
        }

        public async ValueTask ExecuteAsync(Req req, CancellationToken cancellationToken = default)
        {
            await _userAggregateService.CreateAsync(
                new UserAggregateService<TUserSession>.CreateReq()
                {
                    Command =
                        new UserEntity.CreateCommand()
                            {
                                Id = req.UserId,
                                AccountId = req.UserAccountId,
                                Name = req.UserName,
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.UserSession,
                },
                cancellationToken);

            await _timeLineAggregateService.CreateAsync(
                new TimeLineAggregateService<TTimeLineSession>.CreateReq()
                {
                    Command =
                        new TimeLineEntity.CreateCommand()
                            {
                                FilterMembers = [req.UserId],
                                OwnerId = req.UserId,
                                Id = TimeLineId.Create(Guid.NewGuid().ToString()),
                                Name = TimeLineName.Create(req.UserName.Value),
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                });

            await _timeLineAggregateService.CreateAsync(
                new TimeLineAggregateService<TTimeLineSession>.CreateReq()
                {
                    Command =
                        new TimeLineEntity.CreateCommand()
                            {
                                FilterMembers = [],
                                OwnerId = req.UserId,
                                Id = TimeLineId.Create(Guid.NewGuid().ToString()),
                                Name = TimeLineName.Create("全体"),
                            },
                    OperateInfo = req.OperateInfo,
                    Session = req.TimeLineSession,
                });
        }

        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required UserAccountId UserAccountId { get; init; }

            public required UserId UserId { get; init; }

            public required UserName UserName { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
