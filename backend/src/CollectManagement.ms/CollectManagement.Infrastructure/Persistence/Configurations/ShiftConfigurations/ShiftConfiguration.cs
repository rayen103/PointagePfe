using CollectManagement.Domain.Shifts;
using CollectManagement.Domain.Shifts.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.ShiftConfigurations;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        builder.HasKey(c => c.ShiftId);

        builder.Property(c => c.ShiftId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new ShiftId(new Ulid(value)));

        builder.Property(p => p.CodeShift)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleShift)
            .HasMaxLength(200);

        builder.Property(p => p.JourSemaine)
            .HasMaxLength(20);

        builder.Property(p => p.HeureDebut)
            .HasColumnType("time");

        builder.Property(p => p.HeureFin)
            .HasColumnType("time");

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
