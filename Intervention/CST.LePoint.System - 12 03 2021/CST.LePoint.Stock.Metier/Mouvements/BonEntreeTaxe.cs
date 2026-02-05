//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Data.SqlClient;
//using System.Configuration;
//using System.Data;
//using System.Xml.Serialization;
//using System.ComponentModel;

//namespace CST.LePoint.Stock.Metier
//{
//    [Serializable]
//    public class BonEntreeTaxe
//    {
//        #region Propriétés

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public string CEntrepot { get; set; }

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public string NBonEntree { get; set; }

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public string CTaxe { get; set; }

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public decimal MontantAssiette { get; set; }

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public decimal MontantTaxe { get; set; }

//[XmlAttribute("CRemise")]
//[Bindable(true)]
//public decimal TauxTVA { get; set; }

//        [XmlAttribute("DateInsertion")]
//        [Bindable(true)]
//        public DateTime? DateInsertion { get; set; }

//        [XmlAttribute("PCInsertion")]
//        [Bindable(true)]
//        public string PCInsertion { get; set; }

//        [XmlAttribute("CreePar")]
//        [Bindable(true)]
//        public int CreePar { get; set; }

//        [XmlAttribute("PCModification")]
//        [Bindable(true)]
//        public string PCModification { get; set; }

//        [XmlAttribute("ModifiePar")]
//        [Bindable(true)]
//        public int ModifiePar { get; set; }

//        [XmlAttribute("DateModification")]
//        [Bindable(true)]
//        public DateTime? DateModification { get; set; }
//        #endregion

//        public BonEntreeTaxe(string cEntrepot, string nBonEntree)
//        {
//            CEntrepot = cEntrepot;
//            NBonEntree = nBonEntree;
//            MontantAssiette = 0m;
//            MontantTaxe = 0m;
//        }
//        public void Sauvegarder(SqlTransaction transaction)
//        {
//            try
//            {
//                SqlCommand cmd = new SqlCommand();
//                cmd.Transaction = transaction;
//                cmd.Connection = transaction.Connection;
//                cmd.CommandType = CommandType.StoredProcedure;

//                cmd.CommandText = "BonEntreeTaxe_Inserer";
//                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
//                cmd.Parameters.AddWithValue("@NBonEntree", NBonEntree);
//                cmd.Parameters.AddWithValue("@CTaxe", CTaxe);
//                cmd.Parameters.AddWithValue("@MontantAssiette", MontantAssiette);
//                cmd.Parameters.AddWithValue("@MontantTaxe", MontantTaxe);
//                cmd.Parameters.AddWithValue("@TauxTVA", TauxTVA);

//                foreach (SqlParameter parametre in cmd.Parameters)
//                {
//                    if (parametre.Value == null)
//                    {
//                        parametre.Value = DBNull.Value;
//                    }
//                }

//                cmd.ExecuteNonQuery();
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        public static BonEntreeTaxe Charger(string cEntrepot, string nBonEntree, string cTaxe)
//        {
//            BonEntreeTaxe bonEntreeTaxe = null;
//            bonEntreeTaxe.CTaxe = cTaxe;
//            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
//            {
//                cn.Open();
//                SqlTransaction transaction = cn.BeginTransaction();
//                try
//                {
//                    SqlCommand cmd = new SqlCommand();
//                    cmd.Transaction = transaction;
//                    cmd.Connection = transaction.Connection;
//                    cmd.CommandType = CommandType.StoredProcedure;
//                    cmd.CommandText = "BonEntreeTaxe_Charger";
//                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
//                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
//                    cmd.Parameters.AddWithValue("@CTaxe", cTaxe);

//                    using (SqlDataReader dr = cmd.ExecuteReader())
//                    {
//                        while (dr.Read())
//                        {
//                            bonEntreeTaxe = new BonEntreeTaxe(nBonEntree, cEntrepot);
//                            bonEntreeTaxe.CTaxe = cTaxe;
//                            if(dr["MontantAssiette"]!=DBNull.Value)
//                                bonEntreeTaxe.MontantAssiette = decimal.Parse(dr["MontantAssiette"].ToString());
//                            if (dr["MontantTaxe"] != DBNull.Value)
//                                bonEntreeTaxe.MontantTaxe = decimal.Parse(dr["MontantTaxe"].ToString());
//                            if (dr["TauxTVA"] != DBNull.Value)
//                                bonEntreeTaxe.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
//                        }
//                    }
//                }

//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//                return bonEntreeTaxe;
//            }
//        }
//        public static decimal calculPourcentage(decimal TauxTVA, decimal montantHT)
//        {
//            return (montantHT / 100) * TauxTVA;
//        }
//    }

//    public class BonEntreeTaxeCollection : List<BonEntreeTaxe>
//    {
//    }
//}