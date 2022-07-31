using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SetQueryFilterForAllEntitiesByExpression;

public class Point : BaseEntity
{
    public long Id { get; set; }
    
    public double X { get; set; }
    
    public double Y { get; set; }

    public override string ToString()
    {
        return $"Point: Id: {Id}, X: {X}, Y:{Y}, SoftDelete: {SoftDelete}";
    }
}

public class PointConfiguration : IEntityTypeConfiguration<Point>
{
    public void Configure(EntityTypeBuilder<Point> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasQueryFilter(x => !x.SoftDelete);
    }
}