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


        [Serializable]
        public class Chantier : Item
        {
            #region Propriétés

            [XmlAttribute("NChantier")]
            [Bindable(true)]
            public string NChantier { get; set; }

            [XmlAttribute("Observation")]
            [Bindable(true)]
            public string Observation { get; set; }

            [XmlAttribute("CClient")]
            [Bindable(true)]
            public string CClient { get; set; }

            [XmlAttribute("RaisonSociale")]
            [Bindable(true)]
            public string RaisonSociale { get; set; }

            [XmlAttribute("Adresse")]
            [Bindable(true)]
            public string Adresse { get; set; }
            [XmlAttribute("MontantBC")]
            [Bindable(true)]
            public decimal MontantBC { get; set; }

            [XmlAttribute("MontantBL")]
            [Bindable(true)]
            public decimal MontantBL { get; set; }

            [XmlAttribute("DateCreation")]
            [Bindable(true)]
            public DateTime DateCreation { get; set; }
            [XmlAttribute("Nature")]
            [Bindable(true)]
            public string Nature { get; set; }
            [XmlAttribute("Responsable")]
            [Bindable(true)]
            public string Responsable { get; set; }

            public ChantierBCCollection ChantierBCs = new ChantierBCCollection();
           
            
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

            [XmlAttribute("BCloture")]
            [Bindable(true)]
            public bool BCloture { get; set; }

            [XmlAttribute("DateCloture")]
            [Bindable(true)]
            public DateTime DateCloture { get; set; }

            [XmlAttribute("DatePrevuCloture")]
            [Bindable(true)]
            public DateTime DatePrevuCloture { get; set; }

            #endregion Propriétés

            public Chantier()
            {
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
                        cmd.CommandText = "GP_Chantier_Sauvegarder";

                        cmd.Parameters.AddWithValue("@NChantier", this.NChantier);
                        cmd.Parameters.AddWithValue("@Observation", this.Observation);
                        cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                        cmd.Parameters.AddWithValue("@CClient", CClient);
                        cmd.Parameters.AddWithValue("@Adresse", Adresse);
                        cmd.Parameters.AddWithValue("@MontantBC", MontantBC);
                        cmd.Parameters.AddWithValue("@MontantBL", MontantBL);
                        cmd.Parameters.AddWithValue("@DateCreation", DateCreation);
                        cmd.Parameters.AddWithValue("@Nature", Nature);
                        cmd.Parameters.AddWithValue("@Responsable", Responsable);
                        cmd.Parameters.AddWithValue("@CreePar", CreePar);
                        cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                        cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                        cmd.Parameters.AddWithValue("@PCModification", PCModification);
                        cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                        cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);

                        cmd.Parameters.AddWithValue("@BCloture", BCloture);
                        cmd.Parameters.AddWithValue("@DateCloture", DateCloture);
                        cmd.Parameters.AddWithValue("@DatePrevuCloture", DatePrevuCloture);
                        ///
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


            private void SupprimerChantierBCAnterieurs(SqlTransaction transaction)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Transaction = transaction;
                    cmd.Connection = transaction.Connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Chantier_SupprimerBCDetails";

                    cmd.Parameters.AddWithValue("@NChantier", this.NChantier);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }





            public void Supprimer()
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GP_Chantier_Supprimer";
                        cmd.Parameters.AddWithValue("@NChantier", NChantier);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

            public static Chantier Charger(string nChantier)
            {
                Chantier chantier = null;

                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "GP_Chantier_Charger";
                        cmd.Parameters.AddWithValue("@NChantier", nChantier);
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
                                chantier = new Chantier();
                                chantier.NChantier = dr["NChantier"].ToString();
                                chantier.CClient = dr["CClient"].ToString();
                                chantier.RaisonSociale = dr["RaisonSociale"].ToString(); ;
                                if (dr["Observation"] != DBNull.Value)
                                    chantier.Observation = dr["Observation"].ToString();
                                if (dr["Adresse"] != DBNull.Value)
                                    chantier.Adresse = dr["Adresse"].ToString();
                                if (dr["MontantBC"] != DBNull.Value)
                                    chantier.MontantBC = decimal.Parse(dr["MontantBC"].ToString());
                                if (dr["MontantBL"] != DBNull.Value)
                                    chantier.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                                if (dr["DateCreation"] != DBNull.Value)
                                    chantier.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                                if (dr["Nature"] != DBNull.Value)
                                    chantier.Nature = dr["Nature"].ToString();
                                if (dr["Responsable"] != DBNull.Value)
                                    chantier.Responsable = dr["Responsable"].ToString();
                                if (dr["BCloture"] != DBNull.Value)
                                    chantier.BCloture = bool.Parse(dr["BCloture"].ToString());
                                if (dr["DateCloture"] != DBNull.Value)
                                    chantier.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                                if (dr["DatePrevuCloture"] != DBNull.Value)
                                    chantier.DatePrevuCloture = DateTime.Parse(dr["DatePrevuCloture"].ToString());
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                return chantier;
            }
        }



        [Serializable]
        public class ChantierCollection : ItemCollection
        {
           

            
           


            public static ChantierCollection Charger()
            {
                ChantierCollection collection = new ChantierCollection();
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Chantier_Charger";
                    cmd.Parameters.AddWithValue("@NChantier", DBNull.Value);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Chantier chantier = new Chantier();
                        chantier.NChantier = dr["NChantier"].ToString();
                        chantier.CClient = dr["CClient"].ToString();
                        chantier.RaisonSociale = dr["RaisonSociale"].ToString(); ;
                        if (dr["Observation"] != DBNull.Value)
                            chantier.Observation = dr["Observation"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            chantier.Adresse = dr["Adresse"].ToString();
                        if (dr["MontantBC"] != DBNull.Value)
                            chantier.MontantBC = decimal.Parse(dr["MontantBC"].ToString());
                        if (dr["MontantBL"] != DBNull.Value)
                            chantier.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                        if (dr["DateCreation"] != DBNull.Value)
                            chantier.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                        if (dr["Nature"] != DBNull.Value)
                            chantier.Nature = dr["Nature"].ToString();
                        if (dr["Responsable"] != DBNull.Value)
                            chantier.Responsable = dr["Responsable"].ToString();
                        if (dr["BCloture"] != DBNull.Value)
                            chantier.BCloture = bool.Parse(dr["BCloture"].ToString());
                        if (dr["DateCloture"] != DBNull.Value)
                            chantier.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                        if (dr["DatePrevuCloture"] != DBNull.Value)
                            chantier.DatePrevuCloture = DateTime.Parse(dr["DatePrevuCloture"].ToString());

                        collection.Add(chantier);
                    }
                    dr.Close();

                    return (collection);
                }
            }

            public static ChantierCollection Charger(string cclient)
            {
                ChantierCollection collection = new ChantierCollection();
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Chantier_Chargerparclient";
                    cmd.Parameters.AddWithValue("@CClient", cclient);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Chantier chantier = new Chantier();
                        chantier.NChantier = dr["NChantier"].ToString();
                        chantier.CClient = dr["CClient"].ToString();
                        chantier.RaisonSociale = dr["RaisonSociale"].ToString(); ;
                        if (dr["Observation"] != DBNull.Value)
                            chantier.Observation = dr["Observation"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            chantier.Adresse = dr["Adresse"].ToString();
                        if (dr["MontantBC"] != DBNull.Value)
                            chantier.MontantBC = decimal.Parse(dr["MontantBC"].ToString());
                        if (dr["MontantBL"] != DBNull.Value)
                            chantier.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                        if (dr["DateCreation"] != DBNull.Value)
                            chantier.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                        if (dr["Nature"] != DBNull.Value)
                            chantier.Nature = dr["Nature"].ToString();
                        if (dr["Responsable"] != DBNull.Value)
                            chantier.Responsable = dr["Responsable"].ToString();
                        if (dr["BCloture"] != DBNull.Value)
                            chantier.BCloture = bool.Parse(dr["BCloture"].ToString());
                        if (dr["DateCloture"] != DBNull.Value)
                            chantier.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                        if (dr["DatePrevuCloture"] != DBNull.Value)
                            chantier.DatePrevuCloture = DateTime.Parse(dr["DatePrevuCloture"].ToString());

                        collection.Add(chantier);
                    }
                    dr.Close();

                    return (collection);
                }
            }
            public static ChantierCollection ChargerparChantier(string Nchantier)
            {
                ChantierCollection collection = new ChantierCollection();
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_Chantier_ChargerparChantier";
                    cmd.Parameters.AddWithValue("@Nchantier", Nchantier);
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Chantier chantier = new Chantier();
                        chantier.NChantier = dr["NChantier"].ToString();
                        chantier.CClient = dr["CClient"].ToString();
                        chantier.RaisonSociale = dr["RaisonSociale"].ToString(); ;
                        if (dr["Observation"] != DBNull.Value)
                            chantier.Observation = dr["Observation"].ToString();
                        if (dr["Adresse"] != DBNull.Value)
                            chantier.Adresse = dr["Adresse"].ToString();
                        if (dr["MontantBC"] != DBNull.Value)
                            chantier.MontantBC = decimal.Parse(dr["MontantBC"].ToString());
                        if (dr["MontantBL"] != DBNull.Value)
                            chantier.MontantBL = decimal.Parse(dr["MontantBL"].ToString());
                        if (dr["DateCreation"] != DBNull.Value)
                            chantier.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                        if (dr["Nature"] != DBNull.Value)
                            chantier.Nature = dr["Nature"].ToString();
                        if (dr["Responsable"] != DBNull.Value)
                            chantier.Responsable = dr["Responsable"].ToString();
                        if (dr["BCloture"] != DBNull.Value)
                            chantier.BCloture = bool.Parse(dr["BCloture"].ToString());
                        if (dr["DateCloture"] != DBNull.Value)
                            chantier.DateCloture = DateTime.Parse(dr["DateCloture"].ToString());
                        if (dr["DatePrevuCloture"] != DBNull.Value)
                            chantier.DatePrevuCloture = DateTime.Parse(dr["DatePrevuCloture"].ToString());

                        collection.Add(chantier);
                    }
                    dr.Close();

                    return (collection);
                }
            }
        }


    }
   

