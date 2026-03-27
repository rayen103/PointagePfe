using CollectManagement.Domain.Employes;
using CollectManagement.Domain.Employes.Enums;
using CollectManagement.Domain.Employes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.EmployeConfigurations;

public class EmployeConfiguration : IEntityTypeConfiguration<Employe>
{
    public void Configure(EntityTypeBuilder<Employe> builder)
    {
        builder.HasKey(c => c.EmployeId);

        builder.Property(c => c.EmployeId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new EmployeId(new Ulid(value)));

        builder.Property(c => c.SocieteId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.Property(p => p.Matricule)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.RFID)
            .HasMaxLength(50);

        builder.Property(p => p.Nom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Prenom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.TypeEmploye)
            .HasConversion(v => v.ToString(), v => Enum.Parse<TypeEmploye>(v))
            .HasMaxLength(20)
            .IsRequired()
            .HasDefaultValue(TypeEmploye.EmployeSimple);

        builder.Property(p => p.CodeCircuit)
            .HasMaxLength(50);

        builder.Property(p => p.CodePointCollecte)
            .HasMaxLength(50);

        builder.Property(p => p.CodeBus)
            .HasMaxLength(50);

        builder.Property(p => p.CodeShift)
            .HasMaxLength(50);

        builder.Property(p => p.Adresse)
            .HasMaxLength(255);

        builder.Property(p => p.CodeGouvernorat)
            .HasMaxLength(50);

        builder.Property(p => p.CodeRegion)
            .HasMaxLength(50);

        builder.Property(p => p.Latitude);

        builder.Property(p => p.Longitude);

        // Foreign key relationship with Societe
        builder.HasOne(e => e.Societe)
            .WithMany()
            .HasForeignKey(e => e.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
