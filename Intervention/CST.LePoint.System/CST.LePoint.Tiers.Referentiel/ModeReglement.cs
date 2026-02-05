using CST.LePoint.Referentiel;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

//using CST.Framework;

namespace CST.LePoint.Tiers.Referentiel
{
    [Serializable]
    public class ModeReglementCollection : ItemCollection
    {
        public ModeReglementCollection()
        {
        }

        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ModeReglement_Rpt_Charger";
                cmd.Parameters.AddWithValue("@CModeReglement", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "ModeReglement_Rpt_Charger");
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
                cmd.CommandText = "Ref_ModeReglement_Charger";
                cmd.Parameters.AddWithValue("@CModeReglement", DBNull.Value);

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

        public static ModeReglementCollection Charger()
        {
            ModeReglementCollection collection = new ModeReglementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModeReglement modeReglement = new ModeReglement();
                            modeReglement.Code = dr["CModeReglement"].ToString().Trim();
                            if (dr["LibModeReglement"] != DBNull.Value)
                                modeReglement.Libelle = dr["LibModeReglement"].ToString().Trim();
                            if (dr["BEcheance"] != DBNull.Value)
                                modeReglement.BEcheance = bool.Parse(dr["BEcheance"].ToString().Trim());
                            collection.Add(modeReglement);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return collection;
        }

        public static ModeReglementCollection Charger_SansAvrSansNR()
        {
            ModeReglementCollection collection = new ModeReglementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Charger_SansAvrSansNR";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModeReglement modeReglement = new ModeReglement();
                            modeReglement.Code = dr["CModeReglement"].ToString().Trim();
                            if (dr["LibModeReglement"] != DBNull.Value)
                                modeReglement.Libelle = dr["LibModeReglement"].ToString().Trim();
                            if (dr["BEcheance"] != DBNull.Value)
                                modeReglement.BEcheance = bool.Parse(dr["BEcheance"].ToString().Trim());
                            collection.Add(modeReglement);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }
        public static ModeReglementCollection Charger_SansAvr()
        {
            ModeReglementCollection collection = new ModeReglementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Charger_SansAvr";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModeReglement modeReglement = new ModeReglement();
                            modeReglement.Code = dr["CModeReglement"].ToString().Trim();
                            if (dr["LibModeReglement"] != DBNull.Value)
                                modeReglement.Libelle = dr["LibModeReglement"].ToString().Trim();
                            if (dr["BEcheance"] != DBNull.Value)
                                modeReglement.BEcheance = bool.Parse(dr["BEcheance"].ToString().Trim());
                            collection.Add(modeReglement);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return collection;
        }

        public static ModeReglementCollection Charger_ModeRemboursement()
        {
            ModeReglementCollection collection = new ModeReglementCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Charger_ModeRemboursement";
                    

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ModeReglement modeReglement = new ModeReglement();
                            modeReglement.Code = dr["CModeReglement"].ToString().Trim();
                            if (dr["LibModeReglement"] != DBNull.Value)
                                modeReglement.Libelle = dr["LibModeReglement"].ToString().Trim();
                         
                            collection.Add(modeReglement);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return collection;
        }

        //public ModeReglement Obtenir(string cModeReglement)
        //{
        //    ModeReglement modeReglement = this.Where(x => x.Code.Equals(cModeReglement)).FirstOrDefault();
        //    return modeReglement;
        //}
    }

    [Serializable]
    public class ModeReglement : Item, IDisposable
    {
        #region Propriétés

        [XmlAttribute("BEcheance")]
        [Bindable(true)]
        public bool BEcheance { get; set; }

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

        [XmlAttribute("BMobile")]
        [Bindable(true)]
        public bool BMobile { get; set; }
        #endregion Propriétés

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        ~ModeReglement()
        {
            Dispose(false);
        }

        public ModeReglement()
        {
            BEcheance = false;
        }

        public static ModeReglement Charger(string cModeReglement)
        {
            ModeReglement modeReglement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", cModeReglement));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            modeReglement = new ModeReglement();
                            modeReglement.Code = dr["CModeReglement"].ToString().Trim();
                            if (dr["LibModeReglement"] != DBNull.Value)
                                modeReglement.Libelle = dr["LibModeReglement"].ToString().Trim();
                            if (dr["BEcheance"] != DBNull.Value)
                                modeReglement.BEcheance = bool.Parse(dr["BEcheance"].ToString().Trim());
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return modeReglement;
        }

        public void Sauvegarder()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_ModeReglement_Sauvegarder";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", Code));
                    cmd.Parameters.Add(new SqlParameter("@LibModeReglement", Libelle));
                    cmd.Parameters.Add(new SqlParameter("@BEcheance", BEcheance));
                    cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                    cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                    cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                    cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));
                    cmd.Parameters.Add(new SqlParameter("@BMobile", this.BMobile));

                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                    {
                        if (sqlParametre.Value == null)
                        {
                            sqlParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception )
            {
                throw;
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
                    cmd.CommandText = "Ref_ModeReglement_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CModeReglement", Code));
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                    {
                        if (sqlParametre.Value == null)
                        {
                            sqlParametre.Value = DBNull.Value;
                        }
                    }
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}