using CollectManagement.Domain.Common;
using CollectManagement.Domain.Interventions.ValueObjects;

namespace CollectManagement.Domain.Interventions;

public class Intervention : AuditableEntity
{
    public InterventionId InterventionId { get; private set; }
    
    public string NumeroIntervention { get; private set; }
    
    public string? Description { get; private set; }
    
    public DateTime DateIntervention { get; private set; }
    
    public string? TypeIntervention { get; private set; }
    
    public string? Statut { get; private set; }
    
    public decimal? Cout { get; private set; }

    private Intervention(
        InterventionId interventionId,
        string numeroIntervention,
        string? description,
        DateTime dateIntervention,
        string? typeIntervention,
        string? statut,
        decimal? cout)
    {
        InterventionId = interventionId;
        NumeroIntervention = numeroIntervention;
        Description = description;
        DateIntervention = dateIntervention;
        TypeIntervention = typeIntervention;
        Statut = statut;
        Cout = cout;
    }

    public static Intervention Create(
        InterventionId interventionId,
        string numeroIntervention,
        string? description,
        DateTime dateIntervention,
        string? typeIntervention,
        string? statut,
        decimal? cout)
    {
        return new Intervention(
            interventionId,
            numeroIntervention,
            description,
            dateIntervention,
            typeIntervention,
            statut,
            cout);
    }

    public void Update(
        string numeroIntervention,
        string? description,
        DateTime dateIntervention,
        string? typeIntervention,
        string? statut,
        decimal? cout)
    {
        NumeroIntervention = numeroIntervention;
        Description = description;
        DateIntervention = dateIntervention;
        TypeIntervention = typeIntervention;
        Statut = statut;
        Cout = cout;
    }

    public static Intervention QueryCreate(
        InterventionId interventionId,
        string numeroIntervention,
        string? description,
        DateTime dateIntervention,
        string? typeIntervention,
        string? statut,
        decimal? cout)
    {
        return new Intervention(
            interventionId,
            numeroIntervention,
            description,
            dateIntervention,
            typeIntervention,
            statut,
            cout);
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Intervention() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
