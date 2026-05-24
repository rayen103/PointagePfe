using CollectManagement.Domain.Common;
using CollectManagement.Domain.Societes.ValueObjects;

namespace CollectManagement.Domain.Societes;

public class Societe : AuditableEntity
{
    public SocieteId SocieteId { get; private set; }
    
    public string? LogoPath { get; private set; }

    public string Nom { get; private set; }
    
    public string? Initiales { get; private set; }

    public string? Tva { get; private set; }

    public string? Rc { get; private set; }

    public string? MatriculeFiscal { get; private set; }

    public string? Rne { get; private set; }

    public decimal? Capital { get; private set; }

    public DateTime DateOverture { get; private set; }
    
    public string? Telephone1 { get; private set; }

    public string? Telephone2 { get; private set; }

    public string? Fax1 { get; private set; }
    
    public string? Fax2 { get; private set; }
    
    public string? Email { get; private set; }
    
    public string? Adresse { get; private set; }

    public string? CodePostal { get; private set; }

    public string? Ville { get; private set; }

    public string? Pays { get; private set; }
    
    public string? CodeSociete { get; private set; }


    private Societe(
        SocieteId societeId,
        string? logoPath,
        string nom,
        string? initiales,
        string? tva,
        string? rc,
        string? matriculeFiscal,
        string? rne,
        decimal? capital,
        DateTime dateOverture,
        string? telephone1,
        string? telephone2,
        string? fax1,
        string? fax2,
        string? email,
        string? adresse,
        string? codePostal,
        string? ville,
        string? pays,
        string? codeSociete)
    {
        SocieteId = societeId;
        LogoPath = logoPath;
        Nom = nom;
        Initiales = initiales;
        Tva = tva;
        Rc = rc;
        MatriculeFiscal = matriculeFiscal;
        Rne = rne;
        Capital = capital;
        DateOverture = dateOverture;
        Telephone1 = telephone1;
        Telephone2 = telephone2;
        Fax1 = fax1;
        Fax2 = fax2;
        Email = email;
        Adresse = adresse;
        CodePostal = codePostal;
        Ville = ville;
        Pays = pays;
        CodeSociete = codeSociete;
    }

    public static Societe Create(
        SocieteId societeId,
        string? logoPath,
        string nom,
        string? initiales,
        string? tva,
        string? rc,
        string? matriculeFiscal,
        string? rne,
        decimal? capital,
        DateTime dateOverture,
        string? telephone1,
        string? telephone2,
        string? fax1,
        string? fax2,
        string? email,
        string? adresse,
        string? codePostal,
        string? ville,
        string? pays,
        string? codeSociete)
    {
        return new Societe(
            societeId,
            logoPath,
            nom,
            initiales,
            tva,
            rc,
            matriculeFiscal,
            rne,
            capital,
            dateOverture,
            telephone1,
            telephone2,
            fax1,
            fax2,
            email,
            adresse,
            codePostal,
            ville,
            pays,
            codeSociete);
    }

    public void Update(
        string? logoPath,
        string nom,
        string? initiales,
        string? tva,
        string? rc,
        string? matriculeFiscal,
        string? rne,
        decimal? capital,
        DateTime dateOverture,
        string? telephone1,
        string? telephone2,
        string? fax1,
        string? fax2,
        string? email,
        string? adresse,
        string? codePostal,
        string? ville,
        string? pays,
        string? codeSociete
    )
    {
        LogoPath = logoPath;
        Nom = nom;
        Initiales = initiales;
        Tva = tva;
        Rc = rc;
        MatriculeFiscal = matriculeFiscal;
        Rne = rne;
        Capital = capital;
        DateOverture = dateOverture;
        Telephone1 = telephone1;
        Telephone2 = telephone2;
        Fax1 = fax1;
        Fax2 = fax2;
        Email = email;
        Adresse = adresse;
        CodePostal = codePostal;
        Ville = ville;
        Pays = pays;
        CodeSociete = codeSociete;
    }
    
    public static Societe QueryCreate(
        SocieteId societeId,
        string? logoPath,
        string nom,
        string? initiales,
        string? tva,
        string? rc,
        string? matriculeFiscal,
        string? rne,
        decimal? capital,
        DateTime dateOverture,
        string? telephone1,
        string? telephone2,
        string? fax1,
        string? fax2,
        string? email,
        string? adresse,
        string? codePostal,
        string? ville,
        string? pays,
        string? codeSociete)
    {
        return new Societe(
            societeId, 
            logoPath,
            nom,
            initiales,
            tva,
            rc,
            matriculeFiscal,
            rne,
            capital,
            dateOverture,
            telephone1,
            telephone2,
            fax1,
            fax2,
            email,
            adresse,
            codePostal,
            ville,
            pays,
            codeSociete
        );
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Societe() { }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}