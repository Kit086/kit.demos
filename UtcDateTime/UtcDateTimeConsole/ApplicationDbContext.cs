using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UtcDateTimeConsole;

public class ApplicationDbContext : DbContext
{
    private const string ConnectionString = "server=localhost;port=3306;database=kit_demo;user=kit;password=password;";

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MariaDbServerVersion(new Version(10, 6));
        optionsBuilder.UseMySql(ConnectionString, serverVersion)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ValueConverter<DateTime, DateTime> utcConverter =
            new(toDb => toDb,
                fromDb => DateTime.SpecifyKind(fromDb, DateTimeKind.Utc));

        ValueConverter<DateTime?, DateTime?> utcConverterForNullable =
            new(toDb => toDb,
                fromDb => fromDb.HasValue ? DateTime.SpecifyKind((DateTime)fromDb, DateTimeKind.Utc) : null);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes().AsParallel())
        {
            foreach (var entityProperty in entityType.GetProperties().AsParallel())
            {
                if (entityProperty.Name.EndsWith("Utc"))
                {
                    if (entityProperty.ClrType == typeof(DateTime))
                    {
                        entityProperty.SetValueConverter(utcConverter);
                    }
                    else if (entityProperty.ClrType == typeof(DateTime?))
                    {
                        entityProperty.SetValueConverter(utcConverterForNullable);
                    }
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}