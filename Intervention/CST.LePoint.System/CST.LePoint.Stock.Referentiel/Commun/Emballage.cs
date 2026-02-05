using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class EmballageCollection : ItemCollection
    {
        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Emballage_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CEmballage", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "Emballage_Rpt_Charger");
            }
            return (ds);
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Emballage_Charger";
                cmd.Parameters.AddWithValue("@CEmballage", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static EmballageCollection Charger()
        {
            EmballageCollection emballageCollecion = new EmballageCollection();
            Emballage emballage = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Emballage_Charger";
                    cmd.Parameters.AddWithValue("@CEmballage", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            emballage = new Emballage();
                            emballage.Code = dr["CEmballage"].ToString();
                            if (dr["LibEmballage"] != DBNull.Value)
                                emballage.Libelle = dr["LibEmballage"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                emballage.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            emballageCollecion.Add(emballage);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return emballageCollecion;
        }
    }

    [Serializable]
    public class Emballage : Item
    {
        #region Propriétés

        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

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

        #endregion Propriétés

        public Emballage()
        {
        }

        public void Sauvegarder()
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

                    cmd.CommandText = "Ref_Emballage_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CEmballage", this.Code);
                    cmd.Parameters.AddWithValue("@LibEmballage", this.Libelle);
                    cmd.Parameters.AddWithValue("@Quantite", Quantite);
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

                    cmd.CommandText = "Ref_Emballage_Supprimer";
                    cmd.Parameters.AddWithValue("@CEmballage", this.Code);
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

        public static Emballage Charger(string cEmballage)
        {
            Emballage emballage = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Emballage_Charger";
                    cmd.Parameters.AddWithValue("@CEmballage", cEmballage);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            emballage = new Emballage();
                            emballage.Code = dr["CEmballage"].ToString();
                            if (dr["LibEmballage"] != DBNull.Value)
                                emballage.Libelle = dr["LibEmballage"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                emballage.Quantite = decimal.Parse(dr["Quantite"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return emballage;
        }
    }
}