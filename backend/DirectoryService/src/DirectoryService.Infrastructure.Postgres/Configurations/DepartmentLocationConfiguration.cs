using DirectoryService.Domain;
using DirectoryService.Domain.DepartmentSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_location");

        builder.HasKey(dl => dl.Id);
        builder.Property(dl => dl.Id).HasColumnName("id");

        builder.Property(dl => dl.IsPrimary).IsRequired().HasColumnName("is_primary");

        builder.HasOne(dl => dl.Department)
            .WithMany(d => d.Locations)
            .HasForeignKey("department_id")
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(dl => dl.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Property(dl => dl.LocationId).IsRequired().HasColumnName("location_id");

    }
}
