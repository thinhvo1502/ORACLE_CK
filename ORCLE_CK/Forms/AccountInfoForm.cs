using ORCLE_CK.Constants;
using ORCLE_CK.Models;
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
    public partial class AccountInfoForm : Form
    {
        private readonly User currentUser;

        private Label lblUserId;
        private Label lblFullName;
        private Label lblUsername;
        private Label lblEmail;
        private Label lblRole;
        private Label lblCreatedAt;
        private Label lblLastLoginAt;
        private Button btnClose;

        public AccountInfoForm(User user)
        {
            currentUser = user ?? throw new ArgumentNullException(nameof(user));
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            lblUserId.Text = currentUser.UserId.ToString();
            lblFullName.Text = currentUser.FullName;
            lblUsername.Text = currentUser.Username;
            lblEmail.Text = currentUser.Email;
            lblRole.Text = currentUser.RoleDisplayName;
            lblCreatedAt.Text = currentUser.CreatedAt.ToString(AppConstants.DATETIME_FORMAT);
            lblLastLoginAt.Text = currentUser.LastLoginAt?.ToString(AppConstants.DATETIME_FORMAT) ?? "Chưa đăng nhập";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
