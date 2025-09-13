using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;
using System.Collections.Immutable;

namespace ShareLabo.Domain.DomainService.TimeLine
{
    public interface ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>
        : IDomainService<ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>.Req>
        where TTimeLineSession : IDisposable
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public Optional<ImmutableList<UserId>> FilterMembersOptional
            {
                get;
                init;
            } = Optional<ImmutableList<UserId>>.Empty;

            public Optional<TimeLineName> NameOptional { get; init; } = Optional<TimeLineName>.Empty;

            public required OperateInfo OperateInfo { get; init; }

            public required TimeLineId TargetId { get; init; }

            public required TTimeLineSession TimeLineSession { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
