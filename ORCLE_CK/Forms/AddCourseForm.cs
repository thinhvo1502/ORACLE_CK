using ORCLE_CK.Models;
using ORCLE_CK.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ORCLE_CK.Forms
{
    public partial class AddCourseForm : Form
    {
        private CourseService courseService;
        private UserService userService;

        private TextBox txtTitle;
        private TextBox txtDescription;
        private ComboBox cmbInstructor;
        private Button btnSave;
        private Button btnCancel;

        public AddCourseForm()
        {
            courseService = new CourseService();
            userService = new UserService();
            InitializeComponent();
            LoadInstructors();
        }

        

        private void LoadInstructors()
        {
            try
            {
                var instructors = userService.GetInstructors();
                cmbInstructor.DataSource = instructors;
                if (instructors.Any())
                    cmbInstructor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách giảng viên: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTitle.Text))
                {
                    MessageBox.Show("Vui lòng nhập tiêu đề khóa học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbInstructor.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn giảng viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var course = new Course
                {
                    Title = txtTitle.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    InstructorId = (int)cmbInstructor.SelectedValue
                };

                if (courseService.CreateCourse(course))
                {
                    MessageBox.Show("Thêm khóa học thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể thêm khóa học!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
