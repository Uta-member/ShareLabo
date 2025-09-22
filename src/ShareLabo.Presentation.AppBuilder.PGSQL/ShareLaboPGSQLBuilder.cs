using CSStack.TADA;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.QueryService.Follow;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Application.UseCase.QueryService.TimeLine;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Infrastructure.PGSQL.Application.Authentication;
using ShareLabo.Infrastructure.PGSQL.QueryService.Follow;
using ShareLabo.Infrastructure.PGSQL.QueryService.Post;
using ShareLabo.Infrastructure.PGSQL.QueryService.TimeLine;
using ShareLabo.Infrastructure.PGSQL.QueryService.User;
using ShareLabo.Infrastructure.PGSQL.Repository.Follow;
using ShareLabo.Infrastructure.PGSQL.Repository.Post;
using ShareLabo.Infrastructure.PGSQL.Repository.TimeLine;
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
            services.AddTransient<IOAuthIntegrationRepository<ShareLaboPGSQLTransaction>, OAuthIntegrationRepository>();
        }

        private static void AddPGSQL(
            this IServiceCollection services,
            string connectionString)
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddTransient<ShareLaboPGSQLConnectionFactory>(
                provider =>
                {
                    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                    return new ShareLaboPGSQLConnectionFactory(connectionString, loggerFactory);
                });
            services.AddTransient<ITransactionService<ShareLaboPGSQLTransaction>, ShareLaboPGSQLTransactionService>();
        }

        private static void AddQueryServices(this IServiceCollection services)
        {
            // Follow
            services.AddTransient<IUserFollowersGetQueryService, UserFollowersGetQueryService>();
            services.AddTransient<IUserFollowsGetQueryService, UserFollowsGetQueryService>();

            // Post
            services.AddTransient<IGeneralPostsGetQueryService, GeneralPostsGetQueryService>();
            services.AddTransient<IFollowedPostsGetQueryService, FollowedPostsGetQueryService>();
            services.AddTransient<IMyPostsGetQueryService, MyPostsGetQueryService>();
            services.AddTransient<IPostDetailFindByIdQueryService, PostDetailFindByIdQueryService>();
            services.AddTransient<ITimeLinePostsGetQueryService, TimeLinePostsGetQueryService>();

            // TimeLine
            services.AddTransient<ITimeLineFindByIdQueryService, TimeLineFindByIdQueryService>();
            services.AddTransient<IUserTimeLinesGetQueryService, UserTimeLinesGetQueryService>();

            // User
            services.AddTransient<IUserSummariesSearchQueryService, UserSummariesSearchQueryService>();
            services.AddTransient<IUserDetailFindByAccountIdQueryService, UserDetailFindByAccountIdQueryService>();
            services.AddTransient<IUserDetailFindByUserIdQueryService, UserDetailFindByUserIdQueryService>();
            services.AddTransient<IUserDetailFindByOAuthIdentifierQueryService, UserDetailFindByOAuthIdentifierQueryService>(
                );
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFollowRepository<ShareLaboPGSQLTransaction>, FollowRepository>();
            services.AddTransient<IPostRepository<ShareLaboPGSQLTransaction>, PostRepository>();
            services.AddTransient<ITimeLineRepository<ShareLaboPGSQLTransaction>, TimeLineRepository>();
            services.AddTransient<IUserRepository<ShareLaboPGSQLTransaction>, UserRepository>();
        }

        public static IServiceCollection AddShareLaboPGSQL(this IServiceCollection services, BuildOption option)
        {
            services.AddShareLaboApplication<ShareLaboPGSQLTransaction,
                ShareLaboPGSQLTransaction,
                ShareLaboPGSQLTransaction,
                ShareLaboPGSQLTransaction,
                ShareLaboPGSQLTransaction>(
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