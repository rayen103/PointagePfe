using DevExpress.XtraEditors;
using System.Text.RegularExpressions;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    public static class ValidationUtils
    {
        private static string enterPrompt = "Entrer votre {0}";

        public static string EnterPrompt
        {
            get { return enterPrompt; }
            set { enterPrompt = value; }
        }

        private static string invalidPrompt = "{0} invalide";

        public static string InvalidPrompt
        {
            get { return invalidPrompt; }
            set { invalidPrompt = value; }
        }

        public static void SetValidation(this BaseEdit be, string regex, string champs, bool obligatoire = true, bool trim = true)
        {
            be.Validating += (sender, args) =>
            {
                args.Cancel = !ValidateBaseEdit(be, regex, champs, obligatoire, trim);
            };
        }

        private static bool ValidateBaseEdit(BaseEdit baseEdit, string regex, string champs, bool obligatoire, bool trim)
        {
            if (trim)
            {
                baseEdit.Text = baseEdit.Text.Trim();
            }
            string text = baseEdit.Text;
            if (obligatoire)
                if (text == string.Empty)
                {
                    baseEdit.ErrorText = string.Format(enterPrompt, champs);
                    return false;
                }
                else
                {
                    baseEdit.ErrorText = null;
                    //return true;
                }
            if (!Regex.IsMatch(text, "^" + regex + "$"))
            {
                baseEdit.ErrorText = string.Format(invalidPrompt, champs);
                return false;
            }
            baseEdit.ErrorText = null;
            return true;
        }

        public static void SetValidation(DateEdit dateEditDebut, DateEdit dateEditFin, string p)
        {
            dateEditFin.Validating += (s, e) =>
            {
                if ((dateEditDebut.EditValue == null && dateEditFin.EditValue != null) ||
                    (dateEditDebut.EditValue != null && dateEditFin.EditValue == null))
                {
                    e.Cancel = true;
                    if (dateEditDebut.EditValue == null)
                    {
                        dateEditDebut.ErrorText = "La date de début doit être renseignée";
                    }
                    else
                        dateEditFin.ErrorText = "La date de fin doit être renseignée";
                }
                else
                {
                    bool valid = (dateEditDebut.EditValue == null
                                  || dateEditFin.EditValue == null) ||
                                 (dateEditDebut.DateTime <= dateEditFin.DateTime);
                    dateEditFin.ErrorText = !valid
                                                ? p
                                                : null;
                    e.Cancel = !valid;
                }
            };
        }
    }
}