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
   public class RattachementTaches : Item
    {

        #region Propriétés

        [XmlAttribute("NRattachement")]
        [Bindable(true)]
       public string NRattachement { get; set; }

        [XmlAttribute("CArticle")]
        [Bindable(true)]
        public string CArticle { get; set; }
        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("Libelle")]
        [Bindable(true)]
        public string Libelle { get; set; }

        [XmlAttribute("Matricule")]
        [Bindable(true)]
        public string Matricule { get; set; }

        [XmlAttribute("NomPrenom")]
        [Bindable(true)]
        public string NomPrenom { get; set; }

        [XmlAttribute("DateDebut")]
        [Bindable(true)]
        public DateTime? DateDebut { get; set; }

        [XmlAttribute("HeureDebut")]
        [Bindable(true)]
        public string HeureDebut { get; set; }

        [XmlAttribute("DateFin")]
        [Bindable(true)]
        public DateTime? DateFin { get; set; }

        [XmlAttribute("HeureFin")]
        [Bindable(true)]
        public string HeureFin { get; set; }

        [XmlAttribute("NombreHeure")]
        [Bindable(true)]
        public decimal NombreHeure { get; set; }

        [XmlAttribute("Cout")]
        [Bindable(true)]
        public decimal Cout { get; set; }

        [XmlAttribute("Ordre")]
        [Bindable(true)]
        public int Ordre { get; set; }

        [XmlAttribute("FodecArticle")]
        [Bindable(true)]
        public decimal FodecArticle { get; set; }

        [XmlAttribute("CTaxe")]
        [Bindable(true)]
        public string CTaxe { get; set; }

        [XmlAttribute("TauxTVA")]
        [Bindable(true)]
        public decimal TauxTVA { get; set; }

        [XmlAttribute("PrixUN")]
        [Bindable(true)]
        public decimal PrixUN { get; set; }

        [XmlAttribute("PrixENP")]
        [Bindable(true)]
        public decimal PrixENP { get; set; }

        [XmlAttribute("CUNT")]
        [Bindable(true)]
        public string CUNT { get; set; }

        [XmlAttribute("TypeRattachement")]
        [Bindable(true)]
        public string TypeRattachement { get; set; }

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
        
       [XmlAttribute("Quantite")]
        [Bindable(true)]
        public int Quantite { get; set; }
        [XmlAttribute("PrixRevient")]
        [Bindable(true)]
       public decimal PrixRevient { get; set; }


        #endregion Propriétés

        public RattachementTaches()
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
                cmd.CommandText = "GP_RattachementTaches_Sauvegarder";

                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                cmd.Parameters.AddWithValue("@CArticle", CArticle);

                cmd.Parameters.AddWithValue("@Libelle", Libelle);
                cmd.Parameters.AddWithValue("@Matricule", Matricule);
                cmd.Parameters.AddWithValue("@NomPrenom", NomPrenom);
                cmd.Parameters.AddWithValue("@DateDebut", DateDebut);
                cmd.Parameters.AddWithValue("@HeureDebut", HeureDebut);
                cmd.Parameters.AddWithValue("@DateFin", DateFin);
                cmd.Parameters.AddWithValue("@HeureFin", HeureFin);
                cmd.Parameters.AddWithValue("@NombreHeure", NombreHeure);
                cmd.Parameters.AddWithValue("@Cout", Cout);
                cmd.Parameters.AddWithValue("@Ordre", Ordre);
                cmd.Parameters.AddWithValue("@FodecArticle", FodecArticle);
                cmd.Parameters.AddWithValue("@CTaxe", CTaxe);
                cmd.Parameters.AddWithValue("@TauxTVA", TauxTVA);
                cmd.Parameters.AddWithValue("@PrixUN", PrixUN);
                cmd.Parameters.AddWithValue("@PrixENP", PrixENP);
                cmd.Parameters.AddWithValue("@CUNT", CUNT);
                cmd.Parameters.AddWithValue("@TypeRattachement", TypeRattachement);

                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@Quantite", this.Quantite);

                cmd.Parameters.AddWithValue("@PrixRevient", this.PrixRevient);


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

        public static RattachementTaches Charger(string NRattachement, string CArticle)
        {
            RattachementTaches rattachementTaches = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "GI_InterventionTaches_Charger";
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);


                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            rattachementTaches = new RattachementTaches();

                            rattachementTaches.NRattachement = dr["NRattachement"].ToString();
                            rattachementTaches.CArticle = dr["CArticle"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                rattachementTaches.Libelle = dr["Libelle"].ToString();
                            if (dr["Matricule"] != DBNull.Value)
                                rattachementTaches.Matricule = dr["Matricule"].ToString();
                            if (dr["NomPrenom"] != DBNull.Value)
                                rattachementTaches.NomPrenom = dr["NomPrenom"].ToString();
                            if (dr["DateDebut"] != DBNull.Value)
                                rattachementTaches.DateDebut = DateTime.Parse(dr["DateDebut"].ToString());
                            if (dr["HeureDebut"] != DBNull.Value)
                                rattachementTaches.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["DateFin"] != DBNull.Value)
                                rattachementTaches.DateFin = DateTime.Parse(dr["DateFin"].ToString());
                            if (dr["HeureFin"] != DBNull.Value)
                                rattachementTaches.HeureFin = dr["HeureFin"].ToString();
                            if (dr["NombreHeure"] != DBNull.Value)
                                rattachementTaches.NombreHeure = int.Parse(dr["NombreHeure"].ToString());
                            if (dr["Cout"] != DBNull.Value)
                                rattachementTaches.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                rattachementTaches.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["FodecArticle"] != DBNull.Value)
                                rattachementTaches.FodecArticle = decimal.Parse(dr["FodecArticle"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                rattachementTaches.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                rattachementTaches.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["PrixUN"] != DBNull.Value)
                                rattachementTaches.PrixUN = decimal.Parse(dr["PrixUN"].ToString());
                            if (dr["PrixENP"] != DBNull.Value)
                                rattachementTaches.PrixENP = decimal.Parse(dr["PrixENP"].ToString());
                            if (dr["CUNT"] != DBNull.Value)
                                rattachementTaches.CUNT = dr["CUNT"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                rattachementTaches.TypeRattachement = dr["TypeRattachement"].ToString();

                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return rattachementTaches;
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
                    cmd.CommandText = "GI_InterventionTaches_Supprimer";
                    cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                    cmd.Parameters.AddWithValue("@CArticle", CArticle);


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

    public class RattachementTachesCollection  : ItemCollection
    {
        public static RattachementTachesCollection Charger()
        {
            RattachementTachesCollection collection = new RattachementTachesCollection();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "GI_InterventionTaches_Charger";

                    cmd.Parameters.AddWithValue("@CArticle", DBNull.Value);
                    cmd.Parameters.AddWithValue("@NRattachement", DBNull.Value);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            RattachementTaches interventionTaches = new RattachementTaches();

                            interventionTaches.NRattachement = dr["NRattachement"].ToString();
                            interventionTaches.CArticle = dr["CArticle"].ToString();

                            if (dr["Libelle"] != DBNull.Value)
                                interventionTaches.Libelle = dr["Libelle"].ToString();
                            if (dr["Matricule"] != DBNull.Value)
                                interventionTaches.Matricule = dr["Matricule"].ToString();
                            if (dr["NomPrenom"] != DBNull.Value)
                                interventionTaches.NomPrenom = dr["NomPrenom"].ToString();
                            if (dr["DateDebut"] != DBNull.Value)
                                interventionTaches.DateDebut = DateTime.Parse(dr["DateDebut"].ToString());
                            if (dr["HeureDebut"] != DBNull.Value)
                                interventionTaches.HeureDebut = dr["HeureDebut"].ToString();
                            if (dr["DateFin"] != DBNull.Value)
                                interventionTaches.DateFin = DateTime.Parse(dr["DateFin"].ToString());
                            if (dr["HeureFin"] != DBNull.Value)
                                interventionTaches.HeureFin = dr["HeureFin"].ToString();
                            if (dr["NombreHeure"] != DBNull.Value)
                                interventionTaches.NombreHeure = int.Parse(dr["NombreHeure"].ToString());
                            if (dr["Cout"] != DBNull.Value)
                                interventionTaches.Cout = decimal.Parse(dr["Cout"].ToString());
                            if (dr["Ordre"] != DBNull.Value)
                                interventionTaches.Ordre = int.Parse(dr["Ordre"].ToString());
                            if (dr["FodecArticle"] != DBNull.Value)
                                interventionTaches.FodecArticle = decimal.Parse(dr["FodecArticle"].ToString());
                            if (dr["CTaxe"] != DBNull.Value)
                                interventionTaches.CTaxe = dr["CTaxe"].ToString();
                            if (dr["TauxTVA"] != DBNull.Value)
                                interventionTaches.TauxTVA = decimal.Parse(dr["TauxTVA"].ToString());
                            if (dr["PrixUN"] != DBNull.Value)
                                interventionTaches.PrixUN = decimal.Parse(dr["PrixUN"].ToString());
                            if (dr["PrixENP"] != DBNull.Value)
                                interventionTaches.PrixENP = decimal.Parse(dr["PrixENP"].ToString());
                            if (dr["CUNT"] != DBNull.Value)
                                interventionTaches.CUNT = dr["CUNT"].ToString();
                            if (dr["TypeRattachement"] != DBNull.Value)
                                interventionTaches.TypeRattachement = dr["TypeRattachement"].ToString();
                            

                            collection.Add(interventionTaches);
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
