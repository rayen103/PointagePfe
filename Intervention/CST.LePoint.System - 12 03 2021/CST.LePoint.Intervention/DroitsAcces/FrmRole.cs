using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.GestionActions;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraLayout.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Configuration;
using CST.LePoint.Securite;

namespace CST.LePoint.Intervention.DroitsAcces
{
    public partial class FrmRole : XtraForm, IActionsSave
    {
        #region - Attributs -

        private readonly List<Actions> ActionsValeurs =
            new List<Actions>((IEnumerable<Actions>)Enum.GetValues(typeof(Actions)))
                .Where(o => o != Actions.Rien).ToList();

        private readonly IDictionary<string, Dictionary<Actions, bool>> autorisations
            = new Dictionary<string, Dictionary<Actions, bool>>();

        private readonly BindingList<Aut> bindingList = new BindingList<Aut>();
        private readonly RepositoryItem checkEdit = new RepositoryItemCheckEdit { Caption = "" };

        private readonly RepositoryItem disabledCheckEdit = new RepositoryItemCheckEdit
        {
            Enabled = false,
            Caption = "",
            AllowGrayed = true
        };

        private readonly Role role;

        private IDictionary<string, Actions> ActionsApplication;

        private Dictionary<string, string> Formulaires;
        private bool addMode;
        private GridColumn colonneNomForm;

        private class Aut
        {
            public string NomForm { get; set; }
        }

        #endregion - Attributs -

        #region - Méthodes -

        public FrmRole(Role r = null)
        {
            addMode = r == null;
            role = r ?? new Role();
            InitializeComponent();
            InitialiserLabels();
            InitialiserValidateur();
            if (r == null)
            {
                if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() != "ADMINISTRATION")
                    this.CB_Societe.Enabled = false;
            }
            else
            {
                this.CB_Societe.Enabled = false;
            }
            loadSociete();
            LoadData();
        }

        public void Enregistrer(bool enregistrerEtFermer)
        {
            bool rolefound = false;
            if (ValidateChildren())
            {
                if ((string.IsNullOrEmpty(this.txtNomRole.Text)) || string.IsNullOrWhiteSpace(this.txtNomRole.Text))
                {
                    XtraMessageBox.Show("Le Nom du Rôle est vide !", Resources.NomApplication,
                                                     MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.txtNomRole.Focus();
                    return;
                }
                if (CB_Societe.SelectedValue == null)
                {
                    XtraMessageBox.Show("Aucune société n'a été sélectionnée !", Resources.NomApplication,
                                                     MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.CB_Societe.Focus();
                    return;
                }

                txtNomRole.EditValue = txtNomRole.Text.Trim();
                txtDescRole.EditValue = txtDescRole.Text.Trim();
                string selval = string.IsNullOrEmpty(CB_Societe.SelectedValue.ToString()) ? GestionSession.SocieteCourante.CSociete : CB_Societe.SelectedValue.ToString();
                if (role.Nom != txtNomRole.Text && GestionContexteSecurite.ContexteActive.Set<Role>().Any(r => r.Nom == txtNomRole.Text && r.CSociete == selval))
                {
                    txtNomRole.ErrorText = "Role existant!!"; //TODO To Ressource
                    return;
                }
                txtNomRole.ErrorText = null;

                if (!addMode)
                {
                    DialogResult dr = XtraMessageBox.Show(Resources.InfoMsg_MAJEnregistrement, Resources.NomApplication,
                                                          MessageBoxButtons.YesNo);
                    switch (dr)
                    {
                        case DialogResult.Cancel:
                            return;

                        case DialogResult.No:
                            Close();
                            return;

                        case DialogResult.Yes:
                            rolefound = true;
                            break;
                    }
                }
                role.Nom = txtNomRole.Text;
                role.Description = txtDescRole.Text;
                role.CSociete = CB_Societe.SelectedValue.ToString();
                role.Societe = CB_Societe.Text;
                GestionContexteSecurite.ContexteActive.Set<Role>().Remove(role);

                GestionContexteSecurite.ContexteActive.Set<Role>().Add(role);
                GestionContexteSecurite.ContexteActive.Enregistrer();

                //role.Autorisations.Clear();
                foreach (var item in autorisations)
                {
                    var aut = new Autorisation { NomForm = item.Key };
                    foreach (var op in item.Value)
                        if (op.Value && op.Key != Actions.Rien)
                            aut.AddOperation(op.Key);

                    GestionAutorisation.AttribuerAutorisation(role, aut);
                }

                GestionContexteSecurite.ContexteActive.Enregistrer();
                grid.Tag = null;

                if (addMode)
                {
                    addMode = false;
                    Text += @": " + role.Nom;
                    txtIdRole.EditValue = role.Id.ToString();
                    lblIdRole.Visibility = LayoutVisibility.Always;
                }
                ((FrmMDI)MdiParent).ConfigurerMenu();

                if (enregistrerEtFermer)
                    Close();
                else
                {
                    if (rolefound)
                    {
                        XtraMessageBox.Show("Role a été modifié avec succès");
                    }
                    else XtraMessageBox.Show("Role a été ajouté avec succès");
                }
            }
        }

        #region - Méthodes d'initialisation et d'actualisation -

        public void Actualiser()
        {
            //Chargement des tous formulaires + Actions de l'application Mira
            //ChargerActionsApplication(out Formulaires, out ActionsApplication);

            txtIdRole.Enabled = false;
            if (addMode)
                lblIdRole.Visibility = LayoutVisibility.OnlyInCustomization;

            txtIdRole.EditValue = role.Id.ToString();
            txtNomRole.EditValue = role.Nom;
            txtDescRole.EditValue = role.Description;
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
            {
                if (role.CSociete != null)
                    this.CB_Societe.SelectedValue = role.CSociete;
            }
            InitialiserGrid();

            grid.Tag = null;
            txtIdRole.IsModified = false;
            txtNomRole.IsModified = false;
            txtDescRole.IsModified = false;
        }

        private void LoadData()
        {
            //Chargement des tous formulaires + Actions de l'application Mira
            ChargerActionsApplication(out Formulaires, out ActionsApplication);

            Actualiser();
        }

        //TODO change to strings
        private void InitialiserLabels()
        {
            lblIdRole.Text = Resources.lblIdentifiant;
            lblNomRole.Text = Resources.lblNom;
            lblDescRole.Text = Resources.lblDescription;
            lblListeAutorisation.Text = Resources.lblListeAutorisation;
        }

        private void InitialiserValidateur()
        {
            //txtNomRole.SetValidation(@".+", "Nom");
        }

        #endregion - Méthodes d'initialisation et d'actualisation -

        #region - Méthodes du grid -

        private void InitialiserGrid()
        {
            gridV.Columns.Clear();
            autorisations.Clear();
            checkEdit.EditValueChanged += OnCheckEditOnEditValueChanged;
            colonneNomForm = gridV.Columns.AddVisible(@"NomForm", "Ecran");

            colonneNomForm.OptionsColumn.AllowEdit = false;
            colonneNomForm.SortMode = ColumnSortMode.DisplayText;
            //gcn.UnboundType = UnboundColumnType.String;

            foreach (Actions val in ActionsValeurs)
            {
                GridColumn gc = gridV.Columns.AddVisible(val.ToString(), val.ToString());
                gc.UnboundType = UnboundColumnType.Boolean;
            }

            //List<string> frmCaptions = formulaires.Values.ToList();
            //frmCaptions.Sort((n1, n2) => string.CompareOrdinal(FormScanner.Forms[n1], FormScanner.Forms[n2]));
            List<KeyValuePair<string, string>> fkv = Formulaires.ToList();
            fkv.Sort((n1, n2) => string.CompareOrdinal(Formulaires[n1.Key], Formulaires[n2.Key]));
            bindingList.Clear();
            foreach (var item in fkv)
            {
                bindingList.Add(new Aut { NomForm = item.Key });
            }

            foreach (string item in Formulaires.Keys)
            {
                Dictionary<Actions, bool> list = ActionsValeurs.ToDictionary(op => op,
                                                                             op =>
                                                                             GestionRole.EstAffecte(item, op, role));
                autorisations.Add(item, list);
            }

            grid.DataSource = bindingList;
            //Application.DoEvents();
            colonneNomForm.Width = colonneNomForm.GetBestWidth();
        }

        private void ChargerActionsApplication(out Dictionary<string, string> formulaires,
                                               out IDictionary<string, Actions> actionsApplication)
        {
            formulaires = null;

            formulaires = CFEvenementForm.ChargerFormulaires(FrmMDI.CfMenuApplication,
                                                             ResourcesMenus.ResourceManager);
            actionsApplication = new Dictionary<string, Actions>();

            foreach (string frm in formulaires.Keys)
            {
                var action = Actions.Rien;
                foreach (Actions op in ActionsValeurs)
                {
                    if (op != Actions.Consulter)
                    {
                        string frmType = frm;
                        Type type = Type.GetType(frmType);
                        if (type != null)
                        {
                            if (type.FindMembers(MemberTypes.Method,
                                                 BindingFlags.Public | BindingFlags.Instance,
                                                 (mi, obj) => mi.Name == obj.ToString(),
                                                 op).Length != 0)
                                action |= op;
                        }
                    }
                    else
                        action |= op;
                }
                actionsApplication.Add(frm, action);
            }
        }

        private bool OperationAllouee(string fn, string frm)
        {
            Actions op;
            bool success = Enum.TryParse(fn, out op);
            if (success)
            {
                return OperationAllouee(op, frm);
            }
            return true;
        }

        private bool OperationAllouee(Actions op, string frm)
        {
            return (ActionsApplication[frm] & op) == op;
        }

        private void SelectionnerLigne(string frm, bool? selec = null)
        {
            List<Action> actions = autorisations[frm].Keys.
                                                      Select(item1 => (Action)(() =>
                                                      {
                                                          if (OperationAllouee(item1, frm))
                                                              autorisations[frm][item1] = selec ??
                                                                                          !autorisations[frm][item1];
                                                      })).ToList();
            actions.ForEach(a => a());
        }

        private void SelectionnerTout(bool? selec = true)
        {
            foreach (string frm in autorisations.Keys)
                SelectionnerLigne(frm, selec);
        }

        #endregion - Méthodes du grid -

        #endregion - Méthodes -

        #region - Evènements -

        private void gridV_CustomColumnDisplayText(object sender, CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column == colonneNomForm)
                e.DisplayText = Formulaires[e.Value.ToString()];
        }

        private void gridV_CustomUnboundColumnData(object sender, CustomColumnDataEventArgs e)
        {
            if (e.IsGetData)
            {
                if (e.Column != colonneNomForm)
                {
                    string frm = bindingList[e.ListSourceRowIndex].NomForm;
                    Actions op;
                    bool success = Enum.TryParse(e.Column.FieldName, out op);
                    //try
                    //{
                    if (success)
                    {
                        foreach (string frm1 in autorisations.Keys)
                        {
                            if (frm1 == frm)
                                e.Value = autorisations[frm][op]; //GestionRole.EstAffecte(frm, op, role);
                        }
                    }
                    else
                        throw new InvalidOperationException(string.Format("Operation inconnue: {0}", e.Column.FieldName));

                    //}
                    //catch
                    //{
                    //}
                }
            }
            else if (e.IsSetData)
            {
                if (e.Column != colonneNomForm)
                {
                    string frm = bindingList[e.ListSourceRowIndex].NomForm;
                    Actions op;
                    bool success = Enum.TryParse(e.Column.FieldName, out op);
                    if (success)
                    {
                        //if (op != Actions.Consulter)
                        //{
                        //    if ((bool)e.Value)
                        //        autorisations[frm][Actions.Consulter] = true;
                        //}
                        //else if (!(bool)e.Value)
                        //{
                        //    foreach (var o in ActionsValeurs)
                        //    {
                        //        autorisations[frm][o] = false;
                        //    }
                        //}

                        autorisations[frm][op] = (bool)e.Value;
                        grid.RefreshDataSource();
                    }
                    else
                        throw new InvalidOperationException(string.Format("Operation inconnue: {0}", e.Column.FieldName));
                }
            }
        }

        private void grid_DoubleClick(object sender, EventArgs e)
        {
            var frm = (string)gridV.GetRowCellValue(gridV.FocusedRowHandle, colonneNomForm);
            SelectionnerLigne(frm,
                              !(bool)
                               gridV.GetRowCellValue(gridV.FocusedRowHandle, gridV.Columns[Actions.Consulter.ToString()]));
            grid.RefreshDataSource();
        }

        private void chbtnSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            SelectionnerTout(chbtnSelectAll.Checked);
            gridV.FocusedColumn = colonneNomForm;
            grid.RefreshDataSource();
        }

        private void gridV_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
        {
            if (e.Column != colonneNomForm)
            {
                var frm = (string)gridV.GetRowCellValue(e.RowHandle, colonneNomForm);
                if (frm == null) return;
                if (!OperationAllouee(e.Column.FieldName, frm))
                {
                    e.RepositoryItem = disabledCheckEdit;
                }
                else
                {
                    e.RepositoryItem = checkEdit;
                }
            }
        }

        private void gridV_ShowingEditor(object sender, CancelEventArgs e)
        {
            var frm = (string)gridV.GetRowCellValue(gridV.FocusedRowHandle, colonneNomForm);
            var operation = (Actions)Enum.Parse(typeof(Actions), gridV.FocusedColumn.FieldName);
            e.Cancel = (ActionsApplication[frm] & operation) != operation;
        }

        private void OnCheckEditOnEditValueChanged(object s, EventArgs e)
        {
            var frm = (string)gridV.GetRowCellValue(gridV.FocusedRowHandle, colonneNomForm);
            var op = (Actions)Enum.Parse(typeof(Actions), gridV.FocusedColumn.FieldName);
            bool val = !(bool)gridV.GetRowCellValue(gridV.FocusedRowHandle, gridV.FocusedColumn);

            if (op == Actions.Consulter)
            {
                autorisations[frm][Actions.Consulter] = val;
                if (!val)
                {
                    foreach (Actions o in ActionsValeurs)
                    {
                        autorisations[frm][o] = false;
                    }
                }
            }
            else
            {
                if (val)
                    autorisations[frm][Actions.Consulter] = true;

                foreach (Actions o in ActionsValeurs)
                {
                    if (o == op)
                    {
                        autorisations[frm][o] = val;
                        break;
                    }
                }
            }

            grid.RefreshDataSource();
        }

        private void loadSociete()
        {
            CB_Societe.DataSource = Societe.Charger_collection();
            CB_Societe.DisplayMember = "Nom";
            CB_Societe.ValueMember = "CSociete";
            CB_Societe.SelectedIndex = -1;
            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() != "ADMINISTRATION")
                this.CB_Societe.SelectedValue = GestionSession.SocieteCourante.CSociete;
        }

        #endregion - Evènements -
    }
}