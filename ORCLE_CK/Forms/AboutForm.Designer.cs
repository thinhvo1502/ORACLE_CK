using ORCLE_CK.Constants;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class AboutForm
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
            this.btnClose = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Về chương trình";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // App Name
            var lblAppName = new Label
            {
                Text = AppConstants.APP_NAME,
                Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(50, 30),
                Size = new Size(400, 40),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Version
            var lblVersion = new Label
            {
                Text = $"Phiên bản {AppConstants.APP_VERSION}",
                Font = new Font("Microsoft Sans Serif", 12F),
                ForeColor = Color.Gray,
                Location = new Point(50, 80),
                Size = new Size(400, 25),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Description
            var lblDescription = new Label
            {
                Text = "Hệ thống quản lý học tập trực tuyến\n\n" +
                       "Tính năng chính:\n" +
                       "• Quản lý người dùng (Admin, Giảng viên, Học viên)\n" +
                       "• Quản lý khóa học và bài học\n" +
                       "• Hệ thống bài tập và quiz\n" +
                       "• Báo cáo và thống kê\n" +
                       "• Bảo mật và phân quyền\n\n" +
                       "Công nghệ sử dụng:\n" +
                       "• C# WinForms (.NET Framework 4.7.2)\n" +
                       "• Oracle Database\n" +
                       "• Repository Pattern\n" +
                       "• Logging System",
                Font = new Font("Microsoft Sans Serif", 9F),
                Location = new Point(50, 120),
                Size = new Size(400, 200),
                TextAlign = ContentAlignment.TopLeft
            };

            // Company
            var lblCompany = new Label
            {
                Text = $"© 2024 {AppConstants.COMPANY_NAME}\nTất cả quyền được bảo lưu.",
                Font = new Font("Microsoft Sans Serif", 8F),
                ForeColor = Color.Gray,
                Location = new Point(50, 320),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Close Button
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new Point(400, 330);
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.Font = new Font("Microsoft Sans Serif", 9F);
            this.btnClose.BackColor = Color.LightGray;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Add controls
            this.Controls.Add(lblAppName);
            this.Controls.Add(lblVersion);
            this.Controls.Add(lblDescription);
            this.Controls.Add(lblCompany);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}