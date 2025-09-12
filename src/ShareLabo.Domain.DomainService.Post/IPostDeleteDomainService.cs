using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Post
{
    public interface IPostDeleteDomainService<TPostSession> : IDomainService<IPostDeleteDomainService<TPostSession>.Req>
        where TPostSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TPostSession PostSession { get; init; }

            public required PostId TargetPostId { get; init; }
        }
    }
}
