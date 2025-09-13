using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Follow
{
    public interface IFollowDeleteDomainService<TFollowSession>
        : IDomainService<IFollowDeleteDomainService<TFollowSession>.Req>
        where TFollowSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required FollowIdentifier FollowId { get; init; }

            public required TFollowSession FollowSession { get; init; }

            public required OperateInfo OperateInfo { get; init; }
        }
    }
}
