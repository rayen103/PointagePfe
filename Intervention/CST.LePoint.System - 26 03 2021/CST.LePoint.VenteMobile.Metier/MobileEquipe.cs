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
    public class MobileEquipe
    {

        #region Proprietes
        [XmlAttribute("user")]
        [Bindable(true)]
        public string user { get; set; }

        [XmlAttribute("codeequipe")]
        [Bindable(true)]
        public string codeequipe { get; set; }
        [XmlAttribute("nom")]
        [Bindable(true)]
        public string nom { get; set; }

        #endregion Proprietes

        public MobileEquipe()
        {

        }

    }

    [Serializable]
    public class EquipeMobileCollection : List<MobileEquipe>
    {
        public EquipeMobileCollection()
        {
        }

        public static EquipeMobileCollection Charger()
        {
            EquipeMobileCollection EquipeMobile = new EquipeMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_equipes_Charger";
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();
                    cmd.Parameters.AddWithValue("@CClient", DBNull.Value);
                    while (reader.Read())
                    {
                        MobileEquipe user = new MobileEquipe();
                        user.user = reader["Matricule"].ToString() + " | " + reader["NomPrenom"].ToString();
                        user.codeequipe = reader["CEquipe"] == DBNull.Value ? "" : reader["CEquipe"].ToString();
                        user.nom = reader["NomPrenom"] == DBNull.Value ? "" : reader["NomPrenom"].ToString();
                        EquipeMobile.Add(user);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return (EquipeMobile);
        }
    }
}