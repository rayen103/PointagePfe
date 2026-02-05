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
    public class RegionCollection : ItemCollection
    {
        public RegionCollection()
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
                cmd.CommandText = "RptRegion_Charger";
                cmd.Parameters.AddWithValue("@CRegion", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptRegion_Charger");
            }
            return (ds);
        }

        public static DataTable RemplirGrid()
        {
            DataTable dt = new DataTable();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_Region_Charger";
                cmd.Parameters.AddWithValue("@CRegion", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)                
                    if (parametre.Value == null)                    
                        parametre.Value = DBNull.Value;                    
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
            }
            return (dt);
        }

        public static RegionCollection Charger()
        {
            RegionCollection collection = new RegionCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Region_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CRegion", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Region region = new Region();

                            region.Code = dr["CRegion"].ToString().Trim();
                            if (dr["LibRegion"] != DBNull.Value)
                                region.Libelle = dr["LibRegion"].ToString().Trim();
                            collection.Add(region);
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
    }

    [Serializable]
    public class Region : Item
    {
        #region Propriétés

        public bool BActive { get; set; }
        public DateTime DateInsertion { get; set; }
        public DateTime DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }

        #endregion Propriétés

        public Region()
        {
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
                    cmd.CommandText = "Ref_Region_Sauvegarder";
                    cmd.Parameters.Add(new SqlParameter("@CRegion", Code));
                    cmd.Parameters.Add(new SqlParameter("@LibRegion", Libelle));
                    cmd.Parameters.Add(new SqlParameter("@BActive", BActive));
                    cmd.Parameters.Add(new SqlParameter("@DateInsertion", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@DateModification", DateTime.Now));
                    cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
                    cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
                    cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
                    cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));

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
                    cmd.CommandText = "Ref_Region_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CRegion", Code));
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

        public static Region Charger(string cRegion)
        {
            Region region = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Region_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CRegion", cRegion));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            region = new Region();
                            region.Code = dr["CRegion"].ToString().Trim();
                            if (dr["LibRegion"] != DBNull.Value)
                                region.Libelle = dr["LibRegion"].ToString().Trim();
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }

            return region;
        }
    }
}