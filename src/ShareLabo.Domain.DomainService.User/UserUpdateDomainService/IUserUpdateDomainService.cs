using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public interface IUserUpdateDomainService<TUserSession>
        : IDomainService<IUserUpdateDomainService<TUserSession>.Req>
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetId { get; init; }

            public required Optional<UserAccountId> UserAccountIdOptional
            {
                get;
                init;
            } = Optional<UserAccountId>.Empty;

            public required Optional<UserName> UserNameOptional { get; init; } = Optional<UserName>.Empty;

            public required TUserSession UserSession { get; init; }
        }
    }
}
