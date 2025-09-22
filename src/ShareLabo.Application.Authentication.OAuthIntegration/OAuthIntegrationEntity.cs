using System.Collections.Immutable;

namespace ShareLabo.Application.Authentication.OAuthIntegration
{
    public sealed class OAuthIntegrationEntity
    {
        public OAuthIntegrationEntity(string userId, ImmutableList<OAuthInfo> oAuthInfos)
        {
            UserId = userId;
            OAuthInfos = oAuthInfos;
        }

        public ImmutableList<OAuthInfo> OAuthInfos { get; }

        public string UserId { get; }
    }
}
