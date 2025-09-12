using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public interface IUserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>
        : IDomainService<IUserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>.Req>
        where TUserSession : IDisposable
        where TTimeLineSession : IDisposable
        where TFollowSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required TFollowSession FollowSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
