namespace ShareLabo.Application.Authentication.OAuthIntegration
{
    public sealed record OAuthInfo
    {
        public required string OAuthIdentifier { get; init; }

        public required OAuthType OAuthType { get; init; }
    }
}
