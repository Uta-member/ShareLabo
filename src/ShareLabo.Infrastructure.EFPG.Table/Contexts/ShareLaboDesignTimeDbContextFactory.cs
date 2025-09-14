using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ShareLabo.Infrastructure.EFPG.Table
{
    public sealed class ShareLaboDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShareLaboDbContext>
    {
        public ShareLaboDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ShareLaboDbContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            // var connectionString = configuration.GetConnectionString("TestConnection");

            optionsBuilder.UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            return new ShareLaboDbContext(optionsBuilder.Options);
        }
    }
}
