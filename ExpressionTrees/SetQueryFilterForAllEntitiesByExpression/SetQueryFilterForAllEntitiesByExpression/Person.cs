using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SetQueryFilterForAllEntitiesByExpression;

public class Person : BaseEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public override string ToString()
    {
        return $"Person: Id: {Id}, Name: {Name}, SoftDelete: {SoftDelete}";
    }
}

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Name).IsUnicode().HasMaxLength(128).IsRequired();

        builder.HasQueryFilter(x => !x.SoftDelete);
    }
}