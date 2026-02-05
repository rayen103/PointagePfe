using CST.LePoint.Securite.Entites;
using System;

namespace CST.LePoint.Intervention
{
    public partial class FrmAccueil : DevExpress.XtraEditors.XtraForm, IActionsCommun
    {
        public FrmAccueil()
        {
            InitializeComponent();
        }

        private void FrmAccueil_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void Actualiser()
        {
            LoadData();
        }

        private void LoadData()
        {
        }
    }
}