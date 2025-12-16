using SocialManager.frm.UserControls;
using SocialManager.middlewares;
using SocialManager.services;
using SocialManager.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialManager.frm
{
    //public partial class frmAdmin : ProtectFormMiddleware
    public partial class frmAdmin : Form
    {
        private bool sidebarCollapsed = false;
        private int originalSidebarWidth = 280;
        private int collapsedSidebarWidth = 60;

        // User Controls for subforms - use base UserControl type for flexibility
        private UserControl dashboardControl;
        private UserControl postsControl;
        private UserControl commentsControl;
        private UserControl reportsControl;
        private UserControl analyticsControl;
        private UserControl settingsControl;
        private UserControl socialAccountsControl;

        public frmAdmin()
        {
            System.Diagnostics.Debug.WriteLine("frmAdmin constructor started");

            try
            {
                System.Diagnostics.Debug.WriteLine("Calling InitializeComponent...");
                InitializeComponent();
                System.Diagnostics.Debug.WriteLine("InitializeComponent completed");

                System.Diagnostics.Debug.WriteLine("Calling InitializeForm...");
                InitializeForm();
                System.Diagnostics.Debug.WriteLine("InitializeForm completed");
                
                // Apply global settings on startup
                ApplyGlobalSettings();

                System.Diagnostics.Debug.WriteLine("frmAdmin constructor completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in frmAdmin constructor: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                throw; // Re-throw to let caller handle
            }
        }

        private void frmAdmin_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("frmAdmin_Load event fired");

            try
            {
                // Ensure everything is properly initialized
                if (dashboardControl == null)
                {
                    System.Diagnostics.Debug.WriteLine("dashboardControl is null, reinitializing...");
                    InitializeUserControls();
                }

                // Force show dashboard
                System.Diagnostics.Debug.WriteLine("Force showing dashboard...");
                ShowDashboard();

                // Add debug button to test analytics
                AddDebugButtonToAdmin();

                System.Diagnostics.Debug.WriteLine("frmAdmin_Load completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in frmAdmin_Load: {ex.Message}");
                MessageBox.Show($"Lỗi khi khởi tạo form quản trị: {ex.Message}", "Lỗi khởi tạo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddDebugButtonToAdmin()
        {
            try
            {
                var btnDebugAnalytics = new Button
                {
                    Text = "🔧 Kiểm tra Thống kê",
                    BackColor = Color.FromArgb(230, 126, 34),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(120, 30),
                    Location = new Point(this.Width - 140, 10),
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };

                btnDebugAnalytics.FlatAppearance.BorderSize = 0;
                btnDebugAnalytics.Click += BtnDebugAnalytics_Click;

                this.Controls.Add(btnDebugAnalytics);
                btnDebugAnalytics.BringToFront();

                System.Diagnostics.Debug.WriteLine("frmAdmin: Debug button added");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error adding debug button to admin: {ex.Message}");
            }
        }

        private void BtnDebugAnalytics_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("=== ADMIN DEBUG ANALYTICS ===");

                string analyticsType = analyticsControl?.GetType().Name ?? "NULL";
                string analyticsStatus = analyticsControl?.GetType().Name.Contains("SimpleControl") == true ? "FALLBACK" : "REAL";

                System.Diagnostics.Debug.WriteLine($"Current analyticsControl type: {analyticsType}");
                System.Diagnostics.Debug.WriteLine($"Status: {analyticsStatus}");

                // Thử tạo ucAnalytics mới
                try
                {
                    var testAnalytics = new ucAnalytics();
                    MessageBox.Show($"✅ Kiểm tra Thống kê Thành công!\n\nControl hiện tại: {analyticsType}\nTrạng thái: {analyticsStatus}\n\nClick tab Thống kê để xem giao diện thật!",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Replace the fallback with real one
                    analyticsControl = testAnalytics;
                    analyticsControl.Dock = DockStyle.Fill;

                    System.Diagnostics.Debug.WriteLine("✅ Replaced fallback with real ucAnalytics");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"❌ Kiểm tra Thống kê Thất bại!\n\nLỗi: {ex.Message}\n\nControl hiện tại: {analyticsType}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Diagnostics.Debug.WriteLine($"❌ Test failed: {ex.Message}");
                }

                System.Diagnostics.Debug.WriteLine("=== ADMIN DEBUG COMPLETED ===");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Debug Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeForm()
        {
            // Setup initial active button
            SetActiveButton(btnDashboard);

            // Optimize all layouts for better distribution
            OptimizeNavigationLayout();

            // Initialize UserControls FIRST
            InitializeUserControls();

            // Set up responsive design
            SetupResponsiveDesign();

            // THEN show dashboard (after controls are initialized)
            ShowDashboard();
        }

        private void InitializeUserControls()
        {
            try
            {
                // Initialize Dashboard Control
                try
                {
                    dashboardControl = new ucDashboard() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucDashboard initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucDashboard: {ex.Message}");

                    try
                    {
                        // Try simple dashboard fallback
                        dashboardControl = new ucSimpleDashboard() { Dock = DockStyle.Fill };
                        System.Diagnostics.Debug.WriteLine("ucSimpleDashboard fallback initialized");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading ucSimpleDashboard: {ex2.Message}");
                        // Final fallback
                        dashboardControl = CreateSimpleUserControl("Trang chủ", "Đang tải trang chủ...");
                    }
                }

                // Initialize Posts Control
                try
                {
                    postsControl = new ucPostManagement() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucPostManagement initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucPostManagement: {ex.Message}");
                    postsControl = CreateSimpleUserControl("Quản lý Bài viết", "📝 Tính năng đang phát triển...");
                }

                // Initialize Comments Control
                try
                {
                    commentsControl = new ucCommentManagement() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucCommentManagement initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucCommentManagement: {ex.Message}");
                    commentsControl = CreateSimpleUserControl("Quản lý Bình luận", "💬 Tính năng đang phát triển...");
                }

                // Initialize Reports Control
                try
                {
                    reportsControl = new ucReportManagement() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucReportManagement initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucReportManagement: {ex.Message}");
                    reportsControl = CreateSimpleUserControl("Quản lý Báo cáo", "🚨 Tính năng đang phát triển...");
                }

                // Initialize Analytics Control - Try ucAnalyticsInsights first
                try
                {
                    System.Diagnostics.Debug.WriteLine("Starting ucAnalyticsInsights initialization...");
                    analyticsControl = new ucAnalyticsInsights() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("✅ ucAnalyticsInsights initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ ERROR loading ucAnalyticsInsights: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                    // Fallback to Simple version
                    try
                    {
                        System.Diagnostics.Debug.WriteLine("Falling back to ucAnalyticsSimple...");
                        analyticsControl = new ucAnalyticsSimple() { Dock = DockStyle.Fill };
                        System.Diagnostics.Debug.WriteLine("✅ ucAnalyticsSimple initialized successfully");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ ERROR loading ucAnalyticsSimple: {ex2.Message}");
                        // Final fallback
                        analyticsControl = CreateAnalyticsFallback();
                    }
                }

                // Initialize Settings Control
                try
                {
                    settingsControl = new ucSettings() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucSettings initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucSettings: {ex.Message}");
                    settingsControl = CreateSimpleUserControl("Settings", "⚙️ Settings feature coming soon...");
                }

                // Initialize Social Accounts Control (User Management)
                try
                {
                    socialAccountsControl = new ucUserManagementNew() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucUserManagementNew initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucUserManagementNew: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                    // Fallback to old version if new version fails
                    try
                    {
                        socialAccountsControl = new ucUserManagement() { Dock = DockStyle.Fill };
                        System.Diagnostics.Debug.WriteLine("ucUserManagement (old) fallback initialized");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading ucUserManagement fallback: {ex2.Message}");
                        socialAccountsControl = CreateSimpleUserControl("User Management", "👥 User management feature coming soon...");
                    }
                }

                // Verify all controls are initialized
                if (dashboardControl == null)
                    dashboardControl = CreateSimpleUserControl("Dashboard", "Dashboard initialization failed");

                if (postsControl == null)
                    postsControl = CreateSimpleUserControl("Posts", "Posts initialization failed");

                if (commentsControl == null)
                    commentsControl = CreateSimpleUserControl("Comments", "Comments initialization failed");

                if (analyticsControl == null)
                    analyticsControl = CreateSimpleUserControl("Thống kê", "Khởi tạo thống kê thất bại");

                if (settingsControl == null)
                    settingsControl = CreateSimpleUserControl("Cài đặt", "Khởi tạo cài đặt thất bại");

                if (socialAccountsControl == null)
                    socialAccountsControl = CreateSimpleUserControl("Quản lý Người dùng", "Khởi tạo quản lý người dùng thất bại");

                System.Diagnostics.Debug.WriteLine("All UserControls initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Critical error in InitializeUserControls: {ex.Message}");
                MessageBox.Show($"Lỗi khi khởi tạo các thành phần ứng dụng: {ex.Message}\nỨng dụng sẽ tiếp tục với chức năng cơ bản.",
                    "Cảnh báo khởi tạo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Absolute emergency fallback - create all controls as simple ones
                dashboardControl = CreateSimpleUserControl("Trang chủ", "Hệ thống đang khởi động...");
                postsControl = CreateSimpleUserControl("Bài viết", "Đang tải tính năng bài viết...");
                analyticsControl = CreateSimpleUserControl("Thống kê", "Đang tải tính năng thống kê...");
                settingsControl = CreateSimpleUserControl("Cài đặt", "Đang tải cài đặt...");
                socialAccountsControl = CreateSimpleUserControl("Quản lý Người dùng", "Đang tải quản lý người dùng...");
            }
        }

        private UserControl CreateSimpleUserControl(string title, string message)
        {
            try
            {
                var control = new UserControl
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(247, 249, 252),
                    Padding = new Padding(40),
                    Name = $"SimpleControl_{title.Replace(" ", "")}"
                };

                var pnlContent = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Padding = new Padding(40)
                };

                var lblTitle = new Label
                {
                    Text = title,
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    AutoSize = true,
                    Location = new Point(40, 40),
                    BackColor = Color.Transparent
                };

                var lblMessage = new Label
                {
                    Text = message,
                    Font = new Font("Segoe UI", 14F),
                    ForeColor = Color.FromArgb(127, 140, 141),
                    AutoSize = true,
                    Location = new Point(40, 100),
                    BackColor = Color.Transparent,
                    MaximumSize = new Size(600, 0) // Allow text wrapping
                };

                var lblTime = new Label
                {
                    Text = $"Tạo lúc: {DateTime.Now:dd/MM/yyyy HH:mm:ss}",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(149, 165, 166),
                    AutoSize = true,
                    Location = new Point(40, 200),
                    BackColor = Color.Transparent
                };

                // Add all controls to content panel
                pnlContent.Controls.Add(lblTitle);
                pnlContent.Controls.Add(lblMessage);
                pnlContent.Controls.Add(lblTime);

                // Add content panel to main control
                control.Controls.Add(pnlContent);

                System.Diagnostics.Debug.WriteLine($"Created simple UserControl: {title}");
                return control;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating simple UserControl: {ex.Message}");

                // Absolute minimal fallback
                var minimalControl = new UserControl
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White
                };

                var minimalLabel = new Label
                {
                    Text = $"{title}\n{message}",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.Black
                };

                minimalControl.Controls.Add(minimalLabel);
                return minimalControl;
            }
        }

        private UserControl CreateAnalyticsFallback()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Creating Analytics fallback - trying ucAnalyticsSimple...");

                // Try to load ucAnalyticsSimple first
                try
                {
                    var analyticsSimple = new ucAnalyticsSimple
                    {
                        Dock = DockStyle.Fill,
                        Name = "AnalyticsSimple"
                    };
                    System.Diagnostics.Debug.WriteLine("✅ ucAnalyticsSimple loaded successfully!");
                    return analyticsSimple;
                }
                catch (Exception innerEx)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Failed to load ucAnalyticsSimple: {innerEx.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack trace: {innerEx.StackTrace}");
                    // Continue to final fallback UI
                }

                // Fallback UI
                System.Diagnostics.Debug.WriteLine("Creating Analytics fallback UI...");

                var control = new UserControl
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(247, 249, 252),
                    Padding = new Padding(30),
                    Name = "AnalyticsFallback"
                };

                var mainPanel = new Panel
                {
                    Dock = DockStyle.Fill,
                    BackColor = Color.White,
                    Padding = new Padding(30)
                };

                var titleLabel = new Label
                {
                    Text = "📊 Thống kê & Báo cáo",
                    Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(44, 62, 80),
                    AutoSize = true,
                    Location = new Point(30, 30)
                };

                var messageLabel = new Label
                {
                    Text = "Analytics module đang được tải...\n\n" +
                           "📈 Thống kê người dùng\n" +
                           "📊 Phân tích bài viết\n" +
                           "💹 Báo cáo tương tác\n" +
                           "📋 Xuất báo cáo\n\n" +
                           "Click \"🔧 Test Analytics\" để thử tải lại module thực.",
                    Font = new Font("Segoe UI", 12F),
                    ForeColor = Color.FromArgb(127, 140, 141),
                    AutoSize = true,
                    Location = new Point(30, 80),
                    MaximumSize = new Size(500, 0)
                };

                var statusLabel = new Label
                {
                    Text = $"Trạng thái: Fallback mode - {DateTime.Now:HH:mm:ss}",
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(230, 126, 34),
                    AutoSize = true,
                    Location = new Point(30, 250)
                };

                var retryButton = new Button
                {
                    Text = "🔄 Thử Tải Lại Analytics",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(200, 40),
                    Location = new Point(30, 300),
                    Cursor = Cursors.Hand
                };

                retryButton.FlatAppearance.BorderSize = 0;
                retryButton.Click += (s, e) =>
                {
                    try
                    {
                        // Thử ucAnalyticsSimple trước (không cần Chart dependencies)
                        try
                        {
                            var simpleAnalytics = new ucAnalyticsSimple() { Dock = DockStyle.Fill };
                            analyticsControl = simpleAnalytics;
                            MessageBox.Show("✅ Thống kê (Đơn giản) đã được tải thành công!\n\nGiao diện Thống kê không có biểu đồ nhưng hiển thị đầy đủ dữ liệu.",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex1)
                        {
                            System.Diagnostics.Debug.WriteLine($"ucAnalyticsSimple fails: {ex1.Message}");

                            MessageBox.Show($"❌ Không thể tải Thống kê:\n\nPhiên bản đơn giản: {ex1.Message}\n\nVui lòng kiểm tra hoặc liên hệ hỗ trợ.",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"❌ Lỗi retry: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                mainPanel.Controls.Add(titleLabel);
                mainPanel.Controls.Add(messageLabel);
                mainPanel.Controls.Add(statusLabel);
                mainPanel.Controls.Add(retryButton);

                control.Controls.Add(mainPanel);

                System.Diagnostics.Debug.WriteLine("Analytics fallback created successfully");
                return control;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating Analytics fallback: {ex.Message}");
                return CreateSimpleUserControl("Thống kê", "📊 Đang tải tính năng thống kê...\n\nVui lòng đợi hoặc khởi động lại ứng dụng.");
            }
        }

        private void SetupResponsiveDesign()
        {
            // Enable double buffering for smoother rendering
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.DoubleBuffer |
                         ControlStyles.ResizeRedraw, true);

            // Set minimum size
            this.MinimumSize = new Size(1200, 700);
        }

        private void LoadUserControlIntoMainPanel(UserControl userControl)
        {
            System.Diagnostics.Debug.WriteLine($"LoadUserControlIntoMainPanel called with: {userControl?.GetType().Name ?? "null"}");

            if (userControl == null)
            {
                System.Diagnostics.Debug.WriteLine("UserControl is null, creating emergency fallback");

                // Create emergency fallback instead of showing error
                var emergencyControl = CreateSimpleUserControl("Đang tải...", "Vui lòng đợi trong khi nội dung đang tải.");
                userControl = emergencyControl;
            }

            try
            {
                System.Diagnostics.Debug.WriteLine("Clearing main panel controls");

                // Store reference to topbar to re-add it
                var topBar = pnlTopBar;

                // Clear existing content
                pnlMain.Controls.Clear();

                // Re-add the top bar first
                if (topBar != null)
                {
                    pnlMain.Controls.Add(topBar);
                    topBar.BringToFront();
                    System.Diagnostics.Debug.WriteLine("Top bar re-added");
                }

                // Add the user control
                pnlMain.Controls.Add(userControl);
                System.Diagnostics.Debug.WriteLine($"UserControl {userControl.GetType().Name} added to main panel");

                // Ensure proper z-order
                if (topBar != null)
                {
                    topBar.BringToFront();
                }

                // Force refresh
                pnlMain.Refresh();
                System.Diagnostics.Debug.WriteLine("Main panel refreshed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadUserControlIntoMainPanel: {ex.Message}");
                MessageBox.Show($"Lỗi khi tải giao diện: {ex.Message}\nVui lòng thử nhấp vào mục menu lại.",
                    "Lỗi tải giao diện", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Try to recover by creating a simple error control
                try
                {
                    var errorControl = CreateSimpleUserControl("Lỗi Tải Giao diện",
                        $"Có lỗi khi tải giao diện này.\nLỗi: {ex.Message}\n\nVui lòng thử lại hoặc liên hệ hỗ trợ.");

                    pnlMain.Controls.Clear();
                    if (pnlTopBar != null)
                        pnlMain.Controls.Add(pnlTopBar);
                    pnlMain.Controls.Add(errorControl);
                    if (pnlTopBar != null)
                        pnlTopBar.BringToFront();
                }
                catch (Exception ex2)
                {
                    System.Diagnostics.Debug.WriteLine($"Critical error in error recovery: {ex2.Message}");
                    MessageBox.Show("Lỗi nghiêm trọng khi tải giao diện ứng dụng. Vui lòng khởi động lại ứng dụng.",
                        "Lỗi nghiêm trọng", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetActiveButton(Button activeButton)
        {
            // Reset all navigation buttons
            foreach (Control control in pnlNavigation.Controls)
            {
                if (control is Button btn && btn != btnLogout && btn != activeButton)
                {
                    btn.BackColor = Color.Transparent;
                    btn.ForeColor = Color.FromArgb(189, 195, 199);
                }
            }

            // Set active button styling
            activeButton.BackColor = Color.FromArgb(52, 152, 219);
            activeButton.ForeColor = Color.White;
        }

        private void btnToggleSidebar_Click(object sender, EventArgs e)
        {
            sidebarCollapsed = !sidebarCollapsed;

            if (sidebarCollapsed)
            {
                pnlSidebar.Width = collapsedSidebarWidth;
                lblTitle.Visible = false;
                lblSubtitle.Visible = false;
                foreach (Control control in pnlNavigation.Controls)
                {
                    if (control is Button btn)
                    {
                        btn.Text = "";
                        btn.Padding = new Padding(15, 0, 0, 0);
                    }
                }
            }
            else
            {
                pnlSidebar.Width = originalSidebarWidth;
                lblTitle.Visible = true;
                lblSubtitle.Visible = true;
                btnDashboard.Text = "Trang chủ";
                btnPosts.Text = "Bài viết";
                btnComments.Text = "Bình luận";
                btnReports.Text = "Báo cáo";
                btnAnalytics.Text = "Thống kê";
                btnSettings.Text = "Cài đặt";
                btnSocialAccounts.Text = "Người dùng";
                btnLogout.Text = "Đăng xuất";

                foreach (Control control in pnlNavigation.Controls)
                {
                    if (control is Button btn)
                    {
                        btn.Padding = new Padding(50, 0, 0, 0);
                    }
                }
            }
        }

        private void frmAdmin_Resize(object sender, EventArgs e)
        {
            // Handle responsive behavior
            if (this.Width < 1200)
            {
                if (!sidebarCollapsed)
                {
                    btnToggleSidebar_Click(sender, e);
                }
            }
        }

        private void OptimizeNavigationLayout()
        {
            if (pnlNavigation == null) return;

            int buttonHeight = 55;
            int spacing = 10;
            int startY = 30;

            // Position buttons with consistent spacing
            btnDashboard.Location = new Point(20, startY);
            btnPosts.Location = new Point(20, startY + (buttonHeight + spacing) * 1);
            btnComments.Location = new Point(20, startY + (buttonHeight + spacing) * 2);
            btnReports.Location = new Point(20, startY + (buttonHeight + spacing) * 3);
            btnAnalytics.Location = new Point(20, startY + (buttonHeight + spacing) * 4);
            btnSettings.Location = new Point(20, startY + (buttonHeight + spacing) * 5);
            btnSocialAccounts.Location = new Point(20, startY + (buttonHeight + spacing) * 6);
        }

        // Navigation event handlers
        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void btnPosts_Click(object sender, EventArgs e)
        {
            ShowPosts();
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            ShowComments();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            ShowReports();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            ShowAnalytics();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            ShowSettings();
        }

        private void btnSocialAccounts_Click(object sender, EventArgs e)
        {
            ShowSocialAccounts();
        }

        // View switching methods
        private void ShowDashboard()
        {
            SetActiveButton(btnDashboard);
            lblCurrentView.Text = "Trang chủ";
            lblBreadcrumb.Text = "Trang chủ > Bảng điều khiển";
            LoadUserControlIntoMainPanel(dashboardControl);
        }

        private void ShowPosts()
        {
            SetActiveButton(btnPosts);
            lblCurrentView.Text = "Quản lý Bài viết";
            lblBreadcrumb.Text = "Trang chủ > Bài viết";
            LoadUserControlIntoMainPanel(postsControl);
        }

        private void ShowComments()
        {
            SetActiveButton(btnComments);
            lblCurrentView.Text = "Quản lý Bình luận";
            lblBreadcrumb.Text = "Trang chủ > Bình luận";
            LoadUserControlIntoMainPanel(commentsControl);
        }

        private void ShowReports()
        {
            SetActiveButton(btnReports);
            lblCurrentView.Text = "🚨 Quản lý Báo cáo Vi phạm";
            lblBreadcrumb.Text = "Trang chủ > Báo cáo";
            LoadUserControlIntoMainPanel(reportsControl);
        }

        private void ShowAnalytics()
        {
            SetActiveButton(btnAnalytics);
            lblCurrentView.Text = "Thống kê & Báo cáo";
            lblBreadcrumb.Text = "Trang chủ > Thống kê";

            // Nếu chưa có hoặc không phải ucAnalyticsInsights, tạo mới
            if (analyticsControl == null || 
                analyticsControl.GetType().Name != "ucAnalyticsInsights")
            {
                var currentType = analyticsControl?.GetType().Name ?? "null";
                System.Diagnostics.Debug.WriteLine($"Current analytics control: {currentType}, creating new ucAnalyticsInsights...");

                try
                {
                    // Try ucAnalyticsInsights first (new comprehensive control)
                    System.Diagnostics.Debug.WriteLine("Attempting to create ucAnalyticsInsights...");
                    var newAnalyticsControl = new ucAnalyticsInsights() { Dock = DockStyle.Fill };
                    analyticsControl = newAnalyticsControl;
                    System.Diagnostics.Debug.WriteLine("✅ Successfully created ucAnalyticsInsights");
                    MessageBox.Show("✅ Analytics Insights loaded!", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"❌ Failed to create ucAnalyticsInsights: {ex.Message}");
                    System.Diagnostics.Debug.WriteLine($"Stack: {ex.StackTrace}");
                    MessageBox.Show($"❌ Error loading Analytics:\n{ex.Message}\n\nUsing fallback UI.", 
                        "Debug", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    // Fallback to CreateAnalyticsFallback
                    try
                    {
                        analyticsControl = CreateAnalyticsFallback();
                        System.Diagnostics.Debug.WriteLine("✅ Using fallback Analytics UI");
                    }
                    catch (Exception ex2)
                    {
                        System.Diagnostics.Debug.WriteLine($"❌ Critical: Failed to create fallback: {ex2.Message}");
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("✅ ucAnalyticsInsights already loaded, reusing...");
            }

            LoadUserControlIntoMainPanel(analyticsControl);
        }

        private void ShowSettings()
        {
            SetActiveButton(btnSettings);
            lblCurrentView.Text = "Cài đặt Hệ thống";
            lblBreadcrumb.Text = "Trang chủ > Cài đặt";
            LoadUserControlIntoMainPanel(settingsControl);
        }

        private void ShowSocialAccounts()
        {
            SetActiveButton(btnSocialAccounts);
            lblCurrentView.Text = "Quản lý Người dùng";
            lblBreadcrumb.Text = "Trang chủ > Người dùng";
            LoadUserControlIntoMainPanel(socialAccountsControl);
        }

        // Icon drawing methods
        private void picLogo_Paint(object sender, PaintEventArgs e)
        {
            var rect = new Rectangle(8, 8, 34, 34);
            DrawModernIcon(e.Graphics, rect, Color.FromArgb(52, 152, 219), (g, r) =>
            {
                // Draw modern logo - social media symbol
                using (var pen = new Pen(Color.White, 3))
                {
                    g.FillEllipse(new SolidBrush(Color.FromArgb(52, 152, 219)), r);
                    g.DrawEllipse(pen, r.X + 8, r.Y + 8, 6, 6);
                    g.DrawEllipse(pen, r.X + 20, r.Y + 8, 6, 6);
                    g.DrawEllipse(pen, r.X + 14, r.Y + 20, 6, 6);
                    g.DrawLine(pen, r.X + 14, r.Y + 11, r.X + 20, r.Y + 11);
                    g.DrawLine(pen, r.X + 17, r.Y + 14, r.X + 17, r.Y + 20);
                }
            });
        }

        private void DrawModernIcon(Graphics g, Rectangle rect, Color color, Action<Graphics, Rectangle> drawAction)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(color))
            {
                drawAction(g, rect);
            }
        }

        private void btnDashboard_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20), Color.White, "dashboard");
        }

        private void btnPosts_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnPosts.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "posts");
        }

        private void btnComments_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnComments.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "comments");
        }

        private void btnReports_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnReports.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "reports");
        }

        private void btnAnalytics_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnAnalytics.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "analytics");
        }

        private void btnSettings_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnSettings.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "settings");
        }

        private void btnSocialAccounts_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20),
                btnSocialAccounts.BackColor == Color.Transparent ? Color.FromArgb(189, 195, 199) : Color.White, "social");
        }

        private void btnLogout_Paint(object sender, PaintEventArgs e)
        {
            DrawNavigationIcon(e.Graphics, new Rectangle(15, 18, 20, 20), Color.White, "logout");
        }

        private void DrawNavigationIcon(Graphics g, Rectangle rect, Color color, string iconType)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (var pen = new Pen(color, 2.5f) { LineJoin = LineJoin.Round })
            using (var brush = new SolidBrush(color))
            {
                // Center the icon better
                int centerX = rect.X + rect.Width / 2;
                int centerY = rect.Y + rect.Height / 2;
                int iconSize = Math.Min(rect.Width, rect.Height) - 4;

                switch (iconType)
                {
                    case "dashboard":
                        // Grid icon - better centered
                        int gridSize = iconSize / 3;
                        g.FillRectangle(brush, centerX - gridSize, centerY - gridSize, gridSize - 2, gridSize - 2);
                        g.FillRectangle(brush, centerX + 2, centerY - gridSize, gridSize - 2, gridSize - 2);
                        g.FillRectangle(brush, centerX - gridSize, centerY + 2, gridSize - 2, gridSize - 2);
                        g.FillRectangle(brush, centerX + 2, centerY + 2, gridSize - 2, gridSize - 2);
                        break;

                    case "posts":
                        // Document icon - better proportions
                        g.DrawRectangle(pen, centerX - 8, centerY - 10, 16, 20);
                        g.DrawLine(pen, centerX - 5, centerY - 6, centerX + 5, centerY - 6);
                        g.DrawLine(pen, centerX - 5, centerY - 2, centerX + 5, centerY - 2);
                        g.DrawLine(pen, centerX - 5, centerY + 2, centerX + 2, centerY + 2);
                        break;

                    case "comments":
                        // Comment bubble icon
                        g.DrawRectangle(pen, centerX - 8, centerY - 6, 14, 10);
                        // Tail of comment bubble
                        g.DrawLine(pen, centerX - 5, centerY + 4, centerX - 6, centerY + 7);
                        g.DrawLine(pen, centerX - 6, centerY + 7, centerX - 3, centerY + 4);
                        // Three dots inside
                        g.FillEllipse(brush, centerX - 5, centerY - 2, 2, 2);
                        g.FillEllipse(brush, centerX - 1, centerY - 2, 2, 2);
                        g.FillEllipse(brush, centerX + 3, centerY - 2, 2, 2);
                        break;

                    case "reports":
                        // Alert/Warning icon - triangle with exclamation mark
                        // Draw triangle
                        Point[] triangle = new Point[]
                        {
                            new Point(centerX, centerY - 9),
                            new Point(centerX - 9, centerY + 8),
                            new Point(centerX + 9, centerY + 8)
                        };
                        g.DrawPolygon(pen, triangle);
                        // Exclamation mark
                        g.DrawLine(pen, centerX, centerY - 4, centerX, centerY + 2);
                        g.FillEllipse(brush, centerX - 1, centerY + 5, 2, 2);
                        break;

                    case "analytics":
                        // Chart icon - more balanced
                        g.DrawLine(pen, centerX - 8, centerY + 8, centerX + 8, centerY + 8);
                        g.DrawLine(pen, centerX - 8, centerY - 8, centerX - 8, centerY + 8);
                        // Bars with better spacing
                        g.DrawLine(pen, centerX - 4, centerY + 4, centerX - 4, centerY + 8);
                        g.DrawLine(pen, centerX, centerY, centerX, centerY + 8);
                        g.DrawLine(pen, centerX + 4, centerY - 4, centerX + 4, centerY + 8);
                        break;

                    case "settings":
                        // Gear icon - cleaner design
                        g.DrawEllipse(pen, centerX - 6, centerY - 6, 12, 12);
                        g.DrawEllipse(pen, centerX - 3, centerY - 3, 6, 6);
                        // 8 gear teeth
                        for (int i = 0; i < 8; i++)
                        {
                            double angle = i * Math.PI / 4;
                            int x1 = (int)(centerX + 6 * Math.Cos(angle));
                            int y1 = (int)(centerY + 6 * Math.Sin(angle));
                            int x2 = (int)(centerX + 9 * Math.Cos(angle));
                            int y2 = (int)(centerY + 9 * Math.Sin(angle));
                            g.DrawLine(pen, x1, y1, x2, y2);
                        }
                        break;

                    case "social":
                        // Network icon - better connection lines
                        g.DrawEllipse(pen, centerX - 8, centerY - 6, 6, 6);
                        g.DrawEllipse(pen, centerX + 2, centerY - 6, 6, 6);
                        g.DrawEllipse(pen, centerX - 3, centerY + 2, 6, 6);
                        // Connection lines
                        g.DrawLine(pen, centerX - 2, centerY - 3, centerX + 2, centerY - 3);
                        g.DrawLine(pen, centerX - 5, centerY, centerX - 3, centerY + 2);
                        g.DrawLine(pen, centerX + 5, centerY, centerX + 3, centerY + 2);
                        break;

                    case "logout":
                        // Exit icon - more intuitive
                        g.DrawRectangle(pen, centerX - 6, centerY - 4, 8, 8);
                        g.DrawLine(pen, centerX + 2, centerY, centerX + 8, centerY);
                        // Arrow
                        g.DrawLine(pen, centerX + 6, centerY - 2, centerX + 8, centerY);
                        g.DrawLine(pen, centerX + 6, centerY + 2, centerX + 8, centerY);
                        break;
                }
            }
        }

        // Add missing lblTitle_Click event handler
        private void lblTitle_Click(object sender, EventArgs e)
        {
            // Optional: Navigate to home/dashboard when title is clicked
            ShowDashboard();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận Đăng xuất",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Clear authentication
                AuthSessionService.Logout();

                // Create and show login form BEFORE closing admin form
                frmLogin loginForm = new frmLogin();
                //loginForm.Show();

                // Close admin form
                this.Close();
            }
        }

        private void lblBreadcrumb_Click(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// Áp dụng cấu hình global khi khởi động form
        /// </summary>
        private void ApplyGlobalSettings()
        {
            try
            {
                // Apply system name to form title
                this.Text = $"{GlobalSettings.SystemName} - Admin Panel";
                
                // Update title label if exists
                if (lblTitle != null)
                {
                    lblTitle.Text = GlobalSettings.SystemName;
                }
                
                // Update title with SolidVerse name
                if (lblTitle != null)
                {
                    lblTitle.Text = "SolidVerse";
                }
                
                // Apply theme
                GlobalSettings.ApplyTheme(GlobalSettings.Theme);
                
                // Check maintenance mode
                if (GlobalSettings.MaintenanceMode)
                {
                    // Show maintenance indicator (could add a label or status)
                    this.Text += " [🔧 BẢO TRÌ]";
                }
                
                System.Diagnostics.Debug.WriteLine($"Applied global settings - System: {GlobalSettings.SystemName}, Theme: {GlobalSettings.Theme}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error applying global settings: {ex.Message}");
            }
        }
    }

    // Extension method for rounded rectangles (same as admin form)
    public static class GraphicsExtensions
    {
        public static void FillRoundedRectangle(this Graphics graphics, Brush brush, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreateRoundedRectPath(rect, radius))
            {
                graphics.FillPath(brush, path);
            }
        }

        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle rect, int radius)
        {
            using (GraphicsPath path = CreateRoundedRectPath(rect, radius))
            {
                graphics.DrawPath(pen, path);
            }
        }

        private static GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            
            return path;
        }
    }
}

