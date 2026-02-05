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
    public class RemiseCollection : ItemCollection
    {
        public RemiseCollection()
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
                cmd.CommandText = "RptRemise_Charger";
                cmd.Parameters.AddWithValue("@CRemise", DBNull.Value);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptRemise_Charger");
            }
            return (ds);
        }

        public static RemiseCollection Charger()
        {
            RemiseCollection collection = new RemiseCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Remise_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CRemise", DBNull.Value));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Remise remise = new Remise();

                            remise.Code = dr["CRemise"].ToString().Trim();
                            remise.Libelle = dr["LibRemise"].ToString().Trim();
                            if (dr["Priorite"] != DBNull.Value)
                                remise.Priorite = int.Parse(dr["Priorite"].ToString());
                            collection.Add(remise);
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
                throw;
            }

            return collection;
        }

        //public Remise Obtenir(int cRemise)
        //{
        //    Remise remise = this.Where(x => x.CRemise.Equals(cRemise)).FirstOrDefault();
        //    return remise;
        //}
    }

    [Serializable]
    public class Remise : Item
    {
        #region Propriétés

        [XmlAttribute("Priorite")]
        [Bindable(true)]
        public int Priorite { get; set; }

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

        public Remise()
        {
        }

        public static Remise Charger(string cRemise)
        {
            //LogHelper.Info(cRemise);
            Remise remise = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Remise_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CRemise", cRemise));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            remise = new Remise();

                            remise.Code = dr["CRemise"].ToString().Trim();
                            remise.Libelle = dr["LibRemise"].ToString().Trim();
                            if (dr["Priorite"] != DBNull.Value)
                                remise.Priorite = int.Parse(dr["Priorite"].ToString());
                        }
                        dr.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
                throw;
            }

            return remise;
        }

        public void Sauvegarder()
        {
            //LogHelper.Info(this.CRemise);

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Remise_Sauvegarder";
                    cmd.Parameters.Add(new SqlParameter("@CRemise", this.Code));
                    cmd.Parameters.Add(new SqlParameter("@LibRemise", this.Libelle));
                    cmd.Parameters.Add(new SqlParameter("@Priorite", this.Priorite));
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null) sqlParametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
                throw;
            }
        }

        public void Supprimer()
        {
            //LogHelper.Info(this.CRemise);

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Remise_Supprimer";
                    cmd.Parameters.Add(new SqlParameter("@CRemise", this.Code));
                    foreach (SqlParameter sqlParametre in cmd.Parameters)
                        if (sqlParametre.Value == null) sqlParametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex);
                throw;
            }
        }
    }
}