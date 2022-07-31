using SetQueryFilterForAllEntitiesByExpression;
using Microsoft.EntityFrameworkCore;

await using AppDbContext dbContext = new AppDbContext();
await dbContext.SeedAsync(); // 设置种子数据

List<Person> persons = await dbContext.Persons.ToListAsync();
List<Point> points = await dbContext.Points.ToListAsync();

#region ignore query filter

// List<Person> persons = await dbContext.Persons.IgnoreQueryFilters().ToListAsync();
// List<Point> points = await dbContext.Points.IgnoreQueryFilters().ToListAsync();

#endregion

foreach (Person shanghaiPerson in persons)
{
    Console.WriteLine(shanghaiPerson.ToString());
}

foreach (Point point in points)
{
    Console.WriteLine(point.ToString());
}