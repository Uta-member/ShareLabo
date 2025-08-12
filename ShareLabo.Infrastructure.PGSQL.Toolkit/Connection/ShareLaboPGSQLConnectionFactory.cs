using Npgsql;

namespace ShareLabo.Infrastructure.PGSQL.Toolkit
{
    public sealed class ShareLaboPGSQLConnectionFactory
    {
        private string _connectionString;

        public ShareLaboPGSQLConnectionFactory(NpgsqlConnectionStringBuilder connectionStringBuilder)
        {
            _connectionString = connectionStringBuilder.ConnectionString;
        }

        public ShareLaboPGSQLConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection OpenConnection()
        {
            var connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
