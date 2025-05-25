using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ORCLE_CK.Forms;
using ORCLE_CK.Utils;
namespace ORCLE_CK
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                // Initialize logging
                Logger.Initialize();
                Logger.LogInfo("Application starting...");

                // Show splash screen
                using (var splashForm = new SplashForm())
                {
                    splashForm.ShowDialog();
                }

                // Show login form
                var loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Logger.LogInfo($"User {loginForm.CurrentUser.Username} logged in successfully");
                    Application.Run(new MainForm(loginForm.CurrentUser));
                }

                Logger.LogInfo("Application closing...");
            }
            catch (Exception ex)
            {
                Logger.LogError($"Application startup error: {ex.Message}", ex);
                MessageBox.Show($"Lỗi khởi động ứng dụng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
