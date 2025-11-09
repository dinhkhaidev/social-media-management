namespace SocialManager.frm.UserControls
{
  partial class ucSocialAccounts
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tlpMain = new TableLayoutPanel();
            pnlStatistics = new Panel();
            tlpStats = new TableLayoutPanel();
            pnlTotalUsers = new Panel();
            lblTotalUsers = new Label();
            lblTotalUsersLabel = new Label();
            pnlActiveUsers = new Panel();
            lblActivePercentage = new Label();
            lblActiveUsers = new Label();
            lblActiveUsersLabel = new Label();
            pnlNewToday = new Panel();
            lblNewToday = new Label();
            lblNewTodayLabel = new Label();
            pnlNewThisWeek = new Panel();
            lblNewThisWeek = new Label();
            lblNewWeekLabel = new Label();
            pnlAdminUsers = new Panel();
            lblAdminUsers = new Label();
            lblAdminUsersLabel = new Label();
            lblStatisticsTitle = new Label();
            pnlUsersList = new Panel();
            pnlUserActions = new Panel();
            btnUserDetails = new Button();
            btnExportUsers = new Button();
            btnRefreshData = new Button();
            dgvUsers = new DataGridView();
            pnlUsersHeader = new Panel();
            lblLastRefresh = new Label();
            lblUsersTitle = new Label();
            pnlActivities = new Panel();
            lstActivities = new ListBox();
            lblActivitiesTitle = new Label();
            tlpMain.SuspendLayout();
            pnlStatistics.SuspendLayout();
            tlpStats.SuspendLayout();
            pnlTotalUsers.SuspendLayout();
            pnlActiveUsers.SuspendLayout();
            pnlNewToday.SuspendLayout();
            pnlNewThisWeek.SuspendLayout();
            pnlAdminUsers.SuspendLayout();
            pnlUsersList.SuspendLayout();
            pnlUserActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            pnlUsersHeader.SuspendLayout();
            pnlActivities.SuspendLayout();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 76.81035F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 23.1896553F));
            tlpMain.Controls.Add(pnlStatistics, 0, 0);
            tlpMain.Controls.Add(pnlUsersList, 0, 1);
            tlpMain.Controls.Add(pnlActivities, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(20);
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 180F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1200, 800);
            tlpMain.TabIndex = 0;
            // 
            // pnlStatistics
            // 
            pnlStatistics.BackColor = Color.White;
            pnlStatistics.Controls.Add(tlpStats);
            pnlStatistics.Controls.Add(lblStatisticsTitle);
            pnlStatistics.Dock = DockStyle.Fill;
            pnlStatistics.Location = new Point(20, 20);
            pnlStatistics.Margin = new Padding(0, 0, 10, 10);
            pnlStatistics.Name = "pnlStatistics";
            pnlStatistics.Padding = new Padding(20);
            pnlStatistics.Size = new Size(881, 170);
            pnlStatistics.TabIndex = 0;
            pnlStatistics.Paint += pnlCard_Paint;
            // 
            // tlpStats
            // 
            tlpStats.ColumnCount = 5;
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpStats.Controls.Add(pnlTotalUsers, 0, 0);
            tlpStats.Controls.Add(pnlActiveUsers, 1, 0);
            tlpStats.Controls.Add(pnlNewToday, 2, 0);
            tlpStats.Controls.Add(pnlNewThisWeek, 3, 0);
            tlpStats.Controls.Add(pnlAdminUsers, 4, 0);
            tlpStats.Dock = DockStyle.Fill;
            tlpStats.Location = new Point(20, 52);
            tlpStats.Name = "tlpStats";
            tlpStats.RowCount = 1;
            tlpStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpStats.Size = new Size(841, 98);
            tlpStats.TabIndex = 1;
            // 
            // pnlTotalUsers
            // 
            pnlTotalUsers.BackColor = Color.FromArgb(52, 152, 219);
            pnlTotalUsers.Controls.Add(lblTotalUsers);
            pnlTotalUsers.Controls.Add(lblTotalUsersLabel);
            pnlTotalUsers.Dock = DockStyle.Fill;
            pnlTotalUsers.Location = new Point(3, 3);
            pnlTotalUsers.Name = "pnlTotalUsers";
            pnlTotalUsers.Padding = new Padding(15);
            pnlTotalUsers.Size = new Size(162, 92);
            pnlTotalUsers.TabIndex = 0;
            // 
            // lblTotalUsers
            // 
            lblTotalUsers.AutoSize = true;
            lblTotalUsers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalUsers.ForeColor = Color.White;
            lblTotalUsers.Location = new Point(15, 15);
            lblTotalUsers.Name = "lblTotalUsers";
            lblTotalUsers.Size = new Size(71, 54);
            lblTotalUsers.TabIndex = 0;
            lblTotalUsers.Text = "---";
            // 
            // lblTotalUsersLabel
            // 
            lblTotalUsersLabel.AutoSize = true;
            lblTotalUsersLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalUsersLabel.ForeColor = Color.White;
            lblTotalUsersLabel.Location = new Point(15, 69);
            lblTotalUsersLabel.Name = "lblTotalUsersLabel";
            lblTotalUsersLabel.Size = new Size(151, 23);
            lblTotalUsersLabel.TabIndex = 1;
            lblTotalUsersLabel.Text = "Tổng người dùng";
            // 
            // pnlActiveUsers
            // 
            pnlActiveUsers.BackColor = Color.FromArgb(46, 204, 113);
            pnlActiveUsers.Controls.Add(lblActivePercentage);
            pnlActiveUsers.Controls.Add(lblActiveUsers);
            pnlActiveUsers.Controls.Add(lblActiveUsersLabel);
            pnlActiveUsers.Dock = DockStyle.Fill;
            pnlActiveUsers.Location = new Point(171, 3);
            pnlActiveUsers.Name = "pnlActiveUsers";
            pnlActiveUsers.Padding = new Padding(15);
            pnlActiveUsers.Size = new Size(162, 92);
            pnlActiveUsers.TabIndex = 1;
            // 
            // lblActivePercentage
            // 
            lblActivePercentage.AutoSize = true;
            lblActivePercentage.Font = new Font("Segoe UI", 12F);
            lblActivePercentage.ForeColor = Color.White;
            lblActivePercentage.Location = new Point(100, 25);
            lblActivePercentage.Name = "lblActivePercentage";
            lblActivePercentage.Size = new Size(36, 28);
            lblActivePercentage.TabIndex = 2;
            lblActivePercentage.Text = "---";
            // 
            // lblActiveUsers
            // 
            lblActiveUsers.AutoSize = true;
            lblActiveUsers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblActiveUsers.ForeColor = Color.White;
            lblActiveUsers.Location = new Point(15, 15);
            lblActiveUsers.Name = "lblActiveUsers";
            lblActiveUsers.Size = new Size(71, 54);
            lblActiveUsers.TabIndex = 0;
            lblActiveUsers.Text = "---";
            // 
            // lblActiveUsersLabel
            // 
            lblActiveUsersLabel.AutoSize = true;
            lblActiveUsersLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblActiveUsersLabel.ForeColor = Color.White;
            lblActiveUsersLabel.Location = new Point(18, 69);
            lblActiveUsersLabel.Name = "lblActiveUsersLabel";
            lblActiveUsersLabel.Size = new Size(196, 23);
            lblActiveUsersLabel.TabIndex = 1;
            lblActiveUsersLabel.Text = "Người dùng hoạt động";
            // 
            // pnlNewToday
            // 
            pnlNewToday.BackColor = Color.FromArgb(230, 126, 34);
            pnlNewToday.Controls.Add(lblNewToday);
            pnlNewToday.Controls.Add(lblNewTodayLabel);
            pnlNewToday.Dock = DockStyle.Fill;
            pnlNewToday.Location = new Point(339, 3);
            pnlNewToday.Name = "pnlNewToday";
            pnlNewToday.Padding = new Padding(15);
            pnlNewToday.Size = new Size(162, 92);
            pnlNewToday.TabIndex = 2;
            // 
            // lblNewToday
            // 
            lblNewToday.AutoSize = true;
            lblNewToday.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblNewToday.ForeColor = Color.White;
            lblNewToday.Location = new Point(15, 15);
            lblNewToday.Name = "lblNewToday";
            lblNewToday.Size = new Size(71, 54);
            lblNewToday.TabIndex = 0;
            lblNewToday.Text = "---";
            // 
            // lblNewTodayLabel
            // 
            lblNewTodayLabel.AutoSize = true;
            lblNewTodayLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNewTodayLabel.ForeColor = Color.White;
            lblNewTodayLabel.Location = new Point(18, 69);
            lblNewTodayLabel.Name = "lblNewTodayLabel";
            lblNewTodayLabel.Size = new Size(116, 23);
            lblNewTodayLabel.TabIndex = 1;
            lblNewTodayLabel.Text = "Mới hôm nay";
            // 
            // pnlNewThisWeek
            // 
            pnlNewThisWeek.BackColor = Color.FromArgb(155, 89, 182);
            pnlNewThisWeek.Controls.Add(lblNewThisWeek);
            pnlNewThisWeek.Controls.Add(lblNewWeekLabel);
            pnlNewThisWeek.Dock = DockStyle.Fill;
            pnlNewThisWeek.Location = new Point(507, 3);
            pnlNewThisWeek.Name = "pnlNewThisWeek";
            pnlNewThisWeek.Padding = new Padding(15);
            pnlNewThisWeek.Size = new Size(162, 92);
            pnlNewThisWeek.TabIndex = 3;
            // 
            // lblNewThisWeek
            // 
            lblNewThisWeek.AutoSize = true;
            lblNewThisWeek.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblNewThisWeek.ForeColor = Color.White;
            lblNewThisWeek.Location = new Point(15, 15);
            lblNewThisWeek.Name = "lblNewThisWeek";
            lblNewThisWeek.Size = new Size(71, 54);
            lblNewThisWeek.TabIndex = 0;
            lblNewThisWeek.Text = "---";
            // 
            // lblNewWeekLabel
            // 
            lblNewWeekLabel.AutoSize = true;
            lblNewWeekLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNewWeekLabel.ForeColor = Color.White;
            lblNewWeekLabel.Location = new Point(15, 69);
            lblNewWeekLabel.Name = "lblNewWeekLabel";
            lblNewWeekLabel.Size = new Size(81, 23);
            lblNewWeekLabel.TabIndex = 1;
            lblNewWeekLabel.Text = "Tuần này";
            // 
            // pnlAdminUsers
            // 
            pnlAdminUsers.BackColor = Color.FromArgb(231, 76, 60);
            pnlAdminUsers.Controls.Add(lblAdminUsers);
            pnlAdminUsers.Controls.Add(lblAdminUsersLabel);
            pnlAdminUsers.Dock = DockStyle.Fill;
            pnlAdminUsers.Location = new Point(675, 3);
            pnlAdminUsers.Name = "pnlAdminUsers";
            pnlAdminUsers.Padding = new Padding(15);
            pnlAdminUsers.Size = new Size(163, 92);
            pnlAdminUsers.TabIndex = 4;
            // 
            // lblAdminUsers
            // 
            lblAdminUsers.AutoSize = true;
            lblAdminUsers.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblAdminUsers.ForeColor = Color.White;
            lblAdminUsers.Location = new Point(15, 15);
            lblAdminUsers.Name = "lblAdminUsers";
            lblAdminUsers.Size = new Size(71, 54);
            lblAdminUsers.TabIndex = 0;
            lblAdminUsers.Text = "---";
            // 
            // lblAdminUsersLabel
            // 
            lblAdminUsersLabel.AutoSize = true;
            lblAdminUsersLabel.Font = new Font("Segoe UI", 10F);
            lblAdminUsersLabel.ForeColor = Color.White;
            lblAdminUsersLabel.Location = new Point(18, 69);
            lblAdminUsersLabel.Name = "lblAdminUsersLabel";
            lblAdminUsersLabel.Size = new Size(109, 23);
            lblAdminUsersLabel.TabIndex = 1;
            lblAdminUsersLabel.Text = "Quản trị viên";
            // 
            // lblStatisticsTitle
            // 
            lblStatisticsTitle.AutoSize = true;
            lblStatisticsTitle.Dock = DockStyle.Top;
            lblStatisticsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblStatisticsTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblStatisticsTitle.Location = new Point(20, 20);
            lblStatisticsTitle.Name = "lblStatisticsTitle";
            lblStatisticsTitle.Size = new Size(262, 32);
            lblStatisticsTitle.TabIndex = 0;
            lblStatisticsTitle.Text = "Thống kê người dùng";
            // 
            // pnlUsersList
            // 
            pnlUsersList.BackColor = Color.White;
            pnlUsersList.Controls.Add(pnlUserActions);
            pnlUsersList.Controls.Add(dgvUsers);
            pnlUsersList.Controls.Add(pnlUsersHeader);
            pnlUsersList.Dock = DockStyle.Fill;
            pnlUsersList.Location = new Point(20, 210);
            pnlUsersList.Margin = new Padding(0, 10, 10, 0);
            pnlUsersList.Name = "pnlUsersList";
            pnlUsersList.Padding = new Padding(20);
            pnlUsersList.Size = new Size(881, 570);
            pnlUsersList.TabIndex = 1;
            pnlUsersList.Paint += pnlCard_Paint;
            // 
            // pnlUserActions
            // 
            pnlUserActions.Controls.Add(btnUserDetails);
            pnlUserActions.Controls.Add(btnExportUsers);
            pnlUserActions.Controls.Add(btnRefreshData);
            pnlUserActions.Dock = DockStyle.Bottom;
            pnlUserActions.Location = new Point(20, 510);
            pnlUserActions.Name = "pnlUserActions";
            pnlUserActions.Size = new Size(841, 40);
            pnlUserActions.TabIndex = 2;
            // 
            // btnUserDetails
            // 
            btnUserDetails.BackColor = Color.FromArgb(52, 152, 219);
            btnUserDetails.FlatAppearance.BorderSize = 0;
            btnUserDetails.FlatStyle = FlatStyle.Flat;
            btnUserDetails.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnUserDetails.ForeColor = Color.White;
            btnUserDetails.Location = new Point(0, 0);
            btnUserDetails.Name = "btnUserDetails";
            btnUserDetails.Size = new Size(120, 35);
            btnUserDetails.TabIndex = 0;
            btnUserDetails.Text = "?? View Details";
            btnUserDetails.UseVisualStyleBackColor = false;
            btnUserDetails.Click += btnUserDetails_Click;
            // 
            // btnExportUsers
            // 
            btnExportUsers.BackColor = Color.FromArgb(46, 204, 113);
            btnExportUsers.FlatAppearance.BorderSize = 0;
            btnExportUsers.FlatStyle = FlatStyle.Flat;
            btnExportUsers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnExportUsers.ForeColor = Color.White;
            btnExportUsers.Location = new Point(130, 0);
            btnExportUsers.Name = "btnExportUsers";
            btnExportUsers.Size = new Size(120, 35);
            btnExportUsers.TabIndex = 1;
            btnExportUsers.Text = "?? Export CSV";
            btnExportUsers.UseVisualStyleBackColor = false;
            btnExportUsers.Click += btnExportUsers_Click;
            // 
            // btnRefreshData
            // 
            btnRefreshData.BackColor = Color.FromArgb(155, 89, 182);
            btnRefreshData.FlatAppearance.BorderSize = 0;
            btnRefreshData.FlatStyle = FlatStyle.Flat;
            btnRefreshData.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnRefreshData.ForeColor = Color.White;
            btnRefreshData.Location = new Point(260, 0);
            btnRefreshData.Name = "btnRefreshData";
            btnRefreshData.Size = new Size(120, 35);
            btnRefreshData.TabIndex = 2;
            btnRefreshData.Text = "?? Refresh Data";
            btnRefreshData.UseVisualStyleBackColor = false;
            btnRefreshData.Click += btnRefreshData_Click;
            // 
            // dgvUsers
            // 
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.BackgroundColor = Color.White;
            dgvUsers.BorderStyle = BorderStyle.None;
            dgvUsers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Dock = DockStyle.Fill;
            dgvUsers.GridColor = Color.FromArgb(234, 236, 238);
            dgvUsers.Location = new Point(20, 60);
            dgvUsers.MultiSelect = false;
            dgvUsers.Name = "dgvUsers";
            dgvUsers.ReadOnly = true;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.RowHeadersWidth = 51;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.Size = new Size(841, 490);
            dgvUsers.TabIndex = 1;
            dgvUsers.CellContentClick += dgvUsers_CellContentClick;
            // 
            // pnlUsersHeader
            // 
            pnlUsersHeader.Controls.Add(lblLastRefresh);
            pnlUsersHeader.Controls.Add(lblUsersTitle);
            pnlUsersHeader.Dock = DockStyle.Top;
            pnlUsersHeader.Location = new Point(20, 20);
            pnlUsersHeader.Name = "pnlUsersHeader";
            pnlUsersHeader.Size = new Size(841, 40);
            pnlUsersHeader.TabIndex = 0;
            // 
            // lblLastRefresh
            // 
            lblLastRefresh.AutoSize = true;
            lblLastRefresh.Font = new Font("Segoe UI", 9F);
            lblLastRefresh.ForeColor = Color.FromArgb(127, 140, 141);
            lblLastRefresh.Location = new Point(650, 10);
            lblLastRefresh.Name = "lblLastRefresh";
            lblLastRefresh.Size = new Size(168, 20);
            lblLastRefresh.TabIndex = 1;
            lblLastRefresh.Text = "C?p nh?t l?n cu?i: --:--:--";
            // 
            // lblUsersTitle
            // 
            lblUsersTitle.AutoSize = true;
            lblUsersTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblUsersTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblUsersTitle.Location = new Point(0, 1);
            lblUsersTitle.Name = "lblUsersTitle";
            lblUsersTitle.Size = new Size(214, 32);
            lblUsersTitle.TabIndex = 0;
            lblUsersTitle.Text = "?? Users Manager";
            // 
            // pnlActivities
            // 
            pnlActivities.BackColor = Color.White;
            pnlActivities.Controls.Add(lstActivities);
            pnlActivities.Controls.Add(lblActivitiesTitle);
            pnlActivities.Dock = DockStyle.Fill;
            pnlActivities.Location = new Point(921, 20);
            pnlActivities.Margin = new Padding(10, 0, 0, 0);
            pnlActivities.Name = "pnlActivities";
            pnlActivities.Padding = new Padding(20);
            tlpMain.SetRowSpan(pnlActivities, 2);
            pnlActivities.Size = new Size(259, 760);
            pnlActivities.TabIndex = 2;
            pnlActivities.Paint += pnlCard_Paint;
            // 
            // lstActivities
            // 
            lstActivities.BorderStyle = BorderStyle.None;
            lstActivities.Dock = DockStyle.Fill;
            lstActivities.Font = new Font("Segoe UI", 9F);
            lstActivities.Location = new Point(20, 60);
            lstActivities.Name = "lstActivities";
            lstActivities.Size = new Size(219, 680);
            lstActivities.TabIndex = 1;
            // 
            // lblActivitiesTitle
            // 
            lblActivitiesTitle.AutoSize = true;
            lblActivitiesTitle.Dock = DockStyle.Top;
            lblActivitiesTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblActivitiesTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblActivitiesTitle.Location = new Point(20, 20);
            lblActivitiesTitle.Name = "lblActivitiesTitle";
            lblActivitiesTitle.Padding = new Padding(0, 0, 0, 8);
            lblActivitiesTitle.Size = new Size(148, 40);
            lblActivitiesTitle.TabIndex = 0;
            lblActivitiesTitle.Text = "?? Activities";
            // 
            // ucSocialAccounts
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "ucSocialAccounts";
            Size = new Size(1200, 800);
            tlpMain.ResumeLayout(false);
            pnlStatistics.ResumeLayout(false);
            pnlStatistics.PerformLayout();
            tlpStats.ResumeLayout(false);
            pnlTotalUsers.ResumeLayout(false);
            pnlTotalUsers.PerformLayout();
            pnlActiveUsers.ResumeLayout(false);
            pnlActiveUsers.PerformLayout();
            pnlNewToday.ResumeLayout(false);
            pnlNewToday.PerformLayout();
            pnlNewThisWeek.ResumeLayout(false);
            pnlNewThisWeek.PerformLayout();
            pnlAdminUsers.ResumeLayout(false);
            pnlAdminUsers.PerformLayout();
            pnlUsersList.ResumeLayout(false);
            pnlUserActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            pnlUsersHeader.ResumeLayout(false);
            pnlUsersHeader.PerformLayout();
            pnlActivities.ResumeLayout(false);
            pnlActivities.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
    private Panel pnlStatistics;
    private TableLayoutPanel tlpStats;
    private Panel pnlTotalUsers;
    private Label lblTotalUsers;
    private Label lblTotalUsersLabel;
    private Panel pnlActiveUsers;
    private Label lblActivePercentage;
    private Label lblActiveUsers;
    private Label lblActiveUsersLabel;
    private Panel pnlNewToday;
    private Label lblNewToday;
    private Label lblNewTodayLabel;
    private Panel pnlNewThisWeek;
    private Label lblNewThisWeek;
    private Label lblNewWeekLabel;
    private Panel pnlAdminUsers;
    private Label lblAdminUsers;
    private Label lblAdminUsersLabel;
    private Label lblStatisticsTitle;
    private Panel pnlUsersList;
    private Panel pnlUserActions;
    private Button btnUserDetails;
    private Button btnExportUsers;
    private Button btnRefreshData;
    private DataGridView dgvUsers;
    private Panel pnlUsersHeader;
    private Label lblLastRefresh;
    private Label lblUsersTitle;
    private Panel pnlActivities;
    private ListBox lstActivities;
    private Label lblActivitiesTitle;
  }
}
