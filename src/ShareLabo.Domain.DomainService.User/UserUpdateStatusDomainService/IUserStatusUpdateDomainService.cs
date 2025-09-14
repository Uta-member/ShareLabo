using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Domain.DomainService.User
{
    public interface IUserStatusUpdateDomainService<TUserSession>
        : IDomainService<IUserStatusUpdateDomainService<TUserSession>.Req>
        where TUserSession : IDisposable
    {
        public sealed record Req : IDomainServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetUserId { get; init; }

            public required bool ToEnabled { get; init; }

            public required TUserSession UserSession { get; init; }
        }
    }
}
