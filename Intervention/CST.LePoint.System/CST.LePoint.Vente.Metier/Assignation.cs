
using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Assignation : Item
    {
        //[XmlAttribute("CArticle")]
        //[Bindable(true)]
        //public string CArticle { get; set; }

        //[XmlAttribute("Libelle")]
        //[Bindable(true)]
        //public string Libelle { get; set; }

        [XmlAttribute("Emballage")]
        [Bindable(true)]
        public string Emballage { get; set; }

        //[XmlAttribute("Quantite")]
        //[Bindable(true)]
        //public string Quantite { get; set; }

        public Assignation()
        { }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                Assignation ancienne = Assignation.Charger(this.Libelle, this.Emballage);
                if (ancienne != null)
                    ancienne.Supprimer();
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Assignation_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CArticle ", Code);
                    cmd.Parameters.AddWithValue("@Libelle", Libelle);
                    cmd.Parameters.AddWithValue("@Emballage", this.Emballage);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null) parametre.Value = DBNull.Value;
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Assignation_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticle ", Code);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public static Assignation Charger(string cArticle)
        {
            Assignation assignation = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Assignation_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@Libelle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Emballage", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            assignation = new Assignation();
                            assignation.Code = dr["CArticle"].ToString();
                            if (dr["Libelle"] != DBNull.Value)
                                assignation.Libelle = dr["Libelle"].ToString();
                            if (dr["Emballage"] != DBNull.Value)
                                assignation.Emballage = dr["Emballage"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return assignation;
        }

        public static Assignation Charger(string libelle, string emballage)
        {
            Assignation assignation = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Assignation_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Libelle", libelle);
                    cmd.Parameters.AddWithValue("@Emballage", emballage);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            assignation = new Assignation();
                            assignation.Code = dr["CArticle"].ToString();
                            if (dr["Libelle"] != DBNull.Value)
                                assignation.Libelle = dr["Libelle"].ToString();
                            if (dr["Emballage"] != DBNull.Value)
                                assignation.Emballage = dr["Emballage"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return assignation;
        }
    }

    public class AssignationCollection : ItemCollection
    {
    }
}