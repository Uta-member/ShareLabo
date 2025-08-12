using CSStack.TADA;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Application.Authentication;
using ShareLabo.Application.UseCase.CommandService.Post;
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

        private static void AddCommandServices<TUserSession, TGroupSession, TPostSession, TAuthSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TGroupSession : IDisposable
            where TPostSession : IDisposable
            where TAuthSession : IDisposable
        {
            services.AddTransient<ISelfAuthUserCreateCommandService, SelfAuthUserCreateCommandService<TUserSession, TAuthSession>>(
                );
            services.AddTransient<IUserDeleteCommandService, UserDeleteCommandService<TUserSession, TGroupSession, TAuthSession>>(
                );
            services.AddTransient<IUserUpdateCommandService, UserUpdateCommandService<TUserSession, TAuthSession>>();
            services.AddTransient<ISelfAuthUserPasswordUpdateCommandService, SelfAuthUserPasswordUpdateCommandService<TAuthSession>>(
                );
            services.AddTransient<ISelfAuthUserLoginCommandService, SelfAuthUserLoginCommandService<TAuthSession>>();

            services.AddTransient<IPostCreateCommandService, PostCreateCommandService<TPostSession, TUserSession, TGroupSession>>(
                );
        }

        public static IServiceCollection AddShareLaboApplication<TUserSession, TGroupSession, TPostSession, TAuthSession>(
            this IServiceCollection services)
            where TUserSession : IDisposable
            where TGroupSession : IDisposable
            where TPostSession : IDisposable
            where TAuthSession : IDisposable
        {
            services.AddTransient<ITransactionManager, TransactionManager>();
            services.AddShareLaboDomainCore<TUserSession, TGroupSession, TPostSession>();
            services.AddAuthenticationServices<TAuthSession>();
            services.AddCommandServices<TUserSession, TGroupSession, TPostSession, TAuthSession>();
            return services;
        }
    }
}