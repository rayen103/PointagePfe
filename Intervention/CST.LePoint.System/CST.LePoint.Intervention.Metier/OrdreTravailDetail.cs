using CST.LePoint.Referentiel;
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

namespace CST.LePoint.Intervention.Metier
{
    public class OrdreTravailDetail : Item
    {

        #region Proriétès

        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("PrixHTArticle")]
        [Bindable(true)]
        public decimal PrixHTArticle { get; set; }
        [XmlAttribute("PrixHTArticleOT")]
        [Bindable(true)]
        public decimal PrixHTArticleOT { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }
        [XmlAttribute("QuantiteOTRes")]
        [Bindable(true)]
        public decimal QuantiteOTRes { get; set; }

        [XmlAttribute("QuantiteHistorique")]
        [Bindable(true)]
        public decimal QuantiteHistorique { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }

        [XmlAttribute("QuantitePreparee")]
        [Bindable(true)]
        public decimal QuantitePreparee { get; set; }

        [XmlAttribute("QuantiteOT")]
        [Bindable(true)]
        public decimal QuantiteOT { get; set; }

        [XmlAttribute("Image_Article")]
        [Bindable(true)]
        public byte[] Image_Article { get; set; }



        #endregion Proriétès

        public OrdreTravailDetail()
        {
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_OrdredeTravailDetail_Sauvegarder";
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@CUnite", this.CUnite);
                cmd.Parameters.AddWithValue("@LibArticle", this.LibArticle);
                cmd.Parameters.AddWithValue("@CTaxe", this.CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", this.TauxTVA);
                cmd.Parameters.AddWithValue("@QuantiteOT", this.QuantiteOT);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);
                cmd.Parameters.AddWithValue("@PrixHTArticleOT", this.PrixHTArticleOT);
                cmd.Parameters.AddWithValue("@PrixHTArticle", this.PrixHTArticle);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);
                cmd.Parameters.AddWithValue("@QuantiteOTRes", this.QuantiteOTRes);
                cmd.Parameters.AddWithValue("@Image_Article", this.Image_Article);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                //StockHelper.MiseAJourStockReserver(this.CArticle, this.CEntrepot, this.Quantite, 1, transaction);
            }
            catch (Exception ex)
            {
                // transaction.Rollback();
                throw ex;
            }
        }
        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
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
                    cmd.CommandText = "GP_OrdreTravailDetail_Supprimer";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@CArticle", this.CArticle);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
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

                cmd.CommandText = "BonCommande_AjusterQuantiteHistorique";
                cmd.Parameters.AddWithValue("@CArticle", this.CArticle);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                cmd.Parameters.AddWithValue("@QuantiteHistorique", this.QuantiteHistorique);
                cmd.Parameters.AddWithValue("@QuantitePreparee", this.QuantitePreparee);

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

        public static OrdreTravailDetail Charger(string NOrdredeTravail, string cArticle)
        {
            OrdreTravailDetail ordreTravailDetail = null;
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
                    cmd.CommandText = "GP_OrdreTravailDetail_Charger";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ordreTravailDetail = new OrdreTravailDetail();
                            ordreTravailDetail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            ordreTravailDetail.CArticle = dr["CArticle"].ToString();
                            ordreTravailDetail.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["CUnite"] != DBNull.Value)
                                ordreTravailDetail.CUnite = dr["CUnite"].ToString();
                            if (dr["CTaxe"] != DBNull.Value)
                                ordreTravailDetail.CTaxe = dr["CTaxe"].ToString();


                            if (dr["LibArticle"] != DBNull.Value)
                                ordreTravailDetail.LibArticle = dr["LibArticle"].ToString();



                            if (dr["PrixHTArticle"] != DBNull.Value)
                                ordreTravailDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                            if (dr["Quantite"] != DBNull.Value)
                                ordreTravailDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteOTRes"] != DBNull.Value)
                                ordreTravailDetail.QuantiteOTRes = decimal.Parse(dr["QuantiteOTRes"].ToString());
                            if (dr["QuantiteHistorique"] != DBNull.Value)
                                ordreTravailDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());



                            if (dr["QuantitePreparee"] != DBNull.Value)
                                ordreTravailDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                            if (dr["QuantiteOT"] != DBNull.Value)
                                ordreTravailDetail.QuantiteOT = decimal.Parse(dr["QuantiteOT"].ToString());

                            if (dr["TauxTVA"] != DBNull.Value)
                                ordreTravailDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());

                            if (dr["Image_Article"] != DBNull.Value)
                                ordreTravailDetail.Image_Article = (byte[])dr["Image_Article"];
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (ordreTravailDetail);
            }
        }

        public static void ModifierQuantitePrepare(decimal p1, string p2, String p3)
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
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update GP_OrdredeTravailDetail set QuantitePreparee = '" + p1 + "'   where NOrdredeTravail = '" + p2 + "' and CArticle = '" + p3 + "' ";
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

        public static void ModifierQuantiteOTRes(decimal p1, string p2, string p3)
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
                    cmd.CommandType = CommandType.Text;
                    // decimal resultat = qté - fait;
                    cmd.CommandText = "update GP_OrdredeTravailDetail set QuantiteOTRes = '" + p1 + "'   where NOrdredeTravail = '" + p2 + "' and CArticle = '" + p3 + "' ";
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


    }

    public class OrdreTravailDetailCollection : List<OrdreTravailDetail>
        {
            public OrdreTravailDetailCollection()
            {
            }

            public static OrdreTravailDetailCollection Charger(string NOrdredeTravail)
            {
                OrdreTravailDetailCollection collection = new OrdreTravailDetailCollection();

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
                        cmd.CommandText = "BonCommandeDetail_Charger";
                        cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                        cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                OrdreTravailDetail ordreTravailDetail = new OrdreTravailDetail();
                                ordreTravailDetail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                                ordreTravailDetail.CArticle = dr["CArticle"].ToString();
                                ordreTravailDetail.CEntrepot = dr["CEntrepot"].ToString();

                                if (dr["CUnite"] != DBNull.Value)
                                    ordreTravailDetail.CUnite = dr["CUnite"].ToString();
                                if (dr["CTaxe"] != DBNull.Value)
                                    ordreTravailDetail.CTaxe = dr["CTaxe"].ToString();


                                if (dr["LibArticle"] != DBNull.Value)
                                    ordreTravailDetail.LibArticle = dr["LibArticle"].ToString();



                                if (dr["PrixHTArticle"] != DBNull.Value)
                                    ordreTravailDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                                if (dr["Quantite"] != DBNull.Value)
                                    ordreTravailDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                                if (dr["QuantiteOTRes"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteOTRes = decimal.Parse(dr["QuantiteOTRes"].ToString());
                                if (dr["QuantiteHistorique"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());



                                if (dr["QuantitePreparee"] != DBNull.Value)
                                    ordreTravailDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                                if (dr["QuantiteOT"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteOT = decimal.Parse(dr["QuantiteOT"].ToString());

                                if (dr["TauxTVA"] != DBNull.Value)
                                    ordreTravailDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());


                                if (dr["Image_Article"] != DBNull.Value)
                                    ordreTravailDetail.Image_Article = (byte[])dr["Image_Article"];
                                collection.Add(ordreTravailDetail);
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

            //public static DataSet ChargerVue(string nBonCommande)
            //{
            //    DataSet ds = new DataSet();

            //    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            //    {
            //        cn.Open();
            //        SqlCommand cmd = new SqlCommand();
            //        cmd.Connection = cn;
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.CommandText = "BonCommandeDetailRpt_Charger";
            //        cmd.Parameters.AddWithValue("@NBonCommande", nBonCommande);

            //        foreach (SqlParameter parametre in cmd.Parameters)
            //        {
            //            if (parametre.Value == null)
            //            {
            //                parametre.Value = DBNull.Value;
            //            }
            //        }
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                BonCommandeDetail bonCommandeDetail = new BonCommandeDetail();

            //                bonCommandeDetail.CArticle = dr["CArticle"].ToString();
            //                bonCommandeDetail.NBonCommande = dr["NBonCommande"].ToString();
            //                bonCommandeDetail.Ordre = int.Parse(dr["Ordre"].ToString());
            //            }
            //        }

            //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //        sda.Fill(ds, "BonCommandeRptDataSet");
            //    }
            //    return (ds);
            //}

            //public BonCommandeDetail RecupererBonCommandeDetail(string nBonCommande, string cArticle, int ordre)
            //{
            //    BonCommandeDetail bonCommandeDetail = null;
            //    bonCommandeDetail = this.Where(p => p.NBonCommande.Equals(nBonCommande) && p.CArticle.Equals(cArticle) && p.Ordre == ordre).FirstOrDefault();
            //    return bonCommandeDetail;
            //}

            //public BonCommandeDetail RecupererBonCommandeDetail(string cArticle)
            //{
            //    BonCommandeDetail bonCommandeDetail = null;
            //    bonCommandeDetail = this.Where(p => p.CArticle.Equals(cArticle)).FirstOrDefault();
            //    return bonCommandeDetail;
            //}


            public static OrdreTravailDetailCollection ChargerparOT(string NOrdredeTravail)
            {
                OrdreTravailDetailCollection collection = new OrdreTravailDetailCollection();

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
                        cmd.CommandText = "GP_OrdreTravailDetail_ChargerparOT";
                        cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                        // cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                        // cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                OrdreTravailDetail ordreTravailDetail = new OrdreTravailDetail();
                                ordreTravailDetail.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                                ordreTravailDetail.CArticle = dr["CArticle"].ToString();
                                ordreTravailDetail.CEntrepot = dr["CEntrepot"].ToString();

                                if (dr["CUnite"] != DBNull.Value)
                                    ordreTravailDetail.CUnite = dr["CUnite"].ToString();
                                if (dr["CTaxe"] != DBNull.Value)
                                    ordreTravailDetail.CTaxe = dr["CTaxe"].ToString();
                                if (dr["LibArticle"] != DBNull.Value)
                                    ordreTravailDetail.LibArticle = dr["LibArticle"].ToString();
                                if (dr["PrixHTArticle"] != DBNull.Value)
                                    ordreTravailDetail.PrixHTArticle = decimal.Parse(dr["PrixHTArticle"].ToString());
                                if (dr["PrixHTArticleOT"] != DBNull.Value)
                                    ordreTravailDetail.PrixHTArticleOT = decimal.Parse(dr["PrixHTArticleOT"].ToString());
                                if (dr["Quantite"] != DBNull.Value)
                                    ordreTravailDetail.Quantite = decimal.Parse(dr["Quantite"].ToString());
                                if (dr["QuantiteOTRes"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteOTRes = decimal.Parse(dr["QuantiteOTRes"].ToString());
                                if (dr["QuantiteHistorique"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteHistorique = decimal.Parse(dr["QuantiteHistorique"].ToString());
                                if (dr["QuantitePreparee"] != DBNull.Value)
                                    ordreTravailDetail.QuantitePreparee = decimal.Parse(dr["QuantitePreparee"].ToString());
                                if (dr["QuantiteOT"] != DBNull.Value)
                                    ordreTravailDetail.QuantiteOT = decimal.Parse(dr["QuantiteOT"].ToString());
                                if (dr["TauxTVA"] != DBNull.Value)
                                    ordreTravailDetail.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                                if (dr["Image_Article"] != DBNull.Value)
                                    ordreTravailDetail.Image_Article = (byte[])dr["Image_Article"];

                                collection.Add(ordreTravailDetail);
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

