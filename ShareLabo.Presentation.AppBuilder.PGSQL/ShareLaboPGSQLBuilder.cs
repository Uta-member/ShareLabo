using CSStack.TADA;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Infrastructure.PGSQL.Application.Authentication;
using ShareLabo.Infrastructure.PGSQL.QueryService.User;
using ShareLabo.Infrastructure.PGSQL.Repository.Group;
using ShareLabo.Infrastructure.PGSQL.Repository.Post;
using ShareLabo.Infrastructure.PGSQL.Repository.User;
using ShareLabo.Infrastructure.PGSQL.Toolkit;
using ShareLabo.Presentation.AppBuilder.Application;

namespace ShareLabo.Presentation.AppBuilder.PGSQL
{
    public static class ShareLaboPGSQLBuilder
    {
        private static void AddAuthentication(this IServiceCollection services)
        {
            services.AddTransient<IUserAccountRepository<ShareLaboPGSQLTransaction>, UserAccountPGSQLRepository>();
        }

        private static void AddPGSQL(
            this IServiceCollection services,
            string connectionString)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddTransient<ShareLaboPGSQLConnectionFactory>(
                provider => new ShareLaboPGSQLConnectionFactory(connectionString));
            services.AddTransient<ITransactionService<ShareLaboPGSQLTransaction>, ShareLaboPGSQLTransactionService>();
        }

        private static void AddQueryServices(this IServiceCollection services)
        {
            services.AddTransient<IUserSummariesSearchQueryService, UserSummariesSearchQueryService>();
            services.AddTransient<IUserDetailFindByAccountIdQueryService, UserDetailFindByAccountIdQueryService>();
            services.AddTransient<IUserDetailFindByUserIdQueryService, UserDetailFindByUserIdQueryService>();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository<ShareLaboPGSQLTransaction>, UserPGSQLRepository>();
            services.AddTransient<IGroupRepository<ShareLaboPGSQLTransaction>, GroupPGSQLRepository>();
            services.AddTransient<IPostRepository<ShareLaboPGSQLTransaction>, PostPGSQLRepository>();
        }

        public static IServiceCollection AddShareLaboPGSQL(this IServiceCollection services, BuildOption option)
        {
            services.AddShareLaboApplication<ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction, ShareLaboPGSQLTransaction>(
                );
            services.AddPGSQL(option.ShareLaboPGSQLConnectionString);
            services.AddRepositories();
            services.AddQueryServices();
            services.AddAuthentication();
            return services;
        }

        public sealed record BuildOption
        {
            public required string ShareLaboPGSQLConnectionString { get; init; }
        }
    }
}