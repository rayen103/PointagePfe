using CST.LePoint.Referentiel;
using CST.LePoint.Tools;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary
{
    public partial class CtrlHelper
    {
        public static void ValidationProviderDeclare(DXValidationProvider dxValidationProvider1, Control ctrl)
        {
            if (ctrl.GetType().Name == "SpinEdit" || ctrl.GetType().Name == "ComboBoxEdit" || ctrl.GetType().Name == "TextEdit" || ctrl.GetType().Name == "LookUpEdit" || ctrl.GetType().Name == "DateEdit")
            {
                if ((ctrl.Tag != null))
                {
                    if (!string.IsNullOrEmpty(ctrl.Tag.ToString()))
                    {
                        string source = ctrl.Tag.ToString().Trim().ToUpper();
                        if (source.Contains("RQ"))
                        {
                            var conditionValidationRule = new ConditionValidationRule();
                            conditionValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
                            conditionValidationRule.ErrorText = "Champs obligatoire non renseigné !";
                            conditionValidationRule.ErrorType = ErrorType.Information;
                            dxValidationProvider1.SetValidationRule(ctrl, conditionValidationRule);
                            dxValidationProvider1.SetIconAlignment(ctrl, ErrorIconAlignment.MiddleRight);
                        }
                    }
                }
            }

            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlfils in ctrl.Controls)
                    ValidationProviderDeclare(dxValidationProvider1, ctrlfils);
            }
        }

        public static void InitValidationProvider(DXValidationProvider dxValidationProvider1, Control ctrl)
        {
            if (ctrl.GetType().Name == "SpinEdit" || ctrl.GetType().Name == "ComboBoxEdit" || ctrl.GetType().Name == "TextEdit" || ctrl.GetType().Name == "LookUpEdit" || ctrl.GetType().Name == "DateEdit")
            {
                if ((ctrl.Tag != null))
                {
                    if (!string.IsNullOrEmpty(ctrl.Tag.ToString()))
                    {
                        string source = ctrl.Tag.ToString().Trim().ToUpper();
                        if (source.Contains("RQ"))
                        {
                            ConditionValidationRule conditionValidationRule = new ConditionValidationRule();
                            conditionValidationRule.ConditionOperator = ConditionOperator.IsNotBlank;
                            conditionValidationRule.ErrorText = "Champs obligatoire non renseigné !";
                            conditionValidationRule.ErrorType = ErrorType.Information;
                            dxValidationProvider1.SetValidationRule(ctrl, conditionValidationRule);
                            dxValidationProvider1.SetIconAlignment(ctrl, ErrorIconAlignment.MiddleRight);
                        }
                    }
                }
            }

            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlfils in ctrl.Controls)
                    InitValidationProvider(dxValidationProvider1, ctrlfils);
            }
        }

        public static void EmptyControls(Control ctrl)
        {
            switch (ctrl.GetType().Name)
            {
                case "SpinEdit":
                    SpinEdit mySpinEdit = (SpinEdit)ctrl;
                    mySpinEdit.EditValue = string.Empty;
                    mySpinEdit.Text = string.Empty;
                    break;

                case "CalcEdit":
                    CalcEdit myCalcEdit = (CalcEdit)ctrl;
                    myCalcEdit.EditValue = string.Empty;
                    myCalcEdit.Text = string.Empty;
                    break;

                case "TextEdit":
                    TextEdit myText = (TextEdit)ctrl;
                    myText.Text = string.Empty;
                    myText.EditValue = string.Empty;
                    break;

                case "ComboBoxEdit":
                    ComboBoxEdit combox = (ComboBoxEdit)ctrl;
                    combox.Text = string.Empty;
                    combox.EditValue = string.Empty;
                    break;

                case "LookUpEdit":
                    LookUpEdit myLookUpEdit = (LookUpEdit)ctrl;
                    myLookUpEdit.EditValue = string.Empty;
                    break;

                case "DateEdit":
                    DateEdit myDateEdit = (DateEdit)ctrl;
                    myDateEdit.Text = string.Empty;
                    myDateEdit.EditValue = string.Empty;
                    break;

                case "CheckEdit":
                    CheckEdit myCheckEdit = (CheckEdit)ctrl;
                    myCheckEdit.Checked = false;
                    break;

                case "MemoEdit":
                    MemoEdit myMemoEdit = (MemoEdit)ctrl;
                    myMemoEdit.Text = string.Empty;
                    myMemoEdit.EditValue = string.Empty;
                    break;

                case "GridControl":

                    GridControl myGrid = (GridControl)ctrl;
                    for (int i = 0; i < myGrid.MainView.RowCount; i++)
                        myGrid.ViewCollection.Clear();

                    break;

                default:
                    break;
            }

            if (ctrl.Controls.Count > 0)
            {
                foreach (Control ctrlfils in ctrl.Controls)
                    EmptyControls(ctrlfils);
            }
        }

        public static void InitComboBoxEdit(ComboBoxEdit combo)
        {
            combo.Properties.AutoComplete = true;
            combo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
        }

        public static void FillComboBoxEdit(ComboBoxEdit myCombo, ItemCollection collection)
        {
            myCombo.Properties.DropDownRows = 12;
            myCombo.Properties.AutoComplete = true;
            myCombo.Properties.Items.BeginUpdate();
            try
            {
                myCombo.Properties.Items.Clear();
                foreach (var item in collection)
                    myCombo.Properties.Items.Add(item.Code);
            }
            finally
            {
                myCombo.Properties.Items.EndUpdate();
            }
            //myCombo.SelectedIndex = 0;
        }

        public static void FillLookUpEdit(LookUpEdit lookUp, ItemCollection collection)
        {
            FillLookUpEdit(lookUp, collection, false);
        }

        public static void FillLookUpEdit(LookUpEdit lookUp, ItemCollection collection, bool EstCombox)
        {
            lookUp.EditValue = null;
            lookUp.Properties.BeginUpdate();
            try
            {
                lookUp.Properties.ShowFooter = true;
                lookUp.Properties.ShowHeader = true;
                lookUp.Properties.ShowLines = true;
                lookUp.Properties.HotTrackItems = true;
                lookUp.Properties.CaseSensitiveSearch = false;
                lookUp.Properties.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
                lookUp.Properties.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
                lookUp.Properties.AutoSearchColumnIndex = 0;
                lookUp.Properties.DropDownItemHeight = 0;
                lookUp.Properties.NullText = string.Empty;

                if (EstCombox)
                {
                    ItemCollection ItemCollection = (ItemCollection)collection;
                    lookUp.Properties.DataSource = ItemCollection;
                    lookUp.Properties.ValueMember = ReflectionHelper.GetPropertyName<Item>(i => i.Code);
                    lookUp.Properties.DisplayMember = ReflectionHelper.GetPropertyName<Item>(i => i.Libelle);
                    lookUp.Properties.PopulateColumns();
                    lookUp.EditValue = 1;
                    lookUp.Properties.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Near;
                    lookUp.Properties.Columns[1].Alignment = DevExpress.Utils.HorzAlignment.Near;
                    for (var i = 1; i < lookUp.Properties.Columns.Count; i++)
                    {
                        lookUp.Properties.Columns[i].Visible = false;
                    }
                }
                else
                {
                    ItemCollection ItemCollection = (ItemCollection)collection;
                    lookUp.Properties.DataSource = ItemCollection;
                    lookUp.Properties.ValueMember = ReflectionHelper.GetPropertyName<Item>(i => i.Code);
                    lookUp.Properties.DisplayMember = ReflectionHelper.GetPropertyName<Item>(i => i.Libelle);
                    lookUp.Properties.PopulateColumns();
                    lookUp.EditValue = 1;

                    lookUp.Properties.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Near;
                    lookUp.Properties.Columns[1].Alignment = DevExpress.Utils.HorzAlignment.Near;
                    for (var i = 2; i < lookUp.Properties.Columns.Count; i++)
                    {
                        lookUp.Properties.Columns[i].Visible = false;
                    }
                }

                lookUp.Refresh();
            }
            catch (System.Exception )
            {

            }
            finally
            {
                lookUp.Properties.EndUpdate();
            }
        }

        public static decimal EditDecimal(decimal saisie)
        {
            if (saisie != 0)
                saisie = decimal.Parse(saisie.ToString(".###"));
            else
                saisie = 0;

            return (saisie);
        }
    }
}