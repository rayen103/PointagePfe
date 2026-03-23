using CollectManagement.Domain.Circuits;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.CircuitConfigurations;

public class CircuitConfiguration : IEntityTypeConfiguration<Circuit>
{
    public void Configure(EntityTypeBuilder<Circuit> builder)
    {
        builder.HasKey(c => c.CircuitId);

        builder.Property(c => c.CircuitId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new CircuitId(new Ulid(value)));

        builder.Property(p => p.CodeCircuit)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleCircuit)
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.CodePCDepart)
            .HasMaxLength(50);

        builder.Property(p => p.CodePCArrivee)
            .HasMaxLength(50);

        builder.Property(p => p.DistanceKm)
            .HasPrecision(10, 2);

        builder.Property(p => p.Couleur)
            .HasMaxLength(20);

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
