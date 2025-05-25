using System.Drawing;
using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class AccountInfoForm
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
            this.lblUserId = new Label();
            this.lblFullName = new Label();
            this.lblUsername = new Label();
            this.lblEmail = new Label();
            this.lblRole = new Label();
            this.lblCreatedAt = new Label();
            this.lblLastLoginAt = new Label();
            this.btnClose = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thông tin tài khoản";
            this.Size = new Size(450, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Title
            var lblTitle = new Label
            {
                Text = "THÔNG TIN TÀI KHOẢN",
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.DarkBlue,
                Location = new Point(20, 20),
                Size = new Size(400, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // User ID
            var lblUserIdLabel = new Label { Text = "Mã người dùng:", Location = new Point(30, 70), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblUserId.Location = new Point(160, 70);
            this.lblUserId.Size = new Size(250, 23);
            this.lblUserId.Font = new Font("Microsoft Sans Serif", 9F);

            // Full Name
            var lblFullNameLabel = new Label { Text = "Họ tên:", Location = new Point(30, 100), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblFullName.Location = new Point(160, 100);
            this.lblFullName.Size = new Size(250, 23);
            this.lblFullName.Font = new Font("Microsoft Sans Serif", 9F);

            // Username
            var lblUsernameLabel = new Label { Text = "Tên đăng nhập:", Location = new Point(30, 130), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblUsername.Location = new Point(160, 130);
            this.lblUsername.Size = new Size(250, 23);
            this.lblUsername.Font = new Font("Microsoft Sans Serif", 9F);

            // Email
            var lblEmailLabel = new Label { Text = "Email:", Location = new Point(30, 160), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblEmail.Location = new Point(160, 160);
            this.lblEmail.Size = new Size(250, 23);
            this.lblEmail.Font = new Font("Microsoft Sans Serif", 9F);

            // Role
            var lblRoleLabel = new Label { Text = "Vai trò:", Location = new Point(30, 190), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblRole.Location = new Point(160, 190);
            this.lblRole.Size = new Size(250, 23);
            this.lblRole.Font = new Font("Microsoft Sans Serif", 9F);

            // Created At
            var lblCreatedAtLabel = new Label { Text = "Ngày tạo:", Location = new Point(30, 220), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblCreatedAt.Location = new Point(160, 220);
            this.lblCreatedAt.Size = new Size(250, 23);
            this.lblCreatedAt.Font = new Font("Microsoft Sans Serif", 9F);

            // Last Login At
            var lblLastLoginAtLabel = new Label { Text = "Đăng nhập cuối:", Location = new Point(30, 250), Size = new Size(120, 23), Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold) };
            this.lblLastLoginAt.Location = new Point(160, 250);
            this.lblLastLoginAt.Size = new Size(250, 23);
            this.lblLastLoginAt.Font = new Font("Microsoft Sans Serif", 9F);

            // Close Button
            this.btnClose.Text = "Đóng";
            this.btnClose.Location = new Point(350, 280);
            this.btnClose.Size = new Size(75, 30);
            this.btnClose.Font = new Font("Microsoft Sans Serif", 9F);
            this.btnClose.BackColor = Color.LightGray;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUserIdLabel);
            this.Controls.Add(this.lblUserId);
            this.Controls.Add(lblFullNameLabel);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(lblUsernameLabel);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(lblEmailLabel);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(lblRoleLabel);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(lblCreatedAtLabel);
            this.Controls.Add(this.lblCreatedAt);
            this.Controls.Add(lblLastLoginAtLabel);
            this.Controls.Add(this.lblLastLoginAt);
            this.Controls.Add(this.btnClose);

            this.ResumeLayout(false);
        }

        #endregion
    }
}