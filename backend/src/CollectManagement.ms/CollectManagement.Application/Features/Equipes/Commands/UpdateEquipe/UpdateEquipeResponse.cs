namespace CollectManagement.Application.Features.Equipes.Commands.UpdateEquipe;

public class UpdateEquipeResponse
{
    public Ulid EquipeId { get; set; }
    public string CodeEquipe { get; set; } = string.Empty;
    public string? LibelleEquipe { get; set; }
    public string? CodeClient { get; set; }
    public string? CodeEntrepot { get; set; }
    public string? CodeTarif { get; set; }
    public string? CodeFournisseur { get; set; }
    public string? Responsable { get; set; }
    public bool IsInternal { get; set; }
    public string? CodeVehicule { get; set; }
    public bool IsActive { get; set; }
}
