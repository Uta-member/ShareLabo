using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public interface ITimeLineDeleteDomainService<TTimeLineSession>
        : IDomainService<ITimeLineDeleteDomainService<TTimeLineSession>.Req>
        where TTimeLineSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }
        }
    }
}
