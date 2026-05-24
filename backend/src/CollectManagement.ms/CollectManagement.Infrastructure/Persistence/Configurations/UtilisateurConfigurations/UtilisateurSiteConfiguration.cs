using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Utilisateurs.Entities;
using CollectManagement.Domain.Utilisateurs.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectManagement.Infrastructure.Persistence.Configurations.UtilisateurConfigurations;

public class UtilisateurSiteConfiguration : IEntityTypeConfiguration<UtilisateurSite>
{
    public void Configure(EntityTypeBuilder<UtilisateurSite> builder)
    {
        builder.HasKey(x => new { x.UtilisateurId, x.SiteId });

        builder.Property(p => p.UtilisateurId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new UtilisateurId(new Ulid(value)));

        builder.Property(p => p.SiteId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new SiteId(new Ulid(value)));

        builder.HasOne(x => x.Utilisateur)
            .WithMany(x => x.Sites)
            .HasForeignKey(x => x.UtilisateurId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Site)
            .WithMany()
            .HasForeignKey(x => x.SiteId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}