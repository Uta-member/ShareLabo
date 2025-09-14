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
            services.AddTransient<IFollowAggregateService<TFollowSession>, FollowAggregateService<TFollowSession>>();
            services.AddTransient<IPostAggregateService<TPostSession>, PostAggregateService<TPostSession>>();
            services.AddTransient<ITimeLineAggregateService<TTimeLineSession>, TimeLineAggregateService<TTimeLineSession>>(
                );
            services.AddTransient<IUserAggregateService<TUserSession>, UserAggregateService<TUserSession>>();
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
            services.AddTransient<IFollowCreateDomainService<TFollowSession, TUserSession>, FollowCreateDomainService<TFollowSession, TUserSession>>(
                );
            services.AddTransient<IFollowDeleteDomainService<TFollowSession>, FollowDeleteDomainService<TFollowSession>>(
                );

            // Post
            services.AddTransient<IPostCreateDomainService<TPostSession, TUserSession>, PostCreateDomainService<TPostSession, TUserSession>>(
                );
            services.AddTransient<IPostUpdateDomainService<TPostSession>, PostUpdateDomainService<TPostSession>>();
            services.AddTransient<IPostDeleteDomainService<TPostSession>, PostDeleteDomainService<TPostSession>>();

            // TimeLine
            services.AddTransient<ITimeLineCreateDomainService<TTimeLineSession, TUserSession>, TimeLineCreateDomainService<TTimeLineSession, TUserSession>>(
                );
            services.AddTransient<ITimeLineUpdateDomainService<TTimeLineSession, TUserSession>, TimeLineUpdateDomainService<TTimeLineSession, TUserSession>>(
                );
            services.AddTransient<ITimeLineDeleteDomainService<TTimeLineSession>, TimeLineDeleteDomainService<TTimeLineSession>>(
                );

            // User
            services.AddTransient<IUserCreateDomainService<TUserSession>, UserCreateDomainService<TUserSession>>();
            services.AddTransient<IUserUpdateDomainService<TUserSession>, UserUpdateDomainService<TUserSession>>();
            services.AddTransient<IUserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>, UserDeleteDomainService<TUserSession, TTimeLineSession, TFollowSession>>(
                );
            services.AddTransient<IUserStatusUpdateDomainService<TUserSession>, UserStatusUpdateDomainService<TUserSession>>(
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