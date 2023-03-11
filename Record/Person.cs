using System.Text;

namespace Record;

public record Person(string Name, int Age, string Country)
{
    public static Person Create(string name, int age, string country = "") => new Person(name, age, country);
    
    public static Person Create(Person original) => new Person(original);
}

// public class Person : IEquatable<Person>
// {
//     private readonly string _name;
//
//     private readonly int _age;
//
//     protected virtual Type EqualityContract
//     {
//         get { return typeof(Person); }
//     }
//
//     public string Name
//     {
//         get { return _name; }
//         init { _name = value; }
//     }
//
//     public int Age
//     {
//         get { return _age; }
//         init { _age = value; }
//     }
//
//     public Person(string Name, int Age)
//     {
//         _name = Name;
//         _age = Age;
//     }
//
//     public override string ToString()
//     {
//         StringBuilder stringBuilder = new StringBuilder();
//         stringBuilder.Append("Person");
//         stringBuilder.Append(" { ");
//         if (PrintMembers(stringBuilder))
//         {
//             stringBuilder.Append(" ");
//         }
//
//         stringBuilder.Append("}");
//         return stringBuilder.ToString();
//     }
//
//     protected virtual bool PrintMembers(StringBuilder builder)
//     {
//         builder.Append("Name");
//         builder.Append(" = ");
//         builder.Append((object)Name);
//         builder.Append(", ");
//         builder.Append("Age");
//         builder.Append(" = ");
//         builder.Append(Age.ToString());
//         return true;
//     }
//
//     public static bool operator !=(Person left, Person right)
//     {
//         return !(left == right);
//     }
//
//     public static bool operator ==(Person left, Person right)
//     {
//         return (object)left == right || ((object)left != null && left.Equals(right));
//     }
//
//     public override int GetHashCode()
//     {
//         return (EqualityComparer<Type>.Default.GetHashCode(EqualityContract) * -1521134295 +
//                 EqualityComparer<string>.Default.GetHashCode(_name)) * -1521134295 +
//                EqualityComparer<int>.Default.GetHashCode(_age);
//     }
//
//     public override bool Equals(object obj)
//     {
//         return Equals(obj as Person);
//     }
//
//     public virtual bool Equals(Person other)
//     {
//         return (object)this == other || ((object)other != null && EqualityContract == other.EqualityContract &&
//                                          EqualityComparer<string>.Default.Equals(_name, other._name) &&
//                                          EqualityComparer<int>.Default.Equals(_age, other._age));
//     }
//
//     public virtual Person Clone()
//     {
//         return new Person(this);
//     }
//
//     protected Person(Person original)
//     {
//         _name = original._name;
//         _age = original._age;
//     }
//
//     public void Deconstruct(out string Name, out int Age)
//     {
//         Name = this.Name;
//         Age = this.Age;
//     }
// }