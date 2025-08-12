using CSStack.TADA;
using ShareLabo.Application.Authentication;
using ShareLabo.Domain.ValueObject;

namespace ShareLabo.Application.UseCase.CommandService.User
{
    public interface ISelfAuthUserLoginCommandService
        : ICommandServiceWithRes<ISelfAuthUserLoginCommandService.Req, ISelfAuthUserLoginCommandService.Res>
    {
        public sealed record Req : ICommandServiceDTO
        {
            public required AccountPassword AccountPassword { get; init; }

            public required UserAccountId UserAccountId { get; init; }
        }

        public sealed record Res : ICommandServiceDTO
        {
            public required bool IsAuthorized { get; init; }

            public required LoginResultDetail LoginResultDetail { get; init; }
        }
    }
}
