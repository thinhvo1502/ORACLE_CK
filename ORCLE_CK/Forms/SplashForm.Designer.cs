using ORCLE_CK.Constants;
using System.Drawing;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    partial class SplashForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar = new ProgressBar();
            this.lblStatus = new Label();
            this.lblVersion = new Label();
            this.SuspendLayout();

            // Form
            this.Text = "";
            this.Size = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White;
            this.ShowInTaskbar = false;

            // Title
            var lblTitle = new Label
            {
                Text = AppConstants.APP_NAME,
                Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(50, 50),
                Size = new Size(400, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Version
            this.lblVersion.Text = $"Phiên bản {AppConstants.APP_VERSION}";
            this.lblVersion.Font = new Font("Microsoft Sans Serif", 10F);
            this.lblVersion.ForeColor = Color.Gray;
            this.lblVersion.Location = new Point(50, 100);
            this.lblVersion.Size = new Size(400, 20);
            this.lblVersion.TextAlign = ContentAlignment.MiddleCenter;

            // Status
            this.lblStatus.Text = "Đang khởi động...";
            this.lblStatus.Font = new Font("Microsoft Sans Serif", 9F);
            this.lblStatus.Location = new Point(50, 180);
            this.lblStatus.Size = new Size(400, 20);
            this.lblStatus.TextAlign = ContentAlignment.MiddleCenter;

            // Progress Bar
            this.progressBar.Location = new Point(50, 210);
            this.progressBar.Size = new Size(400, 20);
            this.progressBar.Style = ProgressBarStyle.Continuous;
            this.progressBar.Minimum = 0;
            this.progressBar.Maximum = 100;

            // Company
            var lblCompany = new Label
            {
                Text = $"© 2024 {AppConstants.COMPANY_NAME}",
                Font = new Font("Microsoft Sans Serif", 8F),
                ForeColor = Color.Gray,
                Location = new Point(50, 250),
                Size = new Size(400, 15),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(lblCompany);

            this.ResumeLayout(false);
        }

        #endregion
    }
}