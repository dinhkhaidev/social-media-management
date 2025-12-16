namespace SocialManager.frm.UserControls
{
  partial class ucAnalytics
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
            flpMetrics = new FlowLayoutPanel();
            pnlTotalReach = new Panel();
            picTotalReach = new PictureBox();
            lblTotalReachValue = new Label();
            lblTotalReach = new Label();
            pnlEngagementRate = new Panel();
            picEngagementRate = new PictureBox();
            lblEngagementRateValue = new Label();
            lblEngagementRate = new Label();
            pnlNewFollowers = new Panel();
            picNewFollowers = new PictureBox();
            lblNewFollowersValue = new Label();
            lblNewFollowers = new Label();
            pnlClicks = new Panel();
            picClicks = new PictureBox();
            lblClicksValue = new Label();
            lblClicks = new Label();
            tlpContent = new TableLayoutPanel();
            pnlChart = new Panel();
            pnlChartActions = new Panel();
            btnApplyFilter = new Button();
            dtpToDate = new DateTimePicker();
            lblToDate = new Label();
            dtpFromDate = new DateTimePicker();
            lblFromDate = new Label();
            lblChartTitle = new Label();
            pnlTopPosts = new Panel();
            pnlTopPostsActions = new Panel();
            btnExportReport = new Button();
            btnRefreshData = new Button();
            dgvTopPosts = new DataGridView();
            lblTopPostsTitle = new Label();
            tlpMain.SuspendLayout();
            flpMetrics.SuspendLayout();
            pnlTotalReach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picTotalReach).BeginInit();
            pnlEngagementRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picEngagementRate).BeginInit();
            pnlNewFollowers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picNewFollowers).BeginInit();
            pnlClicks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picClicks).BeginInit();
            tlpContent.SuspendLayout();
            pnlChart.SuspendLayout();
            pnlChartActions.SuspendLayout();
            pnlTopPosts.SuspendLayout();
            pnlTopPostsActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTopPosts).BeginInit();
            SuspendLayout();
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 1;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpMain.Controls.Add(flpMetrics, 0, 0);
            tlpMain.Controls.Add(tlpContent, 0, 1);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 0);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(30, 20, 30, 30);
            tlpMain.RowCount = 2;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(1120, 720);
            tlpMain.TabIndex = 0;
            // 
            // flpMetrics
            // 
            flpMetrics.Controls.Add(pnlTotalReach);
            flpMetrics.Controls.Add(pnlEngagementRate);
            flpMetrics.Controls.Add(pnlNewFollowers);
            flpMetrics.Controls.Add(pnlClicks);
            flpMetrics.Dock = DockStyle.Fill;
            flpMetrics.Location = new Point(30, 20);
            flpMetrics.Margin = new Padding(0);
            flpMetrics.Name = "flpMetrics";
            flpMetrics.Size = new Size(1060, 150);
            flpMetrics.TabIndex = 0;
            flpMetrics.WrapContents = false;
            // 
            // pnlTotalReach
            // 
            pnlTotalReach.BackColor = Color.White;
            pnlTotalReach.Controls.Add(picTotalReach);
            pnlTotalReach.Controls.Add(lblTotalReachValue);
            pnlTotalReach.Controls.Add(lblTotalReach);
            pnlTotalReach.Location = new Point(0, 5);
            pnlTotalReach.Margin = new Padding(0, 5, 20, 5);
            pnlTotalReach.Name = "pnlTotalReach";
            pnlTotalReach.Size = new Size(250, 130);
            pnlTotalReach.TabIndex = 0;
            pnlTotalReach.Paint += pnlCard_Paint;
            // 
            // picTotalReach
            // 
            picTotalReach.Location = new Point(20, 20);
            picTotalReach.Name = "picTotalReach";
            picTotalReach.Size = new Size(45, 45);
            picTotalReach.TabIndex = 2;
            picTotalReach.TabStop = false;
            picTotalReach.Paint += picTotalReach_Paint;
            // 
            // lblTotalReachValue
            // 
            lblTotalReachValue.AutoSize = true;
            lblTotalReachValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalReachValue.ForeColor = Color.FromArgb(44, 62, 80);
            lblTotalReachValue.Location = new Point(20, 75);
            lblTotalReachValue.Name = "lblTotalReachValue";
            lblTotalReachValue.Size = new Size(152, 54);
            lblTotalReachValue.TabIndex = 1;
            lblTotalReachValue.Text = "125.4K";
            // 
            // lblTotalReach
            // 
            lblTotalReach.AutoSize = true;
            lblTotalReach.Font = new Font("Segoe UI", 10F);
            lblTotalReach.ForeColor = Color.FromArgb(127, 140, 141);
            lblTotalReach.Location = new Point(75, 30);
            lblTotalReach.Name = "lblTotalReach";
            lblTotalReach.Size = new Size(160, 23);
            lblTotalReach.TabIndex = 0;
            lblTotalReach.Text = "Tổng Lượt Tiếp Cận";
            // 
            // pnlEngagementRate
            // 
            pnlEngagementRate.BackColor = Color.White;
            pnlEngagementRate.Controls.Add(picEngagementRate);
            pnlEngagementRate.Controls.Add(lblEngagementRateValue);
            pnlEngagementRate.Controls.Add(lblEngagementRate);
            pnlEngagementRate.Location = new Point(270, 5);
            pnlEngagementRate.Margin = new Padding(0, 5, 20, 5);
            pnlEngagementRate.Name = "pnlEngagementRate";
            pnlEngagementRate.Size = new Size(250, 130);
            pnlEngagementRate.TabIndex = 1;
            pnlEngagementRate.Paint += pnlCard_Paint;
            // 
            // picEngagementRate
            // 
            picEngagementRate.Location = new Point(20, 20);
            picEngagementRate.Name = "picEngagementRate";
            picEngagementRate.Size = new Size(45, 45);
            picEngagementRate.TabIndex = 2;
            picEngagementRate.TabStop = false;
            picEngagementRate.Paint += picEngagementRate_Paint;
            // 
            // lblEngagementRateValue
            // 
            lblEngagementRateValue.AutoSize = true;
            lblEngagementRateValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblEngagementRateValue.ForeColor = Color.FromArgb(44, 62, 80);
            lblEngagementRateValue.Location = new Point(20, 75);
            lblEngagementRateValue.Name = "lblEngagementRateValue";
            lblEngagementRateValue.Size = new Size(115, 54);
            lblEngagementRateValue.TabIndex = 1;
            lblEngagementRateValue.Text = "3.8%";
            // 
            // lblEngagementRate
            // 
            lblEngagementRate.AutoSize = true;
            lblEngagementRate.Font = new Font("Segoe UI", 10F);
            lblEngagementRate.ForeColor = Color.FromArgb(127, 140, 141);
            lblEngagementRate.Location = new Point(75, 30);
            lblEngagementRate.Name = "lblEngagementRate";
            lblEngagementRate.Size = new Size(132, 23);
            lblEngagementRate.TabIndex = 0;
            lblEngagementRate.Text = "Tỷ Lệ Tương Tác";
            // 
            // pnlNewFollowers
            // 
            pnlNewFollowers.BackColor = Color.White;
            pnlNewFollowers.Controls.Add(picNewFollowers);
            pnlNewFollowers.Controls.Add(lblNewFollowersValue);
            pnlNewFollowers.Controls.Add(lblNewFollowers);
            pnlNewFollowers.Location = new Point(540, 5);
            pnlNewFollowers.Margin = new Padding(0, 5, 20, 5);
            pnlNewFollowers.Name = "pnlNewFollowers";
            pnlNewFollowers.Size = new Size(250, 130);
            pnlNewFollowers.TabIndex = 2;
            pnlNewFollowers.Paint += pnlCard_Paint;
            // 
            // picNewFollowers
            // 
            picNewFollowers.Location = new Point(20, 20);
            picNewFollowers.Name = "picNewFollowers";
            picNewFollowers.Size = new Size(45, 45);
            picNewFollowers.TabIndex = 2;
            picNewFollowers.TabStop = false;
            picNewFollowers.Paint += picNewFollowers_Paint;
            // 
            // lblNewFollowersValue
            // 
            lblNewFollowersValue.AutoSize = true;
            lblNewFollowersValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblNewFollowersValue.ForeColor = Color.FromArgb(44, 62, 80);
            lblNewFollowersValue.Location = new Point(20, 75);
            lblNewFollowersValue.Name = "lblNewFollowersValue";
            lblNewFollowersValue.Size = new Size(120, 54);
            lblNewFollowersValue.TabIndex = 1;
            lblNewFollowersValue.Text = "+542";
            // 
            // lblNewFollowers
            // 
            lblNewFollowers.AutoSize = true;
            lblNewFollowers.Font = new Font("Segoe UI", 10F);
            lblNewFollowers.ForeColor = Color.FromArgb(127, 140, 141);
            lblNewFollowers.Location = new Point(75, 30);
            lblNewFollowers.Name = "lblNewFollowers";
            lblNewFollowers.Size = new Size(165, 23);
            lblNewFollowers.TabIndex = 0;
            lblNewFollowers.Text = "Người Theo Dõi Mới";
            // 
            // pnlClicks
            // 
            pnlClicks.BackColor = Color.White;
            pnlClicks.Controls.Add(picClicks);
            pnlClicks.Controls.Add(lblClicksValue);
            pnlClicks.Controls.Add(lblClicks);
            pnlClicks.Location = new Point(810, 5);
            pnlClicks.Margin = new Padding(0, 5, 0, 5);
            pnlClicks.Name = "pnlClicks";
            pnlClicks.Size = new Size(250, 130);
            pnlClicks.TabIndex = 3;
            pnlClicks.Paint += pnlCard_Paint;
            // 
            // picClicks
            // 
            picClicks.Location = new Point(20, 20);
            picClicks.Name = "picClicks";
            picClicks.Size = new Size(45, 45);
            picClicks.TabIndex = 2;
            picClicks.TabStop = false;
            picClicks.Paint += picClicks_Paint;
            // 
            // lblClicksValue
            // 
            lblClicksValue.AutoSize = true;
            lblClicksValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblClicksValue.ForeColor = Color.FromArgb(44, 62, 80);
            lblClicksValue.Location = new Point(20, 75);
            lblClicksValue.Name = "lblClicksValue";
            lblClicksValue.Size = new Size(106, 54);
            lblClicksValue.TabIndex = 1;
            lblClicksValue.Text = "1.2K";
            // 
            // lblClicks
            // 
            lblClicks.AutoSize = true;
            lblClicks.Font = new Font("Segoe UI", 10F);
            lblClicks.ForeColor = Color.FromArgb(127, 140, 141);
            lblClicks.Location = new Point(75, 30);
            lblClicks.Name = "lblClicks";
            lblClicks.Size = new Size(91, 23);
            lblClicks.TabIndex = 0;
            lblClicks.Text = "Lượt Nhấp";
            // 
            // tlpContent
            // 
            tlpContent.ColumnCount = 2;
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpContent.Controls.Add(pnlChart, 0, 0);
            tlpContent.Controls.Add(pnlTopPosts, 1, 0);
            tlpContent.Dock = DockStyle.Fill;
            tlpContent.Location = new Point(30, 190);
            tlpContent.Margin = new Padding(0, 20, 0, 0);
            tlpContent.Name = "tlpContent";
            tlpContent.RowCount = 1;
            tlpContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpContent.Size = new Size(1060, 500);
            tlpContent.TabIndex = 1;
            // 
            // pnlChart
            // 
            pnlChart.BackColor = Color.White;
            pnlChart.Controls.Add(pnlChartActions);
            pnlChart.Controls.Add(lblChartTitle);
            pnlChart.Dock = DockStyle.Fill;
            pnlChart.Location = new Point(0, 0);
            pnlChart.Margin = new Padding(0, 0, 10, 0);
            pnlChart.Name = "pnlChart";
            pnlChart.Padding = new Padding(25);
            pnlChart.Size = new Size(520, 500);
            pnlChart.TabIndex = 0;
            pnlChart.Paint += pnlCard_Paint;
            // 
            // pnlChartActions
            // 
            pnlChartActions.Controls.Add(btnApplyFilter);
            pnlChartActions.Controls.Add(dtpToDate);
            pnlChartActions.Controls.Add(lblToDate);
            pnlChartActions.Controls.Add(dtpFromDate);
            pnlChartActions.Controls.Add(lblFromDate);
            pnlChartActions.Dock = DockStyle.Top;
            pnlChartActions.Location = new Point(25, 57);
            pnlChartActions.Name = "pnlChartActions";
            pnlChartActions.Size = new Size(470, 78);
            pnlChartActions.TabIndex = 1;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.BackColor = Color.FromArgb(52, 152, 219);
            btnApplyFilter.FlatAppearance.BorderSize = 0;
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.Font = new Font("Segoe UI", 9F);
            btnApplyFilter.ForeColor = Color.White;
            btnApplyFilter.Location = new Point(340, 35);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(100, 35);
            btnApplyFilter.TabIndex = 4;
            btnApplyFilter.Text = "Áp Dụng";
            btnApplyFilter.UseVisualStyleBackColor = false;
            btnApplyFilter.Click += btnApplyFilter_Click;
            // 
            // dtpToDate
            // 
            dtpToDate.Font = new Font("Segoe UI", 9F);
            dtpToDate.Format = DateTimePickerFormat.Short;
            dtpToDate.Location = new Point(190, 40);
            dtpToDate.Name = "dtpToDate";
            dtpToDate.Size = new Size(130, 27);
            dtpToDate.TabIndex = 3;
            // 
            // lblToDate
            // 
            lblToDate.AutoSize = true;
            lblToDate.Font = new Font("Segoe UI", 10F);
            lblToDate.ForeColor = Color.FromArgb(127, 140, 141);
            lblToDate.Location = new Point(190, 15);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(45, 23);
            lblToDate.TabIndex = 2;
            lblToDate.Text = "Đến:";
            // 
            // dtpFromDate
            // 
            dtpFromDate.Font = new Font("Segoe UI", 9F);
            dtpFromDate.Format = DateTimePickerFormat.Short;
            dtpFromDate.Location = new Point(0, 40);
            dtpFromDate.Name = "dtpFromDate";
            dtpFromDate.Size = new Size(130, 27);
            dtpFromDate.TabIndex = 1;
            // 
            // lblFromDate
            // 
            lblFromDate.AutoSize = true;
            lblFromDate.Font = new Font("Segoe UI", 10F);
            lblFromDate.ForeColor = Color.FromArgb(127, 140, 141);
            lblFromDate.Location = new Point(0, 15);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(33, 23);
            lblFromDate.TabIndex = 0;
            lblFromDate.Text = "Từ:";
            // 
            // lblChartTitle
            // 
            lblChartTitle.AutoSize = true;
            lblChartTitle.Dock = DockStyle.Top;
            lblChartTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblChartTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblChartTitle.Location = new Point(25, 25);
            lblChartTitle.Name = "lblChartTitle";
            lblChartTitle.Size = new Size(283, 32);
            lblChartTitle.TabIndex = 0;
            lblChartTitle.Text = "📊 Hiệu Suất Nền Tảng";
            // 
            // pnlTopPosts
            // 
            pnlTopPosts.BackColor = Color.White;
            pnlTopPosts.Controls.Add(pnlTopPostsActions);
            pnlTopPosts.Controls.Add(dgvTopPosts);
            pnlTopPosts.Controls.Add(lblTopPostsTitle);
            pnlTopPosts.Dock = DockStyle.Fill;
            pnlTopPosts.Location = new Point(540, 0);
            pnlTopPosts.Margin = new Padding(10, 0, 0, 0);
            pnlTopPosts.Name = "pnlTopPosts";
            pnlTopPosts.Padding = new Padding(25);
            pnlTopPosts.Size = new Size(520, 500);
            pnlTopPosts.TabIndex = 1;
            pnlTopPosts.Paint += pnlCard_Paint;
            // 
            // pnlTopPostsActions
            // 
            pnlTopPostsActions.Controls.Add(btnExportReport);
            pnlTopPostsActions.Controls.Add(btnRefreshData);
            pnlTopPostsActions.Dock = DockStyle.Bottom;
            pnlTopPostsActions.Location = new Point(25, 435);
            pnlTopPostsActions.Name = "pnlTopPostsActions";
            pnlTopPostsActions.Size = new Size(470, 40);
            pnlTopPostsActions.TabIndex = 2;
            // 
            // btnExportReport
            // 
            btnExportReport.BackColor = Color.FromArgb(46, 204, 113);
            btnExportReport.FlatAppearance.BorderSize = 0;
            btnExportReport.FlatStyle = FlatStyle.Flat;
            btnExportReport.Font = new Font("Segoe UI", 9F);
            btnExportReport.ForeColor = Color.White;
            btnExportReport.Location = new Point(120, 0);
            btnExportReport.Name = "btnExportReport";
            btnExportReport.Size = new Size(110, 35);
            btnExportReport.TabIndex = 1;
            btnExportReport.Text = "📥 Xuất BC";
            btnExportReport.UseVisualStyleBackColor = false;
            btnExportReport.Click += btnExportReport_Click;
            // 
            // btnRefreshData
            // 
            btnRefreshData.BackColor = Color.FromArgb(149, 165, 166);
            btnRefreshData.FlatAppearance.BorderSize = 0;
            btnRefreshData.FlatStyle = FlatStyle.Flat;
            btnRefreshData.Font = new Font("Segoe UI", 9F);
            btnRefreshData.ForeColor = Color.White;
            btnRefreshData.Location = new Point(0, 0);
            btnRefreshData.Name = "btnRefreshData";
            btnRefreshData.Size = new Size(110, 35);
            btnRefreshData.TabIndex = 0;
            btnRefreshData.Text = "🔄 Làm Mới";
            btnRefreshData.UseVisualStyleBackColor = false;
            btnRefreshData.Click += btnRefreshData_Click;
            // 
            // dgvTopPosts
            // 
            dgvTopPosts.AllowUserToAddRows = false;
            dgvTopPosts.AllowUserToDeleteRows = false;
            dgvTopPosts.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvTopPosts.BackgroundColor = Color.White;
            dgvTopPosts.BorderStyle = BorderStyle.None;
            dgvTopPosts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvTopPosts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTopPosts.GridColor = Color.FromArgb(234, 236, 238);
            dgvTopPosts.Location = new Point(25, 65);
            dgvTopPosts.MultiSelect = false;
            dgvTopPosts.Name = "dgvTopPosts";
            dgvTopPosts.ReadOnly = true;
            dgvTopPosts.RowHeadersVisible = false;
            dgvTopPosts.RowHeadersWidth = 51;
            dgvTopPosts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTopPosts.Size = new Size(470, 360);
            dgvTopPosts.TabIndex = 1;
            // 
            // lblTopPostsTitle
            // 
            lblTopPostsTitle.AutoSize = true;
            lblTopPostsTitle.Dock = DockStyle.Top;
            lblTopPostsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTopPostsTitle.ForeColor = Color.FromArgb(44, 62, 80);
            lblTopPostsTitle.Location = new Point(25, 25);
            lblTopPostsTitle.Name = "lblTopPostsTitle";
            lblTopPostsTitle.Size = new Size(309, 32);
            lblTopPostsTitle.TabIndex = 0;
            lblTopPostsTitle.Text = "🔥 Bài Viết Hiệu Suất Cao";
            // 
            // ucAnalytics
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpMain);
            Font = new Font("Segoe UI", 9F);
            Name = "ucAnalytics";
            Size = new Size(1120, 720);
            tlpMain.ResumeLayout(false);
            flpMetrics.ResumeLayout(false);
            pnlTotalReach.ResumeLayout(false);
            pnlTotalReach.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picTotalReach).EndInit();
            pnlEngagementRate.ResumeLayout(false);
            pnlEngagementRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picEngagementRate).EndInit();
            pnlNewFollowers.ResumeLayout(false);
            pnlNewFollowers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picNewFollowers).EndInit();
            pnlClicks.ResumeLayout(false);
            pnlClicks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picClicks).EndInit();
            tlpContent.ResumeLayout(false);
            pnlChart.ResumeLayout(false);
            pnlChart.PerformLayout();
            pnlChartActions.ResumeLayout(false);
            pnlChartActions.PerformLayout();
            pnlTopPosts.ResumeLayout(false);
            pnlTopPosts.PerformLayout();
            pnlTopPostsActions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTopPosts).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tlpMain;
    private FlowLayoutPanel flpMetrics;
    private Panel pnlTotalReach;
    private PictureBox picTotalReach;
    private Label lblTotalReachValue;
    private Label lblTotalReach;
    private Panel pnlEngagementRate;
    private PictureBox picEngagementRate;
    private Label lblEngagementRateValue;
    private Label lblEngagementRate;
    private Panel pnlNewFollowers;
    private PictureBox picNewFollowers;
    private Label lblNewFollowersValue;
    private Label lblNewFollowers;
    private Panel pnlClicks;
    private PictureBox picClicks;
    private Label lblClicksValue;
    private Label lblClicks;
    private TableLayoutPanel tlpContent;
    private Panel pnlChart;
    private Panel pnlChartActions;
    private Button btnApplyFilter;
    private DateTimePicker dtpToDate;
    private Label lblToDate;
    private DateTimePicker dtpFromDate;
    private Label lblFromDate;
    private Label lblChartTitle;
    private Panel pnlTopPosts;
    private Panel pnlTopPostsActions;
    private Button btnExportReport;
    private Button btnRefreshData;
    private DataGridView dgvTopPosts;
    private Label lblTopPostsTitle;
  }
}
