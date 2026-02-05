using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Referentiel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class Client
    {
        #region Proprietes

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CRegion")]
        [Bindable(true)]
        public string CRegion { get; set; }

        [XmlAttribute("CGouvernorat")]
        [Bindable(true)]
        public string CGouvernorat { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Abreviation")]
        [Bindable(true)]
        public string Abreviation { get; set; }

        [XmlAttribute("BActif")]
        [Bindable(true)]
        public bool BActif { get; set; }

        [XmlAttribute("CClientFamille")]
        [Bindable(true)]
        public string CClientFamille { get; set; }

        [XmlAttribute("CGroupe")]
        [Bindable(true)]
        public string CGroupe { get; set; }

        [XmlAttribute("CPays")]
        [Bindable(true)]
        public string CPays { get; set; }

        [XmlAttribute("CRecouvreur")]
        [Bindable(true)]
        public int CRecouvreur { get; set; }

        [XmlAttribute("CSpeciale")]
        [Bindable(true)]
        public string CSpeciale { get; set; }

        [XmlAttribute("CTarif")]
        [Bindable(true)]
        public string CTarif { get; set; }

        [XmlAttribute("CTVA")]
        [Bindable(true)]
        public string CTVA { get; set; }

        [XmlAttribute("CModeReglement")]
        [Bindable(true)]
        public string CModeReglement { get; set; }

        [XmlAttribute("CVendeur")]
        [Bindable(true)]
        public int CVendeur { get; set; }

        [XmlAttribute("TypeContrat")]
        [Bindable(true)]
        public string TypeContrat { get; set; }

        [XmlAttribute("DateFinExonoreFodec")]
        [Bindable(true)]
        public DateTime? DateFinExonoreFodec { get; set; }

        [XmlAttribute("DateFinExonoreTPE")]
        [Bindable(true)]
        public DateTime? DateFinExonoreTPE { get; set; }

        [XmlAttribute("DateFinExonoreTDC")]
        [Bindable(true)]
        public DateTime? DateFinExonoreTDC { get; set; }

        [XmlAttribute("DateFinExonoreTVA")]
        [Bindable(true)]
        public DateTime? DateFinExonoreTVA { get; set; }

        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email { get; set; }

        [XmlAttribute("BFodecExonore")]
        [Bindable(true)]
        public bool BFodecExonore { get; set; }

        [XmlAttribute("BTPEExonore")]
        [Bindable(true)]
        public bool BTPEExonore { get; set; }

        [XmlAttribute("BTDCExonore")]
        [Bindable(true)]
        public bool BTDCExonore { get; set; }

        [XmlAttribute("BTimbreExonore")]
        [Bindable(true)]
        public bool BTimbreExonore { get; set; }

        [XmlAttribute("BContentieux")]
        [Bindable(true)]
        public bool BContentieux { get; set; }

        [XmlAttribute("BTVAExonore")]
        [Bindable(true)]
        public bool BTVAExonore { get; set; }

        [XmlAttribute("Fax")]
        [Bindable(true)]
        public string Fax { get; set; }

        [XmlAttribute("MontantCreditMax")]
        [Bindable(true)]
        public decimal MontantCreditMax { get; set; }

        [XmlAttribute("MontantCreditMin")]
        [Bindable(true)]
        public decimal MontantCreditMin { get; set; }

        [XmlAttribute("MontantExonoreTVA ")]
        [Bindable(true)]
        public decimal MontantExonoreTVA { get; set; }

        [XmlAttribute("NbJourEcheancePaiment")]
        [Bindable(true)]
        public int NbJourEcheancePaiment { get; set; }

        [XmlAttribute("NbJourCreditFacture ")]
        [Bindable(true)]
        public int NbJourCreditFacture { get; set; }

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

        [XmlAttribute("NumCIN ")]
        [Bindable(true)]
        public string NumCIN { get; set; }

        [XmlAttribute("NumTimbre")]
        [Bindable(true)]
        public string NumTimbre { get; set; }

        [XmlAttribute("ObservationClient")]
        [Bindable(true)]
        public string ObservationClient { get; set; }

        [XmlAttribute("BPassager")]
        [Bindable(true)]
        public bool BPassager { get; set; }

        [XmlAttribute("TauxRemise")]
        [Bindable(true)]
        public decimal TauxRemise { get; set; }

        [XmlAttribute("SoldeAvanceRestant")]
        [Bindable(true)]
        public decimal SoldeAvanceRestant { get; set; }

        [XmlAttribute("SoldeAvoirRestant")]
        [Bindable(true)]
        public decimal SoldeAvoirRestant { get; set; }

        [XmlAttribute("SoldeBonRetour")]
        [Bindable(true)]
        public decimal SoldeBonRetour { get; set; }

        [XmlAttribute("SoldeAnterieur")]
        [Bindable(true)]
        public decimal SoldeAnterieur { get; set; }

        [XmlAttribute("SoldeBonLivraison")]
        [Bindable(true)]
        public decimal SoldeBonLivraison { get; set; }

        [XmlAttribute("SoldeFacture")]
        [Bindable(true)]
        public decimal SoldeFacture { get; set; }

        [XmlAttribute("SoldeImpaye")]
        [Bindable(true)]
        public decimal SoldeImpaye { get; set; }

        [XmlAttribute("TauxRetenuSource")]
        [Bindable(true)]
        public decimal TauxRetenuSource { get; set; }

        [XmlAttribute("TauxRetenuTVA")]
        [Bindable(true)]
        public decimal TauxRetenuTVA { get; set; }

        [XmlAttribute("BMajoration")]
        [Bindable(true)]
        public bool BMajoration { get; set; }

        [XmlAttribute("NumeroTelephone1")]
        [Bindable(true)]
        public string NumeroTelephone1 { get; set; }

        [XmlAttribute("NumeroTelephone2")]
        [Bindable(true)]
        public string NumeroTelephone2 { get; set; }



        [XmlAttribute(" BTransfertCompta")]
        [Bindable(true)]
        public bool BTransfertCompta { get; set; }

        [XmlAttribute("BVIP")]
        [Bindable(true)]
        public bool BVIP { get; set; }

        [XmlAttribute("CNatureTiers")]
        [Bindable(true)]
        public int CNatureTiers { get; set; }

        [XmlAttribute("BTransfert ")]
        [Bindable(true)]
        public bool BTransfert { get; set; }

        [XmlAttribute("NumAutorisation")]
        [Bindable(true)]
        public string NumAutorisation { get; set; }

        [XmlAttribute("DateDebutAutorisation")]
        [Bindable(true)]
        public DateTime? DateDebutAutorisation { get; set; }

        [XmlAttribute("BPaiementAvance")]
        [Bindable(true)]
        public bool BPaiementAvance { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        [XmlAttribute("BInitialisationRemise")]
        [Bindable(true)]
        public bool BInitialisationRemise { get; set; }

        [XmlAttribute("RemiseExeptionnel")]
        [Bindable(true)]
        public decimal RemiseExeptionnel { get; set; }

        [XmlAttribute("DateFinAutorisation")]
        [Bindable(true)]
        public DateTime? DateFinAutorisation { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("Adresses")]
        [Bindable(true)]
        public AdresseCollection Adresses { get; set; }

        [XmlAttribute("Contacts")]
        [Bindable(true)]
        public ClientContactCollection Contacts { get; set; }

        [XmlAttribute("Banques")]
        [Bindable(true)]
        public ClientBanqueCollection Banques { get; set; }

        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }

        [XmlAttribute("DateFacture")]
        [Bindable(true)]
        public DateTime DateFacture { get; set; }

        [XmlAttribute("MontantTTCFacture")]
        [Bindable(true)]
        public decimal MontantTTCFacture { get; set; }

        [XmlAttribute("BEtablissement")]
        [Bindable(true)]
        public bool BEtablissement { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("EmplacementScan")]
        [Bindable(true)]
        public string EmplacementScan { get; set; }

        [XmlAttribute("Etablissements")]
        [Bindable(true)]
        public EtablissementColl Etablissements { get; set; }

        [XmlAttribute("Longitude")]
        [Bindable(true)]
        public decimal Longitude { get; set; }

        [XmlAttribute("Latitude")]
        [Bindable(true)]
        public decimal Latitude { get; set; }

        [XmlAttribute("MotDePasse")]
        [Bindable(true)]
        public string MotDePasse { get; set; }

        [XmlAttribute("BElimines")]
        [Bindable(true)]
        public bool BElimines { get; set; }

        public decimal PrcFodecApplique(decimal prcFodecArticle)
        {
            decimal prcFodec = prcFodecArticle;
            if (!GestionSession.SocieteCourante.BAssujetti)
            {
                prcFodec = 0;
                return prcFodec;
            }
            if ((this.BFodecExonore) && (this.DateFinExonoreFodec != null) && (this.DateFinExonoreFodec >= DateTime.Now.Date))
                prcFodec = 0;
            return prcFodec;
        }

        public bool ExonerationFodec()
        {
            bool exonerationFodec = false;
            if ((this.BFodecExonore) && (this.DateFinExonoreFodec != null) && (this.DateFinExonoreFodec >= DateTime.Now.Date))
                exonerationFodec = true;
            return exonerationFodec;
        }

        public decimal PrcTPEApplique(decimal prcTPEArticle)
        {
            decimal prcTPE = prcTPEArticle;
            if ((this.BTPEExonore) && (this.DateFinExonoreTPE != null) && (this.DateFinExonoreTPE >= DateTime.Now.Date))
                prcTPE = 0;
            return prcTPE;
        }

        public bool ExonerationTPE()
        {
            bool exonerationTPE = false;
            if ((this.BTPEExonore) && (this.DateFinExonoreTPE != null) && (this.DateFinExonoreTPE >= DateTime.Now.Date))
                exonerationTPE = true;
            return exonerationTPE;
        }

        public decimal PrcTDCApplique(decimal prcTDCArticle)
        {
            decimal prcTDC = prcTDCArticle;
            if ((this.BTDCExonore) && (this.DateFinExonoreTDC != null) && (this.DateFinExonoreTDC >= DateTime.Now.Date))
                prcTDC = 0;
            return prcTDC;
        }

        public bool ExonerationTDC()
        {
            bool exonerationTDC = false;
            if ((this.BTDCExonore) && (this.DateFinExonoreTDC != null) && (this.DateFinExonoreTDC >= DateTime.Now.Date))
                exonerationTDC = true;
            return exonerationTDC;
        }


        public decimal TauxTVAApplique(string cTaxe)
        {
            Taxe taxe = Taxe.Charger(cTaxe);
            decimal taux = taxe.Taux1;
            if ((this.BTVAExonore) && (this.DateFinExonoreTVA != null) && (this.DateFinExonoreTVA >= DateTime.Now.Date))
                taux = 0;
            else if (this.BMajoration)
                taux = taxe.Taux2;

            return taux;
        }

        public bool ExonerationTVA()
        {
            bool exonerationTVA = false;
            if ((this.BTVAExonore) && (this.DateFinExonoreTVA != null) && (this.DateFinExonoreTVA >= DateTime.Now.Date))
                exonerationTVA = true;
            return exonerationTVA;
        }

        public decimal RemiseApplique(int prioriteRemise, decimal remiseSaisie, decimal remiseArticle, decimal remiseMax, bool focusedRemise)
        {
            if (this.BVIP)
                return remiseSaisie;
            else
            {
                decimal remise = 0;
                switch (prioriteRemise)
                {
                    case 0:
                        {
                            remise = this.TauxRemise;
                            break;
                        }
                    case 1:
                        {
                            remise = remiseArticle;
                            break;
                        }
                    case 2:
                        {
                            if (remiseArticle < this.TauxRemise)
                                remise = remiseArticle;
                            else
                                remise = this.TauxRemise;

                            break;
                        }
                    case 3:
                        {
                            if (remiseArticle < this.TauxRemise)
                                remise = this.TauxRemise;
                            else
                                remise = remiseArticle;

                            break;
                        }
                    case 4:
                        {
                            remise = this.TauxRemise + remiseArticle;
                            break;
                        }
                    case 5:
                        {
                            remise = (remiseArticle + this.TauxRemise) / 2;

                            break;
                        }
                    case 6:
                        {
                            remise = remiseSaisie;
                            break;
                        }
                    default: break;
                }
                if (focusedRemise)
                {

                    if (prioriteRemise == 1)
                    {
                        if (remiseSaisie > remise)
                        {
                            if (remiseSaisie > remiseMax)
                                return remiseMax;
                            else return remiseSaisie;
                        }
                        else return remiseSaisie;
                    }
                    else
                        /*if (remiseSaisie > remise)
                            return remise;
                        else */
                        return remiseSaisie;
                }

                return remise;
            }
        }

        #endregion Proprietes

        public Client()
        {

            this.Banques = new ClientBanqueCollection();
            this.Adresses = new AdresseCollection();
            this.Contacts = new ClientContactCollection();
            this.Etablissements = new EtablissementColl();
            this.DateInsertion = DateTime.Now;
            this.DateModification = DateTime.Now;
        }

        public Client(string cClient)
        {
            this.CClient = cClient;
            this.Banques = new ClientBanqueCollection();
            this.Adresses = new AdresseCollection();
            this.Contacts = new ClientContactCollection();
            this.Etablissements = new EtablissementColl();
        }

        public void Sauvegarder()
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

                    cmd.CommandText = "Client_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@CRegion", CRegion);
                    cmd.Parameters.AddWithValue("@CGouvernorat", CGouvernorat);
                    cmd.Parameters.AddWithValue("@AbreviationClient", Abreviation);
                    cmd.Parameters.AddWithValue("@BActifClient", BActif);
                    cmd.Parameters.AddWithValue("@CClientFamille", CClientFamille);
                    cmd.Parameters.AddWithValue("@CGroupe", CGroupe);
                    cmd.Parameters.AddWithValue("@CPays", CPays);
                    cmd.Parameters.AddWithValue("@CRecouvreur", CRecouvreur);
                    cmd.Parameters.AddWithValue("@CSpeciale", CSpeciale);
                    cmd.Parameters.AddWithValue("@CTarif", CTarif);
                    cmd.Parameters.AddWithValue("@CTVA", CTVA);
                    cmd.Parameters.AddWithValue("@CModeReglement", CModeReglement);
                    cmd.Parameters.AddWithValue("@CVendeur", CVendeur);
                    cmd.Parameters.AddWithValue("@TypeContrat", TypeContrat);
                    cmd.Parameters.AddWithValue("@DateFinExonoreFodec", DateFinExonoreFodec);
                    cmd.Parameters.AddWithValue("@DateFinExonoreTPE", DateFinExonoreTPE);
                    cmd.Parameters.AddWithValue("@DateFinExonoreTDC", DateFinExonoreTDC);
                    cmd.Parameters.AddWithValue("@DateFinExonoreTVA", DateFinExonoreTVA);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@BFodecExonore", BFodecExonore);
                    cmd.Parameters.AddWithValue("@BTPEExonore", BTPEExonore);
                    cmd.Parameters.AddWithValue("@BTDCExonore", BTDCExonore);
                    cmd.Parameters.AddWithValue("@BTimbreExonore", BTimbreExonore);
                    cmd.Parameters.AddWithValue("@BContentieux", BContentieux);
                    cmd.Parameters.AddWithValue("@BTVAExonore", BTVAExonore);
                    cmd.Parameters.AddWithValue("@Fax", Fax);
                    cmd.Parameters.AddWithValue("@MontantCreditMax", MontantCreditMax);
                    cmd.Parameters.AddWithValue("@MontantCreditMin", MontantCreditMin);
                    cmd.Parameters.AddWithValue("@MontantExonoreTVA", MontantExonoreTVA);
                    cmd.Parameters.AddWithValue("@NbJourEcheancePaiment", NbJourEcheancePaiment);
                    cmd.Parameters.AddWithValue("@NbJourCreditFacture", NbJourCreditFacture);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    cmd.Parameters.AddWithValue("@NumCIN", NumCIN);
                    cmd.Parameters.AddWithValue("@NumTimbre", NumTimbre);
                    cmd.Parameters.AddWithValue("@ObservationClient", ObservationClient);
                    cmd.Parameters.AddWithValue("@BPassager", BPassager);
                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                    cmd.Parameters.AddWithValue("@TauxRemise", TauxRemise);
                    cmd.Parameters.AddWithValue("@TauxRetenuSource", TauxRetenuSource);
                    cmd.Parameters.AddWithValue("@TauxRetenuTVA", TauxRetenuTVA);
                    cmd.Parameters.AddWithValue("@BMajoration", BMajoration);
                    cmd.Parameters.AddWithValue("@NumeroTelephone1", NumeroTelephone1);
                    cmd.Parameters.AddWithValue("@NumeroTelephone2", NumeroTelephone2);
                    cmd.Parameters.AddWithValue("@BTransfertCompta", BTransfertCompta);
                    cmd.Parameters.AddWithValue("@BVIP", BVIP);
                    cmd.Parameters.AddWithValue("@CNatureTiers", CNatureTiers);
                    cmd.Parameters.AddWithValue("@BTransfert", BTransfert);
                    cmd.Parameters.AddWithValue("@NumAutorisation", NumAutorisation);
                    cmd.Parameters.AddWithValue("@DateDebutAutorisation", DateDebutAutorisation);
                    cmd.Parameters.AddWithValue("@BPaiementAvance", BPaiementAvance);
                    cmd.Parameters.AddWithValue("@BAvanceForfaitaire", BAvanceForfaitaire);
                    cmd.Parameters.AddWithValue("@BInitialisationRemise", BInitialisationRemise);
                    cmd.Parameters.AddWithValue("@RemiseExeptionnel", RemiseExeptionnel);
                    cmd.Parameters.AddWithValue("@DateFinAutorisation", DateFinAutorisation);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BEtablissement", BEtablissement);
                    cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                    cmd.Parameters.AddWithValue("@EmplacementScan", EmplacementScan);
                    cmd.Parameters.AddWithValue("@Latitude", Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", Longitude);
                    cmd.Parameters.AddWithValue("@Password", MotDePasse);
                    cmd.Parameters.AddWithValue("@BElimines", this.BElimines);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    this.SupprimerClientBanques(transaction);
                    this.SupprimerClientAdresses(transaction);
                    this.SupprimerClientContacts(transaction);
                    // this.SupprimerClientEtablissements(transaction);
                    foreach (ClientBanque banque in Banques)
                    {
                        banque.Sauvegarder(transaction);
                    }

                    foreach (Adresse adresse in Adresses)
                    {
                        adresse.Sauvegarder(transaction);
                    }

                    foreach (ClientContact contact in Contacts)
                    {
                        contact.Sauvegarder(transaction);
                    }
                    foreach (Etablissement etablissement in Etablissements)
                    {
                        etablissement.Sauvegarder(transaction);
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

        private void SupprimerClientEtablissements(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_SupprimerEtablissements";

                cmd.Parameters.AddWithValue("@CClient", this.CClient);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
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

                    foreach (ClientBanque banque in Banques)
                    {
                        banque.Supprimer(transaction);
                    }

                    foreach (Adresse adresse in Adresses)
                    {
                        adresse.Supprimer(transaction);
                    }
                    foreach (ClientContact contact in Contacts)
                    {
                        contact.Supprimer(transaction);
                    }
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Supprimer";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
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

        private void SupprimerClientBanques(SqlTransaction transaction)
        {
            ClientBanqueCollection anciensBanque = ClientBanqueCollection.Charger(CClient);
            foreach (ClientBanque item in anciensBanque)
            {
                if (!this.Banques.Exists(p => p.CClient.Equals(item.CClient) && p.CBanque.Equals(item.CBanque) && p.RIBClient.Equals(item.RIBClient)))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerClientContacts(SqlTransaction transaction)
        {
            ClientContactCollection anciensContacts = ClientContactCollection.Charger(CClient);
            foreach (ClientContact item in anciensContacts)
            {
                if (!this.Contacts.Exists(p => p.CClient == item.CClient && p.CContact == item.CContact))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerClientAdresses(SqlTransaction transaction)
        {
            AdresseCollection anciennesAdresses = AdresseCollection.Charger(CClient);
            foreach (Adresse item in anciennesAdresses)
            {
                if (!this.Adresses.Exists(p => p.NTiers == item.NTiers && p.IdAdresse == item.IdAdresse))
                    item.Supprimer(transaction);
            }
        }

        public static string NouveauCodeClient()
        {
            string code = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_ChargerCode";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            code = dr["CClient"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (code != null)
            {

                string cClient = (int.Parse(code) + 1).ToString();
                while (Client.Charger(cClient) != null)
                {
                    cClient = (int.Parse(cClient) + 1).ToString();
                }
                return cClient;
            }
            return ("41100001");
        }

        public static CST.LePoint.Referentiel.ItemCollection chargerRaisonSociale(string cClient)
        {
            CST.LePoint.Referentiel.ItemCollection clientlist = new CST.LePoint.Referentiel.ItemCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "RaisonSociale_Charger";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        int row = 1;
                        while (dr.Read())
                        {
                            CST.LePoint.Referentiel.Item item = new LePoint.Referentiel.Item();
                            if (dr["RaisonSociale"] != DBNull.Value)
                            {
                                item.Code = row.ToString();
                                item.Libelle = dr["RaisonSociale"].ToString();
                                clientlist.Add(item);
                            }
                            row++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientlist;
        }

        public static Client Charger(string cClient)
        {
            Client client = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Charger";
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = new Client();
                            client.CClient = dr["CClient"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                client.CRegion = dr["CRegion"].ToString();
                            if (dr["CGouvernorat"] != DBNull.Value)
                                client.CGouvernorat = dr["CGouvernorat"].ToString();
                            if (dr["AbreviationClient"] != DBNull.Value)
                                client.Abreviation = dr["AbreviationClient"].ToString();
                            if (dr["BActifClient"] != DBNull.Value)
                                client.BActif = bool.Parse(dr["BActifClient"].ToString());
                            if (dr["CClientFamille"] != DBNull.Value)
                                client.CClientFamille = dr["CClientFamille"].ToString();
                            if (dr["CGroupe"] != DBNull.Value)
                                client.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CPays"] != DBNull.Value)
                                client.CPays = dr["CPays"].ToString();
                            if (dr["CRecouvreur"] != DBNull.Value)
                                client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                            if (dr["CSpeciale"] != DBNull.Value)
                                client.CSpeciale = dr["CSpeciale"].ToString();
                            if (dr["CTarif"] != DBNull.Value)
                                client.CTarif = dr["CTarif"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                client.CTVA = dr["CTVA"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                client.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                client.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["TypeContrat"] != DBNull.Value)
                                client.TypeContrat = dr["TypeContrat"].ToString();
                            if (dr["DateFinExonoreFodec"] != DBNull.Value)
                                client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
                            if (dr["DateFinExonoreTVA"] != DBNull.Value)
                                client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                client.Email = dr["Email"].ToString();

                            if (dr["DateFinExonoreTPE"] != DBNull.Value)
                                client.DateFinExonoreTPE = DateTime.Parse(dr["DateFinExonoreTPE"].ToString());
                            if (dr["DateFinExonoreTDC"] != DBNull.Value)
                                client.DateFinExonoreTDC = DateTime.Parse(dr["DateFinExonoreTDC"].ToString());
                            if (dr["BTPEExonore"] != DBNull.Value)
                                client.BTPEExonore = bool.Parse(dr["BTPEExonore"].ToString());
                            if (dr["BTDCExonore"] != DBNull.Value)
                                client.BTDCExonore = bool.Parse(dr["BTDCExonore"].ToString());

                            if (dr["BFodecExonore"] != DBNull.Value)
                                client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["BTimbreExonore"] != DBNull.Value)
                                client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["BTVAExonore"] != DBNull.Value)
                                client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                client.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMax"] != DBNull.Value)
                                client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
                            if (dr["MontantCreditMin"] != DBNull.Value)
                                client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
                            if (dr["MontantExonoreTVA"] != DBNull.Value)
                                client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
                            if (dr["NbJourEcheancePaiment"] != DBNull.Value)
                                client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
                            if (dr["NbJourCreditFacture"] != DBNull.Value)
                                client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                client.Nom = dr["Nom"].ToString();
                            if (dr["NumCIN"] != DBNull.Value)
                                client.NumCIN = dr["NumCIN"].ToString();
                            if (dr["NumTimbre"] != DBNull.Value)
                                client.NumTimbre = dr["NumTimbre"].ToString();
                            if (dr["ObservationClient"] != DBNull.Value)
                                client.ObservationClient = dr["ObservationClient"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                client.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                client.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TauxRemise"] != DBNull.Value)
                                client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
                            if (dr["SoldeAvanceRestant"] != DBNull.Value)
                                client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
                            if (dr["SoldeAvoirRestant"] != DBNull.Value)
                                client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
                            if (dr["SoldeBonRetour"] != DBNull.Value)
                                client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
                            if (dr["SoldeAnterieur"] != DBNull.Value)
                                client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
                            if (dr["SoldeBonLivraison"] != DBNull.Value)
                                client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
                            if (dr["SoldeFacture"] != DBNull.Value)
                                client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
                            if (dr["SoldeImpaye"] != DBNull.Value)
                                client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
                            if (dr["TauxRetenuSource"] != DBNull.Value)
                                client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
                            if (dr["TauxRetenuTVA"] != DBNull.Value)
                                client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["NumeroTelephone1"] != DBNull.Value)
                                client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            if (dr["NumeroTelephone2"] != DBNull.Value)
                                client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            if (dr["BTransfertCompta"] != DBNull.Value)
                                client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
                            if (dr["BVIP"] != DBNull.Value)
                                client.BVIP = bool.Parse(dr["BVIP"].ToString());
                            if (dr["CNatureTiers"] != DBNull.Value)
                                client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["BTransfert"] != DBNull.Value)
                                client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
                            if (dr["NumAutorisation"] != DBNull.Value)
                                client.NumAutorisation = dr["NumAutorisation"].ToString();
                            if (dr["DateDebutAutorisation"] != DBNull.Value)
                                client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
                            if (dr["BPaiementAvance"] != DBNull.Value)
                                client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BInitialisationRemise"] != DBNull.Value)
                                client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
                            if (dr["RemiseExeptionnel"] != DBNull.Value)
                                client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
                            if (dr["DateFinAutorisation"] != DBNull.Value)
                                client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
                            if (dr["NFacture"] != DBNull.Value)
                                client.NFacture = dr["NFacture"].ToString();
                            if (dr["MontantTTCFacture"] != DBNull.Value)
                                client.MontantTTCFacture = decimal.Parse(dr["MontantTTCFacture"].ToString());
                            if (dr["DateFacture"] != DBNull.Value)
                                client.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if (dr["BEtablissement"] != DBNull.Value)
                                client.BEtablissement = bool.Parse(dr["BEtablissement"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                client.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["EmplacementScan"] != DBNull.Value)
                                client.EmplacementScan = dr["EmplacementScan"].ToString();
                            if (dr["Longitude"] != DBNull.Value)
                                client.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                client.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Password"] != DBNull.Value)
                                client.MotDePasse = dr["Password"].ToString();
                            client.BElimines = bool.Parse(dr["BElimines"].ToString());

                            client.Banques = ClientBanqueCollection.Charger(client.CClient);
                            client.Adresses = AdresseCollection.Charger(client.CClient);
                            client.Contacts = ClientContactCollection.Charger(client.CClient);
                            client.Etablissements = EtablissementColl.Charger(client.CClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return client;
        }

        public static Client ChargerVue(string cClient)
        {

            Client client = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_ChargerVue";
                    cmd.Parameters.AddWithValue("@CClient", cClient);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = new Client();
                            client.CClient = dr["CClient"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                client.CRegion = dr["CRegion"].ToString();
                            if (dr["CGouvernorat"] != DBNull.Value)
                                client.CGouvernorat = dr["CGouvernorat"].ToString();
                            if (dr["AbreviationClient"] != DBNull.Value)
                                client.Abreviation = dr["AbreviationClient"].ToString();
                            if (dr["BActifClient"] != DBNull.Value)
                                client.BActif = bool.Parse(dr["BActifClient"].ToString());
                            if (dr["CClientFamille"] != DBNull.Value)
                                client.CClientFamille = dr["CClientFamille"].ToString();
                            if (dr["CGroupe"] != DBNull.Value)
                                client.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CPays"] != DBNull.Value)
                                client.CPays = dr["CPays"].ToString();
                            if (dr["CRecouvreur"] != DBNull.Value)
                                client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                            if (dr["CSpeciale"] != DBNull.Value)
                                client.CSpeciale = dr["CSpeciale"].ToString();
                            if (dr["CTarif"] != DBNull.Value)
                                client.CTarif = dr["CTarif"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                client.CTVA = dr["CTVA"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                client.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                client.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["TypeContrat"] != DBNull.Value)
                                client.TypeContrat = dr["TypeContrat"].ToString();
                            if (dr["DateFinExonoreFodec"] != DBNull.Value)
                                client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
                            if (dr["DateFinExonoreTVA"] != DBNull.Value)
                                client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                client.Email = dr["Email"].ToString();

                            if (dr["DateFinExonoreTPE"] != DBNull.Value)
                                client.DateFinExonoreTPE = DateTime.Parse(dr["DateFinExonoreTPE"].ToString());
                            if (dr["DateFinExonoreTDC"] != DBNull.Value)
                                client.DateFinExonoreTDC = DateTime.Parse(dr["DateFinExonoreTDC"].ToString());
                            if (dr["BTPEExonore"] != DBNull.Value)
                                client.BTPEExonore = bool.Parse(dr["BTPEExonore"].ToString());
                            if (dr["BTDCExonore"] != DBNull.Value)
                                client.BTDCExonore = bool.Parse(dr["BTDCExonore"].ToString());

                            if (dr["BFodecExonore"] != DBNull.Value)
                                client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["BTimbreExonore"] != DBNull.Value)
                                client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
                            if (dr["BTVAExonore"] != DBNull.Value)
                                client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                client.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMax"] != DBNull.Value)
                                client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
                            if (dr["MontantCreditMin"] != DBNull.Value)
                                client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
                            if (dr["MontantExonoreTVA"] != DBNull.Value)
                                client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
                            if (dr["NbJourEcheancePaiment"] != DBNull.Value)
                                client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
                            if (dr["NbJourCreditFacture"] != DBNull.Value)
                                client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                client.Nom = dr["Nom"].ToString();
                            if (dr["NumCIN"] != DBNull.Value)
                                client.NumCIN = dr["NumCIN"].ToString();
                            if (dr["NumTimbre"] != DBNull.Value)
                                client.NumTimbre = dr["NumTimbre"].ToString();
                            if (dr["ObservationClient"] != DBNull.Value)
                                client.ObservationClient = dr["ObservationClient"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                client.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                client.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TauxRemise"] != DBNull.Value)
                                client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
                            if (dr["SoldeAvanceRestant"] != DBNull.Value)
                                client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
                            if (dr["SoldeAvoirRestant"] != DBNull.Value)
                                client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
                            if (dr["SoldeBonRetour"] != DBNull.Value)
                                client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
                            if (dr["SoldeAnterieur"] != DBNull.Value)
                                client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
                            if (dr["SoldeBonLivraison"] != DBNull.Value)
                                client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
                            if (dr["SoldeFacture"] != DBNull.Value)
                                client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
                            if (dr["SoldeImpaye"] != DBNull.Value)
                                client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
                            if (dr["TauxRetenuSource"] != DBNull.Value)
                                client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
                            if (dr["TauxRetenuTVA"] != DBNull.Value)
                                client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["NumeroTelephone1"] != DBNull.Value)
                                client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            if (dr["NumeroTelephone2"] != DBNull.Value)
                                client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            if (dr["BTransfertCompta"] != DBNull.Value)
                                client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
                            if (dr["BVIP"] != DBNull.Value)
                                client.BVIP = bool.Parse(dr["BVIP"].ToString());
                            if (dr["CNatureTiers"] != DBNull.Value)
                                client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["BTransfert"] != DBNull.Value)
                                client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
                            if (dr["NumAutorisation"] != DBNull.Value)
                                client.NumAutorisation = dr["NumAutorisation"].ToString();
                            if (dr["DateDebutAutorisation"] != DBNull.Value)
                                client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
                            if (dr["BPaiementAvance"] != DBNull.Value)
                                client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BInitialisationRemise"] != DBNull.Value)
                                client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
                            if (dr["RemiseExeptionnel"] != DBNull.Value)
                                client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
                            if (dr["DateFinAutorisation"] != DBNull.Value)
                                client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
                            if (dr["NFacture"] != DBNull.Value)
                                client.NFacture = dr["NFacture"].ToString();
                            if (dr["MontantTTCFacture"] != DBNull.Value)
                                client.MontantTTCFacture = decimal.Parse(dr["MontantTTCFacture"].ToString());
                            if (dr["DateFacture"] != DBNull.Value)
                                client.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if (dr["BEtablissement"] != DBNull.Value)
                                client.BEtablissement = bool.Parse(dr["BEtablissement"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                client.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["EmplacementScan"] != DBNull.Value)
                                client.EmplacementScan = dr["EmplacementScan"].ToString();
                            if (dr["Longitude"] != DBNull.Value)
                                client.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                client.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Password"] != DBNull.Value)
                                client.MotDePasse = dr["Password"].ToString();
                            client.BElimines = bool.Parse(dr["BElimines"].ToString());

                            client.Banques = ClientBanqueCollection.Charger(client.CClient);
                            client.Adresses = AdresseCollection.Charger(client.CClient);
                            client.Contacts = ClientContactCollection.Charger(client.CClient);
                            client.Etablissements = EtablissementColl.Charger(client.CClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return client;
        }

        public static Client Soldes(Client client)
        {
            string sqlSoldeFacture = string.Empty;
            sqlSoldeFacture = sqlSoldeFacture + "SELECT SUM(ISNULL(F.CreditFacture,0)) AS SoldeFacture FROM Facture F";
            sqlSoldeFacture = sqlSoldeFacture + "WHERE F.CreditFacture<>0 AND F.CClient=" + client.CClient;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmdSoldeFacture = new SqlCommand();
                    cmdSoldeFacture.Connection = cn;
                    cmdSoldeFacture.CommandType = CommandType.Text;
                    cmdSoldeFacture.CommandText = sqlSoldeFacture;


                    using (SqlDataReader dr = cmdSoldeFacture.ExecuteReader())
                    {
                        if (dr.Read())
                        { client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString()); }
                    }
                }
            }
            catch { }
            return client;
        }

        public static bool VerificationRIB(string rib)
        {
            bool valide = false;
            try
            {
                var nRIB = decimal.Parse(rib);
                if (rib.Length == 20 && nRIB % 97 == 0)
                    valide = true;
            }
            catch
            {
                return valide;
            }
            return valide;
        }

        public static Client ChargerParFournisseur(string cFournisseur)
        {

            Client client = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_ChargerParFournisseur";
                    cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            client = new Client();
                            client.CClient = dr["CClient"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                client.CRegion = dr["CRegion"].ToString();
                            if (dr["CGouvernorat"] != DBNull.Value)
                                client.CGouvernorat = dr["CGouvernorat"].ToString();
                            if (dr["AbreviationClient"] != DBNull.Value)
                                client.Abreviation = dr["AbreviationClient"].ToString();
                            if (dr["BActifClient"] != DBNull.Value)
                                client.BActif = bool.Parse(dr["BActifClient"].ToString());
                            if (dr["CClientFamille"] != DBNull.Value)
                                client.CClientFamille = dr["CClientFamille"].ToString();
                            if (dr["CGroupe"] != DBNull.Value)
                                client.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CPays"] != DBNull.Value)
                                client.CPays = dr["CPays"].ToString();
                            if (dr["CRecouvreur"] != DBNull.Value)
                                client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                            if (dr["CSpeciale"] != DBNull.Value)
                                client.CSpeciale = dr["CSpeciale"].ToString();
                            if (dr["CTarif"] != DBNull.Value)
                                client.CTarif = dr["CTarif"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                client.CTVA = dr["CTVA"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                client.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                client.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["TypeContrat"] != DBNull.Value)
                                client.TypeContrat = dr["TypeContrat"].ToString();
                            if (dr["DateFinExonoreFodec"] != DBNull.Value)
                                client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
                            if (dr["DateFinExonoreTVA"] != DBNull.Value)
                                client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                client.Email = dr["Email"].ToString();

                            if (dr["DateFinExonoreTPE"] != DBNull.Value)
                                client.DateFinExonoreTPE = DateTime.Parse(dr["DateFinExonoreTPE"].ToString());
                            if (dr["DateFinExonoreTDC"] != DBNull.Value)
                                client.DateFinExonoreTDC = DateTime.Parse(dr["DateFinExonoreTDC"].ToString());
                            if (dr["BTPEExonore"] != DBNull.Value)
                                client.BTPEExonore = bool.Parse(dr["BTPEExonore"].ToString());
                            if (dr["BTDCExonore"] != DBNull.Value)
                                client.BTDCExonore = bool.Parse(dr["BTDCExonore"].ToString());

                            if (dr["BFodecExonore"] != DBNull.Value)
                                client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["BTimbreExonore"] != DBNull.Value)
                                client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["BTVAExonore"] != DBNull.Value)
                                client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                client.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMax"] != DBNull.Value)
                                client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
                            if (dr["MontantCreditMin"] != DBNull.Value)
                                client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
                            if (dr["MontantExonoreTVA"] != DBNull.Value)
                                client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
                            if (dr["NbJourEcheancePaiment"] != DBNull.Value)
                                client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
                            if (dr["NbJourCreditFacture"] != DBNull.Value)
                                client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                client.Nom = dr["Nom"].ToString();
                            if (dr["NumCIN"] != DBNull.Value)
                                client.NumCIN = dr["NumCIN"].ToString();
                            if (dr["NumTimbre"] != DBNull.Value)
                                client.NumTimbre = dr["NumTimbre"].ToString();
                            if (dr["ObservationClient"] != DBNull.Value)
                                client.ObservationClient = dr["ObservationClient"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                client.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                client.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TauxRemise"] != DBNull.Value)
                                client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
                            if (dr["SoldeAvanceRestant"] != DBNull.Value)
                                client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
                            if (dr["SoldeAvoirRestant"] != DBNull.Value)
                                client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
                            if (dr["SoldeBonRetour"] != DBNull.Value)
                                client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
                            if (dr["SoldeAnterieur"] != DBNull.Value)
                                client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
                            if (dr["SoldeBonLivraison"] != DBNull.Value)
                                client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
                            if (dr["SoldeFacture"] != DBNull.Value)
                                client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
                            if (dr["SoldeImpaye"] != DBNull.Value)
                                client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
                            if (dr["TauxRetenuSource"] != DBNull.Value)
                                client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
                            if (dr["TauxRetenuTVA"] != DBNull.Value)
                                client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["NumeroTelephone1"] != DBNull.Value)
                                client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            if (dr["NumeroTelephone2"] != DBNull.Value)
                                client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            if (dr["BTransfertCompta"] != DBNull.Value)
                                client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
                            if (dr["BVIP"] != DBNull.Value)
                                client.BVIP = bool.Parse(dr["BVIP"].ToString());
                            if (dr["CNatureTiers"] != DBNull.Value)
                                client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["BTransfert"] != DBNull.Value)
                                client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
                            if (dr["NumAutorisation"] != DBNull.Value)
                                client.NumAutorisation = dr["NumAutorisation"].ToString();
                            if (dr["DateDebutAutorisation"] != DBNull.Value)
                                client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
                            if (dr["BPaiementAvance"] != DBNull.Value)
                                client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BInitialisationRemise"] != DBNull.Value)
                                client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
                            if (dr["RemiseExeptionnel"] != DBNull.Value)
                                client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
                            if (dr["DateFinAutorisation"] != DBNull.Value)
                                client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
                            if (dr["NFacture"] != DBNull.Value)
                                client.NFacture = dr["NFacture"].ToString();
                            if (dr["MontantTTCFacture"] != DBNull.Value)
                                client.MontantTTCFacture = decimal.Parse(dr["MontantTTCFacture"].ToString());
                            if (dr["DateFacture"] != DBNull.Value)
                                client.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if (dr["BEtablissement"] != DBNull.Value)
                                client.BEtablissement = bool.Parse(dr["BEtablissement"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                client.CFournisseur = dr["CFournisseur"].ToString();

                            client.Banques = ClientBanqueCollection.Charger(client.CClient);
                            client.Adresses = AdresseCollection.Charger(client.CClient);
                            client.Contacts = ClientContactCollection.Charger(client.CClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return client;
        }

        public static void ImportationClient(SqlTransaction transaction, string Clients)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_Importation";
                cmd.Parameters.AddWithValue("@Clients", Clients);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void FusionnerClientCRM(SqlTransaction transaction, string CClient, string CClientCRM)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_FusionnerClientCRM";
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@CClientCRM", CClientCRM);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static DataTable ChargerClient_AvantImportation()
        {
            DataTable ds = new DataTable();
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
                    cmd.CommandText = "Client_Charger_AvantImportation";

                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return ds;
        }

        public void MettreajourClientCA()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PCInsertion", Environment.UserName);
                    cmd.Parameters.AddWithValue("@CreePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                    cmd.CommandText = "Client_Miseajour_CA";

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

        public void MettreajourClient()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_MiseAJour";

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

        public void MettreajourClientRecouvrement()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandTimeout = 0;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Miseajour_Recouvrement";

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

        public void ImportationBL()
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
                    cmd.CommandText = "Client_Importation_BL";

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
    }

    [Serializable]
    public class ClientCollection : List<Client>
    {
        public ClientCollection()
        {
        }

        public static DataSet ChargerVue(string client, string famille, string pays, int mouvement, string vendeur, int actif, DateTime dateDeb, DateTime dateFin, string region)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_Vue_Rpt_Rechercher2";
                cmd.Parameters.AddWithValue("@CClient", client);
                cmd.Parameters.AddWithValue("@CClientFamille", famille);
                cmd.Parameters.AddWithValue("@CPays", pays);
                cmd.Parameters.AddWithValue("@CVendeur", vendeur);
                cmd.Parameters.AddWithValue("@Actif", actif);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                cmd.Parameters.AddWithValue("@DateDebut", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CRegion", region);

                foreach (SqlParameter parametre in cmd.Parameters)                
                    if (parametre.Value == null)                    
                        parametre.Value = DBNull.Value;                    
                
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Client_Vue_Rpt_Charger2");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cClient, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string FamilleClient, string cVendeur, string cEntrepot, string cRegion, string cPays)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Client_Mvt_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CClient", cClient);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@Region", cRegion);
                cmd.Parameters.AddWithValue("@CPays", cPays);
                cmd.Parameters.AddWithValue("@Vendeur", cVendeur);
                cmd.Parameters.AddWithValue("@FamilleClient", FamilleClient);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Client_Mvt_Rpt_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cClient, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string FamilleClient, string cVendeur, string cEntrepot, string cPays, int mouvement)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ClientMvt_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CClient", cClient);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CPays", cPays);
                cmd.Parameters.AddWithValue("@Vendeur", cVendeur);
                cmd.Parameters.AddWithValue("@FamilleClient", FamilleClient);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Client_Mvt_Rpt_Charger");
            }
            return (ds);
        }

        public static ClientCollection Charger()
        {
            ClientCollection clientCollection = new ClientCollection();

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
                    cmd.CommandText = "Client_Charger";
                    cmd.Parameters.AddWithValue("@CClient", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Client client = new Client();
                            if (dr["CClient"] != DBNull.Value)
                                client.CClient = dr["CClient"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                client.CRegion = dr["CRegion"].ToString();
                            if (dr["AbreviationClient"] != DBNull.Value)
                                client.Abreviation = dr["AbreviationClient"].ToString();
                            if (dr["BActifClient"] != DBNull.Value)
                                client.BActif = bool.Parse(dr["BActifClient"].ToString());
                            if (dr["CClientFamille"] != DBNull.Value)
                                client.CClientFamille = dr["CClientFamille"].ToString();
                            if (dr["CGroupe"] != DBNull.Value)
                                client.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CPays"] != DBNull.Value)
                                client.CPays = dr["CPays"].ToString();
                            if (dr["CRecouvreur"] != DBNull.Value)
                                client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                            if (dr["CSpeciale"] != DBNull.Value)
                                client.CSpeciale = dr["CSpeciale"].ToString();
                            if (dr["CTarif"] != DBNull.Value)
                                client.CTarif = dr["CTarif"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                client.CTVA = dr["CTVA"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                client.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                client.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["TypeContrat"] != DBNull.Value)
                                client.TypeContrat = dr["TypeContrat"].ToString();
                            if (dr["DateFinExonoreFodec"] != DBNull.Value)
                                client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
                            if (dr["DateFinExonoreTVA"] != DBNull.Value)
                                client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                client.Email = dr["Email"].ToString();

                            if (dr["DateFinExonoreTPE"] != DBNull.Value)
                                client.DateFinExonoreTPE = DateTime.Parse(dr["DateFinExonoreTPE"].ToString());
                            if (dr["DateFinExonoreTDC"] != DBNull.Value)
                                client.DateFinExonoreTDC = DateTime.Parse(dr["DateFinExonoreTDC"].ToString());
                            if (dr["BTPEExonore"] != DBNull.Value)
                                client.BTPEExonore = bool.Parse(dr["BTPEExonore"].ToString());
                            if (dr["BTDCExonore"] != DBNull.Value)
                                client.BTDCExonore = bool.Parse(dr["BTDCExonore"].ToString());

                            if (dr["BFodecExonore"] != DBNull.Value)
                                client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["BTimbreExonore"] != DBNull.Value)
                                client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
                            if (dr["BTVAExonore"] != DBNull.Value)
                                client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                client.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMax"] != DBNull.Value)
                                client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
                            if (dr["MontantCreditMin"] != DBNull.Value)
                                client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
                            if (dr["MontantExonoreTVA"] != DBNull.Value)
                                client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
                            if (dr["NbJourEcheancePaiment"] != DBNull.Value)
                                client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
                            if (dr["NbJourCreditFacture"] != DBNull.Value)
                                client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                client.Nom = dr["Nom"].ToString();
                            if (dr["NumCIN"] != DBNull.Value)
                                client.NumCIN = dr["NumCIN"].ToString();
                            if (dr["NumTimbre"] != DBNull.Value)
                                client.NumTimbre = dr["NumTimbre"].ToString();
                            if (dr["ObservationClient"] != DBNull.Value)
                                client.ObservationClient = dr["ObservationClient"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                client.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                client.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TauxRemise"] != DBNull.Value)
                                client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
                            if (dr["SoldeAvanceRestant"] != DBNull.Value)
                                client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
                            if (dr["SoldeAvoirRestant"] != DBNull.Value)
                                client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
                            if (dr["SoldeBonRetour"] != DBNull.Value)
                                client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
                            if (dr["SoldeAnterieur"] != DBNull.Value)
                                client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
                            if (dr["SoldeBonLivraison"] != DBNull.Value)
                                client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
                            if (dr["SoldeFacture"] != DBNull.Value)
                                client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
                            if (dr["SoldeImpaye"] != DBNull.Value)
                                client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
                            if (dr["TauxRetenuSource"] != DBNull.Value)
                                client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
                            if (dr["TauxRetenuTVA"] != DBNull.Value)
                                client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["NumeroTelephone1"] != DBNull.Value)
                                client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            if (dr["NumeroTelephone2"] != DBNull.Value)
                                client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            if (dr["BTransfertCompta"] != DBNull.Value)
                                client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
                            if (dr["BVIP"] != DBNull.Value)
                                client.BVIP = bool.Parse(dr["BVIP"].ToString());
                            if (dr["CNatureTiers"] != DBNull.Value)
                                client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["BTransfert"] != DBNull.Value)
                                client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
                            if (dr["NumAutorisation"] != DBNull.Value)
                                client.NumAutorisation = dr["NumAutorisation"].ToString();
                            if (dr["DateDebutAutorisation"] != DBNull.Value)
                                client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
                            if (dr["BPaiementAvance"] != DBNull.Value)
                                client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BInitialisationRemise"] != DBNull.Value)
                                client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
                            if (dr["RemiseExeptionnel"] != DBNull.Value)
                                client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
                            if (dr["DateFinAutorisation"] != DBNull.Value)
                                client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
                            if (dr["BEtablissement"] != DBNull.Value)
                                client.BEtablissement = bool.Parse(dr["BEtablissement"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                client.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Longitude"] != DBNull.Value)
                                client.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                client.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Password"] != DBNull.Value)
                                client.MotDePasse = dr["Password"].ToString();

                            client.Banques = ClientBanqueCollection.Charger(client.CClient);
                            client.Adresses = AdresseCollection.Charger(client.CClient);
                            client.Contacts = ClientContactCollection.Charger(client.CClient);
                            client.Etablissements = EtablissementColl.Charger(client.CClient);
                            clientCollection.Add(client);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (clientCollection);
        }

        public static ClientCollection ChargerVerfication()
        {
            ClientCollection clientCollection = new ClientCollection();

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
                    cmd.CommandText = "Client_Charger";
                    cmd.Parameters.AddWithValue("@CClient", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Client client = new Client();
                            if (dr["CClient"] != DBNull.Value)
                                client.CClient = dr["CClient"].ToString();
                            if (dr["CRegion"] != DBNull.Value)
                                client.CRegion = dr["CRegion"].ToString();
                            if (dr["AbreviationClient"] != DBNull.Value)
                                client.Abreviation = dr["AbreviationClient"].ToString();
                            if (dr["BActifClient"] != DBNull.Value)
                                client.BActif = bool.Parse(dr["BActifClient"].ToString());
                            if (dr["CClientFamille"] != DBNull.Value)
                                client.CClientFamille = dr["CClientFamille"].ToString();
                            if (dr["CGroupe"] != DBNull.Value)
                                client.CGroupe = dr["CGroupe"].ToString();
                            if (dr["CPays"] != DBNull.Value)
                                client.CPays = dr["CPays"].ToString();
                            if (dr["CRecouvreur"] != DBNull.Value)
                                client.CRecouvreur = int.Parse(dr["CRecouvreur"].ToString());
                            if (dr["CSpeciale"] != DBNull.Value)
                                client.CSpeciale = dr["CSpeciale"].ToString();
                            if (dr["CTarif"] != DBNull.Value)
                                client.CTarif = dr["CTarif"].ToString();
                            if (dr["CTVA"] != DBNull.Value)
                                client.CTVA = dr["CTVA"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                client.CModeReglement = dr["CModeReglement"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                client.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["TypeContrat"] != DBNull.Value)
                                client.TypeContrat = dr["TypeContrat"].ToString();
                            if (dr["DateFinExonoreFodec"] != DBNull.Value)
                                client.DateFinExonoreFodec = DateTime.Parse(dr["DateFinExonoreFodec"].ToString());
                            if (dr["DateFinExonoreTVA"] != DBNull.Value)
                                client.DateFinExonoreTVA = DateTime.Parse(dr["DateFinExonoreTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                client.Email = dr["Email"].ToString();

                            if (dr["DateFinExonoreTPE"] != DBNull.Value)
                                client.DateFinExonoreTPE = DateTime.Parse(dr["DateFinExonoreTPE"].ToString());
                            if (dr["DateFinExonoreTDC"] != DBNull.Value)
                                client.DateFinExonoreTDC = DateTime.Parse(dr["DateFinExonoreTDC"].ToString());
                            if (dr["BTPEExonore"] != DBNull.Value)
                                client.BTPEExonore = bool.Parse(dr["BTPEExonore"].ToString());
                            if (dr["BTDCExonore"] != DBNull.Value)
                                client.BTDCExonore = bool.Parse(dr["BTDCExonore"].ToString());

                            if (dr["BFodecExonore"] != DBNull.Value)
                                client.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["BTimbreExonore"] != DBNull.Value)
                                client.BTimbreExonore = bool.Parse(dr["BTimbreExonore"].ToString());
                            if (dr["BTVAExonore"] != DBNull.Value)
                                client.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                client.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMax"] != DBNull.Value)
                                client.MontantCreditMax = decimal.Parse(dr["MontantCreditMax"].ToString());
                            if (dr["MontantCreditMin"] != DBNull.Value)
                                client.MontantCreditMin = decimal.Parse(dr["MontantCreditMin"].ToString());
                            if (dr["MontantExonoreTVA"] != DBNull.Value)
                                client.MontantExonoreTVA = decimal.Parse(dr["MontantExonoreTVA"].ToString());
                            if (dr["NbJourEcheancePaiment"] != DBNull.Value)
                                client.NbJourEcheancePaiment = int.Parse(dr["NbJourEcheancePaiment"].ToString());
                            if (dr["NbJourCreditFacture"] != DBNull.Value)
                                client.NbJourCreditFacture = int.Parse(dr["NbJourCreditFacture"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                client.Nom = dr["Nom"].ToString();
                            if (dr["NumCIN"] != DBNull.Value)
                                client.NumCIN = dr["NumCIN"].ToString();
                            if (dr["NumTimbre"] != DBNull.Value)
                                client.NumTimbre = dr["NumTimbre"].ToString();
                            if (dr["ObservationClient"] != DBNull.Value)
                                client.ObservationClient = dr["ObservationClient"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                client.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["BContentieux"] != DBNull.Value)
                                client.BContentieux = bool.Parse(dr["BContentieux"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                client.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["TauxRemise"] != DBNull.Value)
                                client.TauxRemise = decimal.Parse(dr["TauxRemise"].ToString());
                            if (dr["SoldeAvanceRestant"] != DBNull.Value)
                                client.SoldeAvanceRestant = decimal.Parse(dr["SoldeAvanceRestant"].ToString());
                            if (dr["SoldeAvoirRestant"] != DBNull.Value)
                                client.SoldeAvoirRestant = decimal.Parse(dr["SoldeAvoirRestant"].ToString());
                            if (dr["SoldeBonRetour"] != DBNull.Value)
                                client.SoldeBonRetour = decimal.Parse(dr["SoldeBonRetour"].ToString());
                            if (dr["SoldeAnterieur"] != DBNull.Value)
                                client.SoldeAnterieur = decimal.Parse(dr["SoldeAnterieur"].ToString());
                            if (dr["SoldeBonLivraison"] != DBNull.Value)
                                client.SoldeBonLivraison = decimal.Parse(dr["SoldeBonLivraison"].ToString());
                            if (dr["SoldeFacture"] != DBNull.Value)
                                client.SoldeFacture = decimal.Parse(dr["SoldeFacture"].ToString());
                            if (dr["SoldeImpaye"] != DBNull.Value)
                                client.SoldeImpaye = decimal.Parse(dr["SoldeImpaye"].ToString());
                            if (dr["TauxRetenuSource"] != DBNull.Value)
                                client.TauxRetenuSource = decimal.Parse(dr["TauxRetenuSource"].ToString());
                            if (dr["TauxRetenuTVA"] != DBNull.Value)
                                client.TauxRetenuTVA = decimal.Parse(dr["TauxRetenuTVA"].ToString());
                            if (dr["BMajoration"] != DBNull.Value)
                                client.BMajoration = bool.Parse(dr["BMajoration"].ToString());
                            if (dr["NumeroTelephone1"] != DBNull.Value)
                                client.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            if (dr["NumeroTelephone2"] != DBNull.Value)
                                client.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            if (dr["BTransfertCompta"] != DBNull.Value)
                                client.BTransfertCompta = bool.Parse(dr["BTransfertCompta"].ToString());
                            if (dr["BVIP"] != DBNull.Value)
                                client.BVIP = bool.Parse(dr["BVIP"].ToString());
                            if (dr["CNatureTiers"] != DBNull.Value)
                                client.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["BTransfert"] != DBNull.Value)
                                client.BTransfert = bool.Parse(dr["BTransfert"].ToString());
                            if (dr["NumAutorisation"] != DBNull.Value)
                                client.NumAutorisation = dr["NumAutorisation"].ToString();
                            if (dr["DateDebutAutorisation"] != DBNull.Value)
                                client.DateDebutAutorisation = DateTime.Parse(dr["DateDebutAutorisation"].ToString());
                            if (dr["BPaiementAvance"] != DBNull.Value)
                                client.BPaiementAvance = bool.Parse(dr["BPaiementAvance"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                client.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BInitialisationRemise"] != DBNull.Value)
                                client.BInitialisationRemise = bool.Parse(dr["BInitialisationRemise"].ToString());
                            if (dr["RemiseExeptionnel"] != DBNull.Value)
                                client.RemiseExeptionnel = decimal.Parse(dr["RemiseExeptionnel"].ToString());
                            if (dr["DateFinAutorisation"] != DBNull.Value)
                                client.DateFinAutorisation = DateTime.Parse(dr["DateFinAutorisation"].ToString());
                            if (dr["BEtablissement"] != DBNull.Value)
                                client.BEtablissement = bool.Parse(dr["BEtablissement"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                client.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Longitude"] != DBNull.Value)
                                client.Longitude = decimal.Parse(dr["Longitude"].ToString());
                            if (dr["Latitude"] != DBNull.Value)
                                client.Latitude = decimal.Parse(dr["Latitude"].ToString());
                            if (dr["Password"] != DBNull.Value)
                                client.MotDePasse = dr["Password"].ToString();

                            clientCollection.Add(client);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (clientCollection);
        }
    }
}