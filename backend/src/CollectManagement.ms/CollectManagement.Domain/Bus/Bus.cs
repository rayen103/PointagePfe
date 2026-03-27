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

    public bool AppSagem { get; private set; }

    public bool IsActive { get; private set; } = true;

    public double? Latitude { get; private set; }

    public double? Longitude { get; private set; }

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Bus(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        bool appSagem,
        bool isActive,
        double? latitude,
        double? longitude,
        SocieteId societeId)
    {
        BusId = busId;
        NumeroIMM = numeroIMM;
        ModelBus = modelBus;
        IMEI = imei;
        Capacite = capacite;
        CodeCircuit = codeCircuit;
        AppSagem = appSagem;
        IsActive = isActive;
        Latitude = latitude;
        Longitude = longitude;
        SocieteId = societeId;
    }

    public static Bus Create(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
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
            appSagem,
            isActive,
            latitude,
            longitude,
            societeId);
    }

    public void Update(
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
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
        AppSagem = appSagem;
        IsActive = isActive;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Bus QueryCreate(
        BusId busId,
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
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
            appSagem,
            isActive,
            latitude,
            longitude,
            societeId);
    }

#pragma warning disable CS8618
    private Bus() { }
#pragma warning restore CS8618
}
