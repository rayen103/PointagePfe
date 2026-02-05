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

namespace CST.Stock.Metier.Mouvements
{
    [Serializable]
    public class PriseInventaire
    {
        #region Proprietes
        [XmlAttribute("NPrise")]
        [Bindable(true)]
        public string NPrise {get;set;}

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot {get;set;}

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice {get;set;}

        [XmlAttribute("DatePrise")]
        [Bindable(true)]
        public DateTime DatePrise {get;set;}

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar {get;set;}

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar {get;set;}

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion {get;set;}

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification {get;set;}

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion {get;set;}

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification {get;set;}

        [XmlAttribute("BPriseFinal")]
        [Bindable(true)]
        public bool BPriseFinal {get;set;}

        [XmlAttribute("NBonInventaire")]
        [Bindable(true)]
        public string NBonInventaire {get;set;}

        [XmlAttribute("CReleveur")]
        [Bindable(true)]
        public string CReleveur { get; set; }

        [XmlAttribute("BFinAnne")]
        [Bindable(true)]
        public bool BFinAnne { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        #endregion
        public PriseInventaireDetailCollection PriseInventaireDetailCollection;

        public PriseInventaire() { this.PriseInventaireDetailCollection = new PriseInventaireDetailCollection(); }

        public void Inserer(string nPreInventaire)
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
                        PreInventaire.PreInventaireInsererNPrise(nPreInventaire,this.CEntrepot, this.NPrise, transaction);
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
                cmd.CommandText = "PriseInventaire_Inserer";

                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DatePrise", DatePrise);
                cmd.Parameters.AddWithValue("@BPriseFinal", this.BPriseFinal);
                cmd.Parameters.AddWithValue("@Exercice ", DateTime.Now.Year);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CReleveur", CReleveur);
                cmd.Parameters.AddWithValue("@BFinAnne", BFinAnne);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                using (SqlDataReader dr = cmd.ExecuteReader())
                    if (dr.Read())
                        this.NPrise = dr["NPrise"].ToString();
                foreach (PriseInventaireDetail priseInventaireDetail in this.PriseInventaireDetailCollection)
                {
                    priseInventaireDetail.NPrise = this.NPrise;
                    priseInventaireDetail.Inserer(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifierReleveur()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PriseInventaire_ModifierReleveur";

                    cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                    cmd.Parameters.AddWithValue("@NPrise", NPrise);
                    cmd.Parameters.AddWithValue("@CReleveur ", this.CReleveur);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateModification", this.DateModification);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

        }

        public static PriseInventaire Charger(string cEntrepot, string nPriseInventaire)
        {
            PriseInventaire priseInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PriseInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NPrise", nPriseInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    //foreach (SqlParameter parametre in cmd.Parameters)
                    //    if (parametre.Value == null)
                    //        parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            priseInventaire = new PriseInventaire();

                            priseInventaire.NPrise = dr["NPrise"].ToString();
                            priseInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["DatePrise"] != DBNull.Value)
                                priseInventaire.DatePrise = DateTime.Parse(dr["DatePrise"].ToString());
                            if (dr["BPriseFinal"] != DBNull.Value)
                                priseInventaire.BPriseFinal = bool.Parse(dr["BPriseFinal"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                priseInventaire.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NBonInventaire"] != DBNull.Value)
                                priseInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                priseInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["BFinAnne"] != DBNull.Value)
                                priseInventaire.BFinAnne = bool.Parse(dr["BFinAnne"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                priseInventaire.Observation = dr["Observation"].ToString();
                            priseInventaire.PriseInventaireDetailCollection = PriseInventaireDetailCollection.Charger(priseInventaire.NPrise, priseInventaire.CEntrepot, null);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return priseInventaire;
        }

        public static PriseInventaire ChargerParInv(string cEntrepot, string nBonInventaire)
        {
            PriseInventaire priseInventaire = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PriseInventaire_ChargerParInv";
                    cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    //foreach (SqlParameter parametre in cmd.Parameters)
                    //    if (parametre.Value == null)
                    //        parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            priseInventaire = new PriseInventaire();

                            priseInventaire.NPrise = dr["NPrise"].ToString();
                            priseInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["DatePrise"] != DBNull.Value)
                                priseInventaire.DatePrise = DateTime.Parse(dr["DatePrise"].ToString());
                            if (dr["BPriseFinal"] != DBNull.Value)
                                priseInventaire.BPriseFinal = bool.Parse(dr["BPriseFinal"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                priseInventaire.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NBonInventaire"] != DBNull.Value)
                                priseInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                priseInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["BFinAnne"] != DBNull.Value)
                                priseInventaire.BFinAnne = bool.Parse(dr["BFinAnne"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                priseInventaire.Observation = dr["Observation"].ToString();
                            priseInventaire.PriseInventaireDetailCollection = PriseInventaireDetailCollection.Charger(priseInventaire.NPrise, priseInventaire.CEntrepot, null);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return priseInventaire;
        }

        public void InsererAnalysePrise(string nPrise1, string nprise2, string nBonInventaire, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "AnalysePrise_Inserer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NPrise1", nPrise1);
                cmd.Parameters.AddWithValue("@NPrise2", nprise2);
                cmd.Parameters.AddWithValue("@NBonInventaire", nBonInventaire);
                cmd.Parameters.AddWithValue("@NPrise3 ", this.NPrise);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

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
    }

    public class PriseInventaireCollection : List<PriseInventaire>
    {
        public static PriseInventaireCollection Charger(string cEntrepot, string nPrise)
        {
            PriseInventaireCollection priseInventaieCollection = new PriseInventaireCollection();
            PriseInventaire priseInventaire = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PriseInventaire_Charger";
                    cmd.Parameters.AddWithValue("@NPrise", nPrise);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            priseInventaire = new PriseInventaire();

                            priseInventaire.NPrise = dr["NPrise"].ToString();
                            priseInventaire.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["DatePrise"] != DBNull.Value)
                                priseInventaire.DatePrise = DateTime.Parse(dr["DatePrise"].ToString());
                            if (dr["BPriseFinal"] != DBNull.Value)
                                priseInventaire.BPriseFinal = bool.Parse(dr["BPriseFinal"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                priseInventaire.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NBonInventaire"] != DBNull.Value)
                                priseInventaire.NBonInventaire = dr["NBonInventaire"].ToString();
                            if (dr["CReleveur"] != DBNull.Value)
                                priseInventaire.CReleveur = dr["CReleveur"].ToString();
                            if (dr["BFinAnne"] != DBNull.Value)
                                priseInventaire.BFinAnne = bool.Parse(dr["BFinAnne"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                priseInventaire.Observation = dr["Observation"].ToString();
                            priseInventaire.PriseInventaireDetailCollection = PriseInventaireDetailCollection.Charger(priseInventaire.NPrise, priseInventaire.CEntrepot, null);
                            priseInventaieCollection.Add(priseInventaire);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return priseInventaieCollection;
        }
    }
}
