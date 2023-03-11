using System.Threading.Channels;
using Record;

// var persons = new List<Person>
// {
//     new Person("刘备", 50),
//     new Person("曹操", 51),
//     new Person("孙权", 8)
// };
//
// foreach (var person in persons)
// {
//     Console.WriteLine(person);
// }

// var liubei1 = new Person("刘备", 50);
//
// var liubei2 = Person.Create(liubei1);
//
// Console.WriteLine(liubei1 == liubei2);
// Console.WriteLine(liubei1 != liubei2);


// var liubei1 = new Person("刘备", 50);
//
// var (name, age) = liubei1;
//
// Console.WriteLine(name);
// Console.WriteLine(age);

// var liubei1 = new Person("刘备", 50);
// var liubei2 = liubei1 with { Name = "刘备2" };
// Console.WriteLine(liubei2);

// var persons = new List<Person>
// {
//     new Person("刘备", 50),
//     new Person("刘备", 50),
//     new Person("曹操", 51),
//     new Person("曹操", 51),
//     new Person("孙权", 8)
// };
//
// Console.WriteLine($"总数：{persons.Count}，去重后的总数：{persons.Distinct().Count()}");
//
// var grouped = persons.GroupBy(p => p)
//     .Select(p => (p.Key, p.Count()));
//
// foreach (var (person, count) in grouped)
// {
//     Console.WriteLine($"{person}: {count}");
// }

var persons = new List<Person>
{
    Person.Create("刘备", 50),
    Person.Create("刘备", 50),
    Person.Create("曹操", 51),
    Person.Create("曹操", 51),
    Person.Create("孙权", 8)
};

Console.WriteLine($"总数：{persons.Count}，去重后的总数：{persons.Distinct().Count()}");

var grouped = persons.GroupBy(p => p)
    .Select(p => (p.Key, p.Count()));

foreach (var (person, count) in grouped)
{
    Console.WriteLine($"{person}: {count}");
}

var liubei1 = Person.Create("刘备", 50, "汉");
Console.WriteLine(liubei1);
