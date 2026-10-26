using System;
using DirectoryService.Domain.DepartmentSpace;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedNever();

        builder.Property(d => d.Name).IsRequired().HasColumnName("name");
        builder.Property(d => d.Slug).IsRequired().HasColumnName("slug");
        builder.Property(d => d.Path).IsRequired().HasColumnName("path");

        builder.HasOne(d => d.Parent)
            .WithMany()
            .HasForeignKey(d => d.ParentId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Property(d => d.ParentId).HasColumnName("parent_id");

        builder.Property(d => d.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(d => d.UpdatedAt).IsRequired().HasColumnName("updated_at");
    }
}
