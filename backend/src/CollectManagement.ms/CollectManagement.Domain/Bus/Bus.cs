using CollectManagement.Domain.Bus.ValueObjects;
using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Bus;

public class Bus : AuditableEntity
{
    public BusId BusId { get; private set; }

    public string NumeroIMM { get; private set; }

    public string? ModelBus { get; private set; }

    public string? IMEI { get; private set; }

    public int? Capacite { get; private set; }

    public string? CodeCircuit { get; private set; }

    public string? CodeChauffeur { get; private set; }

    public bool AppSagem { get; private set; }

    public bool IsActive { get; private set; } = true;

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public int CurrentOccupancy { get; private set; }

    public DateTime? LastPositionAt { get; private set; }

    public DateTime? LastOccupancyUpdateAt { get; private set; }

    public DateTime? DeviceRecordedAtUtc { get; private set; }

    public int? BatteryPercentage { get; private set; }

    public double? BatteryVoltage { get; private set; }

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Bus(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        string? codeChauffeur,
        bool appSagem,
        bool isActive,
        double? latitude,
        double? longitude,
        int currentOccupancy,
        DateTime? lastPositionAt,
        DateTime? lastOccupancyUpdateAt,
        DateTime? deviceRecordedAtUtc,
        int? batteryPercentage,
        double? batteryVoltage,
        SocieteId societeId)
    {
        BusId = busId;
        NumeroIMM = numeroIMM;
        ModelBus = modelBus;
        IMEI = imei;
        Capacite = capacite;
        CodeCircuit = codeCircuit;
        CodeChauffeur = codeChauffeur;
        AppSagem = appSagem;
        IsActive = isActive;
        Latitude = latitude;
        Longitude = longitude;
        CurrentOccupancy = currentOccupancy;
        LastPositionAt = lastPositionAt;
        LastOccupancyUpdateAt = lastOccupancyUpdateAt;
        DeviceRecordedAtUtc = deviceRecordedAtUtc;
        BatteryPercentage = batteryPercentage;
        BatteryVoltage = batteryVoltage;
        SocieteId = societeId;
    }

    public static Bus Create(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        string? codeChauffeur,
        bool appSagem,
        bool isActive,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        return new Bus(
            busId,
            numeroIMM,
            modelBus,
            imei,
            capacite,
            codeCircuit,
            codeChauffeur,
            appSagem,
            isActive,
            latitude,
            longitude,
            0,
            null,
            null,
            null,
            null,
            null,
            societeId);
    }

    public void Update(
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        string? codeChauffeur,
        bool appSagem,
        bool isActive,
        double? latitude,
        double? longitude)
    {
        NumeroIMM = numeroIMM;
        ModelBus = modelBus;
        IMEI = imei;
        Capacite = capacite;
        CodeCircuit = codeCircuit;
        CodeChauffeur = codeChauffeur;
        AppSagem = appSagem;
        IsActive = isActive;
        Latitude = latitude;
        Longitude = longitude;
    }

    public void AssignChauffeur(string? codeChauffeur)
    {
        CodeChauffeur = codeChauffeur;
    }

    public void UpdateRuntimeState(
        string? imei,
        double? latitude,
        double? longitude,
        int? occupancy,
        DateTime? deviceRecordedAtUtc,
        int? batteryPercentage,
        double? batteryVoltage,
        DateTime eventDateUtc)
    {
        if (!string.IsNullOrWhiteSpace(imei))
            IMEI = imei;

        if (latitude is not null)
            Latitude = latitude;

        if (longitude is not null)
            Longitude = longitude;

        if (occupancy is not null)
        {
            CurrentOccupancy = Math.Max(0, occupancy.Value);
            LastOccupancyUpdateAt = eventDateUtc;
        }

        if (deviceRecordedAtUtc is not null)
            DeviceRecordedAtUtc = deviceRecordedAtUtc;

        if (batteryPercentage is not null)
            BatteryPercentage = batteryPercentage;

        if (batteryVoltage is not null)
            BatteryVoltage = batteryVoltage;

        LastPositionAt = eventDateUtc;
    }

    public void Empty(DateTime eventDateUtc)
    {
        CurrentOccupancy = 0;
        LastOccupancyUpdateAt = eventDateUtc;
    }

    public static Bus QueryCreate(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        string? codeChauffeur,
        bool appSagem,
        bool isActive,
        double? latitude,
        double? longitude,
        int currentOccupancy,
        DateTime? lastPositionAt,
        DateTime? lastOccupancyUpdateAt,
        DateTime? deviceRecordedAtUtc,
        int? batteryPercentage,
        double? batteryVoltage,
        SocieteId societeId)
    {
        return new Bus(
            busId,
            numeroIMM,
            modelBus,
            imei,
            capacite,
            codeCircuit,
            codeChauffeur,
            appSagem,
            isActive,
            latitude,
            longitude,
            currentOccupancy,
            lastPositionAt,
            lastOccupancyUpdateAt,
            deviceRecordedAtUtc,
            batteryPercentage,
            batteryVoltage,
            societeId);
    }

#pragma warning disable CS8618
    private Bus() { }
#pragma warning restore CS8618
}
