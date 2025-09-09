using CSStack.TADA.MagicOnionHelper.Client;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.Extensions.DependencyInjection;
using ShareLabo.Application.UseCase.CommandService.Follow;
using ShareLabo.Application.UseCase.CommandService.Post;
using ShareLabo.Application.UseCase.CommandService.TimeLine;
using ShareLabo.Application.UseCase.CommandService.User;
using ShareLabo.Application.UseCase.QueryService.Post;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.MagicOnion.Client;

namespace ShareLabo.Presentation.AppBuilder.MagicOnion.Client
{
    public static class ShareLaboMagicOnionClientBuilder
    {
        private static void AddCommandServices(this IServiceCollection services)
        {
            services.AddTransient<ISelfAuthUserCreateCommandService, SelfAuthUserCreateCommandService>();
            services.AddTransient<IUserDeleteCommandService, UserDeleteCommandService>();
            services.AddTransient<IUserUpdateCommandService, UserUpdateCommandService>();
            services.AddTransient<ISelfAuthUserPasswordUpdateCommandService, SelfAuthUserPasswordUpdateCommandService>(
                );
            services.AddTransient<ISelfAuthUserLoginCommandService, SelfAuthUserLoginCommandService>();

            services.AddTransient<IPostCreateCommandService, PostCreateCommandService>();
            services.AddTransient<IPostUpdateCommandService, PostUpdateCommandService>();
            services.AddTransient<IPostDeleteCommandService, PostDeleteCommandService>();

            services.AddTransient<IFollowCreateCommandService, FollowCreateCommandService>();
            services.AddTransient<IFollowDeleteCommandService, FollowDeleteCommandService>();

            services.AddTransient<ITimeLineCreateCommandService, TimeLineCreateCommandService>();
            services.AddTransient<ITimeLineUpdateCommandService, TimeLineUpdateCommandService>();
            services.AddTransient<ITimeLineDeleteCommandService, TimeLineDeleteCommandService>();
        }

        private static void AddGrpcChannel(this IServiceCollection services, string hostUrl, bool isGrpcWeb)
        {
            if(isGrpcWeb)
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));

                services.AddSingleton<GrpcChannel>(
                    GrpcChannel.ForAddress(
                        hostUrl,
                        new GrpcChannelOptions { HttpClient = httpClient }));
            }
            else
            {
                services.AddSingleton<GrpcChannel>(GrpcChannel.ForAddress(hostUrl));
            }

            services.AddTransient<IMOClientChannelFactory, MOClientChannelFactory>();
        }

        private static void AddQueryServices(this IServiceCollection services)
        {
            services.AddTransient<IUserSummariesSearchQueryService, UserSummariesSearchQueryService>();
            services.AddTransient<IUserDetailFindByAccountIdQueryService, UserDetailFindByAccountIdQueryService>(
                );
            services.AddTransient<IUserDetailFindByUserIdQueryService, UserDetailFindByUserIdQueryService>();

            services.AddTransient<IGeneralPostsGetQueryService, GeneralPostsGetQueryService>();
            services.AddTransient<IPostDetailFindByIdQueryService, PostDetailFindByIdQueryService>();
            services.AddTransient<IFollowedPostsGetQueryService, FollowedPostsGetQueryService>();
            services.AddTransient<IMyPostsGetQueryService, MyPostsGetQueryService>();
            services.AddTransient<ITimeLinePostsGetQueryService, TimeLinePostsGetQueryService>();
        }

        public static IServiceCollection AddShareLaboMagicOnionClient(
            this IServiceCollection services,
            BuildOption option)
        {
            services.AddGrpcChannel(option.HostUrl, option.IsGrpcWeb);
            services.AddCommandServices();
            services.AddQueryServices();
            return services;
        }

        public sealed record BuildOption
        {
            public required string HostUrl { get; init; }

            public required bool IsGrpcWeb { get; init; }
        }
    }
}