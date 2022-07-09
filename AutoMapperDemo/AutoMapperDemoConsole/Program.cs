// See https://aka.ms/new-console-template for more information

using System.Reflection;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using AutoMapperDemoConsole;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddAutoMapper(Assembly.GetExecutingAssembly());

IServiceProvider provider = services.BuildServiceProvider();
using IServiceScope scope = provider.CreateScope();
IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

#region without EF Core

// Person p = new()
// {
//     FirstName = "bei",
//     LastName = "liu",
//     Age = 63,
//     Address = new Address
//     {
//         Country = "han",
//         Province = "zhuojun",
//         City = "zhuoxian"
//     },
//     CreatedUtc = DateTime.UtcNow
// };
//
// PersonDto pDto = mapper.Map<PersonDto>(p);
// pDto.Display();
//
// await Task.Delay(1000);
//
// p.Accounts.Add(new Account
// {
//     Username = "liuxuande",
//     Password = "123456",
//     EmailAddress = "liuxuande@han.com"
// });
// p.Accounts.Add(new Account
// {
//     Username = "liupingyuan",
//     Password = "123456"
// });
//
// p.ModifiedUtc = DateTime.UtcNow;
//
// PersonDto modifiedPDto = mapper.Map<PersonDto>(p);
// modifiedPDto.Display();

#endregion

#region with EF Core

Console.WriteLine("==========1==========");
var dbContext = new AutoMapperDbContext();
Person? p = await dbContext.Persons.Where(p => p.FirstName == "Bei").FirstOrDefaultAsync();

Console.WriteLine("==========2==========");
var dbContext2 = new AutoMapperDbContext();
PersonDto? pDto = await dbContext2.Persons.ProjectTo<PersonDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync();

// PersonDto? pDto = await dbContext2.Persons.ProjectTo<PersonDto>(mapper.ConfigurationProvider, new { name = "Xuande Liu" }).FirstOrDefaultAsync();

Console.WriteLine(p?.LastName);
pDto?.Display();

#endregion

