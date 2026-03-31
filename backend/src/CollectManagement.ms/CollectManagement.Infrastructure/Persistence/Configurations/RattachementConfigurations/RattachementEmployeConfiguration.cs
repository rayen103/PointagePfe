using CollectManagement.Domain.Rattachements;
using CollectManagement.Domain.Rattachements.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.RattachementConfigurations;

public class RattachementEmployeConfiguration : IEntityTypeConfiguration<RattachementEmploye>
{
    public void Configure(EntityTypeBuilder<RattachementEmploye> builder)
    {
        builder.HasKey(c => c.RattachementEmployeId);

        builder.Property(c => c.RattachementEmployeId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new RattachementEmployeId(new Ulid(value)));

        builder.Property(c => c.RattachementId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new RattachementId(new Ulid(value)));

        builder.Property(p => p.Matricule)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.NomPrenom)
            .HasMaxLength(100);

        builder.Property(p => p.DateDebut)
            .HasColumnType("date");

        builder.Property(p => p.HeureDebut)
            .HasColumnType("time");

        builder.Property(p => p.DateFin)
            .HasColumnType("date");

        builder.Property(p => p.HeureFin)
            .HasColumnType("time");

        builder.Property(p => p.NombreHeure)
            .HasColumnType("decimal(10,2)");

        builder.Property(p => p.Cout)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.CoutGlobal)
            .HasColumnType("decimal(18,3)");

        builder.Property(p => p.TypeRattachement)
            .HasMaxLength(50);

        builder.Property(p => p.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        builder.Property(p => p.SocieteId)
            .HasConversion(
                c => c.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.HasOne(c => c.Rattachement)
            .WithMany()
            .HasForeignKey(c => c.RattachementId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Societe)
            .WithMany()
            .HasForeignKey(c => c.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
