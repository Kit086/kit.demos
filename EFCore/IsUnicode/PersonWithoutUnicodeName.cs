using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IsUnicode;

public class PersonWithoutUnicodeName
{
    public long Id { get; set; }

    // [Unicode(false)]
    public string Name { get; set; } = null!;
    
    public override string ToString()
    {
        return $"PersonWithoutUnicodeName: Id: {Id}, Name: {Name}, Name.Length: {Name.Length}";
    }
}

public class PersonWithoutUnicodeNameConfiguration : IEntityTypeConfiguration<PersonWithoutUnicodeName>
{
    public void Configure(EntityTypeBuilder<PersonWithoutUnicodeName> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsUnicode(false)
            .HasMaxLength(128)
            .IsRequired();
    }
}