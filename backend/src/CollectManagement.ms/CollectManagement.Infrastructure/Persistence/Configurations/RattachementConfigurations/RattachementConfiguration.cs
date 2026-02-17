using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.RattachementConfigurations;

public class RattachementConfiguration : IEntityTypeConfiguration<Rattachement>
{
    public void Configure(EntityTypeBuilder<Rattachement> builder)
    {
        builder.ToTable("GP_Rattachement");
        
        builder.HasKey(c => c.RattachementId);

        builder.Property(c => c.RattachementId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new RattachementId(new Ulid(value)));

        builder.Property(p => p.NumeroRattachement)
            .HasColumnName("NRattachement")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Exercice)
            .HasMaxLength(20);

        builder.Property(p => p.DateRattachement)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(p => p.NumeroChantier)
            .HasColumnName("NChantier")
            .HasMaxLength(50);

        builder.Property(p => p.CodeClient)
            .HasColumnName("CClient")
            .HasMaxLength(50);

        builder.Property(p => p.IsInternal)
            .HasColumnName("BInterne")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(p => p.Cout)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.Type)
            .HasMaxLength(50);

        builder.Property(p => p.Nature)
            .HasMaxLength(50);

        builder.Property(p => p.Responsable)
            .HasMaxLength(100);

        builder.Property(p => p.HeureDebut)
            .HasMaxLength(10);

        builder.Property(p => p.HeureFin)
            .HasMaxLength(10);

        builder.Property(p => p.Emplacement)
            .HasMaxLength(200);

        builder.Property(p => p.Reference)
            .HasMaxLength(100);

        builder.Property(p => p.Status)
            .HasColumnName("Cloture")
            .HasMaxLength(50);

        builder.Property(p => p.DateCloture)
            .HasColumnType("date");

        builder.Property(p => p.Remarque)
            .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .HasColumnName("BActif")
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
