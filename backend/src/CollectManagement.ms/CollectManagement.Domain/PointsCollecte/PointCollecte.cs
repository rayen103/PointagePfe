using CollectManagement.Domain.Common;
using CollectManagement.Domain.PointsCollecte.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.PointsCollecte;

public class PointCollecte : AuditableEntity
{
    public PointCollecteId PointCollecteId { get; private set; }
    
    public string CodePointCollecte { get; private set; }
    
    public string LibellePointCollecte { get; private set; }
    
    public decimal? Latitude { get; private set; }
    
    public decimal? Longitude { get; private set; }
    
    public string? CodeGouvernorat { get; private set; }
    
    public string? CodeRegion { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private PointCollecte(
        PointCollecteId pointCollecteId,
        string codePointCollecte,
        string libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        string? codeGouvernorat,
        string? codeRegion,
        bool isActive,
        SocieteId societeId)
    {
        PointCollecteId = pointCollecteId;
        CodePointCollecte = codePointCollecte;
        LibellePointCollecte = libellePointCollecte;
        Latitude = latitude;
        Longitude = longitude;
        CodeGouvernorat = codeGouvernorat;
        CodeRegion = codeRegion;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static PointCollecte Create(
        PointCollecteId pointCollecteId,
        string codePointCollecte,
        string libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        string? codeGouvernorat,
        string? codeRegion,
        bool isActive,
        SocieteId societeId)
    {
        return new PointCollecte(
            pointCollecteId,
            codePointCollecte,
            libellePointCollecte,
            latitude,
            longitude,
            codeGouvernorat,
            codeRegion,
            isActive,
            societeId);
    }

    public void Update(
        string codePointCollecte,
        string libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        string? codeGouvernorat,
        string? codeRegion,
        bool isActive)
    {
        CodePointCollecte = codePointCollecte;
        LibellePointCollecte = libellePointCollecte;
        Latitude = latitude;
        Longitude = longitude;
        CodeGouvernorat = codeGouvernorat;
        CodeRegion = codeRegion;
        IsActive = isActive;
    }
    
    public static PointCollecte QueryCreate(
        PointCollecteId pointCollecteId,
        string codePointCollecte,
        string libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        string? codeGouvernorat,
        string? codeRegion,
        bool isActive,
        SocieteId societeId)
    {
        return new PointCollecte(
            pointCollecteId,
            codePointCollecte,
            libellePointCollecte,
            latitude,
            longitude,
            codeGouvernorat,
            codeRegion,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private PointCollecte() { }
#pragma warning restore CS8618
}
