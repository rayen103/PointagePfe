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
    public class Achat_Facture
    {
        #region Proriétès
        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("BTransfereeComptabilite")]
        [Bindable(true)]
        public bool BTransfereeComptabilite { get; set; }
        [XmlAttribute("CreditFacture")]
        [Bindable(true)]
        public decimal CreditFacture { get; set; }
        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }
        [XmlAttribute("DateFacture")]
        [Bindable(true)]
        public DateTime? DateFacture { get; set; }
        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }
        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }
        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }
        [XmlAttribute("BImport")]
        [Bindable(true)]
        public bool BImport { get; set; }
        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }
        [XmlAttribute("MontantTimbre")]
        [Bindable(true)]
        public decimal MontantTimbre { get; set; }
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
        [XmlAttribute("RaisonSociale")]
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
        [XmlAttribute("BDetail")]
        [Bindable(true)]
        public bool BDetail { get; set; }
        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }
        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }
        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }
        public Achat_FactureDetailCollection FactureDetailCollection;
        public Achat_FactureTaxeCollection FactureTaxeCollection;
        #endregion

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

        private void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Facture_Inserer";
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", DateTime.Now.Year.ToString());

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        this.NFacture = dr["NFacture"].ToString();
                }

                int i = 1;
                foreach (Achat_FactureDetail factureDetail in FactureDetailCollection)
                {
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.Sauvegarder(transaction);
                }

                foreach (Achat_FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
                IDictionary<string, string> nBonReceptionCollection = (IDictionary<string, string>)((from detail in FactureDetailCollection select new { detail.NBonReception, detail.CEntrepotReception }).Distinct());// FactureDetailCollection.Select(x => x.NBonReception).Distinct();
                foreach (var dic in nBonReceptionCollection)
                {
                    Achat_BonReception bonReception = Achat_BonReception.Charger(dic.Key, dic.Value);
                    if( bonReception !=null)
                    {
                        if (bonReception.BonReceptionDetailCollection.Sum(x => x.Quantite) == this.FactureDetailCollection.Where(x => x.NBonReception == bonReception.NBonReception && x.CEntrepotReception == bonReception.CEntrepot).Sum(x => x.Quantite))
                        {
                            Achat_BonRetourCollection bonRetourCollection = Achat_BonRetourCollection.ChargerParBonReception(bonReception.NBonReception, bonReception.CEntrepot);
                            foreach (Achat_BonRetour bonRetour in bonRetourCollection)
                            {
                                bonRetour.BTransfertAvoir = true;
                                bonRetour.Modifier(transaction);
                            }
                        }
                        bonReception.NFacture = this.NFacture;
                        bonReception.Modifier(transaction);
                    }
                }
                AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, this.MontantTTC, 0, 0, 0, transaction);
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
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Facture_Modifier";
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@BTransfereeComptabilite ", this.BTransfereeComptabilite);
                cmd.Parameters.AddWithValue("@DateFacture ", this.DateFacture);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTimbre", this.MontantTimbre);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@Indice", this.Indice);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@BDetail", this.BDetail);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                SupprimerDetailFactureAnterieurs(transaction);
                SupprimerTaxeFactureAnterieurs(transaction);

                int i = 1;
                foreach (Achat_FactureDetail factureDetail in FactureDetailCollection)
                {
                    factureDetail.NFacture = this.NFacture;
                    factureDetail.Ordre = i++;
                    factureDetail.Sauvegarder(transaction);
                }

                foreach (Achat_FactureTaxe factureTaxe in FactureTaxeCollection)
                {
                    factureTaxe.NFacture = this.NFacture;
                    factureTaxe.Sauvegarder(transaction);
                }
                Achat_Facture ancienFacture = Achat_Facture.Charger(this.NFacture);
                if (ancienFacture.CFournisseur == this.CFournisseur)
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, (this.MontantTTC - ancienFacture.MontantTTC),0,0,0, transaction);
                else
                {
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, this.MontantTTC, 0, 0, 0, transaction);
                    AchatHelper.MiseAJourSoldeFnr(ancienFacture.CFournisseur, -ancienFacture.MontantTTC, 0, 0, 0, transaction);
                }

                IDictionary<string, string> nBonReceptionCollection = (IDictionary<string, string>)((from detail in FactureDetailCollection select new { detail.NBonReception, detail.CEntrepotReception }).Distinct());
                foreach (var dic in nBonReceptionCollection)
                {
                    Achat_BonReception bonReception = Achat_BonReception.Charger(dic.Key, dic.Value);
                    if (bonReception != null)
                    {
                        Achat_BonRetourCollection bonRetourCollection = Achat_BonRetourCollection.ChargerParBonReception(bonReception.NBonReception, bonReception.CEntrepot);
                        if (bonReception.BonReceptionDetailCollection.Sum(x => x.Quantite) == this.FactureDetailCollection.Where(x => x.NBonReception == bonReception.NBonReception && x.CEntrepotReception == bonReception.CEntrepot).Sum(x => x.Quantite))
                        {
                            foreach (Achat_BonRetour bonRetour in bonRetourCollection)
                            {
                                bonRetour.BTransfertAvoir = true;
                                bonRetour.Modifier(transaction);
                            }
                        }
                        else
                        {
                            foreach (Achat_BonRetour bonRetour in bonRetourCollection)
                            {
                                bonRetour.BTransfertAvoir = false;
                                bonRetour.Modifier(transaction);
                            }
                        }
                        bonReception.NFacture = this.NFacture;
                        bonReception.Modifier(transaction);
                    }
                }

                IDictionary<string, string> ancienNBonReceptionCollection = (IDictionary<string, string>)((from detail in ancienFacture.FactureDetailCollection select new { detail.NBonReception, detail.CEntrepotReception }).Distinct());
                var diff = ancienNBonReceptionCollection.Except(nBonReceptionCollection);
                foreach (var item in diff)
                {
                    Achat_BonReception bonReception = Achat_BonReception.Charger(item.Key, item.Value);
                    if (bonReception != null)
                    {

                        Achat_BonRetourCollection bonRetourCollection = Achat_BonRetourCollection.ChargerParBonReception(bonReception.NBonReception, bonReception.CEntrepot);
                        foreach (Achat_BonRetour bonRetour in bonRetourCollection)
                        {
                            bonRetour.BTransfertAvoir = false;
                            bonRetour.Modifier(transaction);
                        }
                        bonReception.NFacture = string.Empty; ;
                        bonReception.Modifier(transaction);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Achat_Facture Charger(string nFacture)
        {
            Achat_Facture facture = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_Facture_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            facture = new Achat_Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                facture.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["DateFacture"] != DBNull.Value)
                                facture.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                facture.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                facture.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                facture.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["MontantHT"] != DBNull.Value)
                                facture.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                facture.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                facture.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                facture.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if (dr["RaisonSociale"] != DBNull.Value)
                                facture.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                facture.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                facture.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                facture.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                facture.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                facture.PCModification = dr["PCModification"].ToString();
                            if (dr["DateInsertion"] != DBNull.Value)
                                facture.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["BDetail"] != DBNull.Value)
                                facture.BDetail = bool.Parse(dr["BDetail"].ToString());
                            if (dr["BTransfereeComptabilite"] != DBNull.Value)
                                facture.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                            if (dr["BImport"] != DBNull.Value)
                                facture.BImport = bool.Parse(dr["BImport"].ToString());
                            if (dr["NPiece"] != DBNull.Value)
                                facture.NPiece = dr["NPiece"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                facture.Observation = dr["Observation"].ToString();
                            if (dr["CreditFacture"] != DBNull.Value)
                                facture.CreditFacture = decimal.Parse(dr["CreditFacture"].ToString());
                            if (dr["MontantTimbre"] != DBNull.Value)
                                facture.MontantTimbre = decimal.Parse(dr["MontantTimbre"].ToString());
                            facture.FactureDetailCollection = Achat_FactureDetailCollection.Charger(nFacture);
                            facture.FactureTaxeCollection = Achat_FactureTaxeCollection.Charger(nFacture);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return facture;
        }

        private void SupprimerTaxeFactureAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Facture_SupprimerTaxes";
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);

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

        private void SupprimerDetailFactureAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Facture_SupprimerDetails";
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);

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


        public void MiseAJourCreditFacture(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Achat_Facture_MiseAJourCredit";
                cmd.Parameters.AddWithValue("@NFacture", this.NFacture);
                cmd.Parameters.AddWithValue("@CreditFacture", this.CreditFacture);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

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
    }

    public class Achat_FactureCollection : List<Achat_Facture>
    {
        public Achat_FactureCollection Charger(string nFacture)
        {
            Achat_FactureCollection collection = new Achat_FactureCollection();
            try 
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Achat_Facture_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach(SqlParameter parameter in cmd.Parameters)
                    {
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;

                    }
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_Facture facture = new Achat_Facture();
                            facture.NFacture = dr["NFacture"].ToString();
                            if(dr["CFournisseur"]!= DBNull.Value)
                                facture.CFournisseur = dr["CFournisseur"].ToString();
                            if(dr["BTransfereeComptabilite"]!= DBNull.Value)
                                facture.BTransfereeComptabilite = bool.Parse(dr["BTransfereeComptabilite"].ToString());
                            if(dr["CreditFacture"]!= DBNull.Value)
                                facture.CreditFacture = decimal.Parse(dr["CreditFacture"].ToString());
                            if(dr["DateInsertion"]!= DBNull.Value)
                                facture.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if(dr["DateFacture"]!= DBNull.Value)
                                facture.DateFacture = DateTime.Parse(dr["DateFacture"].ToString());
                            if(dr["DateModification"]!= DBNull.Value)
                                facture.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if(dr["BExonoreFodec"]!= DBNull.Value)
                                facture.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if(dr["BExonoreTVA"]!= DBNull.Value)
                                facture.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if(dr["BImport"]!= DBNull.Value)
                                facture.BImport = bool.Parse(dr["BImport"].ToString());
                            if(dr["Observation"]!= DBNull.Value)
                                facture.Observation = dr["Observation"].ToString();
                            if(dr["MontantTimbre"]!= DBNull.Value)
                                facture.MontantTimbre = decimal.Parse(dr["MontantTimbre"].ToString());
                            if(dr["MontantHT"]!= DBNull.Value)
                                facture.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if(dr["MontantTTC"]!= DBNull.Value)
                                facture.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());
                            if(dr["MontantTaxe"]!= DBNull.Value)
                                facture.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if(dr["MontantRemise"]!= DBNull.Value)
                                facture.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if(dr["RaisonSociale"]!= DBNull.Value)
                                facture.RaisonSociale = dr["RaisonSociale"].ToString();
                            if(dr["Indice"]!= DBNull.Value)
                                facture.Indice = int.Parse(dr["Indice"].ToString());
                            if(dr["CreePar"]!= DBNull.Value)
                                facture.CreePar = int.Parse(dr["CreePar"].ToString());
                            if(dr["ModifiePar"]!= DBNull.Value)
                                facture.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if(dr["BDetail"]!= DBNull.Value)
                                facture.BDetail = bool.Parse(dr["BDetail"].ToString());
                            if(dr["PCInsertion"]!= DBNull.Value)
                                facture.PCInsertion = dr["PCInsertion"].ToString();
                            if(dr["PCModification"]!= DBNull.Value)
                                facture.PCModification = dr["PCModification"].ToString();
                            if(dr["NPiece"]!= DBNull.Value)
                                facture.NPiece = dr["NPiece"].ToString();
                            collection.Add(facture);
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
            return collection;
        }
    }
}
