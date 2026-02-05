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

namespace CST.LePoint.Intervention.Metier
{
    public class ChantierBC : Item
    {
        #region Propriétés

        [XmlAttribute("NChantier")]
        [Bindable(true)]
        public string NChantier { get; set; }

        [XmlAttribute("NBonCommande")]
        [Bindable(true)]
        public string NBonCommande { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }


        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        #endregion Propriétés

        public ChantierBC()
        {

        }



        public void Sauvegarder(SqlTransaction transaction)
        {


            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_ChantierBC_Sauvegarder";

                cmd.Parameters.AddWithValue("@NChantier", NChantier);
                cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);


                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);

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

        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
            }
        }




        public static ChantierBC Charger(string NChantier)
        {
            ChantierBC chantierBC = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_ChantierBC_Charger";
                    cmd.Parameters.AddWithValue("@NChantier", NChantier);
                    //cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            chantierBC = new ChantierBC();

                            chantierBC.Code = dr["NChantier"].ToString();
                            chantierBC.NBonCommande = dr["NBonCommande"].ToString();
                            chantierBC.CClient = dr["NBonCommande"].ToString();


                            if (dr["Libelle"] != DBNull.Value)
                                chantierBC.Libelle = dr["RaisonSociale"].ToString();
                            
                           

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return chantierBC;
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
                    cmd.CommandText = "GP_ChantierBC_Supprimer";
                    cmd.Parameters.AddWithValue("@NChantier", NChantier);
                    cmd.Parameters.AddWithValue("@NBonCommande", NBonCommande);

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


    }

    public class ChantierBCCollection : ItemCollection
    {

        public static ChantierBCCollection Charger()
        {
            ChantierBCCollection collection = new ChantierBCCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_ChantierBC_Charger";
                    cmd.Parameters.AddWithValue("@NChantier", DBNull.Value);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ChantierBC chantierBC = new ChantierBC();

                            chantierBC.Code = dr["NChantier"].ToString();
                            chantierBC.NBonCommande = dr["NBonCommande"].ToString();
                            chantierBC.CClient = dr["NBonCommande"].ToString();


                            if (dr["Libelle"] != DBNull.Value)
                                chantierBC.Libelle = dr["RaisonSociale"].ToString();
                            

                            collection.Add(chantierBC);
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