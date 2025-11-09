using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.utils;

namespace SocialManager.frm.UserControls
{
  public partial class ucNewsfeed : UserControl
  {
    private User? currentUser;
    private PostService postService;
    private UserService userService;
    private Panel pnlHeader = null!;
    private Panel pnlMainContent = null!;
    private FlowLayoutPanel flpPosts = null!;
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

    public ucNewsfeed()
    {
      InitializeComponent();
      InitializeCustomComponents();

      postService = new PostService();
      userService = new UserService();
      currentUser = AuthSessionService.CurrentUser;

      if (currentUser == null)
      {
        MessageBox.Show("Vui lòng đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      LoadNewsfeed();
      InitializeFilter();
    }

    private void InitializeCustomComponents()
    {
      this.BackColor = Color.FromArgb(240, 242, 245);
      this.Dock = DockStyle.Fill;
      this.AutoScroll = true;

      // ============ HEADER PANEL ============
      pnlHeader = new Panel
      {
        Dock = DockStyle.Top,
        Height = 60,
        BackColor = Color.White,
        BorderStyle = BorderStyle.None
      };

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
      txtSearch.Location = new Point((this.Width - txtSearch.Width) / 2, 13);

      pnlHeader.Controls.Add(txtSearch);

      // Thêm sự kiện resize để căn giữa search box
      pnlHeader.Resize += (s, e) =>
      {
        if (txtSearch != null)
        {
          txtSearch.Location = new Point((pnlHeader.Width - txtSearch.Width) / 2, 13);
        }
      };

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

      // ============ ADD ALL PANELS TO USERCONTROL ============
      this.Controls.Add(pnlMainContent);
      this.Controls.Add(pnlFilterSection); // Phải add filter trước để nó ở bên phải
      this.Controls.Add(pnlHeader);
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
            postControl.PostReported += PostControl_PostReported; // Lắng nghe event tố cáo
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

    private void PostControl_PostReported(object? sender, EventArgs e)
    {
      // Reload lại danh sách bài viết sau khi tố cáo
      LoadNewsfeed();
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

    // Public method để refresh newsfeed từ bên ngoài
    public void RefreshNewsfeed()
    {
      LoadNewsfeed();
    }
  }
}
