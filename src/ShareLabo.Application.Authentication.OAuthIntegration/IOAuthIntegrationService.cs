using CSStack.TADA;
using System.Collections.Immutable;

namespace ShareLabo.Application.Authentication.OAuthIntegration
{
    public interface IOAuthIntegrationService<TSession>
        where TSession : IDisposable
    {
        ValueTask AddAsync(AddReq req, CancellationToken cancellationToken = default);

        ValueTask CreateAsync(CreateReq req, CancellationToken cancellationToken = default);

        ValueTask DeleteAsync(DeleteReq req, CancellationToken cancellationToken = default);

        ValueTask<Optional<OAuthIntegrationEntity>> FindEntityByUserIdAsync(
            TSession session,
            string userId,
            CancellationToken cancellationToken = default);

        ValueTask RemoveAsync(RemoveReq req, CancellationToken cancellationToken = default);

        ValueTask UpdateAsync(UpdateReq req, CancellationToken cancellationToken = default);

        public sealed record CreateReq
        {
            public required ImmutableList<OAuthInfo> OAuthInfos { get; init; }

            public required TSession Session { get; init; }

            public required string UserId { get; init; }
        }

        public sealed record AddReq
        {
            public required OAuthInfo OAuthInfo { get; init; }

            public required TSession Session { get; init; }

            public required string UserId { get; init; }
        }

        public sealed record RemoveReq
        {
            public required OAuthType OAuthType { get; init; }

            public required TSession Session { get; init; }

            public required string UserId { get; init; }
        }

        public sealed record UpdateReq
        {
            public required OAuthInfo OAuthInfo { get; init; }

            public required TSession Session { get; init; }

            public required string UserId { get; init; }
        }

        public sealed record DeleteReq
        {
            public required TSession Session { get; init; }

            public required string UserId { get; init; }
        }
    }
}
