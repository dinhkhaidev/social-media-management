namespace SocialManager.frm
{
    partial class frmAdmin
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
            pnlSidebar = new Panel();
            pnlNavigation = new Panel();
            btnLogout = new Button();
            btnSocialAccounts = new Button();
            btnSettings = new Button();
            btnAnalytics = new Button();
            btnPosts = new Button();
            btnDashboard = new Button();
            pnlSidebarHeader = new Panel();
            lblSubtitle = new Label();
            lblTitle = new Label();
            picLogo = new PictureBox();
            pnlMain = new Panel();
            pnlTopBar = new Panel();
            pnlBreadcrumb = new Panel();
            lblBreadcrumb = new Label();
            lblCurrentView = new Label();
            btnToggleSidebar = new Button();
            pnlSidebar.SuspendLayout();
            pnlNavigation.SuspendLayout();
            pnlSidebarHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).BeginInit();
            pnlMain.SuspendLayout();
            pnlTopBar.SuspendLayout();
            pnlBreadcrumb.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(44, 62, 80);
            pnlSidebar.Controls.Add(pnlNavigation);
            pnlSidebar.Controls.Add(pnlSidebarHeader);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(280, 653);
            pnlSidebar.TabIndex = 0;
            // 
            // pnlNavigation
            // 
            pnlNavigation.Controls.Add(btnLogout);
            pnlNavigation.Controls.Add(btnSocialAccounts);
            pnlNavigation.Controls.Add(btnSettings);
            pnlNavigation.Controls.Add(btnAnalytics);
            pnlNavigation.Controls.Add(btnPosts);
            pnlNavigation.Controls.Add(btnDashboard);
            pnlNavigation.Dock = DockStyle.Fill;
            pnlNavigation.Location = new Point(0, 120);
            pnlNavigation.Name = "pnlNavigation";
            pnlNavigation.Padding = new Padding(20);
            pnlNavigation.Size = new Size(280, 533);
            pnlNavigation.TabIndex = 1;
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(231, 76, 60);
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatAppearance.MouseDownBackColor = Color.FromArgb(192, 57, 43);
            btnLogout.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 11F);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(20, 463);
            btnLogout.Name = "btnLogout";
            btnLogout.Padding = new Padding(50, 0, 0, 0);
            btnLogout.Size = new Size(240, 50);
            btnLogout.TabIndex = 5;
            btnLogout.Text = "Logout";
            btnLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            btnLogout.Paint += btnLogout_Paint;
            // 
            // btnSocialAccounts
            // 
            btnSocialAccounts.BackColor = Color.Transparent;
            btnSocialAccounts.FlatAppearance.BorderSize = 0;
            btnSocialAccounts.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 73, 94);
            btnSocialAccounts.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 73, 94);
            btnSocialAccounts.FlatStyle = FlatStyle.Flat;
            btnSocialAccounts.Font = new Font("Segoe UI", 11F);
            btnSocialAccounts.ForeColor = Color.FromArgb(189, 195, 199);
            btnSocialAccounts.Location = new Point(20, 290);
            btnSocialAccounts.Margin = new Padding(0, 5, 0, 5);
            btnSocialAccounts.Name = "btnSocialAccounts";
            btnSocialAccounts.Padding = new Padding(55, 0, 0, 0);
            btnSocialAccounts.Size = new Size(240, 55);
            btnSocialAccounts.TabIndex = 4;
            btnSocialAccounts.Text = "Social Accounts";
            btnSocialAccounts.TextAlign = ContentAlignment.MiddleLeft;
            btnSocialAccounts.UseVisualStyleBackColor = false;
            btnSocialAccounts.Click += btnSocialAccounts_Click;
            btnSocialAccounts.Paint += btnSocialAccounts_Paint;
            // 
            // btnSettings
            // 
            btnSettings.BackColor = Color.Transparent;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 73, 94);
            btnSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 73, 94);
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.Font = new Font("Segoe UI", 11F);
            btnSettings.ForeColor = Color.FromArgb(189, 195, 199);
            btnSettings.Location = new Point(20, 225);
            btnSettings.Margin = new Padding(0, 5, 0, 5);
            btnSettings.Name = "btnSettings";
            btnSettings.Padding = new Padding(55, 0, 0, 0);
            btnSettings.Size = new Size(240, 55);
            btnSettings.TabIndex = 3;
            btnSettings.Text = "Settings";
            btnSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            btnSettings.Paint += btnSettings_Paint;
            // 
            // btnAnalytics
            // 
            btnAnalytics.BackColor = Color.Transparent;
            btnAnalytics.FlatAppearance.BorderSize = 0;
            btnAnalytics.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 73, 94);
            btnAnalytics.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 73, 94);
            btnAnalytics.FlatStyle = FlatStyle.Flat;
            btnAnalytics.Font = new Font("Segoe UI", 11F);
            btnAnalytics.ForeColor = Color.FromArgb(189, 195, 199);
            btnAnalytics.Location = new Point(20, 160);
            btnAnalytics.Margin = new Padding(0, 5, 0, 5);
            btnAnalytics.Name = "btnAnalytics";
            btnAnalytics.Padding = new Padding(55, 0, 0, 0);
            btnAnalytics.Size = new Size(240, 55);
            btnAnalytics.TabIndex = 2;
            btnAnalytics.Text = "Analytics";
            btnAnalytics.TextAlign = ContentAlignment.MiddleLeft;
            btnAnalytics.UseVisualStyleBackColor = false;
            btnAnalytics.Click += btnAnalytics_Click;
            btnAnalytics.Paint += btnAnalytics_Paint;
            // 
            // btnPosts
            // 
            btnPosts.BackColor = Color.Transparent;
            btnPosts.FlatAppearance.BorderSize = 0;
            btnPosts.FlatAppearance.MouseDownBackColor = Color.FromArgb(52, 73, 94);
            btnPosts.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 73, 94);
            btnPosts.FlatStyle = FlatStyle.Flat;
            btnPosts.Font = new Font("Segoe UI", 11F);
            btnPosts.ForeColor = Color.FromArgb(189, 195, 199);
            btnPosts.Location = new Point(20, 95);
            btnPosts.Margin = new Padding(0, 5, 0, 5);
            btnPosts.Name = "btnPosts";
            btnPosts.Padding = new Padding(55, 0, 0, 0);
            btnPosts.Size = new Size(240, 55);
            btnPosts.TabIndex = 1;
            btnPosts.Text = "Posts";
            btnPosts.TextAlign = ContentAlignment.MiddleLeft;
            btnPosts.UseVisualStyleBackColor = false;
            btnPosts.Click += btnPosts_Click;
            btnPosts.Paint += btnPosts_Paint;
            // 
            // btnDashboard
            // 
            btnDashboard.BackColor = Color.FromArgb(52, 152, 219);
            btnDashboard.FlatAppearance.BorderSize = 0;
            btnDashboard.FlatAppearance.MouseDownBackColor = Color.FromArgb(41, 128, 185);
            btnDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(41, 128, 185);
            btnDashboard.FlatStyle = FlatStyle.Flat;
            btnDashboard.Font = new Font("Segoe UI", 11F);
            btnDashboard.ForeColor = Color.White;
            btnDashboard.Location = new Point(20, 30);
            btnDashboard.Margin = new Padding(0, 5, 0, 5);
            btnDashboard.Name = "btnDashboard";
            btnDashboard.Padding = new Padding(55, 0, 0, 0);
            btnDashboard.Size = new Size(240, 55);
            btnDashboard.TabIndex = 0;
            btnDashboard.Text = "Dashboard";
            btnDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnDashboard.UseVisualStyleBackColor = false;
            btnDashboard.Click += btnDashboard_Click;
            btnDashboard.Paint += btnDashboard_Paint;
            // 
            // pnlSidebarHeader
            // 
            pnlSidebarHeader.BackColor = Color.FromArgb(52, 73, 94);
            pnlSidebarHeader.Controls.Add(lblSubtitle);
            pnlSidebarHeader.Controls.Add(lblTitle);
            pnlSidebarHeader.Controls.Add(picLogo);
            pnlSidebarHeader.Dock = DockStyle.Top;
            pnlSidebarHeader.Location = new Point(0, 0);
            pnlSidebarHeader.Name = "pnlSidebarHeader";
            pnlSidebarHeader.Padding = new Padding(20);
            pnlSidebarHeader.Size = new Size(280, 120);
            pnlSidebarHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(189, 195, 199);
            lblSubtitle.Location = new Point(80, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(106, 23);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Admin Panel";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(80, 25);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 32);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Social Media";
            lblTitle.Click += lblTitle_Click;
            // 
            // picLogo
            // 
            picLogo.Location = new Point(20, 20);
            picLogo.Name = "picLogo";
            picLogo.Size = new Size(50, 50);
            picLogo.TabIndex = 0;
            picLogo.TabStop = false;
            picLogo.Paint += picLogo_Paint;
            // 
            // pnlMain
            // 
            pnlMain.BackColor = Color.FromArgb(247, 249, 252);
            pnlMain.Controls.Add(pnlTopBar);
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(280, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(902, 653);
            pnlMain.TabIndex = 1;
            // 
            // pnlTopBar
            // 
            pnlTopBar.BackColor = Color.White;
            pnlTopBar.Controls.Add(pnlBreadcrumb);
            pnlTopBar.Controls.Add(lblCurrentView);
            pnlTopBar.Controls.Add(btnToggleSidebar);
            pnlTopBar.Dock = DockStyle.Top;
            pnlTopBar.Location = new Point(0, 0);
            pnlTopBar.Name = "pnlTopBar";
            pnlTopBar.Padding = new Padding(30, 20, 30, 20);
            pnlTopBar.Size = new Size(902, 80);
            pnlTopBar.TabIndex = 0;
            // 
            // pnlBreadcrumb
            // 
            pnlBreadcrumb.Controls.Add(lblBreadcrumb);
            pnlBreadcrumb.Dock = DockStyle.Right;
            pnlBreadcrumb.Location = new Point(676, 20);
            pnlBreadcrumb.Name = "pnlBreadcrumb";
            pnlBreadcrumb.Size = new Size(196, 40);
            pnlBreadcrumb.TabIndex = 2;
            // 
            // lblBreadcrumb
            // 
            lblBreadcrumb.AutoSize = true;
            lblBreadcrumb.Dock = DockStyle.Right;
            lblBreadcrumb.Font = new Font("Segoe UI", 10F);
            lblBreadcrumb.ForeColor = Color.FromArgb(127, 140, 141);
            lblBreadcrumb.Location = new Point(35, 0);
            lblBreadcrumb.Name = "lblBreadcrumb";
            lblBreadcrumb.Padding = new Padding(0, 10, 0, 0);
            lblBreadcrumb.Size = new Size(161, 33);
            lblBreadcrumb.TabIndex = 0;
            lblBreadcrumb.Text = "Home > Dashboard";
            // 
            // lblCurrentView
            // 
            lblCurrentView.AutoSize = true;
            lblCurrentView.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblCurrentView.ForeColor = Color.FromArgb(44, 62, 80);
            lblCurrentView.Location = new Point(80, 20);
            lblCurrentView.Name = "lblCurrentView";
            lblCurrentView.Size = new Size(171, 41);
            lblCurrentView.TabIndex = 1;
            lblCurrentView.Text = "Dashboard";
            // 
            // btnToggleSidebar
            // 
            btnToggleSidebar.BackColor = Color.Transparent;
            btnToggleSidebar.FlatAppearance.BorderSize = 0;
            btnToggleSidebar.FlatAppearance.MouseDownBackColor = Color.FromArgb(236, 240, 241);
            btnToggleSidebar.FlatAppearance.MouseOverBackColor = Color.FromArgb(236, 240, 241);
            btnToggleSidebar.FlatStyle = FlatStyle.Flat;
            btnToggleSidebar.Font = new Font("Segoe UI", 14F);
            btnToggleSidebar.ForeColor = Color.FromArgb(127, 140, 141);
            btnToggleSidebar.Location = new Point(30, 20);
            btnToggleSidebar.Name = "btnToggleSidebar";
            btnToggleSidebar.Size = new Size(40, 40);
            btnToggleSidebar.TabIndex = 0;
            btnToggleSidebar.Text = "☰";
            btnToggleSidebar.UseVisualStyleBackColor = false;
            btnToggleSidebar.Click += btnToggleSidebar_Click;
            // 
            // frmAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(247, 249, 252);
            ClientSize = new Size(1182, 653);
            Controls.Add(pnlMain);
            Controls.Add(pnlSidebar);
            Font = new Font("Segoe UI", 9F);
            MinimumSize = new Size(1200, 700);
            Name = "frmAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Social Media Manager - Admin Dashboard";
            WindowState = FormWindowState.Maximized;
            Load += frmAdmin_Load;
            Resize += frmAdmin_Resize;
            pnlSidebar.ResumeLayout(false);
            pnlNavigation.ResumeLayout(false);
            pnlSidebarHeader.ResumeLayout(false);
            pnlSidebarHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picLogo).EndInit();
            pnlMain.ResumeLayout(false);
            pnlTopBar.ResumeLayout(false);
            pnlTopBar.PerformLayout();
            pnlBreadcrumb.ResumeLayout(false);
            pnlBreadcrumb.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlSidebar;
        private Panel pnlSidebarHeader;
        private PictureBox picLogo;
        private Label lblTitle;
        private Label lblSubtitle;
        private Panel pnlNavigation;
        private Button btnDashboard;
        private Button btnPosts;
        private Button btnAnalytics;
        private Button btnSettings;
        private Button btnSocialAccounts;
        private Button btnLogout;
        private Panel pnlMain;
        private Panel pnlTopBar;
        private Label lblCurrentView;
        private Button btnToggleSidebar;
        private Panel pnlBreadcrumb;
        private Label lblBreadcrumb;
    }}