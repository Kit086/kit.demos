using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IsUnicode;

public class PersonWithUnicodeName
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
    
    public override string ToString()
    {
        return $"PersonWithUnicodeName: Id: {Id}, Name: {Name}, Name.Length: {Name.Length}";
    }
}

public class PersonWithUnicodeNameConfiguration : IEntityTypeConfiguration<PersonWithUnicodeName>
{
    public void Configure(EntityTypeBuilder<PersonWithUnicodeName> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsUnicode()
            .HasMaxLength(128)
            .IsRequired();
    }
}