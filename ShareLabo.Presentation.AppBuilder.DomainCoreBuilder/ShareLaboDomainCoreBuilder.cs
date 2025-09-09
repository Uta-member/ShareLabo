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
            services.AddTransient<UserAggregateService<TUserSession>>();
            services.AddTransient<PostAggregateService<TPostSession>>();
            services.AddTransient<FollowAggregateService<TFollowSession>>();
            services.AddTransient<TimeLineAggregateService<TTimeLineSession>>();
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
            services.AddTransient<UserCreateDomainService<TUserSession>>();
            services.AddTransient<UserUpdateDomainService<TUserSession>>();
            services.AddTransient<UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>>(
                );

            services.AddTransient<PostCreateDomainService<TPostSession, TUserSession>>();
            services.AddTransient<PostUpdateDomainService<TPostSession>>();
            services.AddTransient<PostDeleteDomainService<TPostSession>>();

            services.AddTransient<FollowCreateDomainService<TFollowSession, TUserSession>>();
            services.AddTransient<FollowDeleteDomainService<TFollowSession>>();

            services.AddTransient<TimeLineCreateDomainService<TTimeLineSession, TUserSession>>();
            services.AddTransient<TimeLineUpdateDomainService<TTimeLineSession, TUserSession>>();
            services.AddTransient<TimeLineDeleteDomainService<TTimeLineSession>>();
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