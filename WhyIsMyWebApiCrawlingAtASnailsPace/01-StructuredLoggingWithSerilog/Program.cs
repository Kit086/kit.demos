using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

// 配置一下全局静态 Log 的 Logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        formatter: new CompactJsonFormatter(),
        Path.Combine(".", "logs","log-program-.txt"), 
        rollingInterval: RollingInterval.Day, 
        rollOnFileSizeLimit: true)
    .CreateBootstrapLogger();

try
{
    // 随便记一条日志
    Log.Logger.Information("Hello ASP.NET Core!");
    
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Host.UseSerilog((context, services, config) =>
        config.ReadFrom.Configuration(context.Configuration));
            // .ReadFrom.Services(services)
            // .Enrich.FromLogContext());

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}

return 0;