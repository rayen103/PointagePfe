using CST.LePoint.Referentiel;
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
    public class ChargesDiversCollection : ItemCollection
    {
        //public static DataSet ChargerVue()
        //{
        //    DataSet ds = new DataSet();

        //    using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Vehicule_Rpt_Charger";
        //        cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(ds, "Vehicule_Rpt_Charger");
        //    }
        //    return (ds);
        //}

        //public static DataTable RemplirGrid()
        //{
        //    DataTable dt = new DataTable();

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.CommandText = "Ref_Vehicule_ChargerTous";
        //        cmd.Parameters.AddWithValue("@CVehicule", DBNull.Value);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //        {
        //            if (parametre.Value == null)
        //            {
        //                parametre.Value = DBNull.Value;
        //            }
        //        }
        //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //        adapter.Fill(dt);
        //    }
        //    return (dt);
        //}

        public static ChargesDiversCollection Charger()
        {
            ChargesDiversCollection collection = new ChargesDiversCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_ChargesDivers_Charger";
                    cmd.Parameters.AddWithValue("@CChargesDivers", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ChargesDivers chargesDivers = new ChargesDivers();

                            chargesDivers.Code = dr["CChargesDivers"].ToString();
                            chargesDivers.Libelle = dr["Libelle"].ToString();
                            chargesDivers.BSolde = bool.Parse(dr["BSolde"].ToString());

                            collection.Add(chargesDivers);
                        }
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

    [Serializable]
    public class ChargesDivers : Item
    {
        #region Propriétés

        [XmlAttribute("CChargesDivers")]
        [Bindable(true)]
        public string CChargesDivers { get; set; }

        [XmlAttribute("BSolde")]
        [Bindable(true)]
        public bool BSolde { get; set; }





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

        public ChargesDivers()
        {
            //this.BActif = true;
            //this.BDisponible = true;
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
                    cmd.CommandText = "GP_ChargesDivers_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CChargesDivers", this.Code);
                    cmd.Parameters.AddWithValue("@Libelle", this.Libelle);


                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@BSolde", this.BSolde);


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
                    cmd.CommandText = "GP_ChargesDivers_Supprimer";
                    cmd.Parameters.AddWithValue("@CChargesDivers", this.Code);

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

        public static ChargesDivers Charger(string CChargesDivers)
        {
            ChargesDivers chargesDivers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_ChargesDivers_Charger";
                    cmd.Parameters.AddWithValue("@CChargesDivers", CChargesDivers);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            chargesDivers = new ChargesDivers();

                            chargesDivers.Code = dr["CChargesDivers"].ToString();
                            chargesDivers.Libelle = dr["Libelle"].ToString();
                            chargesDivers.BSolde = bool.Parse(dr["BSolde"].ToString());





                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return chargesDivers;
        }
    }
}
