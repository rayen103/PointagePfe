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

namespace CST.LePoint.Tiers.Referentiel
{
     [Serializable]
   public class Ref_Devise : Item
    {

        #region Propriétés

        [XmlAttribute("CDevise")]
        [Bindable(true)]
        public string CDevise { get; set; }

        [XmlAttribute("LibDevise")]
        [Bindable(true)]
        public string LibDevise { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("TauxDevise")]
        [Bindable(true)]
        public decimal TauxDevise { get; set; }

        #endregion Propriétés

        public Ref_Devise()
        { }

        public Ref_Devise(string CDevise)
            : this()
        {
            Code = CDevise;
        }

        public Ref_Devise(string CDevise, string LibDevise)
        {
            Code = CDevise;
            Libelle = LibDevise;
        }


        /// <summary>
        /// Ajout ou modification d'un Pays
        /// </summary>
        public void Sauvegarder()
        {
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;

                    cmd.CommandText = "Ref_Devise_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CDevise", Code);
                    cmd.Parameters.AddWithValue("@LibDevise", Libelle);
                    cmd.Parameters.AddWithValue("@TauxDevise", TauxDevise);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);

                    foreach (SqlParameter oleDbParametre in cmd.Parameters)
                    {
                        if (oleDbParametre.Value == null)
                        {
                            oleDbParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Suppression d'un Pays
        /// </summary>
        public static bool Supprimer(String CDevise)
        {
            var carton = false;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Devise_Supprimer";
                    cmd.Parameters.AddWithValue("@CDevise", CDevise);

                    foreach (SqlParameter oleDbParametre in cmd.Parameters)
                    {
                        if (oleDbParametre.Value == null)
                        {
                            oleDbParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    carton = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return carton;
        }

        /// <summary>
        /// Récupération de tous les Pays
        /// </summary>
        public static DataTable RecupererDevise()
        {
            var DTDevise = new DataTable();
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Devise_RecupererListe";
                    var Adapter = new SqlDataAdapter(cmd);
                    Adapter.Fill(DTDevise);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DTDevise;
        }

        /// <summary>
        /// Récupération d'un Pays par identifiant
        /// </summary>
        public static Ref_Devise Charger(String CDevise)
        {
            Ref_Devise devise = null;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Devise_Charger";
                    cmd.Parameters.AddWithValue("@CDevise", CDevise);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            devise = new Ref_Devise();
                            if (reader["CDevise"] != DBNull.Value)
                                devise.Code = reader["CDevise"].ToString();
                            if (reader["LibDevise"] != DBNull.Value)
                                devise.Libelle = reader["LibDevise"].ToString();
                            if (reader["TauxDevise"] != DBNull.Value)
                                devise.TauxDevise = decimal.Parse(reader["TauxDevise"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return devise;
        }

    }

     [Serializable]
     public class Ref_DeviseCollection : ItemCollection
     {
         public Ref_DeviseCollection()
         {
         }

         public static DataSet ChargerVue()
         {
             DataSet ds = new DataSet();

             using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
             {
                 cn.Open();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = cn;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "Ref_Devise_Rpt_Charger";
                 cmd.Parameters.AddWithValue("@CDevise", DBNull.Value);

                 foreach (SqlParameter parametre in cmd.Parameters)
                 {
                     if (parametre.Value == null)
                     {
                         parametre.Value = DBNull.Value;
                     }
                 }
                 SqlDataAdapter sda = new SqlDataAdapter(cmd);
                 sda.Fill(ds, "Ref_Devise_Rpt_Charger");
             }
             return (ds);
         }

         public static DataTable RemplirGrid()
         {
             DataTable dt = new DataTable();

             using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
             {
                 cn.Open();
                 SqlCommand cmd = new SqlCommand();
                 cmd.Connection = cn;
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.CommandText = "Ref_Devise_Charger";
                 cmd.Parameters.AddWithValue("@CDevise", DBNull.Value);

                 foreach (SqlParameter parametre in cmd.Parameters)
                 {
                     if (parametre.Value == null)
                     {
                         parametre.Value = DBNull.Value;
                     }
                 }
                 SqlDataAdapter sda = new SqlDataAdapter(cmd);
                 sda.Fill(dt);
             }
             return (dt);
         }

         public static Ref_DeviseCollection Charger()
         {
             Ref_DeviseCollection collection = new Ref_DeviseCollection();
             Ref_Devise devise = null;

             try
             {
                 using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                 {
                     cn.Open();

                     SqlCommand cmd = cn.CreateCommand();
                     cmd.CommandType = CommandType.StoredProcedure;
                     cmd.CommandText = "Ref_Devise_Charger";
                     cmd.Parameters.Add(new SqlParameter("@CDevise", DBNull.Value));

                     using (SqlDataReader dr = cmd.ExecuteReader())
                     {
                         while (dr.Read())
                         {
                             devise = new Ref_Devise();
                             if (dr["CDevise"] != DBNull.Value)
                                 devise.Code = dr["CDevise"].ToString();
                             if (dr["LibDevise"] != DBNull.Value)
                                 devise.Libelle = dr["LibDevise"].ToString();
                             if (dr["TauxDevise"] != DBNull.Value)
                                 devise.TauxDevise = decimal.Parse(dr["TauxDevise"].ToString());
                             collection.Add(devise);
                         }
                         dr.Close();
                     }
                 }
             }
             catch (Exception)
             {
                 throw;
             }

             return collection;
         }
     }
}
