using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.Group;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.DomainService.Group;
using ShareLabo.Domain.DomainService.Post;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Presentation.AppBuilder.DomainCoreBuilder
{
    public static class ShareLaboDomainCoreBuilder
    {
        private static void AddAggregateServices<TUserSession, TGroupSession, TPostSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TGroupSession : IDisposable
            where TPostSession : IDisposable
        {
            services.AddTransient<UserAggregateService<TUserSession>>();
            services.AddTransient<GroupAggregateService<TGroupSession>>();
            services.AddTransient<PostAggregateService<TPostSession>>();
        }

        private static void AddDomainServices<TUserSession, TGroupSession, TPostSession, TTimeLineSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TGroupSession : IDisposable
            where TPostSession : IDisposable
            where TTimeLineSession : IDisposable
        {
            services.AddTransient<UserCreateDomainService<TUserSession, TTimeLineSession>>();
            services.AddTransient<UserUpdateDomainService<TUserSession>>();
            services.AddTransient<UserDeleteDomainService<TUserSession, TGroupSession, TTimeLineSession>>();

            services.AddTransient<GroupCreateDomainService<TGroupSession, TUserSession>>();
            services.AddTransient<GroupUpdateDomainService<TGroupSession, TUserSession>>();
            services.AddTransient<GroupDeleteDomainService<TGroupSession>>();

            services.AddTransient<PostCreateDomainService<TPostSession, TUserSession, TGroupSession>>();
            services.AddTransient<PostUpdateDomainService<TPostSession, TGroupSession>>();
            services.AddTransient<PostDeleteDomainService<TPostSession>>();
        }

        public static IServiceCollection AddShareLaboDomainCore<TUserSession, TGroupSession, TPostSession, TTimeLineSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TGroupSession : IDisposable
            where TPostSession : IDisposable
            where TTimeLineSession : IDisposable
        {
            services.AddAggregateServices<TUserSession, TGroupSession, TPostSession>();
            services.AddDomainServices<TUserSession, TGroupSession, TPostSession, TTimeLineSession>();
            return services;
        }
    }
}