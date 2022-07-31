using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SetQueryFilterForAllEntitiesByExpression;

public class AppDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Point> Points { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=test.db");

        optionsBuilder.LogTo(Console.WriteLine);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // foreach (var entityType in modelBuilder.Model.GetEntityTypes().AsParallel())
        // {
        //     var softDeleteProperty = entityType.FindProperty("SoftDelete");
        //     
        //     if (softDeleteProperty is not null && softDeleteProperty.ClrType == typeof(bool))
        //     {
        //         var parameterExpr = Expression.Parameter(entityType.ClrType, "x");
        //         
        //         var filter =
        //             Expression.Lambda(
        //                 Expression.Equal(Expression.Property(parameterExpr, softDeleteProperty.PropertyInfo!),
        //                     Expression.Constant(false, typeof(bool))),
        //                 parameterExpr);
        //         
        //         entityType.SetQueryFilter(filter);
        //     }
        // }

        base.OnModelCreating(modelBuilder);
    }

    public async Task SeedAsync()
    {
        if (!this.Persons.Any())
        {
            this.Persons.Add(new Person
            {
                Name = "Zhang Three",
                SoftDelete = true
            });
            this.Persons.Add(new Person
            {
                Name = "Li Four",
                SoftDelete = false
            });
            this.Persons.Add(new Person
            {
                Name = "Wang Five",
                SoftDelete = false
            });
        
            await this.SaveChangesAsync();
        }

        if (!this.Points.Any())
        {
            this.Points.Add(new Point
            {
                X = 0.0D,
                Y = 0.0D,
                SoftDelete = true
            });
            this.Points.Add(new Point
            {
                X = 1.1D,
                Y = 1.1D,
                SoftDelete = true
            });
            this.Points.Add(new Point
            {
                X = 2.2D,
                Y = 2.2D,
                SoftDelete = false
            });

            await this.SaveChangesAsync();
        }
    }
}