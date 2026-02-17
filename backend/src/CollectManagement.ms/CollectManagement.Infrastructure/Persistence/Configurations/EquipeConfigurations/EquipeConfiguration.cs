using CollectManagement.Domain.Equipes;
using CollectManagement.Domain.Equipes.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.EquipeConfigurations;

public class EquipeConfiguration : IEntityTypeConfiguration<Equipe>
{
    public void Configure(EntityTypeBuilder<Equipe> builder)
    {
        builder.ToTable("GP_Equipe");
        
        builder.HasKey(c => c.EquipeId);

        builder.Property(c => c.EquipeId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new EquipeId(new Ulid(value)));

        builder.Property(p => p.CodeEquipe)
            .HasColumnName("CEquipe")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleEquipe)
            .HasColumnName("Libelle")
            .HasMaxLength(200);

        builder.Property(p => p.CodeClient)
            .HasColumnName("CClient")
            .HasMaxLength(50);

        builder.Property(p => p.CodeEntrepot)
            .HasColumnName("CEntrepot")
            .HasMaxLength(50);

        builder.Property(p => p.CodeTarif)
            .HasColumnName("CTarif")
            .HasMaxLength(50);

        builder.Property(p => p.CodeFournisseur)
            .HasColumnName("CFournisseur")
            .HasMaxLength(50);

        builder.Property(p => p.Responsable)
            .HasMaxLength(100);

        builder.Property(p => p.IsInternal)
            .HasColumnName("BInterne")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(p => p.CodeVehicule)
            .HasColumnName("CVehicule")
            .HasMaxLength(50);

        builder.Property(p => p.IsActive)
            .HasColumnName("BActif")
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
