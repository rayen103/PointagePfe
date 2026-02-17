using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.PointCollecteConfigurations;

public class PointCollecteConfiguration : IEntityTypeConfiguration<PointCollecte>
{
    public void Configure(EntityTypeBuilder<PointCollecte> builder)
    {
        builder.HasKey(c => c.PointCollecteId);

        builder.Property(c => c.PointCollecteId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new PointCollecteId(new Ulid(value)));

        builder.Property(p => p.CodePointCollecte)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibellePointCollecte)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Latitude)
            .HasColumnType("decimal(18,10)");

        builder.Property(p => p.Longitude)
            .HasColumnType("decimal(18,10)");

        builder.Property(p => p.CodeGouvernorat)
            .HasMaxLength(50);

        builder.Property(p => p.CodeRegion)
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
