using DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

var services = new ServiceCollection();

// services.AddSingleton<IService, Service>();

var serviceDescriptor = new ServiceDescriptor(
    typeof(IService), 
    typeof(Service), 
    ServiceLifetime.Singleton);

services.Add(serviceDescriptor);

var serviceProvider = services.BuildServiceProvider();

var myService = serviceProvider.GetRequiredService<IService>();

myService.DoIt();

#region scoped

// var services = new ServiceCollection();
//
// services.AddSingleton<GuidServiceSingleton>();
// services.AddScoped<GuidServiceScoped>();
//
// var sp = services.BuildServiceProvider();
//
// var singletonGuidService1 = sp.GetRequiredService<GuidServiceSingleton>();
// Console.WriteLine($"Singleton 1: {singletonGuidService1.Id}");
//
// var singletonGuidService2 = sp.GetRequiredService<GuidServiceSingleton>();
// Console.WriteLine($"Singleton 2: {singletonGuidService2.Id}");
//
// var scopedGuidService1 = sp.GetRequiredService<GuidServiceScoped>();
// Console.WriteLine($"Scoped 1: {scopedGuidService1.Id}");
//
// var scopedGuidService2 = sp.GetRequiredService<GuidServiceScoped>();
// Console.WriteLine($"Scoped 2: {scopedGuidService2.Id}");

// ================================================================================

// var services = new ServiceCollection();
//
// services.AddSingleton<GuidServiceSingleton>();
// services.AddScoped<GuidServiceScoped>();
//
// var sp = services.BuildServiceProvider();
//
// using (var serviceScope = sp.CreateScope())
// {
//     var singletonGuidService1 = serviceScope.ServiceProvider.GetRequiredService<GuidServiceSingleton>();
//     Console.WriteLine($"Singleton 1: {singletonGuidService1.Id}");
// }
//
// using (var serviceScope = sp.CreateScope())
// {
//     var singletonGuidService2 = serviceScope.ServiceProvider.GetRequiredService<GuidServiceSingleton>();
//     Console.WriteLine($"Singleton 2: {singletonGuidService2.Id}");
// }
//
// using (var serviceScope = sp.CreateScope())
// {
//     var scopedGuidService1 = serviceScope.ServiceProvider.GetRequiredService<GuidServiceScoped>();
//     Console.WriteLine($"Scoped 1: {scopedGuidService1.Id}");
// }
//
// using (var serviceScope = sp.CreateScope())
// {
//     var scopedGuidService2 = serviceScope.ServiceProvider.GetRequiredService<GuidServiceScoped>();
//     Console.WriteLine($"Scoped 2: {scopedGuidService2.Id}");
// }

#endregion


