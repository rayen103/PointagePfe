using DevExpress.XtraEditors;
using System;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public class DateEditEx : DateEdit
    {
        static DateEditEx()
        {
        }

        public DateEditEx()
        {
            FormatEditValue += DateEditEx_FormatEditValue;
        }

        private void DateEditEx_FormatEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null && e.Value is DateTime && (DateTime)e.Value == DateTime.MinValue)
                e.Value = null;
        }
    }
}