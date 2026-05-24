using CollectManagement.Domain.Modems;
using CollectManagement.Domain.Modems.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.ModemConfigurations;

public class ModemConfiguration : IEntityTypeConfiguration<Modem>
{
    public void Configure(EntityTypeBuilder<Modem> builder)
    {
        builder.HasKey(c => c.ModemId);

        builder.Property(c => c.ModemId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new ModemId(new Ulid(value)));

        builder.Property(p => p.IMEI)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(p => p.IMEI)
            .IsUnique();

        builder.Property(p => p.ModelModem)
            .HasMaxLength(100);

        builder.Property(p => p.NumeroSim)
            .HasMaxLength(50);

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
