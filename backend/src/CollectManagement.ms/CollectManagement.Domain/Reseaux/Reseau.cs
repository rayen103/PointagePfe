using CollectManagement.Domain.Common;
using CollectManagement.Domain.Reseaux.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Reseaux;

public class Reseau : AuditableEntity
{
    public ReseauId ReseauId { get; private set; }
    public string IpAddress { get; private set; }
    public int Port { get; private set; }
    public int? GmtPlus { get; private set; }
    public decimal? Latitude { get; private set; }
    public decimal? Longitude { get; private set; }
    public decimal? Rayon { get; private set; }
    public int? TimeToleranceMinute { get; private set; }
    public bool IsActive { get; private set; } = true;
    public SocieteId SocieteId { get; private set; }
    public Societe? Societe { get; private set; }

    private Reseau(
        ReseauId reseauId,
        string ipAddress,
        int port,
        int? gmtPlus,
        decimal? latitude,
        decimal? longitude,
        decimal? rayon,
        int? timeToleranceMinute,
        bool isActive,
        SocieteId societeId)
    {
        ReseauId = reseauId;
        IpAddress = ipAddress;
        Port = port;
        GmtPlus = gmtPlus;
        Latitude = latitude;
        Longitude = longitude;
        Rayon = rayon;
        TimeToleranceMinute = timeToleranceMinute;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Reseau Create(
        ReseauId reseauId,
        string ipAddress,
        int port,
        int? gmtPlus,
        decimal? latitude,
        decimal? longitude,
        decimal? rayon,
        int? timeToleranceMinute,
        bool isActive,
        SocieteId societeId)
    {
        return new Reseau(reseauId, ipAddress, port, gmtPlus, latitude, longitude, rayon, timeToleranceMinute, isActive, societeId);
    }

    public void Update(
        string ipAddress,
        int port,
        int? gmtPlus,
        decimal? latitude,
        decimal? longitude,
        decimal? rayon,
        int? timeToleranceMinute,
        bool isActive)
    {
        IpAddress = ipAddress;
        Port = port;
        GmtPlus = gmtPlus;
        Latitude = latitude;
        Longitude = longitude;
        Rayon = rayon;
        TimeToleranceMinute = timeToleranceMinute;
        IsActive = isActive;
    }

    public static Reseau QueryCreate(
        ReseauId reseauId,
        string ipAddress,
        int port,
        int? gmtPlus,
        decimal? latitude,
        decimal? longitude,
        decimal? rayon,
        int? timeToleranceMinute,
        bool isActive,
        SocieteId societeId)
    {
        return new Reseau(reseauId, ipAddress, port, gmtPlus, latitude, longitude, rayon, timeToleranceMinute, isActive, societeId);
    }

#pragma warning disable CS8618
    private Reseau() { }
#pragma warning restore CS8618
}
