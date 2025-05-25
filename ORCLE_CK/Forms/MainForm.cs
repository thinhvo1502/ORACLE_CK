using ORCLE_CK.Utils;
using ORCLE_CK.Constants;
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
    public partial class MainForm : Form
    {
        private readonly User currentUser;
        private readonly UserService userService;
        private readonly CourseService courseService;

        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripStatusLabel timeLabel;
        private Panel mainPanel;
        private Timer statusTimer;

        public MainForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            userService = new UserService();
            courseService = new CourseService();
            InitializeComponent();
            SetupUserInterface();
            InitializeStatusTimer();
        }


        private void SetupUserInterface()
        {
            menuStrip.Items.Clear();

            // System Menu
            var systemMenu = new ToolStripMenuItem("Hệ thống");
            systemMenu.DropDownItems.Add("Thông tin tài khoản", null, AccountInfoMenuItem_Click);
            systemMenu.DropDownItems.Add("Đổi mật khẩu", null, ChangePasswordMenuItem_Click);
            systemMenu.DropDownItems.Add(new ToolStripSeparator());
            systemMenu.DropDownItems.Add("Đăng xuất", null, LogoutMenuItem_Click);
            systemMenu.DropDownItems.Add("Thoát", null, ExitMenuItem_Click);

            // Admin menus
            if (currentUser.Role.ToLower() == "admin")
            {
                var userMenu = new ToolStripMenuItem("Quản lý người dùng");
                userMenu.DropDownItems.Add("Danh sách người dùng", null, UserListMenuItem_Click);
                userMenu.DropDownItems.Add("Thêm người dùng", null, AddUserMenuItem_Click);
                userMenu.DropDownItems.Add("Báo cáo người dùng", null, UserReportMenuItem_Click);
                menuStrip.Items.Add(userMenu);

                var courseMenu = new ToolStripMenuItem("Quản lý khóa học");
                courseMenu.DropDownItems.Add("Danh sách khóa học", null, CourseListMenuItem_Click);
                courseMenu.DropDownItems.Add("Thêm khóa học", null, AddCourseMenuItem_Click);
                courseMenu.DropDownItems.Add("Báo cáo khóa học", null, CourseReportMenuItem_Click);
                menuStrip.Items.Add(courseMenu);

                var reportMenu = new ToolStripMenuItem("Báo cáo");
                reportMenu.DropDownItems.Add("Thống kê tổng quan", null, OverviewReportMenuItem_Click);
                reportMenu.DropDownItems.Add("Báo cáo học viên", null, StudentReportMenuItem_Click);
                reportMenu.DropDownItems.Add("Báo cáo giảng viên", null, InstructorReportMenuItem_Click);
                menuStrip.Items.Add(reportMenu);
            }

            // Instructor menus
            if (currentUser.Role.ToLower() == "instructor" || currentUser.Role.ToLower() == "admin")
            {
                var teachingMenu = new ToolStripMenuItem("Giảng dạy");
                teachingMenu.DropDownItems.Add("Khóa học của tôi", null, MyCoursesMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý bài học", null, LessonManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý bài tập", null, AssignmentManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Quản lý quiz", null, QuizManagementMenuItem_Click);
                teachingMenu.DropDownItems.Add("Danh sách học viên", null, StudentListMenuItem_Click);
                menuStrip.Items.Add(teachingMenu);
            }

            // Student menus
            if (currentUser.Role.ToLower() == "student")
            {
                var learningMenu = new ToolStripMenuItem("Học tập");
                learningMenu.DropDownItems.Add("Khóa học của tôi", null, MyEnrolledCoursesMenuItem_Click);
                learningMenu.DropDownItems.Add("Tìm khóa học", null, FindCourseMenuItem_Click);
                learningMenu.DropDownItems.Add("Bài tập của tôi", null, MyAssignmentsMenuItem_Click);
                learningMenu.DropDownItems.Add("Kết quả quiz", null, MyQuizResultsMenuItem_Click);
                learningMenu.DropDownItems.Add("Chứng chỉ", null, MyCertificatesMenuItem_Click);
                menuStrip.Items.Add(learningMenu);
            }

            // Help Menu
            var helpMenu = new ToolStripMenuItem("Trợ giúp");
            helpMenu.DropDownItems.Add("Hướng dẫn sử dụng", null, UserGuideMenuItem_Click);
            helpMenu.DropDownItems.Add("Về chương trình", null, AboutMenuItem_Click);

            menuStrip.Items.Add(systemMenu);
            menuStrip.Items.Add(helpMenu);

            ShowWelcomeScreen();
        }

        private void InitializeStatusTimer()
        {
            statusTimer = new Timer
            {
                Interval = 1000 // Update every second
            };
            statusTimer.Tick += (s, e) => timeLabel.Text = DateTime.Now.ToString(AppConstants.DATETIME_FORMAT);
            statusTimer.Start();
        }

        private void ShowWelcomeScreen()
        {
            mainPanel.Controls.Clear();

            var welcomePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            var welcomeLabel = new Label
            {
                Text = $"Chào mừng {currentUser.FullName}!\n\n" +
                       $"Vai trò: {currentUser.RoleDisplayName}\n\n" +
                       $"Hôm nay là {DateTime.Now:dddd, dd/MM/yyyy}\n\n" +
                       "Vui lòng chọn chức năng từ menu trên.",
                Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill,
                ForeColor = Color.DarkBlue
            };

            welcomePanel.Controls.Add(welcomeLabel);
            mainPanel.Controls.Add(welcomePanel);
        }

        // Event Handlers
        private void AccountInfoMenuItem_Click(object sender, EventArgs e)
        {
            using var accountForm = new AccountInfoForm(currentUser);
            accountForm.ShowDialog();
        }

        private void ChangePasswordMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement change password form
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UserListMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserManagement();
        }

        private void AddUserMenuItem_Click(object sender, EventArgs e)
        {
            using var addUserForm = new AddUserForm();
            if (addUserForm.ShowDialog() == DialogResult.OK)
            {
                ShowUserManagement();
                statusLabel.Text = MessageConstants.USER_CREATED_SUCCESS;
            }
        }

        private void UserReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CourseListMenuItem_Click(object sender, EventArgs e)
        {
            ShowCourseManagement();
        }

        private void AddCourseMenuItem_Click(object sender, EventArgs e)
        {
            using var addCourseForm = new AddCourseForm();
            if (addCourseForm.ShowDialog() == DialogResult.OK)
            {
                ShowCourseManagement();
                statusLabel.Text = MessageConstants.COURSE_CREATED_SUCCESS;
            }
        }

        private void CourseReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OverviewReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void InstructorReportMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyCoursesMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LessonManagementMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AssignmentManagementMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void QuizManagementMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void StudentListMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyEnrolledCoursesMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FindCourseMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyAssignmentsMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyQuizResultsMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MyCertificatesMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UserGuideMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MessageConstants.FEATURE_UNDER_DEVELOPMENT, "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AboutMenuItem_Click(object sender, EventArgs e)
        {
            using var aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void ShowUserManagement()
        {
            mainPanel.Controls.Clear();

            var userListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Microsoft Sans Serif", 9F)
            };

            userListView.Columns.Add("ID", 60);
            userListView.Columns.Add("Họ tên", 200);
            userListView.Columns.Add("Tên đăng nhập", 150);
            userListView.Columns.Add("Email", 200);
            userListView.Columns.Add("Vai trò", 120);
            userListView.Columns.Add("Ngày tạo", 120);
            userListView.Columns.Add("Đăng nhập cuối", 120);
            userListView.Columns.Add("Trạng thái", 80);

            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                var users = userService.GetAllUsers();

                foreach (var user in users)
                {
                    var item = new ListViewItem(user.UserId.ToString());
                    item.SubItems.Add(user.FullName);
                    item.SubItems.Add(user.Username);
                    item.SubItems.Add(user.Email);
                    item.SubItems.Add(user.RoleDisplayName);
                    item.SubItems.Add(user.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(user.LastLoginAt?.ToString(AppConstants.DATETIME_FORMAT) ?? "Chưa đăng nhập");
                    item.SubItems.Add(user.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = user;

                    if (!user.IsActive)
                        item.ForeColor = Color.Gray;

                    userListView.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {users.Count} người dùng";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading users: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }

            mainPanel.Controls.Add(userListView);
        }

        private void ShowCourseManagement()
        {
            mainPanel.Controls.Clear();

            var courseListView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                Font = new Font("Microsoft Sans Serif", 9F)
            };

            courseListView.Columns.Add("ID", 60);
            courseListView.Columns.Add("Tiêu đề", 300);
            courseListView.Columns.Add("Mô tả", 400);
            courseListView.Columns.Add("Giảng viên", 200);
            courseListView.Columns.Add("Ngày tạo", 120);
            courseListView.Columns.Add("Học viên", 80);
            courseListView.Columns.Add("Trạng thái", 80);

            try
            {
                statusLabel.Text = MessageConstants.LOADING_DATA;
                var courses = courseService.GetAllCourses();

                foreach (var course in courses)
                {
                    var item = new ListViewItem(course.CourseId.ToString());
                    item.SubItems.Add(course.Title);
                    item.SubItems.Add(course.Description ?? "");
                    item.SubItems.Add(course.InstructorName ?? "");
                    item.SubItems.Add(course.CreatedAt.ToString(AppConstants.DATE_FORMAT));
                    item.SubItems.Add(course.EnrollmentCount.ToString());
                    item.SubItems.Add(course.IsActive ? "Hoạt động" : "Vô hiệu");
                    item.Tag = course;

                    if (!course.IsActive)
                        item.ForeColor = Color.Gray;

                    courseListView.Items.Add(item);
                }

                statusLabel.Text = $"Đã tải {courses.Count} khóa học";
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading courses: {ex.Message}", ex);
                MessageBox.Show($"Lỗi tải danh sách khóa học: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                statusLabel.Text = "Lỗi tải dữ liệu";
            }

            mainPanel.Controls.Add(courseListView);
        }

        private void LogoutMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MessageConstants.LOGOUT_CONFIRMATION, "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logger.LogInfo($"User {currentUser.Username} logged out");
                this.Hide();

                using var loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Restart with new user
                    Application.Restart();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MessageConstants.EXIT_CONFIRMATION, "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logger.LogInfo("Application exiting");
                Application.Exit();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                ExitMenuItem_Click(this, EventArgs.Empty);
            }
            else
            {
                statusTimer?.Stop();
                statusTimer?.Dispose();
                base.OnFormClosing(e);
            }
        }
    }
}
