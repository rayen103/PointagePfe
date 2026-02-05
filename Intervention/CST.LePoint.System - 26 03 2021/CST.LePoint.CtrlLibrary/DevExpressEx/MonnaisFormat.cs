using CST.LePoint.Tools;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Mask;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Globalization;

namespace Mira.CtrlLibrary
{
    public static class MonnaisFormat
    {
        public static void SetMaskToCurrency(this TextEdit baseEdit, bool formatZeroAsEmptyString = true)
        {
            SetMaskToCurrency(baseEdit.Properties, formatZeroAsEmptyString);
        }

        public static void SetMaskToCurrency(this RepositoryItemTextEdit edit, bool formatZeroAsEmptyString = true)
        {
            edit.Mask.MaskType = MaskType.Numeric;
            edit.Mask.EditMask = @"c";

            edit.Mask.Culture = new CultureInfo("fr-FR");
            edit.Mask.ShowPlaceHolders = false;
            edit.Mask.UseMaskAsDisplayFormat = true;

            if (formatZeroAsEmptyString)
                edit.CustomDisplayText += (s, e) =>
                {
                    if (e.Value is decimal && ((decimal)e.Value) == 0)
                        e.DisplayText = "";
                };
        }

        public static void SetMaskToCurrency(this GridColumn column, bool formatZeroAsEmptyString = true)
        {
            foreach (GridView view in column.View.GridControl.Views)
            {
                view.CustomColumnDisplayText += (s, e) =>
                {
                    if (e.Column == column)
                    {
                        decimal d = SysHelper.ToDecimal(e.Value);
                        if ((d) != 0 || !formatZeroAsEmptyString)
                        {
                            e.DisplayText = String.Format(CultureInfo.GetCultureInfo("fr-FR"), "{0:C}", e.Value);
                        }
                        else
                        {
                            e.DisplayText = "";
                        }
                    }
                };
            }
        }
    }
}