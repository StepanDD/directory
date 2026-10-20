using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");

        builder.Property(p => p.Name).IsRequired().HasColumnName("name");

        builder.Property(p => p.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(p => p.UpdatedAt).IsRequired().HasColumnName("updated_at");
    }
}
