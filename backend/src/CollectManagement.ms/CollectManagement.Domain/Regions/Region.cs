using CollectManagement.Domain.Common;
using CollectManagement.Domain.Regions.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Regions;

public class Region : AuditableEntity
{
    public RegionId RegionId { get; private set; }

    public string CodeRegion { get; private set; }

    public string? LibelleRegion { get; private set; }

    public string? CodeGouvernorat { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Region(
        RegionId regionId,
        string codeRegion,
        string? libelleRegion,
        string? codeGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        RegionId = regionId;
        CodeRegion = codeRegion;
        LibelleRegion = libelleRegion;
        CodeGouvernorat = codeGouvernorat;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Region Create(
        RegionId regionId,
        string codeRegion,
        string? libelleRegion,
        string? codeGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        return new Region(
            regionId,
            codeRegion,
            libelleRegion,
            codeGouvernorat,
            isActive,
            societeId);
    }

    public void Update(
        string codeRegion,
        string? libelleRegion,
        string? codeGouvernorat,
        bool isActive)
    {
        CodeRegion = codeRegion;
        LibelleRegion = libelleRegion;
        CodeGouvernorat = codeGouvernorat;
        IsActive = isActive;
    }

    public static Region QueryCreate(
        RegionId regionId,
        string codeRegion,
        string? libelleRegion,
        string? codeGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        return new Region(
            regionId,
            codeRegion,
            libelleRegion,
            codeGouvernorat,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Region() { }
#pragma warning restore CS8618
}
