using Identity.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    string connectionString = configuration["ConnectionStrings:DefaultConnection"] ??
                              throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<IIdentityDBContext, IdentityDBContext>(options =>
        options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Identity")));
    // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //     .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAdB2C"));
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSerilog(loggerConfig =>
    {
        loggerConfig
            .Enrich.FromLogContext()
            .WriteTo.Async(wt => wt.Console())
            .WriteTo.File("../logs/IdentityAPI.txt", rollingInterval: RollingInterval.Day);
    });

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();

    // app.UseAuthentication();
    // app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex) { Log.Fatal("Error occured while starting Identity.API: Ex: {Ex}", ex.Message); }
finally { Log.CloseAndFlush(); }
