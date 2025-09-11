using CSStack.TADA;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface IUserUpdateCommandService : ICommandService<IUserUpdateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required OperateInfo OperateInfo { get; init; }

            public required UserId TargetUserId { get; init; }

            public required Optional<UserAccountId> UserAccountIdOptional
            {
                get;
                init;
            } = Optional<UserAccountId>.Empty;

            public Optional<UserName> UserNameOptional { get; init; } = Optional<UserName>.Empty;
        }
    }
}
