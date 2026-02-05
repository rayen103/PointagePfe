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
namespace CST.LePoint.Securite.Entites
{
    public class Parametres
    {
        #region Propriétès

        [XmlAttribute("CApplication")]
        [Bindable(true)]
        public string CApplication { get; set; }

        [XmlAttribute("CParametre")]
        [Bindable(true)]
        public string CParametre { get; set; }

        [XmlAttribute("Parametre")]
        [Bindable(true)]
        public string Parametre { get; set; }

        [XmlAttribute("Description")]
        [Bindable(true)]
        public string Description { get; set; }

        [XmlAttribute("TypeParametre")]
        [Bindable(true)]
        public string TypeParametre { get; set; }

        [XmlAttribute("Valeur")]
        [Bindable(true)]
        public string Valeur { get; set; }

        [XmlAttribute("Indication")]
        [Bindable(true)]
        public string Indication { get; set; }

        [XmlAttribute("ModifierPar")]
        [Bindable(true)]
        public int ModifierPar { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GC_Parametre_Inserer";
                cmd.Parameters.AddWithValue("@CApplication", this.CApplication);
                cmd.Parameters.AddWithValue("@CParametre", this.CParametre);
                cmd.Parameters.AddWithValue("@Parametre", this.Parametre);
                cmd.Parameters.AddWithValue("@TypeParametre", this.TypeParametre);
                cmd.Parameters.AddWithValue("@Description ", this.Description);
                cmd.Parameters.AddWithValue("@Valeur", this.Valeur);
                cmd.Parameters.AddWithValue("@Indication", this.Indication);
                cmd.Parameters.AddWithValue("@ModifierPar", this.ModifierPar);
                cmd.Parameters.AddWithValue("@DateModification", this.DateModification);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

                foreach (SqlParameter sqlParameter in cmd.Parameters)
                    if (sqlParameter.Value == null)
                        sqlParameter.Value = DBNull.Value;
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GC_Parametre_Modifier";
                cmd.Parameters.AddWithValue("@CApplication", this.CApplication);
                cmd.Parameters.AddWithValue("@CParametre", this.CParametre);
                cmd.Parameters.AddWithValue("@Parametre", this.Parametre);
                cmd.Parameters.AddWithValue("@TypeParametre", this.TypeParametre);
                cmd.Parameters.AddWithValue("@Description", this.Description);
                cmd.Parameters.AddWithValue("@Valeur", this.Valeur);
                cmd.Parameters.AddWithValue("@Indication", this.Indication);
                cmd.Parameters.AddWithValue("@ModifierPar", this.ModifierPar);
                cmd.Parameters.AddWithValue("@DateModification", this.DateModification);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static Parametres Charger(string Cparametre)
        {
            Parametres Parametre = null;
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
                    cmd.CommandText = "GC_Parametre_Charger";
                    cmd.Parameters.AddWithValue("@CParametre", Cparametre);

                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Parametre = new Parametres();
                            if (dr["CParametre"] != DBNull.Value)
                                Parametre.CParametre = dr["CParametre"].ToString();
                            if (dr["Parametre"] != DBNull.Value)
                                Parametre.Parametre = dr["Parametre"].ToString();
                            if (dr["CApplication"] != DBNull.Value)
                                Parametre.CApplication = dr["CApplication"].ToString();
                            if (dr["TypeParametre"] != DBNull.Value)
                                Parametre.TypeParametre = dr["TypeParametre"].ToString();
                            if (dr["Description"] != DBNull.Value)
                                Parametre.Description = dr["Description"].ToString();
                            if (dr["Valeur"] != DBNull.Value)
                                Parametre.Valeur = dr["Valeur"].ToString();
                            if (dr["Indication"] != DBNull.Value)
                                Parametre.Indication = dr["Indication"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (Parametre);
            }

        }

        public static Parametres ChargerParCodeapplication(string Capplication)
        {
            Parametres Parametre = null;
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
                    cmd.CommandText = "GC_Parametre_Charger_ParCapplication";
                    cmd.Parameters.AddWithValue("@CApplication", Capplication);

                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Parametre = new Parametres();
                            if (dr["CParametre"] != DBNull.Value)
                                Parametre.CParametre = dr["CParametre"].ToString();
                            if (dr["Parametre"] != DBNull.Value)
                                Parametre.Parametre = dr["Parametre"].ToString();
                            if (dr["CApplication"] != DBNull.Value)
                                Parametre.CApplication = dr["CApplication"].ToString();
                            if (dr["TypeParametre"] != DBNull.Value)
                                Parametre.TypeParametre = dr["TypeParametre"].ToString();
                            if (dr["Description"] != DBNull.Value)
                                Parametre.Description = dr["Description"].ToString();
                            if (dr["Valeur"] != DBNull.Value)
                                Parametre.Valeur = dr["Valeur"].ToString();
                            if (dr["Indication"] != DBNull.Value)
                                Parametre.Indication = dr["Indication"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                return (Parametre);
            }

        }

        public void Supprimer(SqlTransaction transaction)
        {
           try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
               
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GC_Parametre_Supprimer";
                    cmd.Parameters.AddWithValue("@Parametre", this.Parametre);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    throw;
                }
            }

        public static int Recuperervaleur(string parametre)
        {
            Parametres P = new Parametres();
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
                    cmd.CommandText = "GC_Parametre_Rechercher";
                    cmd.Parameters.AddWithValue("@Parametre", parametre);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            P.Valeur = dr["Valeur"].ToString();
                            if (dr["Valeur"] != DBNull.Value)
                                P.Valeur = dr["Valeur"].ToString();
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
                return int.Parse(P.Valeur);
            }
        }

    }

    public class ParametreCollection : List<Parametres>
    {
        public ParametreCollection()
        {
        }

        public static ParametreCollection Charger(string Capplication)
        {
            ParametreCollection parametreCollection = new ParametreCollection();

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
                    cmd.CommandText = "GC_Parametre_Charger_ParCapplication";
                    cmd.Parameters.AddWithValue("@CApplication", Capplication);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Parametres Parametre = new Parametres();
                            if (dr["CParametre"] != DBNull.Value)
                                Parametre.CParametre = dr["CParametre"].ToString();
                            if (dr["Parametre"] != DBNull.Value)
                                Parametre.Parametre = dr["Parametre"].ToString();
                            if (dr["CApplication"] != DBNull.Value)
                                Parametre.CApplication = dr["CApplication"].ToString();
                            if (dr["TypeParametre"] != DBNull.Value)
                                Parametre.TypeParametre = dr["TypeParametre"].ToString();
                            if (dr["Description"] != DBNull.Value)
                                Parametre.Description = dr["Description"].ToString();
                            if (dr["Valeur"] != DBNull.Value)
                                Parametre.Valeur = dr["Valeur"].ToString();
                            if (dr["Indication"] != DBNull.Value)
                                Parametre.Indication = dr["Indication"].ToString();
                            parametreCollection.Add(Parametre);
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return (parametreCollection);
        }
    }




}
