using SocialManager.frm.UserControls;
using SocialManager.middlewares;
using SocialManager.services;
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
        private string currentView = "Dashboard";
        private bool sidebarCollapsed = false;
        private int originalSidebarWidth = 280;
        private int collapsedSidebarWidth = 60;

        // User Controls for subforms - use base UserControl type for flexibility
        private UserControl dashboardControl;
        private UserControl postsControl;
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

                System.Diagnostics.Debug.WriteLine("frmAdmin_Load completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in frmAdmin_Load: {ex.Message}");
                MessageBox.Show($"Error initializing admin form: {ex.Message}", "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                        dashboardControl = CreateSimpleUserControl("Dashboard", "Dashboard is loading...");
                    }
                }

                // Initialize Posts Control
                try
                {
                    postsControl = new ucPosts() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucPosts initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucPosts: {ex.Message}");
                    postsControl = CreateSimpleUserControl("Posts Management", "📝 Posts feature coming soon...");
                }

                // Initialize Analytics Control
                try
                {
                    analyticsControl = new ucAnalytics() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucAnalytics initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucAnalytics: {ex.Message}");
                    analyticsControl = CreateSimpleUserControl("Analytics & Reports", "📊 Analytics feature coming soon...");
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

                // Initialize Social Accounts Control
                try
                {
                    socialAccountsControl = new ucSocialAccounts() { Dock = DockStyle.Fill };
                    System.Diagnostics.Debug.WriteLine("ucSocialAccounts initialized successfully");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading ucSocialAccounts: {ex.Message}");
                    socialAccountsControl = CreateSimpleUserControl("Social Accounts", "🔗 Social accounts feature coming soon...");
                }

                // Verify all controls are initialized
                if (dashboardControl == null)
                    dashboardControl = CreateSimpleUserControl("Dashboard", "Dashboard initialization failed");

                if (postsControl == null)
                    postsControl = CreateSimpleUserControl("Posts", "Posts initialization failed");

                if (analyticsControl == null)
                    analyticsControl = CreateSimpleUserControl("Analytics", "Analytics initialization failed");

                if (settingsControl == null)
                    settingsControl = CreateSimpleUserControl("Settings", "Settings initialization failed");

                if (socialAccountsControl == null)
                    socialAccountsControl = CreateSimpleUserControl("Social Accounts", "Social accounts initialization failed");

                System.Diagnostics.Debug.WriteLine("All UserControls initialized successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Critical error in InitializeUserControls: {ex.Message}");
                MessageBox.Show($"Error initializing application components: {ex.Message}\nThe application will continue with basic functionality.",
                    "Initialization Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Absolute emergency fallback - create all controls as simple ones
                dashboardControl = CreateSimpleUserControl("Dashboard", "System is starting up...");
                postsControl = CreateSimpleUserControl("Posts", "Posts feature loading...");
                analyticsControl = CreateSimpleUserControl("Analytics", "Analytics feature loading...");
                settingsControl = CreateSimpleUserControl("Settings", "Settings feature loading...");
                socialAccountsControl = CreateSimpleUserControl("Social Accounts", "Social accounts feature loading...");
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
                    Text = $"Generated at: {DateTime.Now:yyyy-MM-dd HH:mm:ss}",
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
                var emergencyControl = CreateSimpleUserControl("Loading...", "Please wait while the content loads.");
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
                MessageBox.Show($"Error loading view: {ex.Message}\nPlease try clicking the menu item again.",
                    "View Loading Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                // Try to recover by creating a simple error control
                try
                {
                    var errorControl = CreateSimpleUserControl("Error Loading View",
                        $"There was an error loading this view.\nError: {ex.Message}\n\nPlease try again or contact support.");

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
                    MessageBox.Show("Critical error loading application views. Please restart the application.",
                        "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                btnDashboard.Text = "Dashboard";
                btnPosts.Text = "Posts";
                btnAnalytics.Text = "Analytics";
                btnSettings.Text = "Settings";
                btnSocialAccounts.Text = "Social Accounts";
                btnLogout.Text = "Logout";

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
            btnAnalytics.Location = new Point(20, startY + (buttonHeight + spacing) * 2);
            btnSettings.Location = new Point(20, startY + (buttonHeight + spacing) * 3);
            btnSocialAccounts.Location = new Point(20, startY + (buttonHeight + spacing) * 4);
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
            currentView = "Dashboard";
            lblCurrentView.Text = "Dashboard";
            lblBreadcrumb.Text = "Home > Dashboard";
            LoadUserControlIntoMainPanel(dashboardControl);
        }

        private void ShowPosts()
        {
            SetActiveButton(btnPosts);
            currentView = "Posts";
            lblCurrentView.Text = "Posts Management";
            lblBreadcrumb.Text = "Home > Posts";
            LoadUserControlIntoMainPanel(postsControl);
        }

        private void ShowAnalytics()
        {
            SetActiveButton(btnAnalytics);
            currentView = "Analytics";
            lblCurrentView.Text = "Analytics & Reports";
            lblBreadcrumb.Text = "Home > Analytics";
            LoadUserControlIntoMainPanel(analyticsControl);
        }

        private void ShowSettings()
        {
            SetActiveButton(btnSettings);
            currentView = "Settings";
            lblCurrentView.Text = "Settings";
            lblBreadcrumb.Text = "Home > Settings";
            LoadUserControlIntoMainPanel(settingsControl);
        }

        private void ShowSocialAccounts()
        {
            SetActiveButton(btnSocialAccounts);
            currentView = "Social Accounts";
            lblCurrentView.Text = "Social Accounts";
            lblBreadcrumb.Text = "Home > Social Accounts";
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
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", 
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
