using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSTAdmin
{
    public partial class FrmLanding : XtraForm
    {
        private string folder = AppDomain.CurrentDomain.BaseDirectory + "Lib\\";
        private string masterkey = "CST@Dmin1989";

        public FrmLanding()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            this.DoStart();
        }

        private void txt_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (Settings.IsEmpty(this.txtServerName.Text)
                || Settings.IsEmpty(this.txtPassword.Text)
                || Settings.IsEmpty(this.txtDataBase.Text)
                || Settings.IsEmpty(this.txtUser.Text)
                )

                this.Start.Enabled = false;
            else
                this.Start.Enabled = true;
        }

        private void DoStart()
        {
            this.Start.Enabled = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString(this.txtServerName.Text, this.txtUser.Text, this.txtPassword.Text, this.txtDataBase.Text)))
                {
                    cn.Open();
                    SqlTransaction transaction = cn.BeginTransaction();
                    try
                    {
                        DoMasterKey(transaction);
                        DoCertificate(transaction);
                        DoApplicationUser(transaction);
                        DoSaPassword(transaction);                        

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                    DoActivateEncryption(cn);

                }
                
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            finally
            {
                if (Settings.IsEmpty(this.txtServerName.Text)
               || Settings.IsEmpty(this.txtPassword.Text)
               || Settings.IsEmpty(this.txtDataBase.Text)
               || Settings.IsEmpty(this.txtUser.Text))

                    this.Start.Enabled = false;
                else
                    this.Start.Enabled = true;
            }
        }

        private void DoMasterKey(SqlTransaction transaction)
        {
            try
            {
                this.txtLog.Text = "Step 1 -> Starting "; 
                string query = "USE master;"+
                                " CREATE MASTER KEY ENCRYPTION BY PASSWORD = '"+ masterkey+"';";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                this.txtLog.Text = this.txtLog.Text + "Done ✔";
            }
            catch (Exception ex)
            {
                this.txtLog.Text = this.txtLog.Text + "\r\nStep 1 -> Exception\r\n" + ex.Message;
                //throw;
            }
        }

        private void DoCertificate(SqlTransaction transaction)
        {
            try
            {
                this.txtLog.Text = this.txtLog.Text + "\r\nStep 2 -> Starting ";
                //=!PVfwv-rvva8855
                
                string query = "USE master;" +
                                " CREATE CERTIFICATE CSTCertificate#AJ#  " +
                                " FROM FILE = '" + folder + "CSTCertificateB' " +
                                " WITH PRIVATE KEY (FILE = '" + folder + "CSTCertificateK',    " +
                                " DECRYPTION BY PASSWORD = '"+masterkey+"');" +

                                " USE " + this.txtDataBase.Text + "; " +
                                " CREATE DATABASE ENCRYPTION KEY " +
                                " WITH ALGORITHM = AES_256 " +
                                " ENCRYPTION BY SERVER CERTIFICATE CSTCertificate#AJ#; ";               

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();            

                this.txtLog.Text = this.txtLog.Text + "Done ✔";

            }
            catch (Exception ex)
            {
                this.txtLog.Text = this.txtLog.Text + "\r\nStep 2 -> Exception\r\n" + ex.Message;
                
                throw;
            }
        }

        private void DoActivateEncryption(SqlConnection cn)
        {
            try
            {
                string query2 = " ALTER DATABASE " + this.txtDataBase.Text + " " +
                                " SET ENCRYPTION ON; ";

                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = cn;
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = query2;
                cmd2.ExecuteNonQuery();
            }
            catch (Exception)
            {                
                throw;
            }
        }

        private void DoApplicationUser(SqlTransaction transaction)
        {
            try
            {
                if (Settings.IsEmpty(Properties.Settings.Default.au))
                    throw new Exception("Password Empty");

                this.txtLog.Text = this.txtLog.Text + "\r\nStep 3 -> Starting ";

                string query = "USE [master] "+
                               " CREATE LOGIN [au] WITH PASSWORD = '"+ Properties.Settings.Default.au +"',  " +
				               "                   DEFAULT_DATABASE=MASTER,  "+
				               "                   DEFAULT_LANGUAGE=[us_english],  "+
				               "                   CHECK_EXPIRATION=OFF,  "+
				               "                   CHECK_POLICY=ON "+
                               " ALTER LOGIN [au] ENABLE  "+
                               " ALTER LOGIN [au] WITH PASSWORD = '"+ Properties.Settings.Default.au +"' " +

                               " USE "+ this.txtDataBase.Text +" "+
                               " CREATE USER [au] FOR LOGIN [au] WITH DEFAULT_SCHEMA=[dbo] "+
                               
                               " IF DATABASE_PRINCIPAL_ID('db_procexecutor') IS NULL "+
                               " BEGIN "+
	                           "     CREATE ROLE db_procexecutor "+
	                           "     GRANT EXECUTE TO db_procexecutor "+
                               " END "+
                               " EXEC sp_addrolemember 'db_procexecutor', 'au'   "+
                               " EXEC sp_addrolemember 'db_datawriter', 'au' "+
                               " EXEC sp_addrolemember 'db_datareader', 'au' ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();

                this.txtLog.Text = this.txtLog.Text + "Done ✔";

            }
            catch (Exception ex)
            {
                this.txtLog.Text = this.txtLog.Text + "\r\nStep 3 -> Exception\r\n" + ex.Message;

                throw;
            }
        }

        private void DoSaPassword(SqlTransaction transaction)
        {
            try
            {
                if (Settings.IsEmpty(Properties.Settings.Default.sa))
                    throw new Exception("Password Empty");

                this.txtLog.Text = this.txtLog.Text + "\r\nStep 4 -> Starting ";

                string query = "  ALTER LOGIN [sa] WITH DEFAULT_DATABASE=[master]  "+
                               
                               "  USE [master]  "+
                               "  ALTER LOGIN [sa] WITH PASSWORD=N'" + Properties.Settings.Default.sa + "' ";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = transaction.Connection;
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                this.txtPassword.Text = Properties.Settings.Default.sa;
                this.txtLog.Text = this.txtLog.Text + "Done ✔";

            }
            catch (Exception ex)
            {
                this.txtLog.Text = this.txtLog.Text + "\r\nStep 4 -> Exception\r\n" + ex.Message;

                throw;
            }
        }

        private void btnAuthorize_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.EnableRaisingEvents = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.WorkingDirectory = @"C:\";
                p.OutputDataReceived += OnOutputDataRecived;
                p.ErrorDataReceived += OnErrorDataReceived;
                p.Exited += OnExited;
                p.Start();
                p.StandardInput.WriteLine(@"cd C:\Windows\Microsoft.NET\Framework\v4.0.30319");
                p.StandardInput.WriteLine("aspnet_regiis -pa \"CSTKeys\" \"NT AUTHORITY\\NETWORK SERVICE");
                p.StandardInput.WriteLine("aspnet_regiis -pa \"CSTKeys\" \""+ this.txtDomainUser.Text +"\" ");
                p.StandardInput.WriteLine("exit");
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                this.txtLog.Text = this.txtLog.Text + "\r\n" + output;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                Process p = new Process();
                p.EnableRaisingEvents = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.WorkingDirectory = @"C:\";
                p.OutputDataReceived += OnOutputDataRecived;
                p.ErrorDataReceived += OnErrorDataReceived;
                p.Exited += OnExited;
                p.Start();
                p.StandardInput.WriteLine(@"cd C:\Windows\Microsoft.NET\Framework\v4.0.30319");
                p.StandardInput.WriteLine("aspnet_regiis -pi \"CSTKeys\" \" " + folder + "keys.xml\"");
                p.StandardInput.WriteLine("exit");
                string output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                this.txtLog.Text = this.txtLog.Text + "\r\n" + output;
            }
            catch (Exception)
            {                
                throw;
            }

        }

        private void OnExited(object sender, EventArgs e)
        {
            try
            {
                Process p = sender as Process;
                if (p == null)
                    return;
                this.txtLog.Text = this.txtLog.Text + "\r\n" + "Exited";
            }
            catch (Exception)
            {
            }
        }

        private void OnErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            this.txtLog.Text = this.txtLog.Text + "\r\n" + e.Data;
        }

        private void OnOutputDataRecived(object sender, DataReceivedEventArgs e)
        {
            Process p = sender as Process;
            if (p == null)
                return;
            this.txtLog.Text = this.txtLog.Text + "\r\n" + e.Data;
        }

        private void txtDomainUser_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            this.btnAuthorize.Enabled = true;
            if (Settings.IsEmpty(this.txtDomainUser.Text))
                this.btnAuthorize.Enabled = false;
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(Settings.GetConnectionString(this.txtServerName.Text, this.txtUser.Text, this.txtPassword.Text, this.txtDataBase.Text)))
            {
                cn.Open();
                //SqlTransaction transaction = cn.BeginTransaction();
                try
                {
                    DoActivateEncryption(cn);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

    }
}
