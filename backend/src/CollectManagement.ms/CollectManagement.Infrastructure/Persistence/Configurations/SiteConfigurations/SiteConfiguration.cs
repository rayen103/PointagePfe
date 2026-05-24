using CollectManagement.Domain.Sites;
using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.SiteConfigurations;

public class SiteConfiguration : IEntityTypeConfiguration<Site>
{
    public void Configure(EntityTypeBuilder<Site> builder)
    {
        builder.HasKey(c => c.SiteId);

        builder.Property(c => c.SiteId)
            .HasConversion(c => c.Value.ToGuid(), value => new SiteId(new Ulid(value)));

        builder.Property(p => p.Code).HasMaxLength(50).IsRequired();
        builder.Property(p => p.LibelleSite).HasMaxLength(200).IsRequired();
        builder.Property(p => p.Siege).IsRequired();
        builder.Property(p => p.Longitude).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Latitude).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Rayon).HasColumnType("decimal(18,3)");
        builder.Property(p => p.IsActive).HasDefaultValue(true).IsRequired();

        builder.Property(p => p.SocieteId)
            .HasConversion(c => c.Value.ToGuid(), value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
