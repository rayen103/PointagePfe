using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.OrdreTravailConfigurations;

public class OrdreTravailConfiguration : IEntityTypeConfiguration<OrdreTravail>
{
    public void Configure(EntityTypeBuilder<OrdreTravail> builder)
    {
        builder.ToTable("GP_OrdredeTravail");
        
        builder.HasKey(c => c.OrdreTravailId);

        builder.Property(c => c.OrdreTravailId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new OrdreTravailId(new Ulid(value)));

        builder.Property(p => p.NumeroOrdreTravail)
            .HasColumnName("NOrdredeTravail")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.NumeroChantier)
            .HasColumnName("Nchantier")
            .HasMaxLength(50);

        builder.Property(p => p.CodeClient)
            .HasColumnName("CClient")
            .HasMaxLength(50);

        builder.Property(p => p.NumeroBonCommande)
            .HasColumnName("NBonCommande")
            .HasMaxLength(50);

        builder.Property(p => p.CodeEquipe)
            .HasColumnName("CSousTraitant")
            .HasMaxLength(50);

        builder.Property(p => p.EtatOT)
            .HasMaxLength(50);

        builder.Property(p => p.Montant)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.DateCreation)
            .HasColumnType("date");

        builder.Property(p => p.NumeroConvention)
            .HasColumnName("NConvention")
            .HasMaxLength(50);

        builder.Property(p => p.CodeVehicule)
            .HasColumnName("CVehicule")
            .HasMaxLength(50);

        builder.Property(p => p.Libelle)
            .HasColumnName("RaisonSociale")
            .HasMaxLength(200);

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
