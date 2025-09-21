using CSStack.PrimeBlazor.Bootstrap;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Components.Authorization;
using ShareLabo.Presentation.AppBuilder.MagicOnion.Client;
using ShareLabo.Presentation.Blazor.Auth;
using ShareLabo.Presentation.Blazor.Client;
using ShareLabo.Presentation.Blazor.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingServerAuthenticationStateProvider>();

builder.Services.AddAuthorization();
builder.Services
    .AddAuthentication(
        options =>
        {
            // ������Cookie�F�؂��f�t�H���g�ɐݒ�
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // ���F�؎��̃`�������W�X�L�[���Ƃ���Google��ݒ�
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
    .AddCookie(
        options =>
        {
            options.LoginPath = ShareLaboPagePath.Helper.Login();
            options.LogoutPath = ShareLaboPagePath.Helper.Logout();
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true; // �L�������̉�����L���ɂ��邩
            options.Cookie.HttpOnly = true; // JavaScript����̃A�N�Z�X���֎~
            options.Cookie.IsEssential = true; // GDPR���ӂȂ���Cookie������
        })
    .AddGoogle(
        options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            options.CallbackPath = "/signin-google";
        });

builder.Services.AddPrimeBlazorBootstrap();

var magicOnionHost = builder.Configuration.GetConnectionString("MagicOnionHost") ??
    throw new InvalidOperationException("MagicOnionHost connection string is not configured.");

builder.Services
    .AddShareLaboMagicOnionClient(
        new ShareLaboMagicOnionClientBuilder.BuildOption()
        {
            HostUrl = magicOnionHost,
            IsGrpcWeb = false,
        });

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ShareLabo.Presentation.Blazor.Client._Imports).Assembly);

app.MapGet(
    "/Account/Logout",
    async (HttpContext httpContext, string? returnUrl) =>
    {
        await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return TypedResults.Redirect($"~/{returnUrl}");
    });

app.Run();
