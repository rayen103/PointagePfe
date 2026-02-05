using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CST.LePoint.Intervention.Metier;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileClientAprospecter
    {

        private string _raisonsocial;
        public string raisonsocial
        {
            get { return _raisonsocial; }
            set { _raisonsocial = value; }
        }
        private string _nompren;
        public string nompren
        {
            get { return _nompren; }
            set { _nompren = value; }
        }
        private string _Tel;
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }
        private string _lat;
        public string lat
        {
            get { return _lat; }
            set { _lat = value; }
        }
        private string _lng;
        public string lng
        {
            get { return _lng; }
            set { _lng = value; }
        }
        private string _Cequipe;
        public string Cequipe
        {
            get { return _Cequipe; }
            set { _Cequipe = value; }
        }
        private List<Photos> _files;
        public List<Photos> files
        {
            get { return _files; }
            set { _files = value; }
        }
        private string _adresse;
        public string adresse
        {
            get { return _adresse; }
            set { _adresse = value; }
        }

        private string _Cregion;
        public string Cregion
        {
            get { return _Cregion; }
            set { _Cregion = value; }
        }

        private string _tva;
        public string tva
        {
            get { return _tva; }
            set { _tva = value; }
        }
        public MobileClientAprospecter()
        {
        }


        public string prospecterClient(MobileClientAprospecter p)
        {
            int IDCP = 0;
            MobileClientAprospecter clientprospecter = p;
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
                    cmd.CommandText = "Mobile_Client_Inserer";
                    cmd.Parameters.AddWithValue("@raisonsociale", clientprospecter.raisonsocial);
                    cmd.Parameters.AddWithValue("@NomPrenom", clientprospecter.nompren);
                    cmd.Parameters.AddWithValue("@Telephone", clientprospecter.Tel);
                    cmd.Parameters.AddWithValue("@Longitude", clientprospecter.lng == "null" ? "0" : clientprospecter.lng);
                    cmd.Parameters.AddWithValue("@Latitude", clientprospecter.lat == "null" ? "0" : clientprospecter.lat );
                    cmd.Parameters.AddWithValue("@CEquipe", clientprospecter.Cequipe);
                    cmd.Parameters.AddWithValue("@CTVA", clientprospecter.tva);
                     cmd.Parameters.AddWithValue("@Cregion", clientprospecter.Cregion);
                     foreach (SqlParameter parametre in cmd.Parameters)
                         if (parametre.Value == null)
                             parametre.Value = DBNull.Value;
                        
                    IDCP = Convert.ToInt32(cmd.ExecuteScalar());
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.Transaction = transaction;
                    cmd1.Connection = transaction.Connection;
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "Mobile_AttachmentClient_Photos";
                    if (clientprospecter.files != null)
                    {
                        foreach (Photos value in clientprospecter.files)
                        {
                            cmd1.Parameters.AddWithValue("@Src", value.PicID);
                            cmd1.Parameters.AddWithValue("@Emplacement", value.Pic);
                            cmd1.Parameters.AddWithValue("@ID_Client ", IDCP);
                            cmd1.ExecuteNonQuery();
                            cmd1.Parameters.Clear();
                        }
                    }
                    insert(transaction, IDCP,clientprospecter.adresse);
                    transaction.Commit();
                   return "success";
                    

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return "error"+ex;

                }


            }
        }
        public void insert (SqlTransaction transaction,int id,string addr){
               
        try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Mobile_Client_ClientAdresse";
                cmd.Parameters.AddWithValue("@NTIERS", id);
                cmd.Parameters.AddWithValue("@LibAdresse", addr);
                cmd.ExecuteNonQuery();
                }
              catch (Exception)
            {
                throw;
            }
        }
    }
}