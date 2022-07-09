using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoMapperDemoConsole;

public class Address
{
    public int AddressId { get; set; }
    public string Country { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string City { get; set; } = null!;
    public string DetailAddress { get; set; } = null!;

    #region relationships

    public int PersonId { get; set; }

    #endregion
}

public class AddressConfig : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasOne<Person>()
            .WithOne(p => p.Address);
    }
}