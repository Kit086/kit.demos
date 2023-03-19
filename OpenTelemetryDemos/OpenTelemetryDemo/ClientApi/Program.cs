using ClientApi;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Trace;

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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/order/{customerName}/orders", async ([FromRoute] string customerName, 
    IConfiguration configuration, ILogger<Program> logger) =>
{
    logger.LogInformation("Get orders for customer {customerName}", customerName);
    string serverServiceAddress = configuration.GetValue<string>("ServerServiceAddress") ?? string.Empty;
    logger.LogInformation("ServerServiceAddress: {serverServiceAddress}", serverServiceAddress);
    
    using var channel = GrpcChannel.ForAddress(serverServiceAddress, new GrpcChannelOptions
    {
        Credentials = ChannelCredentials.Insecure
    });

    var client = new OrderService.OrderServiceClient(channel);
    var reply = await client.GetOrdersAsync(
        new OrdersRequest { CustomerName = customerName });
    return reply is null ? Results.Problem() : Results.Ok(reply);
});

app.Run();