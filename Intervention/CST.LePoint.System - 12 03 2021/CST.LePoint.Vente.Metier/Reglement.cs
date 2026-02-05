using CST.LePoint.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Reglement
    {
        #region propriete

        [XmlAttribute("CReglement")]
        [Bindable(true)]
        public string CReglement { get; set; }

        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }

        [XmlAttribute("CEtatReglement")]
        [Bindable(true)]
        public string CEtatReglement { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CTypeReglement")]
        [Bindable(true)]
        public string CTypeReglement { get; set; }

        [XmlAttribute("BRegularisationImpaye")]
        [Bindable(true)]
        public bool BRegularisationImpaye { get; set; }

        [XmlAttribute("BAncienReglement")]
        [Bindable(true)]
        public bool BAncienReglement { get; set; }

        [XmlAttribute("BTransfereComptabilite")]
        [Bindable(true)]
        public bool BTransfereComptabilite { get; set; }

        [XmlAttribute("DateEcheance")]
        [Bindable(true)]
        public DateTime? DateEcheance { get; set; }

        [XmlAttribute("DateEmission")]
        [Bindable(true)]
        public DateTime? DateEmission { get; set; }

        [XmlAttribute("DateAvis")]
        [Bindable(true)]
        public DateTime? DateAvis { get; set; }

        [XmlAttribute("Montant")]
        [Bindable(true)]
        public decimal Montant { get; set; }

        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }

        //si cheque c'est le numero du chèque
        [XmlAttribute("NReglement")]
        [Bindable(true)]
        public string NReglement { get; set; }

        [XmlAttribute("BAnnulation")]
        [Bindable(true)]
        public bool BAnnulation { get; set; }

        [XmlAttribute("BGarantie")]
        [Bindable(true)]
        public bool BGarantie { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute(" ObjetReglement")]
        [Bindable(true)]
        public string ObjetReglement { get; set; }

        [XmlAttribute(" Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("CVendeur")]
        [Bindable(true)]
        public int CVendeur { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("ResteReglement")]
        [Bindable(true)]
        public decimal ResteReglement { get; set; }

        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }

        [XmlAttribute("NFeuilleCaisse")]
        [Bindable(true)]
        public int NFeuilleCaisse { get; set; }

        [XmlAttribute("BaseRetenu")]
        [Bindable(true)]
        public decimal BaseRetenu { get; set; }

        [XmlAttribute("DateAnnulation")]
        [Bindable(true)]
        public DateTime? DateAnnulation { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

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

        [XmlAttribute("BDebit")]
        [Bindable(true)]
        public bool BDebit { get; set; }

        [XmlAttribute("CReglementRemp")]
        [Bindable(true)]
        public string CReglementRemp { get; set; }

        [XmlAttribute("Commission")]
        [Bindable(true)]
        public decimal Commission { get; set; }

        [XmlAttribute("Interet")]
        [Bindable(true)]
        public decimal Interet { get; set; }

        [XmlAttribute("BContentieux")]
        [Bindable(true)]
        public bool BContentieux { get; set; }

        [XmlAttribute("RIB")]
        [Bindable(true)]
        public string RIB { get; set; }

        [XmlAttribute("CAgence")]
        [Bindable(true)]
        public string CAgence { get; set; }

        #endregion propriete

        public Reglement()
        { }

        public Reglement(string cReglement)
        {
            this.CReglement = cReglement;
        }

        public void InsererTab(string cReglementTab)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    InsererNumReg(cReglementTab, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void InsererScanDoc(int idScanDoc)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    InsererNumRegScanDoc(idScanDoc,transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
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

        public void InsererNumRegScanDoc(int idScanDoc, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ScanDoc_InsererCReg";
                cmd.Parameters.AddWithValue("@IdScanDoc", idScanDoc);
                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
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

        public void InsererNumReg(string cReglementTab, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReglementTablette_InsererCReg";
                cmd.Parameters.AddWithValue("@CReglementTablette", cReglementTab);
                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
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

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Inserer";

                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@BTransfereComptabilite ", this.BTransfereComptabilite);
                cmd.Parameters.AddWithValue("@BRegularisationImpaye ", this.BRegularisationImpaye);
                cmd.Parameters.AddWithValue("@BAnnulation", this.BAnnulation);
                cmd.Parameters.AddWithValue("@BDebit", this.BDebit);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@ObjetReglement", this.ObjetReglement);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@DateAnnulation", this.DateAnnulation);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@NFeuilleCaisse", this.NFeuilleCaisse);
                cmd.Parameters.AddWithValue("@BaseRetenu", this.BaseRetenu);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@Commission ", this.Commission);
                cmd.Parameters.AddWithValue("@Interet", this.Interet);
                cmd.Parameters.AddWithValue("@BContentieux", this.BContentieux);
                cmd.Parameters.AddWithValue("@RIB", this.RIB);
                cmd.Parameters.AddWithValue("@CAgence", this.CAgence);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.CReglement = dr["CReglement"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                if ((this.CTypeReglement != VenteHelper.TypeReglement.AVR.ToString()) && (this.CTypeReglement != VenteHelper.TypeReglement.AVRAVC.ToString()) && !this.BContentieux)
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, this.Montant, 0, 0, transaction);
                else
                    if (this.CTypeReglement == VenteHelper.TypeReglement.AVRAVC.ToString())
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, this.Montant, 0, transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsererIMP()
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
                    cmd.CommandText = "ReglementIMP2013_Inserer";

                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                    cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                    cmd.Parameters.AddWithValue("@BTransfereComptabilite ", this.BTransfereComptabilite);
                    cmd.Parameters.AddWithValue("@BRegularisationImpaye ", this.BRegularisationImpaye);
                    cmd.Parameters.AddWithValue("@BAnnulation", this.BAnnulation);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@ObjetReglement", this.ObjetReglement);
                    cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                    cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                    cmd.Parameters.AddWithValue("@DateAnnulation", this.DateAnnulation);
                    cmd.Parameters.AddWithValue("@Montant", this.Montant);
                    cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                    cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                    cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@DateAvis", this.DateAvis);

                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.CReglement = dr["CReglement"].ToString();
                            this.Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    }
                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
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
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Modifier";

                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@BTransfereComptabilite ", this.BTransfereComptabilite);
                cmd.Parameters.AddWithValue("@BAnnulation", this.BAnnulation);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@ObjetReglement", this.ObjetReglement);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@DateAnnulation", this.DateAnnulation);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter Parameter in cmd.Parameters)
                {
                    if (Parameter.Value == null)
                    {
                        Parameter.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void ModifierAvoir(string ancienClient, decimal ancienMontant, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_ModifierAvoir";

                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@AncienClient", ancienClient);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@AncienMontant", ancienMontant);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter Parameter in cmd.Parameters)
                {
                    if (Parameter.Value == null)
                    {
                        Parameter.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Supprimer()
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
                    cmd.CommandText = "Reglement_Supprimer";
                    cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                    cmd.ExecuteNonQuery();

                    VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, -this.Montant, 0, 0, transaction);
                    transaction.Commit();
                }

                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void ReglementDouteux(SqlTransaction transaction, string cReglement, string dateContentieux)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "UPDATE Reglement SET BContentieux = 1, DateContentieux = " + SysHelper.ToSqlDatetime(dateContentieux) + " WHERE CReglement='" + cReglement + "'";
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ModifierEtatReg(string cReglement, string etat)
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

                    cmd.CommandText = "Reglement_ModifierEtat";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@Etat", etat);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }
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

        public void Annuler()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    if (this.CTypeReglement != VenteHelper.TypeReglement.AVRAVC.ToString())
                    {
                        this.BAnnulation = true;
                        this.Modifier(transaction);
                    }
                    PaiementClientCollection paiements = PaiementClientCollection.Charger(this.CReglement, string.Empty);
                    if (paiements != null)
                    {
                        foreach (PaiementClient paiement in paiements)
                        {
                            Facture facture = Facture.Charger(paiement.NFacture, transaction);
                            if (facture != null)
                            {
                                facture.CreditFacture = facture.CreditFacture + paiement.MontantReglement;
                                facture.MiseAJourCreditFacture(transaction);
                                VenteHelper.ModifierSolde(null, null, this.CClient, 0, paiement.MontantReglement, 0, 0, 0, 0, transaction);

                                AvoirCollection avoirs = AvoirCollection.ChargerAvoirFacture(facture.NFacture, transaction);
                                if (avoirs != null)
                                {
                                    foreach (Avoir avoir in avoirs)
                                    {
                                        Reglement reglement = Reglement.ChargerReglementAvoir(avoir.NAvoir, transaction);
                                        if (reglement.CTypeReglement == VenteHelper.TypeReglement.AVRAVC.ToString())
                                        {
                                            reglement.CTypeReglement = VenteHelper.TypeReglement.AVR.ToString();
                                            VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, -reglement.ResteReglement, 0, transaction);

                                            PaiementClient nouveauPaiement = new PaiementClient();
                                            nouveauPaiement.NFacture = facture.NFacture;
                                            nouveauPaiement.BAnnulation = false;
                                            nouveauPaiement.CClient = this.CClient;
                                            nouveauPaiement.CReglement = reglement.CReglement;
                                            nouveauPaiement.DateInsertion = (DateTime)this.DateAnnulation;
                                            nouveauPaiement.Sauvegarder(transaction);
                                        }
                                    }
                                }
                                paiement.BAnnulation = true;
                                paiement.Sauvegarder();
                            }
                        }
                    }
                    if (this.CEtatReglement == VenteHelper.EtatReglement.IMP.ToString())
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, 0, this.Montant, transaction);
                    else
                        if ((this.CTypeReglement != VenteHelper.TypeReglement.AVRAVC.ToString()) || (this.CTypeReglement != VenteHelper.TypeReglement.AVR.ToString()))
                            VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, -this.ResteReglement, 0, 0, transaction);
                        else
                            if (this.CTypeReglement == VenteHelper.TypeReglement.AVRAVC.ToString())
                                VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, -this.ResteReglement, 0, transaction);

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void RegulariserGarantie(string cClient, string cReglementGaranti, string cReglement, decimal montant, bool bGaranti)
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
                    cmd.CommandText = "ReglementGaranti_Remplacer";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@CReglementGarantie", cReglementGaranti);
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@Montant", montant);
                    cmd.Parameters.AddWithValue("@BGaranti", bGaranti);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public static void RegulariserContentieux(string cDocumentContentieux, string cReglement, decimal montant, string typeDocument, int creePar)
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
                    cmd.CommandText = "ReglementContentieux_Remplacer";
                    cmd.Parameters.AddWithValue("@CDocumentContentieux", cDocumentContentieux);
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@Montant", montant);
                    cmd.Parameters.AddWithValue("@TypeDocument", typeDocument);
                    cmd.Parameters.AddWithValue("@CreePar", creePar);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
           
        }

        public static void Valider(string cClient, string cReglementImp, string cReglement, decimal reste, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Remplacer";
                cmd.Parameters.AddWithValue("@CClient", cClient);
                cmd.Parameters.AddWithValue("@CReglementImp", cReglementImp);
                cmd.Parameters.AddWithValue("@CReglement", cReglement);
                cmd.Parameters.AddWithValue("@Reste", reste);
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

        public static void Valider(string cClient, string cReglementImp, string cReglement, decimal reste)
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
                    cmd.CommandText = "Reglement_Remplacer";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@CReglementImp", cReglementImp);
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@Reste", reste);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public static void Redresser(string cClient, string cRegRedressement, string cReglement, decimal montant, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Redresser";
                cmd.Parameters.AddWithValue("@CClient", cClient);
                cmd.Parameters.AddWithValue("@cRegRedressement", cRegRedressement);
                cmd.Parameters.AddWithValue("@CReglement", cReglement);
                cmd.Parameters.AddWithValue("@Montant", montant);
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

        public static Reglement Charger(string cReglement, SqlTransaction transaction)
        {
            Reglement reglement = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Charger";
                cmd.Parameters.AddWithValue("@CReglement", cReglement);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        reglement = new Reglement();
                        reglement.CReglement = dr["CReglement"].ToString();
                        if (dr["CBanque"] != DBNull.Value)
                            reglement.CBanque = dr["CBanque"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            reglement.CClient = dr["CClient"].ToString();
                        if (dr["BTransfereComptabilite"] != DBNull.Value)
                            reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                        if (dr["BRegularisationImpaye"] != DBNull.Value)
                            reglement.BRegularisationImpaye = bool.Parse(dr["BRegularisationImpaye"].ToString());
                        if (dr["CEtatReglement"] != DBNull.Value)
                            reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                        if (dr["CTypeReglement"] != DBNull.Value)
                            reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                        if (dr["NPiece"] != DBNull.Value)
                            reglement.NPiece = dr["NPiece"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["CVendeur"] != DBNull.Value)
                            reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                        if (dr["Observation"] != DBNull.Value)
                            reglement.Observation = dr["Observation"].ToString();
                        if (dr["ObjetReglement"] != DBNull.Value)
                            reglement.ObjetReglement = dr["ObjetReglement"].ToString();
                        if (dr["BAnnulation"] != DBNull.Value)
                            reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                        if (dr["Montant"] != DBNull.Value)
                            reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                        if (dr["ResteReglement"] != DBNull.Value)
                            reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                        if (dr["NFeuilleCaisse"] != DBNull.Value)
                            reglement.NFeuilleCaisse = int.Parse(dr["NFeuilleCaisse"].ToString());
                        if (dr["BaseRetenu"] != DBNull.Value)
                            reglement.BaseRetenu = decimal.Parse(dr["BaseRetenu"].ToString());
                        if (dr["NAvoir"] != DBNull.Value)
                            reglement.NAvoir = dr["NAvoir"].ToString();
                        if (dr["NReglement"] != DBNull.Value)
                            reglement.NReglement = dr["NReglement"].ToString();
                        if (dr["DateAnnulation"] != DBNull.Value)
                            reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                        if (dr["DateEcheance"] != DBNull.Value)
                            reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                        if (dr["DateEmission"] != DBNull.Value)
                            reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                        if (dr["Indice"] != DBNull.Value)
                            reglement.Indice = int.Parse(dr["Indice"].ToString());
                        if (dr["BContentieux"] != DBNull.Value)
                            reglement.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                        if (dr["RIB"] != DBNull.Value)
                            reglement.RIB = dr["RIB"].ToString();
                        if (dr["CAgence"] != DBNull.Value)
                            reglement.CAgence = dr["CAgence"].ToString();
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return reglement;
        }

        public static Reglement ChargerTablette(string cReglementTablette )
        {
            Reglement reglement = null;
            
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReglementTablette_Charger";
                cmd.Parameters.AddWithValue("@CReglementTablette", cReglementTablette);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        reglement = new Reglement();
                        reglement.CReglement = dr["CReglementTab"].ToString();
                        if (dr["CBanque"] != DBNull.Value)
                            reglement.CBanque = dr["CBanque"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            reglement.CClient = dr["CClient"].ToString();
                        if (dr["CTypeReglement"] != DBNull.Value)
                            reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["CVendeur"] != DBNull.Value)
                            reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                        if (dr["Observation"] != DBNull.Value)
                            reglement.Observation = dr["Observation"].ToString();
                        if (dr["Montant"] != DBNull.Value)
                            reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                        if (dr["NReglement"] != DBNull.Value)
                            reglement.NReglement = dr["NReglement"].ToString();
                        if (dr["DateEcheance"] != DBNull.Value)
                            reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                        if (dr["DateEmission"] != DBNull.Value)
                            reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                    }
                }
            }
            }
            catch (Exception)
            {
                throw;
            }


            return reglement;
        }


        public static Reglement ChargerReglementAvoir(string nAvoir, SqlTransaction transaction)
        {
            Reglement reglement = null;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReglementAvoir_Charger";
                cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        reglement = new Reglement();
                        reglement.CReglement = dr["CReglement"].ToString();
                        if (dr["CBanque"] != DBNull.Value)
                            reglement.CBanque = dr["CBanque"].ToString();
                        if (dr["CClient"] != DBNull.Value)
                            reglement.CClient = dr["CClient"].ToString();
                        if (dr["BTransfereComptabilite"] != DBNull.Value)
                            reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                        if (dr["CEtatReglement"] != DBNull.Value)
                            reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                        if (dr["CTypeReglement"] != DBNull.Value)
                            reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                        if (dr["NPiece"] != DBNull.Value)
                            reglement.NPiece = dr["NPiece"].ToString();
                        if (dr["RaisonSociale"] != DBNull.Value)
                            reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                        if (dr["CVendeur"] != DBNull.Value)
                            reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                        if (dr["Observation"] != DBNull.Value)
                            reglement.Observation = dr["Observation"].ToString();
                        if (dr["ObjetReglement"] != DBNull.Value)
                            reglement.ObjetReglement = dr["ObjetReglement"].ToString();
                        if (dr["BAnnulation"] != DBNull.Value)
                            reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                        if (dr["Montant"] != DBNull.Value)
                            reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                        if (dr["ResteReglement"] != DBNull.Value)
                            reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                        if (dr["NAvoir"] != DBNull.Value)
                            reglement.NAvoir = dr["NAvoir"].ToString();
                        if (dr["NReglement"] != DBNull.Value)
                            reglement.NReglement = dr["NReglement"].ToString();

                        if (dr["DateAnnulation"] != DBNull.Value)
                            reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                        if (dr["DateEcheance"] != DBNull.Value)
                            reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                        if (dr["DateEmission"] != DBNull.Value)
                            reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                        if (dr["Indice"] != DBNull.Value)
                            reglement.Indice = int.Parse(dr["Indice"].ToString());
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return reglement;
        }

        public static Reglement ChargerReglementAvoir(string nAvoir)
        {
            Reglement reglement = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ReglementAvoir_Charger";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reglement = new Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                reglement.CClient = dr["CClient"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["ObjetReglement"] != DBNull.Value)
                                reglement.ObjetReglement = dr["ObjetReglement"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();

                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return reglement;
        }

        public static Reglement Charger(string cReglement)
        {
            Reglement reglement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Reglement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reglement = new Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                reglement.CClient = dr["CClient"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();

                            if (dr["CVendeur"] != DBNull.Value)
                                reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["ObjetReglement"] != DBNull.Value)
                                reglement.ObjetReglement = dr["ObjetReglement"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["NFeuilleCaisse"] != DBNull.Value)
                                reglement.NFeuilleCaisse = int.Parse(dr["NFeuilleCaisse"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Commission"] != DBNull.Value)
                                reglement.Commission = decimal.Parse(dr["Commission"].ToString());
                            if (dr["Interet"] != DBNull.Value)
                                reglement.Interet = decimal.Parse(dr["Interet"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                reglement.DateInsertion = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                reglement.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                reglement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                reglement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCCreation"] != DBNull.Value)
                                reglement.PCInsertion = dr["PCCreation"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                reglement.PCModification = dr["PCModification"].ToString();
                            if (dr["BContentieux"] != DBNull.Value)
                                reglement.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["RIB"] != DBNull.Value)
                                reglement.RIB = dr["RIB"].ToString();
                            if (dr["CAgence"] != DBNull.Value)
                                reglement.CAgence = dr["CAgence"].ToString();
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return reglement;
        }

        public static Reglement ChargerAvoir(string nAvoir)
        {
            Reglement reglement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Reglement_ChargerAvoir";
                    cmd.Parameters.AddWithValue("@NAvoir", nAvoir);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reglement = new Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                reglement.CClient = dr["CClient"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();

                            if (dr["CVendeur"] != DBNull.Value)
                                reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["ObjetReglement"] != DBNull.Value)
                                reglement.ObjetReglement = dr["ObjetReglement"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["NFeuilleCaisse"] != DBNull.Value)
                                reglement.NFeuilleCaisse = int.Parse(dr["NFeuilleCaisse"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Commission"] != DBNull.Value)
                                reglement.Commission = decimal.Parse(dr["Commission"].ToString());
                            if (dr["Interet"] != DBNull.Value)
                                reglement.Interet = decimal.Parse(dr["Interet"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                reglement.DateInsertion = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                reglement.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                reglement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                reglement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCCreation"] != DBNull.Value)
                                reglement.PCInsertion = dr["PCCreation"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                reglement.PCModification = dr["PCModification"].ToString();

                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }

            return reglement;
        }

        public void MiseAJourReglementImpaye(string cReglement)
        {
            Reglement ancienReglement = Reglement.Charger(CClient);

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
                    cmd.CommandText = "Reglement_Modifier";

                    cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                    cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                    cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                    cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                    cmd.Parameters.AddWithValue("@BTransfereComptabilite ", this.BTransfereComptabilite);
                    cmd.Parameters.AddWithValue("@BAnnulation", this.BAnnulation);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@ObjetReglement", this.ObjetReglement);
                    cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                    cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                    cmd.Parameters.AddWithValue("@DateAnnulation", this.DateAnnulation);
                    cmd.Parameters.AddWithValue("@Montant", this.Montant);
                    cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                    cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                    cmd.Parameters.AddWithValue("@NReglement", this.NReglement);

                    cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                    cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiePar ", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    if ((ancienReglement.CEtatReglement == VenteHelper.EtatReglement.IMP.ToString()) && this.CEtatReglement != VenteHelper.EtatReglement.IMP.ToString())
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, 0m, 0m, 0m, -this.Montant, transaction);
                    if ((ancienReglement.CEtatReglement != VenteHelper.EtatReglement.IMP.ToString()) && this.CEtatReglement == VenteHelper.EtatReglement.IMP.ToString())
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0m, 0m, 0m, 0m, 0m, this.Montant, transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Annulation()
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
                    cmd.CommandText = "Reglement_Annuler";

                    cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                    cmd.Parameters.AddWithValue("@CClient", this.CClient);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateAnnulation", DateTime.Now);

                    foreach (SqlParameter Parameter in cmd.Parameters)
                    {
                        if (Parameter.Value == null)
                        {
                            Parameter.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Modif(string cReglement, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Reglement_Modif";

                cmd.Parameters.AddWithValue("@CReglement", cReglement);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@BDebit", this.BDebit);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@NFeuilleCaisse", this.NFeuilleCaisse);
                cmd.Parameters.AddWithValue("@BaseRetenu", this.BaseRetenu);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@ModifiePar ", this.ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@Commission ", this.Commission);
                cmd.Parameters.AddWithValue("@Interet", this.Interet);
                cmd.Parameters.AddWithValue("@BContentieux", this.BContentieux);
                cmd.Parameters.AddWithValue("@RIB", this.RIB);
                cmd.Parameters.AddWithValue("@CAgence", this.CAgence);
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


        public void InsererTrac(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ReglementTrac_Inserer";

                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@BTransfereComptabilite ", this.BTransfereComptabilite);
                cmd.Parameters.AddWithValue("@BRegularisationImpaye ", this.BRegularisationImpaye);
                cmd.Parameters.AddWithValue("@BAnnulation", this.BAnnulation);
                cmd.Parameters.AddWithValue("@BDebit", this.BDebit);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@ObjetReglement", this.ObjetReglement);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@DateAnnulation", this.DateAnnulation);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@NFeuilleCaisse", this.NFeuilleCaisse);
                cmd.Parameters.AddWithValue("@BaseRetenu", this.BaseRetenu);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateInsertion);
                cmd.Parameters.AddWithValue("@CreePar ", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@Commission ", this.Commission);
                cmd.Parameters.AddWithValue("@Interet", this.Interet);
                cmd.Parameters.AddWithValue("@Indice", this.Indice);
                cmd.Parameters.AddWithValue("@ModifiePar ", this.ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@DateModification ", DateTime.Now);
                cmd.Parameters.AddWithValue("@BContentieux ", this.BContentieux);
                cmd.Parameters.AddWithValue("@RIB ", this.RIB);
                cmd.Parameters.AddWithValue("@CAgence ", this.CAgence);
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

        public void AnnulerInserer(Reglement reglement)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    reglement.InsererTrac(transaction);
                    this.Modif(reglement.CReglement,transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static void LibereGarantie(string cReglement, string cReglementGarantie, string cClient, decimal montant)
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
                    cmd.CommandText = "ReglementGarantie_Liberer";

                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@CReglementGarantie", cReglementGarantie);
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@Montant", montant);

                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void LibereContentieux(string cReglement, string cDocument, string typeDocument, decimal montant,int annulerPar)
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
                    cmd.CommandText = "RegFactureContentieux_Liberer";

                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@CDocument", cDocument);
                    cmd.Parameters.AddWithValue("@TypeDocument", typeDocument);
                    cmd.Parameters.AddWithValue("@Montant", montant);
                    cmd.Parameters.AddWithValue("@AnnulerPar", annulerPar);
                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public static void LibereImpaye(string cReglement, string cReglementImpaye, string cClient, decimal montant)
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
                    cmd.CommandText = "ReglementImpayé_Liberer";

                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@CReglementImpaye", cReglementImpaye);
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@Montant", montant);

                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        ////public void ModifierEtatReglement(SqlTransaction transaction)

        ////{
        ////    try
        ////    {
        ////        SqlCommand cmd = new SqlCommand();
        ////        cmd.Transaction = transaction;
        ////        cmd.Connection = transaction.Connection;
        ////        cmd.CommandType = CommandType.StoredProcedure;

        ////        cmd.CommandText = "Reglement_ModifierEtat";
        ////        cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
        ////        cmd.Parameters.AddWithValue("@CEtatReglement", this.CEtatReglement);
        ////        cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
        ////        cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
        ////        cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
        ////        cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

        ////        foreach (SqlParameter parametre in cmd.Parameters)
        ////        {
        ////            if (parametre.Value == null)
        ////            {
        ////                parametre.Value = DBNull.Value;
        ////            }
        ////        }
        ////        cmd.ExecuteNonQuery();
        ////    }

        ////    catch (Exception ex)
        ////    {
        ////        throw ex;
        ////    }
        ////}
    }

    [Serializable]
    public class ReglementCollection : List<Reglement>
    {
        public ReglementCollection()
        {
        }

        public static ReglementCollection reglementImpayer(string cClient)
        {
            ReglementCollection reglementCollection = new ReglementCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Reglement_Impayer";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Reglement reglement = new Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            reglementCollection.Add(reglement);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return reglementCollection;
        }

        public static ReglementCollection Charger()
        {
            ReglementCollection reglementCollection = new ReglementCollection();

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
                    cmd.CommandText = "Reglement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Reglement reglement = new Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                reglement.CClient = dr["CClient"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["NFeuilleCaisse"] != DBNull.Value)
                                reglement.NFeuilleCaisse = int.Parse(dr["NFeuilleCaisse"].ToString());
                            if (dr["BaseRetenu"] != DBNull.Value)
                                reglement.BaseRetenu = decimal.Parse(dr["BaseRetenu"].ToString());
                            if (dr["CVendeur"] != DBNull.Value)
                                reglement.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["ObjetReglement	"] != DBNull.Value)
                                reglement.ObjetReglement = dr["ObjetReglement	"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                reglement.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["RIB"] != DBNull.Value)
                                reglement.RIB = dr["RIB"].ToString();
                            if (dr["CAgence"] != DBNull.Value)
                                reglement.CAgence = dr["CAgence"].ToString();
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (reglementCollection);
        }

        public static void MettreAJourEtatEcheanceReglements()
        {
            int delaiEcheance = VenteHelper.DELAIE_ECHEANCE;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Reglement_MettreAJourEtatEcheance";
                    cmd.Parameters.AddWithValue("@DelaiEcheance", delaiEcheance);
                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}