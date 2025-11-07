using SocialManager.services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using SocialManager.controls;
using System.ComponentModel;
using System.Diagnostics;

namespace SocialManager.frm
{
  public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
  {
    public DoubleBufferedFlowLayoutPanel()
    {
      this.DoubleBuffered = true;
    }
  }

  public partial class frmDashboard : Form
  {
    private PostService? postService;
    private User? currentUser;
    private List<Post>? allUserPosts; // Lưu tất cả bài viết để lọc
    private bool isLoading = true; // Cờ để tránh trigger event khi đang load
    private bool isDarkMode = false; // Chế độ giao diện
    private const string DarkModeKey = "isDarkMode"; // Registry key for dark mode

    public frmDashboard()
    {
      InitializeComponent();

      // If running inside the designer, skip runtime initialization to avoid crashes
      if (this.DesignMode || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
      {
        return;
      }

      // Khởi tạo service
      postService = new PostService();
      currentUser = AuthSessionService.CurrentUser;
      allUserPosts = new List<Post>();

      // Kiểm tra user đã đăng nhập chưa
      if (currentUser == null)
      {
        // Tránh messagebox/Close() khi không có context đăng nhập trong runtime non-designer
        return;
      }

      // XÓA picAvatar vì không còn dùng
      this.picProfileLarge.Paint += new PaintEventHandler(picProfileLarge_Paint);
      this.picComposerAvatar.Paint += new PaintEventHandler(picComposerAvatar_Paint);
      this.btnThemeToggle.Paint += new PaintEventHandler(btnThemeToggle_Paint); // Vẽ hình tròn
      this.btnNewPost.Paint += new PaintEventHandler(btnNewPost_Paint); // Vẽ hình tròn cho nút tạo bài viết

      // Add hover effects for modern buttons
      AddButtonHoverEffect(this.btnNewPost, Color.FromArgb(10, 102, 194), Color.FromArgb(8, 82, 155));
      AddButtonHoverEffect(this.btnLogout, Color.FromArgb(228, 230, 235), Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnSettings, Color.FromArgb(228, 230, 235), Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnThemeToggle, Color.FromArgb(228, 230, 235), Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnEditProfile, Color.FromArgb(228, 230, 235), Color.FromArgb(208, 210, 215));

      // Add rounded corners to modern buttons - KHÔNG thêm cho btnNewPost vì nó là hình tròn
      AddRoundedButton(this.btnLogout, 6);
      AddRoundedButton(this.btnSettings, 6);
      AddRoundedButton(this.btnEditProfile, 6);
      this.flowLayoutPanelPosts.Resize += new EventHandler(flowLayoutPanelPosts_Resize);

      // Load dữ liệu user
      LoadUserInfo();

      // Load bài viết
      LoadPosts();

      // Khởi tạo filter
      InitializeFilter();

      // Filter events - Đăng ký SAU KHI đã load xong
      this.txtSearch.TextChanged += new EventHandler(TxtSearch_TextChanged);
      this.btnApplyFilter.Click += new EventHandler(BtnApplyFilter_Click);
      this.btnClearFilter.Click += new EventHandler(BtnClearFilter_Click);

      // Load dark mode preference
      isDarkMode = LoadDarkModePreference();

      // Áp dụng giao diện mặc định (sáng)
      ApplyTheme(isDarkMode);

      // Đánh dấu hoàn tất load
      isLoading = false;
    }

    // Event handler khi form load - điều chỉnh vị trí các nút ở góc dưới trái
    private void frmDashboard_Load(object? sender, EventArgs e)
    {
      // Đặt vị trí btnThemeToggle ở góc dưới TRÁI và btnNewPost ở góc dưới PHẢI
      int bottomMargin = 20;
      btnThemeToggle.Location = new Point(20, this.ClientSize.Height - btnThemeToggle.Height - bottomMargin);
      btnNewPost.Location = new Point(this.ClientSize.Width - btnNewPost.Width - 20, this.ClientSize.Height - btnNewPost.Height - bottomMargin);

      // Đưa các nút lên trên cùng (BringToFront) để không bị che bởi các control khác
      btnThemeToggle.BringToFront();
      btnNewPost.BringToFront();
    }

    // Vẽ btnNewPost thành hình tròn (giống btnThemeToggle)
    private void btnNewPost_Paint(object? sender, PaintEventArgs e)
    {
      if (sender is Button btn)
      {
        try
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

          // Vẽ hình tròn
          using (GraphicsPath path = new GraphicsPath())
          {
            path.AddEllipse(0, 0, btn.Width - 1, btn.Height - 1);
            btn.Region = new Region(path);

            // Fill background
            using (SolidBrush brush = new SolidBrush(btn.BackColor))
            {
              e.Graphics.FillEllipse(brush, 0, 0, btn.Width - 1, btn.Height - 1);
            }

            // Draw text (icon)
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, new Rectangle(0, 0, btn.Width, btn.Height),
              btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine($"btnNewPost_Paint error: {ex.Message}");
        }
      }
    }    // Vẽ card trắng bo nhẹ + bóng mờ nhẹ giống Facebook
    private void PanelCard_Paint(object sender, PaintEventArgs e)
    {
      if (sender is Panel pnl)
      {
        try
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
          var rect = new Rectangle(0, 0, pnl.Width - 1, pnl.Height - 1);

          // Shadow
          using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
          {
            e.Graphics.FillRectangle(shadowBrush, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height));
          }

          // Card
          var cardColor = isDarkMode ? Color.FromArgb(36, 37, 38) : Color.White;
          var borderColor = isDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(230, 230, 230);
          using (var cardBrush = new SolidBrush(cardColor))
          using (var borderPen = new Pen(borderColor))
          {
            e.Graphics.FillRectangle(cardBrush, rect);
            e.Graphics.DrawRectangle(borderPen, rect);
          }
        }
        catch (Exception ex)
        {
          System.Diagnostics.Debug.WriteLine($"PanelCard_Paint error: {ex.Message}");
        }
      }
    }

    // Nút cài đặt (gear) mở frmInfor
    private void btnSettings_Click(object sender, EventArgs e)
    {
      lblUsername_Click(sender, e); // Tái sử dụng luồng hiện tại
    }

    // Nút quay về Newsfeed
    private void btnNewsfeed_Click(object sender, EventArgs e)
    {
      this.Close(); // Đóng Dashboard để quay về Newsfeed
    }

    // Nút chuyển chế độ sáng/tối
    private void btnThemeToggle_Click(object sender, EventArgs e)
    {
      isDarkMode = !isDarkMode;
      ApplyTheme(isDarkMode);
      SaveDarkModePreference(isDarkMode);

      // Cập nhật text nút - CHỈ HIỂN THỊ BIỂU TƯỢNG
      if (this.btnThemeToggle != null)
      {
        this.btnThemeToggle.Text = isDarkMode ? "☀" : "◐";
        this.btnThemeToggle.Invalidate(); // Vẽ lại nút
      }
    }

    // Load dark mode from user settings
    private bool LoadDarkModePreference()
    {
      try
      {
        if (currentUser == null) return false;

        string settingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas", $"settings_{currentUser.UserID}.txt");
        if (File.Exists(settingsFile))
        {
          string content = File.ReadAllText(settingsFile);
          return content.Contains("dark=true");
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"LoadDarkMode error: {ex.Message}");
      }
      return false;
    }

    // Save dark mode to user settings
    private void SaveDarkModePreference(bool dark)
    {
      try
      {
        if (currentUser == null) return;

        string dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datas");
        if (!Directory.Exists(dataDir))
          Directory.CreateDirectory(dataDir);

        string settingsFile = Path.Combine(dataDir, $"settings_{currentUser.UserID}.txt");
        string content = $"dark={dark}";
        File.WriteAllText(settingsFile, content);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"SaveDarkMode error: {ex.Message}");
      }
    }

    private void ApplyTheme(bool dark)
    {
      // Màu nền tổng
      this.BackColor = dark ? Color.FromArgb(24, 25, 26) : Color.FromArgb(240, 242, 245);

      // Header & profile area
      panelHeader.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;
      panelProfileHeader.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;
      panelProfileInfo.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;
      panelTabs.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;

      // Content area
      panelContent.BackColor = this.BackColor;
      panelCenterCol.BackColor = this.BackColor;
      panelLeftCol.BackColor = this.BackColor;
      gbFilter.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;
      panelFilter.BackColor = Color.Transparent; // Card paint sẽ phủ
      panelComposer.BackColor = Color.Transparent; // Card paint sẽ phủ
      flowLayoutPanelPosts.BackColor = this.BackColor;

      // Text màu
      var primaryText = dark ? Color.FromArgb(228, 230, 235) : Color.FromArgb(33, 33, 33);
      var secondaryText = dark ? Color.FromArgb(176, 179, 184) : Color.FromArgb(102, 102, 102);

      // XÓA lblUsername vì không còn dùng
      lblProfileName.ForeColor = primaryText;
      lblFollowerStats.ForeColor = secondaryText;
      lblComposerPlaceholder.ForeColor = secondaryText;

      lblType.ForeColor = primaryText;
      lblFromDate.ForeColor = primaryText;
      lblToDate.ForeColor = primaryText;
      lblPostId.ForeColor = primaryText;

      // Input controls
      txtSearch.ForeColor = dark ? Color.White : Color.FromArgb(64, 64, 64);
      txtPostId.ForeColor = dark ? Color.White : Color.FromArgb(64, 64, 64);
      cboType.ForeColor = primaryText;
      cboType.BackColor = dark ? Color.FromArgb(58, 59, 60) : Color.White;
      dtpFrom.CalendarForeColor = primaryText;
      dtpTo.CalendarForeColor = primaryText;

      // Buttons
      var buttonBg = dark ? Color.FromArgb(58, 59, 60) : Color.FromArgb(228, 230, 235);
      var buttonFg = dark ? Color.FromArgb(228, 230, 235) : Color.FromArgb(33, 33, 33);

      btnLogout.BackColor = buttonBg;
      btnLogout.ForeColor = buttonFg;
      btnSettings.BackColor = buttonBg;
      btnSettings.ForeColor = buttonFg;
      btnThemeToggle.BackColor = buttonBg;
      btnThemeToggle.ForeColor = buttonFg;
      btnEditProfile.BackColor = buttonBg;
      btnEditProfile.ForeColor = buttonFg;
      btnNewPost.BackColor = Color.FromArgb(10, 102, 194);

      // Update hover colors
      AddButtonHoverEffect(this.btnLogout, buttonBg, dark ? Color.FromArgb(68, 69, 70) : Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnSettings, buttonBg, dark ? Color.FromArgb(68, 69, 70) : Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnThemeToggle, buttonBg, dark ? Color.FromArgb(68, 69, 70) : Color.FromArgb(208, 210, 215));
      AddButtonHoverEffect(this.btnEditProfile, buttonBg, dark ? Color.FromArgb(68, 69, 70) : Color.FromArgb(208, 210, 215));

      // Update tab colors
      foreach (Control ctrl in panelTabs.Controls)
      {
        if (ctrl is Button btn)
        {
          // Keep active tab color blue, inactive tabs gray
          if (btn.Font.Bold)
          {
            btn.ForeColor = Color.FromArgb(10, 102, 194);
          }
          else
          {
            btn.ForeColor = dark ? Color.FromArgb(176, 179, 184) : Color.FromArgb(66, 66, 66);
          }
        }
      }

      // Trigger repaint for cards
      panelComposer.Invalidate();
      panelFilter.Invalidate();

      // Update underline color
      panelTabUnderline.BackColor = Color.FromArgb(10, 102, 194);
    }

    // Helper: Add rounded corners to button
    private void AddRoundedButton(Button btn, int radius)
    {
      // Set region once on Resize event instead of Paint to avoid flicker
      btn.Resize += (s, e) =>
      {
        if (s is Button b && b.Width > 0 && b.Height > 0)
        {
          var rect = new Rectangle(0, 0, b.Width, b.Height);
          using (var path = GetRoundedRectPath(rect, radius))
          {
            b.Region = new Region(path);
          }
        }
      };

      // Trigger initial resize
      if (btn.Width > 0 && btn.Height > 0)
      {
        var rect = new Rectangle(0, 0, btn.Width, btn.Height);
        using (var path = GetRoundedRectPath(rect, radius))
        {
          btn.Region = new Region(path);
        }
      }
    }

    // Helper: Create rounded rect path
    private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
      var path = new GraphicsPath();
      int diameter = radius * 2;
      var arc = new Rectangle(rect.Location, new Size(diameter, diameter));

      // Top-left
      path.AddArc(arc, 180, 90);
      // Top-right
      arc.X = rect.Right - diameter;
      path.AddArc(arc, 270, 90);
      // Bottom-right
      arc.Y = rect.Bottom - diameter;
      path.AddArc(arc, 0, 90);
      // Bottom-left
      arc.X = rect.Left;
      path.AddArc(arc, 90, 90);

      path.CloseFigure();
      return path;
    }

    // Helper: Add hover effect to button
    private void AddButtonHoverEffect(Button btn, Color normalColor, Color hoverColor)
    {
      btn.MouseEnter += (s, e) => { btn.BackColor = hoverColor; };
      btn.MouseLeave += (s, e) => { btn.BackColor = normalColor; };
    }

    // Composer mở form tạo bài viết (giống nút Tạo bài viết)
    private void panelComposer_Click(object sender, EventArgs e)
    {
      btnNewPost_Click(sender!, e);
    }

    // Handler cho các tab buttons - di chuyển underline và set active
    private void TabButton_Click(object sender, EventArgs e)
    {
      if (sender is Button clickedTab)
      {
        // Reset tất cả tabs về style bình thường
        foreach (Control ctrl in panelTabs.Controls)
        {
          if (ctrl is Button btn)
          {
            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.ForeColor = Color.FromArgb(66, 66, 66);
          }
        }

        // Set tab được click thành active
        clickedTab.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
        clickedTab.ForeColor = Color.FromArgb(10, 102, 194);

        // Di chuyển underline
        panelTabUnderline.Left = clickedTab.Left;
        panelTabUnderline.Width = clickedTab.Width;

        // TODO: Load content tương ứng với tab
        // Hiện tại chỉ tab "Bài viết" có nội dung (posts feed)
      }
    }

    private void LoadUserInfo()
    {
      if (currentUser != null)
      {
        // Hiển thị tên đầy đủ và username ở profile
        lblProfileName.Text = string.IsNullOrWhiteSpace(currentUser.FullName) ? currentUser.UserName : currentUser.FullName;
        lblFollowerStats.Text = $"@{currentUser.UserName}"; // Hiển thị username thay vì follower stats

        // Hiển thị thông tin cá nhân bên trái
        lblDOBValue.Text = currentUser.DOB.ToString("dd/MM/yyyy");
        lblEmailValue.Text = string.IsNullOrWhiteSpace(currentUser.Email) ? "Chưa cập nhật" : currentUser.Email;
        lblPhoneValue.Text = string.IsNullOrWhiteSpace(currentUser.Phone) ? "Chưa cập nhật" : currentUser.Phone;

        // Lấy tỉnh/thành phố từ địa chỉ (lấy phần cuối sau dấu phẩy)
        string city = "Chưa cập nhật";
        if (!string.IsNullOrWhiteSpace(currentUser.Address))
        {
          var parts = currentUser.Address.Split(',');
          if (parts.Length > 0)
          {
            city = parts[parts.Length - 1].Trim();
          }
        }
        lblCityValue.Text = city;

        // Load avatar nếu có
        LoadAvatar();
      }
    }

    private void LoadAvatar()
    {
      if (currentUser != null && !string.IsNullOrEmpty(currentUser.AvatarUrl) && System.IO.File.Exists(currentUser.AvatarUrl))
      {
        try
        {
          var img = System.Drawing.Image.FromFile(currentUser.AvatarUrl);

          // Avatar lớn ở profile
          picProfileLarge.Image = img;
          picProfileLarge.SizeMode = PictureBoxSizeMode.Zoom;

          // Avatar ở composer
          if (picComposerAvatar != null)
          {
            picComposerAvatar.Image = img;
            picComposerAvatar.SizeMode = PictureBoxSizeMode.Zoom;
          }
        }
        catch
        {
          // Nếu load lỗi, để avatar mặc định
          picProfileLarge.BackColor = System.Drawing.Color.LightGray;
          if (picComposerAvatar != null)
            picComposerAvatar.BackColor = System.Drawing.Color.LightGray;
        }
      }
      else
      {
        // Avatar mặc định
        picProfileLarge.BackColor = System.Drawing.Color.LightGray;
        if (picComposerAvatar != null)
          picComposerAvatar.BackColor = System.Drawing.Color.LightGray;
      }
    }

    // XÓA picAvatar_Paint vì không còn dùng
    private void picProfileLarge_Paint(object? sender, PaintEventArgs e) => MakeControlCircular(sender, e);
    private void picComposerAvatar_Paint(object? sender, PaintEventArgs e) => MakeControlCircular(sender, e);

    // Vẽ nút theme toggle hình tròn
    private void btnThemeToggle_Paint(object? sender, PaintEventArgs e)
    {
      if (sender is Button btn)
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Tạo hình tròn
        using (GraphicsPath path = new GraphicsPath())
        {
          path.AddEllipse(0, 0, btn.Width - 1, btn.Height - 1);
          btn.Region = new Region(path);

          // Vẽ background tròn
          using (SolidBrush brush = new SolidBrush(btn.BackColor))
          {
            e.Graphics.FillEllipse(brush, 0, 0, btn.Width - 1, btn.Height - 1);
          }

          // Vẽ text ở giữa
          StringFormat sf = new StringFormat
          {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
          };

          using (SolidBrush textBrush = new SolidBrush(btn.ForeColor))
          {
            e.Graphics.DrawString(btn.Text, btn.Font, textBrush,
              new RectangleF(0, 0, btn.Width, btn.Height), sf);
          }
        }
      }
    }

    private void MakeControlCircular(object? sender, PaintEventArgs e)
    {
      if (sender is Control control)
      {
        GraphicsPath path = new GraphicsPath();
        path.AddEllipse(0, 0, control.Width - 1, control.Height - 1);
        control.Region = new Region(path);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
      }
    }

    private List<Post> GetPostsForCurrentUser()
    {
      if (currentUser == null || postService == null)
        return new List<Post>();

      try
      {
        // Lấy bài viết từ PostService (đọc từ CSV)
        var allPosts = postService.GetUserPosts(currentUser.UserID);

        // Trả về danh sách (có thể rỗng)
        return allPosts ?? new List<Post>();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi load bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);

        // Trả về danh sách rỗng nếu có lỗi
        return new List<Post>();
      }
    }

    private void LoadPosts()
    {
      try
      {
        flowLayoutPanelPosts.Controls.Clear();

        // Load tất cả bài viết của user hiện tại
        allUserPosts = GetPostsForCurrentUser();
        List<Post> userPosts = allUserPosts;

        if (userPosts == null || userPosts.Count == 0)
        {
          // Hiển thị thông báo nếu không có bài viết
          Label lblNoPost = new Label
          {
            Text = "Chưa có bài viết nào.\nNhấn nút '+' để tạo bài viết đầu tiên!",
            Font = new System.Drawing.Font("Segoe UI", 12F),
            ForeColor = System.Drawing.Color.Gray,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            AutoSize = false,
            Width = flowLayoutPanelPosts.ClientSize.Width - 40,
            Height = 100,
            Padding = new Padding(20)
          };
          flowLayoutPanelPosts.Controls.Add(lblNoPost);
        }
        else
        {
          foreach (var postData in userPosts)
          {
            var postControl = new PostControl(postData);
            postControl.PostClicked += PostControl_PostClicked;
            flowLayoutPanelPosts.Controls.Add(postControl);
          }
        }

        flowLayoutPanelPosts_Resize(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi load bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void PostControl_PostClicked(object? sender, string postId)
    {
      try
      {
        // Parse postId to int
        int postIdInt = int.Parse(postId);

        // Tạo form wrapper
        Form detailForm = new Form
        {
          Text = "Chi tiết bài viết",
          Size = new Size(720, 650),
          StartPosition = FormStartPosition.CenterScreen,
          FormBorderStyle = FormBorderStyle.FixedDialog,
          MaximizeBox = false,
          MinimizeBox = false
        };

        // Tạo UserControl
        var postDetailControl = new controls.ucPostDetail(postIdInt)
        {
          Dock = DockStyle.Fill
        };

        // Xử lý sự kiện đóng
        postDetailControl.CloseRequested += (s, e) =>
        {
          detailForm.Close();
        };

        detailForm.Controls.Add(postDetailControl);
        detailForm.ShowDialog();

        // Reload lại posts sau khi đóng form để cập nhật số lượng likes/comments
        LoadPosts();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi mở chi tiết bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void flowLayoutPanelPosts_Resize(object? sender, EventArgs e)
    {
      foreach (Control c in flowLayoutPanelPosts.Controls)
      {
        c.Width = flowLayoutPanelPosts.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
      }

      // XÓA lblUsername vì không còn dùng - cập nhật profile name thay vì
      if (currentUser != null)
      {
        lblProfileName.Text = currentUser.UserName;
      }
    }

    private void lblUsername_Click(object sender, EventArgs e)
    {
      frmInfor inforForm = new frmInfor();
      DialogResult result = inforForm.ShowDialog();

      // Nếu user đã đổi mật khẩu thành công (đã logout), đóng Dashboard
      if (result == DialogResult.OK && !AuthSessionService.IsLoggedIn)
      {
        this.Close();
      }
      else
      {
        // Reload lại thông tin user từ AuthSessionService
        currentUser = AuthSessionService.CurrentUser;

        // Cập nhật UI
        LoadUserInfo();
      }
    }

    private void btnNewPost_Click(object sender, EventArgs e)
    {
      frmPost frmPost = new frmPost();

      // Nếu tạo bài viết thành công, reload lại danh sách
      if (frmPost.ShowDialog() == DialogResult.OK)
      {
        LoadPosts(); // Refresh danh sách bài viết
      }
    }

    private void btnLogout_Click(object? sender, EventArgs e)
    {
      try
      {
        // Hỏi xác nhận đăng xuất
        DialogResult result = MessageBox.Show(
          "Bạn có chắc chắn muốn đăng xuất?",
          "Xác nhận đăng xuất",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Question);

        if (result == DialogResult.Yes)
        {
          // Đăng xuất
          AuthSessionService.Logout();

          // Đóng Dashboard (frmLogin sẽ tự động hiện lại)
          this.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    // ===== FILTER METHODS =====

    private void InitializeFilter()
    {
      // Đánh dấu đang load để tránh trigger event
      isLoading = true;

      try
      {
        // Khởi tạo giá trị mặc định cho filter
        if (cboType != null)
        {
          cboType.SelectedIndex = 0; // "Tất cả"
        }

        if (dtpFrom != null)
        {
          // Set MinDate/MaxDate trước để tránh lỗi validation
          dtpFrom.MinDate = new DateTime(2000, 1, 1);
          dtpFrom.MaxDate = DateTime.Now.AddYears(1);
          dtpFrom.Value = DateTime.Now.AddMonths(-1); // Mặc định 1 tháng trước
        }

        if (dtpTo != null)
        {
          // Set MinDate/MaxDate trước để tránh lỗi validation
          dtpTo.MinDate = new DateTime(2000, 1, 1);
          dtpTo.MaxDate = DateTime.Now.AddYears(1);
          dtpTo.Value = DateTime.Now;
        }

        if (txtSearch != null)
        {
          txtSearch.Text = "";
        }

        if (txtPostId != null)
        {
          txtPostId.Text = "";
        }
      }
      finally
      {
        // Reset cờ loading trong finally để đảm bảo luôn được reset
        isLoading = false;
      }
    }

    private void TxtSearch_TextChanged(object? sender, EventArgs e)
    {
      // Chỉ lọc nếu đã load xong và không đang trong quá trình load
      if (!isLoading && allUserPosts != null && allUserPosts.Count > 0)
      {
        ApplyFilters();
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

      // Đặt cờ loading để tránh trigger TextChanged
      isLoading = true;

      try
      {
        // Xóa tất cả filter
        if (txtSearch != null) txtSearch.Text = "";
        if (cboType != null) cboType.SelectedIndex = 0;
        if (dtpFrom != null) dtpFrom.Value = DateTime.Now.AddMonths(-1);
        if (dtpTo != null) dtpTo.Value = DateTime.Now;
        if (txtPostId != null) txtPostId.Text = "";

        // Load lại tất cả bài viết
        if (allUserPosts != null)
          DisplayPosts(allUserPosts);
      }
      finally
      {
        isLoading = false;
      }
    }

    private void ApplyFilters()
    {
      if (isLoading || allUserPosts == null) return;

      try
      {
        // Bắt đầu từ tất cả bài viết
        var filteredPosts = new List<Post>(allUserPosts);

        // 1. Lọc theo tìm kiếm text
        if (txtSearch != null && !string.IsNullOrWhiteSpace(txtSearch.Text))
        {
          string searchText = txtSearch.Text.ToLower();
          filteredPosts = filteredPosts.Where(p =>
            p.Content.ToLower().Contains(searchText)
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
        if (cboType != null && cboType.SelectedIndex > 0) // Không phải "Tất cả"
        {
          string selectedType = cboType.SelectedItem?.ToString() ?? "";

          if (selectedType == "Status")
          {
            // Bài viết không có media
            filteredPosts = filteredPosts.Where(p => string.IsNullOrWhiteSpace(p.MediaUrl)).ToList();
          }
          else if (selectedType == "Image")
          {
            // Bài viết có hình ảnh
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
            // Bài viết có video
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
          DateTime toDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1); // Đến 23:59:59 của ngày được chọn

          filteredPosts = filteredPosts.Where(p =>
            p.CreatedAt >= fromDate && p.CreatedAt <= toDate
          ).ToList();
        }

        // Hiển thị kết quả
        DisplayPosts(filteredPosts);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi lọc bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void DisplayPosts(List<Post> posts)
    {
      if (isLoading) return;

      try
      {
        flowLayoutPanelPosts.Controls.Clear();

        if (posts == null || posts.Count == 0)
        {
          // Hiển thị thông báo nếu không có bài viết
          Label lblNoPost = new Label
          {
            Text = "Không tìm thấy bài viết nào phù hợp với bộ lọc.",
            Font = new System.Drawing.Font("Segoe UI", 12F),
            ForeColor = System.Drawing.Color.Gray,
            TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            AutoSize = false,
            Width = flowLayoutPanelPosts.ClientSize.Width - 40,
            Height = 100,
            Padding = new Padding(20)
          };
          flowLayoutPanelPosts.Controls.Add(lblNoPost);
        }
        else
        {
          // Sắp xếp theo ngày tạo mới nhất
          var sortedPosts = posts.OrderByDescending(p => p.CreatedAt).ToList();

          foreach (var postData in sortedPosts)
          {
            var postControl = new PostControl(postData);
            postControl.PostClicked += PostControl_PostClicked;
            flowLayoutPanelPosts.Controls.Add(postControl);
          }
        }

        flowLayoutPanelPosts_Resize(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi hiển thị bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
