using OpenTelemetry.Trace;
using ServerService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenTelemetry()
    .WithTracing(tracerProviderBuilder => tracerProviderBuilder
        .AddAspNetCoreInstrumentation()
        .AddGrpcClientInstrumentation()
        .AddConsoleExporter()
        .AddJaegerExporter(opt =>
        {
            opt.AgentHost = builder.Configuration.GetValue<string>("Jaeger:AgentHost");
            opt.AgentPort = builder.Configuration.GetValue<int>("Jaeger:AgentPort");
        }));

builder.Services.AddGrpc();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapGrpcService<OrderService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
