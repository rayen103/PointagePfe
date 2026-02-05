using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    [Serializable]
    public class ClientBanque
    {
        public string CBanque { get; set; }

        public string CClient { get; set; }

        public string Agence { get; set; }

        public string RIBClient { get; set; }

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

        [XmlAttribute("Compte")]
        [Bindable(true)]
        public string Compte { get; set; }

        [XmlAttribute("Cle")]
        [Bindable(true)]
        public string Cle { get; set; }

        public ClientBanque()
        {
            //this.DateInsertion = DateTime.Now;
            //this.DateModification = DateTime.Now;
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

                    cmd.CommandText = "ClientBanque_Sauvegarder";
                    cmd.Parameters.AddWithValue("@CClient", CClient);
                    cmd.Parameters.AddWithValue("@RIBClient", RIBClient);
                    cmd.Parameters.AddWithValue("@Agence", Agence);
                    cmd.Parameters.AddWithValue("@CBanque", CBanque);
                    cmd.Parameters.AddWithValue("@Compte", Compte);
                    cmd.Parameters.AddWithValue("@Cle", Cle);
                    cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                    cmd.Parameters.AddWithValue("@CreePar", CreePar);
                    cmd.Parameters.AddWithValue("@PCModification", PCModification);
                    cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                    cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                    cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

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

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ClientBanque_Sauvegarder";
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@RIBClient", RIBClient);
                cmd.Parameters.AddWithValue("@Agence", Agence);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@Compte", Compte);
                cmd.Parameters.AddWithValue("@Cle", Cle);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
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
                cmd.CommandText = "ClientBanque_Charger";
                cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                cmd.Parameters.AddWithValue("@CClient", DBNull.Value);
                cmd.Parameters.AddWithValue("@RIBClient", DBNull.Value);

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

        public static ClientBanque Charger(string cBanque, string cClient, string RIBClient)
        {
            ClientBanque ClientBanque = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ClientBanque_Charger";
                    cmd.Parameters.AddWithValue("@CBanque", cBanque);
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@RIBClient", RIBClient);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            ClientBanque = new ClientBanque();

                            if (dr["CBanque"] != DBNull.Value)
                                ClientBanque.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                ClientBanque.CClient = dr["CClient"].ToString();
                            if (dr["RIBClient"] != DBNull.Value)
                                ClientBanque.RIBClient = dr["RIBClient"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                ClientBanque.Agence = dr["Agence"].ToString();
                            if (dr["Compte"] != DBNull.Value)
                                ClientBanque.Compte = dr["Compte"].ToString();
                            if (dr["Cle"] != DBNull.Value)
                                ClientBanque.Cle = dr["Cle"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return ClientBanque;
        }

        public void Supprimer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Supprimer(transaction);
                transaction.Commit();
            }
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ClientBanque_Supprimer";
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@RIBClient", RIBClient);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    [Serializable]
    public class ClientBanqueCollection : List<ClientBanque>
    {
        public static ClientBanqueCollection Charger(string cClient)
        {
            ClientBanqueCollection Collection = new ClientBanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "ClientBanque_Charger";
                    cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CClient", cClient);
                    cmd.Parameters.AddWithValue("@RIBClient", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ClientBanque clientBanque = new ClientBanque();

                            if (dr["CBanque"] != DBNull.Value)
                                clientBanque.CBanque = dr["CBanque"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                clientBanque.CClient = dr["CClient"].ToString();
                            if (dr["RIBClient"] != DBNull.Value)
                                clientBanque.RIBClient = dr["RIBClient"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                clientBanque.Agence = dr["Agence"].ToString();
                            if (dr["Compte"] != DBNull.Value)
                                clientBanque.Compte = dr["Compte"].ToString();
                            if (dr["Cle"] != DBNull.Value)
                                clientBanque.Cle = dr["Cle"].ToString();
                            Collection.Add(clientBanque);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Collection;
        }
    }
}