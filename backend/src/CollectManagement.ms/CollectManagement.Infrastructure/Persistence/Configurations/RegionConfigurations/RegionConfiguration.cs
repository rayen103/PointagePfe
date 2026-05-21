using CollectManagement.Domain.Regions;
using CollectManagement.Domain.Regions.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.RegionConfigurations;

public class RegionConfiguration : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.HasKey(c => c.RegionId);

        builder.Property(c => c.RegionId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new RegionId(new Ulid(value)));

        builder.Property(p => p.CodeRegion)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleRegion)
            .HasMaxLength(200);

        builder.Property(p => p.CodeGouvernorat)
            .HasMaxLength(50);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.SocieteId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
