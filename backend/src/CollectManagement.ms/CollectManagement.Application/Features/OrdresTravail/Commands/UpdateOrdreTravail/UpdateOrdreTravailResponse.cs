namespace CollectManagement.Application.Features.OrdresTravail.Commands.UpdateOrdreTravail;

public class UpdateOrdreTravailResponse
{
    public Ulid OrdreTravailId { get; set; }
    public string NumeroOrdreTravail { get; set; } = string.Empty;
    public string? NumeroChantier { get; set; }
    public string? CodeClient { get; set; }
    public string? NumeroBonCommande { get; set; }
    public string? CodeEquipe { get; set; }
    public string? EtatOT { get; set; }
    public decimal? Montant { get; set; }
    public DateTime? DateCreation { get; set; }
    public string? NumeroConvention { get; set; }
    public string? CodeVehicule { get; set; }
    public string? Libelle { get; set; }
    public bool IsActive { get; set; }
}
