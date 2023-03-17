using ServerService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

// app.MapGet("/order/{customerName}/orders", 
//     ([FromRoute] string customerName, OrderService orderService) => 
//         Results.Ok(orderService.GetOrders(customerName)));

app.MapGrpcService<OrderService>();

app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
