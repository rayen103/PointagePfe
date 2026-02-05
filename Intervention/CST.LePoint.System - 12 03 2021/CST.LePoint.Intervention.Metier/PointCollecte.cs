
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CST.LePoint.Referentiel;
using CST.LePoint.Securite;
namespace CST.LePoint.Intervention.Metier
{
    public class PointCollecte : Item
    {
        public string Lib_PC { get; set; }

        public string Code_PC { get; set; }
        public decimal Latt_PC { get; set; }
        public decimal Long_PC { get; set; }
        public string Code_Gouv_PC { get; set; }
        public string Code_Region_PC { get; set; }
        public string Code_Societe { get; set; }
        public string Code_Site { get; set; }

        public DateTime? DateInsertion { get; set; }
        public DateTime? DateModification { get; set; }
        public int CreePar { get; set; }
        public int ModifiePar { get; set; }
        public string PCInsertion { get; set; }
        public string PCModification { get; set; }


        public PointCollecte()
        { }

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
                    cmd.CommandText = "PointCollecte_Sauvegarder";
                    cmd.Parameters.AddWithValue("@Code_PC", this.Code_PC);
                    cmd.Parameters.AddWithValue("@Lib_PC", this.Lib_PC);
                    cmd.Parameters.AddWithValue("@Code_Gouv_PC", this.Code_Gouv_PC);
                    cmd.Parameters.AddWithValue("@Code_Region_PC", this.Code_Region_PC);
                    cmd.Parameters.AddWithValue("@Latt_PC", this.Latt_PC);
                    cmd.Parameters.AddWithValue("@Long_PC", this.Long_PC);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                    cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                    cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);
                    cmd.Parameters.AddWithValue("@Code_Site", GestionSession.SocieteSite);

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
                    cmd.CommandText = "PointCollecte_Supprimer";
                    cmd.Parameters.AddWithValue("@Code_PC", Code_PC);
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

        public static PointCollecte Charger(string Code_PC)
        {
            PointCollecte pc = null;
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PointCollecte_Charger";
                cmd.Parameters.AddWithValue("@Code_PC", Code_PC);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pc = new PointCollecte();
                    pc.Code_PC = dr["Code_PC"].ToString();
                    if (dr["Lib_PC"] != DBNull.Value)
                        pc.Lib_PC = dr["Lib_PC"].ToString();
                    if (dr["Code_Gouv_PC"] != DBNull.Value)
                        pc.Code_Gouv_PC = dr["Code_Gouv_PC"].ToString();
                    if (dr["Code_Region_PC"] != DBNull.Value)
                        pc.Code_Region_PC = dr["Code_Region_PC"].ToString();
                    if (dr["Latt_PC"] != DBNull.Value)
                        pc.Latt_PC = decimal.Parse(dr["Latt_PC"].ToString());
                    if (dr["Long_PC"] != DBNull.Value)
                        pc.Long_PC = decimal.Parse(dr["Long_PC"].ToString());
                }
            }
            return (pc);
        }
    }
    public class PointCollecteCollection : ItemCollection
    {

        public static PointCollecteCollection Charger()
        {
            PointCollecteCollection pointCollecteCollection = new PointCollecteCollection();
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "PointCollecte_Charger";
                cmd.Parameters.AddWithValue("@Code_PC", DBNull.Value);
                cmd.Parameters.AddWithValue("@Code_Societe", GestionSession.SocieteCourante.CSociete);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    PointCollecte pc = new PointCollecte();
                    pc.Code_PC = dr["Code_PC"].ToString();
                    pc.Code = dr["Code_PC"].ToString();
                    pc.Lib_PC = dr["Lib_PC"].ToString();
                    pc.Libelle = dr["Lib_PC"].ToString();
                    pc.Code_Gouv_PC = dr["Code_Gouv_PC"].ToString();

                    pointCollecteCollection.Add(pc);
                }
                dr.Close();
            }
            return (pointCollecteCollection);
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
                cmd.CommandText = "Bien_Charger_avec_surface";
                cmd.Parameters.AddWithValue("@Code_PC", DBNull.Value);

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
    }
}
