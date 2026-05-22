using CollectManagement.Domain.Common;

namespace CollectManagement.Domain.Utilisateurs.Enums;

public enum NavigationAction
{
    [DisplayAj(Name = "Consulter")]
    View,
    [DisplayAj(Name = "Ajouter")]
    Add,
    [DisplayAj(Name = "Modifier")]
    Edit,
    [DisplayAj(Name = "Supprimer")]
    Delete,
    [DisplayAj(Name = "Aperçu")]
    Preview,
    [DisplayAj(Name = "Imprimer")]
    Print,
    [DisplayAj(Name = "Exporter")]
    Export,
    [DisplayAj(Name = "Rechercher")]
    Search,
    [DisplayAj(Name = "Dupliquer")]
    Duplicate,
}