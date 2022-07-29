using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComparisonValueObject;

public class Person
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public Address Address { get; set; } = null!;

    public override string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Country: {Address.Country}, Province: {Address.Province}, City: {Address.City}, Detail: {Address.Detail}";
    }
}

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(x => x.Name).IsUnicode().HasMaxLength(128).IsRequired();

        builder.OwnsOne(x => x.Address, navigationBuilder =>
        {
            navigationBuilder.Property(a => a.Country).IsUnicode().HasMaxLength(128).IsRequired();
            navigationBuilder.Property(a => a.Province).IsUnicode().HasMaxLength(128).IsRequired();
            navigationBuilder.Property(a => a.City).IsUnicode().HasMaxLength(128).IsRequired();
            navigationBuilder.Property(a => a.Detail).IsUnicode().HasMaxLength(512).IsRequired();
        });
    }
}