using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.OrdreTravailConfigurations;

public class OrdreTravailConfiguration : IEntityTypeConfiguration<OrdreTravail>
{
    public void Configure(EntityTypeBuilder<OrdreTravail> builder)
    {
        builder.HasKey(c => c.OrdreTravailId);

        builder.Property(c => c.OrdreTravailId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new OrdreTravailId(new Ulid(value)));

        builder.Property(p => p.NumeroOrdreTravail)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.NumeroChantier)
            .HasMaxLength(50);

        builder.Property(p => p.CodeClient)
            .HasMaxLength(50);

        builder.Property(p => p.NumeroBonCommande)
            .HasMaxLength(50);

        builder.Property(p => p.CodeEquipe)
            .HasMaxLength(50);

        builder.Property(p => p.EtatOT)
            .HasMaxLength(50);

        builder.Property(p => p.Montant)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.DateCreation)
            .HasColumnType("date");

        builder.Property(p => p.NumeroConvention)
            .HasMaxLength(50);

        builder.Property(p => p.CodeVehicule)
            .HasMaxLength(50);

        builder.Property(p => p.Libelle)
            .HasMaxLength(200);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
