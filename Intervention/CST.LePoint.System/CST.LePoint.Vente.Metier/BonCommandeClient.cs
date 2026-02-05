using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonCommandeClient
    {
        #region Proriétès

        public string NBonCommande { get; set; }
        public string NumeroTelephone1 { get; set; }
        public string CTBAchat { get; set; }
        public string LibTBAchat { get; set; }
        public string NumeroTelephone2 { get; set; }
        public string CClient { get; set; }
        public string CModeReglement { get; set; }
        public string LibModeReglement { get; set; }
        public string CTypeBonCommande { get; set; }
        public string LibTBCommande { get; set; }
        public string RaisonSociale { get; set; }
        public string Observation { get; set; }
        public Boolean Etat { get; set; }
        public decimal MontantRemise { get; set; }
        public decimal MontantHT { get; set; }
        public decimal MontantnetHT { get; set; }
        public decimal MontantTaxe { get; set; }
        public decimal MontantTTC { get; set; }
        public String MatriculeFiscale { get; set; }
        public string DateLivraison { get; set; }
        public decimal Remise { get; set; }
        public string ModalitesPaiement { get; set; }
        public string LibModalitesPaiement { get; set; }

        #endregion Proriétès

        public static BonCommandeClient Charger(string nBonCommande)
        {
            BonCommandeClient bcClient = new BonCommandeClient();
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
                    cmd.CommandText = "Mobile_BonCommandeClient_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr["CClient"] != DBNull.Value)
                                bcClient.CClient = dr["CClient"].ToString();
                            bcClient.CTypeBonCommande = dr["CTypeBonCommande"].ToString();
                            bcClient.CTBAchat = dr["CTBAchat"].ToString();
                            bcClient.LibTBAchat = dr["LibTBAchat"].ToString();
                            bcClient.NBonCommande = dr["NBonCommande"].ToString();
                            bcClient.NumeroTelephone1 = dr["NumeroTelephone1"].ToString();
                            bcClient.NumeroTelephone2 = dr["NumeroTelephone2"].ToString();
                            bcClient.CTypeBonCommande = dr["CTypeBonCommande"].ToString();
                            bcClient.LibTBCommande = dr["LibTBCommande"].ToString();
                            bcClient.CModeReglement = dr["CModeReglement"].ToString();
                            bcClient.LibModeReglement = dr["LibModeReglement"].ToString();
                            bcClient.RaisonSociale = dr["RaisonSociale"].ToString();
                            bcClient.Observation = dr["Observation"].ToString();
                            bcClient.MontantTTC = Decimal.Parse(dr["MontantTTC"].ToString());
                            bcClient.MontantHT = Decimal.Parse(dr["MontantHT"].ToString());
                            bcClient.MontantTaxe = Decimal.Parse(dr["MontantTaxe"].ToString());
                            bcClient.Remise = Decimal.Parse(dr["Remise"].ToString());
                            bcClient.MontantRemise = Decimal.Parse(dr["MontantRemise"].ToString());
                            bcClient.MontantnetHT = bcClient.MontantHT - bcClient.MontantRemise;
                            if (dr["DateLivraison"] != DBNull.Value)
                                bcClient.DateLivraison = DateTime.Parse( dr["DateLivraison"].ToString()).ToString("yyyy-MM-dd");

                            if (dr["Etat"].ToString().Equals("Valide") ){
                                bcClient.Etat= true;
                            }
                            else
                                bcClient.Etat = false;
                            bcClient.ModalitesPaiement = dr["ModalitesPaiement"].ToString();
                            bcClient.LibModalitesPaiement = dr["LibModalitesPaiement"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return bcClient;
            }
        }
    }
}