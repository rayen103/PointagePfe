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
    public class MobilePaimentModalite
    {
        #region Proprietes

        [XmlAttribute("CModeReglement")]
        [Bindable(true)]
        public string CModeReglement { get; set; }

        [XmlAttribute("LibModeReglement")]
        [Bindable(true)]
        public string LibModeReglement { get; set; }

        [XmlAttribute("Selected")]
        [Bindable(true)]
        public string Selected { get; set; }

        #endregion Proprietes

        public MobilePaimentModalite()
        {

        }

        [Serializable]
        public class PaimentModaliteCollection : List<MobilePaimentModalite>
        {
            public PaimentModaliteCollection()
            {
            }
        }

        public static PaimentModaliteCollection Charger(string reg, string CClient)
        {
            PaimentModaliteCollection modalites = new PaimentModaliteCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_modalites_Charger";
                    cmd.Parameters.AddWithValue("@reg", reg);
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobilePaimentModalite modalite = new MobilePaimentModalite();
                        modalite.CModeReglement = reader["CModeReglement"] == DBNull.Value ? "" : reader["CModeReglement"].ToString();
                        modalite.LibModeReglement = reader["LibModeReglement"] == DBNull.Value ? "" : reader["LibModeReglement"].ToString();
                        modalite.Selected = reader["Selected"] == DBNull.Value ? "false" : "true";

                        modalites.Add(modalite);

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (modalites);
        }
    }
}