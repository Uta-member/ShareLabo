using CSStack.TADA;

namespace ShareLabo.Infrastructure.PGSQL.Toolkit
{
    public sealed class ShareLaboPGSQLTransactionService : ITransactionService<ShareLaboPGSQLTransaction>
    {
        private readonly ShareLaboPGSQLConnectionFactory _connectionFactory;

        public ShareLaboPGSQLTransactionService(ShareLaboPGSQLConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async ValueTask<ShareLaboPGSQLTransaction> BeginAsync()
        {
            var connection = _connectionFactory.OpenConnection();
            var transaction = await connection.BeginTransactionAsync();
            return new ShareLaboPGSQLTransaction { Transaction = transaction };
        }

        public ValueTask CommitAsync(ShareLaboPGSQLTransaction session)
        {
            session.Transaction.Commit();
            session.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask RollbackAsync(ShareLaboPGSQLTransaction session)
        {
            session.Transaction.Rollback();
            session.Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
