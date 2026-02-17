using CollectManagement.Domain.PointsCollecte;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.PointCollecteConfigurations;

public class PointCollecteConfiguration : IEntityTypeConfiguration<PointCollecte>
{
    public void Configure(EntityTypeBuilder<PointCollecte> builder)
    {
        builder.ToTable("PointCollecte");
        
        builder.HasKey(c => c.PointCollecteId);

        builder.Property(c => c.PointCollecteId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new PointCollecteId(new Ulid(value)));

        builder.Property(p => p.CodePointCollecte)
            .HasColumnName("Code_PC")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibellePointCollecte)
            .HasColumnName("Lib_PC")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Latitude)
            .HasColumnName("Latt_PC")
            .HasColumnType("decimal(18,10)");

        builder.Property(p => p.Longitude)
            .HasColumnName("Long_PC")
            .HasColumnType("decimal(18,10)");

        builder.Property(p => p.CodeGouvernorat)
            .HasColumnName("Code_Gouv_PC")
            .HasMaxLength(50);

        builder.Property(p => p.CodeRegion)
            .HasColumnName("Code_Region_PC")
            .HasMaxLength(50);

        builder.Property(p => p.IsActive)
            .HasColumnName("BActif")
            .HasDefaultValue(true)
            .IsRequired();
        
        builder.Property(p => p.SocieteId)
            .HasColumnName("Code_Societe")
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
