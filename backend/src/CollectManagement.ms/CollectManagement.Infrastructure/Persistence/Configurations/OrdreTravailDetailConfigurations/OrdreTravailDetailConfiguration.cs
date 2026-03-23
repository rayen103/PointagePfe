using CollectManagement.Domain.OrdresTravail;
using CollectManagement.Domain.OrdresTravail.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.OrdreTravailDetailConfigurations;

public class OrdreTravailDetailConfiguration : IEntityTypeConfiguration<OrdreTravailDetail>
{
    public void Configure(EntityTypeBuilder<OrdreTravailDetail> builder)
    {
        builder.HasKey(c => c.OrdreTravailDetailId);

        builder.Property(c => c.OrdreTravailDetailId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new OrdreTravailDetailId(new Ulid(value)));

        builder.Property(p => p.CodeArticle)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.CodeEntrepot)
            .HasMaxLength(50);

        builder.Property(p => p.CodeUnite)
            .HasMaxLength(50);

        builder.Property(p => p.LibelleArticle)
            .HasMaxLength(200);

        builder.Property(p => p.PrixUnitaireHT)
            .HasPrecision(18, 4);

        builder.Property(p => p.Quantite)
            .HasPrecision(18, 4);

        builder.Property(p => p.TauxTVA)
            .HasPrecision(18, 4);

        builder.Property(p => p.Montant)
            .HasPrecision(18, 4);

        builder.Property(c => c.OrdreTravailId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new OrdreTravailId(new Ulid(value)));

        builder.HasOne<OrdreTravail>()
            .WithMany(c => c.OrdreTravailDetails)
            .HasForeignKey(c => c.OrdreTravailId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
