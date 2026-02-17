using CollectManagement.Domain.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.EquipeConfigurations;

public class EquipeConfiguration : IEntityTypeConfiguration<Equipe>
{
    public void Configure(EntityTypeBuilder<Equipe> builder)
    {
        builder.HasKey(c => c.EquipeId);

        builder.Property(c => c.EquipeId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new EquipeId(new Ulid(value)));

        builder.Property(p => p.CodeEquipe)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleEquipe)
            .HasMaxLength(200);

        builder.Property(p => p.CodeClient)
            .HasMaxLength(50);

        builder.Property(p => p.CodeEntrepot)
            .HasMaxLength(50);

        builder.Property(p => p.CodeTarif)
            .HasMaxLength(50);

        builder.Property(p => p.CodeFournisseur)
            .HasMaxLength(50);

        builder.Property(p => p.Responsable)
            .HasMaxLength(100);

        builder.Property(p => p.IsInternal)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(p => p.CodeVehicule)
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
