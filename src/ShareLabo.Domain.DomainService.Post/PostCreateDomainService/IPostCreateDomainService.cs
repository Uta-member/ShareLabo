using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.Post
{
    public interface IPostCreateDomainService<TPostSession, TUserSession>
        : IDomainService<IPostCreateDomainService<TPostSession, TUserSession>.Req>
        where TPostSession : IDisposable
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required PostContent PostContent { get; init; }

            public required DateTime PostDateTime { get; init; }

            public required PostId PostId { get; init; }

            public required TPostSession PostSession { get; init; }

            public required PostTitle PostTitle { get; init; }

            public required UserId PostUser { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
