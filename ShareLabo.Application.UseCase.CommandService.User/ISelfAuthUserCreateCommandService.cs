using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.Aggregate.Toolkit;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserCreateCommandService : ICommandService<ISelfAuthUserCreateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required AccountPassword AccountPassword { get; init; }

            public required OperateInfo OperateInfo { get; init; }

            public required UserAccountId UserAccountId { get; init; }

            public required UserId UserId { get; init; }

            public required UserName UserName { get; init; }
        }
    }
}
