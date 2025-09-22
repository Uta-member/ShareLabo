using CSStack.TADA;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.Toolkit;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface IOAuthUserCreateCommandService : ICommandService<IOAuthUserCreateCommandService.Req>
    {
        sealed record Req : ICommandServiceDTO
        {
            public required string OAuthIdentifier { get; init; }

            public required OAuthType OAuthType { get; init; }

            public required OperateInfoWriteModel OperateInfo { get; init; }

            public required string UserAccountId { get; init; }

            public required string UserId { get; init; }

            public required string UserName { get; init; }
        }
    }
}
