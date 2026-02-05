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
    public class Fournisseur
    {
        #region Propriétés

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Abreviation")]
        [Bindable(true)]
        public string Abreviation { get; set; }

        [XmlAttribute("CFamilleFournisseur")]
        [Bindable(true)]
        public string CFamilleFournisseur { get; set; }

        [XmlAttribute("CNatureTiers")]
        [Bindable(true)]
        public int CNatureTiers { get; set; }

        [XmlAttribute("CPays")]
        [Bindable(true)]
        public string CPays { get; set; }

        [XmlAttribute("MatriculeFiscal")]
        [Bindable(true)]
        public string MatriculeFiscal { get; set; }

        [XmlAttribute("CModeReglement")]
        [Bindable(true)]
        public string CModeReglement { get; set; }

        [XmlAttribute("TypeAchat")]
        [Bindable(true)]
        public string TypeAchat { get; set; }

        [XmlAttribute("Contact")]
        [Bindable(true)]
        public string Contact { get; set; }

        [XmlAttribute("DateFinExonorationFODEC")]
        [Bindable(true)]
        public DateTime? DateFinExonorationFODEC { get; set; }

        [XmlAttribute("DateFinExonorationTVA")]
        [Bindable(true)]
        public DateTime? DateFinExonorationTVA { get; set; }

        [XmlAttribute("Email")]
        [Bindable(true)]
        public string Email { get; set; }

        [XmlAttribute("BTVAExonore")]
        [Bindable(true)]
        public bool BTVAExonore { get; set; }

        [XmlAttribute("BFODECExonore")]
        [Bindable(true)]
        public bool BFODECExonore { get; set; }

        [XmlAttribute("Fax")]
        [Bindable(true)]
        public string Fax { get; set; }

        [XmlAttribute("MontantCreditMaximal")]
        [Bindable(true)]
        public decimal MontantCreditMaximal { get; set; }

        [XmlAttribute("MontantCreditMinimal")]
        [Bindable(true)]
        public decimal MontantCreditMinimal { get; set; }

        [XmlAttribute("MontantTVAExonore")]
        [Bindable(true)]
        public decimal MontantTVAExonore { get; set; }

        [XmlAttribute("Nom")]
        [Bindable(true)]
        public string Nom { get; set; }

        [XmlAttribute("Cin")]
        [Bindable(true)]
        public decimal Cin { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("BPassager")]
        [Bindable(true)]
        public bool BPassager { get; set; }

        [XmlAttribute("Remise")]
        [Bindable(true)]
        public decimal Remise { get; set; }

        [XmlAttribute("BMajorationTVA")]
        [Bindable(true)]
        public bool BMajorationTVA { get; set; }

        [XmlAttribute("Telephone1")]
        [Bindable(true)]
        public string Telephone1 { get; set; }

        [XmlAttribute("Telephone2")]
        [Bindable(true)]
        public string Telephone2 { get; set; }

        [XmlAttribute("DelaiEcheancePrevisionnelle")]
        [Bindable(true)]
        public int DelaiEcheancePrevisionnelle { get; set; }

        [XmlAttribute("BTransfertComptabilite")]
        [Bindable(true)]
        public bool BTransfertComptabilite { get; set; }

        [XmlAttribute("Activite")]
        [Bindable(true)]
        public string Activite { get; set; }

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

        [XmlAttribute("Adresses")]
        [Bindable(true)]
        public AdresseCollection Adresses { get; set; }

        [XmlAttribute("Contacts")]
        [Bindable(true)]
        public FournisseurContactCollection Contacts { get; set; }

        public FournisseurBanqueCollection Banques { get; set; }

        #endregion Propriétés

        public Fournisseur()
        {
            this.Adresses = new AdresseCollection();
            this.Contacts = new FournisseurContactCollection();
            this.Banques = new FournisseurBanqueCollection();
        }

        public Fournisseur(string cFournisseur)
        {
            CFournisseur = cFournisseur;
            Adresses = new AdresseCollection();
            Contacts = new FournisseurContactCollection();
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

                    cmd.CommandText = "Fournisseur_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                    cmd.Parameters.AddWithValue("@Abreviation", Abreviation);
                    cmd.Parameters.AddWithValue("@CFamilleFournisseur", CFamilleFournisseur);
                    cmd.Parameters.AddWithValue("@CNatureTiers", CNatureTiers);
                    cmd.Parameters.AddWithValue("@CPays", CPays);
                    cmd.Parameters.AddWithValue("@MatriculeFiscal", MatriculeFiscal);
                    cmd.Parameters.AddWithValue("@CModeReglement", CModeReglement);
                    cmd.Parameters.AddWithValue("@TypeAchat", TypeAchat);
                    cmd.Parameters.AddWithValue("@Contact", Contact);
                    cmd.Parameters.AddWithValue("@DateFinExonorationFODEC", DateFinExonorationFODEC);
                    cmd.Parameters.AddWithValue("@DateFinExonorationTVA", DateFinExonorationTVA);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@BTVAExonore", BTVAExonore);
                    cmd.Parameters.AddWithValue("@BFODECExonore", BFODECExonore);
                    cmd.Parameters.AddWithValue("@Fax", Fax);
                    cmd.Parameters.AddWithValue("@MontantCreditMaximal", MontantCreditMaximal);
                    cmd.Parameters.AddWithValue("@MontantCreditMinimal", MontantCreditMinimal);
                    cmd.Parameters.AddWithValue("@MontantTVAExonore", MontantTVAExonore);
                    cmd.Parameters.AddWithValue("@Nom", Nom);
                    if (this.Cin != 0)
                        cmd.Parameters.AddWithValue("@Cin", Cin);
                    else
                        cmd.Parameters.AddWithValue("@Cin", DBNull.Value);

                    cmd.Parameters.AddWithValue("@Observation", Observation);
                    cmd.Parameters.AddWithValue("@BPassager", BPassager);
                    cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                    cmd.Parameters.AddWithValue("@Remise", Remise);
                    cmd.Parameters.AddWithValue("@BMajorationTVA", BMajorationTVA);
                    cmd.Parameters.AddWithValue("@Telephone1", Telephone1);
                    cmd.Parameters.AddWithValue("@Telephone2", Telephone2);
                    cmd.Parameters.AddWithValue("@DelaiEcheancePrevisionnelle", DelaiEcheancePrevisionnelle);
                    cmd.Parameters.AddWithValue("@BTransfertComptabilite", BTransfertComptabilite);
                    cmd.Parameters.AddWithValue("@Activite", Activite);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter Parameter in cmd.Parameters)
                        if (Parameter.Value == null)
                            Parameter.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();

                    this.SupprimerAdressesAnterieurs(transaction);
                    this.SupprimerContactsAnterieurs(transaction);
                    this.SupprimerBanquesAnterieurs(transaction);
                    foreach (Adresse adresse in Adresses)
                    {
                        adresse.Sauvegarder(transaction);
                    }
                    foreach (FournisseurContact contact in Contacts)
                    {
                        contact.Sauvegarder(transaction);
                    }
                    foreach (FournisseurBanque banque in Banques)
                    {
                        banque.Sauvegarder(transaction);
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

        private void SupprimerAdressesAnterieurs(SqlTransaction transaction)
        {
            AdresseCollection collection = AdresseCollection.Charger(this.CFournisseur);
            foreach (Adresse item in collection)
            {
                if (!this.Adresses.Exists(p => p.NTiers == item.NTiers && p.IdAdresse == item.IdAdresse))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerContactsAnterieurs(SqlTransaction transaction)
        {
            FournisseurContactCollection collection = FournisseurContactCollection.Charger(this.CFournisseur);
            foreach (FournisseurContact item in collection)
            {
                if (!this.Contacts.Exists(p => p.CContact == item.CContact && p.CFournisseur == item.CFournisseur))
                    item.Supprimer(transaction);
            }
        }

        private void SupprimerBanquesAnterieurs(SqlTransaction transaction)
        {
            FournisseurBanqueCollection anciensBanque = FournisseurBanqueCollection.Charger(CFournisseur);
            foreach (FournisseurBanque item in anciensBanque)
            {
                if (!this.Banques.Exists(p => p.CFournisseur.Equals(item.CFournisseur) && p.CBanque.Equals(item.CBanque) && p.Rib.Equals(item.Rib)))
                    item.Supprimer(transaction);
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
                    //foreach (Adresse adresse in Adresses)
                    //{
                    //    adresse.Supprimer(transaction);
                    //}
                    //foreach (FournisseurContact contact in Contacts)
                    //{
                    //    contact.Supprimer(transaction);
                    //}
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "Fournisseur_Supprimer";
                    cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
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

        public static string NouveauCodeFournisseur()
        {
            string code = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Fournisseur_ChargerCode";

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            code = dr["CFournisseur"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
            if (code != null)
            {
                int rang = int.Parse(code.Substring(code.Length - 1));
                int i = rang + 1;

                string cFournisseur = code.Substring(0, code.Length - rang.ToString().Length) + i;
                return cFournisseur;
            }
            return ("40100001");
        }

        public static Fournisseur Charger(string cfournisseur)
        {
            Fournisseur fournisseur = null;

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
                    cmd.CommandText = "Fournisseur_Charger";
                    cmd.Parameters.AddWithValue("@CFournisseur", cfournisseur);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            fournisseur = new Fournisseur();
                            fournisseur.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Abreviation"] != DBNull.Value)
                                fournisseur.Abreviation = dr["Abreviation"].ToString();
                            if (dr["CFamilleFournisseur"] != DBNull.Value)
                                fournisseur.CFamilleFournisseur = dr["CFamilleFournisseur"].ToString();
                            if (dr["CNatureTiers"] != DBNull.Value)
                                fournisseur.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["CPays"] != DBNull.Value)
                                fournisseur.CPays = dr["CPays"].ToString();
                            if (dr["MatriculeFiscal"] != DBNull.Value)
                                fournisseur.MatriculeFiscal = dr["MatriculeFiscal"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                fournisseur.CModeReglement = (dr["CModeReglement"].ToString());
                            if (dr["TypeAchat"] != DBNull.Value)
                                fournisseur.TypeAchat = dr["TypeAchat"].ToString();
                            if (dr["Contact"] != DBNull.Value)
                                fournisseur.Contact = dr["Contact"].ToString();
                            if (dr["DateFinExonorationFODEC"] != DBNull.Value)
                                fournisseur.DateFinExonorationFODEC = DateTime.Parse(dr["DateFinExonorationFODEC"].ToString());
                            if (dr["DateFinExonorationTVA"] != DBNull.Value)
                                fournisseur.DateFinExonorationTVA = DateTime.Parse(dr["DateFinExonorationTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                fournisseur.Email = dr["Email"].ToString();
                            if (dr["BTVAExonore"] != DBNull.Value)
                                fournisseur.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["BFODECExonore"] != DBNull.Value)
                                fournisseur.BFODECExonore = bool.Parse(dr["BFODECExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                fournisseur.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMaximal"] != DBNull.Value)
                                fournisseur.MontantCreditMaximal = decimal.Parse(dr["MontantCreditMaximal"].ToString());
                            if (dr["MontantCreditMinimal"] != DBNull.Value)
                                fournisseur.MontantCreditMinimal = decimal.Parse(dr["MontantCreditMinimal"].ToString());
                            if (dr["MontantTVAExonore"] != DBNull.Value)
                                fournisseur.MontantTVAExonore = decimal.Parse(dr["MontantTVAExonore"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                fournisseur.Nom = dr["Nom"].ToString();
                            if (dr["Cin"] != DBNull.Value)
                                fournisseur.Cin = decimal.Parse(dr["Cin"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                fournisseur.Observation = dr["Observation"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                fournisseur.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                fournisseur.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Remise"] != DBNull.Value)
                                fournisseur.Remise = decimal.Parse(dr["Remise"].ToString());
                            if (dr["BMajorationTVA"] != DBNull.Value)
                                fournisseur.BMajorationTVA = bool.Parse(dr["BMajorationTVA"].ToString());
                            if (dr["Telephone1"] != DBNull.Value)
                                fournisseur.Telephone1 = dr["Telephone1"].ToString();
                            if (dr["Telephone2"] != DBNull.Value)
                                fournisseur.Telephone2 = dr["Telephone2"].ToString();
                            if (dr["DelaiEcheancePrevisionnelle"] != DBNull.Value)
                                fournisseur.DelaiEcheancePrevisionnelle = int.Parse(dr["DelaiEcheancePrevisionnelle"].ToString());
                            if (dr["BTransfertComptabilite"] != DBNull.Value)
                                fournisseur.BTransfertComptabilite = bool.Parse(dr["BTransfertComptabilite"].ToString());
                            if (dr["Activite"] != DBNull.Value)
                                fournisseur.Activite = dr["Activite"].ToString();
                            fournisseur.Adresses = AdresseCollection.Charger(fournisseur.CFournisseur);
                            fournisseur.Contacts = FournisseurContactCollection.Charger(fournisseur.CFournisseur);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (fournisseur);
        }
        public static Fournisseur ChargerVue(string cfournisseur)
        {
            Fournisseur fournisseur = null;

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
                    cmd.CommandText = "Fournisseur_ChargerVue";
                    cmd.Parameters.AddWithValue("@CFournisseur", cfournisseur);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            fournisseur = new Fournisseur();
                            fournisseur.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Abreviation"] != DBNull.Value)
                                fournisseur.Abreviation = dr["Abreviation"].ToString();
                            if (dr["CFamilleFournisseur"] != DBNull.Value)
                                fournisseur.CFamilleFournisseur = dr["CFamilleFournisseur"].ToString();
                            if (dr["CNatureTiers"] != DBNull.Value)
                                fournisseur.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["CPays"] != DBNull.Value)
                                fournisseur.CPays = dr["CPays"].ToString();
                            if (dr["MatriculeFiscal"] != DBNull.Value)
                                fournisseur.MatriculeFiscal = dr["MatriculeFiscal"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                fournisseur.CModeReglement = (dr["CModeReglement"].ToString());
                            if (dr["TypeAchat"] != DBNull.Value)
                                fournisseur.TypeAchat = dr["TypeAchat"].ToString();
                            if (dr["Contact"] != DBNull.Value)
                                fournisseur.Contact = dr["Contact"].ToString();
                            if (dr["DateFinExonorationFODEC"] != DBNull.Value)
                                fournisseur.DateFinExonorationFODEC = DateTime.Parse(dr["DateFinExonorationFODEC"].ToString());
                            if (dr["DateFinExonorationTVA"] != DBNull.Value)
                                fournisseur.DateFinExonorationTVA = DateTime.Parse(dr["DateFinExonorationTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                fournisseur.Email = dr["Email"].ToString();
                            if (dr["BTVAExonore"] != DBNull.Value)
                                fournisseur.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["BFODECExonore"] != DBNull.Value)
                                fournisseur.BFODECExonore = bool.Parse(dr["BFODECExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                fournisseur.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMaximal"] != DBNull.Value)
                                fournisseur.MontantCreditMaximal = decimal.Parse(dr["MontantCreditMaximal"].ToString());
                            if (dr["MontantCreditMinimal"] != DBNull.Value)
                                fournisseur.MontantCreditMinimal = decimal.Parse(dr["MontantCreditMinimal"].ToString());
                            if (dr["MontantTVAExonore"] != DBNull.Value)
                                fournisseur.MontantTVAExonore = decimal.Parse(dr["MontantTVAExonore"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                fournisseur.Nom = dr["Nom"].ToString();
                            if (dr["Cin"] != DBNull.Value)
                                fournisseur.Cin = decimal.Parse(dr["Cin"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                fournisseur.Observation = dr["Observation"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                fournisseur.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                fournisseur.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Remise"] != DBNull.Value)
                                fournisseur.Remise = decimal.Parse(dr["Remise"].ToString());
                            if (dr["BMajorationTVA"] != DBNull.Value)
                                fournisseur.BMajorationTVA = bool.Parse(dr["BMajorationTVA"].ToString());
                            if (dr["Telephone1"] != DBNull.Value)
                                fournisseur.Telephone1 = dr["Telephone1"].ToString();
                            if (dr["Telephone2"] != DBNull.Value)
                                fournisseur.Telephone2 = dr["Telephone2"].ToString();
                            if (dr["DelaiEcheancePrevisionnelle"] != DBNull.Value)
                                fournisseur.DelaiEcheancePrevisionnelle = int.Parse(dr["DelaiEcheancePrevisionnelle"].ToString());
                            if (dr["BTransfertComptabilite"] != DBNull.Value)
                                fournisseur.BTransfertComptabilite = bool.Parse(dr["BTransfertComptabilite"].ToString());
                            if (dr["Activite"] != DBNull.Value)
                                fournisseur.Activite = dr["Activite"].ToString();
                            fournisseur.Adresses = AdresseCollection.Charger(fournisseur.CFournisseur);
                            fournisseur.Contacts = FournisseurContactCollection.Charger(fournisseur.CFournisseur);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (fournisseur);
        }

        private void SupprimerContacts(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Fournisseur_SupprimerListeFournisseur";
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
                
            }
        }

        private void SupprimerAdresses(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "TiersAdresses_Supprimer";
                cmd.Parameters.AddWithValue("@NTiers", CFournisseur);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
                
            }
        }
    }

    public class FournisseurCollection : List<Fournisseur>
    {
        public FournisseurCollection()
        {
        }

        public static DataSet ChargerVue(string cFournisseur, string cPays, string cFamille, string cNature)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Fournisseur_Rpt_Rechercher";
                cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                cmd.Parameters.AddWithValue("@CPays", cPays);
                cmd.Parameters.AddWithValue("@CFamilleFournisseur", cFamille);
                cmd.Parameters.AddWithValue("@CNatureFournisseur", cNature);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Fournisseur_Rpt_Rechercher");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cFournisseur, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string FamilleFournisseur, string nature, string cEntrepot, string cPays)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Fournisseur_Mvt_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
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
                cmd.Parameters.AddWithValue("@NatureFournisseur", nature);
                cmd.Parameters.AddWithValue("@FamilleFournisseur", FamilleFournisseur);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Fournisseur_Mvt_Rpt_Charger");
            }
            return (ds);
        }

        public static DataSet ChargerVue(string cFournisseur, string cArticle, string cCategorie, string cFamille, string cType, string cNature, string cModele, string cModele1, string cModele2, DateTime dateDeb, DateTime dateFin, string cEntrepot, string naturefournisseur, string cPays, int mouvement)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FournisseurMvt_Vue_Rechercher";
                cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CCategorie", cCategorie);
                cmd.Parameters.AddWithValue("@CFamille", cFamille);
                cmd.Parameters.AddWithValue("@CType", cType);
                cmd.Parameters.AddWithValue("@CModele", cModele);
                cmd.Parameters.AddWithValue("@CModele1", cModele1);
                cmd.Parameters.AddWithValue("@CModele2", cModele2);
                cmd.Parameters.AddWithValue("@CNature", cNature);
                cmd.Parameters.AddWithValue("@CPays", cPays);
                cmd.Parameters.AddWithValue("@Naturefamille", naturefournisseur);
                //cmd.Parameters.AddWithValue("@FamilleFournisseur", cFamilleFournisseur);
                cmd.Parameters.AddWithValue("@Mouvement", mouvement);
                cmd.Parameters.AddWithValue("@DateDeb", dateDeb);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Fournisseur_Mvt_Rpt_Charger");
            }
            return (ds);
        }

        public static FournisseurCollection Charger()
        {
            FournisseurCollection collection = new FournisseurCollection();

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
                    cmd.CommandText = "Fournisseur_Charger";
                    cmd.Parameters.AddWithValue("@CFournisseur", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Fournisseur fournisseur = new Fournisseur();
                            fournisseur.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["Abreviation"] != DBNull.Value)
                                fournisseur.Abreviation = dr["Abreviation"].ToString();
                            if (dr["CFamilleFournisseur"] != DBNull.Value)
                                fournisseur.CFamilleFournisseur = dr["CFamilleFournisseur"].ToString();
                            if (dr["CNatureTiers"] != DBNull.Value)
                                fournisseur.CNatureTiers = int.Parse(dr["CNatureTiers"].ToString());
                            if (dr["CPays"] != DBNull.Value)
                                fournisseur.CPays = dr["CPays"].ToString();
                            if (dr["MatriculeFiscal"] != DBNull.Value)
                                fournisseur.MatriculeFiscal = dr["MatriculeFiscal"].ToString();
                            if (dr["CModeReglement"] != DBNull.Value)
                                fournisseur.CModeReglement = (dr["CModeReglement"].ToString());
                            if (dr["TypeAchat"] != DBNull.Value)
                                fournisseur.TypeAchat = dr["TypeAchat"].ToString();
                            if (dr["Contact"] != DBNull.Value)
                                fournisseur.Contact = dr["Contact"].ToString();
                            if (dr["DateFinExonorationFODEC"] != DBNull.Value)
                                fournisseur.DateFinExonorationFODEC = DateTime.Parse(dr["DateFinExonorationFODEC"].ToString());
                            if (dr["DateFinExonorationTVA"] != DBNull.Value)
                                fournisseur.DateFinExonorationTVA = DateTime.Parse(dr["DateFinExonorationTVA"].ToString());
                            if (dr["Email"] != DBNull.Value)
                                fournisseur.Email = dr["Email"].ToString();
                            if (dr["BTVAExonore"] != DBNull.Value)
                                fournisseur.BTVAExonore = bool.Parse(dr["BTVAExonore"].ToString());
                            if (dr["BFODECExonore"] != DBNull.Value)
                                fournisseur.BFODECExonore = bool.Parse(dr["BFODECExonore"].ToString());
                            if (dr["Fax"] != DBNull.Value)
                                fournisseur.Fax = dr["Fax"].ToString();
                            if (dr["MontantCreditMaximal"] != DBNull.Value)
                                fournisseur.MontantCreditMaximal = decimal.Parse(dr["MontantCreditMaximal"].ToString());
                            if (dr["MontantCreditMinimal"] != DBNull.Value)
                                fournisseur.MontantCreditMinimal = decimal.Parse(dr["MontantCreditMinimal"].ToString());
                            if (dr["MontantTVAExonore"] != DBNull.Value)
                                fournisseur.MontantTVAExonore = decimal.Parse(dr["MontantTVAExonore"].ToString());
                            if (dr["Nom"] != DBNull.Value)
                                fournisseur.Nom = dr["Nom"].ToString();
                            if (dr["Cin"] != DBNull.Value)
                                fournisseur.Cin = decimal.Parse(dr["Cin"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                fournisseur.Observation = dr["Observation"].ToString();
                            if (dr["BPassager"] != DBNull.Value)
                                fournisseur.BPassager = bool.Parse(dr["BPassager"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                fournisseur.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Remise"] != DBNull.Value)
                                fournisseur.Remise = decimal.Parse(dr["Remise"].ToString());
                            if (dr["BMajorationTVA"] != DBNull.Value)
                                fournisseur.BMajorationTVA = bool.Parse(dr["BMajorationTVA"].ToString());
                            if (dr["Telephone1"] != DBNull.Value)
                                fournisseur.Telephone1 = dr["Telephone1"].ToString();
                            if (dr["Telephone2"] != DBNull.Value)
                                fournisseur.Telephone2 = dr["Telephone2"].ToString();
                            if (dr["DelaiEcheancePrevisionnelle"] != DBNull.Value)
                                fournisseur.DelaiEcheancePrevisionnelle = int.Parse(dr["DelaiEcheancePrevisionnelle"].ToString());
                            if (dr["BTransfertComptabilite"] != DBNull.Value)
                                fournisseur.BTransfertComptabilite = bool.Parse(dr["BTransfertComptabilite"].ToString());
                            if (dr["Activite"] != DBNull.Value)
                                fournisseur.Activite = dr["Activite"].ToString();
                            fournisseur.Adresses = AdresseCollection.Charger();
                            fournisseur.Contacts = FournisseurContactCollection.Charger(fournisseur.CFamilleFournisseur);
                            collection.Add(fournisseur);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (collection);
        }
    }
}