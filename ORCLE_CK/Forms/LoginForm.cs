using ORCLE_CK.Constants;
using ORCLE_CK.Models;
using ORCLE_CK.Services;
using ORCLE_CK.Utils;
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
    public partial class LoginForm : Form
    {
        private readonly UserService userService;
        private int loginAttempts = 0;

        public User? CurrentUser { get; private set; }

        public LoginForm()
        {
            userService = new UserService();
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (loginAttempts >= AppSettings.MaxLoginAttempts)
                {
                    MessageBox.Show($"Bạn đã nhập sai quá {AppSettings.MaxLoginAttempts} lần. Vui lòng thử lại sau!",
                        "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Disable login button and show loading
                btnLogin.Enabled = false;
                btnLogin.Text = "Đang đăng nhập...";

                CurrentUser = userService.AuthenticateUser(txtUsername.Text.Trim(), txtPassword.Text);

                if (CurrentUser != null)
                {
                    Logger.LogInfo($"User {CurrentUser.Username} logged in successfully");

                    // Save remember me preference if needed
                    if (chkRememberMe.Checked)
                    {
                        // TODO: Implement remember me functionality
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    loginAttempts++;
                    var remainingAttempts = AppSettings.MaxLoginAttempts - loginAttempts;

                    if (remainingAttempts > 0)
                    {
                        MessageBox.Show($"{MessageConstants.LOGIN_FAILED}\nCòn lại {remainingAttempts} lần thử.",
                            "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("Bạn đã nhập sai quá nhiều lần. Ứng dụng sẽ thoát.",
                            "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }

                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Login error: {ex.Message}", ex);
                MessageBox.Show($"Lỗi đăng nhập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Đăng nhập";
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LinkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Chức năng quên mật khẩu đang được phát triển!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            txtUsername.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                btnExit_Click(this, EventArgs.Empty);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
