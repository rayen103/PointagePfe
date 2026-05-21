using CollectManagement.Domain.Gouvernorats;
using CollectManagement.Domain.Gouvernorats.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.GouvernoratConfigurations;

public class GouvernoratConfiguration : IEntityTypeConfiguration<Gouvernorat>
{
    public void Configure(EntityTypeBuilder<Gouvernorat> builder)
    {
        builder.HasKey(c => c.GouvernoratId);

        builder.Property(c => c.GouvernoratId)
            .HasConversion(c => c.Value.ToGuid(),
                value => new GouvernoratId(new Ulid(value)));

        builder.Property(p => p.CodeGouvernorat)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.LibelleGouvernorat)
            .HasMaxLength(200);

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
