using CollectManagement.Domain.Common;
using CollectManagement.Domain.Modems.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Modems;

public class Modem : AuditableEntity
{
    public ModemId ModemId { get; private set; }

    public string IMEI { get; private set; }

    public string? ModelModem { get; private set; }

    public string? NumeroSim { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Modem(
        ModemId modemId,
        string imei,
        string? modelModem,
        string? numeroSim,
        bool isActive,
        SocieteId societeId)
    {
        ModemId = modemId;
        IMEI = imei;
        ModelModem = modelModem;
        NumeroSim = numeroSim;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Modem Create(
        ModemId modemId,
        string imei,
        string? modelModem,
        string? numeroSim,
        bool isActive,
        SocieteId societeId)
    {
        return new Modem(
            modemId,
            imei,
            modelModem,
            numeroSim,
            isActive,
            societeId);
    }

    public void Update(
        string imei,
        string? modelModem,
        string? numeroSim,
        bool isActive)
    {
        IMEI = imei;
        ModelModem = modelModem;
        NumeroSim = numeroSim;
        IsActive = isActive;
    }

    public static Modem QueryCreate(
        ModemId modemId,
        string imei,
        string? modelModem,
        string? numeroSim,
        bool isActive,
        SocieteId societeId)
    {
        return new Modem(
            modemId,
            imei,
            modelModem,
            numeroSim,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Modem() { }
#pragma warning restore CS8618
}
