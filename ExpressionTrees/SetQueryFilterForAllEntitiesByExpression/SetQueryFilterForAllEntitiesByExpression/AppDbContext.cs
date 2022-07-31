using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        #region 究极简单的写法

        foreach (var entityType in modelBuilder.Model.GetEntityTypes().AsParallel())
        {
            if (entityType.FindProperty("SoftDelete") is { } softDeleteProperty && softDeleteProperty.ClrType == typeof(bool))
            {
                var parameterExpr = Expression.Parameter(entityType.ClrType, "x");
                
                var filter =
                    Expression.Lambda(
                        Expression.Not(Expression.PropertyOrField(parameterExpr, softDeleteProperty.PropertyInfo!.Name)),
                        parameterExpr);
                
                entityType.SetQueryFilter(filter);
            }
        }

        #endregion

        #region 简单的写法

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
        //                 Expression.Not(Expression.PropertyOrField(parameterExpr, softDeleteProperty.PropertyInfo!.Name)),
        //                 parameterExpr);
        //         
        //         entityType.SetQueryFilter(filter);
        //     }
        // }

        #endregion

        #region 详细拆分的写法

        // // 遍历实体类型
        // foreach (var entityType in modelBuilder.Model.GetEntityTypes().AsParallel())
        // {
        //     // 拿到实体类型的 SoftDelete 属性
        //     var softDeleteProperty = entityType.FindProperty("SoftDelete");
        //     
        //     // 如果实体类型有 SoftDelete 属性，且该属性是 bool 类型的
        //     if (softDeleteProperty is not null && softDeleteProperty.ClrType == typeof(bool))
        //     {
        //         // 构建一个 "x => x.SoftDelete == false" lambda 表达式并作为参数传给 entityType.SetQueryFilter()
        //         
        //         // 1. 构建 "x" 参数表达式（ParameterExpression）
        //         var parameterExpr = Expression.Parameter(entityType.ClrType, "x");
        //
        //         // 2. 构建 "false" 常量表达式（ConstantExpression）
        //         var constantExpr = Expression.Constant(false, typeof(bool));
        //         
        //         // 3. 构建 "x.SoftDelete" 成员表达式（MemberExpress）
        //         var memberExpression = Expression.PropertyOrField(parameterExpr, softDeleteProperty.PropertyInfo!.Name);
        //         
        //         // 4. 组合出 "x.SoftDelete == false" 相等二元运算表达式（BinaryExpression）
        //         var equalBinaryExpression = Expression.Equal(memberExpression, constantExpr);
        //         
        //         // 5. 组合出 "x => x.SoftDelete == false" lambda 表达式（LambdaExpression）
        //         var filter = Expression.Lambda(equalBinaryExpression, parameterExpr);
        //         
        //         // 6. 设置 QueryFilter
        //         entityType.SetQueryFilter(filter);
        //     }
        // }

        #endregion

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