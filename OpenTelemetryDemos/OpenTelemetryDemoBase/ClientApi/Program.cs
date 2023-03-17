using ClientApi;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

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
        new OrdersRequest { CustomerName = "KitLau" });
    return reply is null ? Results.Problem() : Results.Ok(reply);
});

// app.MapGet("/order/{customerName}/orders", async ([FromRoute] string customerName, 
//     IConfiguration configuration, ILogger<Program> logger) =>
// {
//     logger.LogInformation("Get orders for customer {customerName}", customerName);
//     string serverServiceAddress = configuration.GetValue<string>("ServerServiceAddress") ?? string.Empty;
//     
//     logger.LogInformation("ServerServiceAddress: {serverServiceAddress}", serverServiceAddress);
//
//     using var httpClient = new HttpClient();
//
//     HttpResponseMessage response = await httpClient.GetAsync($"{serverServiceAddress}/order/{customerName}/orders");
//
//     if (!response.IsSuccessStatusCode)
//     {
//         return Results.Problem();
//     }
//
//     string orders = await response.Content.ReadAsStringAsync();
//     
//     return Results.Ok(orders);
// });

app.Run();