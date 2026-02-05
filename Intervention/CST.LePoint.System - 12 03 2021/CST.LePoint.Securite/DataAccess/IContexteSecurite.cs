using System.Collections.Generic;

namespace CST.LePoint.Securite.DataAccess
{
    public interface IContexteSecurite
    {
        void Enregistrer();

        void Charger();

        ICollection<T> Set<T>() where T : class, new();
    }
}