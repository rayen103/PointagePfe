using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public class GridControlEx : GridControl
    {
        public GridControlEx()
        {
            DataSourceChanged += GridControlEx_DataSourceChanged;
            ViewRegistered += GridControlEx_ViewRegistered;
        }

        private void GridControlEx_ViewRegistered(object sender, ViewOperationEventArgs e)
        {
            ((GridView)e.View).OptionsView.ShowGroupPanel = false;
        }

        private void GridControlEx_DataSourceChanged(object sender, System.EventArgs e)
        {
            var gridV = (GridView)this.MainView;
            if (gridV != null)
                foreach (GridColumn dc in gridV.Columns)
                {
                    switch (dc.ColumnType.FullName)
                    {
                        case "System.Double":
                        case "System.Decimal":
                        case "System.Int64":
                        case "System.Int16":
                        case "System.Int32":
                            dc.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                            break;

                        case "System.DateTime":
                            dc.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                            break;

                        case "System.Boolean":
                            dc.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                            break;

                        default:
                            dc.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                            break;
                    }
                }
        }
    }
}