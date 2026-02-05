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
    public class Ville : Item
    {
        #region Propriétés

       

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

        #endregion Propriétés

        /// <summary>
        /// Constructeur Ville
        /// </summary>
        public Ville()
        { }

        public Ville(string cVille)
            : this()
        {
            Code = cVille;
        }

        public Ville(string cVille, string libVille)
        {
            Code = cVille;
            Libelle = libVille;
        }

        /// <summary>
        /// Ajout ou modification d'une Ville
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

                    cmd.CommandText = "Ref_Ville_Sauvegarder";
                    cmd.Parameters.AddWithValue("CVille", Code);
                    cmd.Parameters.AddWithValue("LibVille", Libelle);
   
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
        /// Suppression d'une Ville
        /// </summary>
        public static bool Supprimer(String cville)
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
                    cmd.CommandText = "Ref_Ville_Supprimer";
                    cmd.Parameters.AddWithValue("CVille", cville);

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
        /// Récupération de tous les Villes
        /// </summary>
        public static DataTable RecupererVille()
        {
            var DTVille = new DataTable();
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Ville_RecupererListe";
                    var Adapter = new SqlDataAdapter(cmd);
                    Adapter.Fill(DTVille);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return DTVille;
        }

        /// <summary>
        /// Récupération d'une Ville par identifiant
        /// </summary>
        public static Ville Charger(String cVille)
        {
            Ville ville = null;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Ville_Charger";
                    cmd.Parameters.AddWithValue("CVille", cVille);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ville = new Ville();
                            if (reader["CVille"] != DBNull.Value)
                                ville.Code = reader["CVille"].ToString();
                            if (reader["LibVille"] != DBNull.Value)
                                ville.Libelle = reader["LibVille"].ToString();
                         
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ville;
        }
    }

    [Serializable]
    public class VilleCollection : ItemCollection
    {
        public VilleCollection()
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
                cmd.CommandText = "Ville_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CVille", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Ville_Rpt_Charger");
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
                cmd.CommandText = "Ref_Ville_Charger";
                cmd.Parameters.AddWithValue("@CVille", DBNull.Value);

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

        public static VilleCollection Charger()
        {
            VilleCollection collection = new VilleCollection();
            Ville ville = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Ville_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CVille", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ville = new Ville();
                            if (dr["CVille"] != DBNull.Value)
                                ville.Code = dr["CVille"].ToString();
                            if (dr["LibVille"] != DBNull.Value)
                                ville.Libelle = dr["LibVille"].ToString();
                 
                            collection.Add(ville);
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
