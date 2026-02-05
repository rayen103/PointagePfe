using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class Adresse : AdresseBase
    {
        #region Propriétés

        [XmlAttribute("NTiers")]
        [Bindable(true)]
        public string NTiers { get; set; }

        [XmlAttribute("BAdresseFacturation")]
        [Bindable(true)]
        public bool BAdresseFacturation { get; set; }

        [XmlAttribute("BAdresseLivraison")]
        [Bindable(true)]
        public bool BAdresseLivraison { get; set; }

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

        public Adresse()
        {
            this.IdAdresse = 0;
            this.LibAdresse = "entrer une adresse";
            this.NTiers = "entrer ntier";
        }

        public static Adresse Charger(int idAdresse)
        {
            Adresse adresse = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Adresse_Charger";
                    cmd.Parameters.Add(new SqlParameter("@IdAdresse", idAdresse));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            adresse = new Adresse();
                            if (dr["AssigneA"] != DBNull.Value)
                                adresse.AssigneA = int.Parse(dr["AssigneA"].ToString().Trim());
                            if (dr["BAdresseFacturation"] != DBNull.Value)
                                adresse.BAdresseFacturation = bool.Parse(dr["BAdresseFacturation"].ToString().Trim());
                            if (dr["BAdresseLivraison"] != DBNull.Value)
                                adresse.BAdresseLivraison = bool.Parse(dr["BAdresseLivraison"].ToString().Trim());
                            if (dr["BNPAI"] != DBNull.Value)
                                adresse.BNPAI = bool.Parse(dr["BNPAI"].ToString().Trim());
                            if (dr["CPays"] != DBNull.Value)
                                adresse.CPays = dr["CPays"].ToString().Trim();
                            if (dr["CPostal"] != DBNull.Value)
                                adresse.CPostal = dr["CPostal"].ToString().Trim();
                            if (dr["LibAdresse"] != DBNull.Value)
                                adresse.LibAdresse = dr["LibAdresse"].ToString().Trim();
                            if (dr["NTiers"] != DBNull.Value)
                                adresse.NTiers = dr["NTiers"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                adresse.Ville = dr["Ville"].ToString().Trim();
                            if (dr["IdAdresse"] != DBNull.Value)
                                adresse.IdAdresse = int.Parse(dr["IdAdresse"].ToString().Trim());
                            if (dr["CTypeAdresse"] != DBNull.Value)
                                adresse.CTypeAdresse = dr["CTypeAdresse"].ToString().Trim();
                            if (dr["SiteWeb"] != DBNull.Value)
                                adresse.SiteWeb = dr["SiteWeb"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
            return adresse;
        }

        public void Sauvegarder()
        {
            SqlTransaction transaction = null;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                try
                {
                    cn.Open();
                    transaction = cn.BeginTransaction();
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception )
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Adresse_Sauvegarder";
                cmd.Parameters.Add(new SqlParameter("@IdAdresse", IdAdresse));
                cmd.Parameters.Add(new SqlParameter("@BAdresseFacturation", BAdresseFacturation));
                cmd.Parameters.Add(new SqlParameter("@BAdresseLivraison", BAdresseLivraison));
                cmd.Parameters.Add(new SqlParameter("@BNPAI", BNPAI));
                cmd.Parameters.Add(new SqlParameter("@CPays", CPays));
                cmd.Parameters.Add(new SqlParameter("@CPostal", CPostal));
                cmd.Parameters.Add(new SqlParameter("@LibAdresse", LibAdresse));
                cmd.Parameters.Add(new SqlParameter("@Ville", Ville));
                cmd.Parameters.Add(new SqlParameter("@NTiers", NTiers));
                cmd.Parameters.Add(new SqlParameter("@AssigneA", AssigneA));
                cmd.Parameters.Add(new SqlParameter("@CTypeAdresse", CTypeAdresse));
                cmd.Parameters.Add(new SqlParameter("@SiteWeb", SiteWeb));
                cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

                foreach (SqlParameter sqlParametre in cmd.Parameters)
                {
                    if (sqlParametre.Value == null)
                    {
                        sqlParametre.Value = DBNull.Value;
                    }
                }
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.IdAdresse = int.Parse(dr["DernierIdAdresse"].ToString());
                    }
                }

                cmd.Dispose();
            }
            catch (Exception )
            {
                throw;
            }
        }

        public void Supprimer()
        {
            SqlTransaction transaction = null;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                try
                {
                    cn.Open();
                    transaction = cn.BeginTransaction();

                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch (Exception )
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Adresse_Supprimer";
                cmd.Parameters.Add(new SqlParameter("@IdAdresse", IdAdresse));

                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch (Exception )
            {
                throw;
            }
        }
    }

    [Serializable]
    public class AdresseCollection : List<Adresse>
    {
        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptAdresse_Charger";
                cmd.Parameters.AddWithValue("@IdAdresse", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptAdresse_Charger");
            }
            return (ds);
        }

        public static AdresseCollection Charger()
        {
            AdresseCollection collection = new AdresseCollection();
            Adresse adresse = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Adresse_Charger";
                    cmd.Parameters.Add(new SqlParameter("@IdAdresse", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            adresse = new Adresse();
                            if (dr["AssigneA"] != DBNull.Value)
                                adresse.AssigneA = int.Parse(dr["AssigneA"].ToString().Trim());
                            if (dr["BAdresseFacturation"] != DBNull.Value)
                                adresse.BAdresseFacturation = bool.Parse(dr["BAdresseFacturation"].ToString().Trim());
                            if (dr["BAdresseLivraison"] != DBNull.Value)
                                adresse.BAdresseLivraison = bool.Parse(dr["BAdresseLivraison"].ToString().Trim());
                            if (dr["BNPAI"] != DBNull.Value)
                                adresse.BNPAI = bool.Parse(dr["BNPAI"].ToString().Trim());
                            if (dr["CPays"] != DBNull.Value)
                                adresse.CPays = dr["CPays"].ToString().Trim();
                            if (dr["CPostal"] != DBNull.Value)
                                adresse.CPostal = dr["CPostal"].ToString().Trim();
                            if (dr["LibAdresse"] != DBNull.Value)
                                adresse.LibAdresse = dr["LibAdresse"].ToString().Trim();
                            if (dr["NTiers"] != DBNull.Value)
                                adresse.NTiers = dr["NTiers"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                adresse.Ville = dr["Ville"].ToString().Trim();
                            if (dr["IdAdresse"] != DBNull.Value)
                                adresse.IdAdresse = int.Parse(dr["IdAdresse"].ToString().Trim());
                            if (dr["CTypeAdresse"] != DBNull.Value)
                                adresse.CTypeAdresse = dr["CTypeAdresse"].ToString().Trim();
                            if (dr["SiteWeb"] != DBNull.Value)
                                adresse.SiteWeb = dr["SiteWeb"].ToString().Trim();

                            collection.Add(adresse);
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

        public static AdresseCollection Charger(string nTiers)
        {
            AdresseCollection collection = new AdresseCollection();
            Adresse adresse = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NTiersAdresses_Charger";
                    cmd.Parameters.Add(new SqlParameter("@NTiers", nTiers));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            adresse = new Adresse();
                            adresse.NTiers = dr["NTiers"].ToString().Trim();
                            if (dr["AssigneA"] != DBNull.Value)
                                adresse.AssigneA = int.Parse(dr["AssigneA"].ToString().Trim());
                            if (dr["BAdresseFacturation"] != DBNull.Value)
                                adresse.BAdresseFacturation = bool.Parse(dr["BAdresseFacturation"].ToString().Trim());
                            if (dr["BAdresseLivraison"] != DBNull.Value)
                                adresse.BAdresseLivraison = bool.Parse(dr["BAdresseLivraison"].ToString().Trim());
                            if (dr["BNPAI"] != DBNull.Value)
                                adresse.BNPAI = bool.Parse(dr["BNPAI"].ToString().Trim());
                            if (dr["CPays"] != DBNull.Value)
                                adresse.CPays = dr["CPays"].ToString().Trim();
                            if (dr["CPostal"] != DBNull.Value)
                                adresse.CPostal = dr["CPostal"].ToString().Trim();
                            if (dr["LibAdresse"] != DBNull.Value)
                                adresse.LibAdresse = dr["LibAdresse"].ToString().Trim();
                            if (dr["CTypeAdresse"] != DBNull.Value)
                                adresse.CTypeAdresse = dr["CTypeAdresse"].ToString().Trim();
                            if (dr["SiteWeb"] != DBNull.Value)
                                adresse.SiteWeb = dr["SiteWeb"].ToString().Trim();
                            if (dr["Ville"] != DBNull.Value)
                                adresse.Ville = dr["Ville"].ToString().Trim();
                            if (dr["IdAdresse"] != DBNull.Value)
                                adresse.IdAdresse = int.Parse(dr["IdAdresse"].ToString().Trim());
                            collection.Add(adresse);
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

        public Adresse Obtenir(int idAdresse, string nTiers)
        {
            Adresse adresse = this.Where(a => (a.IdAdresse == idAdresse) && (a.NTiers.Equals(nTiers))).FirstOrDefault();
            return adresse;
        }
    }
}