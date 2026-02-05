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
    public class TraceEquipeCollection : ItemCollection
    {
        public static TraceEquipeCollection Charger()
        {
            TraceEquipeCollection collection = new TraceEquipeCollection();
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
                    cmd.CommandText = "GP_TraceEquipe_Charger";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", DBNull.Value);


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TraceEquipe traceEquipe = new TraceEquipe();

                            traceEquipe.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                traceEquipe.CEquipe = dr["CEquipe"].ToString();
                            if (dr["CCircuit"] != DBNull.Value)
                                traceEquipe.CCircuit = dr["CCircuit"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                traceEquipe.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipe.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipe.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                traceEquipe.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                traceEquipe.HeureFin = dr["HeureFin"].ToString();
                            if (dr["CumulTemp"] != DBNull.Value)
                                traceEquipe.CumulTemp = dr["CumulTemp"].ToString();
                            if (dr["CumulTrajetkm"] != DBNull.Value)
                                traceEquipe.CumulTrajetkm = decimal.Parse(dr["CumulTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipe.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipe.TempsTrajet = dr["TempsTrajet"].ToString();


                            collection.Add(traceEquipe);
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

        public static TraceEquipeCollection Charger(string circuit, DateTime dateplanif)
        {
            TraceEquipeCollection collection = new TraceEquipeCollection();
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
                    cmd.CommandText = "GP_TraceEquipe_ChargerParcir";
                    cmd.Parameters.AddWithValue("@circuit", circuit);
                    cmd.Parameters.AddWithValue("@dateplanif", dateplanif);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            TraceEquipe traceEquipe = new TraceEquipe();

                            traceEquipe.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                traceEquipe.CEquipe = dr["CEquipe"].ToString();
                            if (dr["CCircuit"] != DBNull.Value)
                                traceEquipe.CCircuit = dr["CCircuit"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                traceEquipe.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipe.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipe.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                traceEquipe.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                traceEquipe.HeureFin = dr["HeureFin"].ToString();
                            if (dr["CumulTemp"] != DBNull.Value)
                                traceEquipe.CumulTemp = dr["CumulTemp"].ToString();
                            if (dr["CumulTrajetkm"] != DBNull.Value)
                                traceEquipe.CumulTrajetkm = decimal.Parse(dr["CumulTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipe.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipe.TempsTrajet = dr["TempsTrajet"].ToString();


                            collection.Add(traceEquipe);
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
    public class TraceEquipe : Item
    {
        #region Propriétés

        [XmlAttribute("CEquipe")]
        [Bindable(true)]
        public string CEquipe { get; set; }

        [XmlAttribute("CCircuit")]
        [Bindable(true)]
        public string CCircuit { get; set; }

        [XmlAttribute("DatePlanification")]
        [Bindable(true)]
        public DateTime? DatePlanification { get; set; }

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

        [XmlAttribute("HeureDebut")]
        [Bindable(true)]
        public string HeureDebut { get; set; }

        [XmlAttribute("HeureFin")]
        [Bindable(true)]
        public string HeureFin { get; set; }

        [XmlAttribute("CumulTrajetkm")]
        [Bindable(true)]
        public decimal CumulTrajetkm { get; set; }
        [XmlAttribute("CumulTemp")]
        [Bindable(true)]
        public string CumulTemp { get; set; }
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

        [XmlAttribute("TempsPause")]
        [Bindable(true)]
        public string TempsPause { get; set; }
        [XmlAttribute("DateDebut")]
        [Bindable(true)]
        public DateTime? DateDebut { get; set; }
        [XmlAttribute("DateFin")]
        [Bindable(true)]
        public DateTime? DateFin { get; set; }

        [XmlAttribute("BGps")]
        [Bindable(true)]
        public bool BGps { get; set; }

        [XmlAttribute("NFeuilleRoute")]
        [Bindable(true)]
        public string NFeuilleRoute { get; set; }

        public TraceEquipeDetailCollection traceEquipeDetails = new TraceEquipeDetailCollection();


        #endregion Propriétés

        public TraceEquipe()
        {

        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_TraceEquipe_Inserer";

                cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                cmd.Parameters.AddWithValue("@CCircuit", this.CCircuit);
                cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                cmd.Parameters.AddWithValue("@CDepart", this.CDepart);
                cmd.Parameters.AddWithValue("@NomDepart", this.NomDepart);
                cmd.Parameters.AddWithValue("@CArrive", this.CArrive);
                cmd.Parameters.AddWithValue("@NomArrive", this.NomArrive);
                cmd.Parameters.AddWithValue("@HeureDebut", this.HeureDebut);
                cmd.Parameters.AddWithValue("@HeureFin", this.HeureFin);
                cmd.Parameters.AddWithValue("@CumulTrajetkm", this.CumulTrajetkm);
                cmd.Parameters.AddWithValue("@CumulTemp", this.CumulTemp);
                cmd.Parameters.AddWithValue("@TempsTrajet", this.TempsTrajet);
                cmd.Parameters.AddWithValue("@TempsInterv", this.TempsInterv);
                cmd.Parameters.AddWithValue("@CreePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                cmd.Parameters.AddWithValue("@PCInsertion", Environment.UserName);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@TempsPause", this.TempsPause);
                cmd.Parameters.AddWithValue("@DateDebut", this.DateDebut);
                cmd.Parameters.AddWithValue("@DateFin", this.DateFin);
                cmd.Parameters.AddWithValue("@BGps", this.BGps);
                cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();

                this.SupprimerTraceEquipeDetailsAnterieurs(transaction, this.NFeuilleRoute);

                foreach (TraceEquipeDetail traceEquipeDetail in traceEquipeDetails)
                {
                    traceEquipeDetail.NFeuilleRoute = this.NFeuilleRoute;
                    traceEquipeDetail.Sauvegarder(transaction);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
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
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Modifier(string n)
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
                    cmd.CommandText = "GP_TraceEquipe_Modifier";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", this.NFeuilleRoute);
                    cmd.Parameters.AddWithValue("@CEquipe", this.CEquipe);
                    cmd.Parameters.AddWithValue("@CCircuit", this.CCircuit);
                    cmd.Parameters.AddWithValue("@DatePlanification", this.DatePlanification);
                    cmd.Parameters.AddWithValue("@CDepart", this.CDepart);
                    cmd.Parameters.AddWithValue("@CArrive", this.CArrive);
                    cmd.Parameters.AddWithValue("@HeureDebut", this.HeureDebut);
                    cmd.Parameters.AddWithValue("@HeureFin", this.HeureFin);
                    cmd.Parameters.AddWithValue("@CumulTrajetkm", this.CumulTrajetkm);
                    cmd.Parameters.AddWithValue("@CumulTemp", this.CumulTemp);
                    cmd.Parameters.AddWithValue("@TempsTrajet", this.TempsTrajet);
                    cmd.Parameters.AddWithValue("@TempsInterv", this.TempsInterv);
                    cmd.Parameters.AddWithValue("@ModifiePar", GestionSession.UtilisateurCourant.IdUtilisateur);
                    cmd.Parameters.AddWithValue("@PCModification", Environment.UserName);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateDebut", this.DateDebut);
                    cmd.Parameters.AddWithValue("@TempsPause", this.TempsPause);
                    cmd.Parameters.AddWithValue("@BGps", this.BGps);
                    cmd.Parameters.AddWithValue("@DateFin", this.DateFin);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            this.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                        }
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

        private void SupprimerTraceEquipeDetailsAnterieurs(SqlTransaction transaction, string NFeuilleRoute)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_SupprimerTraceEquipeDetails";

                cmd.Parameters.AddWithValue("@NFeuilleRoute", NFeuilleRoute);

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
                    cmd.CommandText = "GP_TraceEquipe_Supprimer";
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

        public static TraceEquipe Charger(string NFeuilleRoute)
        {
            TraceEquipe traceEquipe = new TraceEquipe(); ;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_TraceEquipe_Charger";
                    cmd.Parameters.AddWithValue("@NFeuilleRoute", NFeuilleRoute);
                    //cmd.Parameters.AddWithValue("@date", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            traceEquipe.NFeuilleRoute = dr["NFeuilleRoute"].ToString();
                            if (dr["CEquipe"] != DBNull.Value)
                                traceEquipe.CEquipe = dr["CEquipe"].ToString();
                            if (dr["CCircuit"] != DBNull.Value)
                                traceEquipe.CCircuit = dr["CCircuit"].ToString();
                            if (dr["DatePlanification"] != DBNull.Value)
                                traceEquipe.DatePlanification = DateTime.Parse(dr["DatePlanification"].ToString());
                            if (dr["CArrive"] != DBNull.Value)
                                traceEquipe.CArrive = dr["CArrive"].ToString();
                            if (dr["CDepart"] != DBNull.Value)
                                traceEquipe.CDepart = dr["CDepart"].ToString();
                            if (dr["HeureDebut"] != DBNull.Value)
                                traceEquipe.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["HeureFin"] != DBNull.Value)
                                traceEquipe.HeureFin = dr["HeureFin"].ToString();
                            if (dr["CumulTemp"] != DBNull.Value)
                                traceEquipe.CumulTemp = dr["CumulTemp"].ToString();
                            if (dr["CumulTrajetkm"] != DBNull.Value)
                                traceEquipe.CumulTrajetkm = decimal.Parse(dr["CumulTrajetkm"].ToString());
                            if (dr["TempsInterv"] != DBNull.Value)
                                traceEquipe.TempsInterv = dr["TempsInterv"].ToString();
                            if (dr["TempsTrajet"] != DBNull.Value)
                                traceEquipe.TempsTrajet = dr["TempsTrajet"].ToString();
                            if (dr["TempsPause"] != DBNull.Value)
                                traceEquipe.TempsPause = dr["TempsPause"].ToString();
                            if (dr["DateFin"] != DBNull.Value)
                                traceEquipe.DateFin = DateTime.Parse(dr["DateFin"].ToString());
                            if (dr["DateDebut"] != DBNull.Value)
                                traceEquipe.DateDebut = DateTime.Parse(dr["DateDebut"].ToString());
                            if (dr["BGps"] != DBNull.Value)
                                traceEquipe.BGps = bool.Parse(dr["BGps"].ToString());
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return traceEquipe;
        }

    }
}
