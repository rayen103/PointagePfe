using CollectManagement.Domain.Common;
using CollectManagement.Domain.Equipes.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Equipes;

public class Equipe : AuditableEntity
{
    public EquipeId EquipeId { get; private set; }
    
    public string CodeEquipe { get; private set; }
    
    public string? LibelleEquipe { get; private set; }
    
    public string? CodeClient { get; private set; }
    
    public string? CodeEntrepot { get; private set; }
    
    public string? CodeTarif { get; private set; }
    
    public string? CodeFournisseur { get; private set; }
    
    public string? Responsable { get; private set; }
    
    public bool IsInternal { get; private set; }
    
    public string? CodeVehicule { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    
    public SocieteId SocieteId { get; private set; }
    
    public Societe? Societe { get; private set; }

    private Equipe(
        EquipeId equipeId,
        string codeEquipe,
        string? libelleEquipe,
        string? codeClient,
        string? codeEntrepot,
        string? codeTarif,
        string? codeFournisseur,
        string? responsable,
        bool isInternal,
        string? codeVehicule,
        bool isActive,
        SocieteId societeId)
    {
        EquipeId = equipeId;
        CodeEquipe = codeEquipe;
        LibelleEquipe = libelleEquipe;
        CodeClient = codeClient;
        CodeEntrepot = codeEntrepot;
        CodeTarif = codeTarif;
        CodeFournisseur = codeFournisseur;
        Responsable = responsable;
        IsInternal = isInternal;
        CodeVehicule = codeVehicule;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Equipe Create(
        EquipeId equipeId,
        string codeEquipe,
        string? libelleEquipe,
        string? codeClient,
        string? codeEntrepot,
        string? codeTarif,
        string? codeFournisseur,
        string? responsable,
        bool isInternal,
        string? codeVehicule,
        bool isActive,
        SocieteId societeId)
    {
        return new Equipe(
            equipeId,
            codeEquipe,
            libelleEquipe,
            codeClient,
            codeEntrepot,
            codeTarif,
            codeFournisseur,
            responsable,
            isInternal,
            codeVehicule,
            isActive,
            societeId);
    }

    public void Update(
        string codeEquipe,
        string? libelleEquipe,
        string? codeClient,
        string? codeEntrepot,
        string? codeTarif,
        string? codeFournisseur,
        string? responsable,
        bool isInternal,
        string? codeVehicule,
        bool isActive)
    {
        CodeEquipe = codeEquipe;
        LibelleEquipe = libelleEquipe;
        CodeClient = codeClient;
        CodeEntrepot = codeEntrepot;
        CodeTarif = codeTarif;
        CodeFournisseur = codeFournisseur;
        Responsable = responsable;
        IsInternal = isInternal;
        CodeVehicule = codeVehicule;
        IsActive = isActive;
    }
    
    public static Equipe QueryCreate(
        EquipeId equipeId,
        string codeEquipe,
        string? libelleEquipe,
        string? codeClient,
        string? codeEntrepot,
        string? codeTarif,
        string? codeFournisseur,
        string? responsable,
        bool isInternal,
        string? codeVehicule,
        bool isActive,
        SocieteId societeId)
    {
        return new Equipe(
            equipeId,
            codeEquipe,
            libelleEquipe,
            codeClient,
            codeEntrepot,
            codeTarif,
            codeFournisseur,
            responsable,
            isInternal,
            codeVehicule,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Equipe() { }
#pragma warning restore CS8618
}
