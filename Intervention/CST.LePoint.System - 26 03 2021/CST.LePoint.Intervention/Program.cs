using CST.LePoint.CtrlLibrary.Acces;
using CST.LePoint.Securite;
using CST.LePoint.Securite.DataAccess;
using CST.LePoint.Securite.Entites;
using CST.LePoint.Securite.GestionActions;
using CST.LePoint.Securite.Management;
using CST.LePoint.Intervention.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace CST.LePoint.Intervention
{
    internal static class Program
    { 
        private static string AssemblyTitle
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute attribute = (AssemblyTitleAttribute)customAttributes[0];
                    if (attribute.Title != "")
                    {
                        return attribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // The following line provides localization for data formats.
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("fr-FR");
            // The following line provides localization for the application's user interface.
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            DevExpress.UserSkins.BonusSkins.Register();
            Application.Run(new SplashScreen1());

            //GestionSession.SocieteCourante = Societe.Charger("1");
            //FrmLogin frmLogin = new FrmLogin();
            //frmLogin.ShowDialog();

            //if (GestionSession.UtilisateurCourant == null)
            //{
            //    frmLogin.Dispose();
            //    return;
            //}

            //frmLogin.Dispose();
            
            //cfg[1].ConnectionString
            GestionSession.SecuriteActive = bool.Parse(ConfigurationManager.AppSettings["SECURITE_ACTIVEE"].ToString());

            if (ConfigurationManager.AppSettings["MODE_APPLICATION"].ToString() == "ADMINISTRATION")
            {
                GestionSession.SecuriteActive = false;
                IDictionary<string, Actions> ActionsApplication;
                Dictionary<string, string> Formulaires;
                ChargerActionsApplication(out Formulaires, out ActionsApplication);

                //Charger le contexte
                IContexteSecurite cs = GestionContexteSecurite.ContexteActive;
                //cs.Charger();

                //if (cs.Set<Role>() != null)
                //    cs.Set<Role>().Clear();

                //if (cs.Set<Autorisation>() != null)
                //    cs.Set<Autorisation>().Clear();

                //if (cs.Set<Utilisateur>() != null)
                //    cs.Set<Utilisateur>().Clear();

                GestionSession.SocieteCourante = Societe.Charger();
                GestionSession.SocieteSite = "001";
                Role r = new Role();
                r.Nom = "ADMINISTRATEUR";
                r.Description = "ADMINISTRATEUR SYSTÈME";
                r.CSociete = GestionSession.SocieteCourante.CSociete;
                if (!GestionContexteSecurite.ContexteActive.Set<Role>().Any(x => x.Nom == r.Nom))
                {
                    cs.Set<Role>().Remove(r);
                    cs.Set<Role>().Add(r);
                    GestionContexteSecurite.ContexteActive.Enregistrer();

                    List<Actions> ActionsValeurs = new List<Actions>((IEnumerable<Actions>)Enum.GetValues(typeof(Actions))).Where(o => o != Actions.Rien).ToList();

                    IDictionary<string, Dictionary<Actions, bool>> autorisations = new Dictionary<string, Dictionary<Actions, bool>>();
                    foreach (string item in Formulaires.Keys)
                    {
                        Dictionary<Actions, bool> list = ActionsValeurs.ToDictionary(op => op,
                                                                            op =>
                                                                            true);

                        autorisations.Add(item, list);
                    }

                    foreach (var item in autorisations)
                    {
                        var aut = new Autorisation { NomForm = item.Key };
                        foreach (var op in item.Value)
                            if (op.Value && op.Key != Actions.Rien)
                                aut.AddOperation(op.Key);

                        cs.Set<Autorisation>().Add(aut);
                        r.Autorisations.Add(aut);
                    }
                    GestionContexteSecurite.ContexteActive.Enregistrer();

                }
                Utilisateur u = new Utilisateur();
                u.BAdministrateur = true;
                u.CSociete = GestionSession.SocieteCourante.CSociete;
                u.CRole = r.Nom;
                u.Login = "ADMINISTRATEUR";
                u.MotDePasseCry = "7B52009B64FD0A2A49E6D8A939753077792B0554";
                u.Roles.Add(r);
                u.Sauvegarder();
                cs.Set<Utilisateur>().Add(u);
                cs.Enregistrer();

                GestionSession.UtilisateurCourant = u;
                Application.Run(new FrmMDI());
            }
            else
            {
              //  Application.Run(new SplashScreen1());
                string roleInitial =string.Empty;
                bool bModifier = false;
                if (GestionSession.SecuriteActive)
                {
                    GestionSession.SocieteCourante = Societe.Charger();
                    FrmLogin frmLogin = new FrmLogin();
                    if (frmLogin.ShowDialog() != DialogResult.OK)
                        return;
                    else
                    {
                        GestionSession.UtilisateurCourant = frmLogin.Utilisateur;
                        GestionSession.SocieteCourante = Societe.Charger(frmLogin.Utilisateur.CSociete);
                        GestionSession.SocieteSite = frmLogin.Site;
                        roleInitial = GestionSession.UtilisateurCourant.CRole;
                        if (frmLogin._BModifierFocused)
                        {
                            frmLogin.Utilisateur.CRole = "RIEN";
                            bModifier = true;
                        }
                    }
                    frmLogin.Dispose();
                }
                else
                {
                    GestionSession.SocieteCourante = Societe.Charger();
                    GestionSession.UtilisateurCourant = Utilisateur.Charger(null);
                }

                Application.Run(new FrmMDI(bModifier, roleInitial));
            }
        }

        private static void ChargerActionsApplication(out Dictionary<string, string> formulaires,
                                              out IDictionary<string, Actions> actionsApplication)
        {
            formulaires = null;
            List<Actions> ActionsValeurs = new List<Actions>((IEnumerable<Actions>)Enum.GetValues(typeof(Actions))).Where(o => o != Actions.Rien).ToList();

            formulaires = CFEvenementForm.ChargerFormulaires(FrmMDI.CfMenuApplication,
                                                             ResourcesMenus.ResourceManager);
            actionsApplication = new Dictionary<string, Actions>();

            foreach (string frm in formulaires.Keys)
            {
                var action = Actions.Rien;
                foreach (Actions op in ActionsValeurs)
                {
                    if (op != Actions.Consulter)
                    {
                        string frmType = frm;
                        Type type = Type.GetType(frmType);
                        if (type != null)
                        {
                            if (type.FindMembers(MemberTypes.Method,
                                                 BindingFlags.Public | BindingFlags.Instance,
                                                 (mi, obj) => mi.Name == obj.ToString(),
                                                 op).Length != 0)
                                action |= op;
                        }
                    }
                    else
                        action |= op;
                }
                actionsApplication.Add(frm, action);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, Resources.NomApplication);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
                MessageBox.Show(exception.Message, Resources.NomApplication);
        }
    }
}