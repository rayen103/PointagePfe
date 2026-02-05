using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
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


    [Serializable]
    public class TraceEquipeDetailCollection : ItemCollection
    {
        public static TraceEquipeDetailCollection Charger()
        {
            TraceEquipeDetailCollection collection = new TraceEquipeDetailCollection();
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
                    cmd.CommandText = "GP_TraceEquipeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", DBNull.Value);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TraceEquipeDetail traceEquipeDetail = new TraceEquipeDetail();

                            traceEquipeDetail.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipeDetail.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipeDetail.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDepart"] != DBNull.Value)
                                traceEquipeDetail.HeureDepart = dr["HeureDepart"].ToString();
                            if (dr["HeureArrive"] != DBNull.Value)
                                traceEquipeDetail.HeureArrive = dr["HeureArrive"].ToString();
                            if (dr["NConvention"] != DBNull.Value)
                                traceEquipeDetail.NConvention = dr["NConvention"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                traceEquipeDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["DTrajetkm"] != DBNull.Value)
                                traceEquipeDetail.DTrajetkm = decimal.Parse(dr["DTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipeDetail.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipeDetail.TempsTrajet = dr["TempsTrajet"].ToString();


                            collection.Add(traceEquipeDetail);
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

        public static TraceEquipeDetailCollection Charger(string NFeuilleRoute)
        {
            TraceEquipeDetailCollection collection = new TraceEquipeDetailCollection();
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
                    cmd.CommandText = "GP_TraceEquipeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", NFeuilleRoute);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TraceEquipeDetail traceEquipeDetail = new TraceEquipeDetail();

                            //traceEquipeDetail.Id = int.Parse(dr["Id"].ToString());

                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipeDetail.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipeDetail.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDepart"] != DBNull.Value)
                                traceEquipeDetail.HeureDepart = dr["HeureDepart"].ToString();
                            if (dr["HeureArrive"] != DBNull.Value)
                                traceEquipeDetail.HeureArrive = dr["HeureArrive"].ToString();
                            if (dr["NConvention"] != DBNull.Value)
                                traceEquipeDetail.NConvention = dr["NConvention"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                traceEquipeDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["DTrajetkm"] != DBNull.Value)
                                traceEquipeDetail.DTrajetkm = decimal.Parse(dr["DTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipeDetail.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipeDetail.TempsTrajet = dr["TempsTrajet"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                traceEquipeDetail.Date = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["RaisSoc"] != DBNull.Value)
                                traceEquipeDetail.RaisSoc = dr["RaisSoc"].ToString();
                            collection.Add(traceEquipeDetail);
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

    [Serializable]
    public class TraceEquipeDetail : Item
    {
        #region Propriétés

        [XmlAttribute("CDepart")]
        [Bindable(true)]
        public string CDepart { get; set; }

        [XmlAttribute("NomDepart")]
        [Bindable(true)]
        public string NomDepart { get; set; }

        [XmlAttribute("CArrive")]
        [Bindable(true)]
        public string CArrive { get; set; }

        [XmlAttribute("NomArrive")]
        [Bindable(true)]
        public string NomArrive { get; set; }

        [XmlAttribute("HeureDepart")]
        [Bindable(true)]
        public string HeureDepart { get; set; }

        [XmlAttribute("HeureArrive")]
        [Bindable(true)]
        public string HeureArrive { get; set; }
        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("NConvention")]
        [Bindable(true)]
        public string NConvention { get; set; }
        [XmlAttribute("DTrajetkm")]
        [Bindable(true)]
        public decimal DTrajetkm { get; set; }

        [XmlAttribute("TempsTrajet")]
        [Bindable(true)]
        public string TempsTrajet { get; set; }
        [XmlAttribute("TempsInterv")]
        [Bindable(true)]
        public string TempsInterv { get; set; }

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

        [XmlAttribute("DateDepart")]
        [Bindable(true)]
        public DateTime? DateDepart { get; set; }

        [XmlAttribute("DateArrive")]
        [Bindable(true)]
        public DateTime? DateArrive { get; set; }

        [XmlAttribute("TempsPause")]
        [Bindable(true)]
        public string TempsPause { get; set; }

        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }
        [XmlAttribute("Date")]
        [Bindable(true)]
        public DateTime Date { get; set; }


        //

        [XmlAttribute("RaisSoc")]
        [Bindable(true)]
        public string RaisSoc { get; set; }

        [XmlAttribute("NFeuilleRoute")]
        [Bindable(true)]
        public string NFeuilleRoute { get; set; }

        public TraceEquipeDetailCollection traceEquipeDetails = new TraceEquipeDetailCollection();


        #endregion Propriétés

        public TraceEquipeDetail()
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
                cmd.CommandText = "GP_TraceEquipeDetail_Sauvegarder";

                cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);
                cmd.Parameters.AddWithValue("@NConvention", this.NConvention);
                cmd.Parameters.AddWithValue("@CDepart", this.CDepart);
                cmd.Parameters.AddWithValue("@NomDepart", this.NomDepart);
                cmd.Parameters.AddWithValue("@CArrive", this.CArrive);
                cmd.Parameters.AddWithValue("@NomArrive", this.NomArrive);
                cmd.Parameters.AddWithValue("@HeureDepart", this.HeureDepart);
                cmd.Parameters.AddWithValue("@HeureArrive", this.HeureArrive);
                cmd.Parameters.AddWithValue("@DTrajetkm", this.DTrajetkm);
                cmd.Parameters.AddWithValue("@Ordre", this.Ordre);
                cmd.Parameters.AddWithValue("@TempsTrajet", this.TempsTrajet);
                cmd.Parameters.AddWithValue("@TempsInterv", this.TempsInterv);
                cmd.Parameters.AddWithValue("@CreePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                cmd.Parameters.AddWithValue("@ModifiePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                cmd.Parameters.AddWithValue("@PCInsertion", Environment.UserName);
                cmd.Parameters.AddWithValue("@PCModification", Environment.UserName);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@Date", this.Date);
                cmd.Parameters.AddWithValue("@TempsPause", this.TempsPause);
                cmd.Parameters.AddWithValue("@DateDepart", this.DateDepart);
                cmd.Parameters.AddWithValue("@DateArrive", this.DateArrive);
                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@RaisSoc", this.RaisSoc);

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
                try
                {

                    Sauvegarder(transaction);
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
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
                    cmd.CommandText = "GP_TraceEquipeDetail_Supprimer";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);

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

        public static TraceEquipeDetail Charger(string NFeuilleRoute)
        {
            TraceEquipeDetail traceEquipeDetail = new TraceEquipeDetail(); ;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_TraceEquipeDetail_Charger";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", NFeuilleRoute);
                    //cmd.Parameters.AddWithValue("@date", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {

                            traceEquipeDetail.NFeuilleRoute = dr["NFeuilleRoute"].ToString();

                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipeDetail.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipeDetail.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDepart"] != DBNull.Value)
                                traceEquipeDetail.HeureDepart = dr["HeureDepart"].ToString();
                            if (dr["HeureArrive"] != DBNull.Value)
                                traceEquipeDetail.HeureArrive = dr["HeureArrive"].ToString();
                            if (dr["NConvention"] != DBNull.Value)
                                traceEquipeDetail.NConvention = dr["NConvention"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                traceEquipeDetail.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["DTrajetkm"] != DBNull.Value)
                                traceEquipeDetail.DTrajetkm = decimal.Parse(dr["DTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipeDetail.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipeDetail.TempsTrajet = dr["TempsTrajet"].ToString();
                            if (dr["DateArrive"] != DBNull.Value)
                                traceEquipeDetail.DateArrive = DateTime.Parse(dr["DateArrive"].ToString());
                            if (dr["DateDepart"] != DBNull.Value)
                                traceEquipeDetail.DateDepart = DateTime.Parse(dr["DateDepart"].ToString());
                            if (dr["TempsPause"] != DBNull.Value)
                                traceEquipeDetail.TempsPause = dr["TempsPause"].ToString();
                            if (dr["Date"] != DBNull.Value)
                                traceEquipeDetail.Date = DateTime.Parse(dr["Date"].ToString());
                            if (dr["CEquipe"] != DBNull.Value)
                                traceEquipeDetail.CEquipe = dr["CEquipe"].ToString();
                            if (dr["RaisSoc"] != DBNull.Value)
                                traceEquipeDetail.RaisSoc = dr["RaisSoc"].ToString();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return traceEquipeDetail;
        }



    }
}


