using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.Stock.Metier
{
    [Serializable]
    public class PreInventaire
    {
        #region Proprietés
        [XmlAttribute("NPreInventaire")]
        [Bindable(true)]
        public string NPreInventaire { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("BPrise")]
        [Bindable(true)]
        public bool BPrise { get; set; }

        [XmlAttribute("DatePreInventaire")]
        [Bindable(true)]
        public DateTime DatePreInventaire { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("CReleveur")]
        [Bindable(true)]
        public string CReleveur { get; set; }

        [XmlAttribute("NPrise")]
        [Bindable(true)]
        public string NPrise { get; set; }

        public PreInventaireDetailCollection PreInventaireDetailCollection;
        #endregion

        public PreInventaire() 
        {
            this.PreInventaireDetailCollection = new PreInventaireDetailCollection();
        }

        public void Inserer()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        Inserer(transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PreInventaire_Inserer";

                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DatePreInventaire", DatePreInventaire);
                cmd.Parameters.AddWithValue("@BPrise",this.BPrise);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@Exercice ", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CReleveur", CReleveur);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                using (SqlDataReader dr = cmd.ExecuteReader())
                    if (dr.Read())
                        this.NPreInventaire = dr["NPreInventaire"].ToString();
                foreach (PreInventaireDetail preInventaireDetail in this.PreInventaireDetailCollection)
                {
                    preInventaireDetail.NPreInventaire = this.NPreInventaire;
                    preInventaireDetail.Inserer(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void PreInventaireInsererNPrise(string nPreInventaire, string cEntrepot, string nPrise, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PreInventaire_InsererNPrise";
                cmd.Parameters.AddWithValue("@NPreInventaire", nPreInventaire);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@NPrise", nPrise);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static PreInventaire Charger(string cEntrepot,string nPreInventaire)
        {
            PreInventaire preInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PreInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NPreInventaire", nPreInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    //foreach (SqlParameter parametre in cmd.Parameters)
                    //    if (parametre.Value == null)
                    //        parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            preInventaire = new PreInventaire();

                            preInventaire.NPreInventaire = dr["NPreInventaire"].ToString();
                            preInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                preInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                preInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DatePreInventaire"] != DBNull.Value)
                                preInventaire.DatePreInventaire = DateTime.Parse(dr["DatePreInventaire"].ToString());
                            if (dr["BPrise"] != DBNull.Value)
                                preInventaire.BPrise = bool.Parse(dr["BPrise"].ToString());
                            if (dr["NPrise"] != DBNull.Value)
                                preInventaire.NPrise = dr["NPrise"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                preInventaire.Indice = int.Parse(dr["Indice"].ToString());

                            preInventaire.PreInventaireDetailCollection = PreInventaireDetailCollection.Charger(preInventaire.NPreInventaire, preInventaire.CEntrepot,null);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return preInventaire;
        }
    }

    public class PreInventaireCollection : List<PreInventaire>
    {
        public static PreInventaireCollection Charger(string cEntrepot, string nPreInventaire)
        {
            PreInventaireCollection preInventaieCollection = new PreInventaireCollection();
            PreInventaire preInventaire = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PreInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NPreInventaire", nPreInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            preInventaire = new PreInventaire();

                            preInventaire.NPreInventaire = dr["NPreInventaire"].ToString();
                            preInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                preInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                preInventaire.Observation = dr["Observation"].ToString();
                            if (dr["DatePreInventaire"] != DBNull.Value)
                                preInventaire.DatePreInventaire = DateTime.Parse(dr["DatePreInventaire"].ToString());
                            if (dr["BPrise"] != DBNull.Value)
                                preInventaire.BPrise = bool.Parse(dr["BPrise"].ToString());
                            if (dr["NPrise"] != DBNull.Value)
                                preInventaire.NPrise = dr["NPrise"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                preInventaire.Indice = int.Parse(dr["Indice"].ToString());

                            preInventaire.PreInventaireDetailCollection = PreInventaireDetailCollection.Charger(preInventaire.NPreInventaire, preInventaire.CEntrepot,null);
                            preInventaieCollection.Add(preInventaire);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return preInventaieCollection;
        }
    }
}
