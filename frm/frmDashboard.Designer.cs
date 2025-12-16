namespace SocialManager.frm
{
  partial class frmDashboard : Form
  {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null)) components.Dispose();
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();

      // Top bar - CHỈ GIỮ LẠI HEADER VÀ CÁC NÚT
      this.panelHeader = new System.Windows.Forms.Panel();
      this.panelHeaderRight = new System.Windows.Forms.Panel();
      this.txtSearch = new SocialManager.controls.RoundedTextBox(); // Sẽ được thêm vào filter
      this.btnNewPost = new System.Windows.Forms.Button();
      this.btnNewsfeed = new System.Windows.Forms.Button();
      this.btnLogout = new System.Windows.Forms.Button();
      this.panelHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
      this.btnThemeToggle = new System.Windows.Forms.Button();
      this.btnSettings = new System.Windows.Forms.Button();

      // Profile header (no cover - direct profile info)
      this.panelProfileHeader = new System.Windows.Forms.Panel();
      this.panelProfileInfo = new System.Windows.Forms.Panel();
      this.picProfileLarge = new System.Windows.Forms.PictureBox();
      this.lblProfileName = new System.Windows.Forms.Label();
      this.lblFollowerStats = new System.Windows.Forms.Label(); // Hiển thị @username
      this.lblAccountStatus = new System.Windows.Forms.Label(); // Trạng thái tài khoản
      this.btnEditProfile = new System.Windows.Forms.Button();

      // Tabs row
      this.panelTabs = new System.Windows.Forms.Panel();
      this.btnTabBaiViet = new System.Windows.Forms.Button();

      this.menuAvatar = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.miDangXuat = new System.Windows.Forms.ToolStripMenuItem();

      // Content area
      this.panelContent = new System.Windows.Forms.Panel();
      this.tableLayout = new System.Windows.Forms.TableLayoutPanel();
      this.panelLeftCol = new System.Windows.Forms.Panel();
      this.panelUserInfo = new System.Windows.Forms.Panel(); // Thông tin cá nhân
      this.gbUserInfo = new System.Windows.Forms.GroupBox();
      this.lblDOBTitle = new System.Windows.Forms.Label();
      this.lblDOBValue = new System.Windows.Forms.Label();
      this.lblEmailTitle = new System.Windows.Forms.Label();
      this.lblEmailValue = new System.Windows.Forms.Label();
      this.lblPhoneTitle = new System.Windows.Forms.Label();
      this.lblPhoneValue = new System.Windows.Forms.Label();
      this.lblCityTitle = new System.Windows.Forms.Label();
      this.lblCityValue = new System.Windows.Forms.Label();
      this.panelCenterCol = new System.Windows.Forms.Panel(); // Cột giữa - Posts
      this.panelRightCol = new System.Windows.Forms.Panel(); // Cột phải - Filter
      this.panelFilter = new System.Windows.Forms.Panel();
      this.gbFilter = new System.Windows.Forms.GroupBox();
      this.lblSearch = new System.Windows.Forms.Label(); // Label cho ô tìm kiếm
      this.lblType = new System.Windows.Forms.Label();
      this.cboType = new System.Windows.Forms.ComboBox();
      this.lblFromDate = new System.Windows.Forms.Label();
      this.dtpFrom = new System.Windows.Forms.DateTimePicker();
      this.lblToDate = new System.Windows.Forms.Label();
      this.dtpTo = new System.Windows.Forms.DateTimePicker();
      this.lblPostId = new System.Windows.Forms.Label();
      this.txtPostId = new SocialManager.controls.RoundedTextBox();
      this.btnClearFilter = new SocialManager.controls.RoundedButton();
      this.btnApplyFilter = new SocialManager.controls.RoundedButton();
      this.panelCenterCol = new System.Windows.Forms.Panel();
      this.panelComposer = new System.Windows.Forms.Panel();
      this.picComposerAvatar = new System.Windows.Forms.PictureBox();
      this.lblComposerPlaceholder = new System.Windows.Forms.Label();
      this.flowLayoutPanelPosts = new SocialManager.frm.DoubleBufferedFlowLayoutPanel();

      // panelHeader - CHỈ CÒN NÚT BÊN PHẢI
      this.panelHeader.BackColor = System.Drawing.Color.White;
      this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelHeader.Height = 60;
      this.panelHeader.Padding = new System.Windows.Forms.Padding(0);

      // header sub panels - XÓA panelHeaderLeft và panelHeaderCenter
      this.panelHeaderRight.Dock = System.Windows.Forms.DockStyle.Fill; // Chiếm toàn bộ
      this.panelHeaderRight.Width = 360;
      this.panelHeaderRight.Padding = new System.Windows.Forms.Padding(8, 10, 12, 10);

      // btnNewPost - NÚT HÌNH TRÒN Ở GÓC DƯỚI PHẢI (đối diện btnThemeToggle)
      this.btnNewPost.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
      this.btnNewPost.BackColor = System.Drawing.Color.FromArgb(10, 102, 194);
      this.btnNewPost.FlatAppearance.BorderSize = 0;
      this.btnNewPost.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnNewPost.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
      this.btnNewPost.ForeColor = System.Drawing.Color.White;
      this.btnNewPost.Location = new System.Drawing.Point(20, 20);
      this.btnNewPost.Size = new System.Drawing.Size(48, 48);
      this.btnNewPost.Text = "+";
      this.btnNewPost.Name = "btnNewPost";
      this.btnNewPost.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnNewPost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.btnNewPost.Click += new System.EventHandler(this.btnNewPost_Click);

      // btnLogout
      this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.Right;
      this.btnLogout.BackColor = System.Drawing.Color.FromArgb(228, 230, 235);
      this.btnLogout.FlatAppearance.BorderSize = 0;
      this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 9F);
      this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
      this.btnLogout.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
      this.btnLogout.Location = new System.Drawing.Point(0, 8);
      this.btnLogout.Size = new System.Drawing.Size(90, 36);
      this.btnLogout.Text = "Đăng xuất";
      this.btnLogout.Name = "btnLogout";
      this.btnLogout.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);

      // header actions container
      this.panelHeaderActions.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelHeaderActions.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.panelHeaderActions.WrapContents = false;
      this.panelHeaderActions.Padding = new System.Windows.Forms.Padding(0);
      this.panelHeaderActions.BackColor = System.Drawing.Color.Transparent;

      // btnThemeToggle - NÚT HÌNH TRÒN Ở GÓC DƯỚI TRÁI
      this.btnThemeToggle.Text = "◐";
      this.btnThemeToggle.BackColor = System.Drawing.Color.FromArgb(228, 230, 235);
      this.btnThemeToggle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnThemeToggle.FlatAppearance.BorderSize = 0;
      this.btnThemeToggle.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
      this.btnThemeToggle.Size = new System.Drawing.Size(56, 56); // Hình tròn
      this.btnThemeToggle.Location = new System.Drawing.Point(20, 20); // Sẽ được điều chỉnh trong Load event
      this.btnThemeToggle.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
      this.btnThemeToggle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
      this.btnThemeToggle.Name = "btnThemeToggle";
      this.btnThemeToggle.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnThemeToggle.Click += new System.EventHandler(this.btnThemeToggle_Click);

      this.btnSettings.Text = "Chỉnh sửa thông tin cá nhân";
      this.btnSettings.AutoSize = true;
      this.btnSettings.BackColor = System.Drawing.Color.FromArgb(228, 230, 235);
      this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnSettings.FlatAppearance.BorderSize = 0;
      this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
      this.btnSettings.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
      this.btnSettings.Size = new System.Drawing.Size(200, 36);
      this.btnSettings.Name = "btnSettings";
      this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

      this.btnNewsfeed = new System.Windows.Forms.Button();
      this.btnNewsfeed.Text = "Newsfeed";
      this.btnNewsfeed.AutoSize = true;
      this.btnNewsfeed.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
      this.btnNewsfeed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnNewsfeed.FlatAppearance.BorderSize = 0;
      this.btnNewsfeed.ForeColor = System.Drawing.Color.White;
      this.btnNewsfeed.Margin = new System.Windows.Forms.Padding(8, 0, 0, 0);
      this.btnNewsfeed.Size = new System.Drawing.Size(120, 36);
      this.btnNewsfeed.Name = "btnNewsfeed";
      this.btnNewsfeed.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnNewsfeed.Click += new System.EventHandler(this.btnNewsfeed_Click);

      // add buttons into actions container (right-to-left) - KHÔNG CÓ btnThemeToggle, btnNewPost
      this.panelHeaderActions.Controls.Add(this.btnLogout);
      this.panelHeaderActions.Controls.Add(this.btnSettings); // Settings gần Logout
      this.panelHeaderActions.Controls.Add(this.btnNewsfeed); // Newsfeed button

      // assemble header - CHỈ CÒN panelHeaderRight
      this.panelHeaderRight.Controls.Add(this.panelHeaderActions);
      this.panelHeader.Controls.Add(this.panelHeaderRight);

      // panelProfileHeader - ĐẨY LÊN CAO HƠN
      this.panelProfileHeader.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelProfileHeader.Height = 150; // Giảm từ 180
      this.panelProfileHeader.BackColor = System.Drawing.Color.White;

      // profile info (no cover) - GIẢM PADDING
      this.panelProfileInfo.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelProfileInfo.Height = 106; // Giảm từ 136
      this.panelProfileInfo.BackColor = System.Drawing.Color.White;
      this.panelProfileInfo.Padding = new System.Windows.Forms.Padding(24, 8, 24, 8); // Giảm padding top từ 20 → 8

      this.picProfileLarge.BackColor = System.Drawing.Color.LightGray;
      this.picProfileLarge.Size = new System.Drawing.Size(90, 90); // Giảm từ 96x96
      this.picProfileLarge.Location = new System.Drawing.Point(24, 8); // Đẩy lên
      this.picProfileLarge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.picProfileLarge.Name = "picProfileLarge";

      this.lblProfileName.AutoSize = true;
      this.lblProfileName.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold); // Tên đầy đủ
      this.lblProfileName.Location = new System.Drawing.Point(130, 15);
      this.lblProfileName.Text = "Tên đầy đủ"; // Sẽ được cập nhật từ FullName

      this.lblFollowerStats.AutoSize = true;
      this.lblFollowerStats.Font = new System.Drawing.Font("Segoe UI", 9F); // Username
      this.lblFollowerStats.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
      this.lblFollowerStats.Location = new System.Drawing.Point(130, 45);
      this.lblFollowerStats.Text = "@username";

      this.lblAccountStatus.AutoSize = true;
      this.lblAccountStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.lblAccountStatus.Location = new System.Drawing.Point(130, 70);
      this.lblAccountStatus.Text = "Trạng thái: Bình thường";
      this.lblAccountStatus.ForeColor = System.Drawing.Color.FromArgb(40, 167, 69); // Green

      this.btnEditProfile.Text = "Chỉnh sửa hồ sơ";
      this.btnEditProfile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.btnEditProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnEditProfile.FlatAppearance.BorderSize = 0;
      this.btnEditProfile.BackColor = System.Drawing.Color.FromArgb(228, 230, 235);
      this.btnEditProfile.ForeColor = System.Drawing.Color.FromArgb(33, 33, 33);
      this.btnEditProfile.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
      this.btnEditProfile.Location = new System.Drawing.Point(1100, 32);
      this.btnEditProfile.Size = new System.Drawing.Size(140, 36);
      this.btnEditProfile.Cursor = System.Windows.Forms.Cursors.Hand;
      this.btnEditProfile.Click += new System.EventHandler(this.btnSettings_Click);

      this.panelProfileInfo.Controls.Add(this.picProfileLarge);
      this.panelProfileInfo.Controls.Add(this.lblProfileName);
      this.panelProfileInfo.Controls.Add(this.lblFollowerStats); // Hiển thị @username
      this.panelProfileInfo.Controls.Add(this.lblAccountStatus); // Trạng thái tài khoản
      this.panelProfileInfo.Controls.Add(this.btnEditProfile);

      // tabs - CHỈ GIỮ LẠI TAB "BÀI VIẾT" vì các tab khác không có chức năng
      this.panelTabs.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelTabs.Height = 44;
      this.panelTabs.BackColor = System.Drawing.Color.White;
      this.panelTabs.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
      this.btnTabBaiViet = CreateTabButton("Bài viết", 20, 6, 90, 32, true);

      this.panelTabs.Controls.Add(this.btnTabBaiViet);

      // Tab click handler
      this.btnTabBaiViet.Click += new System.EventHandler(this.TabButton_Click);

      // underline under active tab
      this.panelTabUnderline = new System.Windows.Forms.Panel();
      this.panelTabUnderline.BackColor = System.Drawing.Color.FromArgb(10, 102, 194);
      this.panelTabUnderline.Height = 2;
      this.panelTabUnderline.Width = 90;
      this.panelTabUnderline.Location = new System.Drawing.Point(20, 42);
      this.panelTabs.Controls.Add(this.panelTabUnderline);

      // menuTabsMore - REMOVED (không có chức năng)
      // menuAvatar
      this.menuAvatar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
        this.miDangXuat
      });
      this.menuAvatar.Name = "menuAvatar";
      this.miDangXuat.Name = "miDangXuat"; this.miDangXuat.Text = "Đăng xuất";
      this.miDangXuat.Click += new System.EventHandler(this.btnLogout_Click);

      // panelProfileHeader composition (no cover)
      this.panelProfileHeader.Controls.Add(this.panelTabs);
      this.panelProfileHeader.Controls.Add(this.panelProfileInfo);

      // Content area - 3 CỘT: Info (trái) | Posts (giữa) | Filter (phải)
      this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelContent.Padding = new System.Windows.Forms.Padding(16, 8, 16, 16);
      this.tableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayout.ColumnCount = 3; // 3 cột
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 360F)); // Info
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // Posts - fill
      this.tableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 450F)); // Filter - rộng hơn
      this.tableLayout.RowCount = 1;
      this.tableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayout.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);

      // Left column - THÔNG TIN CÁ NHÂN
      this.panelLeftCol.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelLeftCol.Padding = new System.Windows.Forms.Padding(16);

      this.panelUserInfo.Dock = System.Windows.Forms.DockStyle.Top; // Top thay vì Fill
      this.panelUserInfo.Height = 520; // Tăng chiều cao lên
      this.panelUserInfo.Padding = new System.Windows.Forms.Padding(10);

      this.gbUserInfo.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbUserInfo.Text = "Thông tin cá nhân";
      this.gbUserInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.gbUserInfo.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);

      // User info labels - VỪA PHẢI HƠN
      this.lblDOBTitle.AutoSize = true; this.lblDOBTitle.Text = "📅 Ngày sinh:";
      this.lblDOBTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblDOBTitle.Location = new System.Drawing.Point(25, 45);

      this.lblDOBValue.AutoSize = true; this.lblDOBValue.Text = "01/01/2000";
      this.lblDOBValue.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblDOBValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
      this.lblDOBValue.Location = new System.Drawing.Point(25, 72);

      this.lblEmailTitle.AutoSize = true; this.lblEmailTitle.Text = "✉ Email:";
      this.lblEmailTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblEmailTitle.Location = new System.Drawing.Point(25, 115);

      this.lblEmailValue.AutoSize = true; this.lblEmailValue.Text = "user@example.com";
      this.lblEmailValue.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblEmailValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
      this.lblEmailValue.Location = new System.Drawing.Point(25, 142);

      this.lblPhoneTitle.AutoSize = true;
      this.lblPhoneTitle.Text = "Số điện thoại:";
      this.lblPhoneTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblPhoneTitle.Location = new System.Drawing.Point(25, 185);

      this.lblPhoneValue.AutoSize = true;
      this.lblPhoneValue.Text = "0123456789";
      this.lblPhoneValue.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblPhoneValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
      this.lblPhoneValue.Location = new System.Drawing.Point(25, 212);

      this.lblCityTitle.AutoSize = true;
      this.lblCityTitle.Text = "Sống tại:";
      this.lblCityTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblCityTitle.Location = new System.Drawing.Point(25, 255);

      this.lblCityValue.AutoSize = true;
      this.lblCityValue.Text = "Hà Nội";
      this.lblCityValue.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblCityValue.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
      this.lblCityValue.Location = new System.Drawing.Point(25, 282);

      this.gbUserInfo.Controls.Add(this.lblDOBTitle);
      this.gbUserInfo.Controls.Add(this.lblDOBValue);
      this.gbUserInfo.Controls.Add(this.lblEmailTitle);
      this.gbUserInfo.Controls.Add(this.lblEmailValue);
      this.gbUserInfo.Controls.Add(this.lblPhoneTitle);
      this.gbUserInfo.Controls.Add(this.lblPhoneValue);
      this.gbUserInfo.Controls.Add(this.lblCityTitle);
      this.gbUserInfo.Controls.Add(this.lblCityValue);

      this.panelUserInfo.Controls.Add(this.gbUserInfo);
      this.panelUserInfo.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
      this.panelLeftCol.Controls.Add(this.panelUserInfo);

      // Panel filter - BỘ LỌC (cân đối với Info panel)
      this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelFilter.Height = 540; // Tăng lên để hiển thị đủ các nút
      this.panelFilter.Padding = new System.Windows.Forms.Padding(10);

      this.gbFilter.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbFilter.Text = "Bộ lọc";
      this.gbFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);

      // Filter controls layout - VỪA PHẢI HƠN
      this.lblType.AutoSize = true; this.lblType.Text = "Loại bài viết";
      this.lblType.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblType.Location = new System.Drawing.Point(20, 45);
      this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboType.Items.AddRange(new object[] { "Tất cả", "Status", "Image", "Video" });
      this.cboType.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.cboType.Location = new System.Drawing.Point(20, 72); this.cboType.Size = new System.Drawing.Size(300, 30);

      this.lblFromDate.AutoSize = true; this.lblFromDate.Text = "Từ ngày";
      this.lblFromDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblFromDate.Location = new System.Drawing.Point(20, 125);
      this.dtpFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.dtpFrom.Location = new System.Drawing.Point(20, 152); this.dtpFrom.Size = new System.Drawing.Size(300, 30);
      this.dtpFrom.MinDate = new System.DateTime(2000, 1, 1);
      this.dtpFrom.MaxDate = System.DateTime.Now.AddYears(1);
      this.dtpFrom.Value = System.DateTime.Now.AddMonths(-1);

      this.lblToDate.AutoSize = true; this.lblToDate.Text = "Đến ngày";
      this.lblToDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblToDate.Location = new System.Drawing.Point(20, 200);
      this.dtpTo.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.dtpTo.Location = new System.Drawing.Point(20, 227); this.dtpTo.Size = new System.Drawing.Size(300, 30);
      this.dtpTo.MinDate = new System.DateTime(2000, 1, 1);
      this.dtpTo.MaxDate = System.DateTime.Now.AddYears(1);
      this.dtpTo.Value = System.DateTime.Now;

      this.lblPostId.AutoSize = true; this.lblPostId.Text = "ID Bài viết";
      this.lblPostId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblPostId.Location = new System.Drawing.Point(20, 275);
      this.txtPostId.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtPostId.Location = new System.Drawing.Point(20, 302); this.txtPostId.Size = new System.Drawing.Size(300, 38);

      this.lblSearch.AutoSize = true; this.lblSearch.Text = "Tìm kiếm";
      this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.lblSearch.Location = new System.Drawing.Point(20, 358);
      this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.txtSearch.Location = new System.Drawing.Point(20, 385); this.txtSearch.Size = new System.Drawing.Size(300, 40);
      this.txtSearch.PlaceholderText = "Tìm kiếm bài viết...";
      this.txtSearch.Dock = System.Windows.Forms.DockStyle.None;
      this.txtSearch.BorderRadius = 10;

      this.btnClearFilter.Text = "Xóa";
      this.btnClearFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.btnClearFilter.Location = new System.Drawing.Point(20, 445); this.btnClearFilter.Size = new System.Drawing.Size(100, 42);
      this.btnApplyFilter.Text = "Lọc";
      this.btnApplyFilter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.btnApplyFilter.Location = new System.Drawing.Point(220, 445); this.btnApplyFilter.Size = new System.Drawing.Size(100, 42);

      this.gbFilter.Controls.Add(this.lblType);
      this.gbFilter.Controls.Add(this.cboType);
      this.gbFilter.Controls.Add(this.lblFromDate);
      this.gbFilter.Controls.Add(this.dtpFrom);
      this.gbFilter.Controls.Add(this.lblToDate);
      this.gbFilter.Controls.Add(this.dtpTo);
      this.gbFilter.Controls.Add(this.lblPostId);
      this.gbFilter.Controls.Add(this.txtPostId);
      this.gbFilter.Controls.Add(this.lblSearch);
      this.gbFilter.Controls.Add(this.txtSearch);
      this.gbFilter.Controls.Add(this.btnClearFilter);
      this.gbFilter.Controls.Add(this.btnApplyFilter);

      this.panelFilter.Controls.Add(this.gbFilter);
      this.panelFilter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);

      // Right column - BỘ LỌC (cột phải)
      this.panelRightCol.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelRightCol.Padding = new System.Windows.Forms.Padding(10, 0, 16, 10);
      this.panelRightCol.Controls.Add(this.panelFilter);

      // Center column - CHỈ BÀI VIẾT (cột giữa)
      this.panelCenterCol.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panelCenterCol.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);

      // Posts area
      this.flowLayoutPanelPosts.AutoScroll = true;
      this.flowLayoutPanelPosts.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanelPosts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
      this.flowLayoutPanelPosts.WrapContents = false;

      // Composer panel (start a post)
      this.panelComposer.Dock = System.Windows.Forms.DockStyle.Top;
      this.panelComposer.Height = 80;
      this.panelComposer.BackColor = System.Drawing.Color.White;
      this.panelComposer.Padding = new System.Windows.Forms.Padding(16);
      this.panelComposer.Cursor = System.Windows.Forms.Cursors.Hand;
      this.panelComposer.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);

      this.picComposerAvatar.BackColor = System.Drawing.Color.LightGray;
      this.picComposerAvatar.Size = new System.Drawing.Size(40, 40);
      this.picComposerAvatar.Location = new System.Drawing.Point(20, 20);
      this.picComposerAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;

      this.lblComposerPlaceholder.AutoSize = true;
      this.lblComposerPlaceholder.Text = "Bạn đang nghĩ gì?";
      this.lblComposerPlaceholder.ForeColor = System.Drawing.Color.FromArgb(102, 102, 102);
      this.lblComposerPlaceholder.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblComposerPlaceholder.Location = new System.Drawing.Point(76, 28);

      this.panelComposer.Controls.Add(this.picComposerAvatar);
      this.panelComposer.Controls.Add(this.lblComposerPlaceholder);
      // add composer first then posts to avoid overlap
      this.panelCenterCol.Controls.Add(this.flowLayoutPanelPosts);
      this.panelCenterCol.Controls.Add(this.panelComposer);
      this.panelComposer.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelCard_Paint);
      this.panelComposer.Click += new System.EventHandler(this.panelComposer_Click);
      this.picComposerAvatar.Click += new System.EventHandler(this.panelComposer_Click);
      this.lblComposerPlaceholder.Click += new System.EventHandler(this.panelComposer_Click);

      // Add columns to table - 3 CỘT: Info | Posts | Filter
      this.tableLayout.Controls.Add(this.panelLeftCol, 0, 0); // Cột trái - Info
      this.tableLayout.Controls.Add(this.panelCenterCol, 1, 0); // Cột giữa - Posts
      this.tableLayout.Controls.Add(this.panelRightCol, 2, 0); // Cột phải - Filter
      this.panelContent.Controls.Add(this.tableLayout);

      // Form
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);
      this.ClientSize = new System.Drawing.Size(1262, 773);
      this.Controls.Add(this.btnThemeToggle); // Nút theme ở góc dưới trái
      this.Controls.Add(this.btnNewPost); // Nút tạo bài viết ở góc dưới trái
      this.Controls.Add(this.panelContent);
      this.Controls.Add(this.panelProfileHeader);
      this.Controls.Add(this.panelHeader);
      this.Name = "frmDashboard";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Trang cá nhân";
      this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
      this.Load += new System.EventHandler(this.frmDashboard_Load);
    }

    private System.Windows.Forms.Button CreateTabButton(string text, int x, int y, int w, int h, bool active)
    {
      var btn = new System.Windows.Forms.Button();
      btn.Text = text;
      btn.Location = new System.Drawing.Point(x, y);
      btn.Size = new System.Drawing.Size(w, h);
      btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      btn.FlatAppearance.BorderSize = 0;
      btn.Font = new System.Drawing.Font("Segoe UI", 9F, active ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular);
      btn.ForeColor = active ? System.Drawing.Color.FromArgb(10, 102, 194) : System.Drawing.Color.FromArgb(66, 66, 66);
      return btn;
    }



    #endregion

    private System.Windows.Forms.Panel panelHeader;
    private System.Windows.Forms.Panel panelHeaderRight;
    private SocialManager.controls.RoundedTextBox txtSearch;
    private System.Windows.Forms.Button btnNewPost;
    private System.Windows.Forms.Button btnNewsfeed;
    private System.Windows.Forms.Button btnLogout;
    private System.Windows.Forms.FlowLayoutPanel panelHeaderActions;
    private System.Windows.Forms.Button btnThemeToggle;
    private System.Windows.Forms.Button btnSettings;

    private System.Windows.Forms.Panel panelProfileHeader;
    private System.Windows.Forms.Panel panelProfileInfo;
    private System.Windows.Forms.PictureBox picProfileLarge;
    private System.Windows.Forms.Label lblProfileName;
    private System.Windows.Forms.Label lblFollowerStats; // Hiển thị @username
    private System.Windows.Forms.Label lblAccountStatus; // Trạng thái tài khoản
    private System.Windows.Forms.Button btnEditProfile;

    private System.Windows.Forms.Panel panelTabs;
    private System.Windows.Forms.Button btnTabBaiViet;
    private System.Windows.Forms.Panel panelTabUnderline;
    private System.Windows.Forms.ContextMenuStrip menuAvatar;
    private System.Windows.Forms.ToolStripMenuItem miDangXuat;

    private System.Windows.Forms.Panel panelContent;
    private System.Windows.Forms.TableLayoutPanel tableLayout;
    private System.Windows.Forms.Panel panelLeftCol;
    private System.Windows.Forms.Panel panelUserInfo; // Panel thông tin cá nhân
    private System.Windows.Forms.GroupBox gbUserInfo;
    private System.Windows.Forms.Label lblDOBTitle;
    private System.Windows.Forms.Label lblDOBValue;
    private System.Windows.Forms.Label lblEmailTitle;
    private System.Windows.Forms.Label lblEmailValue;
    private System.Windows.Forms.Label lblPhoneTitle;
    private System.Windows.Forms.Label lblPhoneValue;
    private System.Windows.Forms.Label lblCityTitle;
    private System.Windows.Forms.Label lblCityValue;
    private System.Windows.Forms.Panel panelCenterCol; // Posts column
    private System.Windows.Forms.Panel panelRightCol; // Filter column
    private System.Windows.Forms.Panel panelFilter;
    private System.Windows.Forms.GroupBox gbFilter;
    private System.Windows.Forms.Label lblSearch; // Label cho tìm kiếm
    private System.Windows.Forms.Label lblType;
    private System.Windows.Forms.ComboBox cboType;
    private System.Windows.Forms.Label lblFromDate;
    private System.Windows.Forms.DateTimePicker dtpFrom;
    private System.Windows.Forms.Label lblToDate;
    private System.Windows.Forms.DateTimePicker dtpTo;
    private System.Windows.Forms.Label lblPostId;
    private SocialManager.controls.RoundedTextBox txtPostId;
    private SocialManager.controls.RoundedButton btnApplyFilter;
    private SocialManager.controls.RoundedButton btnClearFilter;
    private SocialManager.frm.DoubleBufferedFlowLayoutPanel flowLayoutPanelPosts;
    private System.Windows.Forms.Panel panelComposer;
    private System.Windows.Forms.PictureBox picComposerAvatar;
    private System.Windows.Forms.Label lblComposerPlaceholder;
  }
}



