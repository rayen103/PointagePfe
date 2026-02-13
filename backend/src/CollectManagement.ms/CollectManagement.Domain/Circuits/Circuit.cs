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
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private Circuit(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId)
    {
        CircuitId = circuitId;
        CodeCircuit = codeCircuit;
        LibelleCircuit = libelleCircuit;
        Description = description;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Circuit Create(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId)
    {
        return new Circuit(
            circuitId,
            codeCircuit,
            libelleCircuit,
            description,
            isActive,
            societeId);
    }

    public void Update(
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive)
    {
        CodeCircuit = codeCircuit;
        LibelleCircuit = libelleCircuit;
        Description = description;
        IsActive = isActive;
    }
    
    public static Circuit QueryCreate(
        CircuitId circuitId,
        string codeCircuit,
        string? libelleCircuit,
        string? description,
        bool isActive,
        SocieteId societeId)
    {
        return new Circuit(
            circuitId,
            codeCircuit,
            libelleCircuit,
            description,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Circuit() { }
#pragma warning restore CS8618
}
