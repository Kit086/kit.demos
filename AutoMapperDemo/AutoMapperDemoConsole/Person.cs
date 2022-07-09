using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoMapperDemoConsole;

public class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int? Age { get; set; }
    public Address? Address { get; set; }
    public ICollection<Account> Accounts { get; set; } = new List<Account>();
    public DateTime CreatedUtc { get; set; }
    public DateTime? ModifiedUtc { get; set; }
}

public class PersonConfig : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasOne<Address>(p => p.Address)
            .WithOne();
        builder.HasMany(p => p.Accounts)
            .WithOne();
    }
}