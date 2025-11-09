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
            components = new System.ComponentModel.Container();
            panelHeader = new Panel();
            panelHeaderRight = new Panel();
            panelHeaderActions = new FlowLayoutPanel();
            btnLogout = new Button();
            btnSettings = new Button();
            btnNewsfeed = new Button();
            txtSearch = new SocialManager.controls.RoundedTextBox();
            btnNewPost = new Button();
            btnThemeToggle = new Button();
            panelProfileHeader = new Panel();
            panelTabs = new Panel();
            btnTabBaiViet = new Button();
            panelTabUnderline = new Panel();
            panelProfileInfo = new Panel();
            picProfileLarge = new PictureBox();
            lblProfileName = new Label();
            lblFollowerStats = new Label();
            btnEditProfile = new Button();
            menuAvatar = new ContextMenuStrip(components);
            miDangXuat = new ToolStripMenuItem();
            panelContent = new Panel();
            tableLayout = new TableLayoutPanel();
            panelLeftCol = new Panel();
            panelUserInfo = new Panel();
            gbUserInfo = new GroupBox();
            lblDOBTitle = new Label();
            lblDOBValue = new Label();
            lblEmailTitle = new Label();
            lblEmailValue = new Label();
            lblPhoneTitle = new Label();
            lblPhoneValue = new Label();
            lblCityTitle = new Label();
            lblCityValue = new Label();
            panelCenterCol = new Panel();
            flowLayoutPanelPosts = new FlowLayoutPanel();
            panelComposer = new Panel();
            picComposerAvatar = new PictureBox();
            lblComposerPlaceholder = new Label();
            panelRightCol = new Panel();
            panelFilter = new Panel();
            gbFilter = new GroupBox();
            lblType = new Label();
            cboType = new ComboBox();
            lblFromDate = new Label();
            dtpFrom = new DateTimePicker();
            lblToDate = new Label();
            dtpTo = new DateTimePicker();
            lblPostId = new Label();
            txtPostId = new SocialManager.controls.RoundedTextBox();
            lblSearch = new Label();
            btnClearFilter = new SocialManager.controls.RoundedButton();
            btnApplyFilter = new SocialManager.controls.RoundedButton();
            panelHeader.SuspendLayout();
            panelHeaderRight.SuspendLayout();
            panelHeaderActions.SuspendLayout();
            panelProfileHeader.SuspendLayout();
            panelTabs.SuspendLayout();
            panelProfileInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picProfileLarge).BeginInit();
            menuAvatar.SuspendLayout();
            panelContent.SuspendLayout();
            tableLayout.SuspendLayout();
            panelLeftCol.SuspendLayout();
            panelUserInfo.SuspendLayout();
            gbUserInfo.SuspendLayout();
            panelCenterCol.SuspendLayout();
            panelComposer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picComposerAvatar).BeginInit();
            panelRightCol.SuspendLayout();
            panelFilter.SuspendLayout();
            gbFilter.SuspendLayout();
            SuspendLayout();
            //
            // panelHeader
            //
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(panelHeaderRight);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1262, 60);
            panelHeader.TabIndex = 5;
            //
            // panelHeaderRight
            //
            panelHeaderRight.Controls.Add(panelHeaderActions);
            panelHeaderRight.Dock = DockStyle.Fill;
            panelHeaderRight.Location = new Point(0, 0);
            panelHeaderRight.Name = "panelHeaderRight";
            panelHeaderRight.Padding = new Padding(8, 10, 12, 10);
            panelHeaderRight.Size = new Size(1262, 60);
            panelHeaderRight.TabIndex = 0;
            //
            // panelHeaderActions
            //
            panelHeaderActions.BackColor = Color.Transparent;
            panelHeaderActions.Controls.Add(btnLogout);
            panelHeaderActions.Controls.Add(btnSettings);
            panelHeaderActions.Controls.Add(btnNewsfeed);
            panelHeaderActions.Dock = DockStyle.Fill;
            panelHeaderActions.FlowDirection = FlowDirection.RightToLeft;
            panelHeaderActions.Location = new Point(8, 10);
            panelHeaderActions.Name = "panelHeaderActions";
            panelHeaderActions.Size = new Size(1242, 40);
            panelHeaderActions.TabIndex = 0;
            panelHeaderActions.WrapContents = false;
            //
            // btnLogout
            //
            btnLogout.Anchor = AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(228, 230, 235);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F);
            btnLogout.ForeColor = Color.FromArgb(33, 33, 33);
            btnLogout.Location = new Point(1152, 0);
            btnLogout.Margin = new Padding(8, 0, 0, 0);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(90, 36);
            btnLogout.TabIndex = 0;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            //
            // btnSettings
            //
            btnSettings.AutoSize = true;
            btnSettings.BackColor = Color.FromArgb(228, 230, 235);
            btnSettings.Cursor = Cursors.Hand;
            btnSettings.FlatAppearance.BorderSize = 0;
            btnSettings.FlatStyle = FlatStyle.Flat;
            btnSettings.ForeColor = Color.FromArgb(33, 33, 33);
            btnSettings.Location = new Point(942, 0);
            btnSettings.Margin = new Padding(8, 0, 0, 0);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(202, 36);
            btnSettings.TabIndex = 1;
            btnSettings.Text = "Chỉnh sửa thông tin cá nhân";
            btnSettings.UseVisualStyleBackColor = false;
            btnSettings.Click += btnSettings_Click;
            //
            // btnNewsfeed
            //
            btnNewsfeed.AutoSize = true;
            btnNewsfeed.BackColor = Color.FromArgb(24, 119, 242);
            btnNewsfeed.Cursor = Cursors.Hand;
            btnNewsfeed.FlatAppearance.BorderSize = 0;
            btnNewsfeed.FlatStyle = FlatStyle.Flat;
            btnNewsfeed.ForeColor = Color.White;
            btnNewsfeed.Location = new Point(814, 0);
            btnNewsfeed.Margin = new Padding(8, 0, 0, 0);
            btnNewsfeed.Name = "btnNewsfeed";
            btnNewsfeed.Size = new Size(120, 36);
            btnNewsfeed.TabIndex = 2;
            btnNewsfeed.Text = "Newsfeed";
            btnNewsfeed.UseVisualStyleBackColor = false;
            btnNewsfeed.Click += btnNewsfeed_Click;
            //
            // txtSearch
            //
            txtSearch.BackColor = Color.White;
            txtSearch.BorderColor = Color.MediumSlateBlue;
            txtSearch.BorderFocusColor = Color.HotPink;
            txtSearch.BorderRadius = 10;
            txtSearch.BorderSize = 2;
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.ForeColor = Color.FromArgb(64, 64, 64);
            txtSearch.Location = new Point(20, 385);
            txtSearch.Multiline = false;
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(10, 7, 10, 7);
            txtSearch.PasswordChar = false;
            txtSearch.PlaceholderColor = Color.DarkGray;
            txtSearch.PlaceholderText = "Tìm kiếm bài viết...";
            txtSearch.Size = new Size(300, 38);
            txtSearch.TabIndex = 9;
            txtSearch.UnderlinedStyle = false;
            //
            // btnNewPost
            //
            btnNewPost.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNewPost.BackColor = Color.FromArgb(10, 102, 194);
            btnNewPost.Cursor = Cursors.Hand;
            btnNewPost.FlatAppearance.BorderSize = 0;
            btnNewPost.FlatStyle = FlatStyle.Flat;
            btnNewPost.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnNewPost.ForeColor = Color.White;
            btnNewPost.Location = new Point(20, 20);
            btnNewPost.Name = "btnNewPost";
            btnNewPost.Size = new Size(48, 48);
            btnNewPost.TabIndex = 2;
            btnNewPost.Text = "+";
            btnNewPost.UseVisualStyleBackColor = false;
            btnNewPost.Click += btnNewPost_Click;
            //
            // btnThemeToggle
            //
            btnThemeToggle.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThemeToggle.BackColor = Color.FromArgb(228, 230, 235);
            btnThemeToggle.Cursor = Cursors.Hand;
            btnThemeToggle.FlatAppearance.BorderSize = 0;
            btnThemeToggle.FlatStyle = FlatStyle.Flat;
            btnThemeToggle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            btnThemeToggle.ForeColor = Color.FromArgb(33, 33, 33);
            btnThemeToggle.Location = new Point(20, 20);
            btnThemeToggle.Name = "btnThemeToggle";
            btnThemeToggle.Size = new Size(56, 56);
            btnThemeToggle.TabIndex = 1;
            btnThemeToggle.Text = "◐";
            btnThemeToggle.UseVisualStyleBackColor = false;
            btnThemeToggle.Click += btnThemeToggle_Click;
            //
            // panelProfileHeader
            //
            panelProfileHeader.BackColor = Color.White;
            panelProfileHeader.Controls.Add(panelTabs);
            panelProfileHeader.Controls.Add(panelProfileInfo);
            panelProfileHeader.Dock = DockStyle.Top;
            panelProfileHeader.Location = new Point(0, 60);
            panelProfileHeader.Name = "panelProfileHeader";
            panelProfileHeader.Size = new Size(1262, 150);
            panelProfileHeader.TabIndex = 4;
            //
            // panelTabs
            //
            panelTabs.BackColor = Color.White;
            panelTabs.Controls.Add(btnTabBaiViet);
            panelTabs.Controls.Add(panelTabUnderline);
            panelTabs.Dock = DockStyle.Top;
            panelTabs.Location = new Point(0, 106);
            panelTabs.Name = "panelTabs";
            panelTabs.Padding = new Padding(0, 0, 0, 6);
            panelTabs.Size = new Size(1262, 44);
            panelTabs.TabIndex = 0;
            //
            // btnTabBaiViet
            //
            btnTabBaiViet.Location = new Point(0, 0);
            btnTabBaiViet.Name = "btnTabBaiViet";
            btnTabBaiViet.Size = new Size(75, 23);
            btnTabBaiViet.TabIndex = 0;
            btnTabBaiViet.Click += TabButton_Click;
            //
            // panelTabUnderline
            //
            panelTabUnderline.BackColor = Color.FromArgb(10, 102, 194);
            panelTabUnderline.Location = new Point(20, 42);
            panelTabUnderline.Name = "panelTabUnderline";
            panelTabUnderline.Size = new Size(90, 2);
            panelTabUnderline.TabIndex = 1;
            //
            // panelProfileInfo
            //
            panelProfileInfo.BackColor = Color.White;
            panelProfileInfo.Controls.Add(picProfileLarge);
            panelProfileInfo.Controls.Add(lblProfileName);
            panelProfileInfo.Controls.Add(lblFollowerStats);
            panelProfileInfo.Controls.Add(btnEditProfile);
            panelProfileInfo.Dock = DockStyle.Top;
            panelProfileInfo.Location = new Point(0, 0);
            panelProfileInfo.Name = "panelProfileInfo";
            panelProfileInfo.Padding = new Padding(24, 8, 24, 8);
            panelProfileInfo.Size = new Size(1262, 106);
            panelProfileInfo.TabIndex = 1;
            //
            // picProfileLarge
            //
            picProfileLarge.BackColor = Color.LightGray;
            picProfileLarge.Location = new Point(24, 8);
            picProfileLarge.Name = "picProfileLarge";
            picProfileLarge.Size = new Size(90, 90);
            picProfileLarge.SizeMode = PictureBoxSizeMode.Zoom;
            picProfileLarge.TabIndex = 0;
            picProfileLarge.TabStop = false;
            //
            // lblProfileName
            //
            lblProfileName.AutoSize = true;
            lblProfileName.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProfileName.Location = new Point(130, 15);
            lblProfileName.Name = "lblProfileName";
            lblProfileName.Size = new Size(156, 37);
            lblProfileName.TabIndex = 1;
            lblProfileName.Text = "Tên đầy đủ";
            //
            // lblFollowerStats
            //
            lblFollowerStats.AutoSize = true;
            lblFollowerStats.Font = new Font("Segoe UI", 9F);
            lblFollowerStats.ForeColor = Color.FromArgb(102, 102, 102);
            lblFollowerStats.Location = new Point(130, 52);
            lblFollowerStats.Name = "lblFollowerStats";
            lblFollowerStats.Size = new Size(87, 20);
            lblFollowerStats.TabIndex = 2;
            lblFollowerStats.Text = "@username";
            //
            // btnEditProfile
            //
            btnEditProfile.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEditProfile.BackColor = Color.FromArgb(228, 230, 235);
            btnEditProfile.Cursor = Cursors.Hand;
            btnEditProfile.FlatAppearance.BorderSize = 0;
            btnEditProfile.FlatStyle = FlatStyle.Flat;
            btnEditProfile.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnEditProfile.ForeColor = Color.FromArgb(33, 33, 33);
            btnEditProfile.Location = new Point(2162, 32);
            btnEditProfile.Name = "btnEditProfile";
            btnEditProfile.Size = new Size(140, 36);
            btnEditProfile.TabIndex = 3;
            btnEditProfile.Text = "Chỉnh sửa hồ sơ";
            btnEditProfile.UseVisualStyleBackColor = false;
            btnEditProfile.Click += btnSettings_Click;
            //
            // menuAvatar
            //
            menuAvatar.ImageScalingSize = new Size(20, 20);
            menuAvatar.Items.AddRange(new ToolStripItem[] { miDangXuat });
            menuAvatar.Name = "menuAvatar";
            menuAvatar.Size = new Size(147, 28);
            //
            // miDangXuat
            //
            miDangXuat.Name = "miDangXuat";
            miDangXuat.Size = new Size(146, 24);
            miDangXuat.Text = "Đăng xuất";
            miDangXuat.Click += btnLogout_Click;
            //
            // panelContent
            //
            panelContent.Controls.Add(tableLayout);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 210);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(16, 8, 16, 16);
            panelContent.Size = new Size(1262, 563);
            panelContent.TabIndex = 3;
            //
            // tableLayout
            //
            tableLayout.ColumnCount = 3;
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 360F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 450F));
            tableLayout.Controls.Add(panelLeftCol, 0, 0);
            tableLayout.Controls.Add(panelCenterCol, 1, 0);
            tableLayout.Controls.Add(panelRightCol, 2, 0);
            tableLayout.Dock = DockStyle.Fill;
            tableLayout.Location = new Point(16, 8);
            tableLayout.Name = "tableLayout";
            tableLayout.Padding = new Padding(0, 8, 0, 0);
            tableLayout.RowCount = 1;
            tableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayout.Size = new Size(1230, 539);
            tableLayout.TabIndex = 0;
            //
            // panelLeftCol
            //
            panelLeftCol.Controls.Add(panelUserInfo);
            panelLeftCol.Dock = DockStyle.Fill;
            panelLeftCol.Location = new Point(3, 11);
            panelLeftCol.Name = "panelLeftCol";
            panelLeftCol.Padding = new Padding(16);
            panelLeftCol.Size = new Size(354, 525);
            panelLeftCol.TabIndex = 0;
            //
            // panelUserInfo
            //
            panelUserInfo.Controls.Add(gbUserInfo);
            panelUserInfo.Dock = DockStyle.Top;
            panelUserInfo.Location = new Point(16, 16);
            panelUserInfo.Name = "panelUserInfo";
            panelUserInfo.Padding = new Padding(10);
            panelUserInfo.Size = new Size(322, 520);
            panelUserInfo.TabIndex = 0;
            panelUserInfo.Paint += PanelCard_Paint;
            //
            // gbUserInfo
            //
            gbUserInfo.Controls.Add(lblDOBTitle);
            gbUserInfo.Controls.Add(lblDOBValue);
            gbUserInfo.Controls.Add(lblEmailTitle);
            gbUserInfo.Controls.Add(lblEmailValue);
            gbUserInfo.Controls.Add(lblPhoneTitle);
            gbUserInfo.Controls.Add(lblPhoneValue);
            gbUserInfo.Controls.Add(lblCityTitle);
            gbUserInfo.Controls.Add(lblCityValue);
            gbUserInfo.Dock = DockStyle.Fill;
            gbUserInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbUserInfo.Location = new Point(10, 10);
            gbUserInfo.Name = "gbUserInfo";
            gbUserInfo.Padding = new Padding(20, 30, 20, 20);
            gbUserInfo.Size = new Size(302, 500);
            gbUserInfo.TabIndex = 0;
            gbUserInfo.TabStop = false;
            gbUserInfo.Text = "Thông tin cá nhân";
            //
            // lblDOBTitle
            //
            lblDOBTitle.AutoSize = true;
            lblDOBTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDOBTitle.Location = new Point(25, 45);
            lblDOBTitle.Name = "lblDOBTitle";
            lblDOBTitle.Size = new Size(123, 23);
            lblDOBTitle.TabIndex = 0;
            lblDOBTitle.Text = "📅 Ngày sinh:";
            //
            // lblDOBValue
            //
            lblDOBValue.AutoSize = true;
            lblDOBValue.Font = new Font("Segoe UI", 10F);
            lblDOBValue.ForeColor = Color.FromArgb(66, 66, 66);
            lblDOBValue.Location = new Point(25, 72);
            lblDOBValue.Name = "lblDOBValue";
            lblDOBValue.Size = new Size(96, 23);
            lblDOBValue.TabIndex = 1;
            lblDOBValue.Text = "01/01/2000";
            //
            // lblEmailTitle
            //
            lblEmailTitle.AutoSize = true;
            lblEmailTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmailTitle.Location = new Point(25, 115);
            lblEmailTitle.Name = "lblEmailTitle";
            lblEmailTitle.Size = new Size(88, 23);
            lblEmailTitle.TabIndex = 2;
            lblEmailTitle.Text = "✉ Email:";
            //
            // lblEmailValue
            //
            lblEmailValue.AutoSize = true;
            lblEmailValue.Font = new Font("Segoe UI", 10F);
            lblEmailValue.ForeColor = Color.FromArgb(66, 66, 66);
            lblEmailValue.Location = new Point(25, 142);
            lblEmailValue.Name = "lblEmailValue";
            lblEmailValue.Size = new Size(159, 23);
            lblEmailValue.TabIndex = 3;
            lblEmailValue.Text = "user@example.com";
            //
            // lblPhoneTitle
            //
            lblPhoneTitle.AutoSize = true;
            lblPhoneTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhoneTitle.Location = new Point(25, 185);
            lblPhoneTitle.Name = "lblPhoneTitle";
            lblPhoneTitle.Size = new Size(121, 23);
            lblPhoneTitle.TabIndex = 4;
            lblPhoneTitle.Text = "Số điện thoại:";
            //
            // lblPhoneValue
            //
            lblPhoneValue.AutoSize = true;
            lblPhoneValue.Font = new Font("Segoe UI", 10F);
            lblPhoneValue.ForeColor = Color.FromArgb(66, 66, 66);
            lblPhoneValue.Location = new Point(25, 212);
            lblPhoneValue.Name = "lblPhoneValue";
            lblPhoneValue.Size = new Size(100, 23);
            lblPhoneValue.TabIndex = 5;
            lblPhoneValue.Text = "0123456789";
            //
            // lblCityTitle
            //
            lblCityTitle.AutoSize = true;
            lblCityTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCityTitle.Location = new Point(25, 255);
            lblCityTitle.Name = "lblCityTitle";
            lblCityTitle.Size = new Size(82, 23);
            lblCityTitle.TabIndex = 6;
            lblCityTitle.Text = "Sống tại:";
            //
            // lblCityValue
            //
            lblCityValue.AutoSize = true;
            lblCityValue.Font = new Font("Segoe UI", 10F);
            lblCityValue.ForeColor = Color.FromArgb(66, 66, 66);
            lblCityValue.Location = new Point(25, 282);
            lblCityValue.Name = "lblCityValue";
            lblCityValue.Size = new Size(63, 23);
            lblCityValue.TabIndex = 7;
            lblCityValue.Text = "Hà Nội";
            //
            // panelCenterCol
            //
            panelCenterCol.Controls.Add(flowLayoutPanelPosts);
            panelCenterCol.Controls.Add(panelComposer);
            panelCenterCol.Dock = DockStyle.Fill;
            panelCenterCol.Location = new Point(363, 11);
            panelCenterCol.Name = "panelCenterCol";
            panelCenterCol.Padding = new Padding(10, 0, 10, 10);
            panelCenterCol.Size = new Size(414, 525);
            panelCenterCol.TabIndex = 1;
            //
            // flowLayoutPanelPosts
            //
            flowLayoutPanelPosts.AutoScroll = true;
            flowLayoutPanelPosts.Dock = DockStyle.Fill;
            flowLayoutPanelPosts.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanelPosts.Location = new Point(10, 80);
            flowLayoutPanelPosts.Name = "flowLayoutPanelPosts";
            flowLayoutPanelPosts.Size = new Size(394, 435);
            flowLayoutPanelPosts.TabIndex = 0;
            flowLayoutPanelPosts.WrapContents = false;
            //
            // panelComposer
            //
            panelComposer.BackColor = Color.White;
            panelComposer.Controls.Add(picComposerAvatar);
            panelComposer.Controls.Add(lblComposerPlaceholder);
            panelComposer.Cursor = Cursors.Hand;
            panelComposer.Dock = DockStyle.Top;
            panelComposer.Location = new Point(10, 0);
            panelComposer.Margin = new Padding(0, 8, 0, 8);
            panelComposer.Name = "panelComposer";
            panelComposer.Padding = new Padding(16);
            panelComposer.Size = new Size(394, 80);
            panelComposer.TabIndex = 1;
            panelComposer.Click += panelComposer_Click;
            panelComposer.Paint += PanelCard_Paint;
            //
            // picComposerAvatar
            //
            picComposerAvatar.BackColor = Color.LightGray;
            picComposerAvatar.Location = new Point(20, 20);
            picComposerAvatar.Name = "picComposerAvatar";
            picComposerAvatar.Size = new Size(40, 40);
            picComposerAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picComposerAvatar.TabIndex = 0;
            picComposerAvatar.TabStop = false;
            picComposerAvatar.Click += panelComposer_Click;
            //
            // lblComposerPlaceholder
            //
            lblComposerPlaceholder.AutoSize = true;
            lblComposerPlaceholder.Font = new Font("Segoe UI", 10F);
            lblComposerPlaceholder.ForeColor = Color.FromArgb(102, 102, 102);
            lblComposerPlaceholder.Location = new Point(76, 28);
            lblComposerPlaceholder.Name = "lblComposerPlaceholder";
            lblComposerPlaceholder.Size = new Size(149, 23);
            lblComposerPlaceholder.TabIndex = 1;
            lblComposerPlaceholder.Text = "Bạn đang nghĩ gì?";
            lblComposerPlaceholder.Click += panelComposer_Click;
            //
            // panelRightCol
            //
            panelRightCol.Controls.Add(panelFilter);
            panelRightCol.Dock = DockStyle.Fill;
            panelRightCol.Location = new Point(783, 11);
            panelRightCol.Name = "panelRightCol";
            panelRightCol.Padding = new Padding(10, 0, 16, 10);
            panelRightCol.Size = new Size(444, 525);
            panelRightCol.TabIndex = 2;
            //
            // panelFilter
            //
            panelFilter.Controls.Add(gbFilter);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(10, 0);
            panelFilter.Name = "panelFilter";
            panelFilter.Padding = new Padding(10);
            panelFilter.Size = new Size(418, 540);
            panelFilter.TabIndex = 0;
            panelFilter.Paint += PanelCard_Paint;
            //
            // gbFilter
            //
            gbFilter.Controls.Add(lblType);
            gbFilter.Controls.Add(cboType);
            gbFilter.Controls.Add(lblFromDate);
            gbFilter.Controls.Add(dtpFrom);
            gbFilter.Controls.Add(lblToDate);
            gbFilter.Controls.Add(dtpTo);
            gbFilter.Controls.Add(lblPostId);
            gbFilter.Controls.Add(txtPostId);
            gbFilter.Controls.Add(lblSearch);
            gbFilter.Controls.Add(txtSearch);
            gbFilter.Controls.Add(btnClearFilter);
            gbFilter.Controls.Add(btnApplyFilter);
            gbFilter.Dock = DockStyle.Fill;
            gbFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            gbFilter.Location = new Point(10, 10);
            gbFilter.Name = "gbFilter";
            gbFilter.Size = new Size(398, 520);
            gbFilter.TabIndex = 0;
            gbFilter.TabStop = false;
            gbFilter.Text = "Bộ lọc";
            //
            // lblType
            //
            lblType.AutoSize = true;
            lblType.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblType.Location = new Point(20, 45);
            lblType.Name = "lblType";
            lblType.Size = new Size(108, 23);
            lblType.TabIndex = 0;
            lblType.Text = "Loại bài viết";
            //
            // cboType
            //
            cboType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboType.Font = new Font("Segoe UI", 10F);
            cboType.Items.AddRange(new object[] { "Tất cả", "Status", "Image", "Video" });
            cboType.Location = new Point(20, 72);
            cboType.Name = "cboType";
            cboType.Size = new Size(300, 31);
            cboType.TabIndex = 1;
            //
            // lblFromDate
            //
            lblFromDate.AutoSize = true;
            lblFromDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFromDate.Location = new Point(20, 125);
            lblFromDate.Name = "lblFromDate";
            lblFromDate.Size = new Size(75, 23);
            lblFromDate.TabIndex = 2;
            lblFromDate.Text = "Từ ngày";
            //
            // dtpFrom
            //
            dtpFrom.Font = new Font("Segoe UI", 10F);
            dtpFrom.Location = new Point(20, 152);
            dtpFrom.MaxDate = new DateTime(2026, 11, 9, 10, 44, 7, 812);
            dtpFrom.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Size = new Size(300, 30);
            dtpFrom.TabIndex = 3;
            dtpFrom.Value = new DateTime(2025, 10, 9, 10, 44, 7, 813);
            //
            // lblToDate
            //
            lblToDate.AutoSize = true;
            lblToDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblToDate.Location = new Point(20, 200);
            lblToDate.Name = "lblToDate";
            lblToDate.Size = new Size(86, 23);
            lblToDate.TabIndex = 4;
            lblToDate.Text = "Đến ngày";
            //
            // dtpTo
            //
            dtpTo.Font = new Font("Segoe UI", 10F);
            dtpTo.Location = new Point(20, 227);
            dtpTo.MaxDate = new DateTime(2026, 11, 9, 10, 44, 7, 814);
            dtpTo.MinDate = new DateTime(2000, 1, 1, 0, 0, 0, 0);
            dtpTo.Name = "dtpTo";
            dtpTo.Size = new Size(300, 30);
            dtpTo.TabIndex = 5;
            dtpTo.Value = new DateTime(2025, 11, 9, 10, 44, 7, 814);
            //
            // lblPostId
            //
            lblPostId.AutoSize = true;
            lblPostId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPostId.Location = new Point(20, 275);
            lblPostId.Name = "lblPostId";
            lblPostId.Size = new Size(93, 23);
            lblPostId.TabIndex = 6;
            lblPostId.Text = "ID Bài viết";
            //
            // txtPostId
            //
            txtPostId.BackColor = Color.White;
            txtPostId.BorderColor = Color.MediumSlateBlue;
            txtPostId.BorderFocusColor = Color.HotPink;
            txtPostId.BorderRadius = 0;
            txtPostId.BorderSize = 2;
            txtPostId.Font = new Font("Segoe UI", 10F);
            txtPostId.ForeColor = Color.FromArgb(64, 64, 64);
            txtPostId.Location = new Point(20, 302);
            txtPostId.Multiline = false;
            txtPostId.Name = "txtPostId";
            txtPostId.Padding = new Padding(10, 7, 10, 7);
            txtPostId.PasswordChar = false;
            txtPostId.PlaceholderColor = Color.DarkGray;
            txtPostId.PlaceholderText = "";
            txtPostId.Size = new Size(300, 38);
            txtPostId.TabIndex = 7;
            txtPostId.UnderlinedStyle = false;
            //
            // lblSearch
            //
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSearch.Location = new Point(20, 358);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(86, 23);
            lblSearch.TabIndex = 8;
            lblSearch.Text = "Tìm kiếm";
            //
            // btnClearFilter
            //
            btnClearFilter.BackColor = Color.MediumSlateBlue;
            btnClearFilter.BackgroundColor = Color.MediumSlateBlue;
            btnClearFilter.BorderColor = Color.PaleVioletRed;
            btnClearFilter.BorderRadius = 40;
            btnClearFilter.BorderSize = 0;
            btnClearFilter.FlatStyle = FlatStyle.Flat;
            btnClearFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearFilter.ForeColor = Color.White;
            btnClearFilter.Location = new Point(20, 445);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(100, 42);
            btnClearFilter.TabIndex = 10;
            btnClearFilter.Text = "Xóa";
            btnClearFilter.TextColor = Color.White;
            btnClearFilter.UseVisualStyleBackColor = false;
            //
            // btnApplyFilter
            //
            btnApplyFilter.BackColor = Color.MediumSlateBlue;
            btnApplyFilter.BackgroundColor = Color.MediumSlateBlue;
            btnApplyFilter.BorderColor = Color.PaleVioletRed;
            btnApplyFilter.BorderRadius = 40;
            btnApplyFilter.BorderSize = 0;
            btnApplyFilter.FlatStyle = FlatStyle.Flat;
            btnApplyFilter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApplyFilter.ForeColor = Color.White;
            btnApplyFilter.Location = new Point(220, 445);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(100, 42);
            btnApplyFilter.TabIndex = 11;
            btnApplyFilter.Text = "Lọc";
            btnApplyFilter.TextColor = Color.White;
            btnApplyFilter.UseVisualStyleBackColor = false;
            //
            // frmDashboard
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(1262, 773);
            Controls.Add(btnThemeToggle);
            Controls.Add(btnNewPost);
            Controls.Add(panelContent);
            Controls.Add(panelProfileHeader);
            Controls.Add(panelHeader);
            Name = "frmDashboard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Trang cá nhân";
            WindowState = FormWindowState.Maximized;
            Load += frmDashboard_Load;
            panelHeader.ResumeLayout(false);
            panelHeaderRight.ResumeLayout(false);
            panelHeaderActions.ResumeLayout(false);
            panelHeaderActions.PerformLayout();
            panelProfileHeader.ResumeLayout(false);
            panelTabs.ResumeLayout(false);
            panelProfileInfo.ResumeLayout(false);
            panelProfileInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picProfileLarge).EndInit();
            menuAvatar.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            tableLayout.ResumeLayout(false);
            panelLeftCol.ResumeLayout(false);
            panelUserInfo.ResumeLayout(false);
            gbUserInfo.ResumeLayout(false);
            gbUserInfo.PerformLayout();
            panelCenterCol.ResumeLayout(false);
            panelComposer.ResumeLayout(false);
            panelComposer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picComposerAvatar).EndInit();
            panelRightCol.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            gbFilter.ResumeLayout(false);
            gbFilter.PerformLayout();
            ResumeLayout(false);
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
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelPosts;
    //private SocialManager.frm.DoubleBufferedFlowLayoutPanel flowLayoutPanelPosts;
    private System.Windows.Forms.Panel panelComposer;
    private System.Windows.Forms.PictureBox picComposerAvatar;
    private System.Windows.Forms.Label lblComposerPlaceholder;
  }
}

// dòng 79 và dòng gần cuối

