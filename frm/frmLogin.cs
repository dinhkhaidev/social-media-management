using SocialManager.services;
using SocialManager.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialManager.frm
{
    public partial class frmLogin : Form
    {
        private readonly UserService? _userService;
        public frmLogin()
        {
            InitializeComponent();

            // Skip initialization in design mode
            if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            InitializeForm();
            _userService = new UserService();
            
            // Check maintenance mode on startup
            CheckMaintenanceMode();
        }

        private void InitializeForm()
        {
            // Update form title
            this.Text = "SolidVerse - Đăng nhập";
            
            // Setup form properties
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer |
                         ControlStyles.ResizeRedraw, true);
            
            // Update app title labels if they exist
            var titleLabels = this.Controls.Find("lblTitle", true).Concat(this.Controls.Find("lblAppName", true));
            foreach (Label lbl in titleLabels.OfType<Label>())
            {
                lbl.Text = "SolidVerse";
            }

            // Remove labels since we have placeholders
            if (pnlLoginForm.Controls.Contains(lblUsername))
                pnlLoginForm.Controls.Remove(lblUsername);
            if (pnlLoginForm.Controls.Contains(lblPassword))
                pnlLoginForm.Controls.Remove(lblPassword);

            // Setup input field events
            //txtUsername.Enter += TxtUsername_Enter;
            //txtUsername.Leave += TxtUsername_Leave;
            //txtPassword.Enter += TxtPassword_Enter;
            //txtPassword.Leave += TxtPassword_Leave;

            // Setup Enter key handling
            txtUsername.KeyPress += Input_KeyPress;
            txtPassword.KeyPress += Input_KeyPress;
            this.AcceptButton = btnLogin;

            // Apply rounded corners
            UIHelper.ApplyRoundedCorners(pnlLoginForm, 20);
            UIHelper.ApplyRoundedCorners(pnlUsername, 12);
            UIHelper.ApplyRoundedCorners(pnlPassword, 12);
            UIHelper.ApplyRoundedCorners(btnLogin, 10);
            // btnRegister and btnForgotPassword are LinkLabels, not Buttons - skip
        }

        private void Input_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender!, e);
            }
        }

        private void TxtUsername_Enter(object sender, EventArgs e)
        {
            pnlUsername.BackColor = Color.FromArgb(240, 248, 255);
            pnlUsername.Invalidate();
        }

        private void TxtUsername_Leave(object sender, EventArgs e)
        {
            pnlUsername.BackColor = Color.FromArgb(248, 249, 250);
            pnlUsername.Invalidate();
        }

        private void TxtPassword_Enter(object sender, EventArgs e)
        {
            pnlPassword.BackColor = Color.FromArgb(240, 248, 255);
            pnlPassword.Invalidate();
        }

        private void TxtPassword_Leave(object sender, EventArgs e)
        {
            pnlPassword.BackColor = Color.FromArgb(248, 249, 250);
            pnlPassword.Invalidate();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Check maintenance mode but allow admin login
            GlobalSettings.LoadSettings();
            bool isMaintenanceMode = GlobalSettings.MaintenanceMode;
            bool isAdminUser = false;
            
            // Pre-check if user is admin
            if (isMaintenanceMode && _userService != null && _userService.AuthenticateUser(username, password))
            {
                User? user = _userService.GetUserByUsername(username);
                isAdminUser = user?.Role == 1;
            }
            
            // Block non-admin users in maintenance mode
            if (isMaintenanceMode && !isAdminUser)
            {
                MessageBox.Show(
                    "🔧 Hệ thống đang bảo trì\n\n" +
                    "Hệ thống hiện đang trong chế độ bảo trì.\n" +
                    "Chỉ quản trị viên mới có thể truy cập.\n\n" +
                    "Vui lòng thử lại sau ít phút.",
                    "Hệ thống bảo trì",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }


            // Show loading state
            btnLogin.Text = "ĐANG ĐĂNG NHẬP...";
            btnLogin.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                // Check if service is initialized
                if (_userService == null)
                {
                    MessageBox.Show("Lỗi khởi tạo dịch vụ. Vui lòng khởi động lại ứng dụng.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Force refresh user data before authentication to get latest data
                //_userService.RefreshUser();

                // Check authentication with detailed error message
                string authError = _userService.GetAuthenticationError(username, password);
                
                if (string.IsNullOrEmpty(authError))
                {
                    // Get the most current user data after authentication
                    User? foundUser = _userService.GetUserByUsername(username);

                    if (foundUser != null)
                    {
                        AuthSessionService.Login(foundUser);

                        // Successful login
                        MessageBox.Show("Đăng nhập thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        try
                        {
                            // Create admin form
                            if (AuthSessionService.CurrentUser != null && AuthSessionService.CurrentUser.Role == 1)
                            {
                                frmAdmin adminForm = new frmAdmin();

                                // Hide login form FIRST
                                this.Hide();

                                // Show admin form as dialog
                                var adminResult = adminForm.ShowDialog();

                            }
                            else if (AuthSessionService.CurrentUser != null)
                            {
                                // Mở Dashboard user (có thanh navigation ở trên)
                                frmDashboard dashboardForm = new frmDashboard();
                                this.Hide();

                                // Show dashboard form as dialog
                                var dashboardResult = dashboardForm.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản của bạn không có quyền truy cập.",
                                    "Truy cập bị từ chối", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                                // Show login form again
                                this.Show();
                            }
                            // After dashboard form closes, check if user is still logged in
                            if (!AuthSessionService.IsLoggedIn)
                            {
                                // User logged out, show login form again and clear inputs
                                this.Show();
                                txtPassword.Clear();
                                txtUsername.Focus();
                            }
                            else
                            {
                                // User closed dashboard form but still logged in, close application
                                this.Close();
                            }
                        }
                        catch (Exception adminEx)
                        {
                            MessageBox.Show($"Lỗi khi mở dashboard: {adminEx.Message}",
                                "Lỗi Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Show login form again
                            this.Show();
                        }
                    }
                    else
                    {
                        // Failed to get user data after authentication
                        MessageBox.Show("Đăng nhập thành công nhưng không thể tải dữ liệu người dùng. Vui lòng thử lại.", "Lỗi đăng nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        txtPassword.Clear();
                        txtUsername.Focus();
                    }
                }
                else
                {
                    // Failed login with specific error message
                    MessageBox.Show(authError, "Đăng nhập thất bại",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra khi đăng nhập: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Reset button state
                btnLogin.Text = "ĐĂNG NHẬP";
                btnLogin.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập hoặc email.", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Lỗi nhập liệu",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void llblForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để đặt lại mật khẩu.", "Quên mật khẩu",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void llblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Pass reference của login form hiện tại để không tạo form mới
            frmRegister registerForm = new frmRegister(this); // Pass login form reference

            // Hide login form
            this.Hide();

            // Show register form as dialog
            var result = registerForm.ShowDialog();

            // Refresh user service data when returning from registration
            _userService?.RefreshUser();

            // Show login form again after register is closed
            this.Show();

            // Optional: Focus on username field for new login attempt
            txtUsername.Focus();
        }

        public void SetUsername(string username)
        {
            if (!string.IsNullOrWhiteSpace(username))
            {
                txtUsername.Text = username;
                txtPassword.Focus(); // Focus on password field
            }
        }

        public void RefreshUserData()
        {
            try
            {
                _userService?.RefreshUser();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error refreshing user data in login form: {ex.Message}");
            }
        }

        // Custom paint events for modern UI - using GraphicsExtensions from Admin form
        private void pnlLoginCard_Paint(object sender, PaintEventArgs e)
        {
            Panel? panel = sender as Panel;
            if (panel != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                // Draw subtle shadow
                using (var shadowBrush = new SolidBrush(Color.FromArgb(15, 0, 0, 0)))
                {
                    GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(5, 5, panel.Width - 5, panel.Height - 5), 15);
                }

                // Draw main card
                using (var cardBrush = new SolidBrush(Color.White))
                {
                    GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 5, panel.Height - 5), 15);
                }
            }
        }

        private void pnlInput_Paint(object sender, PaintEventArgs e)
        {
            Panel? panel = sender as Panel;
            if (panel != null)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                using (var brush = new SolidBrush(panel.BackColor))
                {
                    GraphicsExtensions.FillRoundedRectangle(e.Graphics, brush, new Rectangle(0, 0, panel.Width, panel.Height), 8);
                }

                // Draw border
                Color borderColor = panel.BackColor == Color.FromArgb(240, 248, 255) ?
                    Color.FromArgb(52, 152, 219) : Color.FromArgb(220, 221, 222);
                using (var pen = new Pen(borderColor, 1))
                {
                    GraphicsExtensions.DrawRoundedRectangle(e.Graphics, pen, new Rectangle(0, 0, panel.Width - 1, panel.Height - 1), 8);
                }
            }
        }

        private void picLogo_Paint(object sender, PaintEventArgs e)
        {
            DrawLogo(e.Graphics, new Rectangle(5, 5, 40, 40), Color.FromArgb(52, 152, 219));
        }

        private void picAppLogo_Paint(object sender, PaintEventArgs e)
        {
            DrawLogo(e.Graphics, new Rectangle(10, 10, 60, 60), Color.White);
        }

        private void picUsernameIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawUserIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
        }

        private void picPasswordIcon_Paint(object sender, PaintEventArgs e)
        {
            DrawLockIcon(e.Graphics, new Rectangle(2, 2, 16, 16), Color.FromArgb(127, 140, 141));
        }

        private void DrawLogo(Graphics g, Rectangle rect, Color color)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            using (var pen = new Pen(Color.White, 2))
            {
                // Social media logo - connected circles
                g.FillEllipse(brush, rect);
                g.DrawEllipse(pen, rect.X + 8, rect.Y + 8, 12, 12);
                g.DrawEllipse(pen, rect.X + 25, rect.Y + 8, 12, 12);
                g.DrawEllipse(pen, rect.X + 16, rect.Y + 25, 12, 12);
                g.DrawLine(pen, rect.X + 20, rect.Y + 14, rect.X + 25, rect.Y + 14);
                g.DrawLine(pen, rect.X + 22, rect.Y + 20, rect.X + 22, rect.Y + 25);
            }
        }

        private void DrawUserIcon(Graphics g, Rectangle rect, Color color)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(color, 1.5f))
            {
                // User icon
                g.DrawEllipse(pen, rect.X + 4, rect.Y + 2, 8, 8);
                g.DrawArc(pen, rect.X + 2, rect.Y + 8, 12, 8, 0, 180);
            }
        }

        private void DrawLockIcon(Graphics g, Rectangle rect, Color color)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(color, 1.5f))
            {
                // Lock icon
                g.DrawRectangle(pen, rect.X + 3, rect.Y + 8, 10, 6);
                g.DrawArc(pen, rect.X + 5, rect.Y + 3, 6, 8, 180, 180);
                g.FillEllipse(new SolidBrush(color), rect.X + 7, rect.Y + 10, 2, 2);
            }
        }

        private void lblSubtitle_Click(object sender, EventArgs e)
        {

        }

        private void lblAppDescription_Click(object sender, EventArgs e)
        {

        }
        
        private void CheckMaintenanceMode()
        {
            // Force reload settings from file to get latest maintenance mode status
            GlobalSettings.LoadSettings();
            
            if (GlobalSettings.MaintenanceMode)
            {
                // Show maintenance message on form title but keep controls enabled for admin
                this.Text = "SolidVerse - Hệ thống bảo trì (Admin có thể đăng nhập)";
            }
            else
            {
                // Restore normal title
                this.Text = "SolidVerse - Đăng nhập";
            }
        }
    }
}
