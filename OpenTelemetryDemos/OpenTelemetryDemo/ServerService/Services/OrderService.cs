using Bogus;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace ServerService.Services;

public class OrderService : ServerService.OrderService.OrderServiceBase
{
    private readonly ILogger<OrderService> _logger;
    
    private static readonly string[] Fruits = { "apple", "banana", "orange", "strawberry", "kiwi" };
    
    public OrderService(ILogger<OrderService> logger)
    {
        _logger = logger;
    }
    
    public override Task<OrdersReply> GetOrders(OrdersRequest request, ServerCallContext context)
    {
        _logger.LogInformation("Get orders for customer {customerName}", request.CustomerName);
        
        var orderFaker = new Faker<Order>()
            .StrictMode(true)
            .RuleFor(o => o.Id, Guid.NewGuid().ToString)
            .RuleFor(o => o.Item, f => f.PickRandom(Fruits))
            .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
            .RuleFor(o => o.OrderDate, f => f.Date.Recent(10).ToUniversalTime().ToTimestamp())
            .RuleFor(o => o.CustomerName, request.CustomerName);
        
        var orders = orderFaker.Generate(10);
        
        var response = new OrdersReply();
        response.Orders.AddRange(orders);
        
        return Task.FromResult(response);
    }
}
// {
//     private readonly ILogger<OrderService> _logger;
//     
//     private static readonly string[] Fruits = { "apple", "banana", "orange", "strawberry", "kiwi" };
//     
//     public OrderService(ILogger<OrderService> logger)
//     {
//         _logger = logger;
//     }
//     
//     public IReadOnlyCollection<Order> GetOrders(string customerName)
//     {
//         _logger.LogInformation("Get orders for customer {customerName}", customerName);
//         
//         var orderFaker = new Faker<Order>()
//             .StrictMode(true)
//             .RuleFor(o => o.Id, Guid.NewGuid())
//             .RuleFor(o => o.Item, f => f.PickRandom(Fruits))
//             .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
//             .RuleFor(o => o.OrderDate, f => f.Date.Recent(10).ToUniversalTime())
//             .RuleFor(o => o.CustomerName, customerName);
//         
//         var orders = orderFaker.Generate(10);
//     
//         return orders;
//     }
// }

// public sealed class Order
// {
//     public Guid Id { get; set; }
//
//     public string Item { get; set; } = null!;
//
//     public int Quantity { get; set; }
//
//     public DateTime OrderDate { get; set; }
//
//     public string CustomerName { get; set; } = null!;
// }