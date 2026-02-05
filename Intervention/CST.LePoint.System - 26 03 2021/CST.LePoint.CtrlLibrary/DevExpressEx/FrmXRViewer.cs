using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public partial class FrmXRViewer : DevExpress.XtraEditors.XtraForm
    {
        public FrmXRViewer()
        {
            InitializeComponent();           
        }

        public void DoReport(object report)
        {
            this.documentViewer1.DocumentSource = report; 
            this.Show();
        }
    }
}
