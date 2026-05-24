using CollectManagement.Domain.Common;
using CollectManagement.Domain.Sites.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Sites;

public class Site : AuditableEntity
{
    public SiteId SiteId { get; private set; }
    public string Code { get; private set; }
    public string LibelleSite { get; private set; }
    public bool Siege { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Rayon { get; private set; }
    public int? TimeMinute { get; private set; }
    public bool IsActive { get; private set; } = true;
    public SocieteId SocieteId { get; private set; }
    public Societe? Societe { get; private set; }

    private Site(
        SiteId siteId,
        string code,
        string libelleSite,
        bool siege,
        decimal? longitude,
        decimal? latitude,
        decimal? rayon,
        int? timeMinute,
        bool isActive,
        SocieteId societeId)
    {
        SiteId = siteId;
        Code = code;
        LibelleSite = libelleSite;
        Siege = siege;
        Longitude = longitude;
        Latitude = latitude;
        Rayon = rayon;
        TimeMinute = timeMinute;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Site Create(
        SiteId siteId,
        string code,
        string libelleSite,
        bool siege,
        decimal? longitude,
        decimal? latitude,
        decimal? rayon,
        int? timeMinute,
        bool isActive,
        SocieteId societeId)
    {
        return new Site(siteId, code, libelleSite, siege, longitude, latitude, rayon, timeMinute, isActive, societeId);
    }

    public void Update(
        string code,
        string libelleSite,
        bool siege,
        decimal? longitude,
        decimal? latitude,
        decimal? rayon,
        int? timeMinute,
        bool isActive)
    {
        Code = code;
        LibelleSite = libelleSite;
        Siege = siege;
        Longitude = longitude;
        Latitude = latitude;
        Rayon = rayon;
        TimeMinute = timeMinute;
        IsActive = isActive;
    }

    public static Site QueryCreate(
        SiteId siteId,
        string code,
        string libelleSite,
        bool siege,
        decimal? longitude,
        decimal? latitude,
        decimal? rayon,
        int? timeMinute,
        bool isActive,
        SocieteId societeId)
    {
        return new Site(siteId, code, libelleSite, siege, longitude, latitude, rayon, timeMinute, isActive, societeId);
    }

#pragma warning disable CS8618
    private Site() { }
#pragma warning restore CS8618
}
