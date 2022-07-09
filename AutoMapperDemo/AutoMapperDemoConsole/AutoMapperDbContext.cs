using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperDemoConsole;

public class AutoMapperDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=AutoMapperDemoDb")
            .LogTo(Console.WriteLine)
            .EnableDetailedErrors(false)
            .EnableSensitiveDataLogging(false);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        DataSeeding.DoDataSeeding(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }
}