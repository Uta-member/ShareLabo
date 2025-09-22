using Microsoft.EntityFrameworkCore;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class ShareLaboDbContext : DbContext
    {
        public ShareLaboDbContext(DbContextOptions<ShareLaboDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbAccount> Accounts { get; set; }

        public DbSet<DbFollow> Follows { get; set; }

        public DbSet<DbOAuthIntegration> OAuthIntegrations { get; set; }

        public DbSet<DbPost> Posts { get; set; }

        public DbSet<DbTimeLineFilter> TimeLineFilters { get; set; }

        public DbSet<DbTimeLine> TimeLines { get; set; }

        public DbSet<DbUser> Users { get; set; }
    }
}
