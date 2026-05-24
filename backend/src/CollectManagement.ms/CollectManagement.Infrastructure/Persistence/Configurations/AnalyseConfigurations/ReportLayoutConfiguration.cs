using CollectManagement.Domain.Analyse;
using CollectManagement.Domain.Analyse.ValueObjects;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Infrastructure.Persistence.Configurations.AnalyseConfigurations;

public class ReportLayoutConfiguration : IEntityTypeConfiguration<ReportLayout>
{
    public void Configure(EntityTypeBuilder<ReportLayout> builder)
    {
        builder.HasKey(x => x.ReportLayoutId);

        builder.Property(x => x.ReportLayoutId)
            .HasConversion(
                x => x.Value.ToGuid(),
                value => new ReportLayoutId(new Ulid(value)));

        builder.Property(x => x.ReportType)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.ConfigJson)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.IsDefault)
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.SocieteId)
            .HasConversion(
                x => x.Value.ToGuid(),
                value => new SocieteId(new Ulid(value)));

        builder.HasOne(x => x.Societe)
            .WithMany()
            .HasForeignKey(x => x.SocieteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new { x.SocieteId, x.ReportType });
    }
}

