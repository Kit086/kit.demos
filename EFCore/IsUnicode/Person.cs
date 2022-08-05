using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IsUnicode;

public class Person
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"Person: Id: {Id}, Name: {Name}, Name.Length: {Name.Length}";
    }
}

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(128)
            .IsRequired();
    }
} 