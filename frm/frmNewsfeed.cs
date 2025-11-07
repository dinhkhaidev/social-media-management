using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.utils;

namespace SocialManager.frm
{
  public partial class frmNewsfeed : Form
  {
    private User? currentUser;
    private PostService postService;
    private UserService userService;
    private Panel pnlHeader = null!;
    private Panel pnlSidebar = null!;
    private Panel pnlMainContent = null!;
    private FlowLayoutPanel flpPosts = null!;
    private PictureBox picAvatar = null!;
    private TextBox txtSearch = null!;

    // Filter controls
    private Panel pnlFilterSection = null!;
    private GroupBox gbFilter = null!;
    private ComboBox cboType = null!;
    private DateTimePicker dtpFrom = null!;
    private DateTimePicker dtpTo = null!;
    private TextBox txtPostId = null!;
    private Button btnApplyFilter = null!;
    private Button btnClearFilter = null!;

    // Lưu tất cả bài viết để lọc
    private List<Post>? allPosts;
    private bool isLoading = false;

    public frmNewsfeed()
    {
      InitializeComponent();
      InitializeCustomComponents();

      postService = new PostService();
      userService = new UserService();
      currentUser = AuthSessionService.CurrentUser;

      if (currentUser == null)
      {
        MessageBox.Show("Vui lòng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Close();
        return;
      }

      LoadUserProfile();
      LoadNewsfeed();
      InitializeFilter();
    }

    private void InitializeCustomComponents()
    {
      this.Text = "Social Manager - Newsfeed";
      this.Size = new Size(1400, 900);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.BackColor = Color.FromArgb(240, 242, 245);
      this.FormBorderStyle = FormBorderStyle.Sizable;
      this.MinimumSize = new Size(1200, 700);

      // ============ HEADER PANEL ============
      pnlHeader = new Panel
      {
        Dock = DockStyle.Top,
        Height = 60,
        BackColor = Color.White,
        BorderStyle = BorderStyle.None
      };

      // Logo
      Label lblLogo = new Label
      {
        Text = "SM",
        Font = new Font("Segoe UI", 20F, FontStyle.Bold),
        ForeColor = Color.FromArgb(24, 119, 242),
        Location = new Point(20, 15),
        Size = new Size(60, 40),
        Cursor = Cursors.Hand
      };
      lblLogo.Click += (s, e) => LoadNewsfeed();

      // Search Box - ĐẶT GIỮA HEADER
      txtSearch = new TextBox
      {
        Font = new Font("Segoe UI", 11F),
        Size = new Size(500, 35),
        PlaceholderText = "Tìm kiếm bài viết, người dùng...",
        BorderStyle = BorderStyle.FixedSingle
      };
      txtSearch.TextChanged += TxtSearch_TextChanged;
      UIHelper.ApplyRoundedCorners(txtSearch, 18);

      // Tính toán vị trí giữa header
      int searchBoxX = (1400 - txtSearch.Width) / 2;
      txtSearch.Location = new Point(searchBoxX, 13);

      // Avatar - bên phải
      picAvatar = new PictureBox
      {
        Size = new Size(40, 40),
        Location = new Point(1330, 10),
        SizeMode = PictureBoxSizeMode.Zoom,
        BackColor = Color.LightGray,
        Cursor = Cursors.Hand,
        Anchor = AnchorStyles.Top | AnchorStyles.Right
      };
      picAvatar.Click += PicAvatar_Click;
      UIHelper.MakeCircular(picAvatar);

      pnlHeader.Controls.Add(lblLogo);
      pnlHeader.Controls.Add(txtSearch);
      pnlHeader.Controls.Add(picAvatar);

      // Thêm sự kiện resize để căn giữa search box
      pnlHeader.Resize += (s, e) =>
      {
        if (txtSearch != null)
        {
          txtSearch.Location = new Point((pnlHeader.Width - txtSearch.Width) / 2, 13);
        }
        if (picAvatar != null)
        {
          picAvatar.Location = new Point(pnlHeader.Width - 60, 10);
        }
      };

      // ============ SIDEBAR PANEL (BÊN TRÁI) ============
      pnlSidebar = new Panel
      {
        Dock = DockStyle.Left,
        Width = 280,
        BackColor = Color.FromArgb(240, 242, 245),
        Padding = new Padding(10, 20, 10, 10)
      };

      FlowLayoutPanel flpSidebar = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        AutoScroll = true,
        WrapContents = false,
        Padding = new Padding(5)
      };

      flpSidebar.Controls.Add(CreateSidebarItem("", "Trang cá nhân", () => OpenProfile()));
      flpSidebar.Controls.Add(CreateSidebarItem("", "Dashboard", () => OpenDashboard()));
      flpSidebar.Controls.Add(CreateSidebarItem("", "Thống kê", () => ShowStatistics()));
      flpSidebar.Controls.Add(CreateSidebarItem("", "Cài đặt", () => OpenSettings()));
      flpSidebar.Controls.Add(CreateSidebarItem("", "Đăng xuất", () => Logout()));

      pnlSidebar.Controls.Add(flpSidebar);

      // ============ FILTER PANEL (BÊN PHẢI) ============
      pnlFilterSection = CreateFilterSection();
      pnlFilterSection.Dock = DockStyle.Right;
      pnlFilterSection.Width = 350;
      pnlFilterSection.AutoScroll = true;

      // ============ MAIN CONTENT PANEL (GIỮA) ============
      pnlMainContent = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.FromArgb(240, 242, 245),
        Padding = new Padding(20, 10, 20, 20)
      };

      // Create Post Section
      Panel pnlCreatePost = CreatePostInputSection();
      pnlCreatePost.Dock = DockStyle.Top;
      pnlCreatePost.Height = 140;
      pnlCreatePost.Margin = new Padding(0, 0, 0, 15);

      // Posts FlowLayout
      flpPosts = new FlowLayoutPanel
      {
        Dock = DockStyle.Fill,
        FlowDirection = FlowDirection.TopDown,
        AutoScroll = true,
        WrapContents = false,
        BackColor = Color.FromArgb(240, 242, 245),
        Padding = new Padding(0, 155, 0, 0) // Top padding cho CreatePost section
      };

      pnlMainContent.Controls.Add(flpPosts);
      pnlMainContent.Controls.Add(pnlCreatePost); // Add sau để nó nằm trên flpPosts

      // ============ ADD ALL PANELS TO FORM ============
      this.Controls.Add(pnlMainContent);
      this.Controls.Add(pnlFilterSection); // Phải add filter trước sidebar để nó ở bên phải
      this.Controls.Add(pnlSidebar);
      this.Controls.Add(pnlHeader);
    }

    private Panel CreateSidebarItem(string icon, string text, Action onClick)
    {
      Panel panel = new Panel
      {
        Size = new Size(250, 45),
        BackColor = Color.Transparent,
        Cursor = Cursors.Hand,
        Margin = new Padding(0, 2, 0, 2)
      };

      Label lblIcon = new Label
      {
        Text = icon,
        Font = new Font("Segoe UI", 14F),
        Location = new Point(10, 8),
        Size = new Size(30, 30),
        TextAlign = ContentAlignment.MiddleCenter
      };

      Label lblText = new Label
      {
        Text = text,
        Font = new Font("Segoe UI", 11F),
        Location = new Point(50, 12),
        AutoSize = true,
        ForeColor = Color.FromArgb(5, 5, 5)
      };

      panel.Controls.Add(lblIcon);
      panel.Controls.Add(lblText);

      panel.Click += (s, e) => onClick();
      lblIcon.Click += (s, e) => onClick();
      lblText.Click += (s, e) => onClick();

      panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(228, 230, 235);
      panel.MouseLeave += (s, e) => panel.BackColor = Color.Transparent;

      return panel;
    }

    private Panel CreatePostInputSection()
    {
      Panel panel = new Panel
      {
        Height = 120,
        BackColor = Color.White,
        BorderStyle = BorderStyle.None,
        Padding = new Padding(15)
      };
      UIHelper.ApplyRoundedCorners(panel, 15);

      PictureBox picUserAvatar = new PictureBox
      {
        Size = new Size(40, 40),
        Location = new Point(15, 15),
        SizeMode = PictureBoxSizeMode.Zoom,
        BackColor = Color.LightGray
      };
      UIHelper.MakeCircular(picUserAvatar);

      if (currentUser != null && !string.IsNullOrEmpty(currentUser.AvatarUrl) && File.Exists(currentUser.AvatarUrl))
      {
        try { picUserAvatar.Image = Image.FromFile(currentUser.AvatarUrl); }
        catch { }
      }

      TextBox txtPostContent = new TextBox
      {
        Font = new Font("Segoe UI", 11F),
        Location = new Point(65, 15),
        Size = new Size(panel.Width - 85, 40),
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        PlaceholderText = $"Bạn đang nghĩ gì, {currentUser?.FullName}?",
        BorderStyle = BorderStyle.FixedSingle,
        Cursor = Cursors.Hand,
        ReadOnly = true,
        BackColor = Color.FromArgb(240, 242, 245)
      };
      txtPostContent.Click += (s, e) => OpenCreatePostForm();
      UIHelper.ApplyRoundedCorners(txtPostContent, 20);

      Panel pnlActions = new Panel
      {
        Location = new Point(15, 70),
        Size = new Size(panel.Width - 30, 35),
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
        BorderStyle = BorderStyle.None
      };

      Button btnPhoto = CreatePostActionButton("Ảnh/Video", 10);
      btnPhoto.Click += (s, e) => OpenCreatePostForm();
      UIHelper.ApplyRoundedCorners(btnPhoto, 8);

      Button btnFeeling = CreatePostActionButton("Cảm xúc", 180);
      btnFeeling.Click += (s, e) => OpenCreatePostForm();
      UIHelper.ApplyRoundedCorners(btnFeeling, 8);

      Button btnCheckin = CreatePostActionButton("Check in", 350);
      btnCheckin.Click += (s, e) => OpenCreatePostForm();
      UIHelper.ApplyRoundedCorners(btnCheckin, 8);

      pnlActions.Controls.AddRange(new Control[] { btnPhoto, btnFeeling, btnCheckin });

      panel.Controls.Add(picUserAvatar);
      panel.Controls.Add(txtPostContent);
      panel.Controls.Add(pnlActions);

      return panel;
    }

    private Button CreatePostActionButton(string text, int x)
    {
      Button btn = new Button
      {
        Text = text,
        Font = new Font("Segoe UI", 10F),
        Size = new Size(150, 30),
        Location = new Point(x, 2),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.White,
        Cursor = Cursors.Hand,
        TextAlign = ContentAlignment.MiddleCenter
      };
      btn.FlatAppearance.BorderSize = 0;
      btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 242, 245);
      return btn;
    }

    private void LoadUserProfile()
    {
      if (currentUser != null && !string.IsNullOrEmpty(currentUser.AvatarUrl) && File.Exists(currentUser.AvatarUrl))
      {
        try
        {
          picAvatar.Image = Image.FromFile(currentUser.AvatarUrl);
        }
        catch { }
      }
    }

    private void LoadNewsfeed()
    {
      try
      {
        flpPosts.Controls.Clear();

        // Load tất cả bài viết và lưu vào biến toàn cục
        allPosts = postService.GetAllPosts()
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedAt)
            .ToList();

        DisplayPosts(allPosts);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tải newsfeed: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DisplayPosts(List<Post> posts)
    {
      if (isLoading) return;

      try
      {
        flpPosts.Controls.Clear();

        if (posts == null || posts.Count == 0)
        {
          Label lblNoPost = new Label
          {
            Text = "Không tìm thấy bài viết nào",
            Font = new Font("Segoe UI", 12F, FontStyle.Italic),
            ForeColor = Color.Gray,
            AutoSize = true,
            Padding = new Padding(20)
          };
          flpPosts.Controls.Add(lblNoPost);
        }
        else
        {
          foreach (var post in posts)
          {
            var postControl = new controls.PostControl(post);
            postControl.Width = flpPosts.ClientSize.Width - 20;
            postControl.Margin = new Padding(0, 0, 0, 15);
            postControl.PostClicked += PostControl_PostClicked;
            flpPosts.Controls.Add(postControl);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi hiển thị bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void OpenCreatePostForm()
    {
      frmPost postForm = new frmPost();
      if (postForm.ShowDialog() == DialogResult.OK)
      {
        LoadNewsfeed();
      }
    }

    private void PostControl_PostClicked(object? sender, string postId)
    {
      try
      {
        if (int.TryParse(postId, out int postIdInt))
        {
          // Tạo form để hiển thị chi tiết post
          Form detailForm = new Form
          {
            Text = "Chi tiết bài viết",
            Size = new Size(800, 700),
            StartPosition = FormStartPosition.CenterParent,
            BackColor = Color.FromArgb(240, 242, 245)
          };

          var postDetailControl = new controls.ucPostDetail(postIdInt)
          {
            Dock = DockStyle.Fill
          };

          detailForm.Controls.Add(postDetailControl);
          detailForm.ShowDialog();

          // Reload newsfeed sau khi đóng chi tiết (nếu có thay đổi)
          LoadNewsfeed();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi mở chi tiết bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void OpenProfile()
    {
      frmInfor inforForm = new frmInfor();
      inforForm.ShowDialog();
    }

    private void OpenDashboard()
    {
      frmDashboard dashboard = new frmDashboard();
      this.Hide();
      dashboard.ShowDialog();
      this.Show();
    }

    private void ShowStatistics()
    {
      try
      {
        var allPosts = postService.GetAllPosts().Where(p => !p.IsDeleted).ToList();
        var myPosts = allPosts.Where(p => p.UserID == currentUser?.UserID).ToList();

        // Tạo form hiển thị thống kê
        Form statsForm = new Form
        {
          Text = "Thống kê",
          Size = new Size(600, 500),
          StartPosition = FormStartPosition.CenterParent,
          BackColor = Color.White,
          FormBorderStyle = FormBorderStyle.FixedDialog,
          MaximizeBox = false,
          MinimizeBox = false
        };

        FlowLayoutPanel flpStats = new FlowLayoutPanel
        {
          Dock = DockStyle.Fill,
          FlowDirection = FlowDirection.TopDown,
          WrapContents = false,
          AutoScroll = true,
          Padding = new Padding(20)
        };

        Label lblTitle = new Label
        {
          Text = "Thống kê của bạn",
          Font = new Font("Segoe UI", 18F, FontStyle.Bold),
          AutoSize = true,
          ForeColor = Color.FromArgb(24, 119, 242),
          Padding = new Padding(0, 0, 0, 20)
        };

        Panel pnlPosts = CreateStatCard("Bài viết", myPosts.Count.ToString(),
            $"Tổng: {allPosts.Count} bài viết trên hệ thống");

        int totalLikes = myPosts.Sum(p => p.LikesCount);
        Panel pnlLikes = CreateStatCard("Lượt thích", totalLikes.ToString(),
            $"Trung bình: {(myPosts.Count > 0 ? (double)totalLikes / myPosts.Count : 0):F1} likes/bài");

        int totalComments = myPosts.Sum(p => p.CommentsCount);
        Panel pnlComments = CreateStatCard("Bình luận", totalComments.ToString(),
            $"Trung bình: {(myPosts.Count > 0 ? (double)totalComments / myPosts.Count : 0):F1} comments/bài");

        var reportService = new ReportService();
        int reportCount = reportService.GetReportCountForUser(currentUser?.UserID ?? Guid.Empty);
        string statusText = reportCount >= 100 ? "Tạm ngừng" :
                           reportCount >= 50 ? "Cảnh báo" : "Bình thường";
        Color statusColor = reportCount >= 100 ? Color.Red :
                           reportCount >= 50 ? Color.Orange : Color.Green;

        Panel pnlStatus = CreateStatCard("Trạng thái", statusText,
            $"Số lần bị tố cáo: {reportCount}");
        (pnlStatus.Controls[1] as Label)!.ForeColor = statusColor;

        flpStats.Controls.AddRange(new Control[] {
          lblTitle, pnlPosts, pnlLikes, pnlComments, pnlStatus
        });

        statsForm.Controls.Add(flpStats);
        statsForm.ShowDialog();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi hiển thị thống kê: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private Panel CreateStatCard(string title, string value, string description)
    {
      Panel panel = new Panel
      {
        Size = new Size(540, 100),
        BackColor = Color.FromArgb(240, 242, 245),
        Margin = new Padding(0, 0, 0, 15),
        Padding = new Padding(20)
      };

      Label lblTitle = new Label
      {
        Text = title,
        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        AutoSize = true,
        Location = new Point(20, 15),
        ForeColor = Color.FromArgb(101, 103, 107)
      };

      Label lblValue = new Label
      {
        Text = value,
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
        AutoSize = true,
        Location = new Point(20, 40),
        ForeColor = Color.FromArgb(24, 119, 242)
      };

      Label lblDesc = new Label
      {
        Text = description,
        Font = new Font("Segoe UI", 9F),
        AutoSize = true,
        Location = new Point(20, 75),
        ForeColor = Color.FromArgb(101, 103, 107)
      };

      panel.Controls.AddRange(new Control[] { lblTitle, lblValue, lblDesc });
      return panel;
    }

    private void OpenSettings()
    {
      // Tạo form settings đơn giản
      Form settingsForm = new Form
      {
        Text = "Cài đặt",
        Size = new Size(500, 400),
        StartPosition = FormStartPosition.CenterParent,
        BackColor = Color.White,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        MaximizeBox = false,
        MinimizeBox = false
      };

      Panel pnlMain = new Panel
      {
        Dock = DockStyle.Fill,
        Padding = new Padding(30)
      };

      Label lblTitle = new Label
      {
        Text = "Cài đặt",
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
        AutoSize = true,
        Location = new Point(30, 30),
        ForeColor = Color.FromArgb(24, 119, 242)
      };

      Button btnEditInfo = new Button
      {
        Text = "Chỉnh sửa thông tin cá nhân",
        Font = new Font("Segoe UI", 11F),
        Size = new Size(420, 50),
        Location = new Point(30, 80),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.FromArgb(24, 119, 242),
        ForeColor = Color.White,
        Cursor = Cursors.Hand
      };
      btnEditInfo.FlatAppearance.BorderSize = 0;
      btnEditInfo.Click += (s, e) => { settingsForm.Close(); OpenProfile(); };

      Button btnChangePass = new Button
      {
        Text = "Thay đổi mật khẩu",
        Font = new Font("Segoe UI", 11F),
        Size = new Size(420, 50),
        Location = new Point(30, 145),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.FromArgb(240, 242, 245),
        ForeColor = Color.FromArgb(5, 5, 5),
        Cursor = Cursors.Hand
      };
      btnChangePass.FlatAppearance.BorderSize = 0;
      btnChangePass.Click += (s, e) => {
        settingsForm.Close();
        frmForgotPass forgotForm = new frmForgotPass();
        forgotForm.ShowDialog();
      };

      Button btnLogoutSettings = new Button
      {
        Text = "Đăng xuất",
        Font = new Font("Segoe UI", 11F),
        Size = new Size(420, 50),
        Location = new Point(30, 210),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.FromArgb(220, 53, 69),
        ForeColor = Color.White,
        Cursor = Cursors.Hand
      };
      btnLogoutSettings.FlatAppearance.BorderSize = 0;
      btnLogoutSettings.Click += (s, e) => { settingsForm.Close(); Logout(); };

      pnlMain.Controls.AddRange(new Control[] {
        lblTitle, btnEditInfo, btnChangePass, btnLogoutSettings
      });
      settingsForm.Controls.Add(pnlMain);
      settingsForm.ShowDialog();
    }

    private void PicAvatar_Click(object? sender, EventArgs e)
    {
      // Hiển thị menu dropdown
      ContextMenuStrip menu = new ContextMenuStrip();

      ToolStripMenuItem itemProfile = new ToolStripMenuItem((currentUser?.FullName ?? "User"));
      itemProfile.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      itemProfile.Click += (s, ev) => OpenProfile();

      ToolStripSeparator separator1 = new ToolStripSeparator();

      ToolStripMenuItem itemDashboard = new ToolStripMenuItem("Dashboard");
      itemDashboard.Click += (s, ev) => OpenDashboard();

      ToolStripMenuItem itemSettings = new ToolStripMenuItem("Cài đặt");
      itemSettings.Click += (s, ev) => OpenProfile();

      ToolStripSeparator separator2 = new ToolStripSeparator();

      ToolStripMenuItem itemLogout = new ToolStripMenuItem("Đăng xuất");
      itemLogout.Click += (s, ev) => Logout();

      menu.Items.AddRange(new ToolStripItem[] {
        itemProfile, separator1, itemDashboard, itemSettings, separator2, itemLogout
      });

      menu.Show(picAvatar, new Point(0, picAvatar.Height));
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
      // Chỉ lọc nếu đã load xong và không đang trong quá trình load
      if (!isLoading && allPosts != null && allPosts.Count > 0)
      {
        ApplyFilters();
      }
    }

    // ===== FILTER METHODS =====

    private Panel CreateFilterSection()
    {
      Panel panelContainer = new Panel
      {
        BackColor = Color.White,
        Padding = new Padding(15),
        AutoScroll = true
      };

      gbFilter = new GroupBox
      {
        Text = "Bộ lọc nâng cao",
        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(10, 10),
        Size = new Size(320, 500),
        Padding = new Padding(10)
      };

      // ID bài viết
      Label lblPostId = new Label
      {
        Text = "ID bài viết:",
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 30),
        AutoSize = true
      };

      txtPostId = new TextBox
      {
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 50),
        Size = new Size(280, 25),
        PlaceholderText = "Nhập ID bài viết"
      };
      UIHelper.ApplyRoundedCorners(txtPostId, 8);

      // Loại bài viết
      Label lblType = new Label
      {
        Text = "Loại bài viết:",
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 85),
        AutoSize = true
      };

      cboType = new ComboBox
      {
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 105),
        Size = new Size(280, 25),
        DropDownStyle = ComboBoxStyle.DropDownList
      };
      cboType.Items.AddRange(new object[] { "Tất cả", "Status", "Image", "Video" });
      cboType.SelectedIndex = 0;

      // Từ ngày
      Label lblFromDate = new Label
      {
        Text = "Từ ngày:",
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 140),
        AutoSize = true
      };

      dtpFrom = new DateTimePicker
      {
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 160),
        Size = new Size(280, 25),
        Format = DateTimePickerFormat.Short
      };

      // Đến ngày
      Label lblToDate = new Label
      {
        Text = "Đến ngày:",
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 195),
        AutoSize = true
      };

      dtpTo = new DateTimePicker
      {
        Font = new Font("Segoe UI", 9F),
        Location = new Point(15, 215),
        Size = new Size(280, 25),
        Format = DateTimePickerFormat.Short
      };

      // Buttons
      btnApplyFilter = new Button
      {
        Text = "Áp dụng lọc",
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        Location = new Point(15, 260),
        Size = new Size(280, 38),
        BackColor = Color.FromArgb(24, 119, 242),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand
      };
      btnApplyFilter.FlatAppearance.BorderSize = 0;
      btnApplyFilter.Click += BtnApplyFilter_Click;
      UIHelper.ApplyRoundedCorners(btnApplyFilter, 10);

      btnClearFilter = new Button
      {
        Text = "Xóa bộ lọc",
        Font = new Font("Segoe UI", 10F),
        Location = new Point(15, 308),
        Size = new Size(280, 38),
        BackColor = Color.FromArgb(228, 230, 235),
        ForeColor = Color.FromArgb(33, 33, 33),
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand
      };
      btnClearFilter.FlatAppearance.BorderSize = 0;
      btnClearFilter.Click += BtnClearFilter_Click;
      UIHelper.ApplyRoundedCorners(btnClearFilter, 10);
      btnClearFilter.Click += BtnClearFilter_Click;

      gbFilter.Controls.AddRange(new Control[] {
        lblPostId, txtPostId, lblType, cboType,
        lblFromDate, dtpFrom, lblToDate, dtpTo,
        btnApplyFilter, btnClearFilter
      });

      panelContainer.Controls.Add(gbFilter);
      return panelContainer;
    }

    private void InitializeFilter()
    {
      isLoading = true;

      try
      {
        if (cboType != null)
        {
          cboType.SelectedIndex = 0;
        }

        if (dtpFrom != null)
        {
          dtpFrom.MinDate = new DateTime(2000, 1, 1);
          dtpFrom.MaxDate = DateTime.Now.AddYears(1);
          dtpFrom.Value = DateTime.Now.AddMonths(-1);
        }

        if (dtpTo != null)
        {
          dtpTo.MinDate = new DateTime(2000, 1, 1);
          dtpTo.MaxDate = DateTime.Now.AddYears(1);
          dtpTo.Value = DateTime.Now;
        }

        if (txtPostId != null)
        {
          txtPostId.Text = "";
        }
      }
      finally
      {
        isLoading = false;
      }
    }

    private void BtnApplyFilter_Click(object? sender, EventArgs e)
    {
      if (!isLoading)
      {
        ApplyFilters();
      }
    }

    private void BtnClearFilter_Click(object? sender, EventArgs e)
    {
      if (isLoading) return;

      isLoading = true;

      try
      {
        if (txtSearch != null) txtSearch.Text = "";
        if (cboType != null) cboType.SelectedIndex = 0;
        if (dtpFrom != null) dtpFrom.Value = DateTime.Now.AddMonths(-1);
        if (dtpTo != null) dtpTo.Value = DateTime.Now;
        if (txtPostId != null) txtPostId.Text = "";

        if (allPosts != null)
          DisplayPosts(allPosts);
      }
      finally
      {
        isLoading = false;
      }
    }

    private void ApplyFilters()
    {
      if (isLoading || allPosts == null) return;

      try
      {
        var filteredPosts = new List<Post>(allPosts);

        // 1. Lọc theo tìm kiếm text (tìm trong nội dung và tên người đăng)
        if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
        {
          string searchText = txtSearch.Text.ToLower();
          filteredPosts = filteredPosts.Where(p =>
            p.Content.ToLower().Contains(searchText) ||
            userService.GetUserById(p.UserID)?.FullName.ToLower().Contains(searchText) == true
          ).ToList();
        }

        // 2. Lọc theo ID bài viết
        if (txtPostId != null && !string.IsNullOrWhiteSpace(txtPostId.Text))
        {
          if (int.TryParse(txtPostId.Text, out int postId))
          {
            filteredPosts = filteredPosts.Where(p => p.PostID == postId).ToList();
          }
        }

        // 3. Lọc theo loại bài viết (Type)
        if (cboType != null && cboType.SelectedIndex > 0)
        {
          string selectedType = cboType.SelectedItem?.ToString() ?? "";

          if (selectedType == "Status")
          {
            filteredPosts = filteredPosts.Where(p => string.IsNullOrWhiteSpace(p.MediaUrl)).ToList();
          }
          else if (selectedType == "Image")
          {
            filteredPosts = filteredPosts.Where(p =>
              !string.IsNullOrWhiteSpace(p.MediaUrl) &&
              (p.MediaUrl.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
               p.MediaUrl.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
               p.MediaUrl.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
               p.MediaUrl.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            ).ToList();
          }
          else if (selectedType == "Video")
          {
            filteredPosts = filteredPosts.Where(p =>
              !string.IsNullOrWhiteSpace(p.MediaUrl) &&
              (p.MediaUrl.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase) ||
               p.MediaUrl.EndsWith(".avi", StringComparison.OrdinalIgnoreCase) ||
               p.MediaUrl.EndsWith(".mov", StringComparison.OrdinalIgnoreCase))
            ).ToList();
          }
        }

        // 4. Lọc theo khoảng thời gian
        if (dtpFrom != null && dtpTo != null)
        {
          DateTime fromDate = dtpFrom.Value.Date;
          DateTime toDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1);

          filteredPosts = filteredPosts.Where(p =>
            p.CreatedAt >= fromDate && p.CreatedAt <= toDate
          ).ToList();
        }

        // Hiển thị kết quả đã lọc
        DisplayPosts(filteredPosts);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi lọc bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Logout()
    {
      var result = MessageBox.Show("Bạn có chắc muốn đăng xuất?", "Xác nhận",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question);

      if (result == DialogResult.Yes)
      {
        AuthSessionService.Logout();

        frmLogin loginForm = new frmLogin();
        loginForm.Show();

        this.Close();
      }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
      base.OnFormClosing(e);
      Application.Exit();
    }
  }
}
