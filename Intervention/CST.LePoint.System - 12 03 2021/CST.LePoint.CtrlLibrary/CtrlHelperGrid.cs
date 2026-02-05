using CST.LePoint.Referentiel;
using CST.LePoint.Tools;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Drawing;

namespace CST.LePoint.CtrlLibrary
{
    public partial class CtrlHelper
    {
        public static void ChargerRepositoryItemLookUpEdit(RepositoryItemLookUpEdit rlkplookUp, ItemCollection collection)
        {
            rlkplookUp.ShowFooter = true;
            rlkplookUp.ShowHeader = true;
            rlkplookUp.ShowLines = true;
            rlkplookUp.HotTrackItems = true;
            rlkplookUp.CaseSensitiveSearch = false;
            rlkplookUp.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            rlkplookUp.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            rlkplookUp.AutoSearchColumnIndex = 0;
            rlkplookUp.DropDownItemHeight = 0;
            rlkplookUp.NullText = string.Empty;

            rlkplookUp.DataSource = collection;
            rlkplookUp.ValueMember = ReflectionHelper.GetPropertyName<Item>(i => i.Code);
            rlkplookUp.DisplayMember = ReflectionHelper.GetPropertyName<Item>(i => i.Libelle);
            rlkplookUp.PopulateColumns();

            rlkplookUp.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Near;
            rlkplookUp.Columns[1].Alignment = DevExpress.Utils.HorzAlignment.Near;
            for (var i = 2; i < rlkplookUp.Columns.Count; i++)
                rlkplookUp.Columns[i].Visible = false;
            if (collection.Count != 0)
                rlkplookUp.NullText = collection[0].Libelle;
        }

        public static void ChargerRepositoryItemLookUpEditVide(RepositoryItemLookUpEdit rlkplookUp, ItemCollection collection)
        {
            rlkplookUp.ShowFooter = true;
            rlkplookUp.ShowHeader = true;
            rlkplookUp.ShowLines = true;
            rlkplookUp.HotTrackItems = true;
            rlkplookUp.CaseSensitiveSearch = false;
            rlkplookUp.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoFilter;
            rlkplookUp.HeaderClickMode = DevExpress.XtraEditors.Controls.HeaderClickMode.AutoSearch;
            rlkplookUp.AutoSearchColumnIndex = 0;
            rlkplookUp.DropDownItemHeight = 0;
            rlkplookUp.NullText = string.Empty;

            rlkplookUp.DataSource = collection;
            rlkplookUp.ValueMember = ReflectionHelper.GetPropertyName<Item>(i => i.Code);
            rlkplookUp.DisplayMember = ReflectionHelper.GetPropertyName<Item>(i => i.Libelle);
            rlkplookUp.PopulateColumns();

            rlkplookUp.Columns[0].Alignment = DevExpress.Utils.HorzAlignment.Near;
            rlkplookUp.Columns[1].Alignment = DevExpress.Utils.HorzAlignment.Near;
            for (var i = 2; i < rlkplookUp.Columns.Count; i++)
                rlkplookUp.Columns[i].Visible = false;
        }

        public static void FillGridView(GridView grdView, GvColumnProprietes proprietes, DataTable dt)
        {
            for (int i = 0; i < proprietes.Count; i++)
            {
                dt.Columns[i].ColumnName = proprietes[i].Titre;
            }
            grdView.GridControl.DataSource = dt;
            if (grdView.RowCount > 0)
                grdView.FocusedRowHandle = 0;
            grdView.BestFitColumns();

            //foreach (DataRow row in dt.Rows)
            //{
            //    grdView.AddNewRow();
            //    for (int i = 0; i < proprietes.Count; i++)
            //        grdView.SetFocusedRowCellValue(proprietes[i].Titre, row[i]);

            //    grdView.UpdateCurrentRow();
            //}
            //if (grdView.RowCount > 0)
            //    grdView.FocusedRowHandle = 0;

            //grdView.BestFitColumns();
              
            //grdView.OptionsView.ColumnAutoWidth = true;
        }

        public static void FillGridViewWithDataTable(GridView gridV, DataTable dt)
        {
            gridV.GridControl.DataSource = dt;
            if (gridV.RowCount > 0)
                gridV.FocusedRowHandle = 0;
            gridV.BestFitColumns();
            gridV.GridControl.UseEmbeddedNavigator = false;
            gridV.OptionsView.ColumnAutoWidth = true;
            gridV.OptionsBehavior.Editable = false;

        }

        public static void FillGridViewWithCollection(GridView grdView, GvColumnProprietes proprietes, object collection)
        {
            grdView.OptionsView.ColumnAutoWidth = true;
            grdView.GridControl.DataSource = collection;
            grdView.GridControl.UseEmbeddedNavigator = false;
            grdView.PopulateColumns();
            grdView.OptionsView.ColumnAutoWidth = true;
            grdView.OptionsBehavior.Editable = false;

            //int i = 0;
            //foreach (GvColumnPropriete propriete in proprietes)
            //{
            //    GridColumn grdColumn = grdView.Columns[i];
            //    grdColumn.Caption = propriete.Titre;
            //    grdColumn.OptionsColumn.AllowEdit = true;
            //    switch (propriete.Type)
            //    {
            //        case GvColumnPropriete.GvColumnType.String:
            //            grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            //            //
            //            break;

            //        case GvColumnPropriete.GvColumnType.Date:
            //            RepositoryItemDateEdit repItemDateEdit = new RepositoryItemDateEdit();
            //            repItemDateEdit.Mask.EditMask = "dd/MM/yyyy";
            //            repItemDateEdit.Mask.MaskType = MaskType.DateTime;
            //            repItemDateEdit.DisplayFormat.FormatString = "dd/MM/yyyy";
            //            repItemDateEdit.DisplayFormat.FormatType = FormatType.DateTime;
            //            grdColumn.ColumnEdit = repItemDateEdit;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //            break;

            //        case GvColumnPropriete.GvColumnType.DateTime:
            //            RepositoryItemDateEdit repItemDateTimeEdit = new RepositoryItemDateEdit();
            //            repItemDateTimeEdit.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            //            repItemDateTimeEdit.DisplayFormat.FormatType = FormatType.DateTime;
            //            grdColumn.ColumnEdit = repItemDateTimeEdit;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //            break;

            //        case GvColumnPropriete.GvColumnType.Time:
            //            RepositoryItemTimeEdit repItemTime = new RepositoryItemTimeEdit();
            //            grdColumn.ColumnEdit = repItemTime;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //            break;

            //        case GvColumnPropriete.GvColumnType.Decimal:
            //            RepositoryItemSpinEdit repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
            //            repositoryItemSpinEdit1.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //            repositoryItemSpinEdit1.IsFloatValue = true;
            //            grdColumn.ColumnEdit = repositoryItemSpinEdit1;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            break;

            //        case GvColumnPropriete.GvColumnType.Integer:
            //            var repositoryItemSpinEdit = new RepositoryItemSpinEdit();
            //            repositoryItemSpinEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            //            repositoryItemSpinEdit.IsFloatValue = false;
            //            grdColumn.ColumnEdit = repositoryItemSpinEdit;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            break;

            //        case GvColumnPropriete.GvColumnType.Percent:
            //            grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
            //            grdColumn.DisplayFormat.FormatString = "p";
            //            grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            //            break;

            //        case GvColumnPropriete.GvColumnType.Boolean:

            //            RepositoryItemCheckEdit chkedit = new RepositoryItemCheckEdit();
            //            grdColumn.ColumnEdit = chkedit;
            //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //            break;
            //    }

            //    switch (propriete.Etat)
            //    {
            //        case GvColumnPropriete.GvColumnEtat.Invisible:
            //            grdColumn.Visible = false;
            //            break;

            //        case GvColumnPropriete.GvColumnEtat.Disable:
            //            grdColumn.Visible = true;
            //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            //            grdColumn.View.OptionsBehavior.Editable = false;
            //            grdColumn.OptionsColumn.ReadOnly = true;
            //            break;

            //        case GvColumnPropriete.GvColumnEtat.Enable:
            //            grdColumn.Visible = true;
            //            grdColumn.View.OptionsBehavior.Editable = true;
            //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
            //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
            //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
            //            break;
            //    }
            //    i++;
            //}

            //while (grdView.Columns.Count > i)
            //{
            //    GridColumn grdColumn = grdView.Columns[i];
            //    grdColumn.Visible = false;
            //    i++;
            //}
        }

        public static int GetIndiceRow(GridView grdView, string strSearch, int indiceColonne)
        {
            int numLigne = -1;
            int i = 0;
            while ((i < grdView.RowCount) && (numLigne == -1))
            {
                if (grdView != null)
                {
                    string strValue = grdView.GetRowCellDisplayText(i, grdView.Columns[indiceColonne]);
                    if (strValue == strSearch)
                        numLigne = i;
                }
                i++;
            }
            return (numLigne);
        }

        public static void InitGridViewWithoutCurser(GridView grdView, GvColumnProprietes proprietes)
        {
            InitGridViewWithoutCurser(grdView, proprietes, false);
        }

        public static void InitGridViewWithoutCurser(GridView grdView, GvColumnProprietes proprietes, bool bEditable)
        {
            //grdView.GridControl.UseEmbeddedNavigator = false;
            grdView.BorderStyle = BorderStyles.Default;
            grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
            grdView.OptionsSelection.EnableAppearanceHideSelection = false;
            grdView.OptionsSelection.MultiSelect = false;
            grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

            grdView.OptionsView.ShowIndicator = true;
            grdView.OptionsView.ShowGroupPanel = false;
            grdView.OptionsView.ShowAutoFilterRow = false;
            grdView.OptionsView.ColumnAutoWidth = true;
            grdView.OptionsView.ShowColumnHeaders = true;
            grdView.OptionsView.ShowFooter = false;
            grdView.OptionsView.ShowGroupedColumns = true;
            grdView.OptionsView.ShowViewCaption = false;
            grdView.OptionsView.EnableAppearanceEvenRow = true;
            grdView.OptionsView.EnableAppearanceOddRow = true;

            //grdView.GridControl.UseEmbeddedNavigator = true;
            //grdView.GridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            //grdView.GridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;

            grdView.OptionsBehavior.Editable = true;

            //grdView.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
            //grdView.ShowButtonMode = ShowButtonModeEnum.Default;
            grdView.Columns.Clear();

            var dt = new DataTable();

            foreach (GvColumnPropriete propriete in proprietes)
            {
                switch (propriete.Type)
                {
                    case GvColumnPropriete.GvColumnType.LookUp:
                    case GvColumnPropriete.GvColumnType.LookUpVide:
                    case GvColumnPropriete.GvColumnType.Button:
                    case GvColumnPropriete.GvColumnType.String:
                        dt.Columns.Add(propriete.Titre, typeof(String));
                        break;

                    case GvColumnPropriete.GvColumnType.Date:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.DateTime:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Time:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Decimal:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.DecimalPositif:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.Integer:
                        dt.Columns.Add(propriete.Titre, typeof(int));
                        break;

                    case GvColumnPropriete.GvColumnType.Percent:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Currency:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Boolean:
                        dt.Columns.Add(propriete.Titre, typeof(bool));
                        break;
                }
            }

            DataView dv = dt.DefaultView;

            grdView.GridControl.DataSource = dv;
            grdView.PopulateColumns();

            if (bEditable)
            {
                // grdView.OptionsCustomization.AllowColumnMoving = false;
                //grdView.OptionsCustomization.AllowColumnResizing = false;
                grdView.OptionsCustomization.AllowFilter = false;
                grdView.OptionsCustomization.AllowGroup = false;
                // grdView.OptionsCustomization.AllowSort = false;
                grdView.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grdView });
                grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                grdView.RefreshData();
            }
            //else
            //    grdView.OptionsBehavior.Editable = false ;

            foreach (GvColumnPropriete propriete in proprietes)
            {
                foreach (GridColumn v in grdView.Columns)
                {
                    if (propriete.Titre == v.FieldName)
                    {
                        v.Caption = propriete.Titre;
                        v.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                        SetColumnType(propriete, v);
                        SetColumnEtat(propriete, v);
                        SetColumnMerge(propriete, v);
                        break;
                    }
                }
            }

            grdView.BestFitColumns();
        }

        public static void InitGridView(GridView grdView, GvColumnProprietes proprietes)
        {
            InitGridView(grdView, proprietes, false);
        }

        public static void InitGridView(GridView grdView, GvColumnProprietes proprietes, bool bEditable)
        {
            //grdView.GridControl.UseEmbeddedNavigator = false;
            grdView.BorderStyle = BorderStyles.Default;
            grdView.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
            grdView.OptionsSelection.EnableAppearanceHideSelection = false;
            grdView.OptionsSelection.MultiSelect = false;
            grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

            grdView.OptionsView.ShowIndicator = true;
            grdView.OptionsView.ShowGroupPanel = false;
            grdView.OptionsView.ShowAutoFilterRow = false;
            grdView.OptionsView.ColumnAutoWidth = true;
            grdView.OptionsView.ShowColumnHeaders = true;
            grdView.OptionsView.ShowFooter = false;
            grdView.OptionsView.ShowGroupedColumns = true;
            grdView.OptionsView.ShowViewCaption = false;
            grdView.OptionsView.EnableAppearanceEvenRow = true;
            grdView.OptionsView.EnableAppearanceOddRow = true;

            grdView.GridControl.UseEmbeddedNavigator = true;
            grdView.GridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            grdView.GridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;

            grdView.OptionsBehavior.Editable = true;

            //grdView.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
            //grdView.ShowButtonMode = ShowButtonModeEnum.Default;
            grdView.Columns.Clear();

            var dt = new DataTable();

            foreach (GvColumnPropriete propriete in proprietes)
            {
                switch (propriete.Type)
                {
                    case GvColumnPropriete.GvColumnType.LookUp:
                    case GvColumnPropriete.GvColumnType.LookUpVide:
                    case GvColumnPropriete.GvColumnType.Button:
                    case GvColumnPropriete.GvColumnType.String:
                        dt.Columns.Add(propriete.Titre, typeof(String));
                        break;
                    case GvColumnPropriete.GvColumnType.Photos:
                        dt.Columns.Add(propriete.Titre, typeof(Image));
                        break;
                    case GvColumnPropriete.GvColumnType.Date:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.DateTime:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Time:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Decimal:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.DecimalPositif:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.Integer:
                        dt.Columns.Add(propriete.Titre, typeof(int));
                        break;

                    case GvColumnPropriete.GvColumnType.Percent:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Currency:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Boolean:
                        dt.Columns.Add(propriete.Titre, typeof(bool));
                        break;

                    case GvColumnPropriete.GvColumnType.Color:
                        dt.Columns.Add(propriete.Titre, typeof(Color));
                        break;

                    case GvColumnPropriete.GvColumnType.Memo:
                        dt.Columns.Add(propriete.Titre, typeof(string));
                        grdView.OptionsView.RowAutoHeight = true;
                        break;
                }
            }

            DataView dv = dt.DefaultView;

            grdView.GridControl.DataSource = dv;
            grdView.PopulateColumns();

            if (bEditable)
            {
                // grdView.OptionsCustomization.AllowColumnMoving = false;
                //grdView.OptionsCustomization.AllowColumnResizing = false;
                grdView.OptionsCustomization.AllowFilter = false;
                grdView.OptionsCustomization.AllowGroup = false;
                // grdView.OptionsCustomization.AllowSort = false;
                grdView.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grdView });
                grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
                grdView.RefreshData();
            }
            //else
            //    grdView.OptionsBehavior.Editable = false ;

            foreach (GvColumnPropriete propriete in proprietes)
            {
                foreach (GridColumn v in grdView.Columns)
                {
                    if (propriete.Titre == v.FieldName)
                    {
                        v.Caption = propriete.Titre;
                        v.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                        SetColumnType(propriete, v);
                        SetColumnEtat(propriete, v);
                        SetColumnMerge(propriete, v);
                        break;
                    }
                }
            }

            grdView.BestFitColumns();
        }

        public static void InitGridViewSel(GridView grdView, GvColumnProprietes proprietes)
        {
            grdView.GridControl.UseEmbeddedNavigator = false;
            grdView.BorderStyle = BorderStyles.Default;
            grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
            grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

            grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
            grdView.OptionsSelection.EnableAppearanceHideSelection = false;
            grdView.OptionsSelection.MultiSelect = false;
            grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

            grdView.OptionsView.ShowIndicator = true;
            grdView.OptionsView.ShowGroupPanel = false;
            grdView.OptionsView.ShowAutoFilterRow = false;
            grdView.OptionsView.ColumnAutoWidth = true;
            grdView.OptionsView.ShowColumnHeaders = true;
            grdView.OptionsView.ShowFooter = false;
            grdView.OptionsView.ShowGroupedColumns = true;
            grdView.OptionsView.ShowViewCaption = false;
            grdView.OptionsView.EnableAppearanceEvenRow = true;
            grdView.OptionsView.EnableAppearanceOddRow = true;

            grdView.OptionsBehavior.Editable = true;

            //grdView.OptionsBehavior.EditorShowMode = EditorShowMode.Click;
            //grdView.ShowButtonMode = ShowButtonModeEnum.Default;
            grdView.Columns.Clear();

            var dt = new DataTable();

            foreach (GvColumnPropriete propriete in proprietes)
            {
                switch (propriete.Type)
                {
                    case GvColumnPropriete.GvColumnType.LookUp:
                    case GvColumnPropriete.GvColumnType.LookUpVide:
                    case GvColumnPropriete.GvColumnType.Button:
                    case GvColumnPropriete.GvColumnType.String:
                        dt.Columns.Add(propriete.Titre, typeof(String));
                        break;

                    case GvColumnPropriete.GvColumnType.Date:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.DateTime:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Time:
                        dt.Columns.Add(propriete.Titre, typeof(DateTime));
                        break;

                    case GvColumnPropriete.GvColumnType.Decimal:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.DecimalPositif:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;
                    case GvColumnPropriete.GvColumnType.Integer:
                        dt.Columns.Add(propriete.Titre, typeof(int));
                        break;

                    case GvColumnPropriete.GvColumnType.Percent:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Currency:
                        dt.Columns.Add(propriete.Titre, typeof(decimal));
                        break;

                    case GvColumnPropriete.GvColumnType.Boolean:
                        dt.Columns.Add(propriete.Titre, typeof(bool));
                        break;
                }
            }
            DataView dv = dt.DefaultView;
            grdView.GridControl.DataSource = dv;
            grdView.PopulateColumns();
            grdView.OptionsCustomization.AllowFilter = false;
            grdView.OptionsCustomization.AllowGroup = false;
            grdView.RefreshData();
            foreach (GvColumnPropriete propriete in proprietes)
            {
                foreach (GridColumn v in grdView.Columns)
                {
                    if (propriete.Titre == v.FieldName)
                    {
                        v.Caption = propriete.Titre;
                        v.OptionsFilter.FilterPopupMode = FilterPopupMode.CheckedList;
                        SetColumnType(propriete, v);
                        SetColumnEtat(propriete, v);
                        SetColumnMerge(propriete, v);
                        break;
                    }
                }
            }

            grdView.BestFitColumns();
        }

        private static void SetColumnEtat(GvColumnPropriete propriete, GridColumn grdColumn)
        {
            switch (propriete.Etat)
            {
                case GvColumnPropriete.GvColumnEtat.Invisible:
                    grdColumn.Visible = false;
                    grdColumn.OptionsColumn.AllowEdit = false;
                    //grdColumn.OptionsColumn.AllowFocus = false;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    grdColumn.OptionsColumn.ReadOnly = true;
                    break;

                case GvColumnPropriete.GvColumnEtat.Disable:
                    grdColumn.Visible = true;
                    grdColumn.OptionsColumn.AllowEdit = false;
                    // grdColumn.OptionsColumn.AllowFocus = false;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
                    grdColumn.OptionsColumn.ReadOnly = true;
                    //grdColumn.AppearanceCell.BackColor = Color.Tan;
                    break;

                case GvColumnPropriete.GvColumnEtat.Enable:
                    grdColumn.Visible = true;
                    grdColumn.OptionsColumn.AllowEdit = true;
                    grdColumn.OptionsColumn.AllowFocus = true;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
                    grdColumn.View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
                    break;
            }
        }

        private static void SetColumnMerge(GvColumnPropriete propriete, GridColumn grdColumn)
        {
            switch (propriete.Merge)
            {
                case GvColumnPropriete.GvColumnMerge.NotAllowMerge:
                    grdColumn.OptionsColumn.AllowMerge = DefaultBoolean.False;
                    break;

                case GvColumnPropriete.GvColumnMerge.AllowMerge:
                    grdColumn.OptionsColumn.AllowMerge = DefaultBoolean.True;
                    break;

                case GvColumnPropriete.GvColumnMerge.Default:
                    grdColumn.OptionsColumn.AllowMerge = DefaultBoolean.Default;
                    break;
            }
        }

        private static void SetColumnType(GvColumnPropriete propriete, GridColumn grdColumn)
        {
            switch (propriete.Type)
            {
                case GvColumnPropriete.GvColumnType.DateTime:
                    grdColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                    grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grdColumn.DisplayFormat.FormatString = "dd/MM/yyyy hh:mm";
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Default;
                    break;

                case GvColumnPropriete.GvColumnType.Date:
                    grdColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
                    grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    grdColumn.DisplayFormat.FormatString = "d";
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    break;

                case GvColumnPropriete.GvColumnType.Time:
                    RepositoryItemDateEdit rpsdateEdit = new RepositoryItemDateEdit();
                    rpsdateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                    rpsdateEdit.EditFormat.FormatString = "HH:mm";
                    rpsdateEdit.DisplayFormat.FormatString = "HH:mm";
                    rpsdateEdit.EditMask = "HH:mm";
                    
                    grdColumn.ColumnEdit = rpsdateEdit;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                    break;

                case GvColumnPropriete.GvColumnType.Decimal:
                    RepositoryItemCalcEdit calcEdit = new RepositoryItemCalcEdit();
                    calcEdit.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    calcEdit.DisplayFormat.FormatString = "d";

                    calcEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grdColumn.ColumnEdit = calcEdit;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    break;

                case GvColumnPropriete.GvColumnType.DecimalPositif:
                    RepositoryItemCalcEdit calcEditP = new RepositoryItemCalcEdit();
                    calcEditP.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    // calcEditP.DisplayFormat.FormatString = "d";
                    calcEditP.Properties.MaxLength = 16;
                    // calcEditP.Properties.Mask.EditMask = "#,################0.000;";
                    calcEditP.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grdColumn.ColumnEdit = calcEditP;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    break;

                case GvColumnPropriete.GvColumnType.Integer:
                    var spinEditInt = new RepositoryItemSpinEdit();
                    spinEditInt.IsFloatValue = false;
                    spinEditInt.Properties.MaxValue = new decimal(new int[] {
                    2147287040,
                    0,
                    0,
                    0});
                    spinEditInt.Properties.MinValue = new decimal(new int[] {
                    1,
                    0,
                    0,
                    -2147287040});
                    spinEditInt.Mask.EditMask = "N00";
                    grdColumn.ColumnEdit = spinEditInt;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    break;

                case GvColumnPropriete.GvColumnType.Percent:
                    RepositoryItemSpinEdit spinEditPercent = new RepositoryItemSpinEdit();
                    spinEditPercent.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    spinEditPercent.IsFloatValue = true;
                    spinEditPercent.DisplayFormat.FormatString = "p";
                    spinEditPercent.Properties.Increment = new decimal(new int[] {
                    1,
                    0,
                    0,
                    131072});
                    spinEditPercent.Properties.Mask.EditMask = "#,##0.00;";
                    spinEditPercent.Properties.Mask.UseMaskAsDisplayFormat = true;
                    spinEditPercent.Properties.MaxValue = new decimal(new int[] {
                    100,
                    0,
                    0,
                    0});
                    spinEditPercent.Properties.MinValue = new decimal(new int[] {
                    1,
                    0,
                    0,
                    -2147287040});
                    spinEditPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    grdColumn.ColumnEdit = spinEditPercent;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    break;

                case GvColumnPropriete.GvColumnType.Currency:
                    RepositoryItemCalcEdit calcEditCurrency = new RepositoryItemCalcEdit();
                    calcEditCurrency.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
                    calcEditCurrency.Mask.EditMask = "c";
                    calcEditCurrency.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    calcEditCurrency.Properties.Mask.EditMask = "#,##############0.000;";
                    calcEditCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    calcEditCurrency.DisplayFormat.FormatString = "c";
                    grdColumn.ColumnEdit = calcEditCurrency;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                    break;

                case GvColumnPropriete.GvColumnType.Boolean:
                    grdColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                    grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                    grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    break;

                case GvColumnPropriete.GvColumnType.LookUp:
                    RepositoryItemLookUpEdit rlkplookUp = new RepositoryItemLookUpEdit();
                    ChargerRepositoryItemLookUpEdit(rlkplookUp, propriete.ItemCollection);
                    grdColumn.ColumnEdit = rlkplookUp;
                    break;

                case GvColumnPropriete.GvColumnType.Photos:
                    RepositoryItemPictureEdit itemImage = new RepositoryItemPictureEdit();
                    itemImage.SizeMode = PictureSizeMode.Squeeze;
                    // aymen added this line to make sur the image stored as byte
                    // itemImage.PictureStoreMode = PictureStoreMode.ByteArray;
                    grdColumn.ColumnEdit = itemImage;
                    break;

                case GvColumnPropriete.GvColumnType.LookUpVide:
                    RepositoryItemLookUpEdit rlkplookUpV = new RepositoryItemLookUpEdit();
                    ChargerRepositoryItemLookUpEditVide(rlkplookUpV, propriete.ItemCollection);
                    grdColumn.ColumnEdit = rlkplookUpV;
                    break;

                case GvColumnPropriete.GvColumnType.Button:
                    RepositoryItemButtonEdit rlkpButtonEdit = new RepositoryItemButtonEdit();
                    DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
                    rlkpButtonEdit.AutoHeight = false;
                    rlkpButtonEdit.Buttons.RemoveAt(0);
                    rlkpButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
                    new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Ajouter", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
                    rlkpButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
                    //rlkpButtonEdit.Appearance.GetFont()
                    //rlkpButtonEdit.AllowFocused = true;
                    //rlkpButtonEdit.AllowNullInput = DefaultBoolean.Default;
                    //rlkpButtonEdit.DefaultAlignment = HorzAlignment.Center;
                    //rlkpButtonEdit.NullText = propriete.ToolTip;
                    //rlkpButtonEdit.s;

                    rlkpButtonEdit.Enabled = true;
                    rlkpButtonEdit.NullValuePrompt = propriete.ToolTip;
                    grdColumn.ColumnEdit = rlkpButtonEdit;
                    grdColumn.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

                    break;
                case GvColumnPropriete.GvColumnType.Color:
                    RepositoryItemColorEdit rpsColorEdit = new RepositoryItemColorEdit();
                    rpsColorEdit.ShowSystemColors = false;
                    rpsColorEdit.ShowCustomColors = false;
                    //grdColumn.UnboundType = DevExpress.Data.UnboundColumnType.String;
                    grdColumn.ColumnEdit = rpsColorEdit;
                    break;
                                
                case GvColumnPropriete.GvColumnType.Memo:
                    RepositoryItemMemoEdit rpsMemoEdit = new RepositoryItemMemoEdit();
                    //rpsMemoEdit.Appearance.Options.UseTextOptions = true;
                    //rpsMemoEdit.AutoHeight = true;
                    //rpsMemoEdit.Appearance.TextOptions.VAlignment = VertAlignment.Center;
                    grdColumn.ColumnEdit = rpsMemoEdit;
                    break;
            }
        }

        //public static void InitGridView(GridView grdView, GvColumnProprietes proprietes)
        //{
        //    //grdView.PaintStyleName = "MixedXP";
        //    grdView.BorderStyle = BorderStyles.Default;
        //    grdView.OptionsBehavior.Editable = true;
        //    grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
        //    grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

        //    grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
        //    grdView.OptionsSelection.EnableAppearanceHideSelection = false;
        //    grdView.OptionsSelection.MultiSelect = false;
        //    grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

        //    grdView.OptionsView.ShowIndicator = true;
        //    grdView.OptionsView.ShowGroupPanel = false;
        //    grdView.OptionsView.ShowAutoFilterRow = false;
        //    grdView.OptionsView.ColumnAutoWidth = true;
        //    grdView.OptionsView.ShowColumnHeaders = true;
        //    grdView.OptionsView.ShowFooter = false;
        //    grdView.OptionsView.ShowGroupedColumns = true;
        //    grdView.OptionsView.ShowViewCaption = false;

        //    grdView.OptionsView.EnableAppearanceEvenRow = true;
        //    grdView.OptionsView.EnableAppearanceOddRow = true;
        //    grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        //    grdView.OptionsBehavior.Editable = false;

        //    grdView.Columns.Clear();
        //    var dt = new DataTable();
        //    foreach (GvColumnPropriete propriete in proprietes)
        //    {
        //        dt.Columns.Add(propriete.Titre, typeof(string));
        //    }
        //    DataView dv = dt.DefaultView;
        //    grdView.GridControl.DataSource = dv;
        //    grdView.PopulateColumns();

        //    int i = 0;
        //    foreach (GvColumnPropriete propriete in proprietes)
        //    {
        //        switch (propriete.Etat)
        //        {
        //            case GvColumnPropriete.GvColumnEtat.Invisible:
        //                grdView.Columns[i].Visible = false;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Disable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Enable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
        //                break;
        //        }
        //        i++;
        //    }
        //}

        //public static void InitGridViewSaisie(GridView grdView, GvColumnProprietes proprietes)
        //{
        //    InitGridViewSaisie(grdView, proprietes, true);
        //}

        //public static void InitGridViewSaisie(GridView grdView, GvColumnProprietes proprietes, bool columnAutoWidth)
        //{
        //    grdView.GridControl.UseEmbeddedNavigator = false;
        //    grdView.BorderStyle = BorderStyles.Default;
        //    grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
        //    grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

        //    grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
        //    grdView.OptionsSelection.EnableAppearanceHideSelection = false;
        //    grdView.OptionsSelection.MultiSelect = false;
        //    grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

        //    grdView.OptionsView.ShowIndicator = true;
        //    grdView.OptionsView.ShowGroupPanel = false;
        //    grdView.OptionsView.ShowAutoFilterRow = false;
        //    grdView.OptionsView.ColumnAutoWidth = true;
        //    grdView.OptionsView.ShowColumnHeaders = true;
        //    grdView.OptionsView.ShowFooter = false;
        //    grdView.OptionsView.ShowGroupedColumns = true;
        //    grdView.OptionsView.ShowViewCaption = false;
        //    grdView.OptionsView.EnableAppearanceEvenRow = true;
        //    grdView.OptionsView.EnableAppearanceOddRow = true;
        //    grdView.Columns.Clear();

        //    var dt = new DataTable();
        //    foreach (GvColumnPropriete propriete in proprietes)
        //    {
        //        switch (propriete.Type)
        //        {
        //            case GvColumnPropriete.GvColumnType.String:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(string) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Date:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(DateTime) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Decimal:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(decimal) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Integer:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(int) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Percent:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(decimal) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Currency:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(decimal) });
        //                break;

        //            case GvColumnPropriete.GvColumnType.Boolean:
        //                dt.Columns.Add(new DataColumn { Caption = propriete.Titre, ColumnName = propriete.Titre, DataType = typeof(bool) });
        //                break;
        //        }
        //    }

        //    DataView dv = dt.DefaultView;
        //    grdView.GridControl.DataSource = dv;
        //    grdView.PopulateColumns();
        //    grdView.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grdView });
        //    grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
        //    grdView.BestFitColumns();
        //}

        //private static void SetColumnType(GvColumnPropriete propriete, GridColumn grdColumn)
        //{
        //    switch (propriete.Type)
        //    {
        //        case GvColumnPropriete.GvColumnType.String:
        //            grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        //            //
        //            break;

        //        case GvColumnPropriete.GvColumnType.Date:
        //            grdColumn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
        //            grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //            grdColumn.DisplayFormat.FormatString = "D";
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            break;

        //        case GvColumnPropriete.GvColumnType.Decimal:
        //            RepositoryItemSpinEdit repositoryItemSpinEditDecimal = new RepositoryItemSpinEdit();
        //            repositoryItemSpinEditDecimal.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        //            repositoryItemSpinEditDecimal.IsFloatValue = true;
        //            repositoryItemSpinEditDecimal.DisplayFormat.FormatString = "d";
        //            repositoryItemSpinEditDecimal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //            grdColumn.ColumnEdit = repositoryItemSpinEditDecimal;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            break;

        //        case GvColumnPropriete.GvColumnType.Integer:
        //            RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
        //            repositoryItemSpinEdit.IsFloatValue = false;
        //            repositoryItemSpinEdit.Mask.EditMask = "N00";
        //            grdColumn.ColumnEdit = repositoryItemSpinEdit;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            break;

        //        case GvColumnPropriete.GvColumnType.Percent:
        //            RepositoryItemSpinEdit repositoryItemSpinEditPercent = new RepositoryItemSpinEdit();
        //            repositoryItemSpinEditPercent.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        //            repositoryItemSpinEditPercent.IsFloatValue = true;
        //            repositoryItemSpinEditPercent.DisplayFormat.FormatString = "p";
        //            repositoryItemSpinEditPercent.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //            grdColumn.ColumnEdit = repositoryItemSpinEditPercent;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            break;

        //        case GvColumnPropriete.GvColumnType.Currency:
        //            RepositoryItemCalcEdit repositoryItemCalcEditCurrency = new RepositoryItemCalcEdit();
        //            repositoryItemCalcEditCurrency.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
        //            repositoryItemCalcEditCurrency.Mask.EditMask = "c";
        //            repositoryItemCalcEditCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //            repositoryItemCalcEditCurrency.DisplayFormat.FormatString = "c";
        //            grdColumn.ColumnEdit = repositoryItemCalcEditCurrency;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //            break;

        //        case GvColumnPropriete.GvColumnType.Boolean:
        //            // grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
        //            grdColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //            grdColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //            break;
        //    }
        //}

        //private static void SetColumnEtat(GvColumnPropriete propriete, GridColumn grdColumn)
        //{
        //    switch (propriete.Etat)
        //    {
        //        case GvColumnPropriete.GvColumnEtat.Invisible:
        //            grdColumn.Visible = false;
        //            grdColumn.View.OptionsBehavior.Editable = false;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //            grdColumn.OptionsColumn.ReadOnly = true;
        //            break;

        //        case GvColumnPropriete.GvColumnEtat.Disable:
        //            grdColumn.Visible = true;
        //            grdColumn.View.OptionsBehavior.Editable = false;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //            grdColumn.OptionsColumn.ReadOnly = true;
        //            grdColumn.AppearanceCell.BackColor = Color.Tan;
        //            break;

        //        case GvColumnPropriete.GvColumnEtat.Enable:
        //            grdColumn.Visible = true;
        //            grdColumn.View.OptionsBehavior.Editable = true;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
        //            grdColumn.View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
        //            break;
        //    }
        //}
        //public static void FillGridViewSaisie(GridView grdView, GvColumnProprietes proprietes, object collectionLookUp, string valueMember, string displayMember) //object collection,
        //{
        //    InitGridView(grdView, proprietes);
        //    grdView.GridControl.UseEmbeddedNavigator = false;
        //    int i = 0;
        //    foreach (GvColumnPropriete  propriete in proprietes)
        //    {
        //       grdColumn.Caption = propriete.Titre;
        //        grdView.Columns[i].OptionsColumn.AllowEdit = true;
        //        switch (propriete.Type)
        //        {
        //            case GvColumnPropriete.GvColumnType.lookUp:
        //                RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();
        //                ChargerRepositoryItemLookUpEdit(lookUp, collectionLookUp, valueMember, displayMember);
        //                grdView.Columns[i].ColumnEdit = lookUp;
        //                break;

        //            case GvColumnPropriete.GvColumnType.String:
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        //                //
        //                break;

        //            case GvColumnPropriete.GvColumnType.Date:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Decimal:
        //                RepositoryItemSpinEdit repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
        //                InitrepositoryItemSpinEdit(repositoryItemSpinEdit1);
        //                repositoryItemSpinEdit1.IsFloatValue = true;
        //                grdView.Columns[i].ColumnEdit = repositoryItemSpinEdit1;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Integer:
        //                RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
        //                InitrepositoryItemSpinEdit(repositoryItemSpinEdit);
        //                grdView.Columns[i].ColumnEdit = repositoryItemSpinEdit;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Percent:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        //                grdView.Columns[i].DisplayFormat.FormatString = "p";
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Boolean:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //                break;
        //        }

        //        switch (propriete.Etat)
        //        {
        //            case GvColumnPropriete.GvColumnEtat.Invisible:
        //                grdView.Columns[i].View.OptionsBehavior.Editable = true;
        //                grdView.Columns[i].Visible = false;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Visible:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Disable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Enable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
        //                break;
        //        }
        //        i++;
        //    }
        //    grdView.GridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { grdView });
        //    grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
        //    grdView.OptionsView.ColumnAutoWidth = true;
        //    grdView.BestFitColumns();
        //}

        //public static void FillGridView(GridView grdView, GvColumnProprietes proprietes, object collection, object collectionLookUp, string valueMember, string displayMember)
        //{
        //    FillGridView(grdView, proprietes, collection);
        //    int i = 0;
        //    foreach (GvColumnPropriete  propriete in proprietes)
        //    {
        //        if (propriete.Type == GvColumnPropriete.GvColumnType.lookUp)
        //        {
        //            RepositoryItemLookUpEdit lookUp = new RepositoryItemLookUpEdit();
        //            ChargerRepositoryItemLookUpEdit(lookUp, collectionLookUp, valueMember, displayMember);
        //            grdView.Columns[i].ColumnEdit = lookUp;
        //            i++;
        //        }
        //    }
        //}

        //public static void FillGridView(GridView grdView, GvColumnProprietes proprietes, DataTable collection)
        //{
        //    InitGridView(grdView, proprietes);
        //    grdView.OptionsView.ColumnAutoWidth = true;

        //    grdView.GridControl.DataSource = collection;
        //    grdView.GridControl.UseEmbeddedNavigator = false;

        //    grdView.PopulateColumns();
        //    int i = 0;
        //    foreach (GvColumnPropriete  propriete in proprietes)
        //    {
        //        grdView.Columns[i].Caption = propriete.Titre;
        //        grdView.Columns[i].OptionsColumn.AllowEdit = true;
        //        switch (propriete.Type)
        //        {
        //            case GvColumnPropriete.GvColumnType.String:
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
        //                //
        //                break;

        //            case GvColumnPropriete.GvColumnType.Date:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Decimal:
        //                RepositoryItemSpinEdit repositoryItemSpinEdit1 = new RepositoryItemSpinEdit();
        //                InitrepositoryItemSpinEdit(repositoryItemSpinEdit1);
        //                repositoryItemSpinEdit1.IsFloatValue = true;
        //                grdView.Columns[i].ColumnEdit = repositoryItemSpinEdit1;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Integer:
        //                RepositoryItemSpinEdit repositoryItemSpinEdit = new RepositoryItemSpinEdit();
        //                InitrepositoryItemSpinEdit(repositoryItemSpinEdit);
        //                grdView.Columns[i].ColumnEdit = repositoryItemSpinEdit;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Percent:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Decimal;
        //                grdView.Columns[i].DisplayFormat.FormatString = "p";
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
        //                break;

        //            case GvColumnPropriete.GvColumnType.Boolean:
        //                grdView.Columns[1].UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
        //                grdView.Columns[i].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
        //                grdView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
        //                break;
        //        }

        //        switch (propriete.Etat)
        //        {
        //            case GvColumnPropriete.GvColumnEtat.Invisible:
        //                grdView.Columns[i].Visible = false;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Visible:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Disable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Enable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
        //                break;
        //        }
        //        i++;
        //    }

        //    while (grdView.Columns.Count > i)
        //    {
        //        grdView.Columns[i].Visible = false;
        //        i++;
        //    }
        //    grdView.BestFitColumns();
        //}
        //public static void InitGridView(GridView grdView, GvColumnProprietes proprietes, bool bEditable)
        //{
        //    int i = 0;

        //    //grdView.PaintStyleName = "MixedXP";
        //    grdView.BorderStyle = BorderStyles.Default;
        //    grdView.OptionsBehavior.Editable = true;
        //    grdView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
        //    grdView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;

        //    grdView.OptionsSelection.EnableAppearanceFocusedCell = false;
        //    grdView.OptionsSelection.EnableAppearanceHideSelection = false;
        //    grdView.OptionsSelection.MultiSelect = false;
        //    grdView.OptionsSelection.EnableAppearanceFocusedRow = true;

        //    grdView.OptionsView.ShowIndicator = true;
        //    grdView.OptionsView.ShowGroupPanel = false;
        //    grdView.OptionsView.ShowAutoFilterRow = false;
        //    grdView.OptionsView.ColumnAutoWidth = true;
        //    grdView.OptionsView.ShowColumnHeaders = true;
        //    grdView.OptionsView.ShowFooter = false;
        //    grdView.OptionsView.ShowGroupedColumns = true;
        //    grdView.OptionsView.ShowViewCaption = false;

        //    grdView.OptionsView.EnableAppearanceEvenRow = true;
        //    grdView.OptionsView.EnableAppearanceOddRow = true;
        //    grdView.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;

        //    grdView.OptionsBehavior.Editable = bEditable;

        //    grdView.OptionsSelection.MultiSelect = true;
        //    grdView.OptionsView.ColumnAutoWidth = true;
        //    grdView.OptionsView.ShowColumnHeaders = true;
        //    grdView.OptionsView.ShowGroupedColumns = true;

        //    if (!bEditable)
        //    {
        //        grdView.OptionsView.ShowGroupPanel = false;
        //        grdView.OptionsView.ShowAutoFilterRow = false;
        //        grdView.OptionsView.ShowFooter = false;
        //        grdView.OptionsView.ShowViewCaption = false;
        //        grdView.OptionsBehavior.Editable = false;
        //    }

        //    grdView.Columns.Clear();
        //    DataTable dt = new DataTable();
        //    foreach (GvColumnPropriete  propriete in proprietes)
        //    {
        //        dt.Columns.Add(propriete.Titre, typeof(string));
        //    }
        //    DataView dv = dt.DefaultView;
        //    grdView.GridControl.DataSource = dv;
        //    grdView.PopulateColumns();

        //    foreach (GvColumnPropriete  propriete in proprietes)
        //    {
        //        grdView.Columns[i].Tag = "Article";
        //        switch (propriete.Etat)
        //        {
        //            case GvColumnPropriete.GvColumnEtat.Invisible:
        //                grdView.Columns[i].Visible = false;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Visible:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Disable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = false;
        //                grdView.Columns[i].OptionsColumn.ReadOnly = true;
        //                break;

        //            case GvColumnPropriete.GvColumnEtat.Enable:
        //                grdView.Columns[i].Visible = true;
        //                grdView.Columns[i].View.OptionsBehavior.Editable = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = true;
        //                grdView.Columns[i].View.GridControl.EmbeddedNavigator.Buttons.Append.Visible = true;
        //                break;
        //        }
        //        i++;
        //    }
        //}

        //public static void ChargerGrid(DataTable dt, GridView gridV)
        //{
        //    int i = 0;
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        col.Caption = gridV.Columns[i].FieldName; // = col.ColumnName;
        //        gridV.Columns[i].FieldName = col.ColumnName;
        //        i = i + 1;
        //    }
        //    gridV.GridControl.DataSource = dt;

        //    foreach (DataColumn dc in dt.Columns)
        //    {
        //        switch (dc.DataType.FullName)
        //        {
        //            case "System.Double":
        //            case "System.Decimal":
        //            case "System.Int64":
        //            case "System.Int16":
        //            case "System.Int32":
        //                gridV.Columns[dc.ColumnName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
        //                break;

        //            case "System.DateTime":
        //                gridV.Columns[dc.ColumnName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        //                break;

        //            case "System.Boolean":
        //                gridV.Columns[dc.ColumnName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
        //                break;

        //            default:
        //                gridV.Columns[dc.ColumnName].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
        //                break;
        //        }
        //    }

        //    gridV.GridControl.Refresh();
        //    gridV.FocusedRowHandle = 0;
        //}

        //public static void InitialiserGrid(GridView gridV, string[] titles)
        //{
        //    //Design
        //    gridV.OptionsSelection.EnableAppearanceFocusedCell = false;
        //    gridV.OptionsSelection.EnableAppearanceHideSelection = false;
        //    gridV.OptionsSelection.MultiSelect = false;
        //    gridV.OptionsView.ShowGroupPanel = false;
        //    gridV.OptionsView.ShowIndicator = true;
        //    gridV.OptionsSelection.EnableAppearanceFocusedRow = true;
        //    gridV.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
        //    gridV.OptionsBehavior.Editable = false;
        //    gridV.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        //    gridV.OptionsBehavior.Editable = false;
        //    gridV.OptionsView.EnableAppearanceEvenRow = true;
        //    gridV.OptionsView.EnableAppearanceOddRow = true;
        //    gridV.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.Default;
        //    gridV.OptionsView.ShowAutoFilterRow = false;

        //    //Data
        //    gridV.Columns.Clear();
        //    gridV.GridControl.DataSource = null;
        //    DataTable dt = new DataTable();
        //    for (int i = 0; i < titles.Length; i++)
        //    {
        //        DataColumn col = new DataColumn();
        //        col.ColumnName = titles[i];
        //        col.Caption = titles[i];
        //        dt.Columns.Add(col);
        //    }
        //    gridV.GridControl.DataSource = dt;
        //    gridV.GridControl.Refresh();
        //}

        //public static void InitialiserGrid_WithFiltre(GridView gridV, string[] titles)
        //{
        //    InitialiserGrid(gridV, titles);
        //    gridV.OptionsView.ShowAutoFilterRow = true;
        //}
    }
}