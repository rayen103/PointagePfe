using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.BusConfigurations;

public class BusConfiguration : IEntityTypeConfiguration<Bus>
{
    public void Configure(EntityTypeBuilder<Bus> builder)
    {
        builder.HasKey(c => c.BusId);

        builder.Property(c => c.BusId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new BusId(new Ulid(value)));

        builder.Property(p => p.NumeroIMM)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ModelBus)
            .HasMaxLength(100);

        builder.Property(p => p.IMEI)
            .HasMaxLength(50);

        builder.Property(p => p.Capacite);

        builder.Property(p => p.CodeCircuit)
            .HasMaxLength(50);

        builder.Property(p => p.CodeChauffeur)
            .HasMaxLength(50);

        builder.Property(p => p.AppSagem)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.Latitude);

        builder.Property(p => p.Longitude);

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
