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

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileGouvernorat
    {
        #region Propriétés

        public string Code { get; set; }
        public string Libelle { get; set; }

        #endregion Propriétés
    }

    [Serializable]
    public class MobileGouvernoratCollection : List<MobileGouvernorat>
    {
        public static MobileGouvernoratCollection Charger()
        {
            MobileGouvernoratCollection collection = new MobileGouvernoratCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Gouvernorat_Charger";
                    cmd.Parameters.AddWithValue("@CGouvernorat", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEquipe", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileGouvernorat gouv = new MobileGouvernorat();

                            gouv.Code = dr["CGouvernorat"].ToString().Trim();
                            if (dr["LibGouvernorat"] != DBNull.Value)
                                gouv.Libelle = dr["LibGouvernorat"].ToString();
                            collection.Add(gouv);
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

        public static MobileGouvernoratCollection Charger(string CEquipe)
        {
            MobileGouvernoratCollection collection = new MobileGouvernoratCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Gouvernorat_Charger";
                    cmd.Parameters.AddWithValue("@CGouvernorat", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CEquipe", string.IsNullOrEmpty(CEquipe) ? null : CEquipe );
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileGouvernorat gouv = new MobileGouvernorat();

                            gouv.Code = dr["CGouvernorat"].ToString().Trim();
                            if (dr["LibGouvernorat"] != DBNull.Value)
                                gouv.Libelle = dr["LibGouvernorat"].ToString();
                            collection.Add(gouv);
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
    }
}
