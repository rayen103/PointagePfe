using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileTabArticle
    {
        #region Proprietes

        public string codeArt { get; set; }
        public string libArt { get; set; }
        public int qteOt { get; set; }
        public int qteRes { get; set; }
        public int qtePrep { get; set; }
        public bool b { get; set; }
        public string CUnite { get; set; }
        public int gratuite { get; set; }
        public decimal i { get; set; }
        public string ImageArt { get; set; }
        public decimal remise { get; set; }
        public decimal remis { get; set; }
        public bool BGratuit { get; set; }        

        #endregion Proprietes

        public MobileTabArticle()
        {

        }

    }
}