# IsUnicode

该项目用于测试 EF Core 配置实体时对 string 类型的属性使用 `IsUnicode()` 方法的作用。

## How to run

运行条件：

- .NET 6.0 SDK
- dotnet-ef cli tools
- MariaDb
- MS SQL Server

运行步骤：

1. 将 AppDbContext.cs 的 `OnConfiguring()` 方法中的 MariaDB 或 MS SQL Server 的连接字符串改为您自己的测试数据库的连接字符串：
   ```c#
   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
       #region MariaDB Configuration
   
       // optionsBuilder.UseMySql("server=localhost;port=3306;database=is_unicode_test;user=root;password=password;",
       //     new MariaDbServerVersion(new Version(10, 6)));
       
       #endregion
   
       #region MSSQL Configuration
   
       optionsBuilder.UseSqlServer("Server=localhost;Database=IsUnicodeTest;User Id=sa;Password=Password01!;");
       
       #endregion
       
       optionsBuilder.LogTo(Console.WriteLine);
       
       base.OnConfiguring(optionsBuilder);
   }
   ```
2. 假如您想要使用 MariaDB 进行测试，解除 MariaDB Configuration region 中配置代码的注释，然后注释掉 MSSQL Configuration region 中对 MSSQL 配置的代码
3. cd 到含有 IsUnicode.csproj 文件的目录下
4. 删除 Migrations_MSSQL 和 Migrations_MariaDB 两个目录及其中的内容。因为这是我本地的 Migration 文件
5. 运行命令 `dotnet ef migrations add Init`
6. 运行命令 `dotnet ef database update` 命令，生成数据库
7. 运行命令 dotnet run