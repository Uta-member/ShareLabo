using CSStack.PrimeBlazor.Bootstrap;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using ShareLabo.Application.Authentication.OAuthIntegration;
using ShareLabo.Application.UseCase.QueryService.User;
using ShareLabo.Presentation.AppBuilder.MagicOnion.Client;
using ShareLabo.Presentation.Blazor.Client;
using ShareLabo.Presentation.Blazor.Client.Services;
using ShareLabo.Presentation.Blazor.Components;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthorization();

builder.Services.AddHttpClient("Default", client => client.BaseAddress = new Uri("https://localhost:7181"));
builder.Services
    .AddScoped(
        sp =>
        {
            var factory = sp.GetRequiredService<IHttpClientFactory>();
            return factory.CreateClient("Default");
        });

builder.Services
    .AddAuthentication(
        options =>
        {
            // 既存のCookie認証をデフォルトに設定
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            // 未認証時のチャレンジスキームとしてGoogleを設定
            options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        })
    .AddCookie(
        options =>
        {
            options.LogoutPath = ShareLaboPagePath.Helper.Logout();
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true; // 有効期限の延長を有効にするか
            options.Cookie.HttpOnly = true; // JavaScriptからのアクセスを禁止
            options.Cookie.IsEssential = true; // GDPR同意なしでCookieを許可
        })
    .AddGoogle(
        options =>
        {
            options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
            options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
            options.CallbackPath = "/signin-google";
            options.Events.OnTicketReceived = async context =>
            {
                var email = context.Principal?.FindFirst(ClaimTypes.Email)?.Value;
                var identifier = context.Principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if(string.IsNullOrWhiteSpace(identifier))
                {
                    return;
                }

                var userFindService = context.HttpContext.RequestServices
                    .GetRequiredService<IUserDetailFindByOAuthIdentifierQueryService>();
                var userFindRes = await userFindService.ExecuteAsync(
                    new IUserDetailFindByOAuthIdentifierQueryService.Req()
                        {
                            OAuthIdentifier = identifier,
                            OAuthType = OAuthType.Google,
                        });

                context.Principal?.Identities.First()
                .AddClaim(
                    new Claim(ShareLaboClaimTypes.OAuthType, ((int)OAuthType.Google).ToString()));

                if(userFindRes.UserOptional.TryGetValue(out var userDetail))
                {
                    context.Principal?.Identities
                        .First()
                    .AddClaim(new Claim(ShareLaboClaimTypes.IsProfileRegistered, "true"));
                }
                else
                {
                    context.Principal?.Identities
                        .First()
                    .AddClaim(new Claim(ShareLaboClaimTypes.IsProfileRegistered, "false"));
                }

                return;
            };
        });

builder.Services.AddScoped<UserProfileService>();

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

app.MapGet(
    "/Account/UserInfo",
    (HttpContext ctx) =>
    {
        if(ctx.User.Identity?.IsAuthenticated == true)
        {
            return Results.Json(
                new
                {
                    IsAuthenticated = true,
                    Name = ctx.User.Identity.Name,
                    Claims = ctx.User.Claims.Select(c => new { c.Type, c.Value })
                });
        }
        return Results.Json(new { IsAuthenticated = false });
    });


app.MapPost(
    "/Account/RefreshSignIn",
    async (HttpContext context) =>
    {
        if(context.User.Identity?.IsAuthenticated != true)
        {
            return Results.Unauthorized();
        }

        var identity = (ClaimsIdentity)context.User.Identity;
        var claims = identity.Claims.ToList();

        // 既存のクレームに加えて、IsProfileRegistered を true に更新
        claims.RemoveAll(c => c.Type == ShareLaboClaimTypes.IsProfileRegistered);
        claims.Add(new Claim(ShareLaboClaimTypes.IsProfileRegistered, "true"));

        var newIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var newPrincipal = new ClaimsPrincipal(newIdentity);

        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, newPrincipal);

        return Results.Ok();
    });


app.Run();
