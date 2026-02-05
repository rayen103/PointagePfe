using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class Devis
    {
        #region Proriétès

        [XmlAttribute("NDevis")]
        [Bindable(true)]
        public string NDevis { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("DateDevis")]
        [Bindable(true)]
        public DateTime? DateDevis { get; set; }

        [XmlAttribute("DureeValidite")]
        [Bindable(true)]
        public int DureeValidite { get; set; }

        [XmlAttribute("DateRelance")]
        [Bindable(true)]
        public DateTime? DateRelance { get; set; }

        [XmlAttribute("MatriculeFiscale")]
        [Bindable(true)]
        public string MatriculeFiscale { get; set; }

        [XmlAttribute("Adresse")]
        [Bindable(true)]
        public string Adresse { get; set; }

        [XmlAttribute("NTelephone")]
        [Bindable(true)]
        public string NTelephone { get; set; }

        [XmlAttribute("CVendeur")]
        [Bindable(true)]
        public int CVendeur { get; set; }

        [XmlAttribute("BExonoreTVA")]
        [Bindable(true)]
        public bool BExonoreTVA { get; set; }

        [XmlAttribute("BExonoreFodec")]
        [Bindable(true)]
        public bool BExonoreFodec { get; set; }

        [XmlAttribute("BAvanceForfaitaire")]
        [Bindable(true)]
        public bool BAvanceForfaitaire { get; set; }

        [XmlAttribute("BExport")]
        [Bindable(true)]
        public bool BExport { get; set; }

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

        [XmlAttribute("MontantRetenuForfaitaire")]
        [Bindable(true)]
        public decimal MontantRetenuForfaitaire { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("AdresserA")]
        [Bindable(true)]
        public string AdresserA { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

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

        public DevisDetailCollection DevisDetailCollection;
        public DevisTaxeCollection DevisTaxeCollection;

        #endregion Proriétès

        public Devis()
        {
            this.NDevis = string.Empty;
            this.DevisDetailCollection = new DevisDetailCollection();
            this.DevisTaxeCollection = new DevisTaxeCollection();
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
                cmd.CommandText = "Devis_Inserer";

                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateDevis ", this.DateDevis);
                cmd.Parameters.AddWithValue("@DureeValidite ", this.DureeValidite);
                cmd.Parameters.AddWithValue("@AdresserA", this.AdresserA);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@DateRelance", this.DateRelance);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                //cmd.Parameters.AddWithValue("@Indice", this.Indice);
                cmd.Parameters.AddWithValue("@BExport", this.BExport);
                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);

                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NDevis = dr["NDevis"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (DevisDetail DevisDetail in DevisDetailCollection)
                {
                    DevisDetail.NDevis = this.NDevis;
                    DevisDetail.Ordre = i++;
                    DevisDetail.Sauvegarder(transaction);
                }
                this.SupprimerTaxeDevisAnterieurs(transaction);
                foreach (DevisTaxe DevisTaxe in DevisTaxeCollection)
                {
                    DevisTaxe.NDevis = this.NDevis;
                    DevisTaxe.Sauvegarder(transaction);
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
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Devis_Modifier";
                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", this.MatriculeFiscale);
                cmd.Parameters.AddWithValue("@Adresse", this.Adresse);
                cmd.Parameters.AddWithValue("@NTelephone", this.NTelephone);
                cmd.Parameters.AddWithValue("@CVendeur ", this.CVendeur);
                cmd.Parameters.AddWithValue("@DateDevis ", this.DateDevis);
                cmd.Parameters.AddWithValue("@DureeValidite ", this.DureeValidite);
                cmd.Parameters.AddWithValue("@AdresserA", this.AdresserA);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@MontantRetenuForfaitaire", this.MontantRetenuForfaitaire);
                cmd.Parameters.AddWithValue("@DateRelance", this.DateRelance);

                cmd.Parameters.AddWithValue("@BExport", this.BExport);

                cmd.Parameters.AddWithValue("@BExonoreTVA", this.BExonoreTVA);
                cmd.Parameters.AddWithValue("@BExonoreFodec", this.BExonoreFodec);
                cmd.Parameters.AddWithValue("@BAvanceForfaitaire", this.BAvanceForfaitaire);

                cmd.Parameters.AddWithValue("@MontantHT", this.MontantHT);
                cmd.Parameters.AddWithValue("@MontantRemise", this.MontantRemise);
                cmd.Parameters.AddWithValue("@MontantTaxe", this.MontantTaxe);
                cmd.Parameters.AddWithValue("@MontantTTC", this.MontantTTC);

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
                this.SupprimerDetailDevisAnterieurs(transaction);
                this.SupprimerTaxeDevisAnterieurs(transaction);
                int i = 0;
                foreach (DevisDetail DevisDetail in DevisDetailCollection)
                {
                    DevisDetail.NDevis = this.NDevis;
                    DevisDetail.Ordre = i++;
                    DevisDetail.Sauvegarder(transaction);
                }

                foreach (DevisTaxe DevisTaxe in DevisTaxeCollection)
                {
                    DevisTaxe.NDevis = this.NDevis;
                    DevisTaxe.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RecupererNouveauNDevis(string exercice, out int indice)
        {
            string nDevis = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                var cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "Devis_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nDevis = dr["NDevis"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nDevis;
        }

        public static string RecupererNouveauNDevis(string exercice)
        {
            int indice = 0;
            return Devis.RecupererNouveauNDevis(exercice, out indice);
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();

                try
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Devis_Supprimer";
                    cmd.Parameters.AddWithValue("@NDevis", this.NDevis);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private void SupprimerDetailDevisAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Devis_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);

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

        private void SupprimerTaxeDevisAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Devis_SupprimerTaxes";

                cmd.Parameters.AddWithValue("@NDevis", this.NDevis);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static public Devis Charger(string nDevis)
        {
            Devis devis = null;
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
                    cmd.CommandText = "Devis_Charger";
                    cmd.Parameters.AddWithValue("@NDevis", nDevis);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            devis = new Devis();
                            devis.NDevis = dr["NDevis"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                devis.CUnite = dr["CUnite"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                devis.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                devis.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                devis.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                devis.CClient = dr["CClient"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                devis.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                devis.Indice = int.Parse(dr["Indice"].ToString());

                            if (dr["DateDevis"] != DBNull.Value)
                                devis.DateDevis = DateTime.Parse(dr["DateDevis"].ToString());
                            if (dr["DureeValidite"] != DBNull.Value)
                                devis.DureeValidite = int.Parse(dr["DureeValidite"].ToString());
                            if (dr["DateRelance"] != DBNull.Value)
                                devis.DateRelance = DateTime.Parse(dr["DateRelance"].ToString());

                            if (dr["AdresserA"] != DBNull.Value)
                                devis.AdresserA = dr["AdresserA"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                devis.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                devis.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                devis.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());
                            if (dr["BExport"] != DBNull.Value)
                                devis.BExport = bool.Parse(dr["BExport"].ToString());

                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                devis.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                devis.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                devis.RaisonSociale = dr["RaisonSociale"].ToString();

                            if (dr["MontantHT"] != DBNull.Value)
                                devis.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                devis.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                devis.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                devis.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                devis.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());

                            devis.DevisDetailCollection = DevisDetailCollection.Charger(devis.NDevis);
                            devis.DevisTaxeCollection = DevisTaxeCollection.Charger(devis.NDevis);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (devis);
            }
        }
    }

    public class DevisCollection : List<Devis>
    {
        public DevisCollection()
        {
        }

        public static DevisCollection Charger()
        {
            DevisCollection collection = new DevisCollection();
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
                    cmd.CommandText = "Devis_Charger";
                    cmd.Parameters.AddWithValue("@NDevis", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Devis devis = new Devis();

                            devis.NDevis = dr["NDevis"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                devis.CUnite = dr["CUnite"].ToString();
                            if (dr["MatriculeFiscale"] != DBNull.Value)
                                devis.MatriculeFiscale = dr["MatriculeFiscale"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                devis.Adresse = dr["Adresse"].ToString();
                            if (dr["NTelephone"] != DBNull.Value)
                                devis.NTelephone = dr["NTelephone"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                devis.CClient = dr["CClient"].ToString();
                            if (dr["CVendeur"] != DBNull.Value)
                                devis.CVendeur = int.Parse(dr["CVendeur"].ToString());
                            if (dr["AdresserA"] != DBNull.Value)
                                devis.AdresserA = dr["AdresserA"].ToString();
                            if (dr["BExonoreTVA"] != DBNull.Value)
                                devis.BExonoreTVA = bool.Parse(dr["BExonoreTVA"].ToString());
                            if (dr["BExonoreFodec"] != DBNull.Value)
                                devis.BExonoreFodec = bool.Parse(dr["BExonoreFodec"].ToString());
                            if (dr["BAvanceForfaitaire"] != DBNull.Value)
                                devis.BAvanceForfaitaire = bool.Parse(dr["BAvanceForfaitaire"].ToString());

                            //if (dr["BExport"] != DBNull.Value)
                            //    devis.BExport = bool.Parse(dr["BExport"].ToString());

                            if (dr["Observation"] != DBNull.Value)
                                devis.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                devis.RaisonSociale = dr["RaisonSociale"].ToString();

                            if (dr["MontantHT"] != DBNull.Value)
                                devis.MontantHT = decimal.Parse(dr["MontantHT"].ToString());
                            if (dr["MontantRemise"] != DBNull.Value)
                                devis.MontantRemise = decimal.Parse(dr["MontantRemise"].ToString());
                            if (dr["MontantRetenuForfaitaire"] != DBNull.Value)
                                devis.MontantRetenuForfaitaire = decimal.Parse(dr["MontantRetenuForfaitaire"].ToString());
                            if (dr["MontantTaxe"] != DBNull.Value)
                                devis.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["MontantTTC"] != DBNull.Value)
                                devis.MontantTTC = decimal.Parse(dr["MontantTTC"].ToString());

                            devis.DevisDetailCollection = DevisDetailCollection.Charger(devis.NDevis);
                            devis.DevisTaxeCollection = DevisTaxeCollection.Charger(devis.NDevis);
                            collection.Add(devis);
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