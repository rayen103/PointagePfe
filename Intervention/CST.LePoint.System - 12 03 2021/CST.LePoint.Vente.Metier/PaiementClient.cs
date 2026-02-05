using CST.LePoint.Tiers.Referentiel;
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
    public class PaiementClient
    {
        #region Proprieté

        [XmlAttribute("NFacture")]
        [Bindable(true)]
        public string NFacture { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CReglement")]
        [Bindable(true)]
        public string CReglement { get; set; }

        [XmlAttribute("MontantReglement")]
        [Bindable(true)]
        public decimal MontantReglement { get; set; }

        [XmlAttribute("BAnnulation")]
        [Bindable(true)]
        public bool BAnnulation { get; set; }

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

        #endregion Proprieté

        public PaiementClient()
        {
        }

        public PaiementClient(string nFacture, string cReglement)
        {
            this.NFacture = nFacture;
            this.CReglement = cReglement;
        }

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
            Facture facture = Facture.Charger(this.NFacture, transaction);
            Reglement reglement = Reglement.Charger(this.CReglement, transaction);
            if (this.MontantReglement == 0)
            {
                if (facture.CreditFacture >= reglement.ResteReglement)
                    this.MontantReglement = reglement.ResteReglement;
                else
                    this.MontantReglement = facture.CreditFacture;
            }
            facture.CreditFacture = facture.CreditFacture - this.MontantReglement;

            reglement.ResteReglement = reglement.ResteReglement - this.MontantReglement;

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PaiementClient_Sauvegarder";
                cmd.Parameters.AddWithValue("@NFacture", NFacture);
                cmd.Parameters.AddWithValue("@CReglement", CReglement);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@MontantReglement", MontantReglement);
                cmd.Parameters.AddWithValue("@BAnnulation", BAnnulation);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                facture.ModifiePar = this.CreePar;
                facture.PCModification = this.PCInsertion;
                facture.MiseAJourCreditFacture(transaction);

                if ((reglement.CTypeReglement != VenteHelper.TypeReglement.AVRAVC.ToString()) && (reglement.CTypeReglement != VenteHelper.TypeReglement.AVR.ToString()))
                    VenteHelper.ModifierSolde(null, null, this.CClient, 0, -this.MontantReglement, 0, -this.MontantReglement, 0, 0, transaction);
                else
                    if (reglement.CTypeReglement == VenteHelper.TypeReglement.AVRAVC.ToString())
                    {
                        reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, this.MontantReglement, 0, transaction);
                    }
                    else
                    {
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0, -this.MontantReglement, 0, 0, 0, 0, transaction);
                        reglement.CEtatReglement = VenteHelper.EtatReglement.ASSOCIE.ToString();
                    }
                reglement.ModifiePar = this.CreePar;
                reglement.PCModification = this.PCInsertion;
                reglement.Modifier(transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static PaiementClient Charger(string nFacture, string cReglement)
        {
            PaiementClient paiementClient = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PaiementClient_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            paiementClient = new PaiementClient();

                            if (dr["CClient"] != DBNull.Value)
                                paiementClient.CClient = dr["CClient"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                paiementClient.NFacture = dr["NFacture"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                paiementClient.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["CReglement"] != DBNull.Value)
                                paiementClient.CReglement = dr["CReglement"].ToString();
                            if (dr["MontantReglement"] != DBNull.Value)
                                paiementClient.MontantReglement = decimal.Parse(dr["MontantReglement"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paiementClient;
        }

        public void Liberer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Facture facture = Facture.Charger(this.NFacture, transaction);
                    Reglement reglement = Reglement.Charger(this.CReglement, transaction);

                    facture.CreditFacture = facture.CreditFacture + this.MontantReglement;
                    reglement.ResteReglement = reglement.ResteReglement + this.MontantReglement;

                    if (reglement.CTypeReglement == VenteHelper.TypeReglement.AVRAVC.ToString())
                        VenteHelper.ModifierSolde(null, null, this.CClient, 0, 0, 0, 0, this.MontantReglement, 0, transaction);

                    VenteHelper.ModifierSolde(null, null, this.CClient, 0, this.MontantReglement, 0, this.MontantReglement, 0, 0, transaction);
                    ModeReglement mode = ModeReglement.Charger(reglement.CTypeReglement);

                    if (reglement.ResteReglement == reglement.Montant)
                    {
                        if (!mode.BEcheance)
                            reglement.CEtatReglement = VenteHelper.EtatReglement.ENATTENTE.ToString();
                    }

                    reglement.DateModification = DateTime.Today;
                    reglement.ModifiePar = this.ModifiePar;
                    reglement.Modifier(transaction);

                    facture.ModifiePar = this.CreePar;
                    facture.PCModification = this.PCInsertion;
                    facture.MiseAJourCreditFacture(transaction);

                    Supprimer(transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PaiementClient_Supprimer";
                cmd.Parameters.AddWithValue("@CReglement", CReglement);
                cmd.Parameters.AddWithValue("@NFacture", NFacture);

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
    }

    [Serializable]
    public class PaiementClientCollection : List<PaiementClient>
    {
        public static PaiementClientCollection Charger(string cReglement, string nFacture)
        {
            PaiementClientCollection Collection = new PaiementClientCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PaiementClient_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PaiementClient paiementClient = new PaiementClient();

                            if (dr["CReglement"] != DBNull.Value)
                                paiementClient.CReglement = dr["CReglement"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                paiementClient.CClient = dr["CClient"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                paiementClient.NFacture = dr["NFacture"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                paiementClient.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["MontantReglement"] != DBNull.Value)
                                paiementClient.MontantReglement = SysHelper.ToDecimal(paiementClient.MontantReglement);
                            Collection.Add(paiementClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Collection;
        }

        public static PaiementClientCollection Charger(string nFacture)
        {
            PaiementClientCollection Collection = new PaiementClientCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PaiementClientParFacture_Charger";
                    cmd.Parameters.AddWithValue("@NFacture", nFacture);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            PaiementClient paiementClient = new PaiementClient();

                            if (dr["CReglement"] != DBNull.Value)
                                paiementClient.CReglement = dr["CReglement"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                paiementClient.CClient = dr["CClient"].ToString();
                            if (dr["NFacture"] != DBNull.Value)
                                paiementClient.NFacture = dr["NFacture"].ToString();
                            if (dr["BAnnulation"] != DBNull.Value)
                                paiementClient.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["MontantReglement"] != DBNull.Value)
                                paiementClient.MontantReglement = SysHelper.ToDecimal(dr["MontantReglement"].ToString());
                            Collection.Add(paiementClient);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Collection;
        }
    }
}