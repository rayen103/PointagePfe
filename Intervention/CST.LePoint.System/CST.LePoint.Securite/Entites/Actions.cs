using System;

namespace CST.LePoint.Securite.Entites
{
    [Flags]
    public enum Actions
    {
        Rien = 0,
        Consulter = 1,
        Ajouter = 2,
        Modifier = 4,
        Supprimer = 8,
        Apercu = 16,
        Imprimer = 32,
        Exporter = 64,
        Rechercher = 128,
        Dupliquer=256
    }
}