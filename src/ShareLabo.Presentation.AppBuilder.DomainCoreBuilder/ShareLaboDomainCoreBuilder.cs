using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Domain.Aggregate.Follow;
using ShareLabo.Domain.Aggregate.Post;
using ShareLabo.Domain.Aggregate.TimeLine;
using ShareLabo.Domain.Aggregate.User;
using ShareLabo.Domain.DomainService.Follow;
using ShareLabo.Domain.DomainService.Post;
using ShareLabo.Domain.DomainService.TimeLine;
using ShareLabo.Domain.DomainService.User;

namespace ShareLabo.Presentation.AppBuilder.DomainCoreBuilder
{
    public static class ShareLaboDomainCoreBuilder
    {
        private static void AddAggregateServices<TUserSession, TPostSession, TFollowSession, TTimeLineSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TPostSession : IDisposable
            where TFollowSession : IDisposable
            where TTimeLineSession : IDisposable
        {
            services.AddTransient<FollowAggregateService<TFollowSession>>();
            services.AddTransient<PostAggregateService<TPostSession>>();
            services.AddTransient<TimeLineAggregateService<TTimeLineSession>>();
            services.AddTransient<UserAggregateService<TUserSession>>();
        }

        private static void AddDomainServices<TUserSession,
            TPostSession,
            TTimeLineSession,
            TFollowSession>(this IServiceCollection services)
            where TUserSession : IDisposable
            where TPostSession : IDisposable
            where TTimeLineSession : IDisposable
            where TFollowSession : IDisposable
        {
            // Follow
            services.AddTransient<FollowCreateDomainService<TFollowSession, TUserSession>>();
            services.AddTransient<FollowDeleteDomainService<TFollowSession>>();

            // Post
            services.AddTransient<PostCreateDomainService<TPostSession, TUserSession>>();
            services.AddTransient<PostUpdateDomainService<TPostSession>>();
            services.AddTransient<PostDeleteDomainService<TPostSession>>();

            // TimeLine
            services.AddTransient<TimeLineCreateDomainService<TTimeLineSession, TUserSession>>();
            services.AddTransient<TimeLineUpdateDomainService<TTimeLineSession, TUserSession>>();
            services.AddTransient<TimeLineDeleteDomainService<TTimeLineSession>>();

            // User
            services.AddTransient<UserCreateDomainService<TUserSession>>();
            services.AddTransient<UserUpdateDomainService<TUserSession>>();
            services.AddTransient<UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>>(
                );
        }

        public static IServiceCollection AddShareLaboDomainCore<TUserSession,
            TPostSession,
            TFollowSession,
            TTimeLineSession>(this IServiceCollection services)
            where TUserSession : IDisposable
            where TPostSession : IDisposable
            where TFollowSession : IDisposable
            where TTimeLineSession : IDisposable
        {
            services.AddAggregateServices<TUserSession, TPostSession, TFollowSession, TTimeLineSession>();
            services.AddDomainServices<TUserSession, TPostSession, TTimeLineSession, TFollowSession>();
            return services;
        }
    }
}