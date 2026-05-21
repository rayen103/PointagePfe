using CollectManagement.Domain.Common;
using CollectManagement.Domain.Gouvernorats.ValueObjects;
using CollectManagement.Domain.Societes;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Gouvernorats;

public class Gouvernorat : AuditableEntity
{
    public GouvernoratId GouvernoratId { get; private set; }

    public string CodeGouvernorat { get; private set; }

    public string? LibelleGouvernorat { get; private set; }

    public bool IsActive { get; private set; } = true;

    public SocieteId SocieteId { get; private set; }

    public Societe? Societe { get; private set; }

    private Gouvernorat(
        GouvernoratId gouvernoratId,
        string codeGouvernorat,
        string? libelleGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        GouvernoratId = gouvernoratId;
        CodeGouvernorat = codeGouvernorat;
        LibelleGouvernorat = libelleGouvernorat;
        IsActive = isActive;
        SocieteId = societeId;
    }

    public static Gouvernorat Create(
        GouvernoratId gouvernoratId,
        string codeGouvernorat,
        string? libelleGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        return new Gouvernorat(
            gouvernoratId,
            codeGouvernorat,
            libelleGouvernorat,
            isActive,
            societeId);
    }

    public void Update(
        string codeGouvernorat,
        string? libelleGouvernorat,
        bool isActive)
    {
        CodeGouvernorat = codeGouvernorat;
        LibelleGouvernorat = libelleGouvernorat;
        IsActive = isActive;
    }

    public static Gouvernorat QueryCreate(
        GouvernoratId gouvernoratId,
        string codeGouvernorat,
        string? libelleGouvernorat,
        bool isActive,
        SocieteId societeId)
    {
        return new Gouvernorat(
            gouvernoratId,
            codeGouvernorat,
            libelleGouvernorat,
            isActive,
            societeId);
    }

#pragma warning disable CS8618
    private Gouvernorat() { }
#pragma warning restore CS8618
}
