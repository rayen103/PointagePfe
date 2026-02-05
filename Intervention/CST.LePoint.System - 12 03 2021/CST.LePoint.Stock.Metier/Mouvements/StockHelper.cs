using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CST.LePoint.Stock.Metier
{
    public class StockHelper
    {
        public static string STATUT_INITIALE_LOT = ConfigurationManager.AppSettings["STATUT_INITIALE_LOT"].ToString();
        public static string NOMAPPLICATION = ConfigurationManager.AppSettings["NomApplication"].ToString() + ".";
        public static int MAJ_PRIX_REVIENT = UpdatePrixRevient();
        public enum TypesMouvementStock
        {
            BE_MANUEL = 0,
            BE_INVENTAIRE = 1,
            BE_BONLIVRAISONINTERNE = 2,
            BE_BONTRANSFORMATION = 3,
            BE_BONLIVRAISONCLIENT = 4,
            BE_BONRETOURCLIENT = 5,
            BE_BONRECEPTION = 6,
            BE_BONRETOURFOURNISSEUR = 7,
            BE_BONPRODUCTION = 17,


            BS_MANUEL = 8,
            BS_INVENTAIRE = 9,
            BS_BONLIVRAISONINTERNE = 10,
            BS_BONTRANSFORMATION = 11,
            BS_BONLIVRAISONCLIENT = 12,
            BS_BONRETOURCLIENT = 13,
            BS_BONRECEPTION = 14,
            BS_BONRETOURFOURNISSEUR = 15,
            BS_DON = 16,
        }

        public enum TypeEntrepot
        {
            PRODUCTION=1,
            STOCKAGE=2
        }

        public static void AjusterStockReel(string cArticle, string cEntrepot, decimal quantite, int Signe, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Article_AjusterStockReel";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Signe", Signe);

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

        public static void AjusterStockReelParLot(string cArticle, string cLot, string cEntrepot, decimal quantite, int Signe, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "ArticleLot_AjusterStockReel";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CLot", cLot);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Signe", Signe);

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

        public static void AjusterStockEnCommandeDAP(string cArticle, string cEntrepot, decimal quantite, int Signe, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Article_AjusterStockEnCommandeDAP";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Signe", Signe);
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

        //public static void AjusterStockReel(string cArticle, string cEntrepot, decimal quantite, int Signe, SqlTransaction transaction)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.CommandText = "Article_AjusterStockReel";
        //        cmd.Parameters.AddWithValue("@CArticle", cArticle);
        //        cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
        //        cmd.Parameters.AddWithValue("@Quantite", quantite);
        //        cmd.Parameters.AddWithValue("@Signe", Signe);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }

        //        cmd.ExecuteNonQuery();
        //    }

        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static void MiseAJourStockReserver(string cArticle, string cEntrepot, decimal quantite, int signe, SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "Article_AjusterStockReserver";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd.Parameters.AddWithValue("@Quantite", quantite);
                cmd.Parameters.AddWithValue("@Signe", signe);

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

        public static int UpdatePrixRevient()
        {
            int valeur = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT INST_LNG FROM SETTINGS WHERE INSTRUCTION = 'UPDATE_PRIX_REVIENT' ";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        if (dr["INST_LNG"] != DBNull.Value)
                            valeur = int.Parse(dr["INST_LNG"].ToString());
                    }
                }
            }
            return valeur;
        }
    }
}