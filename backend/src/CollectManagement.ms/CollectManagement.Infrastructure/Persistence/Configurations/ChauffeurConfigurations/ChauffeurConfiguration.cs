using CollectManagement.Domain.Chauffeurs;
using CollectManagement.Domain.Chauffeurs.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.ChauffeurConfigurations;

public class ChauffeurConfiguration : IEntityTypeConfiguration<Chauffeur>
{
    public void Configure(EntityTypeBuilder<Chauffeur> builder)
    {
        builder.HasKey(c => c.ChauffeurId);

        builder.Property(c => c.ChauffeurId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new ChauffeurId(new Ulid(value)));

        builder.Property(p => p.CodeChauffeur)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Nom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Prenom)
            .HasMaxLength(100);

        builder.Property(p => p.CIN)
            .HasMaxLength(50);

        builder.Property(p => p.RFIDChauffeur)
            .HasMaxLength(50);

        builder.Property(p => p.Externe)
            .HasDefaultValue(false)
            .IsRequired();

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
