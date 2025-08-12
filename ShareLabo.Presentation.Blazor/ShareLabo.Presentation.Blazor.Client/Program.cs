using CSStack.PrimeBlazor.Bootstrap;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShareLabo.Presentation.AppBuilder.MagicOnion.Client;
using ShareLabo.Presentation.Blazor.Client;
using ShareLabo.Presentation.Blazor.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddPrimeBlazorBootstrap();

builder.Services
    .AddShareLaboMagicOnionClient(
        new ShareLaboMagicOnionClientBuilder.BuildOption()
        {
            HostUrl = "https://localhost:7202",
            IsGrpcWeb = true,
        });

builder.Services.AddScoped<LoginedUserStorageService>();

await builder.Build().RunAsync();
