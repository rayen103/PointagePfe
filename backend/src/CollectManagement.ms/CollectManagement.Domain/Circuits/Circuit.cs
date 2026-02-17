using CollectManagement.Domain.Common;
using CollectManagement.Domain.Circuits.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Circuits;

public class Circuit : AuditableEntity
{
    public CircuitId CircuitId { get; private set; }
    
    public string CodeCircuit { get; private set; }
    
    public string? LibelleCircuit { get; private set; }
    
    public string? Description { get; private set; }
    
    public double? Latitude { get; private set; }
    
    public double? Longitude { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private Circuit(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId,
        double? latitude = null,
        double? longitude = null)
    {
        CircuitId = circuitId;
        CodeCircuit = codeCircuit;
        LibelleCircuit = libelleCircuit;
        Description = description;
        IsActive = isActive;
        SocieteId = societeId;
        Latitude = latitude;
        Longitude = longitude;
    }

    public static Circuit Create(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId,
        double? latitude = null,
        double? longitude = null)
    {
        return new Circuit(
            circuitId,
            codeCircuit,
            libelleCircuit,
            description,
            isActive,
            societeId,
            latitude,
            longitude);
    }

    public void Update(
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        double? latitude = null,
        double? longitude = null)
    {
        CodeCircuit = codeCircuit;
        LibelleCircuit = libelleCircuit;
        Description = description;
        IsActive = isActive;
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public static Circuit QueryCreate(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId,
        double? latitude = null,
        double? longitude = null)
    {
        return new Circuit(
            circuitId,
            codeCircuit,
            libelleCircuit,
            description,
            isActive,
            societeId,
            latitude,
            longitude);
    }

#pragma warning disable CS8618
    private Circuit() { }
#pragma warning restore CS8618
}
