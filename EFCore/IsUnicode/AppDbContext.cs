using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace IsUnicode;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;

    public DbSet<PersonWithUnicodeName> PersonWithUnicodeNames { get; set; } = null!;

    public DbSet<PersonWithoutUnicodeName> PersonWithoutUnicodeNames { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        #region MariaDb Configuration
        
        // optionsBuilder.UseMySql("server=localhost;port=3306;database=is_unicode_test;user=root;password=password;",
        //     new MariaDbServerVersion(new Version(10, 6)));
        
        #endregion

        #region MSSQL Configuration

        optionsBuilder.UseSqlServer("Server=localhost;Database=IsUnicodeTest;User Id=sa;Password=Password01!;");

        #endregion

        optionsBuilder.LogTo(Console.WriteLine);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }

    public async Task SeedAsync()
    {
        if (!this.Persons.Any())
        {
            this.Persons.Add(new Person
            {
                Name = "Zhang Three"
            });
            this.Persons.Add(new Person
            {
                Name = "李四"
            });
            this.Persons.Add(new Person
            {
                Name = "王😿"
            });
        }

        if (!this.PersonWithUnicodeNames.Any())
        {
            this.PersonWithUnicodeNames.Add(new PersonWithUnicodeName
            {
                Name = "Zhang Three"
            });
            this.PersonWithUnicodeNames.Add(new PersonWithUnicodeName
            {
                Name = "李四"
            });
            this.PersonWithUnicodeNames.Add(new PersonWithUnicodeName
            {
                Name = "王😿"
            });
        }

        if (!this.PersonWithoutUnicodeNames.Any())
        {
            this.PersonWithoutUnicodeNames.Add(new PersonWithoutUnicodeName
            {
                Name = "Zhang Three"
            });
            this.PersonWithoutUnicodeNames.Add(new PersonWithoutUnicodeName
            {
                Name = "李四"
            });
            this.PersonWithoutUnicodeNames.Add(new PersonWithoutUnicodeName
            {
                Name = "王😿"
            });
        }

        await this.SaveChangesAsync();
    }
}