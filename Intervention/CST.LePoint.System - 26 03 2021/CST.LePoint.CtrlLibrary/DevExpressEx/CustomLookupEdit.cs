using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Drawing;
using DevExpress.XtraEditors.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.ViewInfo;
using System;
using System.ComponentModel;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public class CustomLookUpEdit : LookUpEdit
    {
        static CustomLookUpEdit()
        {
            RepositoryItemCustomLookUpEdit.Register();
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new RepositoryItemCustomLookUpEdit Properties
        {
            get
            {
                return base.Properties as RepositoryItemCustomLookUpEdit;
            }
        }

        public override string EditorTypeName
        {
            get
            {
                return RepositoryItemCustomLookUpEdit.EditorName;
            }
        }

        protected override bool ProcessNewValueCore(bool partital, string text)
        {
            if (this.Properties.IsNullInputAllowed && String.IsNullOrEmpty(text))
            {
                this.EditValue = null;
                return true;
            }

            return base.ProcessNewValueCore(partital, text);
        }
    }

    public class RepositoryItemCustomLookUpEdit : RepositoryItemLookUpEdit
    {
        internal const string EditorName = "CustomLookUpEdit";

        static RepositoryItemCustomLookUpEdit()
        {
            Register();
        }

        public RepositoryItemCustomLookUpEdit()
        {
        }

        public static void Register()
        {
            EditorRegistrationInfo.Default.Editors.Add(new EditorClassInfo(EditorName,
              typeof(CustomLookUpEdit), typeof(RepositoryItemCustomLookUpEdit),
                typeof(ButtonEditViewInfo), new ButtonEditPainter(), true));
        }

        public override string EditorTypeName
        {
            get
            {
                return EditorName;
            }
        }

        protected internal new bool IsNullInputAllowed
        {
            get
            {
                return base.IsNullInputAllowed;
            }
        }
    }
}