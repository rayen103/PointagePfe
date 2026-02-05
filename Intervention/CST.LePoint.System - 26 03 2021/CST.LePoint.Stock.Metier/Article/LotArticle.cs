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

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class LotArticle
    {
        #region Proprietés

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("DateReception")]
        [Bindable(true)]
        public DateTime? DateReception { get; set; }

        [XmlAttribute("DateFabrication")]
        [Bindable(true)]
        public DateTime? DateFabrication { get; set; }

        [XmlAttribute("DatePeremption")]
        [Bindable(true)]
        public DateTime? DatePeremption { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("CStatut")]
        [Bindable(true)]
        public string CStatut { get; set; }

        [XmlAttribute("NBonCommandeFournisseur")]
        [Bindable(true)]
        public string NBonCommandeFournisseur { get; set; }

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

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

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        public ArticleLotEntrepotCollection ArticleLotEntrepots;

        #endregion Proprietés

        public LotArticle()
        {
            this.ArticleLotEntrepots = new ArticleLotEntrepotCollection();
        }

        public LotArticle(string cArticle, string cLot)
        {
            this.CArticle = cArticle;
            this.CLot = cLot;
            this.ArticleLotEntrepots = new ArticleLotEntrepotCollection();
        }

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "LotArticle_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
                cmd.Parameters.AddWithValue("@DateReception", DateReception);
                cmd.Parameters.AddWithValue("@DateFabrication", DateFabrication);
                cmd.Parameters.AddWithValue("@DatePeremption", DatePeremption);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                cmd.Parameters.AddWithValue("@CStatut", CStatut);
                cmd.Parameters.AddWithValue("@NBonCommandeFournisseur", NBonCommandeFournisseur);
                cmd.Parameters.AddWithValue("@Quantite", Quantite);
                cmd.Parameters.AddWithValue("@Observation", Observation);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();


                this.SupprimerArticleLotEntrepotAnterieurs(transaction);

                foreach (ArticleLotEntrepot articleLotEntrepot in ArticleLotEntrepots)
                {
                    articleLotEntrepot.CreePar = this.CreePar;
                    articleLotEntrepot.ModifiePar = this.ModifiePar;
                    articleLotEntrepot.PCInsertion = this.PCInsertion;
                    articleLotEntrepot.PCModification = this.PCModification;
                    articleLotEntrepot.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerArticleLotEntrepotAnterieurs(SqlTransaction transaction)
        {
            ArticleLotEntrepotCollection collection = ArticleLotEntrepotCollection.Charger(this.CArticle, this.CLot);
            foreach (ArticleLotEntrepot item in collection)
            {
                if (!this.ArticleLotEntrepots.Exists(p => p.CArticle == item.CArticle && p.CLot == item.CLot && p.CEntrepot == item.CEntrepot))
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
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "LotArticle_Supprimer";
                    cmd.Parameters.AddWithValue("@CLot", CLot);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
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

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.CommandText = "LotArticle_Supprimer";
                cmd.Parameters.AddWithValue("@CLot", CLot);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static LotArticle Charger(string cArticle, string cLot)
        {
            LotArticle lotArticle = null;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "LotArticle_Charger";
                cmd.Parameters.AddWithValue("@CArticle", cArticle);
                cmd.Parameters.AddWithValue("@CLot", cLot);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lotArticle = new LotArticle();
                    lotArticle.CArticle = dr["CArticle"].ToString();
                    lotArticle.CLot = dr["CLot"].ToString();
                    if (dr["LibArticle"] != DBNull.Value)
                        lotArticle.LibArticle = dr["LibArticle"].ToString();
                    if (dr["DateReception"] != DBNull.Value)
                        lotArticle.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                    if (dr["DateFabrication"] != DBNull.Value)
                        lotArticle.DateFabrication = DateTime.Parse(dr["DateFabrication"].ToString());
                    if (dr["DatePeremption"] != DBNull.Value)
                        lotArticle.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                    if (dr["CFournisseur"] != DBNull.Value)
                        lotArticle.CFournisseur = dr["CFournisseur"].ToString();
                    if (dr["RaisonSociale"] != DBNull.Value)
                        lotArticle.RaisonSociale = dr["RaisonSociale"].ToString();
                    if (dr["CStatut"] != DBNull.Value)
                        lotArticle.CStatut = dr["CStatut"].ToString();
                    if (dr["NBonCommandeFournisseur"] != DBNull.Value)
                        lotArticle.NBonCommandeFournisseur = dr["NBonCommandeFournisseur"].ToString();
                    if (dr["Quantite"] != DBNull.Value)
                        lotArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                    if (dr["Observation"] != DBNull.Value)
                        lotArticle.Observation = dr["Observation"].ToString();
                    if (dr["CreePar"] != DBNull.Value)
                        lotArticle.CreePar = int.Parse(dr["CreePar"].ToString());
                    if (dr["ModifiePar"] != DBNull.Value)
                        lotArticle.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                    if (dr["DateInsertion"] != DBNull.Value)
                        lotArticle.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                    if (dr["DateModification"] != DBNull.Value)
                        lotArticle.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                    if (dr["PCInsertion"] != DBNull.Value)
                        lotArticle.PCInsertion = dr["PCInsertion"].ToString();
                    if (dr["PCModification"] != DBNull.Value)
                        lotArticle.PCModification = dr["PCModification"].ToString();

                    lotArticle.ArticleLotEntrepots = ArticleLotEntrepotCollection.Charger(cArticle, cLot);
                }
            }

            return (lotArticle);
        }

    }

    public class LotArticleCollection : List<LotArticle>
    {
        public static LotArticleCollection Charger()
        {
            LotArticleCollection lotArticleCollection = new LotArticleCollection();
            LotArticle lotArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "LotArticle_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lotArticle = new LotArticle();
                            lotArticle.CArticle = dr["CArticle"].ToString();
                            lotArticle.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                lotArticle.LibArticle = dr["LibArticle"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                lotArticle.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["DateFabrication"] != DBNull.Value)
                                lotArticle.DateFabrication = DateTime.Parse(dr["DateFabrication"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                lotArticle.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                lotArticle.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                lotArticle.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["CStatut"] != DBNull.Value)
                                lotArticle.CStatut = dr["CStatut"].ToString();
                            if (dr["NBonCommandeFournisseur"] != DBNull.Value)
                                lotArticle.NBonCommandeFournisseur = dr["NBonCommandeFournisseur"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                lotArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                lotArticle.Observation = dr["Observation"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                lotArticle.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                lotArticle.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                lotArticle.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                lotArticle.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                lotArticle.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                lotArticle.PCModification = dr["PCModification"].ToString();

                            lotArticle.ArticleLotEntrepots = ArticleLotEntrepotCollection.Charger(lotArticle.CArticle, lotArticle.CLot);
                            lotArticleCollection.Add(lotArticle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lotArticleCollection;
        }

        public static LotArticleCollection Charger(string cArticle)
        {
            LotArticleCollection lotArticleCollection = new LotArticleCollection();
            LotArticle lotArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "LotArticle_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lotArticle = new LotArticle();
                            lotArticle.CArticle = dr["CArticle"].ToString();
                            lotArticle.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                lotArticle.LibArticle = dr["LibArticle"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                lotArticle.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["DateFabrication"] != DBNull.Value)
                                lotArticle.DateFabrication = DateTime.Parse(dr["DateFabrication"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                lotArticle.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                lotArticle.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                lotArticle.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["CStatut"] != DBNull.Value)
                                lotArticle.CStatut = dr["CStatut"].ToString();
                            if (dr["NBonCommandeFournisseur"] != DBNull.Value)
                                lotArticle.NBonCommandeFournisseur = dr["NBonCommandeFournisseur"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                lotArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                lotArticle.Observation = dr["Observation"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                lotArticle.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                lotArticle.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                lotArticle.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                lotArticle.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                lotArticle.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                lotArticle.PCModification = dr["PCModification"].ToString();

                            lotArticle.ArticleLotEntrepots = ArticleLotEntrepotCollection.Charger(lotArticle.CArticle, lotArticle.CLot);
                            lotArticleCollection.Add(lotArticle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lotArticleCollection;
        }

        public static LotArticleCollection ChargerAvecStatut(string cArticle, string cEntrepot)
        {
            LotArticleCollection lotArticleCollection = new LotArticleCollection();
            LotArticle lotArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "LotArticle_ChargerAvecStatut";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lotArticle = new LotArticle();
                            lotArticle.CArticle = dr["CArticle"].ToString();
                            lotArticle.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                lotArticle.LibArticle = dr["LibArticle"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                lotArticle.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["DateFabrication"] != DBNull.Value)
                                lotArticle.DateFabrication = DateTime.Parse(dr["DateFabrication"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                lotArticle.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                lotArticle.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                lotArticle.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["LibStatut"] != DBNull.Value)
                                lotArticle.CStatut = dr["LibStatut"].ToString();
                            if (dr["NBonCommandeFournisseur"] != DBNull.Value)
                                lotArticle.NBonCommandeFournisseur = dr["NBonCommandeFournisseur"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                lotArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                lotArticle.Observation = dr["Observation"].ToString();

                            lotArticleCollection.Add(lotArticle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lotArticleCollection;
        }

        public static LotArticleCollection ChargerAvecStkReel(string cArticle, string cEntrepot)
        {
            LotArticleCollection lotArticleCollection = new LotArticleCollection();
            LotArticle lotArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "LotArticle_ChargerAvecStkReel";
                    cmd.Parameters.AddWithValue("@CArticle", cArticle);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lotArticle = new LotArticle();
                            lotArticle.CArticle = dr["CArticle"].ToString();
                            lotArticle.CLot = dr["CLot"].ToString();
                            if (dr["LibArticle"] != DBNull.Value)
                                lotArticle.LibArticle = dr["LibArticle"].ToString();
                            if (dr["DateReception"] != DBNull.Value)
                                lotArticle.DateReception = DateTime.Parse(dr["DateReception"].ToString());
                            if (dr["DateFabrication"] != DBNull.Value)
                                lotArticle.DateFabrication = DateTime.Parse(dr["DateFabrication"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                lotArticle.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["CFournisseur"] != DBNull.Value)
                                lotArticle.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                lotArticle.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["LibStatut"] != DBNull.Value)
                                lotArticle.CStatut = dr["LibStatut"].ToString();
                            if (dr["NBonCommandeFournisseur"] != DBNull.Value)
                                lotArticle.NBonCommandeFournisseur = dr["NBonCommandeFournisseur"].ToString();
                            if (dr["StockReel"] != DBNull.Value)
                                lotArticle.Quantite = decimal.Parse(dr["StockReel"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                lotArticle.Observation = dr["Observation"].ToString();

                            lotArticleCollection.Add(lotArticle);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lotArticleCollection;
        }

    }
}
