using System.Data;

namespace ShareLabo.Infrastructure.PGSQL.Toolkit
{
    public sealed class ShareLaboPGSQLTransaction : IDisposable
    {
        public void Dispose()
        {
            Transaction?.Dispose();
        }

        public required IDbTransaction Transaction { get; set; }
    }
}
