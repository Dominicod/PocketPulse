using Financial.API.Data;
using Financial.API.Repositories;
using Financial.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Serilog;
using Utilities.Services;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;

    var connectionString = configuration["ConnectionStrings:DefaultConnection"] ??
                           throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<IFinancialDBContext, FinancialDBContext>(options =>
        options.UseSqlServer(connectionString, x => x.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "Financial"))
    );
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
            .WriteTo.File("../logs/FinancialAPI.txt", rollingInterval: RollingInterval.Day);
    });
    builder.Services.AddScoped<IBillService, BillService>();
    builder.Services.AddScoped<IBillRepository, BillRepository>();
    builder.Services.AddScoped<IResponseHandlerService, ResponseHandlerService>();

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
catch (Exception ex) { Log.Fatal("Error occured while starting Financial.API: Ex: {Ex}", ex.Message); }
finally { Log.CloseAndFlush(); }