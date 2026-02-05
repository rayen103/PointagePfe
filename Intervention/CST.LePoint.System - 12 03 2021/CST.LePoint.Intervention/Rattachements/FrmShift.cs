

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using CST.LePoint.CtrlLibrary;
using CST.LePoint.Intervention.Metier;
using CST.LePoint.Securite;
using CST.LePoint.Intervention.Properties;
using CST.LePoint.Securite.Entites;
using System.Data.SqlClient;
using System.Configuration;
using CST.LePoint.Referentiel;

namespace CST.LePoint.Intervention.Rattachements
{
    public partial class FrmShift : DevExpress.XtraEditors.XtraForm, IActionsSave
    {
        public FrmShift()
        {
            InitializeComponent();
        }
        private bool bRowValide = false;
        private bool breload = false;

        private void FrmPosteDeTravaille_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public GvColumnProprietes TitresPoste()
        {
            GvColumnProprietes proprietes = new GvColumnProprietes();
            //  Pointage_Service_Emp_Collection collection = Pointage_Service_Emp_Collection.Charger(null);
            ShiftCollection collection = ShiftCollection.Charger();
            proprietes.Add(new GvColumnPropriete("Code", GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Jour/Semaine", GvColumnPropriete.GvColumnType.LookUp, GvColumnPropriete.GvColumnEtat.Enable, chargeJour()));
            proprietes.Add(new GvColumnPropriete("Libellé", GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Heure Debut", GvColumnPropriete.GvColumnType.Time, GvColumnPropriete.GvColumnEtat.Enable));
            proprietes.Add(new GvColumnPropriete("Heure Fin",GvColumnPropriete.GvColumnType.Time,  GvColumnPropriete.GvColumnEtat.Enable));
          //  proprietes.Add(new GvColumnPropriete("modifier", GvColumnPropriete.GvColumnEtat.Invisible));
            return proprietes;
        }

        private static ItemCollection chargeJour()
        {
            ItemCollection Ic = new ItemCollection();
            Item I = new Item();
            I.Code = "Lundi";
            I.Libelle = "Lundi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Mardi";
            I.Libelle = "Mardi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Mercredi";
            I.Libelle = "Mercredi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Jeudi";
            I.Libelle = "Jeudi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Vendredi";
            I.Libelle = "Vendredi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Samedi";
            I.Libelle = "Samedi";
            Ic.Add(I);
            I = new Item();
            I.Code = "Dimanche";
            I.Libelle = "Dimanche";
            Ic.Add(I);
            return (Ic);
        }



        private void GridVPoste_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            try
            {
                if (!bRowValide)
                {
                    ColumnView view = sender as ColumnView;


                    if (string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("Code")))
                    {
                        //this.gridV.DeleteSelectedRows();
                        //return;
                        this.bRowValide = false;
                        e.Valid = false;
                        e.ErrorText = "Code est non renseigné !";
                        view.SetColumnError(null, e.ErrorText);
                    }
                    else if (string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("Libellé")))
                    {
                        this.bRowValide = false;
                        e.Valid = false;
                        e.ErrorText = "Libellé est non renseignée !";
                        view.SetColumnError(null, e.ErrorText);
                    }
                    else if (string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("Jour/Semaine")))
                    {
                        this.bRowValide = false;
                        e.Valid = false;
                        e.ErrorText = "Jour de la semaine est non renseignée !";
                        view.SetColumnError(null, e.ErrorText);
                    }
                    else if (string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("Heure Debut")))
                    {
                        this.bRowValide = false;
                        e.Valid = false;
                        e.ErrorText = "Heure Debut est non renseignée !";
                        view.SetColumnError(null, e.ErrorText);
                    }
                    else if (string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("Heure Fin")))
                    {
                        this.bRowValide = false;
                        e.Valid = false;
                        e.ErrorText = "Heure Fin est non renseignée !";
                        view.SetColumnError(null, e.ErrorText);
                    }
                   
                    //else if (!string.IsNullOrEmpty(GridVPoste.GetFocusedRowCellDisplayText("modifier")))
                    //{
                    //   // ModificationPoste();
                    //    this.GridVPoste.SetFocusedRowCellValue("modifier", 2);
                    //}
                    else
                    {
                        InsertionPoste();
                        //this.GridVPoste.SetFocusedRowCellValue("modifier", 0);
                    }
                }
            }
            catch
            {
                return;
            }
        }






        private void RemplirGrid()
        {
            var dt = new DataTable();



            try
            {
                CtrlHelper.InitGridView(GridVPoste, TitresPoste(), true);

                using (
                    var cn =
                        new SqlConnection(
                            ConfigurationManager.ConnectionStrings["CST_ConnectionString"].ConnectionString))
                {
                    cn.Open();
                    var cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Shift_Charger";
                    cmd.Parameters.AddWithValue("@Code_Shift", DBNull.Value);
                    foreach (SqlParameter parametre in cmd.Parameters)
                        if (parametre.Value == null)
                            parametre.Value = DBNull.Value;

                    var adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }

                CtrlHelper.FillGridView(GridVPoste, TitresPoste(), dt);
                
                 

                //gridView1.SortInfo.ClearAndAddRange(new[] {

                //    new GridMergedColumnSortInfo(
                //    new[] {
                //    colShipCountry, colShipCity, colShipRegion},
                //    new[] {
                //    ColumnSortOrder.Ascending, ColumnSortOrder.Descending, ColumnSortOrder.Ascending }),
                //    new GridColumnSortInfo(colCustomerID, ColumnSortOrder.Descending)
                //    }, 4); 
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GridVPoste_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            // nomColonneModifie = e.Column.Caption;
            //if ((!bRowValide) && (GridVPoste.GetFocusedRowCellDisplayText("modifier") != "1"))
            //{
                for (int i = 0; i < GridVPoste.RowCount; i++)
                {
                    if (GridVPoste.GetRowCellDisplayText(i, "Code").Equals(e.Value.ToString()))
                    {

                        DialogResult dialogResult1 = XtraMessageBox.Show("Cet Code_Poste existe déjà. Voulez-vous le Modifier ?",
                                                                            Resources.NomApplication,
                                                                            MessageBoxButtons.YesNo,
                                                                            MessageBoxIcon.Question,
                                                                            MessageBoxDefaultButton.Button1);

                        if (dialogResult1 == DialogResult.No)
                        {
                            this.GridVPoste.DeleteRow(e.RowHandle);
                            //gridV.FocusedRowHandle =e.RowHandle;
                            GridVPoste.FocusedColumn = GridVPoste.Columns["Code"];
                            return;
                        }
                        else
                        {
                            //return;
                            this.GridVPoste.DeleteRow(e.RowHandle);
                            GridVPoste.FocusedRowHandle = i;
                            GridVPoste.FocusedColumn = GridVPoste.Columns["Code"];
                            return;
                        }
                        break;
                    }
                    if (GridVPoste.GetRowCellDisplayText(i, "Libellé").Equals(e.Value.ToString()))
                    {

                        DialogResult dialogResult1 = XtraMessageBox.Show("Cet Libellé existe déjà. Voulez-vous le Modifier ?",
                                                                            Resources.NomApplication,
                                                                            MessageBoxButtons.YesNo,
                                                                            MessageBoxIcon.Question,
                                                                            MessageBoxDefaultButton.Button1);

                        if (dialogResult1 == DialogResult.No)
                        {
                            this.GridVPoste.DeleteRow(e.RowHandle);
                            //gridV.FocusedRowHandle =e.RowHandle;
                            GridVPoste.FocusedColumn = GridVPoste.Columns["Libellé"];
                            return;
                        }
                        else
                        {
                            //return;
                            this.GridVPoste.DeleteRow(e.RowHandle);
                            GridVPoste.FocusedRowHandle = i;
                            GridVPoste.FocusedColumn = GridVPoste.Columns["Libellé"];
                            return;
                        }
                        break;
                    }
                //}

            }


        }
        private void GridVPoste_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (breload)
            {
                Actualiser();
            }
        }
        public void InsertionPoste()
        {
            try
            {
                Shift Poste = new Shift();
                Poste.Code_Shift = GridVPoste.GetFocusedRowCellValue("Code").ToString();
                Poste.Lib_Shift = GridVPoste.GetFocusedRowCellValue("Libellé").ToString();
                Poste.Jour_Semaine = GridVPoste.GetFocusedRowCellDisplayText("Jour/Semaine").ToString();
                if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()))
                    Poste.Heure_Debut = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()).TimeOfDay;
                if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()))
                    Poste.Heure_Fin = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()).TimeOfDay;
                Poste.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                Poste.DateInsertion = DateTime.Now;
                Poste.PCInsertion = Environment.UserName;

                DialogResult dialogResult1 = XtraMessageBox.Show("Voulez-vous Enregistrer ?",
                                                                            Resources.NomApplication,
                                                                            MessageBoxButtons.YesNo,
                                                                            MessageBoxIcon.Question,
                                                                            MessageBoxDefaultButton.Button1);

                if (dialogResult1 == DialogResult.No)
                {
                    Actualiser();
                }
                else
                {
                    Poste.Sauvegarder();
                    Actualiser();
                    XtraMessageBox.Show(" Enregistrement Avec Succes. ",
                                          Resources.NomApplication,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information,
                                            MessageBoxDefaultButton.Button1);
                }



            }
            catch (Exception)
            {
                XtraMessageBox.Show(" échec de l'enregistrement. ",
                      Resources.NomApplication,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                Actualiser();
            }


        }
        public void ModificationPoste()
        {
            try
            {
                Shift Poste = new Shift();
                Poste.Code_Shift = GridVPoste.GetFocusedRowCellValue("Code").ToString();
                Poste.Lib_Shift = GridVPoste.GetFocusedRowCellValue("Libellé").ToString();
                Poste.Jour_Semaine = GridVPoste.GetFocusedRowCellDisplayText("Jour/Semaine").ToString();
                if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()))
                    Poste.Heure_Debut = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()).TimeOfDay;
                if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()))
                    Poste.Heure_Fin = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()).TimeOfDay;
                Poste.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                Poste.DateModification = DateTime.Now;
                Poste.PCModification = Environment.UserName;

                DialogResult dialogResult1 = XtraMessageBox.Show("Voulez-vous Enregistrer Vos Modifications ?",
                                                                           Resources.NomApplication,
                                                                           MessageBoxButtons.YesNo,
                                                                           MessageBoxIcon.Question,
                                                                           MessageBoxDefaultButton.Button1);

                if (dialogResult1 == DialogResult.No)
                {
                    breload = true;
                }
                else
                {
                    Poste.Sauvegarder();
                    XtraMessageBox.Show(" Modification Avec Succes. ",
                                   Resources.NomApplication,
                                     MessageBoxButtons.OK,
                                     MessageBoxIcon.Information,
                                     MessageBoxDefaultButton.Button1);
                }
            }
            catch (Exception)
            {
                XtraMessageBox.Show(" échec de la Modification. ",
                      Resources.NomApplication,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                Actualiser();
            }



        }
        //public void ChargePoste()
        //{
        //    bRowValide = true;
        //    ShiftCollection Poste = ShiftCollection.Charger();
        //    for (int i = 0; i < Poste.Count; i++)
        //    {
        //        Pointage_Poste PstTravaille = Poste[i];
        //        this.GridVPoste.AddNewRow();
        //        this.GridVPoste.SetFocusedRowCellValue("Code", PstTravaille.CPoste);
        //        this.GridVPoste.SetFocusedRowCellValue("Libellé", PstTravaille.DesigPoste);
        //        this.GridVPoste.SetFocusedRowCellValue("Service", PstTravaille.CService);
        //        this.GridVPoste.SetFocusedRowCellValue("Retard Permis(min)", PstTravaille.RetardPermise);
        //        this.GridVPoste.SetFocusedRowCellValue("Sortie(min)", PstTravaille.SortiePermise);
        //        this.GridVPoste.SetFocusedRowCellValue("Poste Nuit", PstTravaille.BNUIT);
        //        this.GridVPoste.SetFocusedRowCellValue("modifier", 1);
        //        this.GridVPoste.UpdateCurrentRow();

        //    }
        //    bRowValide = false;
        //}
        public void loadData()
        {
            CtrlHelper.InitGridView(GridVPoste, TitresPoste(), true);
            RemplirGrid();
            //ChargePoste();
        }
        public void Actualiser()
        {
            breload = false;
            loadData();
        }


        public void Enregistrer(bool enregistrerEtFermer)
        {
            DialogResult dialogResult1 = XtraMessageBox.Show("Voulez-vous Enregistrer ?",
                                                                        Resources.NomApplication,
                                                                        MessageBoxButtons.YesNo,
                                                                        MessageBoxIcon.Question,
                                                                        MessageBoxDefaultButton.Button1);

            if (dialogResult1 == DialogResult.No)
            {
                Actualiser();
            }
            else
            {
                for (int i = 0; i < GridVPoste.RowCount; i++)
                {
                    //if (GridVPoste.GetRowCellDisplayText(i, "modifier") != "1")
                    //{
                        Shift Poste = new Shift();
                        Poste.Code_Shift = GridVPoste.GetFocusedRowCellValue("Code").ToString();
                        Poste.Lib_Shift = GridVPoste.GetFocusedRowCellValue("Libellé").ToString();
                        Poste.Jour_Semaine = GridVPoste.GetFocusedRowCellDisplayText("Jour/Semaine").ToString();
                        if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()))
                            Poste.Heure_Debut = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Debut").ToString()).TimeOfDay;
                        if (!string.IsNullOrWhiteSpace(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()))
                            Poste.Heure_Fin = DateTime.Parse(GridVPoste.GetFocusedRowCellValue("Heure Fin").ToString()).TimeOfDay;
                        //try
                        //{
                        //    Poste.BNUIT = bool.Parse(GridVPoste.GetRowCellValue(i, "Poste Nuit").ToString());
                        //}
                        //catch (Exception) { Poste.BNUIT = false; }
                        //if (GridVPoste.GetRowCellDisplayText(i, "modifier") == "0")
                        //{
                        Poste.CreePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                        Poste.DateInsertion = DateTime.Now;
                        Poste.PCInsertion = Environment.UserName;
                    //    Poste.Sauvegarder();
                    //      }
                    //    else if (GridVPoste.GetRowCellDisplayText(i, "modifier") == "2")
                    //{
                        Poste.ModifiePar = GestionSession.UtilisateurCourant.IdUtilisateur;
                        Poste.DateModification = DateTime.Now;
                        Poste.PCModification = Environment.UserName;
                        Poste.Sauvegarder();
                    }
                    //}
               // }
            }
        }
        private void warning(string msg)
        {
            XtraMessageBox.Show(msg,
                                         "Alerte",
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button1);
        }
        private void GridVPoste_KeyDown(object sender, KeyEventArgs e)
        {
            string msgEchoue = "Impossible de supprimer cet Element?! ";
            string msgSucces = "Suppression effectuée avec succès!";
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                if (this.GridVPoste.SelectedRowsCount == 0)
                    return;

                 DialogResult dialogResult1 = XtraMessageBox.Show("Êtes-vous sûr de vouloir supprimer Ce Poste ?",
                                                                         Resources.NomApplication,
                                                                         MessageBoxButtons.YesNo,
                                                                         MessageBoxIcon.Question,
                                                                         MessageBoxDefaultButton.Button1);

                 if (dialogResult1 == DialogResult.No)
                 {
                     return;
                 }
                 else
                 {
                     try
                     {
                         Shift shift = new Shift();
                         shift.Code = this.GridVPoste.GetFocusedRowCellDisplayText(GridVPoste.Columns[0]);
                         shift.Supprimer();
                     }
                     catch (Exception)
                     {
                         warning(msgEchoue);
                     }
                 }
            }
        }
    }
}