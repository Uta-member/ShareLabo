using Microsoft.Extensions.Logging;
using Npgsql;

namespace ShareLabo.Infrastructure.PGSQL.Toolkit
{
    public sealed class ShareLaboPGSQLConnectionFactory
    {
        private readonly NpgsqlDataSource _dataSource;

        public ShareLaboPGSQLConnectionFactory(string connectionString, ILoggerFactory loggerFactory)
        {
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            dataSourceBuilder.UseLoggerFactory(loggerFactory);
            _dataSource = dataSourceBuilder.Build();
        }

        public NpgsqlConnection OpenConnection()
        {
            return _dataSource.OpenConnection();
        }
    }
}
