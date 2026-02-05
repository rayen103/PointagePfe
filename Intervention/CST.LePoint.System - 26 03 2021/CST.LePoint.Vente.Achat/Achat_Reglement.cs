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

namespace CST.LePoint.Achat.Metier
{
    public class Achat_Reglement
    {
        #region Propriètés
        [XmlAttribute("CReglement")]
        [Bindable(true)]
        public string CReglement { get; set; }
        [XmlAttribute("RIBBanque")]
        [Bindable(true)]
        public string RIBBanque { get; set; }
        [XmlAttribute("CBanque")]
        [Bindable(true)]
        public string CBanque { get; set; }
        [XmlAttribute("CEtatReglement")]
        [Bindable(true)]
        public string CEtatReglement { get; set; }
        [XmlAttribute("CFournisseur")]
        [Bindable(true)]
        public string CFournisseur { get; set; }
        [XmlAttribute("CTypeReglement")]
        [Bindable(true)]
        public string CTypeReglement { get; set; }
        [XmlAttribute("BTransfereComptabilite")]
        [Bindable(true)]
        public bool BTransfereComptabilite { get; set; }
        [XmlAttribute("DateCreation")]
        [Bindable(true)]
        public DateTime? DateCreation { get; set; }
        [XmlAttribute("DateEcheance")]
        [Bindable(true)]
        public DateTime? DateEcheance { get; set; }
        [XmlAttribute("DateEmission")]
        [Bindable(true)]
        public DateTime? DateEmission { get; set; }
        [XmlAttribute("DateModification")]
        [Bindable(true)]
        public DateTime? DateModification { get; set; }
        [XmlAttribute("BAnnulation")]
        [Bindable(true)]
        public bool BAnnulation { get; set; }
        [XmlAttribute("Montant")]
        [Bindable(true)]
        public decimal Montant { get; set; }
        [XmlAttribute("NAvoir")]
        [Bindable(true)]
        public string NAvoir { get; set; }
        [XmlAttribute("NReglement")]
        [Bindable(true)]
        public string NReglement { get; set; }
        [XmlAttribute("Observation")]
        [Bindable(true)]
        public string Observation { get; set; }
        [XmlAttribute("RaisonSociale")]
        [Bindable(true)]
        public string RaisonSociale { get; set; }
        [XmlAttribute("Indice")]
        [Bindable(true)]
        public int Indice { get; set; }
        [XmlAttribute("ResteReglement")]
        [Bindable(true)]
        public decimal ResteReglement { get; set; }
        [XmlAttribute("CreePar")]
        [Bindable(true)]
        public int CreePar { get; set; }
        [XmlAttribute("ModifiePar")]
        [Bindable(true)]
        public int ModifiePar { get; set; }
        [XmlAttribute("PCCreation")]
        [Bindable(true)]
        public string PCCreation { get; set; }
        [XmlAttribute("PCModification")]
        [Bindable(true)]
        public string PCModification { get; set; }
        [XmlAttribute("NPiece")]
        [Bindable(true)]
        public string NPiece { get; set; }
        [XmlAttribute("DateAnnulation")]
        [Bindable(true)]
        public DateTime? DateAnnulation { get; set; }
        [XmlAttribute("NOrdredeTravail")]
        [Bindable(true)]
        public string NOrdredeTravail { get; set; }

        #endregion

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
                cmd.CommandText = "Achat_Reglement_Inserer";

                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@RIBBanque", this.RIBBanque);
                cmd.Parameters.AddWithValue("@CEtatReglement", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CTypeReglement", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@BTransfereComptabilite", this.BTransfereComptabilite);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@NAvoir", this.NAvoir);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@NPiece", this.NPiece);
                cmd.Parameters.AddWithValue("@DateInsertion", this.DateCreation);
                cmd.Parameters.AddWithValue("@CreePar", this.CreePar);
                cmd.Parameters.AddWithValue("@PCInsertion", this.PCCreation);
                cmd.Parameters.AddWithValue("@Exercice", DateTime.Now.Year.ToString());
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                foreach (SqlParameter parametre in cmd.Parameters)
                    if (parametre.Value == null)
                        parametre.Value = DBNull.Value;

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                        this.CReglement = dr["CReglement"].ToString();
                }

                if (this.CTypeReglement == AchatHelper.TypeReglement.AVRAVC.ToString() || this.CTypeReglement == AchatHelper.TypeReglement.AVR.ToString())
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, 0, 0, 0, this.Montant, transaction);
                else
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, 0, this.Montant, 0, 0, transaction);
                
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
            Achat_Reglement reglement = Achat_Reglement.Charger(this.CReglement);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Transaction = transaction;
                cmd.Connection = transaction.Connection;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Achat_Reglement_Modifier";

                cmd.Parameters.AddWithValue("@CReglement", this.CReglement);
                cmd.Parameters.AddWithValue("@CFournisseur", this.CFournisseur);
                cmd.Parameters.AddWithValue("@CEtatReglement ", this.CEtatReglement);
                cmd.Parameters.AddWithValue("@CBanque", this.CBanque);
                cmd.Parameters.AddWithValue("@CTypeReglement ", this.CTypeReglement);
                cmd.Parameters.AddWithValue("@Observation", this.Observation);
                cmd.Parameters.AddWithValue("@DateEcheance", this.DateEcheance);
                cmd.Parameters.AddWithValue("@DateEmission", this.DateEmission);
                cmd.Parameters.AddWithValue("@Montant", this.Montant);
                cmd.Parameters.AddWithValue("@RIBBanque", this.RIBBanque);
                cmd.Parameters.AddWithValue("@NReglement", this.NReglement);
                cmd.Parameters.AddWithValue("@RaisonSociale", this.RaisonSociale);
                cmd.Parameters.AddWithValue("@ResteReglement", this.ResteReglement);
                cmd.Parameters.AddWithValue("@PCModification", this.PCModification);
                cmd.Parameters.AddWithValue("@ModifiePar", this.ModifiePar);
                cmd.Parameters.AddWithValue("@DateModification", DateTime.Now);
                cmd.Parameters.AddWithValue("@NOrdredeTravail", this.NOrdredeTravail);
                foreach (SqlParameter Parameter in cmd.Parameters)
                    if (Parameter.Value == null)
                        Parameter.Value = DBNull.Value;
                cmd.ExecuteNonQuery();

                if (this.CTypeReglement == AchatHelper.TypeReglement.AVRAVC.ToString() || this.CTypeReglement == AchatHelper.TypeReglement.AVR.ToString())
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, 0, 0, 0, (this.Montant - reglement.Montant), transaction);
                else
                    AchatHelper.MiseAJourSoldeFnr(this.CFournisseur, 0, (this.Montant - reglement.Montant), 0, 0, transaction);
            }

               
            catch (Exception)
            {
                throw;
            }
        }

        public void ModifierNOrdredeTravail()
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
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Achat_Reglement set NOrdredeTravail = '" + this.NOrdredeTravail + "' where CReglement = '" + this.CReglement + "'";
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


        public static Achat_Reglement Charger(string cReglement)
        {
            Achat_Reglement reglement = null;

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
                    cmd.CommandText = "Achat_Reglement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            reglement = new Achat_Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                reglement.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RIBBanque"] != DBNull.Value)
                                reglement.RIBBanque = dr["RIBBanque"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                reglement.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                reglement.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["PCCreation"] != DBNull.Value)
                                reglement.PCCreation = dr["PCCreation"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                reglement.PCModification = dr["PCModification"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                reglement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                reglement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                reglement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();

                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return reglement;
        }
    }

    public class Achat_ReglementCollection : List<Achat_Reglement>
    {
        public static Achat_ReglementCollection Charger(string cReglement)
        {
            Achat_ReglementCollection collection = new Achat_ReglementCollection();

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
                    cmd.CommandText = "Achat_Reglement_Charger";
                    cmd.Parameters.AddWithValue("@CReglement", cReglement);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_Reglement reglement = new Achat_Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                reglement.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RIBBanque"] != DBNull.Value)
                                reglement.RIBBanque = dr["RIBBanque"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                reglement.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                reglement.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["PCCreation"] != DBNull.Value)
                                reglement.PCCreation = dr["PCCreation"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                reglement.PCModification = dr["PCModification"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                reglement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                reglement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                reglement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            collection.Add(reglement);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return collection;
        }
        public static Achat_ReglementCollection ChargerparNOrdredeTravail(string NOrdredeTravail)
        {
            Achat_ReglementCollection collection = new Achat_ReglementCollection();

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
                    cmd.CommandText = "Achat_Reglement_ChargerParOT";
                    cmd.Parameters.AddWithValue("@NOrdredeTravail", NOrdredeTravail);
                    foreach (SqlParameter parameter in cmd.Parameters)
                        if (parameter.Value == null)
                            parameter.Value = DBNull.Value;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Achat_Reglement reglement = new Achat_Reglement();
                            reglement.CReglement = dr["CReglement"].ToString();
                            if (dr["CFournisseur"] != DBNull.Value)
                                reglement.CFournisseur = dr["CFournisseur"].ToString();
                            if (dr["RIBBanque"] != DBNull.Value)
                                reglement.RIBBanque = dr["RIBBanque"].ToString();
                            if (dr["CBanque"] != DBNull.Value)
                                reglement.CBanque = dr["CBanque"].ToString();
                            if (dr["CEtatReglement"] != DBNull.Value)
                                reglement.CEtatReglement = dr["CEtatReglement"].ToString();
                            if (dr["CTypeReglement"] != DBNull.Value)
                                reglement.CTypeReglement = dr["CTypeReglement"].ToString();
                            if (dr["BTransfereComptabilite"] != DBNull.Value)
                                reglement.BTransfereComptabilite = bool.Parse(dr["BTransfereComptabilite"].ToString());
                            if (dr["DateCreation"] != DBNull.Value)
                                reglement.DateCreation = DateTime.Parse(dr["DateCreation"].ToString());
                            if (dr["DateEcheance"] != DBNull.Value)
                                reglement.DateEcheance = DateTime.Parse(dr["DateEcheance"].ToString());
                            if (dr["DateEmission"] != DBNull.Value)
                                reglement.DateEmission = DateTime.Parse(dr["DateEmission"].ToString());
                            if (dr["DateModification"] != DBNull.Value)
                                reglement.DateModification = DateTime.Parse(dr["DateModification"].ToString());
                            if (dr["BAnnulation"] != DBNull.Value)
                                reglement.BAnnulation = bool.Parse(dr["BAnnulation"].ToString());
                            if (dr["NAvoir"] != DBNull.Value)
                                reglement.NAvoir = dr["NAvoir"].ToString();
                            if (dr["NReglement"] != DBNull.Value)
                                reglement.NReglement = dr["NReglement"].ToString();
                            if (dr["Observation"] != DBNull.Value)
                                reglement.Observation = dr["Observation"].ToString();
                            if (dr["RaisonSociale"] != DBNull.Value)
                                reglement.RaisonSociale = dr["RaisonSociale"].ToString();
                            if (dr["PCCreation"] != DBNull.Value)
                                reglement.PCCreation = dr["PCCreation"].ToString();
                            if (dr["PCModification"] != DBNull.Value)
                                reglement.PCModification = dr["PCModification"].ToString();
                            if (dr["NPiece"] != DBNull.Value)
                                reglement.NPiece = dr["NPiece"].ToString();
                            if (dr["Montant"] != DBNull.Value)
                                reglement.Montant = decimal.Parse(dr["Montant"].ToString());
                            if (dr["Indice"] != DBNull.Value)
                                reglement.Indice = int.Parse(dr["Indice"].ToString());
                            if (dr["ResteReglement"] != DBNull.Value)
                                reglement.ResteReglement = decimal.Parse(dr["ResteReglement"].ToString());
                            if (dr["CreePar"] != DBNull.Value)
                                reglement.CreePar = int.Parse(dr["CreePar"].ToString());
                            if (dr["ModifiePar"] != DBNull.Value)
                                reglement.ModifiePar = int.Parse(dr["ModifiePar"].ToString());
                            if (dr["DateAnnulation"] != DBNull.Value)
                                reglement.DateAnnulation = DateTime.Parse(dr["DateAnnulation"].ToString());
                            if (dr["NOrdredeTravail"] != DBNull.Value)
                                reglement.NOrdredeTravail = dr["NOrdredeTravail"].ToString();
                            collection.Add(reglement);
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return collection;
        }
    }
}
