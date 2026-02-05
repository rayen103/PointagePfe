using CST.LePoint.Securite.GestionActions;
using DevExpress.XtraNavBar;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace CST.LePoint.CtrlLibrary.DevExpressEx
{
    /// <summary>
    ///     Les type présente dans le fichier CFEvenements.xml doivent résider dans le EntryAssembly
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RemplisseurNavBar<T> where T : Form
    {
        private readonly Action<T, Form, string> AffichierFormulaire;
        private readonly NavBarControl barManager;
        private readonly CFApplication2 cfApplication;
        private readonly T myForm;
        private readonly ResourceManager resourceManagerImages;
        private readonly ResourceManager resourceManagerMenus;
        private Func<string, bool> peutFaireAction = s => true;

        public RemplisseurNavBar(CFApplication2 cfApplication2, T form, NavBarControl barManager,
                              ResourceManager resManagerMenus, ResourceManager resManagerImages,
                              Action<T, Form, string> ouvreurFormulaire)
        {
            cfApplication = cfApplication2;
            resourceManagerMenus = resManagerMenus;
            resourceManagerImages = resManagerImages;
            AffichierFormulaire = ouvreurFormulaire;
            myForm = form;
            this.barManager = barManager;
        }

        public Func<string, bool> PeutFaireAction
        {
            get { return peutFaireAction; }
            set { peutFaireAction = value; }
        }

        public void RemplirNavBar(NavBarControl navbar)
        {
            string caption = string.Empty;
            navbar.Groups.Clear();
            foreach (CFMenu cfMenu in cfApplication.CFMenus.CFMenus)
            {
                var cfMainMenu = cfMenu as CFMenuLink;
                if (cfMainMenu != null)
                {
                    caption = resourceManagerMenus.GetString(cfMainMenu.RessourceCaption);
                    if (string.IsNullOrEmpty(caption))
                        caption = cfMainMenu.RessourceCaption;

                    if (cfMainMenu.SousMenus.Count > 0)
                    {
                        NavBarGroup group = new NavBarGroup(caption);
                       
                        if (!string.IsNullOrEmpty(cfMainMenu.RessourceIdIcone))
                            group.LargeImage = (Bitmap)resourceManagerImages.GetObject(cfMainMenu.RessourceIdIcone);

                        navbar.Groups.Add(group);
                      
                        RemplirMenu((NavBarGroup)group, cfMainMenu);
                    }
                    else
                    {
                        NavBarItem bItem = GetBarButtonItem(cfMainMenu.IdEvenement, caption);
                        caption = resourceManagerMenus.GetString(cfMainMenu.RessourceCaption);
                        if (string.IsNullOrEmpty(caption))
                            caption = cfMainMenu.RessourceCaption;

                        bItem.Caption = caption;
                        if (bItem.Enabled)
                            navbar.Items.Add(bItem);
                    }
                }
            }
        }

        private bool RemplirMenu(NavBarGroup bsiParent, CFMenuLink menuParent)
        {
            bool beginGroup = false;
            bool tousSousMenusDesactives = true;
            bsiParent.ItemLinks.Clear();
            foreach (CFMenu sousMenu in menuParent.SousMenus)
            {
                if (sousMenu is CFMenuLink)
                {
                    var menuFils = (CFMenuLink)sousMenu;

                    string caption = resourceManagerMenus.GetString(menuFils.RessourceCaption);
                    if (string.IsNullOrEmpty(caption))
                        caption = menuFils.RessourceCaption;

                    if (menuFils.SousMenus.Count > 0)
                    {
                        NavBarGroup navGroup = new NavBarGroup(caption);
                        bool tsmd = RemplirMenu((NavBarGroup)navGroup, menuFils);
                        //navGroup = !tsmd; //désactiver la BarSubItem si tt les sous menus sont désactivés!
                    }
                    else
                    {
                        NavBarItem bi = GetBarButtonItem(menuFils.IdEvenement, caption);
                        if (!string.IsNullOrEmpty(menuFils.RessourceIdIcone))
                        {
                            // bi.LargeImage = (Bitmap)resourceManagerImages.GetObject(menuFils.RessourceIdIcone);
                            bi.SmallImage = (Bitmap)resourceManagerImages.GetObject(menuFils.RessourceIdIcone);
                        }

                        //bsiParent.LinksPersistInfo.Add(new LinkPersistInfo(bi, beginGroup));
                        //bsiParent.(new LinkPersistInfo(bi, beginGroup));


                        // Pour ne plus afficher les menus non autorisés(désactiver) dans la NavBar B.G.N :)
                        if (bi.Enabled)
                            bsiParent.ItemLinks.Add(bi);
                        beginGroup = false;

                        if (bi != null && bi.Enabled)
                            tousSousMenusDesactives = false;
                    }
                }
                else
                {
                    beginGroup = true;
                }
            }
            // Pour ne pas afficher les menus vides dans NavBar B.G.N :)
            if (bsiParent.ItemLinks.Count == 0)
                bsiParent.Visible = false;
            return tousSousMenusDesactives;
        }

        private NavBarItem GetBarButtonItem(string idEvenement, string caption)
        {
            var bi = new NavBarItem(caption);
            var bi2 = new NavBarItemLink(bi);
            try
            {
                if (!string.IsNullOrEmpty(idEvenement))
                {
                    CFEvenementAbstrait cfEvenement =
                        cfApplication.CFEvenements.CFEvenements.FirstOrDefault(cfe => cfe.IdEvenement == idEvenement);
                    if (cfEvenement != null)
                    {
                        //ItemClickEventHandler
                        NavBarLinkEventHandler actionEvenementForm = null;

                        if (cfEvenement is CFEvenementMethode)
                        {
                            var cfEvenementMethode = (CFEvenementMethode)cfEvenement;
                            if (peutFaireAction(cfEvenementMethode.NomCompletMethode))
                                actionEvenementForm = GenererActionMethode(cfEvenementMethode.NomCompletMethode, bi);
                            else
                                bi.Enabled = false;
                        }
                        else if (cfEvenement is CFEvenementForm)
                        {
                            var cfEvenementForm = (CFEvenementForm)cfEvenement;

                            if (peutFaireAction(cfEvenementForm.NomCompletForm))
                                actionEvenementForm = GenererActionForm(cfEvenementForm.NomCompletForm,
                                                                        resourceManagerMenus.GetString(
                                                                            cfEvenementForm.ResTitreForm), bi);
                            else
                                bi.Enabled = false;
                        }

                        bi.LinkClicked += actionEvenementForm;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return bi;
        }

        private NavBarLinkEventHandler GenererActionMethode(string nomMethodeComplet, object parametre)
        {
            string nomClasse = nomMethodeComplet.Substring(0,
                                                           nomMethodeComplet.LastIndexOf(".", StringComparison.Ordinal));
            string nomMethode = nomMethodeComplet.Substring(
                nomMethodeComplet.LastIndexOf(".", StringComparison.Ordinal) + 1);
            Type type = Assembly.GetEntryAssembly().GetType(nomClasse, true, false);
            MethodInfo methodInfo = (type.GetMethod(nomMethode, new[] { typeof(T), typeof(object) }) ??
                                     type.GetMethod(nomMethode, new[] { typeof(T) })) ??
                                    type.GetMethod(nomMethode, Type.EmptyTypes);
            if (methodInfo == null)
                throw new InvalidOperationException(
                    string.Format("la méthode {0} n'existe pas!" +
                                  "elle doit être sans paramètre, " +
                                  "avec un paramètre {1} ou " +
                                  "avec un paramètre {1} et un paramètre object ", nomMethodeComplet, typeof(T).Name));

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

        private NavBarLinkEventHandler GenererActionForm(string nomFormComplet, string titre, object parametre)
        {
            NavBarLinkEventHandler act = (s, e) =>
            {
                Type type = Assembly.GetEntryAssembly().GetType(nomFormComplet, true, false);
                if (!(type.IsSubclassOf(typeof(Form))))
                {
                    throw new InvalidOperationException(string.Format("la type {0} n'est pas un formulaire!",
                                                                      nomFormComplet));
                }

                //Form instance;
                //try
                //{
                //    instance = (Form)Activator.CreateInstance(type, myForm, parametre);
                //}
                //catch (Exception)
                //{
                //    try
                //    {
                //        instance = (Form)Activator.CreateInstance(type, myForm);
                //    }
                //    catch (Exception)
                //    {
                //        instance = (Form)Activator.CreateInstance(type);
                //    }
                //}

                Form instance = (Form)Activator.CreateInstance(type);
                AffichierFormulaire(myForm, instance, titre);
            };
            return act;
        }
    }
}