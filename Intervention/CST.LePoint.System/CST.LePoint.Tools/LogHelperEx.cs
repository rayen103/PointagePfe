using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CST.LePoint.Tools
{
    public class LogHelperEx
    {
        public static void AddUnhandledExceptionsHandler()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject != null && e.ExceptionObject is Exception)
                handleException(e.ExceptionObject as Exception);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception != null)
                handleException(e.Exception);
        }

        private static string CURR_DIR = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath + "\\";

        private static string LOG_DIR = CURR_DIR + "log\\";

        private static void handleException(Exception ex)
        {
            try
            {
                var thread =
                new Thread(() => MessageBox.Show(ex.Message, "Erreur Technique", MessageBoxButtons.OK, MessageBoxIcon.Stop));
                thread.Start();

                if (!Directory.Exists(LOG_DIR))
                {
                    Directory.CreateDirectory(LOG_DIR);
                }
                var str = LOG_DIR + SysHelper.FileNameValide(DateTime.Today.ToString("yyMMdd") + ".log");

                string message = DateTime.Now + Environment.NewLine + ex;
                StreamWriter writer = new StreamWriter(str, true, Encoding.UTF8);

                using (writer)
                {
                    writer.WriteLine(message);
                    writer.WriteLine(string.Empty.PadLeft(40, '-'));
                }
                //MailSender.EnvoiMail("ahmed.krm.tn@gmail.com",
                //    "Ahmed KRM", "[Mira] Bug Report", message);
                thread.Join();
                Environment.Exit(2);
            }
            catch { }
        }
    }
}