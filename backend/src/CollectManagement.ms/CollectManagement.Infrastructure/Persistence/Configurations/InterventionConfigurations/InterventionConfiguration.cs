using CollectManagement.Domain.Interventions;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.InterventionConfigurations;

public class InterventionConfiguration : IEntityTypeConfiguration<Intervention>
{
    public void Configure(EntityTypeBuilder<Intervention> builder)
    {
        builder.HasKey(c => c.InterventionId);

        builder.Property(c => c.InterventionId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new InterventionId(new Ulid(value)));

        builder.Property(p => p.NumeroIntervention)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.Property(p => p.DateIntervention)
            .IsRequired();

        builder.Property(p => p.TypeIntervention)
            .HasMaxLength(50);

        builder.Property(p => p.Statut)
            .HasMaxLength(50);

        builder.Property(p => p.Cout)
            .HasColumnType("decimal(18,2)");
    }
}
