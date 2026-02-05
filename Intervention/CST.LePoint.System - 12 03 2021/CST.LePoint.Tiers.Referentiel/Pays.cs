using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

//using CST.Framework;

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class Pays : Item
    {
        #region Propriétés

        [XmlAttribute("BActif")]
        [Bindable(true)]
        public bool BActif { get; set; }

        [XmlAttribute("BEurope")]
        [Bindable(true)]
        public bool BEurope { get; set; }

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
        /// Constructeur Pays
        /// </summary>
        public Pays()
        { }

        public Pays(string cPays)
            : this()
        {
            Code = cPays;
        }

        public Pays(string cPays, string libPays, string codeEtat, string cDevise, string communaute)
        {
            Code = cPays;
            Libelle = libPays;
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

                    cmd.CommandText = "Ref_Pays_Sauvegarder";
                    cmd.Parameters.AddWithValue("CPays", Code);
                    cmd.Parameters.AddWithValue("LibPays", Libelle);
                    cmd.Parameters.AddWithValue("@BActif", BActif);
                    cmd.Parameters.AddWithValue("@BEurope", BEurope);
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
            catch (Exception )
            {
                throw;
            }
        }

        /// <summary>
        /// Suppression d'un Pays
        /// </summary>
        public static bool Supprimer(String cpays)
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
                    cmd.CommandText = "Ref_Pays_Supprimer";
                    cmd.Parameters.AddWithValue("CPays", cpays);

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
            catch (Exception )
            {
                throw;
            }

            return carton;
        }

        /// <summary>
        /// Récupération de tous les Pays
        /// </summary>
        public static DataTable RecupererPays()
        {
            var DTPays = new DataTable();
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Pays_RecupererListe";
                    var Adapter = new SqlDataAdapter(cmd);
                    Adapter.Fill(DTPays);
                }
            }
            catch (Exception )
            {
                throw;
            }
            return DTPays;
        }

        /// <summary>
        /// Récupération d'un Pays par identifiant
        /// </summary>
        public static Pays Charger(String cPays)
        {
            Pays pays = null;
            try
            {
                using (SqlConnection connexion = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    connexion.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = connexion;
                    cmd.CommandText = "Ref_Pays_Charger";
                    cmd.Parameters.AddWithValue("CPays", cPays);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            pays = new Pays();
                            if (reader["CPays"] != DBNull.Value)
                                pays.Code = reader["CPays"].ToString();
                            if (reader["LibPays"] != DBNull.Value)
                                pays.Libelle = reader["LibPays"].ToString();
                            //if (reader["BActif"] != DBNull.Value)
                            //    pays.BActif = bool.Parse(reader["BActif"].ToString());
                            //if (reader["BEurope"] != DBNull.Value)
                            //    pays.BEurope = bool.Parse(reader["BEurope"].ToString());
                        }
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return pays;
        }
    }

    [Serializable]
    public class PaysCollection : ItemCollection
    {
        public PaysCollection()
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
                cmd.CommandText = "Pays_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CPays", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Pays_Rpt_Charger");
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
                cmd.CommandText = "Ref_Pays_Charger";
                cmd.Parameters.AddWithValue("@CPays", DBNull.Value);

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

        public static PaysCollection Charger()
        {
            PaysCollection collection = new PaysCollection();
            Pays pays = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Pays_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CPays", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            pays = new Pays();
                            if (dr["CPays"] != DBNull.Value)
                                pays.Code = dr["CPays"].ToString();
                            if (dr["LibPays"] != DBNull.Value)
                                pays.Libelle = dr["LibPays"].ToString();
                            //if (dr["BActif"] != DBNull.Value)
                            //    pays.BActif = bool.Parse(dr["BActif"].ToString());
                            //if (dr["BEurope"] != DBNull.Value)
                            //    pays.BEurope = bool.Parse(dr["BEurope"].ToString());
                            collection.Add(pays);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return collection;
        }
    }
}