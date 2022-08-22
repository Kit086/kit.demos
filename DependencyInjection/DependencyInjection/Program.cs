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

