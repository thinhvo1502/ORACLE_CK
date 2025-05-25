using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class AddUserForm
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
            this.txtFullName = new TextBox();
            this.txtUsername = new TextBox();
            this.txtEmail = new TextBox();
            this.txtPassword = new TextBox();
            this.cmbRole = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm người dùng mới";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Full Name
            var lblFullName = new Label { Text = "Họ tên:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 23) };
            this.txtFullName.Location = new System.Drawing.Point(130, 20);
            this.txtFullName.Size = new System.Drawing.Size(230, 23);

            // Username
            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 23) };
            this.txtUsername.Location = new System.Drawing.Point(130, 60);
            this.txtUsername.Size = new System.Drawing.Size(230, 23);

            // Email
            var lblEmail = new Label { Text = "Email:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(100, 23) };
            this.txtEmail.Location = new System.Drawing.Point(130, 100);
            this.txtEmail.Size = new System.Drawing.Size(230, 23);

            // Password
            var lblPassword = new Label { Text = "Mật khẩu:", Location = new System.Drawing.Point(20, 140), Size = new System.Drawing.Size(100, 23) };
            this.txtPassword.Location = new System.Drawing.Point(130, 140);
            this.txtPassword.Size = new System.Drawing.Size(230, 23);
            this.txtPassword.UseSystemPasswordChar = true;

            // Role
            var lblRole = new Label { Text = "Vai trò:", Location = new System.Drawing.Point(20, 180), Size = new System.Drawing.Size(100, 23) };
            this.cmbRole.Location = new System.Drawing.Point(130, 180);
            this.cmbRole.Size = new System.Drawing.Size(230, 23);
            this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbRole.Items.Add(new ComboBoxItem("Học viên","student"));
            this.cmbRole.Items.Add(new ComboBoxItem("Giảng viên", "instructor"));
            this.cmbRole.Items.Add(new ComboBoxItem("Quản trị viên", "admin"));
            this.cmbRole.SelectedIndex = 0;
            this.cmbRole.DisplayMember = "Text";
            this.cmbRole.ValueMember = "Value";

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new System.Drawing.Point(200, 230);
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new System.Drawing.Point(285, 230);
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Add controls
            this.Controls.Add(lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(lblUsername);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(lblPassword);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }


        #endregion
    }
}