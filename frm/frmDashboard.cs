﻿using SocialManager.services;
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

    // Navigation bar controls
    private Panel? pnlSidebar;
    private Button? btnNavHome;
    private Button? btnNavDashboard;
    private Button? btnNavCommented;
    private Button? btnNavLiked;
    private Button? btnNavSettings;
    private Panel? pnlSettingsDropdown;
    private bool isSettingsDropdownOpen = false;
    private string currentView = "Home"; // Track current view

    // UserControls for different views
    private UserControls.ucNewsfeed? ucNewsfeed;

    // Lazy loading variables
    private int currentPostIndex = 0;
    private const int postsPerLoad = 15;
    private List<Post>? allAvailablePosts; public frmDashboard()
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

      // Only keep btnNewPost hover effect
      AddButtonHoverEffect(this.btnNewPost, Color.FromArgb(10, 102, 194), Color.FromArgb(8, 82, 155));
      this.flowLayoutPanelPosts.Resize += new EventHandler(flowLayoutPanelPosts_Resize);
      this.flowLayoutPanelPosts.Scroll += new ScrollEventHandler(flowLayoutPanelPosts_Scroll);

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

      // Initialize navigation bar
      InitializeSidebar();

      // Add app title header
      CreateAppTitle();

      ShowHome();
    }

    // Event handler khi form load - điều chỉnh vị trí các controls
    private void frmDashboard_Load(object? sender, EventArgs e)
    {
      // Only position btnNewPost at bottom right
      int bottomMargin = 20;
      btnNewPost.Location = new Point(this.ClientSize.Width - btnNewPost.Width - 20, this.ClientSize.Height - btnNewPost.Height - bottomMargin);
      btnNewPost.BringToFront();

      // Hide old buttons by making them invisible
      if (this.Controls.Contains(btnThemeToggle)) btnThemeToggle.Visible = false;
      if (this.Controls.Contains(btnLogout)) btnLogout.Visible = false;
      if (this.Controls.Contains(btnSettings)) btnSettings.Visible = false;
      if (this.Controls.Contains(btnEditProfile)) btnEditProfile.Visible = false;

      // Bring navigation bar to front and position it in center
      if (pnlSidebar != null)
      {
        pnlSidebar.BringToFront();
      }
    }

    #region Navigation Bar Implementation

    private void InitializeSidebar()
    {
      // Create navigation panel - positioned in center, not docked
      pnlSidebar = new Panel
      {
        Height = 70,
        BackColor = Color.FromArgb(250, 248, 246), // Ivory white
        Name = "pnlSidebar"
      };

      // Calculate button positions with spacing
      int buttonSize = 70;
      int spacing = 20;
      int totalWidth = 5 * buttonSize + 4 * spacing; // 5 buttons + 4 gaps
      pnlSidebar.Width = totalWidth;

      // Position navbar in center of form (replace old buttons position)
      PositionNavbarInCenter();

      int startX = 0; // Relative to panel, not form

      // Create Home button
      btnNavHome = new Button
      {
        Text = "⌂", // Unicode house symbol
        Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular),
        Size = new Size(buttonSize, buttonSize),
        Location = new Point(startX, 0),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.FromArgb(90, 90, 90),
        Cursor = Cursors.Hand,
        Name = "btnNavHome"
      };
      btnNavHome.FlatAppearance.BorderSize = 0;
      btnNavHome.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnNavHome.Click += (s, e) =>
      {
        if (isSettingsDropdownOpen) ToggleSettingsDropdown();
        ShowHome();
      };
      // Add rounded corners
      AddRoundedCorners(btnNavHome, 15);

      // Create Dashboard button
      btnNavDashboard = new Button
      {
        Text = "✎", // Unicode pencil symbol
        Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular),
        Size = new Size(buttonSize, buttonSize),
        Location = new Point(startX + buttonSize + spacing, 0),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.FromArgb(90, 90, 90),
        Cursor = Cursors.Hand,
        Name = "btnNavDashboard"
      };
      btnNavDashboard.FlatAppearance.BorderSize = 0;
      btnNavDashboard.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnNavDashboard.Click += (s, e) =>
      {
        if (isSettingsDropdownOpen) ToggleSettingsDropdown();
        ShowDashboard();
      };
      // Add rounded corners
      AddRoundedCorners(btnNavDashboard, 15);

      // Create Commented button
      btnNavCommented = new Button
      {
        Text = "✉", // Unicode envelope symbol
        Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular),
        Size = new Size(buttonSize, buttonSize),
        Location = new Point(startX + 2 * (buttonSize + spacing), 0),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.FromArgb(90, 90, 90),
        Cursor = Cursors.Hand,
        Name = "btnNavCommented"
      };
      btnNavCommented.FlatAppearance.BorderSize = 0;
      btnNavCommented.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnNavCommented.Click += (s, e) =>
      {
        if (isSettingsDropdownOpen) ToggleSettingsDropdown();
        ShowCommentedPosts();
      };
      // Add rounded corners
      AddRoundedCorners(btnNavCommented, 15);

      // Create Liked button
      btnNavLiked = new Button
      {
        Text = "♥", // Unicode heart symbol
        Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular),
        Size = new Size(buttonSize, buttonSize),
        Location = new Point(startX + 3 * (buttonSize + spacing), 0),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.FromArgb(90, 90, 90),
        Cursor = Cursors.Hand,
        Name = "btnNavLiked"
      };
      btnNavLiked.FlatAppearance.BorderSize = 0;
      btnNavLiked.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnNavLiked.Click += (s, e) =>
      {
        if (isSettingsDropdownOpen) ToggleSettingsDropdown();
        ShowLikedPosts();
      };
      // Add rounded corners
      AddRoundedCorners(btnNavLiked, 15);

      // Create Settings button
      btnNavSettings = new Button
      {
        Text = "⚙", // Unicode gear symbol
        Font = new Font("Segoe UI Symbol", 24F, FontStyle.Regular),
        Size = new Size(buttonSize, buttonSize),
        Location = new Point(startX + 4 * (buttonSize + spacing), 0),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.Transparent,
        ForeColor = Color.FromArgb(90, 90, 90),
        Cursor = Cursors.Hand,
        Name = "btnNavSettings"
      };
      btnNavSettings.FlatAppearance.BorderSize = 0;
      btnNavSettings.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnNavSettings.Click += (s, e) => ToggleSettingsDropdown();
      // Add rounded corners
      AddRoundedCorners(btnNavSettings, 15);

      // Add buttons to panel
      pnlSidebar.Controls.Add(btnNavHome);
      pnlSidebar.Controls.Add(btnNavDashboard);
      pnlSidebar.Controls.Add(btnNavCommented);
      pnlSidebar.Controls.Add(btnNavLiked);
      pnlSidebar.Controls.Add(btnNavSettings);

      // Add panel to form
      this.Controls.Add(pnlSidebar);
      pnlSidebar.BringToFront();

      // Handle resize to recalculate button positions
      this.Resize += (s, e) => UpdateNavigationLayout();

      // Create settings dropdown
      CreateSettingsDropdown();
    }

    private void PositionNavbarInCenter()
    {
      if (pnlSidebar == null) return;

      // Position in center of form, where old buttons were (around profile area)
      int centerX = (this.ClientSize.Width - pnlSidebar.Width) / 2;
      int centerY = 150; // Position near profile area

      pnlSidebar.Location = new Point(centerX, centerY);
    }

    private void UpdateNavigationLayout()
    {
      if (pnlSidebar == null || btnNavHome == null || btnNavDashboard == null ||
          btnNavCommented == null || btnNavLiked == null || btnNavSettings == null)
        return;

      // Reposition the entire navbar panel
      PositionNavbarInCenter();

      // Update button positions within panel (relative to panel, not form)
      int buttonSize = 70;
      int spacing = 20;
      int startX = 0;

      btnNavHome.Location = new Point(startX, 0);
      btnNavDashboard.Location = new Point(startX + buttonSize + spacing, 0);
      btnNavCommented.Location = new Point(startX + 2 * (buttonSize + spacing), 0);
      btnNavLiked.Location = new Point(startX + 3 * (buttonSize + spacing), 0);
      btnNavSettings.Location = new Point(startX + 4 * (buttonSize + spacing), 0);
    }
    private void CreateSettingsDropdown()
    {
      pnlSettingsDropdown = new Panel
      {
        Size = new Size(200, 120),
        BackColor = Color.White,
        Visible = false,
        BorderStyle = BorderStyle.FixedSingle,
        Name = "pnlSettingsDropdown"
      };

      // Edit Profile button
      var btnEditProfileDropdown = new Button
      {
        Text = "Edit Profile",
        Location = new Point(0, 0),
        Size = new Size(198, 40),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.White,
        ForeColor = Color.FromArgb(90, 90, 90),
        TextAlign = ContentAlignment.MiddleLeft,
        Padding = new Padding(10, 0, 0, 0),
        Cursor = Cursors.Hand
      };
      btnEditProfileDropdown.FlatAppearance.BorderSize = 0;
      btnEditProfileDropdown.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnEditProfileDropdown.Click += (s, e) =>
      {
        ToggleSettingsDropdown();
        OpenEditProfile();
      };

      // Toggle Theme button
      var btnToggleThemeDropdown = new Button
      {
        Text = "Toggle Theme",
        Location = new Point(0, 40),
        Size = new Size(198, 40),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.White,
        ForeColor = Color.FromArgb(90, 90, 90),
        TextAlign = ContentAlignment.MiddleLeft,
        Padding = new Padding(10, 0, 0, 0),
        Cursor = Cursors.Hand
      };
      btnToggleThemeDropdown.FlatAppearance.BorderSize = 0;
      btnToggleThemeDropdown.FlatAppearance.MouseOverBackColor = Color.FromArgb(240, 240, 240);
      btnToggleThemeDropdown.Click += (s, e) =>
      {
        ToggleSettingsDropdown();
        isDarkMode = !isDarkMode;
        ApplyTheme(isDarkMode);
        SaveDarkModePreference(isDarkMode);
      };

      // Logout button
      var btnLogoutDropdown = new Button
      {
        Text = "Logout",
        Location = new Point(0, 80),
        Size = new Size(198, 40),
        FlatStyle = FlatStyle.Flat,
        BackColor = Color.White,
        ForeColor = Color.FromArgb(231, 76, 60),
        TextAlign = ContentAlignment.MiddleLeft,
        Padding = new Padding(10, 0, 0, 0),
        Cursor = Cursors.Hand
      };
      btnLogoutDropdown.FlatAppearance.BorderSize = 0;
      btnLogoutDropdown.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 240, 240);
      btnLogoutDropdown.Click += (s, e) =>
      {
        ToggleSettingsDropdown();
        var result = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (result == DialogResult.Yes)
        {
          AuthSessionService.Logout();
          this.Close();
        }
      };

      pnlSettingsDropdown.Controls.Add(btnEditProfileDropdown);
      pnlSettingsDropdown.Controls.Add(btnToggleThemeDropdown);
      pnlSettingsDropdown.Controls.Add(btnLogoutDropdown);

      this.Controls.Add(pnlSettingsDropdown);
      pnlSettingsDropdown.BringToFront();
    }

    private void ToggleSettingsDropdown()
    {
      if (pnlSettingsDropdown == null || btnNavSettings == null) return;

      isSettingsDropdownOpen = !isSettingsDropdownOpen;
      pnlSettingsDropdown.Visible = isSettingsDropdownOpen;

      if (isSettingsDropdownOpen)
      {
        // Position dropdown below settings button
        pnlSettingsDropdown.Location = new Point(
          btnNavSettings.Location.X + btnNavSettings.Width - pnlSettingsDropdown.Width,
          pnlSidebar!.Height
        );
      }
    }

    private void SetActiveNavButton(Button? activeButton)
    {
      if (btnNavHome == null || btnNavDashboard == null || btnNavCommented == null ||
          btnNavLiked == null || btnNavSettings == null)
        return;

      // Reset all buttons
      btnNavHome.BackColor = Color.Transparent;
      btnNavHome.ForeColor = Color.FromArgb(90, 90, 90);
      btnNavDashboard.BackColor = Color.Transparent;
      btnNavDashboard.ForeColor = Color.FromArgb(90, 90, 90);
      btnNavCommented.BackColor = Color.Transparent;
      btnNavCommented.ForeColor = Color.FromArgb(90, 90, 90);
      btnNavLiked.BackColor = Color.Transparent;
      btnNavLiked.ForeColor = Color.FromArgb(90, 90, 90);
      btnNavSettings.BackColor = Color.Transparent;
      btnNavSettings.ForeColor = Color.FromArgb(90, 90, 90);

      // Highlight active button
      if (activeButton != null)
      {
        activeButton.BackColor = Color.FromArgb(240, 240, 240);
        activeButton.ForeColor = Color.FromArgb(52, 152, 219); // Blue accent
      }
    }

    #endregion

    #region View Navigation Methods

    private void ShowHome()
    {
      currentView = "Home";
      SetActiveNavButton(btnNavHome);

      // Hide user profile info in newsfeed mode
      if (panelProfileHeader != null) panelProfileHeader.Visible = false;

      // Load all posts for newsfeed (not just user's posts)
      LoadAllPosts();
    }

    private void ShowDashboard()
    {
      currentView = "Dashboard";
      SetActiveNavButton(btnNavDashboard);

      // Show user profile info in user posts view
      if (panelProfileHeader != null) panelProfileHeader.Visible = true;

      // Show user's own posts (already implemented in LoadPosts)
      LoadPosts();

      // Ensure navigation bar stays on top
      if (pnlSidebar != null) pnlSidebar.BringToFront();
    }

    private void ShowCommentedPosts()
    {
      currentView = "Commented";
      SetActiveNavButton(btnNavCommented);

      // Show user profile info in user-specific views
      if (panelProfileHeader != null) panelProfileHeader.Visible = true;

      // Load posts with comments
      LoadCommentedPosts();

      // Ensure navigation bar stays on top
      if (pnlSidebar != null) pnlSidebar.BringToFront();
    }

    private void ShowLikedPosts()
    {
      currentView = "Liked";
      SetActiveNavButton(btnNavLiked);

      // Show user profile info in user-specific views
      if (panelProfileHeader != null) panelProfileHeader.Visible = true;

      // Load liked posts
      LoadLikedPosts();

      // Ensure navigation bar stays on top
      if (pnlSidebar != null) pnlSidebar.BringToFront();
    }

    private void ReloadCurrentView()
    {
      // Reload the current view based on currentView variable
      switch (currentView)
      {
        case "Home":
          LoadAllPosts();
          break;
        case "Dashboard":
          LoadPosts();
          break;
        case "Commented":
          LoadCommentedPosts();
          break;
        case "Liked":
          LoadLikedPosts();
          break;
        default:
          LoadPosts();
          break;
      }
    }

    private void LoadCommentedPosts()
    {
      try
      {
        if (postService == null || currentUser == null) return;

        // Get posts that the current user has commented on
        var commentedPosts = postService.GetPostsCommentedByUser(currentUser.UserID);
        allUserPosts = commentedPosts;
        DisplayPosts(commentedPosts);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading commented posts: {ex.Message}", "Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadLikedPosts()
    {
      try
      {
        if (postService == null || currentUser == null) return;

        // Get posts that the current user has liked
        var likedPosts = postService.GetPostsLikedByUser(currentUser.UserID);
        allUserPosts = likedPosts;
        DisplayPosts(likedPosts);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading liked posts: {ex.Message}", "Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadAllPosts()
    {
      try
      {
        if (postService == null) return;

        // Get all posts from all users (for newsfeed)
        allAvailablePosts = postService.GetAllPosts();
        currentPostIndex = 0;

        // Clear existing posts
        flowLayoutPanelPosts.Controls.Clear();

        // Load first batch
        LoadMorePosts();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading newsfeed: {ex.Message}", "Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void LoadMorePosts()
    {
      if (allAvailablePosts == null || currentPostIndex >= allAvailablePosts.Count) return;

      try
      {
        int endIndex = Math.Min(currentPostIndex + postsPerLoad, allAvailablePosts.Count);
        var postsToLoad = allAvailablePosts.Skip(currentPostIndex).Take(endIndex - currentPostIndex).ToList();

        foreach (var postData in postsToLoad)
        {
          var postControl = new PostControl(postData);
          postControl.PostClicked += PostControl_PostClicked;
          flowLayoutPanelPosts.Controls.Add(postControl);
        }

        currentPostIndex = endIndex;
        flowLayoutPanelPosts_Resize(this, EventArgs.Empty);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading more posts: {ex.Message}");
      }
    }

    private void CreateAppTitle()
    {
      var lblAppTitle = new Label
      {
        Text = "Social Media",
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
        ForeColor = Color.FromArgb(24, 119, 242), // Facebook blue
        AutoSize = true,
        BackColor = Color.Transparent,
        Name = "lblAppTitle",
        Location = new Point(20, 17) // Căn chỉnh với header height 60px
      };

      // Add to header panel instead of form
      if (this.panelHeader != null && this.panelHeader.Controls.Count > 0)
      {
        this.panelHeader.Controls.Add(lblAppTitle);
        lblAppTitle.BringToFront();
      }
      else
      {
        // Fallback: add to form
        this.Controls.Add(lblAppTitle);
        lblAppTitle.BringToFront();
      }

      // Apply theme-aware colors
      if (isDarkMode)
      {
        lblAppTitle.ForeColor = Color.FromArgb(100, 181, 246); // Lighter blue for dark theme
      }
    }

    private void OpenEditProfile()
    {
      try
      {
        var editForm = new frmInfor();
        editForm.ShowDialog();

        // Refresh user info after editing
        LoadUserInfo();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error opening edit profile: {ex.Message}", "Error",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    #endregion


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

      // Text màu - Fix for dark mode
      var primaryText = dark ? Color.FromArgb(240, 242, 245) : Color.FromArgb(33, 33, 33);
      var secondaryText = dark ? Color.FromArgb(185, 187, 190) : Color.FromArgb(102, 102, 102);

      // XÓA lblUsername vì không còn dùng
      lblProfileName.ForeColor = primaryText;
      lblFollowerStats.ForeColor = secondaryText;
      lblComposerPlaceholder.ForeColor = secondaryText;

      lblType.ForeColor = primaryText;
      lblFromDate.ForeColor = primaryText;
      lblToDate.ForeColor = primaryText;
      lblPostId.ForeColor = primaryText;

      // Input controls - Fix for dark mode
      txtSearch.ForeColor = dark ? Color.FromArgb(240, 242, 245) : Color.FromArgb(64, 64, 64);
      txtSearch.BackColor = dark ? Color.FromArgb(58, 59, 60) : Color.White;
      txtPostId.ForeColor = dark ? Color.FromArgb(240, 242, 245) : Color.FromArgb(64, 64, 64);
      txtPostId.BackColor = dark ? Color.FromArgb(58, 59, 60) : Color.White;
      cboType.ForeColor = primaryText;
      cboType.BackColor = dark ? Color.FromArgb(58, 59, 60) : Color.White;
      dtpFrom.CalendarForeColor = primaryText;
      dtpTo.CalendarForeColor = primaryText;

      // Apply theme to navigation bar if exists
      if (pnlSidebar != null)
      {
        pnlSidebar.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.FromArgb(250, 248, 246);

        // Update navigation buttons
        var navButtonColor = dark ? Color.FromArgb(185, 187, 190) : Color.FromArgb(90, 90, 90);
        var navHoverColor = dark ? Color.FromArgb(58, 59, 60) : Color.FromArgb(240, 240, 240);

        if (btnNavHome != null)
        {
          btnNavHome.ForeColor = navButtonColor;
          btnNavHome.FlatAppearance.MouseOverBackColor = navHoverColor;
        }
        if (btnNavDashboard != null)
        {
          btnNavDashboard.ForeColor = navButtonColor;
          btnNavDashboard.FlatAppearance.MouseOverBackColor = navHoverColor;
        }
        if (btnNavCommented != null)
        {
          btnNavCommented.ForeColor = navButtonColor;
          btnNavCommented.FlatAppearance.MouseOverBackColor = navHoverColor;
        }
        if (btnNavLiked != null)
        {
          btnNavLiked.ForeColor = navButtonColor;
          btnNavLiked.FlatAppearance.MouseOverBackColor = navHoverColor;
        }
        if (btnNavSettings != null)
        {
          btnNavSettings.ForeColor = navButtonColor;
          btnNavSettings.FlatAppearance.MouseOverBackColor = navHoverColor;
        }
      }

      // Apply theme to settings dropdown if exists
      if (pnlSettingsDropdown != null)
      {
        pnlSettingsDropdown.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;

        foreach (Control ctrl in pnlSettingsDropdown.Controls)
        {
          if (ctrl is Button btn)
          {
            btn.BackColor = dark ? Color.FromArgb(36, 37, 38) : Color.White;
            btn.ForeColor = ctrl.Name?.Contains("Logout") == true
              ? (dark ? Color.FromArgb(255, 100, 100) : Color.FromArgb(231, 76, 60))
              : primaryText;
            btn.FlatAppearance.MouseOverBackColor = dark ? Color.FromArgb(58, 59, 60) : Color.FromArgb(240, 240, 240);
          }
        }
      }

      // Only btnNewPost styling (other buttons are hidden)
      btnNewPost.BackColor = Color.FromArgb(10, 102, 194);

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

    // Helper: Add rounded corners (alias for AddRoundedButton)
    private void AddRoundedCorners(Button btn, int radius)
    {
      AddRoundedButton(btn, radius);
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

        // Hiển thị trạng thái tài khoản chi tiết
        UpdateAccountStatusDisplay();

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

    private void UpdateAccountStatusDisplay()
    {
      if (currentUser == null || lblAccountStatus == null) return;

      string statusText = "";
      Color statusColor;

      // Kiểm tra trạng thái dựa trên ViolationCount và ReportCount
      if (currentUser.ViolationCount >= 3 || currentUser.ReportCount >= 50)
      {
        // Bị cấm
        statusText = $"⛔ Tài khoản bị cấm";
        if (currentUser.ViolationCount >= 3)
          statusText += $" - Bạn đã vi phạm {currentUser.ViolationCount} lần";
        if (currentUser.ReportCount >= 50)
          statusText += $" - {currentUser.ReportCount} báo cáo";
        statusColor = Color.FromArgb(220, 53, 69); // Red
      }
      else if (currentUser.ViolationCount >= 1 || currentUser.ReportCount >= 30)
      {
        // Cảnh báo
        statusText = $"⚠️ Tài khoản đang bị cảnh cáo";
        if (currentUser.ViolationCount >= 1)
          statusText += $" - Bạn đã bị cảnh cáo {currentUser.ViolationCount} lần";
        if (currentUser.ReportCount >= 30)
          statusText += $" - {currentUser.ReportCount} báo cáo";
        statusColor = Color.FromArgb(255, 193, 7); // Yellow/Orange
      }
      else
      {
        // Bình thường
        statusText = $"✓ Tài khoản hoạt động bình thường";
        if (currentUser.ReportCount > 0)
          statusText += $" - {currentUser.ReportCount} báo cáo";
        statusColor = Color.FromArgb(40, 167, 69); // Green
      }

      lblAccountStatus.Text = statusText;
      lblAccountStatus.ForeColor = statusColor;
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

        // Reload lại view hiện tại thay vì luôn quay về Home
        ReloadCurrentView();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi mở chi tiết bài viết: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void flowLayoutPanelPosts_Resize(object? sender, EventArgs e)
    {
      // Match post width with composer panel width
      int targetWidth = panelComposer?.Width ?? (flowLayoutPanelPosts.ClientSize.Width - SystemInformation.VerticalScrollBarWidth);

      foreach (Control c in flowLayoutPanelPosts.Controls)
      {
        c.Width = targetWidth;
      }

      // XÓA lblUsername vì không còn dùng - cập nhật profile name thay vì
      if (currentUser != null)
      {
        lblProfileName.Text = currentUser.UserName;
      }
    }

    private void flowLayoutPanelPosts_Scroll(object? sender, ScrollEventArgs e)
    {
      // Check if user scrolled near the bottom (when at 10th post from end)
      if (flowLayoutPanelPosts.Controls.Count >= 10)
      {
        var scrollPosition = e.NewValue;
        var maxScroll = flowLayoutPanelPosts.VerticalScroll.Maximum;
        var visibleHeight = flowLayoutPanelPosts.ClientSize.Height;

        // Load more when scrolled to 80% of content
        if (scrollPosition >= (maxScroll - visibleHeight) * 0.8)
        {
          LoadMorePosts();
        }
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