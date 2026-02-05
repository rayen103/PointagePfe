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
    public class RattachementMobile
    {
        #region Proprietes

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("COptions")]
        [Bindable(true)]
        public string COptions { get; set; }

        [XmlAttribute("CTypeOptions")]
        [Bindable(true)]
        public string CTypeOptions { get; set; }
        [XmlAttribute("LibOptions")]
        [Bindable(true)]
        public string LibOptions { get; set; }

        [XmlAttribute("LibTypeOptions")]
        [Bindable(true)]
        public string LibTypeOptions { get; set; }

        #endregion Proprietes
        
        public RattachementMobile()
        {

        }
    }

    [Serializable]
    public class RattachementMobileCollection : List<RattachementMobile>
    {
        public RattachementMobileCollection()
        {
        }

        public static RattachementMobileCollection Charger(string nrattachement)
        {
            RattachementMobileCollection PnoteMobile = new RattachementMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Rattachement_article_charger";
                    cmd.Parameters.AddWithValue("@NRattachement", nrattachement);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RattachementMobile note = new RattachementMobile();
                        note.CArticle = reader["CArticle"] == DBNull.Value ? "" : reader["CArticle"].ToString();
                        note.LibArticle = reader["LibArticle"] == DBNull.Value ? "" : reader["LibArticle"].ToString();
                        note.LibTypeOptions = reader["LibTypeOptions"] == DBNull.Value ? "" : reader["LibTypeOptions"].ToString();
                        note.LibOptions = reader["LibOptions"] == DBNull.Value ? "" : reader["LibOptions"].ToString();
                        note.COptions = reader["COptions"] == DBNull.Value ? "" : reader["COptions"].ToString();
                        note.CTypeOptions = reader["CTypeOptions"] == DBNull.Value ? "" : reader["CTypeOptions"].ToString();
                        PnoteMobile.Add(note);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (PnoteMobile);





        }
    }
}
