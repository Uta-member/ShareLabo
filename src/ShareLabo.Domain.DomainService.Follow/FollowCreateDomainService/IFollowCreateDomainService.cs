using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow
{
    public interface IFollowCreateDomainService<TFollowSession, TUserSession>
        : IDomainService<IFollowCreateDomainService<TFollowSession, TUserSession>.Req>
        where TFollowSession : IDisposable
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required TFollowSession FollowSession { get; init; }

            public required DateTime FollowStartDateTime { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
