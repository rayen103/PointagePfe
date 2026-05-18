using CollectManagement.Domain.Bus;
using CollectManagement.Domain.Bus.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.BusConfigurations;

public class BusRuntimeEventConfiguration : IEntityTypeConfiguration<BusRuntimeEvent>
{
    public void Configure(EntityTypeBuilder<BusRuntimeEvent> builder)
    {
        builder.HasKey(c => c.BusRuntimeEventId);

        builder.Property(c => c.BusRuntimeEventId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new BusRuntimeEventId(new Ulid(value)));

        builder.Property(c => c.BusId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new BusId(new Ulid(value)));

        builder.Property(c => c.EventType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(c => c.IMEI)
            .HasMaxLength(50);

        builder.Property(c => c.Latitude);
        builder.Property(c => c.Longitude);
        builder.Property(c => c.Occupancy);

        builder.Property(c => c.OccurredAtUtc)
            .IsRequired();

        builder.HasIndex(c => c.BusId);
        builder.HasIndex(c => c.OccurredAtUtc);

        builder.HasOne<Bus>()
            .WithMany()
            .HasForeignKey(c => c.BusId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
