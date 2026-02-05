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

namespace CST.LePoint.VenteMobile.Metier
{
    public class MobileRattachementArticle
    {

        #region Propriétés

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }

        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }
        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }
        [XmlAttribute("NChantier")]
        [Bindable(true)]
        public string NChantier { get; set; }
        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }
        
        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }
        
        [XmlAttribute("Quantite")]
        [Bindable(true)]
        public decimal Quantite { get; set; }

        [XmlAttribute("QuantiteOTRattachement")]
        [Bindable(true)]
        public decimal QuantiteOTRattachement { get; set; }
        
        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
        public decimal PrixRevient { get; set; }
        
        [XmlAttribute("FodecArticle")]
        [Bindable(true)]
        public decimal FodecArticle { get; set; }
        
        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }
        
        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }
        
        [XmlAttribute("CUnite")]
        [Bindable(true)]
        public string CUnite { get; set; }

        [XmlAttribute("TypeRattachement")]
        [Bindable(true)]
        public string TypeRattachement { get; set; }
        
        //[XmlAttribute("NombreHeure")]
        //[Bindable(true)]
        //public decimal NombreHeure { get; set; }
        
        [XmlAttribute("NBonLivraison")]
        [Bindable(true)]
        public int NBonLivraison { get; set; }
        
        [XmlAttribute("DateBonLivraison")]
        [Bindable(true)]
        public DateTime? DateBonLivraison { get; set; }
        
        [XmlAttribute("Revient")]
        [Bindable(true)]
        public decimal Revient { get; set; }
        
        [XmlAttribute("NSerie")]
        [Bindable(true)]
        public string NSerie { get; set; }
        
        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

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

        [XmlAttribute("Image_Article")]
        [Bindable(true)]
        public byte[] Image_Article { get; set; }

        [XmlAttribute("CNoteRattachement")]
        [Bindable(true)]
        public string CNoteRattachement { get; set; }


    
       
        #endregion Propriétés

        public MobileRattachementArticle()
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
                cmd.CommandText = "MobileGP_RattachementArticle_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@Libelle", Libelle);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@CEntrepot", "");

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

        public void CRM_Sauvegarder(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "MobileCRMGP_RattachementArticle_Sauvegarder";
                cmd.Parameters.AddWithValue("@CArticle", CArticle);
                cmd.Parameters.AddWithValue("@Libelle", Libelle);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@CEntrepot", "");
                cmd.Parameters.AddWithValue("@COptions", this.CNoteRattachement);

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
   
        public void Sauvegarder()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                Sauvegarder(transaction);
            }
        }

        public static MobileRattachementArticle Charger(string CArticle, string NRattachement)
        {
            MobileRattachementArticle rattachementArticle = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GP_RattachementArticle_Charger";
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachementArticle = new MobileRattachementArticle();

                            rattachementArticle.CArticle = dr["CArticle"].ToString();
                            rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            rattachementArticle.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            rattachementArticle.NChantier = dr["NChantier"].ToString();
                            rattachementArticle.CClient = dr["CClient"].ToString();
                            rattachementArticle.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementArticle.Libelle = dr["Libelle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                rattachementArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteOTRattachement"] != DBNull.Value)
                                rattachementArticle.QuantiteOTRattachement = decimal.Parse(dr["QuantiteOTRattachement"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                rattachementArticle.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["NRattachement"] != DBNull.Value)
                                rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            if (dr["FodecArticle"] != DBNull.Value)
                                rattachementArticle.FodecArticle = decimal.Parse(dr["FodecArticle"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                rattachementArticle.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                rattachementArticle.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                rattachementArticle.CUnite = dr["CUnite"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementArticle.TypeRattachement = dr["TypeRattachement"].ToString();
                            //if (dr["NombreHeure"] != DBNull.Value)
                            //    rattachementArticle.NombreHeure = decimal.Parse(dr["NombreHeure"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachementArticle.NBonLivraison = int.Parse(dr["NBonLivraison"].ToString());
                            if (dr["DateBonLivraison"] != DBNull.Value)
                                rattachementArticle.DateBonLivraison = DateTime.Parse(dr["DateBonLivraison"].ToString());
                            if (dr["Revient"] != DBNull.Value)
                                rattachementArticle.Revient = decimal.Parse(dr["Revient"].ToString());
                            if (dr["NSerie"] != DBNull.Value)
                                rattachementArticle.NSerie = dr["NSerie"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                rattachementArticle.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Image_Article"] != DBNull.Value)
                                rattachementArticle.Image_Article = (byte[])dr["Image_Article"];
               
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rattachementArticle;
        }

        public void Supprimer(string NRattachement)
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
                    cmd.CommandText = "GP_RattachementArticle_Supprimer";
                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

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

        public void Supprimer(SqlTransaction transaction, string NRattachement)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GP_RattachementArticle_Supprimer";
                cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }

        }

        //public static void modifierQuantiteOTRattachement(int qtéotrattachement, string article,string not)
        //{

        //    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
        //    {
        //        cn.Open();
        //        SqlTransaction transaction = cn.BeginTransaction();
        //        try
        //        {
        //            SqlCommand cmd = new SqlCommand();
        //            cmd.Transaction = transaction;
        //            cmd.Connection = transaction.Connection;
        //            cmd.CommandType = CommandType.Text;
                   
        //            cmd.CommandText = "update GP_RattachementArticle set QuantiteOTRattachement = '" + qtéotrattachement + "'   where NRattachement = '" + nr + "' and CArticle = '" + article + "'";
        //            cmd.ExecuteNonQuery();
        //            transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
    }

    public class MobileRattachementArticleCollection : List<MobileRattachementArticle>
    {

        public static MobileRattachementArticleCollection Charger()
        {
            MobileRattachementArticleCollection collection = new MobileRattachementArticleCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_RattachementArticle_Charger";

                    cmd.Parameters.AddWithValue("@CArticle",  DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileRattachementArticle rattachementArticle = new MobileRattachementArticle();

                            rattachementArticle.CArticle = dr["CArticle"].ToString();
                            rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            rattachementArticle.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            rattachementArticle.NChantier = dr["NChantier"].ToString();
                            rattachementArticle.CClient = dr["CClient"].ToString();
                            rattachementArticle.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementArticle.Libelle = dr["Libelle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                rattachementArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteOTRattachement"] != DBNull.Value)
                                rattachementArticle.QuantiteOTRattachement = decimal.Parse(dr["QuantiteOTRattachement"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                rattachementArticle.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["NRattachement"] != DBNull.Value)
                                rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            if (dr["FodecArticle"] != DBNull.Value)
                                rattachementArticle.FodecArticle = decimal.Parse(dr["FodecArticle"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                rattachementArticle.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                rattachementArticle.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                rattachementArticle.CUnite = dr["CUnite"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementArticle.TypeRattachement = dr["TypeRattachement"].ToString();
                            //if (dr["NombreHeure"] != DBNull.Value)
                            //    rattachementArticle.NombreHeure = decimal.Parse(dr["NombreHeure"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachementArticle.NBonLivraison = int.Parse(dr["NBonLivraison"].ToString());
                            if (dr["DateBonLivraison"] != DBNull.Value)
                                rattachementArticle.DateBonLivraison = DateTime.Parse(dr["DateBonLivraison"].ToString());
                            if (dr["Revient"] != DBNull.Value)
                                rattachementArticle.Revient = decimal.Parse(dr["Revient"].ToString());
                            if (dr["NSerie"] != DBNull.Value)
                                rattachementArticle.NSerie = dr["NSerie"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                rattachementArticle.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Image_Article"] != DBNull.Value)
                                rattachementArticle.Image_Article = (byte[])dr["Image_Article"];

                            collection.Add(rattachementArticle);
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

        public static MobileRattachementArticleCollection ChargerparNRattachement(String NRattachement)
        {
            MobileRattachementArticleCollection collection = new MobileRattachementArticleCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GP_RattachementArticle_ChargerparNRattachement";

                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            MobileRattachementArticle rattachementArticle = new MobileRattachementArticle();

                            rattachementArticle.CArticle = dr["CArticle"].ToString();
                            rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            rattachementArticle.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            rattachementArticle.NChantier = dr["NChantier"].ToString();
                            rattachementArticle.CClient = dr["CClient"].ToString();
                            rattachementArticle.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementArticle.Libelle = dr["Libelle"].ToString();
                            if (dr["Quantite"] != DBNull.Value)
                                rattachementArticle.Quantite = decimal.Parse(dr["Quantite"].ToString());
                            if (dr["QuantiteOTRattachement"] != DBNull.Value)
                                rattachementArticle.QuantiteOTRattachement = decimal.Parse(dr["QuantiteOTRattachement"].ToString());
                            if (dr["PrixRevient"] != DBNull.Value)
                                rattachementArticle.PrixRevient = decimal.Parse(dr["PrixRevient"].ToString());
                            if (dr["NRattachement"] != DBNull.Value)
                                rattachementArticle.NRattachement = dr["NRattachement"].ToString();
                            if (dr["FodecArticle"] != DBNull.Value)
                                rattachementArticle.FodecArticle = decimal.Parse(dr["FodecArticle"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                rattachementArticle.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                rattachementArticle.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["CUnite"] != DBNull.Value)
                                rattachementArticle.CUnite = dr["CUnite"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementArticle.TypeRattachement = dr["TypeRattachement"].ToString();
                            //if (dr["NombreHeure"] != DBNull.Value)
                            //    rattachementArticle.NombreHeure = decimal.Parse(dr["NombreHeure"].ToString());
                            if (dr["NBonLivraison"] != DBNull.Value)
                                rattachementArticle.NBonLivraison = int.Parse(dr["NBonLivraison"].ToString());
                            if (dr["DateBonLivraison"] != DBNull.Value)
                                rattachementArticle.DateBonLivraison = DateTime.Parse(dr["DateBonLivraison"].ToString());
                            if (dr["Revient"] != DBNull.Value)
                                rattachementArticle.Revient = decimal.Parse(dr["Revient"].ToString());
                            if (dr["NSerie"] != DBNull.Value)
                                rattachementArticle.NSerie = dr["NSerie"].ToString();
                            if (dr["Ordre"] != DBNull.Value)
                                rattachementArticle.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["Image_Article"] != DBNull.Value)
                                rattachementArticle.Image_Article = (byte[])dr["Image_Article"];


                            collection.Add(rattachementArticle);
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
