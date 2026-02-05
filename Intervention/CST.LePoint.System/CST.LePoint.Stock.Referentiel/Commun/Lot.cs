using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Referentiel.Commun
{
    [Serializable]
    public class LotCollection : List<Lot>
    {
        public static LotCollection Charger()
        {
            LotCollection lotCollection = new LotCollection();
            Lot lot = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Lot_Charger";
                    cmd.Parameters.AddWithValue("@CLot", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lot = new Lot();
                            lot.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                lot.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                lot.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["CFournisseur"] != DBNull.Value)
                                lot.CFournisseur = dr["CFournisseur"].ToString().Trim();
                            if (dr["DatePeremption"] != DBNull.Value)
                                lot.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                lot.Statut = dr["Statut"].ToString().Trim();
                            if (dr["NombreContenant"] != DBNull.Value)
                                lot.NombreContenant = decimal.Parse(dr["NombreContenant"].ToString());
                            if (dr["LotFabrication"] != DBNull.Value)
                                lot.LotFabrication = dr["LotFabrication"].ToString().Trim();
                            if (dr["CEmballage"] != DBNull.Value)
                                lot.CEmballage = dr["CEmballage"].ToString().Trim();
                            if (dr["QC"] != DBNull.Value)
                                lot.QC = decimal.Parse(dr["QC"].ToString());
                            if (dr["BEclate"] != DBNull.Value)
                                lot.BEclate = bool.Parse(dr["BEclate"].ToString());
                            if (dr["DateProduction"] != DBNull.Value)
                                lot.DateProduction = DateTime.Parse(dr["DateProduction"].ToString());
                            if (dr["QuantitePrevu"] != DBNull.Value)
                                lot.QuantitePrevu = decimal.Parse(dr["QuantitePrevu"].ToString());
                            if (dr["QuantiteInitiale"] != DBNull.Value)
                                lot.QuantiteInitiale = decimal.Parse(dr["QuantiteInitiale"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                lot.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                lot.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                lot.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                lot.PCModification = decimal.Parse(dr["PCModification"].ToString());
                            lotCollection.Add(lot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return lotCollection;
        }
    }

    [Serializable]
    public class Lot
    {
        #region Proprietés

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("LibArticle")]
        [Bindable(true)]
        public string LibArticle { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("DatePeremption")]
        [Bindable(true)]
        public DateTime? DatePeremption { get; set; }

        [XmlAttribute("Statut")]
        [Bindable(true)]
        public string Statut { get; set; }

        [XmlAttribute("NombreContenant")]
        [Bindable(true)]
        public decimal NombreContenant { get; set; }

        [XmlAttribute("LotFabrication")]
        [Bindable(true)]
        public string LotFabrication { get; set; }

        [XmlAttribute("CEmballage")]
        [Bindable(true)]
        public string CEmballage { get; set; }

        [XmlAttribute("QC")]
        [Bindable(true)]
        public decimal QC { get; set; }

        [XmlAttribute("BEclate")]
        [Bindable(true)]
        public bool BEclate { get; set; }

        [XmlAttribute("DateProduction")]
        [Bindable(true)]
        public DateTime? DateProduction { get; set; }

        [XmlAttribute("QuantitePrevu")]
        [Bindable(true)]
        public decimal QuantitePrevu { get; set; }

        [XmlAttribute("QuantiteInitiale")]
        [Bindable(true)]
        public decimal QuantiteInitiale { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public decimal PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public decimal PCModification { get; set; }

        #endregion Proprietés

        public Lot()
        {
        }

        public static Lot Charger(string clot)
        {
            Lot lot = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Ref_Lot_Charger";
                    cmd.Parameters.Add(new SqlParameter("@CLot", clot));

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            lot = new Lot();
                            lot.CLot = dr["CLot"].ToString().Trim();
                            if (dr["CArticle"] != DBNull.Value)
                                lot.CArticle = dr["CArticle"].ToString().Trim();
                            if (dr["LibArticle"] != DBNull.Value)
                                lot.LibArticle = dr["LibArticle"].ToString().Trim();
                            if (dr["CFournisseur"] != DBNull.Value)
                                lot.CFournisseur = dr["CFournisseur"].ToString().Trim();
                            if (dr["DatePeremption"] != DBNull.Value)
                                lot.DatePeremption = DateTime.Parse(dr["DatePeremption"].ToString());
                            if (dr["Statut"] != DBNull.Value)
                                lot.Statut = dr["Statut"].ToString().Trim();
                            if (dr["NombreContenant"] != DBNull.Value)
                                lot.NombreContenant = decimal.Parse(dr["NombreContenant"].ToString());
                            if (dr["LotFabrication"] != DBNull.Value)
                                lot.LotFabrication = dr["LotFabrication"].ToString().Trim();
                            if (dr["CEmballage"] != DBNull.Value)
                                lot.CEmballage = dr["CEmballage"].ToString().Trim();
                            if (dr["QC"] != DBNull.Value)
                                lot.QC = decimal.Parse(dr["QC"].ToString());
                            if (dr["BEclate"] != DBNull.Value)
                                lot.BEclate = bool.Parse(dr["BEclate"].ToString());
                            if (dr["DateProduction"] != DBNull.Value)
                                lot.DateProduction = DateTime.Parse(dr["DateProduction"].ToString());
                            if (dr["QuantitePrevu"] != DBNull.Value)
                                lot.QuantitePrevu = decimal.Parse(dr["QuantitePrevu"].ToString());
                            if (dr["QuantiteInitiale"] != DBNull.Value)
                                lot.QuantiteInitiale = decimal.Parse(dr["QuantiteInitiale"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                lot.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                lot.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["PCInsertion"] != DBNull.Value)
                                lot.PCInsertion = decimal.Parse(dr["PCInsertion"].ToString());
                            if (dr["PCModification"] != DBNull.Value)
                                lot.PCModification = decimal.Parse(dr["PCModification"].ToString());
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return lot;
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

                    cmd.CommandText = "Ref_Lot_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CLot", CLot);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.Parameters.AddWithValue("@LibArticle", LibArticle);
                    cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                    cmd.Parameters.AddWithValue("@DatePeremption", DatePeremption);
                    cmd.Parameters.AddWithValue("@Statut", Statut);
                    cmd.Parameters.AddWithValue("@NombreContenant", NombreContenant);
                    cmd.Parameters.AddWithValue("@LotFabrication", LotFabrication);
                    cmd.Parameters.AddWithValue("@CEmballage", CEmballage);
                    cmd.Parameters.AddWithValue("@QC", QC);
                    cmd.Parameters.AddWithValue("@BEclate", BEclate);
                    cmd.Parameters.AddWithValue("@DateProduction", DateProduction);
                    cmd.Parameters.AddWithValue("@QuantitePrevu", QuantitePrevu);
                    cmd.Parameters.AddWithValue("@QuantiteInitiale", QuantiteInitiale);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
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

                    cmd.CommandText = "Ref_Lot_Supprimer";
                    cmd.Parameters.AddWithValue("@CLot", CLot);
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
    }
}