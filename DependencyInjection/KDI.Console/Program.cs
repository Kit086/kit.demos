using KDI;
using KDI.Console;

var kServices = new KServiceCollection();

kServices.AddSingleton<IGuidServiceSingleton, GuidServiceSingleton>();
kServices.AddTransient<IGuidServiceTransient, GuidServiceTransient>();

var kServiceProvider = kServices.BuildKServiceProvider();

var singleton1 = kServiceProvider.GetKService<IGuidServiceSingleton>();
var singleton2 = kServiceProvider.GetKService<IGuidServiceSingleton>();

var transient1 = kServiceProvider.GetKService<IGuidServiceTransient>();
var transient2 = kServiceProvider.GetKService<IGuidServiceTransient>();

Console.WriteLine($"singleton1: {singleton1.Guid}");
Console.WriteLine($"singleton2: {singleton2.Guid}");
Console.WriteLine($"transient1: {transient1.Guid}");
Console.WriteLine($"transient2: {transient2.Guid}");