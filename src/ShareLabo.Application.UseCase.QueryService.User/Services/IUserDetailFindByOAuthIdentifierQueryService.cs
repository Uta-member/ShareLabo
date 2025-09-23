using CSStack.TADA;
using ShareLabo.Application.Authentication.OAuthIntegration;

namespace ShareLabo.Application.UseCase.QueryService.User
{
    public interface IUserDetailFindByOAuthIdentifierQueryService
        : IQueryService<IUserDetailFindByOAuthIdentifierQueryService.Req, IUserDetailFindByOAuthIdentifierQueryService.Res>
    {
        public sealed record Req : IQueryServiceDTO
        {
            public required string OAuthIdentifier { get; init; }

            public required OAuthType OAuthType { get; init; }
        }

        public sealed record Res : IQueryServiceDTO
        {
            public required Optional<UserDetailReadModel> UserOptional { get; init; }
        }
    }
}
