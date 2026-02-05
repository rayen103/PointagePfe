using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonPreparation
    {
        #region Propriétés

        [XmlAttribute("NBonPreparation")]
        [Bindable(true)]
        public string NBonPreparation { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("DatePreparation")]
        [Bindable(true)]
        public DateTime DatePreparation { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        public BonPreparationDetailCollection BonPreparationDetailCollection;

        #endregion Propriétés

        public BonPreparation()
        {
            this.NBonPreparation = string.Empty;

            this.BonPreparationDetailCollection = new BonPreparationDetailCollection();
        }

        public void Inserer()
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
                    cmd.CommandText = "BonPreparation_Inserer";
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@DatePreparation ", this.DatePreparation);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                        if (dr.Read())
                        {
                            NBonPreparation = dr["NBonPreparation"].ToString();
                            Indice = int.Parse(dr["DernierIndice"].ToString());
                        }
                    int i = 0;
                    foreach (BonPreparationDetail bonPreparationDetail in BonPreparationDetailCollection)
                    {
                        bonPreparationDetail.NBonPreparation = NBonPreparation;
                        bonPreparationDetail.Ordre = i++;
                        bonPreparationDetail.Sauvegarder(transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
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
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonPreparation_Modifier";
                    cmd.Parameters.AddWithValue("@NBonPreparation", this.NBonPreparation);
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@DatePreparation", this.DatePreparation);
                    cmd.Parameters.AddWithValue("@Observation", this.Observation);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;
                    cmd.ExecuteNonQuery();

                    BonPreparationDetail detail = new BonPreparationDetail();
                    detail.NBonPreparation = this.NBonPreparation;
                    detail.CEntrepot = this.CEntrepot;
                    detail.Supprimer(transaction);

                    foreach (BonPreparationDetail bonPreparationDetail in BonPreparationDetailCollection)
                    {
                        bonPreparationDetail.NBonPreparation = this.NBonPreparation;
                        bonPreparationDetail.CEntrepot = this.CEntrepot;
                        bonPreparationDetail.Sauvegarder(transaction);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Supprimer()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonPreparation_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@NBonPreparation", NBonPreparation));

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static BonPreparation Charger(string nBonPreparation, string cEntrepot)
        {
            BonPreparation bonPreparation = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonPreparation_Charger";
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@NBonPreparation", nBonPreparation);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonPreparation = new BonPreparation();
                            bonPreparation.NBonPreparation = dr["NBonPreparation"].ToString();
                            bonPreparation.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonPreparation.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                bonPreparation.Observation = dr["Observation"].ToString();
                            if (dr["DatePreparation"] != DBNull.Value)
                                bonPreparation.DatePreparation = DateTime.Parse(dr["DatePreparation"].ToString());
                        }
                    }
                }
                if (bonPreparation != null)
                    bonPreparation.BonPreparationDetailCollection = BonPreparationDetailCollection.Charger(nBonPreparation, cEntrepot);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return (bonPreparation);
        }
    }

    public class BonPreparationCollection : List<BonPreparation>
    {
        public static DataSet ChargerVue(DateTime dateDebut, DateTime dateFin)
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptBonPreparationListe_Charger";
                cmd.Parameters.AddWithValue("@DateDeb", dateDebut);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptBonPreparationListe_Charger");
            }
            return (ds);
        }
    }
}