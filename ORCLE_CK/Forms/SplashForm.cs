using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
namespace ORCLE_CK.Forms
{
    public partial class SplashForm : Form
    {
        private ProgressBar progressBar;
        private Label lblStatus;
        private Label lblVersion;
        private Timer timer;
        private int progress = 0;

        public SplashForm()
        {
            InitializeComponent();
            InitializeTimer();
        }
        private void InitializeTimer()
        {
            timer = new Timer
            {
                Interval = 50
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            progress += 2;
            progressBar.Value = Math.Min(progress, 100);

            // Update status based on progress
            if (progress < 20)
                lblStatus.Text = "Đang khởi tạo ứng dụng...";
            else if (progress < 40)
                lblStatus.Text = "Đang tải cấu hình...";
            else if (progress < 60)
                lblStatus.Text = "Đang kết nối cơ sở dữ liệu...";
            else if (progress < 80)
                lblStatus.Text = "Đang tải giao diện...";
            else if (progress < 100)
                lblStatus.Text = "Hoàn tất...";

            // Simulate some work
            if (progress == 60)
            {
                // Test database connection in background thread
                var thread = new Thread(() =>
                {
                    try
                    {
                        var connectionTest = Data.DatabaseConnection.TestConnection();
                        if (!connectionTest)
                        {
                            this.Invoke(new Action(() =>
                            {
                                lblStatus.Text = "Cảnh báo: Không thể kết nối cơ sở dữ liệu";
                                lblStatus.ForeColor = Color.Red;
                            }));
                        }
                    }
                    catch
                    {
                        this.Invoke(new Action(() =>
                        {
                            lblStatus.Text = "Cảnh báo: Lỗi kết nối cơ sở dữ liệu";
                            lblStatus.ForeColor = Color.Red;
                        }));
                    }
                });
                thread.Start();
            }

            if (progress >= 100)
            {
                timer.Stop();
                Thread.Sleep(500); // Show completed state briefly
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Draw border
            using (var pen = new Pen(Color.DarkBlue, 2))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, this.Width - 1, this.Height - 1);
            }
        }
    }
}
