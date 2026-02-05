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
    public class RattachementChargesDivers
    {


         #region Propriétés

       
        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }

        [XmlAttribute("CChargesDivers")]
        [Bindable(true)]
        public string CChargesDivers { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }
        [XmlAttribute("BSolde")]
        [Bindable(true)]
        public bool BSolde { get; set; }

        [XmlAttribute("Cout")]
        [Bindable(true)]
        public decimal Cout { get; set; }

        [XmlAttribute("TypeRattachement")]
        [Bindable(true)]
        public string TypeRattachement { get; set; }

        [XmlAttribute("DateCharge")]
        [Bindable(true)]
        public DateTime DateCharge { get; set; }


        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }
        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        #endregion Propriétés

        public RattachementChargesDivers()
        {
          
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
           try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_RattachementChargesDivers_Sauvegarder";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@CChargesDivers", CChargesDivers);
                    cmd.Parameters.AddWithValue("@Libelle", Libelle);
                    cmd.Parameters.AddWithValue("@Cout", Cout);
                    cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);
                    cmd.Parameters.AddWithValue("@DateCharge", DateCharge);

                    
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@BSolde", this.BSolde);
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);

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



        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
            }
        }








        public static RattachementChargesDivers Charger(string NRattachement, string CChargesDivers)
        {
            RattachementChargesDivers rattachementChargesDivers = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_RattachementChargesDivers_Charger";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@CChargesDivers", CChargesDivers);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachementChargesDivers = new RattachementChargesDivers();


                            rattachementChargesDivers.NRattachement = dr["NRattachement"].ToString();
                            rattachementChargesDivers.CChargesDivers = dr["CChargesDivers"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementChargesDivers.Libelle = dr["Libelle"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                rattachementChargesDivers.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementChargesDivers.TypeRattachement = dr["TypeRattachement"].ToString();
                            if (dr["DateCharge"] != DBNull.Value)
                                rattachementChargesDivers.DateCharge = DateTime.Parse(dr["DateCharge"].ToString());
                            if (dr["BSolde"] != DBNull.Value)
                                rattachementChargesDivers.BSolde = bool.Parse(dr["BSolde"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                rattachementChargesDivers.NOrdredeTravail =dr["NOrdredeTravail"].ToString();
               
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return rattachementChargesDivers;
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
                    cmd.CommandText = "GP_RattachementChargesDivers_Supprimer";

                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@CChargesDivers", CChargesDivers);

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
    }

    public class RattachementChargesDiversCollection : List<RattachementChargesDivers>
    {

        public static RattachementChargesDiversCollection Charger(string nratt)
        {
            RattachementChargesDiversCollection collection = new RattachementChargesDiversCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_RattachementChargesDivers_Charger";

                    cmd.Parameters.AddWithValue("@CChargesDivers", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", nratt);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RattachementChargesDivers rattachementChargesDivers = new RattachementChargesDivers();


                            rattachementChargesDivers.NRattachement = dr["NRattachement"].ToString();
                            rattachementChargesDivers.CChargesDivers = dr["CChargesDivers"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementChargesDivers.Libelle = dr["Libelle"].ToString();
                            if (dr["Cout"] != DBNull.Value)
                                rattachementChargesDivers.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementChargesDivers.TypeRattachement = dr["TypeRattachement"].ToString();
                            if (dr["DateCharge"] != DBNull.Value)
                                rattachementChargesDivers.DateCharge = DateTime.Parse(dr["DateCharge"].ToString());
                            if (dr["BSolde"] != DBNull.Value)
                                rattachementChargesDivers.BSolde = bool.Parse(dr["BSolde"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                rattachementChargesDivers.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            collection.Add(rattachementChargesDivers);
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

}
