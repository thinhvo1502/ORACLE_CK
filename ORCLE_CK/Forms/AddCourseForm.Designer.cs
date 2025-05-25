using System.Windows.Forms;
using System;

namespace ORCLE_CK.Forms
{
    partial class AddCourseForm
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
            this.txtTitle = new TextBox();
            this.txtDescription = new TextBox();
            this.cmbInstructor = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // Form
            this.Text = "Thêm khóa học mới";
            this.Size = new System.Drawing.Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Title
            var lblTitle = new Label { Text = "Tiêu đề:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(100, 23) };
            this.txtTitle.Location = new System.Drawing.Point(130, 20);
            this.txtTitle.Size = new System.Drawing.Size(330, 23);

            // Description
            var lblDescription = new Label { Text = "Mô tả:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(100, 23) };
            this.txtDescription.Location = new System.Drawing.Point(130, 60);
            this.txtDescription.Size = new System.Drawing.Size(330, 150);
            this.txtDescription.Multiline = true;
            this.txtDescription.ScrollBars = ScrollBars.Vertical;

            // Instructor
            var lblInstructor = new Label { Text = "Giảng viên:", Location = new System.Drawing.Point(20, 230), Size = new System.Drawing.Size(100, 23) };
            this.cmbInstructor.Location = new System.Drawing.Point(130, 230);
            this.cmbInstructor.Size = new System.Drawing.Size(330, 23);
            this.cmbInstructor.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbInstructor.DisplayMember = "FullName";
            this.cmbInstructor.ValueMember = "UserId";

            // Buttons
            this.btnSave.Text = "Lưu";
            this.btnSave.Location = new System.Drawing.Point(300, 280);
            this.btnSave.Size = new System.Drawing.Size(75, 30);
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            this.btnCancel.Text = "Hủy";
            this.btnCancel.Location = new System.Drawing.Point(385, 280);
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // Add controls
            this.Controls.Add(lblTitle);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(lblInstructor);
            this.Controls.Add(this.cmbInstructor);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
        }

        #endregion
    }
}