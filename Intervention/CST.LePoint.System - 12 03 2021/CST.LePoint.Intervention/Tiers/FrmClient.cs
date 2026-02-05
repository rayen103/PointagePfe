using CST.LePoint.CtrlLibrary;
using CST.LePoint.CtrlLibrary.Search;
using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Stock.Referentiel.Commun;
using CST.LePoint.Tiers.Metier;
using CST.LePoint.Tiers.Referentiel;
using CST.LePoint.Intervention.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CST.LePoint.Intervention.Tiers
{
    public partial class FrmClient : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        private string _CodeClient;
        private string nomColonneModifie = string.Empty;

        public FrmClient()
        {
            InitializeComponent();
        }

        public FrmClient(string cClient)
        {
            InitializeComponent();
            this._CodeClient = cClient;
        }

        #region Utilitaires

        public void ChargerEntite(string codeClient)
        {
            //***************************** Tab Info************************************//
            Client client = null;
            try
            {
                int x = int.Parse(codeClient);
                client = Client.ChargerVue(codeClient);
            }
            catch
            {
                client = Client.Charger(codeClient);
            }
        
            if (client == null)
                return;

            this.txtCClient.Text = client.CClient;
            this.Text = Resources.Titre_FrmClient + ": " + client.CClient;
            this.PageClient.SelectedTabPage = this.tabInfo;
            this.txtAbreviationCode.Text = client.Abreviation;
            this.txtRaisonSocial.Text = client.RaisonSociale;
            if(client.CClientFamille != null)
                this.LkpCFamille.EditValue = client.CClientFamille.ToString();
            this.lkpCVendeur.EditValue = client.CVendeur.ToString();
            this.lkpRecouvreur.EditValue = client.CRecouvreur.ToString();
            if(client.CRegion!= null)
                this.lkpCRegion.EditValue = client.CRegion.ToString();
            if (client.CGouvernorat != null)
                this.lkpGouvernorat.EditValue = client.CGouvernorat.ToString();
            this.chkElimine.Checked = client.BElimines;

            foreach (Adresse adresse in client.Adresses)
            {
                if (adresse.BAdresseFacturation == true)
                {
                    this.txtLibAdresseFac.Text = adresse.LibAdresse;
                    this.txtVilleAdresseFac.Text = adresse.Ville;
                    this.txtCPostalFac.Text = adresse.CPostal;

                    this.lkpCPaysFacturation.EditValue = adresse.CPays.ToString();
                    this.chkAdresseLivraison.Checked = adresse.BAdresseLivraison;
                }
                if (adresse.BAdresseLivraison == true)
                {
                    this.txtLibAdresseLiv.Text = adresse.LibAdresse;
                    this.txtVilleAdresseLiv.Text = adresse.Ville;
                    this.txtCPostalLiv.Text = adresse.CPostal;
                    this.lkpCPaysLivraison.EditValue = adresse.CPays.ToString();
                    this.chkAdresseLivraison.Checked = adresse.BAdresseFacturation;
                }
            }
            this.txtNumeroTelephone1.Text = client.NumeroTelephone1;
            this.txtNumeroTelephone2.Text = client.NumeroTelephone2;
            this.txtObservationClient.Text = client.ObservationClient;
            this.txtEmail.Text = client.Email;
            this.txtNumCIN.Text = client.NumCIN;
            this.txtFax.Text = client.Fax;
            this.txtCFournisseur.Text = client.CFournisseur;
            this.txtemplacement.Text = client.EmplacementScan;
            this.txtMdp.Text = client.MotDePasse;

            if (client.CFournisseur != null)
            {
                Fournisseur fournisseur = Fournisseur.Charger(client.CFournisseur);
                this.txtRaisonSocialeFour.Text = fournisseur.RaisonSociale;
            }
            //***************************** Tab Vente************************************//

            this.PageClient.SelectedTabPage = this.tabVente;
            this.chkBTransfertCompta.Checked = client.BTransfertCompta;
            if (!string.IsNullOrEmpty(client.CTVA))
            {
                try
                {
                    this.txtCTVA.Text = client.CTVA.Substring(0, 7);
                    this.lkpcle.EditValue = client.CTVA.Substring(7, 1);
                    this.lkpcodetva.EditValue = client.CTVA.Substring(8, 1);
                    this.lkpcodecateg.EditValue = client.CTVA.Substring(9, 1);
                    this.txtnumetablissement.EditValue = client.CTVA.Substring(10, 3);
                   
                }
                catch (Exception)
                {
                    XtraMessageBox.Show("Matricule Fiscal non valide");
                }
            }
            this.txtRemiseExceptionnelle.EditValue = client.RemiseExeptionnel.ToString();
            if (client.BMajoration == true)
            {
                this.radioMajore.Checked = true;
                this.radioNonMajore.Checked = false;
            }
            else
            {
                this.radioMajore.Checked = false;
                this.radioNonMajore.Checked = true;
            }

            this.chkBAvanceForfaitaire.Checked = client.BAvanceForfaitaire;

            if (client.BFodecExonore == true)
            {
                this.radioFodecNonExonore.Checked = false;
                this.radioFodecExonore.Checked = true;
            }
            else
            {
                this.radioFodecNonExonore.Checked = true;
                this.radioFodecExonore.Checked = false;
            }

            this.txtDateFinExoFodec.EditValue = client.DateFinExonoreFodec;

            if (client.BTPEExonore == true)
            {
                this.radioTPENonExonere.Checked = false;
                this.radioTPEExonere.Checked = true;
            }
            else
            {
                this.radioTPENonExonere.Checked = true;
                this.radioTPEExonere.Checked = false;
            }

            this.txtDateFinExoTPE.EditValue = client.DateFinExonoreTPE;

            if (client.BTDCExonore == true)
            {
                this.radioNonExoTDC.Checked = false;
                this.radioExoTDC.Checked = true;
            }
            else
            {
                this.radioNonExoTDC.Checked = true;
                this.radioExoTDC.Checked = false;
            }

            this.txtDateFinExoTDC.EditValue = client.DateFinExonoreTDC;

            if (client.BTVAExonore == true)
            {
                this.radioTVANonExonore.Checked = false;
                this.radioTVAExonore.Checked = true;
            }
            else
            {
                this.radioTVANonExonore.Checked = true;
                this.radioTVAExonore.Checked = false;
            }
            if (client.BContentieux)
            {
                this.rbNonContentieux.Checked = false;
                this.rbContentieux.Checked = true;
            }
            else
            {
                this.rbNonContentieux.Checked = true;
                this.rbContentieux.Checked = false;
            }
            this.txtDateFinExoTVA.EditValue = client.DateFinExonoreTVA;
            this.txtMontantExonoreTVA.Text = client.MontantExonoreTVA.ToString();

            if (client.BTimbreExonore == true)
            {
                this.radioTimbreNonExonore.Checked = false;
                this.radioTimbreExonore.Checked = true;
            }
            else
            {
                this.radioTimbreNonExonore.Checked = true;
                this.radioTimbreExonore.Checked = false;
            }
            this.txtTauxRetenuSource.Text = client.TauxRetenuSource.ToString();
            this.txtTauxRetenuTVA.Text = client.TauxRetenuTVA.ToString();
            this.chkBInitialisationRemise.Checked = client.BInitialisationRemise;
            this.txtMontantCreditMin.Text = client.MontantCreditMin.ToString();
            this.txtMontantCreditMax.Text = client.MontantCreditMax.ToString();
            this.txtNbJourEcheancePaiment.Text = client.NbJourEcheancePaiment.ToString();
            this.txtNbJourCreditFacture.Text = client.NbJourCreditFacture.ToString();
            this.txtTauxRemise.Text = client.TauxRemise.ToString();
            if(client.CModeReglement != null)
                this.lkpModePaiement.EditValue = client.CModeReglement.ToString();
            this.lkpCTarif.EditValue = client.CTarif.ToString();

            if (client.BActif == true)
            {
                this.radioNonActif.Checked = false;
                this.radioActif.Checked = true;
            }
            else
            {
                this.radioNonActif.Checked = true;
                this.radioActif.Checked = false;
            }

            if (client.BVIP == true)
            {
                this.radioNonVIP.Checked = false;
                this.radioVIP.Checked = true;
            }
            else
            {
                this.radioNonVIP.Checked = true;
                this.radioVIP.Checked = false;
            }

            this.chkClientPassager.Checked = client.BPassager;
            this.chkBEtablissement.Checked = client.BEtablissement;
            switch (client.CNatureTiers)
            {
                case 1:
                    {
                        this.radioLocale.Checked = true;
                        break;
                    }
                case 3:
                    {
                        this.radioSuspension.Checked = true;
                        this.txtNAutorisation.Text = client.NumAutorisation;
                        this.txtDateDebSusp.EditValue = client.DateDebutAutorisation;
                        this.txtDateFinSusp.EditValue = client.DateFinAutorisation;
                        break;
                    }
                case 2:
                    {
                        this.radioExport.Checked = true;

                        break;
                    }
                default:
                    break;
            }
            //***************************** Tab Banques ************************************//

            this.PageClient.SelectedTabPage = this.tabBanque;
            ClientBanqueCollection collectionClientBanque = ClientBanqueCollection.Charger(client.CClient);
            for (int i = 0; i < collectionClientBanque.Count; i++)
            {
                Banque banque = Banque.Charger(collectionClientBanque[i].CBanque);
                this.gridVClientBanque.AddNewRow();
                this.gridVClientBanque.SetFocusedRowCellValue("Code", collectionClientBanque[i].CBanque);
                this.gridVClientBanque.SetFocusedRowCellValue("Libellé", banque.Libelle);
                this.gridVClientBanque.SetFocusedRowCellValue("Agence", collectionClientBanque[i].Agence);
                this.gridVClientBanque.SetFocusedRowCellValue("RIB", collectionClientBanque[i].RIBClient);
                this.gridVClientBanque.UpdateCurrentRow();
            }

            //***************************** Tab Contacts ************************************//

            this.PageClient.SelectedTabPage = this.tabContacts;
            for (int i = 0; i < client.Contacts.Count; i++)
            {
                this.gridVClientContact.AddNewRow();
                this.gridVClientContact.SetFocusedRowCellValue("Code", client.Contacts[i].CContact);
                if (client.Contacts[i].BPrincipal)
                    this.gridVClientContact.SetFocusedRowCellValue("Principal", 1);
                else
                    this.gridVClientContact.SetFocusedRowCellValue("Principal", 0);
                this.gridVClientContact.SetFocusedRowCellValue("Civilité", client.Contacts[i].CCivilite);
                this.gridVClientContact.SetFocusedRowCellValue("Nom", client.Contacts[i].Nom);
                this.gridVClientContact.SetFocusedRowCellValue("Prénom", client.Contacts[i].Prenom);
                this.gridVClientContact.SetFocusedRowCellValue("Fonction", client.Contacts[i].Fonction);
                this.gridVClientContact.SetFocusedRowCellValue("Téléphone", client.Contacts[i].Telephone);
                this.gridVClientContact.SetFocusedRowCellValue("Portable", client.Contacts[i].Portable);
                this.gridVClientContact.SetFocusedRowCellValue("Email", client.Contacts[i].Email);
                //   this.gridVClientContact.SetFocusedRowCellValue("Interlocuteur", client.Contacts[i].Interlocuteur);
                this.gridVClientContact.UpdateCurrentRow();
            }
            //***************************** Tab Etablissements ************************************//
            if (client.Etablissements.Count == 0 && client.BEtablissement)
            {

                this.gridVEtab.AddNewRow();
                this.gridVEtab.SetFocusedRowCellValue("Code Etablissement", client.CClient+"/001");
                this.gridVEtab.SetFocusedRowCellValue("Libellé", client.RaisonSociale);
                this.gridVEtab.SetFocusedRowCellValue("Région", client.CRegion);
                this.gridVEtab.SetFocusedRowCellValue("Adresse", this.txtLibAdresseFac.Text);
                this.gridVEtab.SetFocusedRowCellValue("Ville", this.txtVilleAdresseFac.Text);
                this.gridVEtab.SetFocusedRowCellValue("Code Postale", this.txtCPostalFac.Text);
                this.gridVEtab.SetFocusedRowCellValue("Latitude", client.Latitude);
                this.gridVEtab.SetFocusedRowCellValue("Longitude", client.Longitude);
                this.gridVEtab.UpdateCurrentRow();

            }
            else
            {
                for (int i = 0; i < client.Etablissements.Count; i++)
                {
                    this.gridVEtab.AddNewRow();
                    this.gridVEtab.SetFocusedRowCellValue("Code Etablissement", client.Etablissements[i].Code);
                    this.gridVEtab.SetFocusedRowCellValue("Libellé", client.Etablissements[i].Libelle);
                    this.gridVEtab.SetFocusedRowCellValue("Région", client.Etablissements[i].CRegion);
                    this.gridVEtab.SetFocusedRowCellValue("Adresse", client.Etablissements[i].Adresse);
                    this.gridVEtab.SetFocusedRowCellValue("Ville", client.Etablissements[i].Ville);
                    this.gridVEtab.SetFocusedRowCellValue("Code Postale", client.Etablissements[i].CodePostale);
                    this.gridVEtab.SetFocusedRowCellValue("Latitude", client.Etablissements[i].Latitude);
                    this.gridVEtab.SetFocusedRowCellValue("Longitude", client.Etablissements[i].Longitude);
                    this.gridVEtab.UpdateCurrentRow();
                }
            }
            this.PageClient.SelectedTabPage = this.tabInfo;
        }

        public static bool IsValidEmail(string email)
        {
            var r = new Regex(@"^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$");
            return !String.IsNullOrEmpty(email) && r.IsMatch(email);
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            this.gridVClientBanque.UpdateCurrentRow();
            this.gridVClientContact.UpdateCurrentRow();

            if (!dxValidationProvider1.Validate())
            {
                foreach (Control c in dxValidationProvider1.GetInvalidControls())
                {
                    DevExpress.XtraEditors.DXErrorProvider.ValidationRuleBase rule =
                        dxValidationProvider1.GetValidationRule(c);

                    XtraMessageBox.Show(rule.ErrorText,
                   Resources.NomApplication,
                   MessageBoxButtons.OK,
                   MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    c.Focus();
                    return;
                }
            }

            DialogResult dialogResult = XtraMessageBox.Show("Voulez-vous sauvegarder cet enregistrement ?",
                       Resources.NomApplication,
                       MessageBoxButtons.OKCancel,
                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (dialogResult == DialogResult.Cancel)
                return;

            if (this.radioExport.Checked || this.radioSuspension.Checked)
            {
                string msgAlerte;

                if (this.radioExport.Checked)
                    msgAlerte = " Ce client est de nature Export.";
                else
                    msgAlerte = " Ce client est de nature Suspension.";

                if ((this.radioFodecNonExonore.Checked) && (this.radioTVANonExonore.Checked))
                {
                    msgAlerte = msgAlerte + "\n - Non Exonéré ni en Fodec ni en TVA. \n Voulez-vous continuer ?";
                    DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                               Resources.NomApplication,
                               MessageBoxButtons.YesNo,
                               MessageBoxIcon.Question,
                               MessageBoxDefaultButton.Button1);
                    if (dialogResultAlerte == DialogResult.No)
                    {
                        this.PageClient.SelectedTabPage = this.tabVente;
                        return;
                    }
                }
                else
                    if (this.radioFodecNonExonore.Checked)
                    {
                        msgAlerte = msgAlerte + "\nNon Exonéré en Fodec. \n Voulez-vous continuer ?";
                        DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                                   Resources.NomApplication,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1);
                        if (dialogResultAlerte == DialogResult.No)
                        {
                            this.PageClient.SelectedTabPage = this.tabVente;
                            return;
                        }
                    }
                    else
                        if (this.radioTVANonExonore.Checked)
                        {
                            msgAlerte = msgAlerte + "\n Non Exonéré en TVA. \n Voulez-vous continuer ?";
                            DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                                       Resources.NomApplication,
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Question,
                                       MessageBoxDefaultButton.Button1);
                            if (dialogResultAlerte == DialogResult.No)
                            {
                                this.PageClient.SelectedTabPage = this.tabVente;
                                return;
                            }
                        }
                if (this.radioSuspension.Checked)
                {
                    if(string.IsNullOrWhiteSpace(this.txtNAutorisation.Text))
                    {
                            msgAlerte = msgAlerte + "\nVeuillez entrer le Numéro d'autorisation de suspension";
                            DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                                       Resources.NomApplication,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                            this.txtNAutorisation.Focus();
                            return;
                    }
                    if(string.IsNullOrWhiteSpace(this.txtDateDebSusp.Text))
                    {
                            msgAlerte = msgAlerte + "\n Veuillez entrer la date de début d'autorisation de suspension";
                            DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                                       Resources.NomApplication,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                            this.txtDateDebSusp.Focus();
                            return;
                    }
                    if (string.IsNullOrWhiteSpace(this.txtDateFinSusp.Text))
                    {
                            msgAlerte = msgAlerte + "\n Veuillez entrer la date de fin d'autorisation de suspension";
                            DialogResult dialogResultAlerte = XtraMessageBox.Show(msgAlerte,
                                       Resources.NomApplication,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information,
                                       MessageBoxDefaultButton.Button1);
                            this.txtDateFinSusp.Focus();
                            return;
                    }
                    if (DateTime.Parse(this.txtDateDebSusp.Text) > DateTime.Parse(this.txtDateFinSusp.Text))
                    {
                        DialogResult dialogResultAlerte = XtraMessageBox.Show("Vérifiez la période d'autorisation de suspension\nDate début supérieure à la date fin!!",
                                   Resources.NomApplication,
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1);
                        this.txtDateDebSusp.Focus();
                        return;
                    }
                }
            }

            if ((this.radioFodecExonore.Checked) && (string.IsNullOrEmpty(this.txtDateFinExoFodec.Text)))
            {
                XtraMessageBox.Show("Veuillez entrer la date du fin d'exonération Fodec. ",
                           Resources.NomApplication,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1);
                this.PageClient.SelectedTabPage = this.tabVente;
                this.txtDateFinExoFodec.Focus();

                return;
            }

            if ((this.radioTPEExonere.Checked) && (string.IsNullOrEmpty(this.txtDateFinExoTPE.Text)))
            {
                XtraMessageBox.Show("Veuillez entrer la date du fin d'exonération TPE. ",
                           Resources.NomApplication,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1);
                this.PageClient.SelectedTabPage = this.tabVente;
                this.txtDateFinExoTPE.Focus();

                return;
            }

            if ((this.radioExoTDC.Checked) && (string.IsNullOrEmpty(this.txtDateFinExoTDC.Text)))
            {
                XtraMessageBox.Show("Veuillez entrer la date du fin d'exonération Taxe droits de consommation. ",
                           Resources.NomApplication,
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Information,
                           MessageBoxDefaultButton.Button1);
                this.PageClient.SelectedTabPage = this.tabVente;
                this.txtDateFinExoTDC.Focus();

                return;
            }

            if (this.radioTVAExonore.Checked && (string.IsNullOrEmpty(this.txtDateFinExoTVA.Text)))
            {
                XtraMessageBox.Show("Veuillez entrer la date du fin d'exonération TVA. ",
                                    Resources.NomApplication,
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information,
                                      MessageBoxDefaultButton.Button1);
                this.PageClient.SelectedTabPage = this.tabVente;
                this.txtDateFinExoTVA.Focus();
                return;
            }
            /// controler la longeur du cin
            //if (!string.IsNullOrEmpty(this.txtNumCIN.Text))
            //{
            //    if (this.txtNumCIN.Text.Length > 8)
            //    {
            //        XtraMessageBox.Show("Numéro CIN invalide. ",
            //                                   Resources.NomApplication,
            //                                   MessageBoxButtons.OK,
            //                                   MessageBoxIcon.Information,
            //                                   MessageBoxDefaultButton.Button1);
            //        this.PageClient.SelectedTabPage = this.tabInfo;
            //        this.txtNumCIN.EditValue = string.Empty;
            //        this.txtNumCIN.Focus();
            //        return;
            //    }
            //}

            //if (!this.chkClientPassager.Checked && string.IsNullOrEmpty(this.txtCPostalFac.Text))
            //{
            //    XtraMessageBox.Show("Code Postal invalide. ",
            //                                  Resources.NomApplication,
            //                                  MessageBoxButtons.OK,
            //                                  MessageBoxIcon.Information,
            //                                  MessageBoxDefaultButton.Button1);
            //    this.PageClient.SelectedTabPage = this.tabInfo;
            //    this.txtCPostalFac.EditValue = string.Empty;
            //    this.txtCPostalFac.Focus();
            //    return;
            //}

            //if (!string.IsNullOrEmpty(this.txtCPostalFac.Text))
            //{
            //    try
            //    {
            //        decimal d = decimal.Parse(this.txtCPostalFac.Text);
            //        if (this.txtCPostalFac.Text.Length != 4)
            //        {
            //            XtraMessageBox.Show("Code Postal invalide. ",
            //                                    Resources.NomApplication,
            //                                    MessageBoxButtons.OK,
            //                                    MessageBoxIcon.Information,
            //                                    MessageBoxDefaultButton.Button1);
            //            this.PageClient.SelectedTabPage = this.tabInfo;
            //            this.txtCPostalFac.EditValue = string.Empty;
            //            this.txtCPostalFac.Focus();
            //            return;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        XtraMessageBox.Show("Code Postal invalide. ",
            //                      Resources.NomApplication,
            //                      MessageBoxButtons.OK,
            //                      MessageBoxIcon.Information,
            //                      MessageBoxDefaultButton.Button1);
            //        this.PageClient.SelectedTabPage = this.tabInfo;
            //        this.txtCPostalFac.EditValue = string.Empty;
            //        this.txtCPostalFac.Focus();
            //        return;
            //    }
            //}

            //if (!string.IsNullOrEmpty(this.txtCPostalLiv.Text))
            //{
            //    try
            //    {
            //        decimal d = decimal.Parse(this.txtCPostalLiv.Text);
            //        if (this.txtCPostalFac.Text.Length != 4)
            //        {
            //            XtraMessageBox.Show("Code Postal invalide. ",
            //                                       Resources.NomApplication,
            //                                       MessageBoxButtons.OK,
            //                                       MessageBoxIcon.Information,
            //                                       MessageBoxDefaultButton.Button1);
            //            this.PageClient.SelectedTabPage = this.tabInfo;
            //            this.txtCPostalLiv.EditValue = string.Empty;
            //            this.txtCPostalLiv.Focus();
            //            return;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        XtraMessageBox.Show("Code Postal invalide. ",
            //                       Resources.NomApplication,
            //                       MessageBoxButtons.OK,
            //                       MessageBoxIcon.Information,
            //                       MessageBoxDefaultButton.Button1);
            //        this.PageClient.SelectedTabPage = this.tabInfo;
            //        this.txtCPostalLiv.EditValue = string.Empty;
            //        this.txtCPostalLiv.Focus();
            //        return;
            //    }
            //}


            if (!string.IsNullOrEmpty(this.txtEmail.Text))
            {
                try
                {
                    string mail = this.txtEmail.Text;
                    if (!IsValidEmail(mail))
                    {
                        XtraMessageBox.Show("Adresse Mail invalide. ",
                                                 Resources.NomApplication,
                                                   MessageBoxButtons.OK,
                                                   MessageBoxIcon.Information,
                                                   MessageBoxDefaultButton.Button1);
                        this.PageClient.SelectedTabPage = this.tabInfo;
                        this.txtEmail.EditValue = string.Empty;
                        this.txtEmail.Focus();
                        return;
                    }
                }
                catch
                {
                    XtraMessageBox.Show("Adresse Mail invalide. ",
                                                  Resources.NomApplication,
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Information,
                                                    MessageBoxDefaultButton.Button1);
                    this.PageClient.SelectedTabPage = this.tabInfo;
                    this.txtEmail.EditValue = string.Empty;
                    this.txtEmail.Focus();
                    
                    return;
                }
            }

          
            Client client = new Client();

            client.CClient = this.txtCClient.Text;
            client.Abreviation = this.txtAbreviationCode.Text;
            client.RaisonSociale = this.txtRaisonSocial.Text;
            client.BElimines = this.chkElimine.Checked;
            if (!string.IsNullOrEmpty(this.LkpCFamille.Text))
                client.CClientFamille = this.LkpCFamille.EditValue.ToString();
            if (!string.IsNullOrEmpty(this.lkpCVendeur.Text))
                client.CVendeur = int.Parse(this.lkpCVendeur.EditValue.ToString());
            if (!string.IsNullOrEmpty(this.lkpCRegion.Text))
                client.CRegion = this.lkpCRegion.EditValue.ToString();
            if (!string.IsNullOrEmpty(this.lkpRecouvreur.Text))
                client.CRecouvreur = int.Parse(this.lkpRecouvreur.EditValue.ToString());
            if (!string.IsNullOrEmpty(this.lkpGouvernorat.Text))
                client.CGouvernorat = this.lkpGouvernorat.EditValue.ToString();
            
            client.BPassager = this.chkClientPassager.Checked;
            client.BEtablissement = this.chkBEtablissement.Checked;
            Adresse adresseFac = new Adresse();
            adresseFac.NTiers = client.CClient;
            adresseFac.LibAdresse = this.txtLibAdresseFac.Text;
            adresseFac.Ville = this.txtVilleAdresseFac.Text;
            adresseFac.CPostal = this.txtCPostalFac.Text;
            adresseFac.BAdresseFacturation = true;
            adresseFac.CPays = this.lkpCPaysFacturation.EditValue.ToString();

            if (!this.chkAdresseLivraison.Checked)
            {
                Adresse adresseLiv = new Adresse();
                adresseLiv.NTiers = client.CClient;
                adresseLiv.LibAdresse = this.txtLibAdresseLiv.Text;
                adresseLiv.Ville = this.txtVilleAdresseLiv.Text;
                adresseLiv.CPostal = this.txtCPostalLiv.Text;
                adresseLiv.BAdresseLivraison = true;
                adresseLiv.CPays = this.lkpCPaysLivraison.EditValue.ToString();
                client.Adresses.Add(adresseLiv);
            }
            else
                adresseFac.BAdresseLivraison = true;
            client.Adresses.Add(adresseFac);

            client.NumeroTelephone1 = this.txtNumeroTelephone1.Text;
            client.NumeroTelephone2 = this.txtNumeroTelephone2.Text;
            client.Fax = this.txtFax.Text;
           
            client.Email = this.txtEmail.Text;
            client.ObservationClient = this.txtObservationClient.Text;
            if (!string.IsNullOrWhiteSpace(this.txtCFournisseur.Text))
                client.CFournisseur = this.txtCFournisseur.Text;

            if (!string.IsNullOrWhiteSpace(this.txtemplacement.Text))
                client.EmplacementScan = this.txtemplacement.Text;

            if (!string.IsNullOrWhiteSpace(this.txtMdp.Text))
                client.MotDePasse = this.txtMdp.Text;

            if (!string.IsNullOrEmpty(this.lkpCPaysFacturation.EditValue.ToString()))
                client.CPays = this.lkpCPaysFacturation.EditValue.ToString();

            client.RemiseExeptionnel = decimal.Parse(this.txtRemiseExceptionnelle.Text);
            client.BTransfertCompta = bool.Parse(this.chkBTransfertCompta.EditValue.ToString());
            ClientCollection col = ClientCollection.ChargerVerfication();
            if (!string.IsNullOrEmpty(this.txtnumetablissement.Text) && (txtCTVA.Text.Length == 7) && !string.IsNullOrEmpty(this.lkpcle.Text) && !string.IsNullOrEmpty(this.lkpcodetva.Text) && !string.IsNullOrEmpty(this.lkpcodecateg.Text) && !string.IsNullOrEmpty(this.txtCTVA.Text))
            {
                client.CTVA = this.txtCTVA.Text + this.lkpcle.EditValue + this.lkpcodetva.EditValue + this.lkpcodecateg.EditValue + this.txtnumetablissement.Text;
                if (col.Exists(cl => cl.CTVA == client.CTVA && cl.CClient != client.CClient))
                {
                    client.CTVA = null;
                }
                
            }
            if (!string.IsNullOrEmpty(this.txtNumCIN.Text))
            {
                if (col.Exists(cl => cl.NumCIN == this.txtNumCIN.Text && cl.CClient != client.CClient && cl.CTVA == client.CTVA))
                {
                    client.NumCIN = null;
                    XtraMessageBox.Show("CIN existant");
                }
                else {
                    client.NumCIN = this.txtNumCIN.Text;
                }
            }

            //if (string.IsNullOrEmpty(client.NumCIN) && string.IsNullOrEmpty(client.CTVA) && !this.chkClientPassager.Checked)
            //{
            //    XtraMessageBox.Show("vous devez saisir votre : CIN/MATRICULE FISCALE");
            //    return;
            //}
            if (this.radioMajore.Checked)
                client.BMajoration = true;
            if (this.radioNonMajore.Checked)
                client.BMajoration = false;
            client.BAvanceForfaitaire = bool.Parse(this.chkBAvanceForfaitaire.EditValue.ToString());
            if (this.radioFodecExonore.Checked)
            {
                client.BFodecExonore = true;
                if (!string.IsNullOrEmpty(this.txtDateFinExoFodec.Text))
                    client.DateFinExonoreFodec = DateTime.Parse(this.txtDateFinExoFodec.EditValue.ToString());
            }
            if (this.radioTPEExonere.Checked)
            {
                client.BTPEExonore = true;
                if (!string.IsNullOrEmpty(this.txtDateFinExoTPE.Text))
                    client.DateFinExonoreTPE = DateTime.Parse(this.txtDateFinExoTPE.EditValue.ToString());
            }
            if (this.radioExoTDC.Checked)
            {
                client.BTDCExonore = true;
                if (!string.IsNullOrEmpty(this.txtDateFinExoTDC.Text))
                    client.DateFinExonoreTDC = DateTime.Parse(this.txtDateFinExoTDC.EditValue.ToString());
            }

            if (this.radioTVAExonore.Checked)
            {
                client.BTVAExonore = this.radioTVAExonore.Checked;
                if (!string.IsNullOrEmpty(this.txtDateFinExoTVA.Text))
                    client.DateFinExonoreTVA = DateTime.Parse(this.txtDateFinExoTVA.Text.ToString());
            }
            if (this.radioTVANonExonore.Checked)
            {
                client.BTVAExonore = false;
                this.txtDateFinExoTVA.Tag = string.Empty;
            }
            if (!string.IsNullOrEmpty(this.txtMontantExonoreTVA.Text))
            {
                client.MontantExonoreTVA = decimal.Parse(this.txtMontantExonoreTVA.Text.ToString());
            }
            if (this.radioLocale.Checked)
                client.CNatureTiers = 1;

            if (this.radioSuspension.Checked)
            {
                client.CNatureTiers = 3;
                client.NumAutorisation = txtNAutorisation.Text;
                client.DateDebutAutorisation = DateTime.Parse(this.txtDateDebSusp.EditValue.ToString());
                client.DateFinAutorisation = DateTime.Parse(this.txtDateFinSusp.EditValue.ToString());
            }

            if (this.radioExport.Checked)
                client.CNatureTiers = 2;

            client.BTimbreExonore = this.radioTimbreExonore.Checked;

            if (!string.IsNullOrEmpty(this.txtTauxRetenuSource.Text))
                client.TauxRetenuSource = decimal.Parse(this.txtTauxRetenuSource.Text);
            if (!string.IsNullOrEmpty(this.txtTauxRetenuTVA.Text))
                client.TauxRetenuTVA = decimal.Parse(this.txtTauxRetenuTVA.Text);
            client.BInitialisationRemise = bool.Parse(this.chkBInitialisationRemise.EditValue.ToString());
            if (!string.IsNullOrEmpty(this.txtMontantCreditMin.Text))
                client.MontantCreditMin = decimal.Parse(this.txtMontantCreditMin.Text);
            if (!string.IsNullOrEmpty(this.txtMontantCreditMax.Text))
                client.MontantCreditMax = decimal.Parse(this.txtMontantCreditMax.Text);
            if (!string.IsNullOrEmpty(this.txtNbJourEcheancePaiment.Text))
                client.NbJourEcheancePaiment = int.Parse(this.txtNbJourEcheancePaiment.Text);
            if (!string.IsNullOrEmpty(this.txtNbJourCreditFacture.Text))
                client.NbJourCreditFacture = int.Parse(this.txtNbJourCreditFacture.Text);
            if (!string.IsNullOrEmpty(this.txtTauxRemise.Text))
                client.TauxRemise = decimal.Parse(this.txtTauxRemise.Text);
            if (!string.IsNullOrEmpty(this.lkpCTarif.EditValue.ToString()))
                client.CTarif = this.lkpCTarif.EditValue.ToString();

            if (!string.IsNullOrEmpty(this.lkpModePaiement.Text))
                client.CModeReglement = this.lkpModePaiement.EditValue.ToString();
            if (this.radioActif.Checked)
                client.BActif = true;
            if (this.radioNonActif.Checked)
                client.BActif = false;
            if (this.radioVIP.Checked)
                client.BVIP = true;
            if (this.radioNonVIP.Checked)
                client.BVIP = false;
            if (this.rbContentieux.Checked)
                client.BContentieux = true;
            if (this.rbNonContentieux.Checked)
                client.BContentieux = false;

            
            for (int i = 0; i < this.gridVClientBanque.RowCount; i++)
            {
                string cBanque = this.gridVClientBanque.GetRowCellValue(i, "Code").ToString();

                if (!string.IsNullOrEmpty(cBanque))
                {
                    ClientBanque clientBanque = new ClientBanque();
                    clientBanque.CBanque = cBanque;
                    clientBanque.CClient = client.CClient;
                    try
                    {
                        decimal c = decimal.Parse(this.gridVClientBanque.GetRowCellDisplayText(i, "RIB"));

                        if (this.gridVClientBanque.GetRowCellDisplayText(i, "RIB").Length != 20)
                        {
                            string ancienRib = this.gridVClientBanque.GetRowCellDisplayText(i, "RIB").ToString();
                            XtraMessageBox.Show("RIB invalide. ",
                                                       Resources.NomApplication,
                                                       MessageBoxButtons.OK,
                                                       MessageBoxIcon.Information,
                                                       MessageBoxDefaultButton.Button1);
                            this.PageClient.SelectedTabPage = this.tabBanque;
                            this.gridVClientBanque.SetRowCellValue(i, "RIB", ancienRib);
                            return;
                        }

                        /// controler si le rib contient des lettres
                    }
                    catch (Exception)
                    {
                        XtraMessageBox.Show("RIB invalide. ",
                                                   Resources.NomApplication,
                                                   MessageBoxButtons.OK,
                                                   MessageBoxIcon.Information,
                                                   MessageBoxDefaultButton.Button1);
                        this.PageClient.SelectedTabPage = this.tabBanque;
                        this.gridVClientBanque.SetRowCellValue(i, "RIB", "");
                        return;
                    }
                    clientBanque.RIBClient = this.gridVClientBanque.GetRowCellDisplayText(i, "RIB");

                    clientBanque.Agence = this.gridVClientBanque.GetRowCellDisplayText(i, "Agence");
                    client.Banques.Add(clientBanque);
                }
            }

            for (int i = 0; i < this.gridVClientContact.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.gridVClientContact.GetRowCellDisplayText(i, "Code")))
                {
                    int cContact = int.Parse(this.gridVClientContact.GetRowCellDisplayText(i, "Code"));
                    ClientContact clientContact = new ClientContact();
                    clientContact.CClient = client.CClient;
                    clientContact.CContact = cContact;
                    if (this.gridVClientContact.GetRowCellValue(i, "Principal").ToString() != "")
                        clientContact.BPrincipal = bool.Parse(this.gridVClientContact.GetRowCellValue(i, "Principal").ToString());
                    clientContact.CCivilite = this.gridVClientContact.GetRowCellValue(i, "Civilité").ToString();
                    clientContact.Nom = this.gridVClientContact.GetRowCellDisplayText(i, "Nom");
                    clientContact.Prenom = this.gridVClientContact.GetRowCellDisplayText(i, "Prénom");
                    clientContact.Fonction = this.gridVClientContact.GetRowCellDisplayText(i, "Fonction");
                    clientContact.Telephone = this.gridVClientContact.GetRowCellDisplayText(i, "Téléphone");
                    // clientContact.Portable = this.gridVClientContact.GetRowCellDisplayText(i, "Portable");
                    clientContact.Email = this.gridVClientContact.GetRowCellDisplayText(i, "Email");
                    // clientContact.Interlocuteur = this.gridVClientContact.GetRowCellDisplayText(i, "Interlocuteur");
                    client.Contacts.Add(clientContact);
                }
            }
            ///Etablissement
            ///
            for (int i = 0; i < this.gridVEtab.RowCount; i++)
            {
                if (!string.IsNullOrEmpty(this.gridVEtab.GetRowCellDisplayText(i, "Code Etablissement")))
                {
                    string code = this.gridVEtab.GetRowCellDisplayText(i, "Code Etablissement");
                    Etablissement etablissement = new Etablissement();
                    etablissement.CClient = client.CClient;
                    etablissement.Code = code;
                    etablissement.Libelle = this.gridVEtab.GetRowCellValue(i, "Libellé").ToString();
                    etablissement.CRegion = this.gridVEtab.GetRowCellDisplayText(i, "Région");
                    etablissement.Adresse = this.gridVEtab.GetRowCellDisplayText(i, "Adresse");
                    etablissement.Ville = this.gridVEtab.GetRowCellDisplayText(i, "Ville");
                    etablissement.CodePostale = this.gridVEtab.GetRowCellDisplayText(i, "Code Postale");
                    if (!string.IsNullOrEmpty(gridVEtab.GetRowCellDisplayText(i, "Latitude")))
                        etablissement.Latitude = decimal.Parse(this.gridVEtab.GetRowCellDisplayText(i, "Latitude"));
                    if (!string.IsNullOrEmpty(gridVEtab.GetRowCellDisplayText(i, "Longitude")))
                        etablissement.Longitude = decimal.Parse(this.gridVEtab.GetRowCellDisplayText(i, "Longitude"));
                    client.Etablissements.Add(etablissement);
                }
            }
            ///
            client.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
            client.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
            client.Sauvegarder();

            XtraMessageBox.Show("Enregistrement avec succés. ",
                                     Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
            if (enregistrerEtFermer)
            {
                this.Close();
            }
            else
            {
                Actualiser();
                //this.Text = Resources.Titre_FrmClient + ": " + client.CClient;
                //this._CodeClient = client.CClient;
            }
        }

        private void LoadData()
        {
            char[] CLECONTROL = "ABCDEFGHJKLMNPQRSTVWXYZ".ToCharArray();
            char[] CODETVA = "APBDN".ToCharArray();
            string[] CODETVATITRE = { "A : Assujetti obligatoire à la TVA", "P : Assujetti partiel à la TVA", "B : Assujetti par option à la TVA", "D : Assujetti partel par option à la TVA", "N : Non Assujetti à la TVA" };
            char[] CODECATEGORIE = "MPCNE".ToCharArray();
            string[] CODECATEGORIETITRE = { "M : PERSONNE MORAL", "P : PERSONNE PHYSIQUE,PROFESSION LIBERALE", "C : PERSONNE PHYSIQUE,ACTIVITE COMMERCIAL OU INDUSTRIELLE", "N : PERSONNE NON SOUMISE A L'IMPOT", "E : NE S'AGIT PAS DE L'ETABLISSEMENT PRINCIPAL" };
            CleCollection CLECONTROLCol = new CleCollection();
            for (int i = 0; i < CLECONTROL.Length; i++)
            {
                Cle cll = new Cle();
                cll.Code = CLECONTROL[i].ToString(); cll.Libelle = CLECONTROL[i].ToString();
                CLECONTROLCol.Add(cll);
            }
            CleCollection CODETVACol = new CleCollection();
            for (int i = 0; i < CODETVA.Length; i++)
            {
                Cle cll = new Cle();
                cll.Code = CODETVA[i].ToString(); cll.Libelle = CODETVATITRE[i].ToString();
                CODETVACol.Add(cll);
            }
            CleCollection CODECATEGORIECol = new CleCollection();
            for (int i = 0; i < CODECATEGORIE.Length; i++)
            {
                Cle cll = new Cle();
                cll.Code = CODECATEGORIE[i].ToString(); cll.Libelle = CODECATEGORIETITRE[i].ToString();
                CODECATEGORIECol.Add(cll);
            }
            CtrlHelper.FillLookUpEdit(this.lkpcle, CLECONTROLCol);
            CtrlHelper.FillLookUpEdit(this.lkpcodetva, CODETVACol);
            CtrlHelper.FillLookUpEdit(this.lkpcodecateg, CODECATEGORIECol);
            CtrlHelper.FillLookUpEdit(this.LkpCFamille, ClientFamilleCollection.Charger());
            if (ClientFamilleCollection.Charger().Count == 1)
                LkpCFamille.ItemIndex = 0;
           
            CtrlHelper.FillLookUpEdit(this.lkpCVendeur, CommercialCollection.Charger());
            if (CommercialCollection.Charger().Count == 1)
                lkpCVendeur.ItemIndex = 0;

            CtrlHelper.FillLookUpEdit(this.lkpRecouvreur, CommercialCollection.Charger());
            if (CommercialCollection.Charger().Count == 1)
                lkpRecouvreur.ItemIndex = 0;

            CtrlHelper.FillLookUpEdit(this.lkpCRegion, RegionCollection.Charger());
            if (RegionCollection.Charger().Count == 1)
                lkpCRegion.ItemIndex = 0;

            CtrlHelper.FillLookUpEdit(this.lkpGouvernorat, GouvernoratCollection.Charger());
            if (GouvernoratCollection.Charger().Count == 1)
                lkpGouvernorat.ItemIndex = 0;
            //etablissement
            CtrlHelper.FillLookUpEdit(this.lkpRegionEtab, RegionCollection.Charger());
            if (RegionCollection.Charger().Count == 1)
                lkpRegionEtab.ItemIndex = 0;
            CtrlHelper.FillLookUpEdit(this.lkpCPaysFacturation, PaysCollection.Charger());
            this.lkpCPaysFacturation.EditValue = "TN";

            if (string.IsNullOrWhiteSpace(this.lkpCPaysFacturation.Text))
                this.lkpCPaysFacturation.ItemIndex = 0;

            CtrlHelper.FillLookUpEdit(this.lkpCPaysLivraison, PaysCollection.Charger());
            this.lkpCPaysLivraison.EditValue = "TN";

            if (string.IsNullOrWhiteSpace(this.lkpCPaysLivraison.Text))
                this.lkpCPaysLivraison.ItemIndex = 0;

            this.PageClient.SelectedTabPage = this.tabVente;
            this.chkBTransfertCompta.Checked = true;
            this.radioMajore.Checked = true;
            CtrlHelper.FillLookUpEdit(this.lkpCTarif, TarifCollection.Charger());
            this.lkpCTarif.ItemIndex = 0;
            CtrlHelper.FillLookUpEdit(this.lkpModePaiement, ModeReglementCollection.Charger_SansAvr());
            //this.lkpModePaiement.ItemIndex = 6;

            radioFodecNonExonore.Checked = true;
            radioTPENonExonere.Checked = true;
            radioNonExoTDC.Checked = true;
            radioTVANonExonore.Checked = true;
            radioLocale.Checked = true;
            radioTimbreNonExonore.Checked = true;
            radioActif.Checked = true;
            radioNonVIP.Checked = true;
            chkClientPassager.Checked = false;
            this.chkBEtablissement.Checked = false;
            radioNonMajore.Checked = true;

            CtrlHelper.InitGridView(this.gridVClientBanque, TitresClientBanque(), true);
            CtrlHelper.InitGridView(this.gridVClientContact, TitresListeContact(), true);
            CtrlHelper.InitGridView(this.gridVEtab, TitresEtab(), false);
            if (!string.IsNullOrEmpty(this._CodeClient))
            {
                ChargerEntite(this._CodeClient);
            }
            else
            {
                this.txtCClient.EditValue = Client.NouveauCodeClient();
                this.Text = Resources.Titre_FrmClient;
            }

            this.PageClient.SelectedTabPage = this.tabInfo;
        }
        //Etablissement
        private void RemplirGridVListeEtab()
        {

            DataTable dtListeBL = new DataTable();

            try
            {


                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Client_Vue_RechercherEtab";
                    cmd.Parameters.AddWithValue("@CClient", txtCClient.Text);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dtListeBL);
                }

                CtrlHelper.FillGridView(gridVEtab, TitresEtab(), dtListeBL);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public GvColumnProprietes TitresEtab()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();

            proprietes.Add(new GvColumnPropriete("Code Etablissement"));
            proprietes.Add(new GvColumnPropriete("Libellé"));
            proprietes.Add(new GvColumnPropriete("Région"));
            proprietes.Add(new GvColumnPropriete("Adresse"));
            proprietes.Add(new GvColumnPropriete("Ville"));
            proprietes.Add(new GvColumnPropriete("Code Postale"));
            proprietes.Add(new GvColumnPropriete("Latitude", GvColumnPropriete.GvColumnType.Currency));
            proprietes.Add(new GvColumnPropriete("Longitude", GvColumnPropriete.GvColumnType.Currency));

            return proprietes;
        }
        
        private GvColumnProprietes TitresClientBanque()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, BanqueCollection.Charger()));
            proprietes.Add(new GvColumnPropriete("Agence", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("RIB", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            return proprietes;
        }

        private GvColumnProprietes TitresListeContact()
        {
            GvColumnProprietes proprites = new GvColumnProprietes();
            proprites.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnType.Integer, GvColumnPropriete.GvColumnEtat.Enable));
            proprites.Add(new GvColumnPropriete("Principal", GvColumnPropriete.GvColumnType.Boolean, GvColumnPropriete.GvColumnEtat.Enable));
            proprites.Add(new GvColumnPropriete("Civilité", GvColumnPropriete.GvColumnType.LookUpVide, GvColumnPropriete.GvColumnEtat.Enable, CiviliteCollection.Charger()));
            proprites.Add(new GvColumnPropriete("Nom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprites.Add(new GvColumnPropriete("Prénom", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprites.Add(new GvColumnPropriete("Fonction", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            proprites.Add(new GvColumnPropriete("Téléphone", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            // proprites.Add(new GvColumnPropriete("Portable", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Invisible));
            proprites.Add(new GvColumnPropriete("Email", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            //    proprites.Add(new GvColumnPropriete("Interlocuteur", GvColumnPropriete.GvColumnType.String, GvColumnPropriete.GvColumnEtat.Enable));
            return proprites;
        }

        #endregion Utilitaires

        #region Action

        public void Actualiser()
        {
            //int longeurGridBanque = gridVClientBanque.RowCount;
            //int longeurGridContact = gridVClientContact.RowCount;
            //CtrlHelper.EmptyControls(this);
            //for (int i = 0; i < longeurGridBanque; i++)
            //    this.gridVClientBanque.DeleteRow(0);
            //for (int i = 0; i < longeurGridContact; i++)
            //    this.gridVClientContact.DeleteRow(0);
            //if (!string.IsNullOrEmpty(this._CodeClient))
            //    ChargerEntite(this._CodeClient);
            //else
            //{
            //    this.txtCClient.EditValue = ChargerCodeClient();
            //    this.Text = Resources.Titre_FrmClient;
            //}
            CtrlHelper.EmptyControls(this);

            txtLibAdresseFac.Text = string.Empty;
            txtLibAdresseFac.EditValue = string.Empty;

            txtLibAdresseLiv.Text = string.Empty;
            txtLibAdresseLiv.EditValue = string.Empty;

            for (int i = 0; i < gridVClientBanque.RowCount; i++)
                this.gridVClientBanque.DeleteRow(0);

            for (int i = 0; i < gridVClientContact.RowCount; i++)
                this.gridVClientContact.DeleteRow(0);

            LoadData();
        }

        #endregion Action

        private void FrmClientSaisie_Load(object sender, EventArgs e)
        {
            CtrlHelper.InitValidationProvider(this.dxValidationProvider1, this);
            LoadData();
        }

        private void FrmClientSaisie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3 || e.KeyCode == Keys.F4)
            {
                Control item = this.ActiveControl;

                if (item.GetType().Name == "TextBoxMaskBox")
                {
                    TextEdit txtSelection = (TextEdit)item.Parent;

                    if ((txtSelection.Tag != null) && (txtSelection.IsEditorActive) &&
                   (!string.IsNullOrEmpty(txtSelection.Tag.ToString())))
                    {
                        string source = txtSelection.Tag.ToString().Trim().ToUpper();

                        if ((source.Contains("ARTICLE")) || (source.Contains("FOURNISSEUR")) || (source.Contains("CLIENT")))
                        {
                            bool bRechercheParCode = true;
                            if (e.KeyCode == Keys.F4) bRechercheParCode = false;
                            string selectedvalue = HelperRecherche.FindFieldValue(source, txtSelection.Text, bRechercheParCode);

                            if (!string.IsNullOrEmpty(selectedvalue) && txtSelection.Text != selectedvalue)
                                txtSelection.Text = selectedvalue;
                        }
                    }
                }
            }
            if (e.Control && e.KeyCode == Keys.S)
                Enregistrer(false);
        }

        private void txtCClient_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCClient.Text))
            {
                VideChamps();
                return;
            }
            else
            {
                if (txtCClient.Text.Length > 4)
                {
                    if (txtCClient.Text.Substring(0, 3).Equals("411"))
                        ChargerEntite(txtCClient.Text);
                    else
                    {
                        XtraMessageBox.Show("Code Client doit commencer par 411 !! ",
                              Resources.NomApplication,
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information,
                              MessageBoxDefaultButton.Button1);
                        txtCClient.Text = null;
                    }
                }
                //else {
                //    XtraMessageBox.Show("Code Client doit avoir 8 chiffres !! ",
                //                  Resources.NomApplication,
                //                  MessageBoxButtons.OK,
                //                  MessageBoxIcon.Information,
                //                  MessageBoxDefaultButton.Button1);
                //    txtCClient.Text = null;
                //}
            }
        }

        private void VideChamps()
        {
            CtrlHelper.EmptyControls(this);

            for (int i = 0; i < this.gridVClientBanque.RowCount; i++)
                this.gridVClientBanque.DeleteRow(0);
            for (int i = 0; i < this.gridVClientContact.RowCount; i++)
                this.gridVClientContact.DeleteRow(0);

            this.txtCClient.Text = string.Empty;
            this.LkpCFamille.ItemIndex = 0;
            this.lkpCVendeur.ItemIndex = 0;
            this.lkpRecouvreur.ItemIndex = 0;
            this.lkpCPaysFacturation.EditValue = "TN";
            this.lkpCPaysLivraison.EditValue = "TN";
            this.PageClient.SelectedTabPage = this.tabVente;
            this.chkBTransfertCompta.Checked = true;
            this.radioMajore.Checked = true;
            this.lkpCTarif.ItemIndex = 3;
            this.lkpModePaiement.ItemIndex = 6;
            this.PageClient.SelectedTabPage = this.tabInfo;
        }

        private void txtTauxRemise_Validated(object sender, EventArgs e)
        {
            this.txtTauxRemise.Text = Convert.ToDecimal(this.txtTauxRemise.Text).ToString("0.00");
        }

        private void txtMontantExonoreTVA_Validated(object sender, EventArgs e)
        {
            this.txtMontantExonoreTVA.Text = Convert.ToDecimal(this.txtMontantExonoreTVA.Text).ToString("0.000");
        }

        private void txtTauxRetenuSource_Validated(object sender, EventArgs e)
        {
            this.txtTauxRetenuSource.Text = Convert.ToDecimal(this.txtTauxRetenuSource.Text).ToString("0.00");
        }

        private void txtTauxRetenuTVA_Validated(object sender, EventArgs e)
        {
            this.txtTauxRetenuTVA.Text = Convert.ToDecimal(this.txtTauxRetenuTVA.Text).ToString("0.00");
        }

        private void txtMontantCreditMin_Validated(object sender, EventArgs e)
        {
            this.txtMontantCreditMin.Text = Convert.ToDecimal(this.txtMontantCreditMin.Text).ToString("0.000");
        }

        private void txtMontantCreditMax_Validated(object sender, EventArgs e)
        {
            this.txtMontantCreditMax.Text = Convert.ToDecimal(this.txtMontantCreditMax.Text).ToString("0.000");
        }

        private void gridVClientBanque_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (this.gridVClientBanque.SelectedRowsCount == 0)
                    return;
                this.gridVClientBanque.DeleteRow(this.gridVClientBanque.GetSelectedRows()[0]);
            }
        }

        private void gridVClientBanque_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            string val = gridVClientBanque.GetRowCellValue(e.RowHandle, view.Columns["Code"]).ToString();
            if (string.IsNullOrEmpty(val))
            {
                e.Valid = false;
                e.ErrorText = "Code banque est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientBanque.GetFocusedRowCellDisplayText("Agence")))
            {
                e.Valid = false;
                e.ErrorText = "Agence est non renseignée !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientBanque.GetFocusedRowCellDisplayText("RIB")))
            {
                e.Valid = false;
                e.ErrorText = "Clé R.I.B est non renseignée !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (!Client.VerificationRIB(gridVClientBanque.GetFocusedRowCellDisplayText("RIB")))
            {
                e.Valid = false;
                e.ErrorText = "R.I.B non Valide !";
                view.SetColumnError(null, e.ErrorText);
            }
        }

        private void gridVClientContact_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (this.gridVClientContact.SelectedRowsCount == 0)
                    return;
                this.gridVClientContact.DeleteRow(this.gridVClientContact.GetSelectedRows()[0]);
            }
        }

        private void gridVClientContact_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            ColumnView view = sender as ColumnView;
            string val = gridVClientContact.GetRowCellValue(e.RowHandle, view.Columns["Code"]).ToString();
            if (string.IsNullOrEmpty(val))
            {
                e.Valid = false;
                e.ErrorText = "Code contact est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientContact.GetFocusedRowCellDisplayText("Principal")))
            {
                e.Valid = false;
                e.ErrorText = "Champs principal est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientContact.GetFocusedRowCellDisplayText("Civilité")))
            {
                e.Valid = false;
                e.ErrorText = "Civilité est non renseignée !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientContact.GetFocusedRowCellDisplayText("Nom")))
            {
                e.Valid = false;
                e.ErrorText = "Nom est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientContact.GetFocusedRowCellDisplayText("Prénom")))
            {
                e.Valid = false;
                e.ErrorText = "Prénom est non renseigné !";
                view.SetColumnError(null, e.ErrorText);
            }
            else if (string.IsNullOrEmpty(gridVClientContact.GetFocusedRowCellDisplayText("Fonction")))
            {
                e.Valid = false;
                e.ErrorText = "Fonction est non renseignée !";
                view.SetColumnError(null, e.ErrorText);
            }
        }

        private void chkAdresseLivraison_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void radioFodecNonExonore_Click(object sender, EventArgs e)
        {
            if (radioFodecNonExonore.Checked == true)
                txtDateFinExoFodec.Text = string.Empty;
        }

        private void radioTVANonExonore_Click(object sender, EventArgs e)
        {
            if (this.radioTVANonExonore.Checked == true)
            {
                this.txtMontantExonoreTVA.Text = 0.ToString("0.000");
                this.txtDateFinExoTVA.Text = string.Empty;
            }
        }

        private void chkAdresseLivraison_Validated(object sender, EventArgs e)
        {
            if (this.chkAdresseLivraison.Checked)
            {
                this.txtLibAdresseLiv.Enabled = false;
                this.lkpCPaysLivraison.Enabled = false;
                this.txtCPostalLiv.Enabled = false;
                this.txtVilleAdresseLiv.Enabled = false;
            }
            else
            {
                this.txtLibAdresseLiv.Enabled = true;
                this.lkpCPaysLivraison.Enabled = true;
                this.txtCPostalLiv.Enabled = true;
                this.txtVilleAdresseLiv.Enabled = true;
            }
        }

        private void radioSuspension_CheckedChanged(object sender, EventArgs e)
        {
            if (radioSuspension.Checked)
            {
                txtNAutorisation.Enabled = true;
                txtDateDebSusp.Enabled = true;
                txtDateFinSusp.Enabled = true;
            }
            else
            {
                txtNAutorisation.Enabled = false;
                txtDateDebSusp.Enabled = false;
                txtDateFinSusp.Enabled = false;
            }
        }

        private void radioTPENonExonere_CheckedChanged(object sender, EventArgs e)
        {
            if (radioTPENonExonere.Checked == true)
                txtDateFinExoTPE.Text = string.Empty;
        }

        private void radioNonExoTDC_CheckedChanged(object sender, EventArgs e)
        {
            if (radioNonExoTDC.Checked == true)
                txtDateFinExoTDC.Text = string.Empty;
        }

        private void textEdit1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            txtRaisonSocialeFour.Text = String.Empty;
            if (!string.IsNullOrEmpty(this.txtCFournisseur.Text))
            {
                Fournisseur fournisseur = Fournisseur.Charger(this.txtCFournisseur.Text);
                if (fournisseur != null)
                    txtRaisonSocialeFour.EditValue = fournisseur.RaisonSociale;
                else
                {
                    txtRaisonSocialeFour.Text = String.Empty;
                    txtCFournisseur.Text = string.Empty;
                }
            }
        }

        private void bttparcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Title = "Open PDF/DOC/TXT/BMP/JPG File";

            fDialog.Filter = "PDF Files|*.pdf|DOC Files|*.doc|TXT Files|*.txt|BMP Files|*.bmp|JPG Files|*.jpg";

            fDialog.InitialDirectory = @"C:\";

            int size = -1;
            DialogResult result = fDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = fDialog.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                    this.txtemplacement.Text = file;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void bttapercu_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtemplacement.Text))
                Process.Start(this.txtemplacement.Text);
        }

        private void gridVEtab_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            this.txtCodeEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Code Etablissement");
            this.txtLibEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Libellé");
            this.lkpRegionEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Région").ToString();
            this.txtadresseEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Adresse");
            this.txtVilleEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Ville");
            this.txtCodePostalEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Code Postale");
            this.txtlongEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Longitude");
            this.txtlatEtab.EditValue = this.gridVEtab.GetFocusedRowCellValue("Latitude");

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.lkpRegionEtab.Text))
            {
                XtraMessageBox.Show("Veuillez choisir la Region !",
                                        Resources.NomApplication,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtCodeEtab.EditValue.ToString()))
            {
                XtraMessageBox.Show("Veuillez Saisir le Code Etablissement !",
                                       Resources.NomApplication,
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtLibEtab.Text))
            {
                XtraMessageBox.Show("Veuillez Saisir le Libellé de L'Etablissement !",
                                        Resources.NomApplication,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


                return;
            }


            string codeetablis = this.txtCodeEtab.Text;
            //Etablissement etabli = Etablissement.Charger(codeetablis);
            //if (etabli != null)
            //{

            //    XtraMessageBox.Show("Ce Code existe déjà ! Veuillez Choisir un autre Code Etablissement  !",
            //                                Resources.NomApplication,
            //                                MessageBoxButtons.OK,
            //                                MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);


            //    return;
            //}

            bool trouve = false;
            for (int i = 0; i < gridVEtab.RowCount; i++)
            {
                if (this.txtCodeEtab.Text.Equals(this.gridVEtab.GetRowCellValue(i, "Code Etablissement").ToString()))
                {

                    trouve = true;
                    DialogResult dialogResult = XtraMessageBox.Show(" Cet Etablissement existe déjà ! Voulez-vous le modifier ? ",
                                                       Resources.NomApplication,
                                                       MessageBoxButtons.YesNo,
                                                       MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                    if (dialogResult == DialogResult.No)
                        return;
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.gridVEtab.SetRowCellValue(i, "Libellé", txtLibEtab.Text);
                        this.gridVEtab.SetRowCellValue(i, "Région", lkpRegionEtab.EditValue);
                        this.gridVEtab.SetRowCellValue(i, "Adresse", txtadresseEtab.Text);
                        this.gridVEtab.SetRowCellValue(i, "Ville", txtVilleEtab.Text);
                        this.gridVEtab.SetRowCellValue(i, "Code Postale", txtCodePostalEtab.Text);
                        this.gridVEtab.SetRowCellValue(i, "Latitude", txtlatEtab.Text);
                        this.gridVEtab.SetRowCellValue(i, "Longitude", txtlongEtab.Text);

                        break;
                    }
                }
            }


            if (!trouve)
            {
                this.gridVEtab.AddNewRow();
                this.gridVEtab.SetFocusedRowCellValue("Code Etablissement", txtCodeEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Libellé", txtLibEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Région", lkpRegionEtab.EditValue);
                this.gridVEtab.SetFocusedRowCellValue("Adresse", txtadresseEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Ville", txtVilleEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Code Postale", txtCodePostalEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Latitude", txtlatEtab.Text);
                this.gridVEtab.SetFocusedRowCellValue("Longitude", txtlongEtab.Text);

                this.gridVEtab.UpdateCurrentRow();

            }

            this.txtCodeEtab.EditValue = string.Empty;
            this.txtLibEtab.EditValue = string.Empty;
            this.lkpRegionEtab.EditValue = string.Empty;
            this.txtadresseEtab.EditValue = string.Empty;
            this.txtVilleEtab.EditValue = string.Empty;
            this.txtCodePostalEtab.EditValue = string.Empty;
            this.txtlongEtab.EditValue = string.Empty;
            this.txtlatEtab.EditValue = string.Empty;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if ( ((!(this.gridVEtab.RowCount == 1)) && (!this.gridVEtab.GetFocusedRowCellDisplayText("Code Etablissement").Equals(this.txtCClient.Text)) ) || !chkBEtablissement.Checked )
            {
                this.gridVEtab.DeleteSelectedRows();
            } 

            else 
            {
                XtraMessageBox.Show("Le client doit avoir au moins un établissement", "Alerte");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtemplacement.Text))
            {
                DialogResult dialogResult = XtraMessageBox.Show("Etes-vous sur de vouloir vider l'emplacement?",
                           Resources.NomApplication,
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dialogResult == DialogResult.Cancel)
                    return;
                else
                    txtemplacement.Text = string.Empty;
            }
        }

        private void lkpcodecateg_EditValueChanged(object sender, EventArgs e)
        {
            if (this.lkpcodecateg.EditValue != null && this.lkpcodecateg.EditValue.Equals("E"))
            {
                txtnumetablissement.Enabled = true;
            }
            else {
                txtnumetablissement.Enabled = false;
                txtnumetablissement.Text = "000";
            }
        }

        private void txtCTVA_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCTVA.Text))
                return;
            if (txtCTVA.Text.Length != 7)
                txtCTVA.Text = "";
        }

        private void txtnumetablissement_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnumetablissement.Text))
                return;
            if (txtnumetablissement.Text.Length != 3)
                txtnumetablissement.Text = "";
        }

    }
    public class Cle : Item { }
    public class CleCollection : ItemCollection { 
    }
}