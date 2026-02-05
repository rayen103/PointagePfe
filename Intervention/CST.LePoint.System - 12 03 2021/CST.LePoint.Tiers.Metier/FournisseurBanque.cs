using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Tiers.Metier
{
    public class FournisseurBanqueCollection : List<FournisseurBanque>
    {
        public FournisseurBanqueCollection()
        {
        }

        public static FournisseurBanqueCollection Charger(string cFournisseur)
        {
            FournisseurBanqueCollection collection = new FournisseurBanqueCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurBanque_Charger";

                    cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                    cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                    cmd.Parameters.AddWithValue("@Rib", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            FournisseurBanque fournisseurBanque = new FournisseurBanque();

                            fournisseurBanque.CBanque = dr["CBanque"].ToString();
                            fournisseurBanque.CFournisseur = dr["CFournisseur"].ToString();
                            fournisseurBanque.Rib = dr["Rib"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                fournisseurBanque.Agence = dr["Agence"].ToString();
                            collection.Add(fournisseurBanque);
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

        public static DataSet ChargerVue()
        {
            DataSet ds = new DataSet();

            using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "RptFournisseurBanque_Charger";
                cmd.Parameters.AddWithValue("@CFournisseur", DBNull.Value);
                cmd.Parameters.AddWithValue("@CBanque", DBNull.Value);
                cmd.Parameters.AddWithValue("@Rib", DBNull.Value);

                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "RptFournisseurBanque_Charger");
            }
            return (ds);
        }
    }

    [Serializable]
    public class FournisseurBanque
    {
        #region Propriétés

        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("Rib")]
        [Bindable(true)]
        public string Rib { get; set; }

        [XmlAttribute("Agence")]
        [Bindable(true)]
        public string Agence { get; set; }

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

        #endregion Propriétés

        public FournisseurBanque()
        {
            //this.DateInsertion = DateTime.Now;
            //this.DateModification = DateTime.Now;
        }

        public void Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FournisseurBanque_Sauvegarder";
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@Rib", Rib);
                cmd.Parameters.AddWithValue("@Agence", Agence);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

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

        public static FournisseurBanque Charger(string cBanque, string cFournisseur, string Rib)
        {
            FournisseurBanque FournisseurBanque = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "FournisseurBanque_Charger";
                    cmd.Parameters.AddWithValue("@CBanque", cBanque);
                    cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                    cmd.Parameters.AddWithValue("@Rib", Rib);
                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            FournisseurBanque = new FournisseurBanque();

                            FournisseurBanque.CBanque = dr["CBanque"].ToString();
                            FournisseurBanque.CFournisseur = dr["CFournisseur"].ToString();
                            FournisseurBanque.Rib = dr["Rib"].ToString();
                            if (dr["Agence"] != DBNull.Value)
                                FournisseurBanque.Agence = dr["Agence"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return FournisseurBanque;
        }

        public void Supprimer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FournisseurBanque_Supprimer";
                cmd.Parameters.AddWithValue("@CBanque", CBanque);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@Rib", Rib);

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
    }
}