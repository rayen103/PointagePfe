//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Xml.Serialization;

//namespace CST.LePoint.Tiers.Referentiel
//{
//    [Serializable]
//    public class TypeAdresseCollection : List<TypeAdresse>
//    {
//        public TypeAdresseCollection()
//        {
//        }

//        public static DataSet ChargerVue()
//        {
//            DataSet ds = new DataSet();

//            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//            {
//                cn.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = cn;
//                cmd.CommandType = CommandType.StoredProcedure;
//                cmd.CommandText = "RptTypeAdresse_Charger";
//                cmd.Parameters.AddWithValue("@CTypeAdresse", DBNull.Value);

//                foreach (SqlParameter parametre in cmd.Parameters)
//                {
//                    if (parametre.Value == null)
//                    {
//                        parametre.Value = DBNull.Value;
//                    }
//                }

//                SqlDataAdapter sda = new SqlDataAdapter(cmd);
//                sda.Fill(ds, "RptTypeAdresse_Charger");
//            }
//            return (ds);
//        }

//        public static TypeAdresseCollection Charger()
//        {
//            TypeAdresseCollection collection = new TypeAdresseCollection();

//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();

//                    SqlCommand cmd = cn.CreateCommand();
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Ref_TypeAdresse_Charger";
//                    cmd.Parameters.Add(new SqlParameter("@CTypeAdresse", DBNull.Value));

//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        while (dr.Read())
//                        {
//                            TypeAdresse typeAdresse = new TypeAdresse();
//                            typeAdresse.CTypeAdresse = dr["CTypeAdresse"].ToString().Trim();
//                            if (dr["LibTypeAdresse"] != DBNull.Value)
//                                typeAdresse.LibTypeAdresse = dr["LibTypeAdresse"].ToString().Trim();
//                            collection.Add(typeAdresse);
//                        }
//                        dr.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//            return collection;
//        }

//        public TypeAdresse Obtenir(string cTypeAdresse)
//        {
//            TypeAdresse typeAdresse = this.Where(x => x.CTypeAdresse.Equals(cTypeAdresse)).FirstOrDefault();
//            return typeAdresse;
//        }
//    }

//    [Serializable]
//    public class TypeAdresse
//    {
//        #region Propriétés

//        [XmlAttribute("CTypeAdresse")]
//        [Bindable(true)]
//        public string CTypeAdresse { get; set; }

//        [XmlAttribute("LibTypeAdresse")]
//        [Bindable(true)]
//        public string LibTypeAdresse { get; set; }

//        [XmlAttribute("DateInsertion")]
//        [Bindable(true)]
//        public DateTime DateInsertion { get; set; }

//        [XmlAttribute("DateModification")]
//        [Bindable(true)]
//        public DateTime DateModification { get; set; }

//        [XmlAttribute("CreePar")]
//        [Bindable(true)]
//        public int CreePar { get; set; }

//        [XmlAttribute("ModifiePar")]
//        [Bindable(true)]
//        public int ModifiePar { get; set; }

//        [XmlAttribute("PCInsertion")]
//        [Bindable(true)]
//        public string PCInsertion { get; set; }

//        [XmlAttribute("PCModification")]
//        [Bindable(true)]
//        public string PCModification { get; set; }

//        #endregion Propriétés

//        public TypeAdresse()
//        {
//            this.CTypeAdresse = string.Empty;
//            this.LibTypeAdresse = string.Empty;
//        }

//        public void Sauvegarder()
//        {
//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();

//                    SqlCommand cmd = cn.CreateCommand();
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Ref_TypeAdresse_Sauvegarder";
//                    cmd.Parameters.Add(new SqlParameter("@CTypeAdresse", CTypeAdresse));
//                    cmd.Parameters.Add(new SqlParameter("@LibTypeAdresse", LibTypeAdresse));
//                    cmd.Parameters.Add(new SqlParameter("@CreePar", CreePar));
//                    cmd.Parameters.Add(new SqlParameter("@ModifiePar", ModifiePar));
//                    cmd.Parameters.Add(new SqlParameter("@PCInsertion", PCInsertion));
//                    cmd.Parameters.Add(new SqlParameter("@PCModification", PCModification));
//                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
//                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

//                    foreach (SqlParameter sqlParametre in cmd.Parameters)
//                        if (sqlParametre.Value == null)
//                            sqlParametre.Value = DBNull.Value;

//                    cmd.ExecuteNonQuery();
//                    cmd.Dispose();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public void Supprimer()
//        {
//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();

//                    SqlCommand cmd = cn.CreateCommand();
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Ref_TypeAdresse_Supprimer";
//                    cmd.Parameters.Add(new SqlParameter("@CTypeAdresse", CTypeAdresse));
//                    foreach (SqlParameter sqlParametre in cmd.Parameters)
//                    {
//                        if (sqlParametre.Value == null)
//                        {
//                            sqlParametre.Value = DBNull.Value;
//                        }
//                    }
//                    cmd.ExecuteNonQuery();
//                    cmd.Dispose();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public static TypeAdresse Charger(string cTypeAdresse)
//        {
//            TypeAdresse typeAdresse = null;

//            try
//            {
//                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//                {
//                    cn.Open();

//                    SqlCommand cmd = cn.CreateCommand();
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "Ref_TypeAdresse_Charger";
//                    cmd.Parameters.Add(new SqlParameter("@CTypeAdresse", cTypeAdresse));

//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        if (dr.Read())
//                        {
//                            typeAdresse = new TypeAdresse();

//                            typeAdresse.CTypeAdresse = dr["CTypeAdresse"].ToString().Trim();
//                            if (dr["LibTypeAdresse"] != DBNull.Value)
//                                typeAdresse.LibTypeAdresse = dr["LibTypeAdresse"].ToString().Trim();
//                        }
//                        dr.Close();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }

//            return typeAdresse;
//        }
//    }
//}