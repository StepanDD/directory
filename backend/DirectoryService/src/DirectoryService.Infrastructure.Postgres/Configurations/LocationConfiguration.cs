using DirectoryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("locations");

        builder.HasKey(l => l.Id);
        builder.Property(l => l.Id).HasColumnName("id").ValueGeneratedNever();

        builder.Property(l => l.Name).IsRequired().HasColumnName("name");

        builder.OwnsOne(l => l.Address, a =>
        {
            a.Property(ad => ad.Country).IsRequired().HasColumnName("country");
            a.Property(ad => ad.City).IsRequired().HasColumnName("city");
            a.Property(ad => ad.Street).IsRequired().HasColumnName("street");
            a.Property(ad => ad.HouseNumber).IsRequired().HasColumnName("house_number");
            a.Property(ad => ad.ApartmentNumber).HasColumnName("apartment_number");
        });

        builder.Property(l => l.CreatedAt).IsRequired().HasColumnName("created_at");
        builder.Property(l => l.UpdatedAt).IsRequired().HasColumnName("updated_at");
    }
}
