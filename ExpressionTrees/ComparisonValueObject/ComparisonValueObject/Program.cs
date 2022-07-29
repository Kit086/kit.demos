using ComparisonValueObject;
using Microsoft.EntityFrameworkCore;

await using AppDbContext dbContext = new AppDbContext();
await dbContext.SeedAsync(); // 设置种子数据

List<Person> shanghaiPersons = await dbContext.Persons
    .Where(p => p.Address.Country == "China"
                && p.Address.Province == "Shanghai"
                && p.Address.City == "Shanghai")
    .ToListAsync();

// List<Person> shanghaiPersons = await dbContext.Persons
//     .Where(p => p.Address == new Address
//     {
//         Country = "China",
//         Province = "Shanghai",
//         City = "Shanghai"
//     })
//     .ToListAsync();

// List<Person> shanghaiPersons = await dbContext.Persons
//     .Where(ValueObjectEqualHelper.CheckEqual<Person, Address>(p => p.Address,
//         new Address
//         {
//             Country = "China",
//             Province = "Shanghai",
//             City = "Shanghai"
//         }))
//     .ToListAsync();

foreach (Person shanghaiPerson in shanghaiPersons)
{
    Console.WriteLine(shanghaiPerson.ToString());
}