using CollectManagement.Domain.Reseaux;
using CollectManagement.Domain.Reseaux.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.ReseauConfigurations;

public class ReseauConfiguration : IEntityTypeConfiguration<Reseau>
{
    public void Configure(EntityTypeBuilder<Reseau> builder)
    {
        builder.HasKey(c => c.ReseauId);

        builder.Property(c => c.ReseauId)
            .HasConversion(c => c.Value.ToGuid(), value => new ReseauId(new Ulid(value)));

        builder.Property(p => p.IpAddress).HasMaxLength(100).IsRequired();
        builder.Property(p => p.Port).IsRequired();
        builder.Property(p => p.Latitude).HasColumnType("decimal(18,10)");
        builder.Property(p => p.Longitude).HasColumnType("decimal(18,10)");
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
