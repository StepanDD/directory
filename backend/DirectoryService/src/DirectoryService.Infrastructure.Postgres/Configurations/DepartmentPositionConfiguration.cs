using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentPositionConfiguration : IEntityTypeConfiguration<DepartmentPosition>
{
    public void Configure(EntityTypeBuilder<DepartmentPosition> builder)
    {
        builder.ToTable("department_position");

        builder.HasKey(dp => dp.Id);
        builder.Property(dp => dp.Id).HasColumnName("id");

        builder.HasOne(dp => dp.Department)
            .WithMany(d => d.Positions)
            .HasForeignKey("department_id")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(dp => dp.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(dp => dp.PositionId).IsRequired().HasColumnName("position_id");
    }
}
