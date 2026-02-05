using CST.LePoint.Referentiel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CST.LePoint.Stock.Metier
{
    public class TypeMouvement : Item
    {
        public string NatureMouvement { get; set; }

        public TypeMouvement()
        {
        }
    }

    public class TypeMouvementCollection : ItemCollection
    {
        public static TypeMouvementCollection Charger(string _NatureMouvement)
        {
            TypeMouvementCollection TypeMouvementcollection = new TypeMouvementCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_TypeMouvement_Charger";
                cmd.Parameters.AddWithValue("@CTypeMouvement", DBNull.Value);
                cmd.Parameters.AddWithValue("@NatureMouvement", _NatureMouvement);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TypeMouvement TypeMouvement = new TypeMouvement();
                    TypeMouvement.Code = dr["CTypeMouvement"].ToString();
                    if (dr["LibTypeMouvement"] != DBNull.Value)
                        TypeMouvement.Libelle = dr["LibTypeMouvement"].ToString();
                    if (dr["NatureMouvement"] != DBNull.Value)
                        TypeMouvement.NatureMouvement = dr["NatureMouvement"].ToString();

                    TypeMouvementcollection.Add(TypeMouvement);
                }
                dr.Close();

                return (TypeMouvementcollection);
            }
        }

        public static TypeMouvementCollection ChargerBE()
        {
            TypeMouvementCollection TypeMouvementcollection = new TypeMouvementCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_TypeMouvement_ChargerBE";
                cmd.Parameters.AddWithValue("@CTypeMouvement", DBNull.Value);
                cmd.Parameters.AddWithValue("@NatureMouvement", "BE");
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TypeMouvement TypeMouvement = new TypeMouvement();
                    TypeMouvement.Code = dr["CTypeMouvement"].ToString();
                    if (dr["LibTypeMouvement"] != DBNull.Value)
                        TypeMouvement.Libelle = dr["LibTypeMouvement"].ToString();
                    if (dr["NatureMouvement"] != DBNull.Value)
                        TypeMouvement.NatureMouvement = dr["NatureMouvement"].ToString();

                    TypeMouvementcollection.Add(TypeMouvement);
                }
                dr.Close();

                return (TypeMouvementcollection);
            }
        }

        public static TypeMouvementCollection ChargerBSManuel()
        {
            TypeMouvementCollection TypeMouvementcollection = new TypeMouvementCollection();

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Ref_TypeMouvement_ChargerBSManuel";
        
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    TypeMouvement TypeMouvement = new TypeMouvement();
                    TypeMouvement.Code = dr["CTypeMouvement"].ToString();
                    if (dr["LibTypeMouvement"] != DBNull.Value)
                        TypeMouvement.Libelle = dr["LibTypeMouvement"].ToString();
                    if (dr["NatureMouvement"] != DBNull.Value)
                        TypeMouvement.NatureMouvement = dr["NatureMouvement"].ToString();

                    TypeMouvementcollection.Add(TypeMouvement);
                }
                dr.Close();

                return (TypeMouvementcollection);
            }
        }
    }
}