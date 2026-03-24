using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.RattachementConfigurations;

public class RattachementArticleConfiguration : IEntityTypeConfiguration<RattachementArticle>
{
    public void Configure(EntityTypeBuilder<RattachementArticle> builder)
    {
        builder.HasKey(c => c.RattachementArticleId);

        builder.Property(c => c.RattachementArticleId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new RattachementArticleId(new Ulid(value)));

        builder.Property(c => c.RattachementId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new RattachementId(new Ulid(value)));

        builder.Property(p => p.CodeArticle)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Libelle)
            .HasMaxLength(200);

        builder.Property(p => p.Quantite)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.PrixRevient)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.TauxTVA)
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.CodeUnite)
            .HasMaxLength(20);

        builder.Property(p => p.CodeEntrepot)
            .HasMaxLength(50);

        builder.Property(p => p.TypeRattachement)
            .HasMaxLength(50);

        builder.Property(p => p.NumeroBonLivraison)
            .HasMaxLength(50);

        builder.Property(p => p.DateBonLivraison)
            .HasColumnType("date");

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.SocieteId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Rattachement)
            .WithMany()
            .HasForeignKey(c => c.RattachementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
