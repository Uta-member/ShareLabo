using CSStack.TADA;
using ShareLabo.Application.Authentication;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserLoginCommandService
        : ICommandServiceWithRes<ISelfAuthUserLoginCommandService.Req, ISelfAuthUserLoginCommandService.Res>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required string AccountPassword { get; init; }

            public required string UserAccountId { get; init; }
        }

        public sealed record Res : ICommandServiceDTO
        {
            public required bool IsAuthorized { get; init; }

            public required LoginResultDetail LoginResultDetail { get; init; }
        }
    }
}
