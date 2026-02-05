using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileNote
    {
        #region Proprietes

        [XmlAttribute("Cnote")]
        [Bindable(true)]
        public string Cnote { get; set; }

        [XmlAttribute("Libnote")]
        [Bindable(true)]
        public string Libnote { get; set; }

        #endregion Proprietes

        public MobileNote()
        {

        }

    }

    [Serializable]
    public class noteMobileCollection : List<MobileNote>
    {
        public noteMobileCollection()
        {
        }

        public static noteMobileCollection Charger(string type)
        {
            noteMobileCollection PnoteMobile = new noteMobileCollection();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Options_chargerType";
                    cmd.Parameters.AddWithValue("@CTypeOptions", type);              
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        MobileNote note = new MobileNote();
                        note.Cnote = reader["COptions"] == DBNull.Value ? "" : reader["COptions"].ToString();
                        note.Libnote = reader["LibOptions"] == DBNull.Value ? "" : reader["LibOptions"].ToString();
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

        public static MobileNote Charger_Options(string COption)
        {
            MobileNote note = new MobileNote();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader reader;
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Options_Charger";
                    cmd.Parameters.AddWithValue("@COptions", COption);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        note.Cnote = reader["COptions"] == DBNull.Value ? "" : reader["COptions"].ToString();
                        note.Libnote = reader["LibOptions"] == DBNull.Value ? "" : reader["LibOptions"].ToString();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return (note);
        }

    }
}