using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public interface IUserCreateDomainService<TUserSession>
        : IDomainService<IUserCreateDomainService<TUserSession>.Req>
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserAccountId UserAccountId { get; init; }

            public required UserId UserId { get; init; }

            public required UserName UserName { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
