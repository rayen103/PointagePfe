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
    public class BonEntree
    {
        #region Propriétés

        [XmlAttribute("NBonEntree")]
        [Bindable(true)]
        public string NBonEntree { get; set; }

        [XmlAttribute("CEntrepot")]
        [Bindable(true)]
        public string CEntrepot { get; set; }

        [XmlAttribute("CClient")]
        [Bindable(true)]
        public string CClient { get; set; }

        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }

        [XmlAttribute("TypeMouvement")]
        [Bindable(true)]
        public string TypeMouvement { get; set; }

        [XmlAttribute("BTvaExonore")]
        [Bindable(true)]
        public bool BTvaExonore { get; set; }

        [XmlAttribute("BFodecExonore")]
        [Bindable(true)]
        public bool BFodecExonore { get; set; }

        [XmlAttribute("NFactureAchat")]
        [Bindable(true)]
        public string NFactureAchat { get; set; }

        [XmlAttribute("NDocumentSource")]
        [Bindable(true)]
        public string NDocumentSource { get; set; }

        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }

        [XmlAttribute("Exercice")]
        [Bindable(true)]
        public string Exercice { get; set; }

        [XmlAttribute("DateEntree")]
        [Bindable(true)]
        public DateTime DateEntree { get; set; }

        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }

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

        [XmlAttribute("CLot")]
        [Bindable(true)]
        public string CLot { get; set; }

        [XmlAttribute("SourceProduction")]
        [Bindable(true)]
        public string SourceProduction { get; set; }

        public BonEntreeDetailCollection BonEntreeDetailCollection;

        #endregion Propriétés

        public BonEntree()
        {
            this.NBonEntree = string.Empty;
            this.BonEntreeDetailCollection = new BonEntreeDetailCollection();
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
                cmd.CommandText = "BonEntree_Inserer";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);

                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CFournisseur ", this.CFournisseur);
                cmd.Parameters.AddWithValue("@TypeMouvement", this.TypeMouvement);
                cmd.Parameters.AddWithValue("@BTvaExonore ", this.BTvaExonore);
                cmd.Parameters.AddWithValue("@BFodecExonore ", this.BFodecExonore);
                cmd.Parameters.AddWithValue("@NFactureAchat ", this.NFactureAchat);
                cmd.Parameters.AddWithValue("@NDocumentSource", this.NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale ", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateEntree ", this.DateEntree);
                cmd.Parameters.AddWithValue("@Exercice", this.Exercice);
                cmd.Parameters.AddWithValue("@DateInsertion", DateTime.Now);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCInsertion);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@CLot", this.CLot);
                cmd.Parameters.AddWithValue("@SourceProduction", this.SourceProduction);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        this.NBonEntree = dr["NBonEntree"].ToString();
                        this.Indice = int.Parse(dr["DernierIndice"].ToString());
                    }
                }
                int i = 1;
                foreach (BonEntreeDetail bonEntreeDetail in BonEntreeDetailCollection)
                {
                    bonEntreeDetail.NBonEntree = this.NBonEntree;
                    bonEntreeDetail.Ordre = i++;
                    bonEntreeDetail.CreePar = this.CreePar;
                    bonEntreeDetail.PCInsertion = this.PCInsertion;
                    bonEntreeDetail.Sauvegarder(transaction);
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
                cmd.CommandText = "BonEntree_Modifier";
                cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
                cmd.Parameters.AddWithValue("@CClient", this.CClient);
                cmd.Parameters.AddWithValue("@CFournisseur ", this.CFournisseur);
                cmd.Parameters.AddWithValue("@TypeMouvement", this.TypeMouvement);
                cmd.Parameters.AddWithValue("@BTvaExonore ", this.BTvaExonore);
                cmd.Parameters.AddWithValue("@BFodecExonore ", this.BFodecExonore);
                cmd.Parameters.AddWithValue("@NFactureAchat ", this.NFactureAchat);
                cmd.Parameters.AddWithValue("@NDocumentSource", this.NDocumentSource);
                cmd.Parameters.AddWithValue("@RaisonSociale ", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@DateEntree ", this.DateEntree);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }
                cmd.ExecuteNonQuery();
                int i = 1;
                foreach (BonEntreeDetail bonEntreeDetail in BonEntreeDetailCollection)
                {
                    bonEntreeDetail.NBonEntree = this.NBonEntree;
                    bonEntreeDetail.Ordre = i++;
                    bonEntreeDetail.CreePar = this.ModifiePar;
                    bonEntreeDetail.PCInsertion = this.PCModification;
                    bonEntreeDetail.Sauvegarder(transaction);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void SupprimerTaxes(SqlTransaction transaction)
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Transaction = transaction;
        //        cmd.Connection = transaction.Connection;
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        cmd.CommandText = "BonEntreeTaxe_Supprimer";
        //        cmd.Parameters.AddWithValue("@CEntrepot", CEntrepot);
        //        cmd.Parameters.AddWithValue("@NBonEntree", NBonEntree);

        //        foreach (SqlParameter parametre in cmd.Parameters)
        //            if (parametre.Value == null)
        //                parametre.Value = DBNull.Value;

        //        cmd.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void RestituerStockReel(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "BonEntreeDetail_RestituerStockReel";
            cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
            cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
            foreach (SqlParameter parametre in cmd.Parameters)
            {
                if (parametre.Value == null)
                {
                    parametre.Value = DBNull.Value;
                }
            }

            cmd.ExecuteNonQuery();
        }

        public void RestituerStockACommandee()
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
                    cmd.CommandText = "BonEntreeDetail_RestituerStockACommandee";
                    cmd.Parameters.AddWithValue("@CEntrepot", this.CEntrepot);
                    cmd.Parameters.AddWithValue("@NBonEntree", this.NBonEntree);
                    cmd.Parameters.AddWithValue("@NDocumentSource", this.NDocumentSource);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public static string RecupererNumeroBonEntree(string exercice, string cEntrepot, out int indice)
        {
            string nBonEntree = string.Empty;
            indice = 0;

            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                var cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.CommandText = "BonEntree_RecupererNouveauNumero";
                cmd2.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                cmd2.Parameters.AddWithValue("@Exercice", exercice);
                SqlDataReader dr = cmd2.ExecuteReader();
                if (dr.Read())
                {
                    nBonEntree = dr["NBonEntree"].ToString();
                    indice = int.Parse(dr["DernierIndice"].ToString());
                }
                dr.Close();
            }

            return nBonEntree;
        }

        public static string RecupererNumeroBonEntree(string exercice, string cEntrepot)
        {
            int indice = 0;
            return BonEntree.RecupererNumeroBonEntree(exercice, cEntrepot, out indice);
        }

        public static BonEntree Charger(string nBonEntree, string cEntrepot)
        {
            BonEntree bonEntree = null;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonEntree_Charger";
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            bonEntree = new BonEntree();
                            bonEntree.NBonEntree = dr["NBonEntree"].ToString();
                            bonEntree.CEntrepot = dr["CEntrepot"].ToString();
                            if (dr["CClient"] != DBNull.Value)
                                bonEntree.CClient = dr["CClient"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonEntree.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["TypeMouvement"] != DBNull.Value)
                                bonEntree.TypeMouvement = dr["TypeMouvement"].ToString();
                            if (dr["BTvaExonore"] != DBNull.Value)
                                bonEntree.BTvaExonore = bool.Parse(dr["BTvaExonore"].ToString());
                            if (dr["BFodecExonore"] != DBNull.Value)
                                bonEntree.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["NFactureAchat"] != DBNull.Value)
                                bonEntree.NFactureAchat = dr["NFactureAchat"].ToString();
                            if (dr["NDocumentSource"] != DBNull.Value)
                                bonEntree.NDocumentSource = dr["NDocumentSource"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonEntree.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateEntree"] != DBNull.Value)
                                bonEntree.DateEntree = DateTime.Parse(dr["DateEntree"].ToString());
                           if (dr["DateInsertion"] != DBNull.Value)
                                bonEntree.DateInsertion = DateTime.Parse(dr["DateInsertion"].ToString());
                            if (dr["indice"] != DBNull.Value)
                                bonEntree.Indice = int.Parse(dr["Indice"].ToString());
                        }
                    }
                    bonEntree.BonEntreeDetailCollection = BonEntreeDetailCollection.Charger(bonEntree.NBonEntree, bonEntree.CEntrepot);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonEntree;
        }

        public static BonEntree ChargerParDocumentSource(string typeMouvement, string nDocumentSource)
        {
            BonEntree bonEntree = null;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonEntree_ChargerParDocumentSource";
                    cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                    cmd.Parameters.AddWithValue("@NDocumentSource", nDocumentSource);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            bonEntree = new BonEntree();

                            bonEntree.NBonEntree = dr["NBonEntree"].ToString();
                            bonEntree.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["CClient"] != DBNull.Value)
                                bonEntree.CClient = dr["CClient"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonEntree.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["TypeMouvement"] != DBNull.Value)
                                bonEntree.TypeMouvement = dr["TypeMouvement"].ToString();
                            if (dr["BTvaExonore"] != DBNull.Value)
                                bonEntree.BTvaExonore = bool.Parse(dr["BTvaExonore"].ToString());
                            if (dr["BFodecExonore"] != DBNull.Value)
                                bonEntree.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["NFactureAchat"] != DBNull.Value)
                                bonEntree.NFactureAchat = dr["NFactureAchat"].ToString();
                            if (dr["NDocumentSource"] != DBNull.Value)
                                bonEntree.NDocumentSource = dr["NDocumentSource"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonEntree.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateEntree"] != DBNull.Value)
                                bonEntree.DateEntree = DateTime.Parse(dr["DateEntree"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonEntree.Indice = int.Parse(dr["Indice"].ToString());
                            bonEntree.BonEntreeDetailCollection = BonEntreeDetailCollection.Charger(bonEntree.NBonEntree, bonEntree.CEntrepot);
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return bonEntree;
        }

        public static decimal calculPourcentage(decimal pourcentage, decimal montantHT)
        {
            return (montantHT / 100) * pourcentage;
        }

        public static bool HaveLotCollection(string nBonEntree, string cEntrepot)
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
                    cmd.CommandText = "BonEntree_HaveLotCollection";
                    cmd.Parameters.AddWithValue("@NBonEntree", nBonEntree);
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

        public void InsererProd(string connectionString)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
            {
                cn.Open();
                SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    Inserer(transaction);
                    if (connectionString == ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString)
                        ModifierEtatBonProduction(transaction);
                    else
                    {
                        using (SqlConnection cn2 = new SqlConnection(connectionString))
                        {
                            cn2.Open();
                            SqlTransaction transaction2 = cn2.BeginTransaction();
                            try
                            {
                                ModifierEtatBonProduction(transaction2);
                                transaction2.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction2.Rollback();
                                throw ex;
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void ModifierEtatBonProduction(SqlTransaction transaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Transaction = transaction;
            cmd.Connection = transaction.Connection;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update BON_PRODUCTION set FLAG_ETAT_BP = 'T' where NUM_BP = '" + this.NDocumentSource +"'";
            cmd.ExecuteNonQuery();
        }
    }

    public class BonEntreeCollection : List<BonEntree>
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
                cmd.CommandText = "BonEntreeListe_Rpt_Charger";
                cmd.Parameters.AddWithValue("@DateDeb", dateDebut);
                cmd.Parameters.AddWithValue("@DateFin", dateFin);
                cmd.Parameters.AddWithValue("@CFamille", famille);
                cmd.Parameters.AddWithValue("@CTiers", nature);
                cmd.Parameters.AddWithValue("@CPays", pays);
                foreach (SqlParameter parametre in cmd.Parameters)
                {
                    if (parametre.Value == null)
                    {
                        parametre.Value = DBNull.Value;
                    }
                }

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds, "BonEntreeListe_Rpt_Charger");
            }
            return (ds);
        }

        public static BonEntreeCollection Charger(string typeMouvement, string nDocumentSource)
        {
            BonEntreeCollection collection = new BonEntreeCollection();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonEntree_ChargerParDocumentSource";
                    cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                    cmd.Parameters.AddWithValue("@NDocumentSource", nDocumentSource);

                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            BonEntree bonEntree = new BonEntree();

                            bonEntree.NBonEntree = dr["NBonEntree"].ToString();
                            bonEntree.CEntrepot = dr["CEntrepot"].ToString();

                            if (dr["CClient"] != DBNull.Value)
                                bonEntree.CClient = dr["CClient"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                bonEntree.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["TypeMouvement"] != DBNull.Value)
                                bonEntree.TypeMouvement = dr["TypeMouvement"].ToString();
                            if (dr["BTvaExonore"] != DBNull.Value)
                                bonEntree.BTvaExonore = bool.Parse(dr["BTvaExonore"].ToString());
                            if (dr["BFodecExonore"] != DBNull.Value)
                                bonEntree.BFodecExonore = bool.Parse(dr["BFodecExonore"].ToString());
                            if (dr["NFactureAchat"] != DBNull.Value)
                                bonEntree.NFactureAchat = dr["NFactureAchat"].ToString();
                            if (dr["NDocumentSource"] != DBNull.Value)
                                bonEntree.NDocumentSource = dr["NDocumentSource"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                bonEntree.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["DateEntree"] != DBNull.Value)
                                bonEntree.DateEntree = DateTime.Parse(dr["DateEntree"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                bonEntree.Indice = int.Parse(dr["Indice"].ToString());
                            bonEntree.BonEntreeDetailCollection = BonEntreeDetailCollection.Charger(bonEntree.NBonEntree, bonEntree.CEntrepot);
                            collection.Add(bonEntree);
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

        public static DataTable ChargerFiltre(DataTable collection, string cFournisseur, string cEntrepot, string typeMouvement, string NBonEntree, DateTime? dt1, DateTime? dt2)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "BonEntree_Filtre";
                    cmd.Parameters.AddWithValue("@CFournisseur", cFournisseur);
                    cmd.Parameters.AddWithValue("@CEntrepot", cEntrepot);
                    cmd.Parameters.AddWithValue("@TypeMouvement", typeMouvement);
                    cmd.Parameters.AddWithValue("@NBonEntree", NBonEntree);
                    cmd.Parameters.AddWithValue("@DateEntreeDu", dt1);
                    cmd.Parameters.AddWithValue("@DateEntreeAu", dt2);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
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