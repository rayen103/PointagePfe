namespace CST.LePoint.Securite.Entites
{
    public interface IActionsCommun
    {
        void Actualiser();
    }

    public interface IActionsAjout : IActionsCommun
    {
        void Ajouter();
    }

    public interface IActionsSelectionnerGridRow : IActionsCommun
    {
        void SelectionnerGridRow(bool bHaut);
    }

    public interface IActionsConfiguration : IActionsCommun
    {
        void Configurer();
    }

    public interface IActionsDupliquer : IActionsCommun
    {
        void Dupliquer();
    }

    public interface IActionsEdition : IActionsCommun
    {
        void Apercu();
    }
    public interface IActionsEditionSpecifier : IActionsCommun
    {
        void ApercuStandards();
        void ApercuPreImprimer();
    }

    public interface IActionsEditionListe : IActionsCommun
    {
        void ApercuRecap();
        void ApercuDetaille();
    }

    public interface IActionsExport : IActionsCommun
    {
        void Exporter(string formatCible);
    }

    public interface IActionsImport : IActionsCommun
    {
        void Importer();
    }

    public interface IActionsRechercher : IActionsCommun
    {
        void Rechercher();
    }

    public interface IActionsSave : IActionsCommun
    {
        void Enregistrer(bool enregistrerEtFermer);
    }

    public interface IActionsModification : IActionsCommun
    {
        void Modifier();
    }

    public interface IActionsSuppression : IActionsCommun
    {
        void Supprimer();
    }

    public interface IActionsListe : IActionsAjout, IActionsSelectionnerGridRow, IActionsModification
    {
    }

    public interface IActionsListeSuppression : IActionsAjout, IActionsSelectionnerGridRow, IActionsModification, IActionsSuppression
    {
    }

    public interface IActionsSaveSuppression : IActionsSave, IActionsSuppression
    {
    }
}