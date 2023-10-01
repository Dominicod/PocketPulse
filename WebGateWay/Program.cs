using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
    builder.Services.AddSerilog(loggerConfig =>
    {
        loggerConfig
            .Enrich.FromLogContext()
            .WriteTo.Async(wt => wt.Console())
            .WriteTo.File("../logs/WebGateWay.txt", rollingInterval: RollingInterval.Day);
    });

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseSerilogRequestLogging();
    app.MapControllers();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapReverseProxy();
    });

    app.Run();
}
catch (Exception ex) { Log.Fatal("Error occured while starting Identity.API: Ex: {Ex}", ex.Message); }
finally { Log.CloseAndFlush(); }