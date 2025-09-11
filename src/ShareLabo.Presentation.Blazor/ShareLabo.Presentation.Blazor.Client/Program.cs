using CSStack.PrimeBlazor.Bootstrap;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShareLabo.Presentation.AppBuilder.MagicOnion.Client;
using ShareLabo.Presentation.Blazor.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var httpClient = new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
};
using var stream = await httpClient.GetStreamAsync("appsettings.json");
builder.Configuration.AddJsonStream(stream);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddPrimeBlazorBootstrap();

var magicOnionHost = builder.Configuration.GetConnectionString("MagicOnionHost") ??
    throw new InvalidOperationException("MagicOnionHost connection string is not configured.");

builder.Services
    .AddShareLaboMagicOnionClient(
        new ShareLaboMagicOnionClientBuilder.BuildOption()
        {
            HostUrl = magicOnionHost,
            IsGrpcWeb = true,
        });

await builder.Build().RunAsync();
