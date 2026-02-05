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

namespace CST.LePoint.Tools
{
    [Serializable]
    public class PivotGrid : Item
    {
        #region Propriétés

        [XmlAttribute("Id")]
        [Bindable(true)]
        public string Id { get; set; }

        [XmlAttribute("NomPivotGrid")]
        [Bindable(true)]
        public string NomPivotGrid { get; set; }

        [XmlAttribute("NomRapport")]
        [Bindable(true)]
        public string NomRapport { get; set; }

        [XmlAttribute("Chemin")]
        [Bindable(true)]
        public string Chemin { get; set; }

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

        public PivotGrid()
        {
            Code = this.Id;
            this.Chemin = string.Empty;
        }

        public void Sauvgarder()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PivotGrid_Sauvgarder";

                    cmd.Parameters.AddWithValue("@NomPivotGrid", NomPivotGrid);
                    cmd.Parameters.AddWithValue("@Chemin", Chemin);
                    cmd.Parameters.AddWithValue("@NomRapport", NomRapport);
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
                }
            }
            catch (Exception)
            {
                throw;
            }


        }
        public string Charger()
        {

            return "";
        }
        public void Supprimmer(string Nom)
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
                    cmd.CommandText = "PivotGridFormatSupprimer";
                    cmd.Parameters.AddWithValue("@Nom", Nom);

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
        public static PivotGrid Charger(string NomRapport, int CreePar, string NomPivotGrid)
        {
            PivotGrid pv = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {

                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PivotGrid_ListeCharger";
                    cmd.Parameters.AddWithValue("@NomPivotGrid", NomPivotGrid);
                    cmd.Parameters.AddWithValue("@NomRapport", NomRapport);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {

                            pv = new PivotGrid();
                            if (dr["Id"] != DBNull.Value)
                                pv.Libelle = dr["Id"].ToString();
                            if (dr["NomPivotGrid"] != DBNull.Value)
                                pv.Code = dr["NomPivotGrid"].ToString();
                            if (dr["Chemin"] != DBNull.Value)
                                pv.Chemin = dr["Chemin"].ToString();
                            if (dr["NomRapport"] != DBNull.Value)
                                pv.NomRapport = dr["NomRapport"].ToString();
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return pv;
        }



        public static PivotGrid ChargerChemin(string NomRapport, int CreePar, string NomPivotGrid)
        {
            PivotGrid pv = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {

                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PivotGrid_ListeCharger";
                    cmd.Parameters.AddWithValue("@NomPivotGrid", NomPivotGrid);
                    cmd.Parameters.AddWithValue("@NomRapport", NomRapport);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {

                            pv = new PivotGrid();
                            if (dr["Id"] != DBNull.Value)
                                pv.Libelle = dr["Id"].ToString();
                            if (dr["NomPivotGrid"] != DBNull.Value)
                                pv.Code = dr["NomPivotGrid"].ToString();
                            if (dr["Chemin"] != DBNull.Value)
                                pv.Chemin = dr["Chemin"].ToString();

                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return pv;
        }
    }
    public class PivotGridCollection : ItemCollection
    {


        public PivotGridCollection()
        {

        }

        public static PivotGridCollection Charger(string NomRapport, int CreePar, string NomPivotGrid)
        {
            PivotGridCollection pvc = new PivotGridCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    //////SqlCommand cmd = new SqlCommand() { Connection = cn, CommandType = CommandType.StoredProcedure, CommandText = "ScanDocPivotGrid_ListeCharger" };
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PivotGrid_ListeCharger";
                    cmd.Parameters.AddWithValue("@NomPivotGrid", NomPivotGrid);
                    cmd.Parameters.AddWithValue("@NomRapport", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        while (dr.Read())
                        {
                            PivotGrid pv = new PivotGrid();


                            if (dr["NomRapport"] != DBNull.Value)
                                pv.Code = dr["NomRapport"].ToString();
                            if (dr["Id"] != DBNull.Value)
                                pv.Libelle = dr["Id"].ToString();
                            if (dr["Chemin"] != DBNull.Value)
                                pv.Chemin = dr["Chemin"].ToString();
                            if (dr["NomPivotGrid"] != DBNull.Value)
                                pv.NomRapport = dr["NomPivotGrid"].ToString();
                            pvc.Add(pv);
                        }

                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return pvc;
        }


    }
}
