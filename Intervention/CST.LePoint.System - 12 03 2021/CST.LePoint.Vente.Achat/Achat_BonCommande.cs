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

namespace CST.LePoint.Achat.Metier
{
    public class Achat_BonCommande
    {
        #region Proriétès
        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateCommande")]
        [Bindable(true)]
        public DateTime? DateCommande { get; set; }

        [XmlAttribute("DateLivraisonSouhaite")]
        [Bindable(true)]
        public DateTime? DateLivraisonSouhaite { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

        [XmlAttribute("Etat")]
        [Bindable(true)]
        public string Etat { get; set; }

        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("MontantHT")]
        [Bindable(true)]
        public decimal MontantHT { get; set; }

        [XmlAttribute("MontantRemise")]
        [Bindable(true)]
        public decimal MontantRemise { get; set; }

        [XmlAttribute("MontantTaxe")]
        [Bindable(true)]
        public decimal MontantTaxe { get; set; }

        [XmlAttribute("MontantTTC")]
        [Bindable(true)]
        public decimal MontantTTC { get; set; }

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

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

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        public Achat_BonCommandeDetailCollection BonCommandeDetailCollection;
        public Achat_BonCommandeTaxeCollection BonCommandeTaxeCollection;
        #endregion

        public Achat_BonCommande()
        {
            this.BonCommandeDetailCollection = new Achat_BonCommandeDetailCollection();
            this.BonCommandeTaxeCollection = new Achat_BonCommandeTaxeCollection();
        }

        public void Inserer()
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

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_Inserer";
                cmd.Parameters.AddWithValue("@BExonoreTVA ", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@CFournisseur ", this.CFournisseur);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateInsertion);
                cmd.Parameters.AddWithValue("@DateCommande", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateLivraisonSouhaite", this.DateLivraisonSouhaite);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                foreach (SqlParameter sqlParameter in cmd.Parameters)
                    if (sqlParameter.Value == null)
                        sqlParameter.Value = DBNull.Value;
                using (SqlDataReader rd = cmd.ExecuteReader())
                {
                    this.NBonCommande = rd["NBonCommande"].ToString();
                }
                int i = 1;
                foreach (Achat_BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {
                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.Sauvegarder(transaction);
                }

                foreach (Achat_BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            Achat_BonCommande ancienCommande = Achat_BonCommande.Charger(NBonCommande);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonCommande_Modifier";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@DateCommande", this.DateCommande);
                cmd.Parameters.AddWithValue("@DateLivraisonSouhaite", this.DateLivraisonSouhaite);
                cmd.Parameters.AddWithValue("@Etat", this.Etat);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                this.SupprimerDetails(transaction);
                this.SupprimerTaxes(transaction);
                int i = 1;
                foreach (Achat_BonCommandeDetail bonCommandeDetail in BonCommandeDetailCollection)
                {

                    bonCommandeDetail.NBonCommande = this.NBonCommande;
                    bonCommandeDetail.Ordre = i++;
                    bonCommandeDetail.Sauvegarder(transaction);
                }
                
                foreach (Achat_BonCommandeTaxe bonCommandeTaxe in BonCommandeTaxeCollection)
                {
                    bonCommandeTaxe.NBonCommande = this.NBonCommande;
                    bonCommandeTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerTaxes(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Achat_BonCommande_SupprimerTaxes";
            cmd.Parameters.AddWithValue("NBonCommande", this.NBonCommande);
            foreach (SqlParameter parameter in cmd.Parameters)
                if (parameter.Value == null)
                    parameter.Value = DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        private void SupprimerDetails(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Achat_BonCommande_SupprimerDetails";
            cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
            foreach (SqlParameter parameter in cmd.Parameters)
                if (parameter.Value == null)
                    parameter.Value = DBNull.Value;
            cmd.ExecuteNonQuery();
        }

        public static Achat_BonCommande Charger(string nBonCommande)
        {
            Achat_BonCommande bonCommande = null;
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
                    cmd.CommandText = "Achat_BonCommande_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonCommande = new Achat_BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonCommande.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraisonSouhaite"] != DBNull.Value)
                                bonCommande.DateLivraisonSouhaite = DateTime.Parse(dr["DateLivraisonSouhaite"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            bonCommande.BonCommandeDetailCollection = Achat_BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = Achat_BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (bonCommande);
            }

        }

        public void Purger()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Purger(transaction);
                    ModifierEtatBonCommande(this.NBonCommande, AchatHelper.EtatBonCommande.PURGER.ToString(), transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

       

        public void Purger(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_BonCommande_Purger";
                cmd.Parameters.AddWithValue("@NBonCommande", this.NBonCommande);
                cmd.ExecuteNonQuery();

            }
            catch
            {
                throw;
            }
        }

        public static void ModifierEtatBonCommande(string nBonCommande, string etat)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    ModifierEtatBonCommande(nBonCommande, etat, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static void ModifierEtatBonCommande(string nCommande, string etat, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_BonCommande_ModifierEtat";
                cmd.Parameters.AddWithValue("@NBonCommande", nCommande);
                cmd.Parameters.AddWithValue("@Etat", etat);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                cmd.ExecuteNonQuery();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Annuler()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    // purger Bon Commande: annule la réservation du stock 
                    Purger(transaction);
                    // SupprimerBonCommande(transaction);
                    ModifierEtatBonCommande(this.NBonCommande, AchatHelper.EtatBonCommande.ANNULER.ToString(), transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

       
    }

    public class Achat_BonCommandeCollection : List<Achat_BonCommande>
    {
        public Achat_BonCommandeCollection()
        {
        }

        public static Achat_BonCommandeCollection Charger(string nBonCommande)
        {
            Achat_BonCommandeCollection collection = new Achat_BonCommandeCollection();
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
                    cmd.CommandText = "Achat_BonCommande_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_BonCommande bonCommande = new Achat_BonCommande();
                            bonCommande.NBonCommande = dr["NBonCommande"].ToString();

                            if (dr["BExonoreTVA"] != DBNull.Value)
                                bonCommande.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                bonCommande.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonCommande.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonCommande.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["DateCommande"] != DBNull.Value)
                                bonCommande.DateCommande = DateTime.Parse(dr["DateCommande"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonCommande.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateLivraisonSouhaite"] != DBNull.Value)
                                bonCommande.DateLivraisonSouhaite = DateTime.Parse(dr["DateLivraisonSouhaite"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonCommande.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["Etat"] != DBNull.Value)
                                bonCommande.Etat = dr["Etat"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonCommande.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonCommande.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                bonCommande.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                bonCommande.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommande.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                bonCommande.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonCommande.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonCommande.PCModification = dr["PCModification"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonCommande.RaisonSociale = dr["RaisonSociale"].ToString();
                            bonCommande.BonCommandeDetailCollection = Achat_BonCommandeDetailCollection.Charger(bonCommande.NBonCommande);
                            bonCommande.BonCommandeTaxeCollection = Achat_BonCommandeTaxeCollection.Charger(bonCommande.NBonCommande);
                            collection.Add(bonCommande);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (collection);
            }
        }
    }
}
