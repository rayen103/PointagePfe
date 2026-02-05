using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Vente.Metier
{
    public class BonChargement
    {
        #region Proriétès

        [XmlAttribute("NBonChargement")]
        [Bindable(true)]
        public string NBonChargement { get; set; }

        [XmlAttribute("DateChargement")]
        [Bindable(true)]
        public DateTime DateChargement { get; set; }



        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public string CVehicule { get; set; }

        [XmlAttribute("Chauffeur")]
        [Bindable(true)]
        public string Chauffeur { get; set; }

        [XmlAttribute("PoidsTotal")]
        [Bindable(true)]
        public decimal PoidsTotal { get; set; }

        [XmlAttribute("BValide")]
        [Bindable(true)]
        public bool BValide { get; set; }


        [XmlAttribute("BAnnuler")]
        [Bindable(true)]
        public bool BAnnuler { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime? DateInsertion { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("PCModication")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        public BonChargementDetailCollection OrdresPreparation = new BonChargementDetailCollection();
        public BonChargementDetailArticleCollection DetailsArticle = new BonChargementDetailArticleCollection();

        #endregion Proriétès

        public BonChargement()
        {
            OrdresPreparation = new BonChargementDetailCollection();
            DetailsArticle = new BonChargementDetailArticleCollection();
        }

        public void Inserer()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Inserer(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonChargement_Inserer";

                cmd.Parameters.AddWithValue("@BValide", this.BValide);

                cmd.Parameters.AddWithValue("@DateChargement", this.DateChargement);
                cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonChargement = dr["NBonChargement"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                foreach (BonChargementDetail detail in this.OrdresPreparation)
                {
                    detail.NBonChargement = this.NBonChargement;
                    detail.Inserer(transaction);
                }
                this.DetailsArticle = BonChargementDetailArticleCollection.Charger(this.OrdresPreparation);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Annuler()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE BonChargement SET BAnnuler= 1 WHERE NBonChargement = '" + this.NBonChargement + "'";
                    cmd.ExecuteNonQuery();
                    this.BAnnuler = true;

                }
            }
            catch { throw; }
        }

        public void Valider()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE BonChargement SET BValide= 1 WHERE NBonChargement = '" + this.NBonChargement + "'";
                    cmd.ExecuteNonQuery();
                    this.BAnnuler = true;

                }
            }
            catch { throw; }
        }

        public void Invalider()
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE BonChargement SET BValide= 0 WHERE NBonChargement = '" + this.NBonChargement + "'";
                    cmd.ExecuteNonQuery();
                    this.BAnnuler = true;

                }
            }
            catch { throw; }
        }

        public void Modifier()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Modifier(transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void Modifier(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonChargement_Modifier";
                cmd.Parameters.AddWithValue("@NBonChargement", this.NBonChargement);

                cmd.Parameters.AddWithValue("@BValide", this.BValide);

                cmd.Parameters.AddWithValue("@DateChargement", this.DateChargement);
                cmd.Parameters.AddWithValue("@Chauffeur", this.Chauffeur);
                cmd.Parameters.AddWithValue("@CVehicule", this.CVehicule);
                cmd.Parameters.AddWithValue("@PoidsTotal", this.PoidsTotal);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                cmd.ExecuteNonQuery();
                this.SupprimerDetailBonChargementAnterieurs(transaction);
                foreach (BonChargementDetail detail in this.OrdresPreparation)
                {
                    detail.NBonChargement = this.NBonChargement;
                    detail.Inserer(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void SupprimerDetailBonChargementAnterieurs(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonChargement_SupprimerDetails";

                cmd.Parameters.AddWithValue("@NBonChargement", this.NBonChargement);

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

        public static BonChargement Charger(string nBonChargement)
        {
            BonChargement bonChargement = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonChargement_Charger";
                    cmd.Parameters.AddWithValue("@NBonChargement", nBonChargement);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonChargement = new BonChargement();
                            bonChargement.NBonChargement = dr["NBonChargement"].ToString();
                            if (dr["BValide"] != DBNull.Value)
                                bonChargement.BValide = bool.Parse(dr["BValide"].ToString());
                            if (dr["BAnnuler"] != DBNull.Value)
                                bonChargement.BAnnuler = bool.Parse(dr["BAnnuler"].ToString());

                            if (dr["PoidsTotal"] != DBNull.Value)
                                bonChargement.PoidsTotal = decimal.Parse(dr["PoidsTotal"].ToString());
                            if (dr["DateChargement"] != DBNull.Value)
                                bonChargement.DateChargement = DateTime.Parse(dr["DateChargement"].ToString());

                            if (dr["Chauffeur"] != DBNull.Value)
                                bonChargement.Chauffeur = dr["Chauffeur"].ToString();


                            if (dr["CVehicule"] != DBNull.Value)
                                bonChargement.CVehicule = dr["CVehicule"].ToString();
                            if (dr["Indice"] != DBNull.Value)
                                bonChargement.Indice = int.Parse(dr["Indice"].ToString());
                            bonChargement.OrdresPreparation = BonChargementDetailCollection.Charger(nBonChargement);
                        }
                    }
                }
                bonChargement.DetailsArticle = BonChargementDetailArticleCollection.Charger(bonChargement.OrdresPreparation);
            }
            catch (Exception)
            {
                throw;
            }

            return bonChargement;
        }

        public static DataTable VerifieStock(string nBonChargement)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonChargement_VerifieStock";
                    cmd.Parameters.AddWithValue("@NBonChargement", nBonChargement);
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    ad.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
    }
}