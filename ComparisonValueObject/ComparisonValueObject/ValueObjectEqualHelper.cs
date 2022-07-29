using System.Linq.Expressions;

namespace ComparisonValueObject;

public static class ValueObjectEqualHelper
{
    /// <summary>
    /// 生成”检查值对象是否相等“的表达式树
    /// </summary>
    /// <param name="firstParameterPropertyAccessor">Func 委托表达式，用于取实体的值对象属性，在本例中是（p => p.Address）</param>
    /// <param name="secondParameter">用于比较的值对象参数（实际上就是 EF Core 的查询条件），在本例中是 Address 类型的值对象</param>
    /// <typeparam name="T">实体类型，本例中是 Person</typeparam>
    /// <typeparam name="TProperty">值对象类型（值对象是实体的 Property），本例中是 Address</typeparam>
    public static Expression<Func<T, bool>> CheckEqual<T, TProperty>(
        Expression<Func<T, TProperty>> firstParameterPropertyAccessor,
        TProperty? secondParameter)
        where T : class
        where TProperty : class
    {
        // 获取相等比较的第一个参数表达式
        ParameterExpression firstParameterExpr = firstParameterPropertyAccessor.Parameters.Single();

        // 将要构建的相等条件表达式
        BinaryExpression? conditionalExpr = null;

        // 遍历值对象的每一个属性，构造两个对象属性间比较的表达式
        foreach (var propertyInfo in typeof(TProperty).GetProperties())
        {
            // 将要构建的比较条件表达式
            BinaryExpression equalExpr;

            // 用于比较的值对象参数（实际上就是 EF Core 的查询条件）的值
            object? secondValue = secondParameter is not null ? propertyInfo.GetValue(secondParameter) : null;
            
            // 如果值对象，也就是查询条件的某个属性值为 null，则跳过这个属性的比较表达式生成
            // 否则生成的表达式就会同时要求该属性的值必须为 null
            // 翻译成的 SQL 就会多一个查询条件
            // 例如我们要查中国上海的人，查询条件会是：
            // new Address {Country = "China", Province = "Shanghai", City = "Shanghai"}
            // Address 的 Detail 属性的值就会为 null
            // 翻译成的 SQL 就会多这个查询条件：
            // AND ("p"."Address_Detail" IS NULL)
            // 会导致查询的数据不是我们想要的全部数据
            if (secondValue is null)
            {
                continue;
            }

            // 左表达式子树是 firstParameter 的属性
            var leftExpr = Expression.PropertyOrField(firstParameterPropertyAccessor.Body, propertyInfo.Name);

            // 右表达式子树是 secondParameter 的属性
            Expression rightExpr = Expression.Convert(Expression.Constant(secondValue), propertyInfo.PropertyType);

            // 判断属性的类型是否是原始类型，如果是 int 等原始类型，则直接调用 Equal 方法构建表达式即可
            if (propertyInfo.PropertyType.IsPrimitive)
            {
                equalExpr = Expression.Equal(leftExpr, rightExpr);
            }
            // 如果属性类型不是原始类型，而是 string 等，则需要调用相等运算符重载方法 op_Equality
            // 所以需要使用 MakeBinary 来手动构建 Binary 表达式
            else
            {
                equalExpr = Expression.MakeBinary(ExpressionType.Equal, leftExpr, rightExpr, false,
                    propertyInfo.PropertyType.GetMethod("op_Equality"));
            }

            // 遍历的第一个属性
            if (conditionalExpr is null)
            {
                conditionalExpr = equalExpr;
            }
            // 后续的属性
            else
            {
                // 多个连续的相等比较是由多个 AndAlso 操作组成的二叉树
                // 所以第一个属性确定了比较的 conditionalExpr 表达式
                // 后续属性都 AndAlso 即可
                conditionalExpr = Expression.AndAlso(conditionalExpr, equalExpr);
            }
        }

        // 如果比较的值对象的类没有任何属性
        if (conditionalExpr is null)
        {
            throw new Exception("cannot compare two ValueObject that have no properties.");
        }

        return Expression.Lambda<Func<T, bool>>(conditionalExpr, firstParameterExpr);
    }
}