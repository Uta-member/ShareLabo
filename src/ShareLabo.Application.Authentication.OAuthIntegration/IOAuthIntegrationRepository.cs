using CSStack.TADA;

namespace ShareLabo.Application.Authentication.OAuthIntegration
{
    public interface IOAuthIntegrationRepository<TSession>
        where TSession : IDisposable
    {
        ValueTask DeleteAsync(
            TSession session,
            OAuthIntegrationEntity entity,
            CancellationToken cancellationToken = default);

        ValueTask<Optional<OAuthIntegrationEntity>> FindByUserIdAsync(
            TSession session,
            string userId,
            CancellationToken cancellationToken = default);

        ValueTask SaveAsync(
            TSession session,
            OAuthIntegrationEntity entity,
            CancellationToken cancellationToken = default);
    }
}
