using CSStack.TADA;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserCreateCommandService : ICommandService<ISelfAuthUserCreateCommandService.Req>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required string AccountPassword { get; init; }

            public required OperateInfoWriteModel OperateInfo { get; init; }

            public required string UserAccountId { get; init; }

            public required string UserId { get; init; }

            public required string UserName { get; init; }
        }
    }
}
