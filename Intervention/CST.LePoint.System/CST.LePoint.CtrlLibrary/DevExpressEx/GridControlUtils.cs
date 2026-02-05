using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System.Globalization;

namespace Mira.CtrlLibrary
{
    public static class GridControlUtils
    {
        public static void AddRowNumbersToIndicatior(this GridView gridV)
        {
            gridV.OptionsView.ShowIndicator = true;
            gridV.IndicatorWidth = 50;
            gridV.CustomDrawRowIndicator += (s, e) =>
            {
                if (e.Info.IsRowIndicator && e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString(CultureInfo.InvariantCulture);
                    e.Info.ImageIndex = -1;
                }
            };
        }

        public static void ColumnsBestFit(params GridColumn[] cols)
        {
            for (int i = cols.Length - 1; i >= 0; i--)
            {
                var gridColumn = cols[i];
                gridColumn.BestFit();
            }
        }
    }
}