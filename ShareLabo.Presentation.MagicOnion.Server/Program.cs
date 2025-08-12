using NLog;
using NLog.Web;
using ShareLabo.Presentation.AppBuilder.PGSQL;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    var shareLaboPGSQLConnectionString = builder.Configuration.GetConnectionString("ShareLaboPGSQL");
    if(string.IsNullOrEmpty(shareLaboPGSQLConnectionString))
    {
        throw new InvalidOperationException("ShareLaboPGSQL connection string is not configured.");
    }

    builder.Services
        .AddShareLaboPGSQL(
            new ShareLaboPGSQLBuilder.BuildOption()
            {
                ShareLaboPGSQLConnectionString = shareLaboPGSQLConnectionString,
            });

    builder.Services.AddGrpc();

    builder.Services
        .AddMagicOnion()
        // Swagger >>>
        .AddJsonTranscoding();
    builder.Services.AddMagicOnionJsonTranscodingSwagger();
    builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(type => type.FullName?.Replace("+", ".")));
    // Swagger <<<

    // gRPC-Web >>>
    builder.Services
        .AddCors(
            o => o.AddPolicy(
                "AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
                }));
    // gRPC-Web <<<

    var app = builder.Build();

    // gRPC-Web >>>
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });

    app.UseCors();
    // gRPC-Web <<<

    // Swagger >>>
    app.UseSwagger();
    if(app.Environment.IsDevelopment())
    {
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
        // Access to => https://localhost:7202/swagger/index.html
    }
    // Swagger <<<

    app.MapMagicOnionService()
        // gRPC-Web >>>
        .EnableGrpcWeb()
        .RequireCors("AllowAll");
    // gRPC-Web <<<

    app.MapGet(
        "/",
        () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

    app.Run();
}
catch(Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

