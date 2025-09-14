namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class SharedServiceProviderFixture
    {
        public readonly IServiceProvider ServiceProvider = ServiceProviderFactory.Create();
    }
}
