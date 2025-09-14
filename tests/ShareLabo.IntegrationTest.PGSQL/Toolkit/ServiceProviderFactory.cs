using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Presentation.AppBuilder.PGSQL;

namespace ShareLabo.IntegrationTest.PGSQL
{
    public sealed class ServiceProviderFactory
    {
        private static readonly object _lock = new object();
        private static IServiceProvider _serviceProvider = default!;

        public static IServiceProvider Create()
        {
            // ロックを使用して、複数のスレッドから同時に呼ばれても安全にインスタンスを生成
            if(_serviceProvider == null)
            {
                lock(_lock)
                {
                    if(_serviceProvider == null)
                    {
                        var configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .Build();

                        var services = new ServiceCollection();

                        services.AddSingleton<IConfiguration>(configuration);
                        services.AddLogging();

                        var connectionString = configuration.GetConnectionString("TestConnection");

                        if(string.IsNullOrWhiteSpace(connectionString))
                        {
                            throw new Exception(string.Empty);
                        }

                        services.AddShareLaboPGSQL(
                            new ShareLaboPGSQLBuilder.BuildOption()
                            {
                                ShareLaboPGSQLConnectionString = connectionString,
                            });

                        _serviceProvider = services.BuildServiceProvider();
                    }
                }
            }
            return _serviceProvider;
        }
    }
}
