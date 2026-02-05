using CST.LePoint.Referentiel;
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

    [Serializable]
    public class MobileClientVisite
    {
        #region Propriétés

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }


        [XmlAttribute("NumeroTelephone1")]
        [Bindable(true)]
        public string NumeroTelephone1 { get; set; }


        [XmlAttribute("NumeroTelephone2")]
        [Bindable(true)]
        public string NumeroTelephone2 { get; set; }
        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }
        #endregion Propriétés

        public MobileClientVisite()
        {
        }





        public static MobileClientVisite Charger(string CClient)
        {
            MobileClientVisite equipe = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Client_Charger";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            equipe = new MobileClientVisite();
                            equipe.CClient = dr["CClient"].ToString();
                            equipe.RaisonSociale = dr["RaisonSociale"].ToString();
                            equipe.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            equipe.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            equipe.NBonCommande = "";
                           
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return equipe;
        }
    }








    

}
