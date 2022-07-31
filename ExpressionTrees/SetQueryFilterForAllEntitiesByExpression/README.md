# Set Query Filter For All Entities By Expression

这个 demo 用于演示如何动态构建表达式树，来为 EF Core 所有实体设置 QueryFilter。

## How to run

运行条件：
- .NET 6.0 SDK
- dotnet-ef cli tools

运行步骤：
1. cd 到含有 `SetQueryFilterForAllEntitiesByExpression.csproj` 文件的目录下
2. 运行命令 `dotnet ef database update` 命令，生成 test.db 数据库（sqlite）
3. 运行命令 `dotnet run`

或使用 Visual Studio 2019/2022 或 Rider 等工具打开 `SetQueryFilterForAllEntitiesByExpression.sln`，点击”调试“或”运行“按钮。

ef-core-dynamically-building-expression-trees-sets-soft-delete-query-filter-for-all-entities