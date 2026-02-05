using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileRecouvrement
    {
        #region propriété

        public string CClient { get; set; }
        public string NDocument { get; set; }
        public string DateDocument { get; set; }
        public int JourCredit { get; set; }
        public decimal MontantTTC { get; set; }
        public decimal MontantRecu { get; set; }
        public decimal Credit { get; set; }
        public string TypeDocument { get; set; }

        #endregion
    }
    public class MobileRecouvrementClient
    {
        #region propriété

        public string CClient { get; set; }
        public decimal Avance { get; set; }
        public decimal Avoir { get; set; }

        #endregion
    }
    public class MobileRecouvrementCollection : List<MobileRecouvrement>
    {
        public static MobileRecouvrementCollection Charger(string CClient)
        {
            MobileRecouvrementCollection collection = new MobileRecouvrementCollection();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader rd;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_Recouvrement_Charger";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        MobileRecouvrement rec = new MobileRecouvrement();
                        rec.CClient = (string)rd["CClient"];
                        rec.NDocument = (string)rd["NDocument"];
                        rec.DateDocument = rd["DateDocument"] != DBNull.Value ? rd["DateDocument"].ToString() : null;
                        rec.JourCredit = int.Parse(rd["JourCredit"].ToString());
                        rec.MontantTTC = decimal.Parse(rd["MontantTTC"]!= DBNull.Value ?  rd["MontantTTC"].ToString() : "0");
                        rec.MontantRecu = decimal.Parse(rd["MontantRecu"] != DBNull.Value ? rd["MontantRecu"].ToString() : "0");
                        rec.Credit = decimal.Parse(rd["Credit"] != DBNull.Value ? rd["Credit"].ToString() : "0");
                        rec.TypeDocument = (string)rd["TypeDocument"];
                        collection.Add(rec);
                    }
                    rd.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return collection;
        }
    }

    public class MobileRecouvrementClientCollection : List<MobileRecouvrementClient>
    {
        public static MobileRecouvrementClient Charger(string CClient)
        {
            MobileRecouvrementClient rec = new MobileRecouvrementClient();

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    SqlDataReader rd;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Mobile_RecouvrementClient_Charger";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        rec.CClient = (string)rd["CClient"];
                        rec.Avance = decimal.Parse(rd["Avance"] != DBNull.Value ? rd["Avance"].ToString() : "0");
                        rec.Avoir = decimal.Parse(rd["Avoir"] != DBNull.Value ? rd["Avoir"].ToString() : "0");
                        
                    }
                    rd.Close();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    connection.Close();
                }
            }
            return rec;
        }
    }

}
