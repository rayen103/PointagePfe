using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.CircuitPointCollecteConfigurations;

public class CircuitPointCollecteConfiguration : IEntityTypeConfiguration<CircuitPointCollecte>
{
    public void Configure(EntityTypeBuilder<CircuitPointCollecte> builder)
    {
        builder.HasKey(c => c.CircuitPointCollecteId);

        builder.Property(c => c.CircuitPointCollecteId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new CircuitPointCollecteId(new Ulid(value)));

        builder.Property(p => p.CodePointCollecte)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibellePointCollecte)
            .HasMaxLength(200);

        builder.Property(p => p.Latitude)
            .HasPrecision(18, 6);

        builder.Property(p => p.Longitude)
            .HasPrecision(18, 6);

        builder.Property(c => c.CircuitId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new CircuitId(new Ulid(value)));

        builder.HasOne<Circuit>()
            .WithMany()
            .HasForeignKey(c => c.CircuitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
