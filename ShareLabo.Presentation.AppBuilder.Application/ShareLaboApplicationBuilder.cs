using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Application.UseCase.CommandService.Implementation.Follow;
using ShareLabo.Application.UseCase.CommandService.Implementation.Post;
using ShareLabo.Application.UseCase.CommandService.Implementation.TimeLine;
using ShareLabo.Application.UseCase.CommandService.Implementation.User;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Presentation.AppBuilder.DomainCoreBuilder;

namespace ShareLabo.Presentation.AppBuilder.Application
{
    public static class ShareLaboApplicationBuilder
    {
        private static void AddAuthenticationServices<TAuthSession>(
            this IServiceCollection services)
            where TAuthSession : IDisposable
        {
            services.AddTransient<UserAccountCreateService<TAuthSession>>();
            services.AddTransient<UserAccountDeleteService<TAuthSession>>();
            services.AddTransient<UserAccountUpdateService<TAuthSession>>();
            services.AddTransient<UserAccountPasswordUpdateService<TAuthSession>>();
            services.AddTransient<UserLoginService<TAuthSession>>();
        }

        private static void AddCommandServices<TUserSession,
            TPostSession,
            TAuthSession,
            TTimeLineSession,
            TFollowSession>(this IServiceCollection services)
            where TUserSession : IDisposable
            where TPostSession : IDisposable
            where TAuthSession : IDisposable
            where TTimeLineSession : IDisposable
            where TFollowSession : IDisposable
        {
            services.AddTransient<ISelfAuthUserCreateCommandService, SelfAuthUserCreateCommandService<TUserSession, TAuthSession>>(
                );
            services.AddTransient<IUserDeleteCommandService, UserDeleteCommandService<TUserSession, TAuthSession, TTimeLineSession, TFollowSession>>(
                );
            services.AddTransient<IUserUpdateCommandService, UserUpdateCommandService<TUserSession, TAuthSession>>();
            services.AddTransient<ISelfAuthUserPasswordUpdateCommandService, SelfAuthUserPasswordUpdateCommandService<TAuthSession>>(
                );
            services.AddTransient<ISelfAuthUserLoginCommandService, SelfAuthUserLoginCommandService<TAuthSession>>();

            services.AddTransient<IPostCreateCommandService, PostCreateCommandService<TPostSession, TUserSession>>(
                );
            services.AddTransient<IPostUpdateCommandService, PostUpdateCommandService<TPostSession>>();
            services.AddTransient<IPostDeleteCommandService, PostDeleteCommandService<TPostSession>>();

            services.AddTransient<IFollowCreateCommandService, FollowCreateCommandService<TFollowSession, TUserSession>>(
                );
            services.AddTransient<IFollowDeleteCommandService, FollowDeleteCommandService<TFollowSession>>();

            services.AddTransient<ITimeLineCreateCommandService, TimeLineCreateCommandService<TTimeLineSession, TUserSession>>(
                );
            services.AddTransient<ITimeLineUpdateCommandService, TimeLineUpdateCommandService<TTimeLineSession, TUserSession>>(
                );
            services.AddTransient<ITimeLineDeleteCommandService, TimeLineDeleteCommandService<TTimeLineSession>>();
        }

        public static IServiceCollection AddShareLaboApplication<TUserSession,
            TPostSession,
            TAuthSession,
            TTimeLineSession,
            TFollowSession>(this IServiceCollection services)
            where TUserSession : IDisposable
            where TPostSession : IDisposable
            where TAuthSession : IDisposable
            where TTimeLineSession : IDisposable
            where TFollowSession : IDisposable
        {
            services.AddTransient<ITransactionManager, TransactionManager>();
            services.AddShareLaboDomainCore<TUserSession, TPostSession, TTimeLineSession, TFollowSession>(
                );
            services.AddAuthenticationServices<TAuthSession>();
            services.AddCommandServices<TUserSession, TPostSession, TAuthSession, TTimeLineSession, TFollowSession>(
                );
            return services;
        }
    }
}