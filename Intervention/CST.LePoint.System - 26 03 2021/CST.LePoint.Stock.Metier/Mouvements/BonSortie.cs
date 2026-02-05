using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace CST.LePoint.Stock.Metier
{
    [Serializable]
    public class BonSortie
    {
        #region Propriétés

        [XmlAttribute("NBonSortie")]
        [Bindable(true)]
        public string NBonSortie { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CChauffeur")]
        [Bindable(true)]
        public string CChauffeur { get; set; }

        //Besoin dans le cas ou le chauffeur est indefinit (BExterne = true)
        //si le chauffeur est defini ce champ contient Prenom + ' ' + Nom
        [XmlAttribute("LibChauffeur")]
        [Bindable(true)]
        public string LibChauffeur { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("TypeMouvement")]
        [Bindable(true)]
        public string TypeMouvement { get; set; }

        [XmlAttribute("CVehicule")]
        [Bindable(true)]
        public string CVehicule { get; set; }

        [XmlAttribute("MatriculeVoiture")]
        [Bindable(true)]
        public string MatriculeVoiture { get; set; }

        [XmlAttribute("NDocumentSource")]
        [Bindable(true)]
        public string NDocumentSource { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("DateSortie")]
        [Bindable(true)]
        public DateTime DateSortie { get; set; }

        [XmlAttribute("DateInsertion")]
        [Bindable(true)]
        public DateTime DateInsertion { get; set; }

        [XmlAttribute("PCInsertion")]
        [Bindable(true)]
        public string PCInsertion { get; set; }

        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }

        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime DateModification { get; set; }

        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }

        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }


        [XmlAttribute("NTelephone")]
        [Bindable(true)]
        public string NTelephone { get; set; }


        [XmlAttribute("Adresse")]
        [Bindable(true)]
        public string Adresse { get; set; }

        [XmlAttribute("MatriculeFiscale")]
        [Bindable(true)]
        public string MatriculeFiscale { get; set; }

        [XmlAttribute("NRattachement")]
        [Bindable(true)]
        public string NRattachement { get; set; }

        public BonSortieDetailCollection BonSortieDetailCollection;
        public Boolean Existance;

        #endregion Propriétés

        public BonSortie()
        {
            this.NBonSortie = string.Empty;
            this.CEntrepot = string.Empty;
            this.BonSortieDetailCollection = new BonSortieDetailCollection();
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
                cmd.CommandText = "BonSortie_Inserer";
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateSortie", DateSortie);
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@TypeMouvement", TypeMouvement);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@MatriculeVoiture", MatriculeVoiture);
                cmd.Parameters.AddWithValue("@NDocumentSource", NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                cmd.Parameters.AddWithValue("@Exercice", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);

                cmd.Parameters.AddWithValue("@NTelephone", NTelephone);
                cmd.Parameters.AddWithValue("@Adresse", Adresse);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", MatriculeFiscale);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        NBonSortie = dr["NBonSortie"].ToString();
                        Indice = int.Parse(dr["dernierIndice"].ToString());
                    }
                }
                int i = 0;
                foreach (BonSortieDetail bonSortieDetail in BonSortieDetailCollection)
                {
                    bonSortieDetail.NBonSortie = NBonSortie;
                    bonSortieDetail.Ordre = i++;
                    bonSortieDetail.CreePar = this.CreePar;
                    bonSortieDetail.PCInsertion = this.PCInsertion;
                    bonSortieDetail.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void InsererViaRattachement()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    InsererViaRattachement(transaction);
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void InsererViaRattachement(SqlTransaction transaction)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortie_InsererViaRattachement";
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateSortie", DateSortie);
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@TypeMouvement", TypeMouvement);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@MatriculeVoiture", MatriculeVoiture);
                cmd.Parameters.AddWithValue("@NDocumentSource", NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                cmd.Parameters.AddWithValue("@Exercice", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@NTelephone", NTelephone);
                cmd.Parameters.AddWithValue("@Adresse", Adresse);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", MatriculeFiscale);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);

                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        NBonSortie = dr["NBonSortie"].ToString();
                        Indice = int.Parse(dr["dernierIndice"].ToString());
                    }
                }
                int i = 0;
                foreach (BonSortieDetail bonSortieDetail in BonSortieDetailCollection)
                {
                    bonSortieDetail.NBonSortie = NBonSortie;
                    bonSortieDetail.Ordre = i++;
                    bonSortieDetail.CreePar = this.CreePar;
                    bonSortieDetail.PCInsertion = this.PCInsertion;
                    bonSortieDetail.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        public void InsererDon()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    InsererDon(transaction);
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public void InsererDon(SqlTransaction transaction)
        {
            NBonSortie = BonSortie.ChargerCodeBonSortieDon(transaction, this.Exercice);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortieDon_Inserer";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateSortie", DateSortie);
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@TypeMouvement", TypeMouvement);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@MatriculeVoiture", MatriculeVoiture);
                cmd.Parameters.AddWithValue("@NDocumentSource", NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                cmd.Parameters.AddWithValue("@Exercice", Exercice);
                cmd.Parameters.AddWithValue("@PCInsertion", PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", CreePar);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@NTelephone", NTelephone);
                cmd.Parameters.AddWithValue("@Adresse", Adresse);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", MatriculeFiscale);


                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                //using (SqlDataReader dr = cmd.ExecuteReader())
                //{
                //    if (dr.Read())
                //    {
                //        NBonSortie = dr["NBonSortie"].ToString();
                //       // Indice = int.Parse(dr["dernierIndice"].ToString());
                //    }
                //}
                int i = 0;
                foreach (BonSortieDetail bonSortieDetail in BonSortieDetailCollection)
                {
                    bonSortieDetail.NBonSortie = NBonSortie;
                    bonSortieDetail.Ordre = i++;
                    bonSortieDetail.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
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
                this.RestituerStockReel(transaction);

                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "BonSortie_Modifier";
                cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
                cmd.Parameters.AddWithValue("@DateSortie", DateSortie);
                cmd.Parameters.AddWithValue("@CChauffeur", CChauffeur);
                cmd.Parameters.AddWithValue("@CFournisseur", CFournisseur);
                cmd.Parameters.AddWithValue("@CClient", CClient);
                cmd.Parameters.AddWithValue("@TypeMouvement", TypeMouvement);
                cmd.Parameters.AddWithValue("@CVehicule", CVehicule);
                cmd.Parameters.AddWithValue("@MatriculeVoiture", MatriculeVoiture);
                cmd.Parameters.AddWithValue("@NDocumentSource", NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale", RaisonSociale);
                cmd.Parameters.AddWithValue("@PCModification", PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@NTelephone", NTelephone);
                cmd.Parameters.AddWithValue("@Adresse", Adresse);
                cmd.Parameters.AddWithValue("@MatriculeFiscale", MatriculeFiscale);
                cmd.Parameters.AddWithValue("@NRattachement", NRattachement);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                int i = 1;

                foreach (BonSortieDetail bonSortieDetail in BonSortieDetailCollection)
                {
                    bonSortieDetail.NBonSortie = this.NBonSortie;
                    bonSortieDetail.Ordre = i++;
                    bonSortieDetail.CreePar = this.ModifiePar;
                    bonSortieDetail.PCInsertion = this.PCModification;
                    bonSortieDetail.Sauvegarder(transaction);
                }
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void RestituerStockReel(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonSortieDetail_RestituerStockReel";
            cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
            cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);

            foreach (SqlParameter parametre in cmd.Parameters)
            {
                if (parametre.Value == null)
                {
                    parametre.Value = DBNull.Value;
                }
            }
            cmd.ExecuteNonQuery();
        }

        public static string RecupererNumeroBonSortie(string exercice, string cEntrepot, out int indice)
        {
            string nBonSortie = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;

                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonSortie_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonSortie = dr["NBonSortie"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }

                dr.Close();
            }

            return nBonSortie;
        }

        public static string RecupererNumeroBonSortie(string exercice, string cEntrepot)
        {
            int indice = 0;
            return BonSortie.RecupererNumeroBonSortie(exercice, cEntrepot, out indice);
        }

        public static BonSortie Charger(string nBonSortie, string cEntrepot)
        {
            BonSortie bonSortie = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonSortie_Charger";
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
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
                            bonSortie = new BonSortie();
                            bonSortie.NBonSortie = dr["NBonSortie"].ToString();
                            bonSortie.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CChauffeur"] != DBNull.Value)
                                bonSortie.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonSortie.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonSortie.CClient = dr["CClient"].ToString();
                            if (dr["TypeMouvement"] != DBNull.Value)
                                bonSortie.TypeMouvement = dr["TypeMouvement"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonSortie.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MatriculeVoiture"] != DBNull.Value)
                                bonSortie.MatriculeVoiture = dr["MatriculeVoiture"].ToString();
                            if (dr["NDocumentSource"] != DBNull.Value)
                                bonSortie.NDocumentSource = dr["NDocumentSource"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonSortie.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateSortie"] != DBNull.Value)
                                bonSortie.DateSortie = DateTime.Parse(dr["DateSortie"].ToString());
                            if (dr["DateInsertion"] != DBNull.Value)
                                bonSortie.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonSortie.Indice = int.Parse(dr["Indice"].ToString());

                            if (dr["NTelephone"] != DBNull.Value)
                                bonSortie.NTelephone = dr["NTelephone"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonSortie.Adresse = dr["Adresse"].ToString();
                            if (dr["MatriculeFiscal"] != DBNull.Value)
                                bonSortie.MatriculeFiscale = dr["MatriculeFiscal"].ToString();
                            if (dr["NRattachement"] != DBNull.Value)
                                bonSortie.NRattachement = dr["NRattachement"].ToString();
                        }
                    }
                    if (bonSortie != null)
                        bonSortie.BonSortieDetailCollection = BonSortieDetailCollection.Charger(bonSortie.NBonSortie, bonSortie.CEntrepot);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonSortie;
        }

        public static BonSortie ChargerParDocumentSource(string typeMouvement, string nDocumentSource)
        {
            BonSortie bonSortie = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonSortie_ChargerParDocumentSource";
                    cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                    cmd.Parameters.AddWithValue("@NDocumentSource", nDocumentSource);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bonSortie = new BonSortie();
                            bonSortie.NBonSortie = dr["NBonSortie"].ToString();
                            bonSortie.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CChauffeur"] != DBNull.Value)
                                bonSortie.CChauffeur = dr["CChauffeur"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonSortie.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonSortie.CClient = dr["CClient"].ToString();
                            if (dr["TypeMouvement"] != DBNull.Value)
                                bonSortie.TypeMouvement = dr["TypeMouvement"].ToString();
                            if (dr["CVehicule"] != DBNull.Value)
                                bonSortie.CVehicule = dr["CVehicule"].ToString();
                            if (dr["MatriculeVoiture"] != DBNull.Value)
                                bonSortie.MatriculeVoiture = dr["MatriculeVoiture"].ToString();
                            if (dr["NDocumentSource"] != DBNull.Value)
                                bonSortie.NDocumentSource = dr["NDocumentSource"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonSortie.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateSortie"] != DBNull.Value)
                                bonSortie.DateSortie = DateTime.Parse(dr["DateSortie"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonSortie.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["NTelephone"] != DBNull.Value)
                                bonSortie.NTelephone = dr["NTelephone"].ToString();
                            if (dr["Adresse"] != DBNull.Value)
                                bonSortie.Adresse = dr["Adresse"].ToString();
                            if (dr["MatriculeFiscal"] != DBNull.Value)
                                bonSortie.MatriculeFiscale = dr["MatriculeFiscal"].ToString();
                            if (dr["NRattachement"] != DBNull.Value)
                                bonSortie.NRattachement = dr["NRattachement"].ToString();

                            bonSortie.BonSortieDetailCollection = BonSortieDetailCollection.Charger(bonSortie.NBonSortie, bonSortie.CEntrepot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonSortie;
        }

        public static string ChargerCodeBonSortieDon(SqlTransaction transaction, string exercice)
        {
            string codeSortieDon = string.Empty;
            string dernierNBonSortie = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT TOP 1 NBonSortie FROM BonSortie WHERE NBonSortie LIKE 'BS_DON" + exercice.Substring(2, 2) + "%' ORDER BY NBonSortie DESC";
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        dernierNBonSortie = dr["NBonSortie"].ToString();
                }
                if (!string.IsNullOrEmpty(dernierNBonSortie))
                {
                    string dernierIndice = dernierNBonSortie.Substring(8);
                    int indice = int.Parse(dernierIndice) + 1;
                    codeSortieDon = "BS_DON" + exercice.Substring(2, 2) + indice.ToString().PadLeft(6, '0');

                }
                else
                    codeSortieDon = "BS_DON" + exercice.Substring(2, 2) + "000001";
            }

            catch (Exception)
            {
                throw;
            }
            return (codeSortieDon);
        }

        public static bool HaveLotCollection(string nBonSortie, string cEntrepot)
        {
            bool have = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonSortie_HaveLotCollection";
                    cmd.Parameters.AddWithValue("@NBonSortie", nBonSortie);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                            have = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return have;
        }
    }


    public class BonSortieCollection : List<BonSortie>
        {
            public static DataSet ChargerVue(DateTime dateDebut, DateTime dateFin, string famille, string nature, string pays)
            {
                DataSet ds = new DataSet();

                using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonSortieListe_Rpt_Charger";
                    cmd.Parameters.AddWithValue("@DateDeb", dateDebut);
                    cmd.Parameters.AddWithValue("@DateFin", dateFin);
                    cmd.Parameters.AddWithValue("@CFamille", famille);
                    cmd.Parameters.AddWithValue("@CPays", pays);
                    cmd.Parameters.AddWithValue("@CTiers", nature);

                    foreach (SqlParameter parametre in cmd.Parameters)
                    {
                        if (parametre.Value == null)
                        {
                            parametre.Value = DBNull.Value;
                        }
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds, "BonSortieListe_Rpt_Charger");
                }
                return (ds);
            }

            public static BonSortieCollection Charger(string typeMouvement, string nDocumentSource)
            {
                BonSortieCollection collection = new BonSortieCollection();
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BonSortie_ChargerParDocumentSource";
                        cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                        cmd.Parameters.AddWithValue("@NDocumentSource", nDocumentSource);

                        foreach (SqlParameter parametre in cmd.Parameters)
                            if (parametre.Value == null)
                                parametre.Value = DBNull.Value;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                BonSortie bonSortie = new BonSortie();
                                bonSortie.NBonSortie = dr["NBonSortie"].ToString();
                                bonSortie.CEntrepot = dr["CEntrepot"].ToString();
                                if (dr["CChauffeur"] != DBNull.Value)
                                    bonSortie.CChauffeur = dr["CChauffeur"].ToString();
                                if (dr["CFournisseur"] != DBNull.Value)
                                    bonSortie.CFournisseur = dr["CFournisseur"].ToString();
                                if (dr["CClient"] != DBNull.Value)
                                    bonSortie.CClient = dr["CClient"].ToString();
                                if (dr["TypeMouvement"] != DBNull.Value)
                                    bonSortie.TypeMouvement = dr["TypeMouvement"].ToString();
                                if (dr["CVehicule"] != DBNull.Value)
                                    bonSortie.CVehicule = dr["CVehicule"].ToString();
                                if (dr["MatriculeVoiture"] != DBNull.Value)
                                    bonSortie.MatriculeVoiture = dr["MatriculeVoiture"].ToString();
                                if (dr["NDocumentSource"] != DBNull.Value)
                                    bonSortie.NDocumentSource = dr["NDocumentSource"].ToString();
                                if (dr["RaisonSociale"] != DBNull.Value)
                                    bonSortie.RaisonSociale = dr["RaisonSociale"].ToString();
                                if (dr["DateSortie"] != DBNull.Value)
                                    bonSortie.DateSortie = DateTime.Parse(dr["DateSortie"].ToString());
                                if (dr["Indice"] != DBNull.Value)
                                    bonSortie.Indice = int.Parse(dr["Indice"].ToString());
                                if (dr["NTelephone"] != DBNull.Value)
                                    bonSortie.NTelephone = dr["NTelephone"].ToString();
                                if (dr["Adresse"] != DBNull.Value)
                                    bonSortie.Adresse = dr["Adresse"].ToString();
                                if (dr["MatriculeFiscal"] != DBNull.Value)
                                    bonSortie.MatriculeFiscale = dr["MatriculeFiscal"].ToString();
                                if (dr["NRattachement"] != DBNull.Value)
                                    bonSortie.NRattachement = dr["NRattachement"].ToString();

                                bonSortie.BonSortieDetailCollection = BonSortieDetailCollection.Charger(bonSortie.NBonSortie, bonSortie.CEntrepot);
                                collection.Add(bonSortie);
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

            public static DataTable ChargerFiltre(DataTable collection, string cClient, string cEntrepot, string typeMouvement, string NBonSortie, DateTime? dtDebut, DateTime? dtFin)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "BonSortie_Filtre";
                        cmd.Parameters.AddWithValue("@CClient", cClient);
                        cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                        cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                        cmd.Parameters.AddWithValue("@NBonSortie", NBonSortie);
                        cmd.Parameters.AddWithValue("@DateSortieDu", dtDebut);
                        cmd.Parameters.AddWithValue("@DateSortieAu", dtFin);
                        foreach (SqlParameter parametre in cmd.Parameters)
                            if ((parametre.Value == null) || (parametre.Value == ""))
                                parametre.Value = DBNull.Value;

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(collection);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return collection;
            }

            public static DataTable ChargerTypeMouvement(DataTable collection, string natureMouvement)
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                    {
                        cn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = cn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "Ref_TypeMouvement_Charger";
                        cmd.Parameters.AddWithValue("@CTypeMouvement", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NatureMouvement", natureMouvement);

                        foreach (SqlParameter parametre in cmd.Parameters)
                            if ((parametre.Value == null) || (parametre.Value == ""))
                                parametre.Value = DBNull.Value;


                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(collection);
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