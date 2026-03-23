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
            societeId);
    }

    public void Update(
        string numeroIMM,
        string? modelBus,
        string? imei,
        int? capacite,
        string? codeCircuit,
        bool appSagem,
        bool isActive)
    {
        NumeroIMM = numeroIMM;
        ModelBus = modelBus;
        IMEI = imei;
        Capacite = capacite;
        CodeCircuit = codeCircuit;
        AppSagem = appSagem;
        IsActive = isActive;
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
            societeId);
    }

#pragma warning disable CS8618
    private Bus() { }
#pragma warning restore CS8618
}
