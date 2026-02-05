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
    public class Achat_Paiement
    {
        #region Proriétès
        [XmlAttribute("CReglement")]
        [Bindable(true)]
        public string CReglement { get; set; }
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }
        [XmlAttribute("BAnnulation")]
        [Bindable(true)]
        public bool BAnnulation { get; set; }
        [XmlAttribute("Montant")]
        [Bindable(true)]
        public decimal Montant { get; set; }
        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }
        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }
        #endregion

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
                transaction.Commit();
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            Achat_Facture facture = Achat_Facture.Charger(this.NFacture);
            Achat_Reglement reglement = Achat_Reglement.Charger(this.CReglement);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Paiement_Sauvegarder";
                cmd.Parameters.AddWithValue("@NFacture", NFacture);
                cmd.Parameters.AddWithValue("@CReglement", CReglement);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@Montant", Montant);
                cmd.Parameters.AddWithValue("@BAnnulation", BAnnulation);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                facture.CreditFacture = facture.CreditFacture - this.Montant;
                reglement.ResteReglement = reglement.ResteReglement - this.Montant;

                facture.ModifiePar = this.CreePar;
                facture.MiseAJourCreditFacture(transaction);

                if ((reglement.CTypeReglement != AchatHelper.TypeReglement.AVRAVC.ToString()) && (reglement.CTypeReglement != AchatHelper.TypeReglement.AVR.ToString()))
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, -this.Montant, -this.Montant, 0, 0, transaction);
                else
                    if (reglement.ResteReglement!=0)
                    {
                        reglement.CEtatReglement = AchatHelper.EtatReglement.ENATTENTE.ToString();
                        AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, -this.Montant, 0, 0,-this.Montant, transaction);
                    }
                    else
                    {
                        AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, -this.Montant, 0, 0, -this.Montant, transaction);
                        reglement.CEtatReglement = AchatHelper.EtatReglement.ASSOCIE.ToString();
                    }
                reglement.ModifiePar = this.CreePar;
                reglement.Modifier(transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Annuler(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Paiement_Annuler";
                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Liberer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Achat_Facture facture = Achat_Facture.Charger(this.NFacture);
                    Achat_Reglement reglement = Achat_Reglement.Charger(this.CReglement);

                    facture.CreditFacture = facture.CreditFacture + this.Montant;
                    reglement.ResteReglement = reglement.ResteReglement + this.Montant;

                    if (reglement.CTypeReglement != AchatHelper.TypeReglement.AVRAVC.ToString())
                        AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, this.Montant, this.Montant, 0, 0, transaction);
                    else
                        if (reglement.ResteReglement != 0)
                        {
                            reglement.CEtatReglement = AchatHelper.EtatReglement.ENATTENTE.ToString();
                            AchatHelper.MiseAJourSoldeFnr(this.CFournisseur,this.Montant, 0, 0, this.Montant, transaction);
                        }
                        else
                        {
                            AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, this.Montant, 0, 0,this.Montant, transaction);
                            reglement.CEtatReglement = AchatHelper.EtatReglement.ASSOCIE.ToString();
                        }

                    reglement.DateModification = DateTime.Today;
                    reglement.ModifiePar = this.ModifiePar;
                    reglement.Modifier(transaction);

                    facture.ModifiePar = this.CreePar;
                    facture.MiseAJourCreditFacture(transaction);

                    Annuler(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Achat_Paiement Charger(string nFacture, string cReglement)
        {
            Achat_Paiement paiement = null;

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
                    cmd.CommandText = "Achat_Paiement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            paiement = new Achat_Paiement();
                            paiement.CReglement = dr["CReglement"].ToString();
                            paiement.NFacture = dr["NFacture"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                paiement.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                paiement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                paiement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                paiement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                paiement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                paiement.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());

                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return paiement;
        }

    }
    public class Achat_PaiementCollection : List<Achat_Paiement>
    {
        public Achat_PaiementCollection Charger(string nFacture, string cReglement)
        {
            Achat_PaiementCollection collection = new Achat_PaiementCollection();

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
                    cmd.CommandText = "Achat_Paiement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_Paiement paiement = new Achat_Paiement();
                            paiement.CReglement = dr["CReglement"].ToString();
                            paiement.NFacture = dr["NFacture"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                paiement.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                paiement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                paiement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                paiement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                paiement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                paiement.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            collection.Add(paiement);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return collection;
        }
    }
}
