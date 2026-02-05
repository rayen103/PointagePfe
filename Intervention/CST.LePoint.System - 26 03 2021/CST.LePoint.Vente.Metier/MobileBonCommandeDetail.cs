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

namespace CST.LePoint.Vente.Metier
{
    public class MobileBonCommandeDetail
    {
        #region Proriétès

        public string CArticle { get; set; }
        public string CFamille { get; set; }
        public string CType { get; set; }
        public string CEntrepot { get; set; }
        public string CUnite { get; set; }
        public string LibArticle { get; set; }
        public decimal Poids { get; set; }
        public decimal MontantTaxe { get; set; }
        public decimal MontantNet { get; set; }
        public decimal PourcentageFodec { get; set; }
        public decimal PourcentageTPE { get; set; }
        public decimal PourcentageTDC { get; set; }
        public decimal PourcentageRemise { get; set; }
        public decimal PrixHTArticle { get; set; }
        public decimal Quantite { get; set; }
        public decimal QuantiteHistorique { get; set; }
        public decimal QuantitePurge { get; set; }
        public decimal QuantitePreparee { get; set; }
        public decimal TauxTVA { get; set; }
        public string CTaxe { get; set; }
        public decimal Remise1 { get; set; }
        public decimal Remise2 { get; set; }
        public decimal PrixVentePublic { get; set; }
        public decimal Dividende { get; set; }
        public decimal Diviseur { get; set; }
        public string CGratuites { get; set; }
        public string DateGratuitesDebut { get; set; }
        public string DateGratuitesFin { get; set; }
        public string ImageArt { get; set; }
        public decimal StockReel { get; set; }
        public string gratuite { get; set; }
        public decimal TVA { get; set; }
        public bool BGratuit { get; set; }

        #endregion Proriétès

        public MobileBonCommandeDetail()
        {
        }

    }

    public class MobileBonCommandeDetailCollection : List<MobileBonCommandeDetail>
    {
        public MobileBonCommandeDetailCollection()
        {
        }

        public static MobileBonCommandeDetailCollection Mobile_Charger(string nBonCommande)
        {
            MobileBonCommandeDetailCollection collection = new MobileBonCommandeDetailCollection();

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
                    cmd.CommandText = "Mobile_BonCommandeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileBonCommandeDetail bonCommandeDetail = new MobileBonCommandeDetail();
                           
                            bonCommandeDetail.CArticle = dr["codeArt"].ToString();
                          /* if (dr["CEntrepot"] != DBNull.Value)
                                bonCommandeDetail.CEntrepot = dr["CEntrepot"].ToString();
                           /* if (dr["CTaxe"] != DBNull.Value)
                                bonCommandeDetail.CTaxe = dr["CTaxe"].ToString(); */
                            if (dr["CUnite"] != DBNull.Value)
                                bonCommandeDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["libArt"] != DBNull.Value)
                                bonCommandeDetail.LibArticle = dr["libArt"].ToString();
                            if (dr["CFamille"] != DBNull.Value)
                                bonCommandeDetail.CFamille = dr["CFamille"].ToString();
                         /*   if (dr["Poids"] != DBNull.Value)
                                bonCommandeDetail.Poids = decimal.Parse(dr["Poids"].ToString());
                          /*  if (dr["PourcentageTPE"] != DBNull.Value)
                                bonCommandeDetail.PourcentageTPE = decimal.Parse(dr["PourcentageTPE"].ToString());
                            if (dr["PourcentageTDC"] != DBNull.Value)
                                bonCommandeDetail.PourcentageTDC = decimal.Parse(dr["PourcentageTDC"].ToString()); */
                            if (dr["MontantNet"] != DBNull.Value)
                                bonCommandeDetail.MontantNet = decimal.Parse(dr["MontantNet"].ToString());
                            /*if (dr["MontantTaxe"] != DBNull.Value)
                                bonCommandeDetail.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
                            if (dr["PourcentageFodec"] != DBNull.Value)
                                bonCommandeDetail.PourcentageFodec = decimal.Parse(dr["PourcentageFodec"].ToString());
                            if (dr["PourcentageRemise"] != DBNull.Value)
                                bonCommandeDetail.PourcentageRemise = decimal.Parse(dr["PourcentageRemise"].ToString());   */
                            if (dr["Prix"] != DBNull.Value)
                                bonCommandeDetail.PrixHTArticle = decimal.Parse(dr["Prix"].ToString());
                            if (dr["PrixVentePublic"] != DBNull.Value)
                                bonCommandeDetail.PrixVentePublic = decimal.Parse(dr["PrixVentePublic"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                bonCommandeDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                bonCommandeDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                            if (dr["QuantitePurge"] != DBNull.Value)
                                bonCommandeDetail.QuantitePurge = decimal.Parse(dr["QuantitePurge"].ToString());
                            if (dr["QuantitePreparee"] != DBNull.Value)
                                bonCommandeDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                            if (dr["Remise1"] != DBNull.Value)
                                bonCommandeDetail.Remise1 = decimal.Parse(dr["Remise1"].ToString());
                            if (dr["Remise2"] != DBNull.Value)
                                bonCommandeDetail.Remise2 = decimal.Parse(dr["Remise2"].ToString());
                              ////////////////////////////////////////
                            if (dr["Dividende"] != DBNull.Value)
                                bonCommandeDetail.Dividende = decimal.Parse(dr["Dividende"].ToString());
                            if (dr["Diviseur"] != DBNull.Value)
                                bonCommandeDetail.Diviseur = decimal.Parse(dr["Diviseur"].ToString());
                          /*  if (dr["TauxTVA"] != DBNull.Value)
                                bonCommandeDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());           */
                            if (dr["CGratuites"] != DBNull.Value)
                                bonCommandeDetail.CGratuites = dr["CGratuites"].ToString();
                            if (dr["DateGratuitesDebut"] != DBNull.Value)
                                bonCommandeDetail.DateGratuitesDebut = DateTime.Parse( dr["DateGratuitesDebut"].ToString() ).ToShortDateString();
                            if (dr["DateGratuitesFin"] != DBNull.Value)
                                bonCommandeDetail.DateGratuitesFin = DateTime.Parse(dr["DateGratuitesFin"].ToString()).ToShortDateString();
                            if (dr["Image_Article"] != DBNull.Value)
                                bonCommandeDetail.ImageArt = "data:image/png;base64," + Convert.ToBase64String((byte[])dr["Image_Article"]);
                            if (dr["StockReel"] != DBNull.Value)
                                bonCommandeDetail.StockReel = decimal.Parse(dr["StockReel"].ToString());
                            if (dr["LibGratuites"] != DBNull.Value)
                                bonCommandeDetail.gratuite = dr["LibGratuites"].ToString();
                            if (dr["BGratuit"] != DBNull.Value)
                                bonCommandeDetail.BGratuit = (bool)dr["BGratuit"];
                            bonCommandeDetail.TVA = decimal.Parse( dr["TVA"].ToString() );

                            collection.Add(bonCommandeDetail);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return collection;
            }
        }
    }
}