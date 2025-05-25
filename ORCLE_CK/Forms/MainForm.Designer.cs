using ORCLE_CK.Constants;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class MainForm
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
            this.menuStrip = new MenuStrip();
            this.statusStrip = new StatusStrip();
            this.statusLabel = new ToolStripStatusLabel();
            this.timeLabel = new ToolStripStatusLabel();
            this.mainPanel = new Panel();

            this.SuspendLayout();

            // Form
            this.Text = $"{AppConstants.APP_NAME} - {currentUser.FullName}";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.Icon = SystemIcons.Application;

            // Menu Strip
            this.menuStrip.Location = new Point(0, 0);
            this.menuStrip.Size = new Size(1200, 24);
            this.menuStrip.BackColor = Color.WhiteSmoke;

            // Status Strip
            this.statusStrip.Location = new Point(0, 776);
            this.statusStrip.Size = new Size(1200, 22);
            this.statusStrip.BackColor = Color.LightGray;

            this.statusLabel.Text = $"Đăng nhập: {currentUser.FullName} ({currentUser.RoleDisplayName})";
            this.statusLabel.Spring = true;
            this.statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            this.timeLabel.Text = DateTime.Now.ToString(AppConstants.DATETIME_FORMAT);
            this.timeLabel.TextAlign = ContentAlignment.MiddleRight;

            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.Items.Add(this.timeLabel);

            // Main Panel
            this.mainPanel.Dock = DockStyle.Fill;
            this.mainPanel.Location = new Point(0, 24);
            this.mainPanel.Size = new Size(1200, 752);
            this.mainPanel.BackColor = Color.White;

            // Add controls
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.statusStrip);
            this.MainMenuStrip = this.menuStrip;

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}