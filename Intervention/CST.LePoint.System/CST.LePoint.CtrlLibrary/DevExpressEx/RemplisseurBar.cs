using CST.LePoint.Securite.GestionActions;
using DevExpress.XtraBars;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    /// <summary>
    /// Les type présente dans le fichier CFEvenements.xml doivent résider dans le EntryAssembly
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RemplisseurBar<T> where T : Form
    {
        private Func<string, bool> peutFaireAction = (s) => true;

        public Func<string, bool> PeutFaireAction
        {
            get { return peutFaireAction; }
            set { peutFaireAction = value; }
        }

        private readonly CFApplication2 cfApplication;
        private readonly ResourceManager resourceManagerMenus;
        private readonly ResourceManager resourceManagerImages;
        private readonly T myForm;
        private readonly BarManager barManager;

        private readonly Action<T, Form, string> AffichierFormulaire;

        public RemplisseurBar(CFApplication2 cfApplication2, T form, BarManager barManager, ResourceManager resManagerMenus, ResourceManager resManagerImages, Action<T, Form, string> ouvreurFormulaire)
        {
            this.cfApplication = cfApplication2;
            this.resourceManagerMenus = resManagerMenus;
            this.resourceManagerImages = resManagerImages;
            this.AffichierFormulaire = ouvreurFormulaire;
            this.myForm = form;
            this.barManager = barManager;
        }

        public void RemplirBar(Bar bar)
        {
            string caption = string.Empty;

            foreach (var cfMenu in cfApplication.CFMenus.CFMenus)
            {
                var cfMainMenu = cfMenu as CFMenuLink;
                if (cfMainMenu != null)
                {
                    caption = resourceManagerMenus.GetString(cfMainMenu.RessourceCaption);
                    if (string.IsNullOrEmpty(caption))
                        caption = cfMainMenu.RessourceCaption;

                    BarItem bi;
                    if (cfMainMenu.SousMenus.Count > 0)
                    {
                        bi = new BarSubItem(barManager, caption);
                        if (!string.IsNullOrEmpty(cfMainMenu.RessourceIdIcone))
                            bi.Glyph = (Bitmap)resourceManagerImages.GetObject(cfMainMenu.RessourceIdIcone);

                        RemplirMenu((BarSubItem)bi, cfMainMenu);
                    }
                    else
                    {
                        bi = GetBarButtonItem(cfMainMenu.IdEvenement, caption);
                    }

                    caption = resourceManagerMenus.GetString(cfMainMenu.RessourceCaption);
                    if (string.IsNullOrEmpty(caption))
                        caption = cfMainMenu.RessourceCaption;

                    bi.Caption = caption;

                    bar.AddItem(bi);
                }
            }
        }

        private bool RemplirMenu(BarSubItem bsiParent, CFMenuLink menuParent)
        {
            bool beginGroup = false;
            bool tousSousMenusDesactives = true;

            foreach (var sousMenu in menuParent.SousMenus)
            {
                if (sousMenu is CFMenuLink)
                {
                    var menuFils = (CFMenuLink)sousMenu;

                    string caption = resourceManagerMenus.GetString(menuFils.RessourceCaption);
                    if (string.IsNullOrEmpty(caption))
                        caption = menuFils.RessourceCaption;

                    BarItem bi;

                    if (menuFils.SousMenus.Count > 0)
                    {
                        bi = new BarSubItem(barManager, caption);
                        bool tsmd = RemplirMenu((BarSubItem)bi, menuFils);
                        bi.Enabled = !tsmd;//désactiver la BarSubItem si tt les sous menus sont désactivés!
                    }
                    else
                    {
                        bi = GetBarButtonItem(menuFils.IdEvenement, caption);
                    }

                    if (!string.IsNullOrEmpty(menuFils.RessourceIdIcone))
                        bi.Glyph = (Bitmap)resourceManagerImages.GetObject(menuFils.RessourceIdIcone);
                    // Pour ne plus afficher tous les écrans à tout les utilistateurs B.G.N :)
                    if (bi.Enabled)
                        bsiParent.LinksPersistInfo.Add(new LinkPersistInfo(bi, beginGroup));
                    beginGroup = false;

                    if (bi != null && bi.Enabled)
                        tousSousMenusDesactives = false;
                }
                else
                {
                    beginGroup = true;
                }
            }
            // Pour ne pas afficher tous les menus vides B.G.N :)
            if (bsiParent.LinksPersistInfo.Count == 0)
                bsiParent.Visibility = BarItemVisibility.Never;

            return tousSousMenusDesactives;
        }

        private BarItem GetBarButtonItem(string idEvenement, string caption)
        {
            var bi = new BarButtonItem(barManager, caption);
            try
            {
                if (!string.IsNullOrEmpty(idEvenement))
                {
                    var cfEvenement = cfApplication.CFEvenements.CFEvenements.FirstOrDefault(cfe => cfe.IdEvenement == idEvenement);
                    if (cfEvenement != null)
                    {
                        ItemClickEventHandler actionEvenementForm = null;

                        if (cfEvenement is CFEvenementMethode)
                        {
                            CFEvenementMethode cfEvenementMethode = (CFEvenementMethode)cfEvenement;
                            actionEvenementForm = GenererActionMethode(cfEvenementMethode.NomCompletMethode, bi);
                        }
                        else if (cfEvenement is CFEvenementForm)
                        {
                            CFEvenementForm cfEvenementForm = (CFEvenementForm)cfEvenement;

                            if (peutFaireAction(cfEvenementForm.NomCompletForm))
                                actionEvenementForm = GenererActionForm(cfEvenementForm.NomCompletForm, resourceManagerMenus.GetString(cfEvenementForm.ResTitreForm), bi);
                            else
                                bi.Enabled = false;
                        }

                        bi.ItemClick += actionEvenementForm;
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return bi;
        }

        private ItemClickEventHandler GenererActionMethode(string nomMethodeComplet, object parametre)
        {
            string nomClasse = nomMethodeComplet.Substring(0,
                nomMethodeComplet.LastIndexOf(".", StringComparison.Ordinal));
            string nomMethode = nomMethodeComplet.Substring(
                nomMethodeComplet.LastIndexOf(".", StringComparison.Ordinal) + 1);
            Type type = Assembly.GetEntryAssembly().GetType(nomClasse, true, false);
            var methodInfo = (type.GetMethod(nomMethode, new[] { typeof(T), typeof(object) }) ??
                              type.GetMethod(nomMethode, new[] { typeof(T) })) ??
                             type.GetMethod(nomMethode, Type.EmptyTypes);
            if (methodInfo == null)
                throw new InvalidOperationException(
                    string.Format("la méthode {0} n'existe pas!" +
                    "elle doit être sans paramètre, " +
                    "avec un paramètre {1} ou " +
                    "avec un paramètre {1} et un paramètre object ", nomMethodeComplet, typeof(T).Name)
                    );

            if (!methodInfo.IsStatic)
                throw new InvalidOperationException("la méthode " + nomMethodeComplet + " n'est pas statique!");
            if (methodInfo.GetParameters().Length == 2)
                return (s, e) => methodInfo.Invoke(null, new[] { myForm, parametre });
            if (methodInfo.GetParameters().Length == 1)
                return (s, e) => methodInfo.Invoke(null, new[] { (object)myForm });
            if (methodInfo.GetParameters().Length == 0)
                return (s, e) => methodInfo.Invoke(null, new object[] { });
            //La méthode a déjà retourné, si cette erreur durvient verifier "methodInfo"
            throw new InvalidOperationException("what the hell!!!??");
        }

        private ItemClickEventHandler GenererActionForm(string nomFormComplet, string titre, object parametre)
        {
            ItemClickEventHandler act = (s, e) =>
            {
                Type type = Assembly.GetEntryAssembly().GetType(nomFormComplet, true, false);
                if (!(type.IsSubclassOf(typeof(Form))))
                {
                    throw new InvalidOperationException(string.Format("le type {0} n'est pas un formulaire!", nomFormComplet));
                }

                Form instance;
                try
                {
                    instance = (Form)Activator.CreateInstance(type, myForm, parametre);
                }
                catch (Exception)
                {
                    try
                    {
                        instance = (Form)Activator.CreateInstance(type, myForm);
                    }
                    catch (Exception)
                    {
                        instance = (Form)Activator.CreateInstance(type);
                    }
                }

                AffichierFormulaire(myForm, instance, titre);
            };
            return act;
        }
    }
}