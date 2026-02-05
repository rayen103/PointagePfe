using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.Referentiel
{
    public class DetailSaisie : Item
    {
        public decimal Quantite { get; set; }
        public decimal QuantiteHistorique { get; set; }

        public DetailSaisie()
        {
            this.Code = string.Empty;
            this.Libelle = string.Empty;
            this.Quantite = 0;
            this.QuantiteHistorique = 0;
        }
    }

    public class DetailSaisieCollection : List<DetailSaisie>
    {
    }

}
