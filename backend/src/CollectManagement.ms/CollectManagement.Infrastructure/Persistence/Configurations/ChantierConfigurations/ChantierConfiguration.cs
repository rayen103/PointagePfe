using CollectManagement.Domain.Chantiers;
using CollectManagement.Domain.Chantiers.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.ChantierConfigurations;

public class ChantierConfiguration : IEntityTypeConfiguration<Chantier>
{
    public void Configure(EntityTypeBuilder<Chantier> builder)
    {
        builder.HasKey(c => c.ChantierId);

        builder.Property(c => c.ChantierId)
            .HasConversion(c => c.Value.ToGuid(), value => new ChantierId(new Ulid(value)));

        builder.Property(p => p.NumeroChantier).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LibelleChantier).HasMaxLength(200);
        builder.Property(p => p.CodeClient).HasMaxLength(50);
        builder.Property(p => p.Adresse).HasMaxLength(300);
        builder.Property(p => p.MontantHT).HasPrecision(18, 2);
        builder.Property(p => p.MontantTTC).HasPrecision(18, 2);
        builder.Property(p => p.Nature).HasMaxLength(100);
        builder.Property(p => p.Responsable).HasMaxLength(100);
        builder.Property(p => p.Status).HasMaxLength(50);
        builder.Property(p => p.IsActive).HasDefaultValue(true).IsRequired();

        builder.Property(p => p.SocieteId)
            .HasConversion(c => c.Value.ToGuid(), value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
