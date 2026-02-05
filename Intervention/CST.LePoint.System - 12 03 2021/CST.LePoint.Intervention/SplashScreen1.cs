using DevExpress.XtraSplashScreen;
using System;
using System.Windows.Forms;

namespace CST.LePoint.Intervention
{
    public partial class SplashScreen1 : SplashScreen
    {
        private Timer t = new Timer();
        private bool fadeIn = true;
        private bool fadeOut = false;

        public SplashScreen1()
        {
            InitializeComponent();
            ExtraFormSettings();
            // If we use solution2 we need to comment the following line.
            SetAndStartTimer();
        }

        #region Overrides

        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }

        #endregion Overrides

        public enum SplashScreenCommand
        {
        }

        private void SetAndStartTimer()
        {
            t.Interval = 30;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        private void ExtraFormSettings()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Opacity = 0.5;
            //  this.BackgroundImage = CSWinFormSplashScreen.Properties.Resources.SplashImage;
        }

        private void t_Tick(object sender, EventArgs e)
        {
            // Fade in by increasing the opacity of the splash to 1.0
            if (fadeIn)
            {
                if (this.Opacity < 1.0)
                {
                    this.Opacity += 0.02;
                }
                // After fadeIn complete, begin fadeOut
                else
                {
                    fadeIn = false;
                    fadeOut = true;
                }
            }
            else if (fadeOut) // Fade out by increasing the opacity of the splash to 1.0
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.02;
                }
                else
                {
                    fadeOut = false;
                }
            }

            // After fadeIn and fadeOut complete, stop the timer and close this splash.
            if (!(fadeIn || fadeOut))
            {
                t.Stop();
                this.Close();
            }
        }
    }
}