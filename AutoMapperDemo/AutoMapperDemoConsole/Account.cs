using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoMapperDemoConsole;

public class Account
{
    public int AccountId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? EmailAddress { get; set; } = null!;

    #region relationships

    public int PersonId { get; set; }

    #endregion
}

public class AccountConfig : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasOne<Person>()
            .WithMany(p => p.Accounts);
    }
}