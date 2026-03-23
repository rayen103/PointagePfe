using CollectManagement.Domain.Common;
using CollectManagement.Domain.Circuits.ValueObjects;

namespace CollectManagement.Domain.Circuits;

public class CircuitPointCollecte : AuditableEntity
{
    public CircuitPointCollecteId CircuitPointCollecteId { get; private set; }

    public CircuitId CircuitId { get; private set; }

    public string CodePointCollecte { get; private set; }

    public string? LibellePointCollecte { get; private set; }

    public decimal? Latitude { get; private set; }

    public decimal? Longitude { get; private set; }

    public int? Ordre { get; private set; }

    private CircuitPointCollecte(
        CircuitPointCollecteId circuitPointCollecteId,
        CircuitId circuitId,
        string codePointCollecte,
        string? libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        int? ordre)
    {
        CircuitPointCollecteId = circuitPointCollecteId;
        CircuitId = circuitId;
        CodePointCollecte = codePointCollecte;
        LibellePointCollecte = libellePointCollecte;
        Latitude = latitude;
        Longitude = longitude;
        Ordre = ordre;
    }

    public static CircuitPointCollecte Create(
        CircuitPointCollecteId circuitPointCollecteId,
        CircuitId circuitId,
        string codePointCollecte,
        string? libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        int? ordre)
    {
        return new CircuitPointCollecte(
            circuitPointCollecteId,
            circuitId,
            codePointCollecte,
            libellePointCollecte,
            latitude,
            longitude,
            ordre);
    }

    public void Update(
        string codePointCollecte,
        string? libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        int? ordre)
    {
        CodePointCollecte = codePointCollecte;
        LibellePointCollecte = libellePointCollecte;
        Latitude = latitude;
        Longitude = longitude;
        Ordre = ordre;
    }

    public static CircuitPointCollecte QueryCreate(
        CircuitPointCollecteId circuitPointCollecteId,
        CircuitId circuitId,
        string codePointCollecte,
        string? libellePointCollecte,
        decimal? latitude,
        decimal? longitude,
        int? ordre)
    {
        return new CircuitPointCollecte(
            circuitPointCollecteId,
            circuitId,
            codePointCollecte,
            libellePointCollecte,
            latitude,
            longitude,
            ordre);
    }

#pragma warning disable CS8618
    private CircuitPointCollecte() { }
#pragma warning restore CS8618
}
