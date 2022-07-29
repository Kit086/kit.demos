using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ComparisonValueObject;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");

        optionsBuilder.LogTo(Console.WriteLine);
        
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PersonConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    public async Task SeedAsync()
    {
        if (!this.Persons.Any())
        {
            this.Persons.Add(new Person
            {
                Name = "Zhang Three",
                Address = new Address(country: "China", 
                    province: "Shanghai", 
                    city: "Shanghai", 
                    detail: "Xuhui District xxx Road xxx Long 1-101")
            });
            
            this.Persons.Add(new Person
            {
                Name = "Li Four",
                Address = new Address(country: "China", 
                    province: "Shanghai", 
                    city: "Shanghai",
                    detail: "Xuhui District xxx Road xxx Long 1-102")
            });
            
            this.Persons.Add(new Person
            {
                Name = "Wang Five",
                Address = new Address(country: "China", 
                    province: "Guangdong", 
                    city: "Guangzhou",
                    detail: "Tianhe District xxx Road No. xxx 10-1-101")
            });

            await this.SaveChangesAsync();
        }
    }
}