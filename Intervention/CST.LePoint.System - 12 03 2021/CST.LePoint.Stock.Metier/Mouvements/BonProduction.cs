using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonProduction
    {
        [XmlAttribute("NBonProduction")]
        [Bindable(true)]
        public string NBonProduction { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("DateProduction")]
        [Bindable(true)]
        public DateTime DateProduction { get; set; }

        [XmlAttribute("DatePeremption")]
        [Bindable(true)]
        public DateTime DatePeremption { get; set; }

        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

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

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        public BonEntree BonEntree = new BonEntree();

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
                catch
                {
                    transaction.Rollback();
                }
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
                catch
                {
                    transaction.Rollback();
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
                cmd.CommandText = "BonProduction_Inserer";

                cmd.Parameters.AddWithValue("@CEntrepot ", this.CEntrepot);
                cmd.Parameters.AddWithValue("@DateProduction ", this.DateProduction);
                cmd.Parameters.AddWithValue("@DatePeremption ", this.DatePeremption);
                cmd.Parameters.AddWithValue("@Observation ", this.Observation);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonProduction = dr["NBonProduction"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                BonEntree.NDocumentSource = this.NBonProduction;
                BonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONPRODUCTION.ToString();
                BonEntree.Inserer(transaction);
            }
            catch (Exception)
            {
                throw;
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
                cmd.CommandText = "BonProduction_Modifier";

                cmd.Parameters.AddWithValue("@NBonProduction ", this.NBonProduction);
                cmd.Parameters.AddWithValue("@CEntrepot ", this.CEntrepot);
                cmd.Parameters.AddWithValue("@DateProduction ", this.DateProduction);
                cmd.Parameters.AddWithValue("@Observation ", this.Observation);

                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                BonEntree.NDocumentSource = this.NBonProduction;
                BonEntree.TypeMouvement = StockHelper.TypesMouvementStock.BE_BONPRODUCTION.ToString();
                BonEntree.Modifier(transaction);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static BonProduction Charger(string nbonProduction)
        {
            BonProduction bonProduction = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonProduction_Charger";
                    cmd.Parameters.AddWithValue("@NBonProduction", nbonProduction);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonProduction = new BonProduction();
                            bonProduction.NBonProduction = dr["NBonProduction"].ToString();
                            if (dr["CEntrepot"] != DBNull.Value)
                                bonProduction.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonProduction.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonProduction.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonProduction.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["DateProduction"] != DBNull.Value)
                                bonProduction.DateProduction = DateTime.Parse(dr["DateProduction"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                bonProduction.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonProduction.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonProduction.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                bonProduction.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonProduction.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonProduction.PCModification = dr["PCModification"].ToString();
                        }
                    }
                }
                bonProduction.BonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONPRODUCTION.ToString(), bonProduction.NBonProduction);
            }
            catch
            { }
            return bonProduction;
        }
    }

    public class BonProductionCollection : List<BonProduction>
    {
        public static BonProductionCollection Charger(string nbonProduction)
        {
            BonProductionCollection collection = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonProduction_Charger";
                    cmd.Parameters.AddWithValue("@NBonProduction", DBNull.Value);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            BonProduction bonProduction = new BonProduction();
                            bonProduction.NBonProduction = dr["NBonProduction"].ToString();
                            if (dr["CEntrepot"] != DBNull.Value)
                                bonProduction.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CreePar"] != DBNull.Value)
                                bonProduction.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonProduction.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                bonProduction.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["DateProduction"] != DBNull.Value)
                                bonProduction.DateProduction = DateTime.Parse(dr["DateProduction"].ToString());
                            if (dr["DatePeremption"] != DBNull.Value)
                                bonProduction.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonProduction.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                bonProduction.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["Observation"] != DBNull.Value)
                                bonProduction.Observation = dr["Observation"].ToString();
                            if (dr["PCInsertion"] != DBNull.Value)
                                bonProduction.PCInsertion = dr["PCInsertion"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                bonProduction.PCModification = dr["PCModification"].ToString();
                            bonProduction.BonEntree = BonEntree.ChargerParDocumentSource(StockHelper.TypesMouvementStock.BE_BONPRODUCTION.ToString(), bonProduction.NBonProduction);
                            collection.Add(bonProduction);
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