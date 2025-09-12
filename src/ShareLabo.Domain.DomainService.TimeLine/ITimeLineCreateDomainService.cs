using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public interface ITimeLineCreateDomainService<TTimeLineSession, TUserSession>
        : IDomainService<ITimeLineCreateDomainService<TTimeLineSession, TUserSession>.Req>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
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
