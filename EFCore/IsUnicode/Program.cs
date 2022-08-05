using IsUnicode;
using Microsoft.EntityFrameworkCore;

await using AppDbContext dbContext = new AppDbContext();
await dbContext.SeedAsync();

var personList = await dbContext.Persons.ToListAsync();
var personWithUnicodeNameList = await dbContext.PersonWithUnicodeNames.ToListAsync();
var personWithoutUnicodeNameList = await dbContext.PersonWithoutUnicodeNames.ToListAsync();

// Console.OutputEncoding = Encoding.UTF8;

personList.ForEach(p => Console.WriteLine(p.ToString()));
personWithUnicodeNameList.ForEach(pwu => Console.WriteLine(pwu.ToString()));
personWithoutUnicodeNameList.ForEach(pwtu => Console.WriteLine(pwtu.ToString()));