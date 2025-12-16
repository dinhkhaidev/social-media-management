using SocialManager.utils;
using SocialManager.services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SocialManager.frm.UserControls
{
  public partial class ucDashboard : UserControl
  {
    #region Fields
    private DashboardService dashboardService;
    private UserService userService;
    private PostService postService;
    private ReportService reportService;
    private CommentService commentService;
    private System.Windows.Forms.Timer refreshTimer;
    private Panel mainScrollPanel;
    private Dictionary<string, Label> metricLabels;
    private ComboBox cmbTimeFilter;
    private Panel chartContainer;
    private Panel topActivitiesPanel;
    private Panel alertsPanel;
    private string selectedTimeFilter = "24h"; // 24h, 7days, 30days, custom
    #endregion

    #region Constructor
    public ucDashboard()
    {
      System.Diagnostics.Debug.WriteLine("ucDashboard constructor started");

      try
      {
        System.Diagnostics.Debug.WriteLine("Calling InitializeComponent...");
        InitializeComponent();
        System.Diagnostics.Debug.WriteLine("InitializeComponent completed successfully");

        System.Diagnostics.Debug.WriteLine("Calling InitializeDashboard...");
        InitializeDashboard();
        System.Diagnostics.Debug.WriteLine("InitializeDashboard completed successfully");

        System.Diagnostics.Debug.WriteLine("ucDashboard constructor completed successfully");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error in ucDashboard constructor: {ex.Message}");
        System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
        ShowErrorUI(ex);
      }
    }
    #endregion

    #region Initialization
    private void InitializeDashboard()
    {
      this.BackColor = Color.FromArgb(247, 249, 252);
      this.AutoScroll = false;
      metricLabels = new Dictionary<string, Label>();
      
      // Initialize services
      try
      {
        dashboardService = new DashboardService();
        userService = new UserService();
        postService = new PostService();
        reportService = new ReportService();
        commentService = new CommentService();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error initializing services: {ex.Message}");
      }
      
      CreateModernDashboardLayout();
      
      // Load data after control is created and has handle
      this.Load += async (s, e) => {
        await LoadDashboardDataAsync();
      };
      
      InitializeAutoRefresh();
    }

    private void InitializeAutoRefresh()
    {
      // Auto refresh every 30 seconds
      refreshTimer = new System.Windows.Forms.Timer();
      refreshTimer.Interval = 30000;
      refreshTimer.Tick += async (s, e) => {
        if (this.IsHandleCreated)
          await LoadDashboardDataAsync();
      };
      refreshTimer.Start();
    }

    private void ShowErrorUI(Exception ex)
    {
      this.Size = new Size(800, 600);
      this.BackColor = Color.FromArgb(247, 249, 252);

      var lblError = new Label
      {
        Text = $"⚠️ Lỗi tải Bảng điều khiển\n\nChi tiết: {ex.Message}\n\nĐây là giao diện dự phòng. Một số chức năng có thể bị hạn chế.",
        Dock = DockStyle.Fill,
        TextAlign = ContentAlignment.MiddleCenter,
        Font = new Font("Segoe UI", 12F),
        ForeColor = Color.FromArgb(231, 76, 60),
        BackColor = Color.White,
        Padding = new Padding(20)
      };

      this.Controls.Clear();
      this.Controls.Add(lblError);

      System.Diagnostics.Debug.WriteLine("ucDashboard fallback UI created");
    }
    #endregion

    #region Layout Creation
    private void CreateModernDashboardLayout()
    {
      this.Controls.Clear();
      
      // Main scrollable panel with better scrolling
      mainScrollPanel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = DashboardTheme.BackgroundLight,
        AutoScroll = true,
        Padding = new Padding(0)
      };

      // Container for all content
      var contentContainer = new Panel
      {
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        Dock = DockStyle.Top,
        BackColor = Color.Transparent,
        Padding = new Padding(24, 24, 24, 40),
        MinimumSize = new Size(0, 900)
      };

      // Header section - Enhanced with better spacing
      var headerPanel = CreateHeaderSection();
      headerPanel.Dock = DockStyle.Top;
      headerPanel.Height = 90;
      headerPanel.Margin = new Padding(0, 0, 0, 24);

      // Key metrics cards (4 cards in a row) - Enhanced spacing
      var metricsPanel = CreateMetricsSection();
      metricsPanel.Dock = DockStyle.Top;
      metricsPanel.Height = 140;
      metricsPanel.Margin = new Padding(0, 0, 0, 24);

      // Three column layout: Alerts - Shortcuts - Recent Activities
      var threeColumnPanel = new TableLayoutPanel
      {
        Dock = DockStyle.Top,
        Height = 280,
        ColumnCount = 3,
        RowCount = 1,
        BackColor = Color.Transparent,
        Margin = new Padding(0, 0, 0, 24),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };
      threeColumnPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F)); // Alerts
      threeColumnPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Shortcuts
      threeColumnPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F)); // Recent Activities
      threeColumnPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

      alertsPanel = CreateAlertsPanel();
      var shortcutsPanel = CreateShortcutsPanel();
      var recentActivitiesPanel = CreateRecentActivitiesPanel();

      threeColumnPanel.Controls.Add(alertsPanel, 0, 0);
      threeColumnPanel.Controls.Add(shortcutsPanel, 1, 0);
      threeColumnPanel.Controls.Add(recentActivitiesPanel, 2, 0);

      // Two column layout for charts and top activities - Better proportions
      var twoColumnPanel = new TableLayoutPanel
      {
        Dock = DockStyle.Top,
        Height = 420,
        ColumnCount = 2,
        RowCount = 1,
        BackColor = Color.Transparent,
        Margin = new Padding(0, 0, 0, 24),
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None
      };
      
      twoColumnPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
      twoColumnPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
      twoColumnPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

      // Chart panel (left) - Enhanced
      var chartPanel = CreateChartSection();
      
      // Top activities panel (right) - Enhanced
      topActivitiesPanel = CreateTopActivitiesPanel();

      twoColumnPanel.Controls.Add(chartPanel, 0, 0);
      twoColumnPanel.Controls.Add(topActivitiesPanel, 1, 0);

      // Quick stats row - Enhanced
      var quickStatsPanel = CreateQuickStatsSection();
      quickStatsPanel.Dock = DockStyle.Top;
      quickStatsPanel.Height = 140;
      quickStatsPanel.Margin = new Padding(0, 0, 0, 0);

      // Add all sections to content container in REVERSE order (bottom to top)
      contentContainer.Controls.Add(quickStatsPanel);
      contentContainer.Controls.Add(twoColumnPanel);
      contentContainer.Controls.Add(threeColumnPanel);
      contentContainer.Controls.Add(metricsPanel);
      contentContainer.Controls.Add(headerPanel);

      mainScrollPanel.Controls.Add(contentContainer);
      this.Controls.Add(mainScrollPanel);
    }

    private Panel CreateHeaderSection()
    {
      var panel = new Panel { BackColor = Color.Transparent };

      var lblTitle = new Label
      {
        Text = "Dashboard Tổng Quan",
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
        ForeColor = Color.FromArgb(30, 40, 50),
        AutoSize = true,
        Location = new Point(0, 5)
      };

      var lblSubtitle = new Label
      {
        Text = $"Cập nhật: {DateTime.Now:HH:mm dd/MM/yyyy}",
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(120, 130, 140),
        AutoSize = true,
        Location = new Point(0, 38),
        Name = "lblLastUpdate"
      };

      // Time filter dropdown
      var lblFilter = new Label
      {
        Text = "Thời gian:",
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(80, 90, 100),
        AutoSize = true,
        Location = new Point(panel.Width - 420, 12),
        Anchor = AnchorStyles.Top | AnchorStyles.Right
      };

      cmbTimeFilter = new ComboBox
      {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = new Font("Segoe UI", 9F),
        Size = new Size(110, 28),
        Location = new Point(panel.Width - 340, 8),
        Anchor = AnchorStyles.Top | AnchorStyles.Right,
        FlatStyle = FlatStyle.Flat
      };
      cmbTimeFilter.Items.AddRange(new object[] { "24 giờ", "7 ngày", "30 ngày" });
      cmbTimeFilter.SelectedIndex = 0;
      cmbTimeFilter.SelectedIndexChanged += CmbTimeFilter_Changed;

      var btnRefresh = new Button
      {
        Text = "Làm mới",
        Size = new Size(85, 30),
        BackColor = Color.FromArgb(52, 152, 219),
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 9F),
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand,
        Location = new Point(panel.Width - 210, 8),
        Anchor = AnchorStyles.Top | AnchorStyles.Right
      };
      btnRefresh.FlatAppearance.BorderSize = 0;
      btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(52, 152, 219);
      btnRefresh.Click += async (s, e) => await LoadDashboardDataAsync();

      var btnExport = new Button
      {
        Text = "Xuất CSV",
        Size = new Size(85, 30),
        BackColor = Color.FromArgb(46, 204, 113),
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 9F),
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand,
        Location = new Point(panel.Width - 115, 8),
        Anchor = AnchorStyles.Top | AnchorStyles.Right
      };
      btnExport.FlatAppearance.BorderSize = 0;
      btnExport.Click += ExportReport_Click;

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(lblSubtitle);
      panel.Controls.Add(lblFilter);
      panel.Controls.Add(cmbTimeFilter);
      panel.Controls.Add(btnRefresh);
      panel.Controls.Add(btnExport);

      return panel;
    }

    private Panel CreateMetricsSection()
    {
      var tablePanel = new TableLayoutPanel
      {
        ColumnCount = 4,
        RowCount = 1,
        Dock = DockStyle.Fill,
        BackColor = Color.Transparent,
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
        Padding = new Padding(0)
      };

      // Set equal column widths
      for (int i = 0; i < 4; i++)
      {
        tablePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
      }
      tablePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

      var metrics = new[]
      {
        new { Title = "Tổng Người Dùng", Icon = "U", Color = Color.FromArgb(52, 152, 219), Key = "totalUsers", SubText = "Đang tải..."},
        new { Title = "Bài Viết 24h", Icon = "P", Color = Color.FromArgb(46, 204, 113), Key = "postsToday", SubText = "Hôm nay"},
        new { Title = "Báo Cáo Chờ", Icon = "!", Color = Color.FromArgb(230, 126, 34), Key = "pendingReports", SubText = "Cần xử lý"},
        new { Title = "Bình Luận Mới", Icon = "C", Color = Color.FromArgb(155, 89, 182), Key = "newComments", SubText = "24h qua"}
      };

      for (int i = 0; i < metrics.Length; i++)
      {
        var metric = metrics[i];
        
        // Container với margin để tạo khoảng cách
        var container = new Panel
        {
          Dock = DockStyle.Fill,
          BackColor = Color.Transparent,
          Padding = new Padding(0, 0, i < 3 ? 15 : 0, 0)
        };

        var card = CreateEnhancedMetricCard(
          metric.Title,
          "...",
          metric.SubText,
          metric.Icon,
          metric.Color,
          0,
          metric.Key
        );
        card.Dock = DockStyle.Fill;
        
        container.Controls.Add(card);
        tablePanel.Controls.Add(container, i, 0);
      }

      return tablePanel;
    }

    private Panel CreateEnhancedMetricCard(string title, string value, string subText, string icon, Color accentColor, int xPosition, string key)
    {
      var card = new Panel
      {
        BackColor = Color.White,
        Name = $"card_{key}",
        Cursor = Cursors.Hand,
        Padding = new Padding(20)
      };

      card.Paint += (sender, e) => {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        // Background với border radius
        using (var path = GetRoundedRect(card.ClientRectangle, 8))
        {
          // Shadow nhẹ
          using (var shadowBrush = new SolidBrush(Color.FromArgb(10, 0, 0, 0)))
          {
            g.FillPath(shadowBrush, path);
          }
          
          // Background trắng
          using (var bgBrush = new SolidBrush(Color.White))
          {
            g.FillPath(bgBrush, path);
          }
          
          // Border mỏng
          using (var borderPen = new Pen(Color.FromArgb(230, 235, 240), 1))
          {
            g.DrawPath(borderPen, path);
          }
        }
        
        // Accent bar bên trái
        using (var accentBrush = new SolidBrush(accentColor))
        {
          g.FillRectangle(accentBrush, 0, 10, 4, card.Height - 20);
        }
      };

      // Hover effect
      card.MouseEnter += (s, e) => {
        card.BackColor = Color.FromArgb(248, 250, 252);
        card.Invalidate();
      };
      card.MouseLeave += (s, e) => {
        card.BackColor = Color.White;
        card.Invalidate();
      };

      // Icon circle
      var iconPanel = new Panel
      {
        Size = new Size(50, 50),
        Location = new Point(15, 15),
        BackColor = Color.FromArgb(20, accentColor)
      };
      iconPanel.Paint += (s, e) => {
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(Color.FromArgb(20, accentColor)))
        {
          e.Graphics.FillEllipse(brush, 0, 0, 49, 49);
        }
      };

      var lblIcon = new Label
      {
        Text = icon,
        Font = new Font("Segoe UI", 20F, FontStyle.Bold),
        ForeColor = accentColor,
        Size = new Size(50, 50),
        TextAlign = ContentAlignment.MiddleCenter,
        BackColor = Color.Transparent,
        Parent = iconPanel
      };

      var lblTitle = new Label
      {
        Text = title,
        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
        ForeColor = Color.FromArgb(100, 110, 120),
        Location = new Point(75, 15),
        AutoSize = true,
        BackColor = Color.Transparent
      };

      var lblValue = new Label
      {
        Text = value,
        Font = new Font("Segoe UI", 22F, FontStyle.Bold),
        ForeColor = Color.FromArgb(40, 50, 60),
        Location = new Point(75, 35),
        AutoSize = true,
        Name = $"value_{key}",
        BackColor = Color.Transparent
      };

      var lblSubText = new Label
      {
        Text = subText,
        Font = new Font("Segoe UI", 8F),
        ForeColor = Color.FromArgb(140, 150, 160),
        Location = new Point(75, 80),
        AutoSize = true,
        BackColor = Color.Transparent,
        Name = $"sub_{key}"
      };

      metricLabels[key] = lblValue;
      metricLabels[$"{key}_sub"] = lblSubText;

      card.Controls.Add(iconPanel);
      card.Controls.Add(lblTitle);
      card.Controls.Add(lblValue);
      card.Controls.Add(lblSubText);

      return card;
    }

    private System.Drawing.Drawing2D.GraphicsPath GetRoundedRect(Rectangle bounds, int radius)
    {
      var path = new System.Drawing.Drawing2D.GraphicsPath();
      
      // Validate bounds - must have positive dimensions
      if (bounds.Width <= 0 || bounds.Height <= 0)
      {
        System.Diagnostics.Debug.WriteLine($"⚠️ GetRoundedRect: Invalid bounds {bounds}");
        // Return empty path for invalid bounds
        return path;
      }

      // Validate radius
      if (radius <= 0)
      {
        path.AddRectangle(bounds);
        return path;
      }

      // Ensure diameter doesn't exceed bounds and is at least 1
      int diameter = Math.Max(1, Math.Min(radius * 2, Math.Min(bounds.Width, bounds.Height)));
      
      // If bounds are too small for rounded corners, use regular rectangle
      if (bounds.Width < diameter || bounds.Height < diameter)
      {
        path.AddRectangle(bounds);
        return path;
      }

      try
      {
        var arc = new Rectangle(bounds.Location, new Size(diameter, diameter));

        // Top left
        path.AddArc(arc, 180, 90);

        // Top right
        arc.X = bounds.Right - diameter;
        path.AddArc(arc, 270, 90);

        // Bottom right
        arc.Y = bounds.Bottom - diameter;
        path.AddArc(arc, 0, 90);

        // Bottom left
        arc.X = bounds.Left;
        path.AddArc(arc, 90, 90);

        path.CloseFigure();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"❌ GetRoundedRect error: {ex.Message} for bounds {bounds}, diameter {diameter}");
        path.Dispose();
        path = new System.Drawing.Drawing2D.GraphicsPath();
        path.AddRectangle(bounds);
      }
      
      return path;
    }

    private Panel CreateChartSection()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(0, 0, 10, 0),
        Dock = DockStyle.Fill
      };

      panel.Paint += (sender, e) => DrawModernCard(e.Graphics, panel.ClientRectangle, Color.FromArgb(52, 152, 219));

      var lblTitle = new Label
      {
        Text = "📊 Biểu đồ hoạt động 7 ngày gần đây",
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        AutoSize = true
      };

      var chartArea = new Panel
      {
        Location = new Point(20, 50),
        Size = new Size(panel.Width - 40, panel.Height - 70),
        BackColor = Color.Transparent,
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
        Name = "chartArea"
      };

      chartArea.Paint += DrawSimpleBarChart;

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(chartArea);

      return panel;
    }

    private Panel CreateActivitySection()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(10, 0, 0, 0),
        Dock = DockStyle.Fill,
        AutoScroll = true
      };

      panel.Paint += (sender, e) => DrawModernCard(e.Graphics, panel.ClientRectangle, Color.FromArgb(46, 204, 113));

      var lblTitle = new Label
      {
        Text = "� Hoạt động gần đây",
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        AutoSize = true
      };

      var activityContainer = new Panel
      {
        Location = new Point(20, 50),
        Size = new Size(panel.Width - 40, panel.Height - 70),
        BackColor = Color.Transparent,
        AutoScroll = true,
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
        Name = "activityContainer"
      };

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(activityContainer);

      return panel;
    }

    private void ShortcutButton_Click(object sender, EventArgs e)
    {
      if (sender is Panel panel && panel.Tag != null)
      {
        string tag = panel.Tag.ToString();
        NavigateToSection(tag);
      }
    }

    private void NavigateToSection(string section)
    {
      // Tìm form cha (frmAdmin) để thực hiện navigation
      Form parentForm = this.FindForm();
      if (parentForm != null && parentForm.GetType().Name == "frmAdmin")
      {
        // Sử dụng reflection để gọi các method navigation của frmAdmin
        var adminForm = parentForm;
        
        switch (section.ToLower())
        {
          case "reports":
            // Gọi method ShowReports của frmAdmin
            var showReportsMethod = adminForm.GetType().GetMethod("ShowReports", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            showReportsMethod?.Invoke(adminForm, null);
            break;
            
          case "users":
            // Gọi method ShowSocialAccounts của frmAdmin
            var showUsersMethod = adminForm.GetType().GetMethod("ShowSocialAccounts", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            showUsersMethod?.Invoke(adminForm, null);
            break;
            
          case "posts":
            // Gọi method ShowPosts của frmAdmin
            var showPostsMethod = adminForm.GetType().GetMethod("ShowPosts", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            showPostsMethod?.Invoke(adminForm, null);
            break;
            
          case "analytics":
            // Gọi method ShowAnalytics của frmAdmin
            var showAnalyticsMethod = adminForm.GetType().GetMethod("ShowAnalytics", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            showAnalyticsMethod?.Invoke(adminForm, null);
            break;
        }
      }
    }

    private Panel CreateAlertsPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(0, 0, 10, 0),
        Dock = DockStyle.Fill,
        Name = "alertsPanel",
        Padding = new Padding(20, 15, 20, 15)
      };

      panel.Paint += (s, e) =>
      {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        var rect = panel.ClientRectangle;
        if (rect.Width > 0 && rect.Height > 0)
        {
          using (var path = GetRoundedRect(rect, 8))
          {
            using (var bgBrush = new SolidBrush(Color.White))
            {
              g.FillPath(bgBrush, path);
            }
            using (var borderPen = new Pen(Color.FromArgb(230, 235, 240), 1))
            {
              g.DrawPath(borderPen, path);
            }
          }
        }
      };

      var lblTitle = new Label
      {
        Text = "📢 Thông báo",
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        AutoSize = true
      };

      var alertsFlow = new FlowLayoutPanel
      {
        Location = new Point(20, 45),
        Size = new Size(panel.Width - 40, panel.Height - 60),
        BackColor = Color.Transparent,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
        Name = "alertsFlow",
        AutoScroll = true
      };

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(alertsFlow);

      // Thêm một số thông báo mẫu với khả năng click để navigate
      AddSampleNotifications(alertsFlow);

      return panel;
    }

    private void AddSampleNotifications(FlowLayoutPanel alertsFlow)
    {
      try
      {
        // Get real data
        var pendingReports = reportService?.GetAllReports()?.Count() ?? 0;
        var today = DateTime.Now.Date;
        var todayUsers = userService?.GetAllUsers()?.Count(u => u.CreatedAt.Date == today) ?? 0;
        var todayPosts = postService?.GetAllPosts()?.Count(p => p.CreatedAt.Date == today) ?? 0;
        var allComments = Comment.GetList("datas\\Comment.csv");
        var todayComments = allComments.Count(c => c.CreatedAt.Date == today);
        
        var notifications = new List<dynamic>();
        
        // Add notifications based on real data
        if (pendingReports > 0)
          notifications.Add(new { Text = $"{pendingReports} báo cáo vi phạm cần xử lý", Icon = "⚠️", Color = Color.FromArgb(231, 76, 60), Action = "reports" });
          
        if (todayUsers > 0)
          notifications.Add(new { Text = $"{todayUsers} người dùng mới đăng ký hôm nay", Icon = "👥", Color = Color.FromArgb(52, 152, 219), Action = "users" });
          
        if (todayPosts > 0)
          notifications.Add(new { Text = $"{todayPosts} bài viết mới hôm nay", Icon = "📝", Color = Color.FromArgb(230, 126, 34), Action = "posts" });
          
        if (todayComments > 0)
          notifications.Add(new { Text = $"{todayComments} bình luận mới hôm nay", Icon = "💬", Color = Color.FromArgb(155, 89, 182), Action = "analytics" });
          
        // Always show analytics option
        notifications.Add(new { Text = "Xem báo cáo thống kê chi tiết", Icon = "📊", Color = Color.FromArgb(46, 204, 113), Action = "analytics" });
        
        // If no notifications, show default message
        if (notifications.Count == 1) // Only analytics
        {
          notifications.Insert(0, new { Text = "Hệ thống hoạt động bình thường", Icon = "✅", Color = Color.FromArgb(46, 204, 113), Action = "" });
        }

        foreach (var notification in notifications.Take(4)) // Limit to 4 notifications
        {
          var notifPanel = new Panel
          {
            Size = new Size(alertsFlow.Width - 25, 45),
            BackColor = Color.FromArgb(248, 250, 252),
            Cursor = Cursors.Hand,
            Margin = new Padding(0, 2, 0, 2),
            Tag = notification.Action
          };

          notifPanel.Paint += (s, e) =>
          {
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (notifPanel.ClientRectangle.Width > 0 && notifPanel.ClientRectangle.Height > 0)
            {
              using (var path = GetRoundedRect(notifPanel.ClientRectangle, 4))
              {
                using (var brush = new SolidBrush(notifPanel.BackColor))
                {
                  g.FillPath(brush, path);
                }
                using (var borderPen = new Pen(Color.FromArgb(220, 225, 230), 1))
                {
                  g.DrawPath(borderPen, path);
                }
              }
            }
          };

          // Add hover and click events only if action is not empty
          if (!string.IsNullOrEmpty(notification.Action))
          {
            AddHoverAndClickEvents(notifPanel, notification.Action);
          }
          else
          {
            notifPanel.Cursor = Cursors.Default;
          }

          // Icon
          var lblIcon = new Label
          {
            Text = notification.Icon,
            Font = new Font("Segoe UI", 12F),
            ForeColor = notification.Color,
            Location = new Point(8, 12),
            Size = new Size(20, 20),
            BackColor = Color.Transparent
          };

          // Text
          var lblText = new Label
          {
            Text = notification.Text,
            Font = new Font("Segoe UI", 8.5F),
            ForeColor = Color.FromArgb(60, 70, 80),
            Location = new Point(35, 14),
            Size = new Size(notifPanel.Width - 45, 18),
            BackColor = Color.Transparent
          };

          notifPanel.Controls.Add(lblIcon);
          notifPanel.Controls.Add(lblText);
          alertsFlow.Controls.Add(notifPanel);
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error adding sample notifications: {ex.Message}");
      }
    }

    private void NotificationPanel_Click(object sender, EventArgs e)
    {
      if (sender is Panel panel && panel.Tag != null)
      {
        string action = panel.Tag.ToString();
        NavigateToSection(action);
      }
    }

    private void AddHoverAndClickEvents(Panel panel, string action)
    {
      // Set tag for navigation
      panel.Tag = action;
      
      // Add hover effects to main panel
      panel.MouseEnter += (s, e) => {
        panel.BackColor = Color.FromArgb(240, 245, 250);
        panel.Invalidate();
      };
      panel.MouseLeave += (s, e) => {
        panel.BackColor = Color.FromArgb(248, 250, 252);
        panel.Invalidate();
      };
      
      // Add click event to main panel
      panel.Click += (s, e) => NavigateToSection(action);
      
      // Add events to all child controls recursively
      AddEventsToChildren(panel, action);
    }
    
    private void AddEventsToChildren(Control parent, string action)
    {
      foreach (Control child in parent.Controls)
      {
        // Set cursor
        child.Cursor = Cursors.Hand;
        
        // Forward mouse events to parent
        child.MouseEnter += (s, e) => {
          parent.BackColor = Color.FromArgb(240, 245, 250);
          parent.Invalidate();
        };
        child.MouseLeave += (s, e) => {
          parent.BackColor = Color.FromArgb(248, 250, 252);
          parent.Invalidate();
        };
        
        // Forward click events
        child.Click += (s, e) => NavigateToSection(action);
        
        // Recursively add to nested children
        if (child.HasChildren)
        {
          AddEventsToChildren(child, action);
        }
      }
    }

    private Panel CreateShortcutsPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(0, 0, 10, 0),
        Dock = DockStyle.Fill,
        Padding = new Padding(20),
        Name = "shortcutsPanel"
      };

      panel.Paint += (s, e) =>
      {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        using (var path = GetRoundedRect(panel.ClientRectangle, 8))
        {
          using (var bgBrush = new SolidBrush(Color.White))
          {
            g.FillPath(bgBrush, path);
          }
          using (var borderPen = new Pen(Color.FromArgb(230, 235, 240), 1))
          {
            g.DrawPath(borderPen, path);
          }
        }
      };

      var lblTitle = new Label
      {
        Text = "Truy Cập Nhanh",
        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        ForeColor = Color.FromArgb(40, 50, 60),
        Location = new Point(20, 15),
        AutoSize = true
      };

      // Get real data for shortcuts
      var totalReports = reportService?.GetAllReports()?.Count() ?? 0;
      var totalUsers = userService?.GetAllUsers()?.Count() ?? 0;
      var totalPosts = postService?.GetAllPosts()?.Count() ?? 0;
      var allComments = Comment.GetList("datas\\Comment.csv");
      var totalComments = allComments.Count;
      
      // Add shortcut buttons with real data
      var shortcuts = new[]
      {
        new { Text = $"Báo cáo ({totalReports})", Icon = "!", Color = Color.FromArgb(231, 76, 60), Tag = "reports" },
        new { Text = $"Người dùng ({totalUsers})", Icon = "U", Color = Color.FromArgb(52, 152, 219), Tag = "users" },
        new { Text = $"Bài viết ({totalPosts})", Icon = "P", Color = Color.FromArgb(46, 204, 113), Tag = "posts" },
        new { Text = $"Bình luận ({totalComments})", Icon = "C", Color = Color.FromArgb(155, 89, 182), Tag = "analytics" }
      };

      int yPos = 45;
      foreach (var shortcut in shortcuts)
      {
        var btn = new Panel
        {
          Size = new Size(panel.Width - 45, 38),
          Location = new Point(20, yPos),
          BackColor = Color.FromArgb(248, 250, 252),
          Cursor = Cursors.Hand,
          Tag = shortcut.Tag
        };

        btn.Paint += (s, e) =>
        {
          var g = e.Graphics;
          g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
          if (btn.ClientRectangle.Width > 0 && btn.ClientRectangle.Height > 0)
          {
            using (var path = GetRoundedRect(btn.ClientRectangle, 6))
            {
              using (var brush = new SolidBrush(btn.BackColor))
              {
                g.FillPath(brush, path);
              }
            }
          }
        };

        // Add hover and click events to panel and all child controls
        AddHoverAndClickEvents(btn, shortcut.Tag);
        btn.Tag = shortcut.Tag; // Ensure tag is set for navigation

        // Icon circle
        var iconPanel = new Panel
        {
          Size = new Size(26, 26),
          Location = new Point(8, 6),
          BackColor = Color.FromArgb(30, shortcut.Color)
        };
        iconPanel.Paint += (s, e) =>
        {
          e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
          using (var brush = new SolidBrush(Color.FromArgb(30, shortcut.Color)))
          {
            e.Graphics.FillEllipse(brush, 0, 0, 25, 25);
          }
        };

        var lblIcon = new Label
        {
          Text = shortcut.Icon,
          Font = new Font("Segoe UI", 11F, FontStyle.Bold),
          ForeColor = shortcut.Color,
          Size = new Size(26, 26),
          TextAlign = ContentAlignment.MiddleCenter,
          BackColor = Color.Transparent,
          Parent = iconPanel
        };

        var lblText = new Label
        {
          Text = shortcut.Text,
          Font = new Font("Segoe UI", 9F),
          ForeColor = Color.FromArgb(60, 70, 80),
          Location = new Point(40, 10),
          AutoSize = true,
          BackColor = Color.Transparent,
          Parent = btn
        };

        btn.Controls.Add(iconPanel);
        panel.Controls.Add(btn);
        yPos += 42;
      }

      panel.Controls.Add(lblTitle);

      return panel;
    }

    private Panel CreateRecentActivitiesPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(10, 0, 0, 0),
        Dock = DockStyle.Fill,
        Padding = new Padding(20),
        AutoScroll = true
      };

      panel.Paint += (s, e) =>
      {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        using (var path = GetRoundedRect(panel.ClientRectangle, 8))
        {
          using (var bgBrush = new SolidBrush(Color.White))
          {
            g.FillPath(bgBrush, path);
          }
          using (var borderPen = new Pen(Color.FromArgb(230, 235, 240), 1))
          {
            g.DrawPath(borderPen, path);
          }
        }
      };

      var lblTitle = new Label
      {
        Text = "Hoạt Động Gần Đây",
        Font = new Font("Segoe UI", 11F, FontStyle.Bold),
        ForeColor = Color.FromArgb(40, 50, 60),
        Location = new Point(20, 15),
        AutoSize = true
      };

      // Container for activities - using FlowLayoutPanel for better scrolling
      var activitiesContainer = new FlowLayoutPanel
      {
        Location = new Point(20, 45),
        Size = new Size(panel.Width - 40, panel.Height - 60),
        BackColor = Color.Transparent,
        AutoScroll = true,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
        Name = "recentActivitiesContainer"
      };

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(activitiesContainer);

      return panel;
    }

    private Panel CreateTopActivitiesPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Margin = new Padding(10, 0, 0, 0),
        Dock = DockStyle.Fill,
        AutoScroll = true,
        Name = "topActivitiesPanel"
      };

      panel.Paint += (sender, e) => DrawModernCard(e.Graphics, panel.ClientRectangle, Color.FromArgb(46, 204, 113));

      var lblTitle = new Label
      {
        Text = "🏆 Top hoạt động",
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        AutoSize = true
      };

      // Tab selector
      var tabPanel = new Panel
      {
        Location = new Point(20, 45),
        Size = new Size(panel.Width - 40, 30),
        BackColor = Color.Transparent,
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
      };

      var tabs = new[] { "👤 Người dùng", "🔥 Bài hot", "⚠️ Báo cáo" };
      for (int i = 0; i < tabs.Length; i++)
      {
        var btn = new Button
        {
          Text = tabs[i],
          Size = new Size(80, 25),
          Location = new Point(i * 85, 0),
          BackColor = i == 0 ? Color.FromArgb(52, 152, 219) : Color.FromArgb(236, 240, 241),
          ForeColor = i == 0 ? Color.White : Color.FromArgb(52, 73, 94),
          Font = new Font("Segoe UI", 7F, FontStyle.Bold),
          FlatStyle = FlatStyle.Flat,
          Cursor = Cursors.Hand,
          Tag = i
        };
        btn.FlatAppearance.BorderSize = 0;
        btn.Click += TopActivityTab_Click;
        tabPanel.Controls.Add(btn);
      }

      var contentPanel = new Panel
      {
        Location = new Point(20, 80),
        Size = new Size(panel.Width - 40, panel.Height - 95),
        BackColor = Color.Transparent,
        AutoScroll = true,
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
        Name = "topActivitiesContent"
      };

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(tabPanel);
      panel.Controls.Add(contentPanel);

      return panel;
    }

    // This method is now handled by the first ShortcutButton_Click method above

    private void TopActivityTab_Click(object sender, EventArgs e)
    {
      var btn = sender as Button;
      if (btn == null || btn.Parent == null) return;

      int selectedTab = (int)btn.Tag;

      // Update button styles
      foreach (Button tabBtn in btn.Parent.Controls.OfType<Button>())
      {
        tabBtn.BackColor = tabBtn == btn 
          ? Color.FromArgb(52, 152, 219) 
          : Color.FromArgb(236, 240, 241);
        tabBtn.ForeColor = tabBtn == btn 
          ? Color.White 
          : Color.FromArgb(52, 73, 94);
      }

      // Load selected tab content
      UpdateTopActivities(selectedTab);
    }

    private void CmbTimeFilter_Changed(object sender, EventArgs e)
    {
      if (cmbTimeFilter == null) return;

      selectedTimeFilter = cmbTimeFilter.SelectedIndex switch
      {
        0 => "24h",
        1 => "7days",
        2 => "30days",
        3 => "custom",
        _ => "24h"
      };

      // Refresh chart
      chartContainer?.Invalidate();
      
      // Reload data
      LoadDashboardDataAsync();
    }

    private Panel CreateQuickStatsSection()
    {
      var container = new TableLayoutPanel
      {
        ColumnCount = 3,
        RowCount = 1,
        Dock = DockStyle.Fill,
        BackColor = Color.Transparent,
        CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
        Padding = new Padding(0)
      };

      for (int i = 0; i < 3; i++)
      {
        container.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
      }
      container.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

      var stats = new[]
      {
        new { Title = "Tỷ Lệ Tương Tác", Value = "0%", Icon = "~", Color = Color.FromArgb(52, 152, 219), Key = "engagement" },
        new { Title = "Tăng Trưởng", Value = "0%", Icon = "+", Color = Color.FromArgb(46, 204, 113), Key = "growth" },
        new { Title = "Thời Gian Trung Bình", Value = "0m", Icon = "T", Color = Color.FromArgb(230, 126, 34), Key = "avgTime" }
      };

      for (int i = 0; i < stats.Length; i++)
      {
        var stat = stats[i];
        var cardContainer = new Panel
        {
          Dock = DockStyle.Fill,
          BackColor = Color.Transparent,
          Padding = new Padding(0, 0, i < 2 ? 15 : 0, 0)
        };

        var card = CreateQuickStatCard(stat.Title, stat.Value, stat.Icon, stat.Color, stat.Key);
        card.Dock = DockStyle.Fill;
        cardContainer.Controls.Add(card);
        container.Controls.Add(cardContainer, i, 0);
      }

      return container;
    }

    private Panel CreateQuickStatCard(string title, string value, string icon, Color accentColor, string key)
    {
      var card = new Panel
      {
        BackColor = Color.White,
        Padding = new Padding(20),
        Name = $"quickStat_{key}"
      };

      card.Paint += (s, e) =>
      {
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        using (var path = GetRoundedRect(card.ClientRectangle, 8))
        {
          using (var bgBrush = new SolidBrush(Color.White))
          {
            g.FillPath(bgBrush, path);
          }
          using (var borderPen = new Pen(Color.FromArgb(230, 235, 240), 1))
          {
            g.DrawPath(borderPen, path);
          }
        }

        // Top accent bar
        using (var accentBrush = new SolidBrush(accentColor))
        {
          var rect = new Rectangle(0, 0, card.Width, 3);
          g.FillRectangle(accentBrush, rect);
        }
      };

      // Icon
      var iconPanel = new Panel
      {
        Size = new Size(45, 45),
        Location = new Point(15, 15),
        BackColor = Color.FromArgb(15, accentColor)
      };
      iconPanel.Paint += (s, e) =>
      {
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(Color.FromArgb(15, accentColor)))
        {
          e.Graphics.FillEllipse(brush, 0, 0, 44, 44);
        }
      };

      var lblIcon = new Label
      {
        Text = icon,
        Font = new Font("Segoe UI", 18F, FontStyle.Bold),
        ForeColor = accentColor,
        Size = new Size(45, 45),
        TextAlign = ContentAlignment.MiddleCenter,
        BackColor = Color.Transparent,
        Parent = iconPanel
      };

      // Title
      var lblTitle = new Label
      {
        Text = title,
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(100, 110, 120),
        Location = new Point(70, 15),
        AutoSize = true,
        BackColor = Color.Transparent
      };

      // Value
      var lblValue = new Label
      {
        Text = value,
        Font = new Font("Segoe UI", 20F, FontStyle.Bold),
        ForeColor = accentColor,
        Location = new Point(70, 35),
        AutoSize = true,
        Name = $"value_{key}",
        BackColor = Color.Transparent
      };

      // Store in dictionary for updates
      if (!metricLabels.ContainsKey(key))
        metricLabels[key] = lblValue;

      card.Controls.Add(iconPanel);
      card.Controls.Add(lblTitle);
      card.Controls.Add(lblValue);

      return card;
    }

    private Panel CreateRecentPostsSection()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        Dock = DockStyle.Top,
        AutoScroll = true
      };

      panel.Paint += (sender, e) => DrawModernCard(e.Graphics, panel.ClientRectangle, Color.FromArgb(155, 89, 182));

      var lblTitle = new Label
      {
        Text = "📌 Bài viết gần đây",
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        AutoSize = true
      };

      var postsContainer = new FlowLayoutPanel
      {
        Location = new Point(20, 50),
        Size = new Size(panel.Width - 40, panel.Height - 70),
        BackColor = Color.Transparent,
        AutoScroll = true,
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
        Name = "recentPostsContainer",
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false
      };

      panel.Controls.Add(lblTitle);
      panel.Controls.Add(postsContainer);

      return panel;
    }
    #endregion

    #region Data Loading
    private async Task LoadDashboardDataAsync()
    {
      try
      {
        // Show loading state
        ShowLoadingState();

        // Load data asynchronously
        await Task.Run(() =>
        {
          var stats = dashboardService?.GetDashboardStatistics();
          if (stats != null)
          {
            // Update UI on UI thread
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate
            {
              UpdateMetrics(stats);
              UpdateQuickStats(stats);
              UpdateRecentActivity();
              UpdateRecentPosts();
              UpdateLastUpdateTime();
            });
          }
        });
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading dashboard data: {ex.Message}");
        this.Invoke((System.Windows.Forms.MethodInvoker)delegate
        {
          MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Warning);
        });
      }
    }

    private void ShowLoadingState()
    {
      foreach (var label in metricLabels.Values)
      {
        label.Text = "⏳";
      }
    }

    private void UpdateMetrics(DashboardStatistics stats)
    {
      // Update main metrics
      if (metricLabels.ContainsKey("totalUsers"))
        metricLabels["totalUsers"].Text = dashboardService.FormatNumber(stats.TotalUsers);

      if (metricLabels.ContainsKey("postsToday"))
        metricLabels["postsToday"].Text = stats.PostsToday.ToString();

      if (metricLabels.ContainsKey("pendingReports"))
      {
        // Get pending reports count from ReportService (all reports are considered pending)
        var allReports = reportService?.GetAllReports() ?? new List<SocialManager.models.Report>();
        var pendingCount = allReports.Count();
        metricLabels["pendingReports"].Text = pendingCount.ToString();
      }

      if (metricLabels.ContainsKey("newComments"))
      {
        // Get comments today from Comment model
        var allComments = Comment.GetList("datas\\Comment.csv");
        var today = DateTime.Now.Date;
        var commentsToday = allComments.Count(c => c.CreatedAt.Date == today);
        metricLabels["newComments"].Text = commentsToday.ToString();
      }

      // Update sub texts
      if (metricLabels.ContainsKey("totalUsers_sub"))
        metricLabels["totalUsers_sub"].Text = $"{stats.ActiveUsers} online";

      if (metricLabels.ContainsKey("postsToday_sub"))
        metricLabels["postsToday_sub"].Text = "Mới nhất";

      if (metricLabels.ContainsKey("pendingReports_sub"))
      {
        var allReports = reportService?.GetAllReports() ?? new List<SocialManager.models.Report>();
        var pendingCount = allReports.Count();
        metricLabels["pendingReports_sub"].Text = pendingCount > 0 ? "Cần xử lý" : "Ổn định";
      }

      if (metricLabels.ContainsKey("newComments_sub"))
        metricLabels["newComments_sub"].Text = "24h qua";

      // Update alerts, shortcuts and top activities
      UpdateAlertsPanel();
      RefreshShortcutsPanel();
      UpdateTopActivities(0);
    }

    private void UpdateQuickStats(DashboardStatistics stats)
    {
      // Calculate engagement rate (likes + comments) / total posts
      var allPosts = postService?.GetAllPosts() ?? new List<Post>();
      var allComments = Comment.GetList("datas\\Comment.csv");
      
      double engagementRate = 0;
      if (allPosts.Count > 0)
      {
        var totalLikes = allPosts.Sum(p => p.LikesCount);
        var totalComments = allComments.Count;
        engagementRate = ((totalLikes + totalComments) / (double)allPosts.Count / 10) * 100; // Scale to reasonable %
      }

      // Calculate growth rate (new posts this week vs last week)
      var today = DateTime.Now;
      var thisWeekPosts = allPosts.Count(p => p.CreatedAt >= today.AddDays(-7));
      var lastWeekPosts = allPosts.Count(p => p.CreatedAt >= today.AddDays(-14) && p.CreatedAt < today.AddDays(-7));
      
      double growthRate = 0;
      if (lastWeekPosts > 0)
      {
        growthRate = ((thisWeekPosts - lastWeekPosts) / (double)lastWeekPosts) * 100;
      }
      else if (thisWeekPosts > 0)
      {
        growthRate = 100;
      }

      // Calculate average time between posts (in minutes)
      double avgTime = 0;
      if (allPosts.Count > 1)
      {
        var orderedPosts = allPosts.OrderBy(p => p.CreatedAt).ToList();
        var totalMinutes = 0.0;
        for (int i = 1; i < orderedPosts.Count; i++)
        {
          totalMinutes += (orderedPosts[i].CreatedAt - orderedPosts[i - 1].CreatedAt).TotalMinutes;
        }
        avgTime = totalMinutes / (orderedPosts.Count - 1);
      }

      // Update quick stat labels
      if (metricLabels.ContainsKey("engagement"))
        metricLabels["engagement"].Text = $"{engagementRate:F1}%";

      if (metricLabels.ContainsKey("growth"))
      {
        var sign = growthRate >= 0 ? "+" : "";
        metricLabels["growth"].Text = $"{sign}{growthRate:F1}%";
        metricLabels["growth"].ForeColor = growthRate >= 0 ? 
          Color.FromArgb(46, 204, 113) : Color.FromArgb(231, 76, 60);
      }

      if (metricLabels.ContainsKey("avgTime"))
      {
        if (avgTime < 60)
          metricLabels["avgTime"].Text = $"{avgTime:F0}m";
        else if (avgTime < 1440)
          metricLabels["avgTime"].Text = $"{(avgTime / 60):F1}h";
        else
          metricLabels["avgTime"].Text = $"{(avgTime / 1440):F1}d";
      }
    }

    private void UpdateAlertsPanel()
    {
      var alertsFlow = this.Controls.Find("alertsFlow", true).FirstOrDefault() as FlowLayoutPanel;
      if (alertsFlow == null) return;

      alertsFlow.Controls.Clear();
      
      // Add real notifications with navigation
      AddSampleNotifications(alertsFlow);
    }
    
    private void RefreshShortcutsPanel()
    {
      // Find and refresh shortcuts panel with updated data
      var shortcutsPanel = this.Controls.Find("shortcutsPanel", true).FirstOrDefault();
      if (shortcutsPanel != null)
      {
        var parent = shortcutsPanel.Parent;
        if (parent != null)
        {
          var newShortcutsPanel = CreateShortcutsPanel();
          newShortcutsPanel.Name = "shortcutsPanel";
          
          parent.Controls.Remove(shortcutsPanel);
          parent.Controls.Add(newShortcutsPanel);
        }
      }
    }

    private Panel CreateAlertBadge(string text, Color accentColor)
    {
      var badge = new Panel
      {
        Size = new Size(0, 44), // Auto width, fixed height
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        BackColor = Color.FromArgb(25, accentColor),
        Margin = new Padding(0, 0, 0, 8),
        Padding = new Padding(12, 8, 12, 8),
        Cursor = Cursors.Hand,
        Tag = accentColor
      };

      badge.Paint += (s, e) =>
      {
        // Only paint if we have valid size
        if (badge.Width <= 0 || badge.Height <= 0) return;
        
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        
        var rect = badge.ClientRectangle;
        if (rect.Width > 0 && rect.Height > 0)
        {
          using (var path = GetRoundedRect(rect, 6))
          {
            using (var brush = new SolidBrush(badge.BackColor))
            {
              e.Graphics.FillPath(brush, path);
            }
            // Add border
            using (var pen = new Pen(accentColor, 1))
            {
              e.Graphics.DrawPath(pen, path);
            }
          }
        }
      };

      var label = new Label
      {
        Text = text,
        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
        ForeColor = accentColor,
        AutoSize = true,
        BackColor = Color.Transparent,
        Cursor = Cursors.Hand,
        Dock = DockStyle.Fill
      };

      // Hover effects
      badge.MouseEnter += (s, e) =>
      {
        badge.BackColor = Color.FromArgb(50, accentColor);
        badge.Invalidate();
      };

      badge.MouseLeave += (s, e) =>
      {
        badge.BackColor = Color.FromArgb(25, accentColor);
        badge.Invalidate();
      };

      // Click handler
      EventHandler clickHandler = (s, e) =>
      {
        if (text.Contains("báo cáo"))
        {
          MessageBox.Show("Chuyển đến quản lý báo cáo", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (text.Contains("bài"))
        {
          MessageBox.Show("Chuyển đến quản lý bài viết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (text.Contains("bình luận"))
        {
          MessageBox.Show("Xem tất cả bình luận mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      };

      badge.Click += clickHandler;
      label.Click += clickHandler; // Forward click from label to badge

      badge.Controls.Add(label);
      return badge;
    }

    private void UpdateRecentActivity()
    {
      try
      {
        var activityContainer = this.Controls.Find("recentActivitiesContainer", true).FirstOrDefault() as FlowLayoutPanel;
        if (activityContainer == null) 
        {
          System.Diagnostics.Debug.WriteLine("❌ recentActivitiesContainer not found!");
          return;
        }

        System.Diagnostics.Debug.WriteLine($"✅ Found recentActivitiesContainer: {activityContainer.Width}x{activityContainer.Height}");

        activityContainer.SuspendLayout();
        activityContainer.Controls.Clear();

        var allPosts = postService?.GetAllPosts();
        System.Diagnostics.Debug.WriteLine($"📊 Total posts from service: {allPosts?.Count ?? 0}");

        var posts = allPosts?
          .Where(p => !p.IsDeleted)
          .OrderByDescending(p => p.CreatedAt)
          .Take(10) // Show 10 items for scrolling demo
          .ToList();

        System.Diagnostics.Debug.WriteLine($"📝 Filtered posts to display: {posts?.Count ?? 0}");

        if (posts == null || !posts.Any())
        {
          var noActivity = new Label
          {
            Text = "Chưa có hoạt động nào",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.FromArgb(149, 165, 166),
            AutoSize = true,
            Margin = new Padding(5)
          };
          activityContainer.Controls.Add(noActivity);
          System.Diagnostics.Debug.WriteLine("ℹ️ No activities to display");
          activityContainer.ResumeLayout();
          return;
        }

        foreach (var post in posts)
        {
          var activityItem = CreateActivityItem(post);
          activityContainer.Controls.Add(activityItem);
          System.Diagnostics.Debug.WriteLine($"➕ Added activity: {post.Content?.Substring(0, Math.Min(30, post.Content?.Length ?? 0))}...");
        }

        activityContainer.ResumeLayout();
        System.Diagnostics.Debug.WriteLine($"✅ UpdateRecentActivity completed with {posts.Count} items");
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"❌ Error in UpdateRecentActivity: {ex.Message}");
        System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
      }
    }

    private Panel CreateActivityItem(Post post)
    {
      // Get user name from UserService
      var user = userService?.GetUserById(post.UserID);
      var userName = user?.FullName ?? "Người dùng";
      
      // Debug: Check CreatedAt
      System.Diagnostics.Debug.WriteLine($"📅 Post CreatedAt: {post.CreatedAt:yyyy-MM-dd HH:mm:ss}, Now: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
      var timeSpan = DateTime.Now - post.CreatedAt;
      System.Diagnostics.Debug.WriteLine($"⏱️ Time difference: {timeSpan.TotalDays} days, {timeSpan.TotalHours} hours, {timeSpan.TotalMinutes} minutes");
      
      var panel = new Panel
      {
        Size = new Size(530, 50), // Increased height for better spacing
        BackColor = Color.Transparent,
        Cursor = Cursors.Hand,
        Margin = new Padding(0, 0, 0, 3),
        Tag = post
      };

      // Icon badge (left side)
      var iconPanel = new Panel
      {
        Size = new Size(28, 28),
        Location = new Point(8, 11),
        BackColor = Color.FromArgb(230, 240, 255)
      };
      
      iconPanel.Paint += (s, e) =>
      {
        // Only paint if we have valid size
        if (iconPanel.Width <= 0 || iconPanel.Height <= 0) return;
        
        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(Color.FromArgb(230, 240, 255)))
        {
          e.Graphics.FillEllipse(brush, 0, 0, 27, 27);
        }
        using (var pen = new Pen(Color.FromArgb(52, 152, 219), 2))
        {
          e.Graphics.DrawEllipse(pen, 1, 1, 25, 25);
        }
      };

      var iconLabel = new Label
      {
        Text = "📝",
        Font = new Font("Segoe UI", 10F),
        Size = new Size(28, 28),
        TextAlign = ContentAlignment.MiddleCenter,
        BackColor = Color.Transparent,
        Parent = iconPanel
      };

      // User name and action (first line)
      var lblUser = new Label
      {
        Text = $"{userName} đã đăng bài viết",
        Font = new Font("Segoe UI", 9F, FontStyle.Regular),
        ForeColor = Color.FromArgb(44, 62, 80),
        AutoSize = true,
        Location = new Point(45, 8),
        MaximumSize = new Size(340, 20),
        BackColor = Color.Transparent
      };

      // Content (second line)
      var content = post.Content?.Length > 45 ? post.Content.Substring(0, 45) + "..." : (post.Content ?? "Bài viết mới");
      var lblContent = new Label
      {
        Text = content,
        Font = new Font("Segoe UI", 8.5F),
        ForeColor = Color.FromArgb(127, 140, 141),
        Location = new Point(45, 26),
        Size = new Size(340, 16),
        AutoEllipsis = true,
        BackColor = Color.Transparent
      };

      // Time label (right side) - Fixed time display
      var timeText = TimeHelper.GetRelativeTime(post.CreatedAt);
      var lblTime = new Label
      {
        Text = timeText,
        Font = new Font("Segoe UI", 7.5F),
        ForeColor = Color.FromArgb(149, 165, 166),
        AutoSize = false,
        Size = new Size(120, 16),
        TextAlign = ContentAlignment.TopRight,
        BackColor = Color.Transparent
      };
      lblTime.Location = new Point(panel.Width - lblTime.Width - 10, 8);

      panel.Controls.Add(iconPanel);
      panel.Controls.Add(lblUser);
      panel.Controls.Add(lblContent);
      panel.Controls.Add(lblTime);

      // Click handler
      panel.Click += (s, e) =>
      {
        MessageBox.Show($"Bài viết của {userName}:\n\n{post.Content}\n\nĐăng lúc: {post.CreatedAt:dd/MM/yyyy HH:mm}", 
          "Chi tiết bài viết", MessageBoxButtons.OK, MessageBoxIcon.Information);
      };

      // Hover effect with separator line
      panel.Paint += (s, e) =>
      {
        // Only paint if we have valid size
        if (panel.Width <= 0 || panel.Height <= 0) return;
        
        if (panel.BackColor != Color.Transparent)
        {
          using (var brush = new SolidBrush(Color.FromArgb(245, 250, 252)))
          {
            e.Graphics.FillRectangle(brush, panel.ClientRectangle);
          }
        }
        // Draw separator line at bottom
        using (var pen = new Pen(Color.FromArgb(230, 235, 240), 1))
        {
          e.Graphics.DrawLine(pen, 5, panel.Height - 1, panel.Width - 5, panel.Height - 1);
        }
      };

      panel.MouseEnter += (s, e) => 
      { 
        panel.BackColor = Color.FromArgb(245, 250, 252); 
        iconPanel.BackColor = Color.FromArgb(210, 230, 255);
        iconPanel.Invalidate();
        panel.Invalidate(); 
      };
      
      panel.MouseLeave += (s, e) => 
      { 
        panel.BackColor = Color.Transparent; 
        iconPanel.BackColor = Color.FromArgb(230, 240, 255);
        iconPanel.Invalidate();
        panel.Invalidate(); 
      };

      return panel;
    }

    private void UpdateRecentPosts()
    {
      var postsContainer = this.Controls.Find("recentPostsContainer", true).FirstOrDefault() as FlowLayoutPanel;
      if (postsContainer == null) return;

      postsContainer.Controls.Clear();

      var posts = postService?.GetAllPosts()
        .OrderByDescending(p => p.CreatedAt)
        .Take(3)
        .ToList();

      if (posts == null || !posts.Any())
      {
        var noPost = new Label
        {
          Text = "Chưa có bài viết nào",
          Font = new Font("Segoe UI", 10F, FontStyle.Italic),
          ForeColor = Color.FromArgb(149, 165, 166),
          Margin = new Padding(0, 10, 0, 0)
        };
        postsContainer.Controls.Add(noPost);
        return;
      }

      foreach (var post in posts)
      {
        var postCard = CreateRecentPostCard(post);
        postsContainer.Controls.Add(postCard);
      }
    }

    private Panel CreateRecentPostCard(Post post)
    {
      // Find the postsContainer from the main panel
      var recentPostsPanel = mainScrollPanel?.Controls.OfType<Panel>()
        .FirstOrDefault()?.Controls.OfType<Panel>()
        .FirstOrDefault(p => p.Controls.OfType<FlowLayoutPanel>().Any(f => f.Name == "recentPostsContainer"));
      
      var postsContainer = recentPostsPanel?.Controls.OfType<FlowLayoutPanel>()
        .FirstOrDefault(f => f.Name == "recentPostsContainer");
      
      var card = new Panel
      {
        Size = new Size(postsContainer?.Width - 20 ?? 700, 70),
        BackColor = Color.FromArgb(250, 252, 255),
        Margin = new Padding(0, 0, 0, 10),
        Cursor = Cursors.Hand
      };

      card.Paint += (s, e) =>
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(Color.FromArgb(236, 240, 241), 1))
        {
          e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
        }
      };

      // Get user name from UserService
      var user = userService?.GetUserById(post.UserID);
      var userName = user?.FullName ?? "Unknown";

      var lblUser = new Label
      {
        Text = $"👤 {userName}",
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        ForeColor = Color.FromArgb(52, 73, 94),
        Location = new Point(15, 10),
        AutoSize = true
      };

      var lblContent = new Label
      {
        Text = post.Content?.Length > 80 ? post.Content.Substring(0, 80) + "..." : post.Content,
        Font = new Font("Segoe UI", 9F),
        ForeColor = Color.FromArgb(84, 102, 115),
        Location = new Point(15, 30),
        Size = new Size(card.Width - 250, 20)
      };

      var lblStats = new Label
      {
        Text = $"💬 {post.CommentsCount}  ❤️ {post.LikesCount}  📅 {post.CreatedAt:dd/MM/yyyy}",
        Font = new Font("Segoe UI", 8F),
        ForeColor = Color.FromArgb(127, 140, 141),
        Location = new Point(15, 50),
        AutoSize = true
      };

      card.Controls.Add(lblUser);
      card.Controls.Add(lblContent);
      card.Controls.Add(lblStats);

      // Hover effect
      card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(245, 247, 250);
      card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(250, 252, 255);

      return card;
    }

    private void UpdateLastUpdateTime()
    {
      var lblLastUpdate = this.Controls.Find("lblLastUpdate", true).FirstOrDefault() as Label;
      if (lblLastUpdate != null)
      {
        lblLastUpdate.Text = $"Cập nhật lúc: {DateTime.Now:HH:mm:ss dd/MM/yyyy}";
      }
    }
    #endregion

    #region Drawing Methods
    private void DrawModernCard(Graphics g, Rectangle rect, Color accentColor)
    {
      g.SmoothingMode = SmoothingMode.AntiAlias;

      // Draw shadow
      using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
      {
        g.FillRectangle(shadowBrush, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height));
      }

      // Draw card background
      int radius = 12;
      using (var brush = new SolidBrush(Color.White))
      {
        GraphicsExtensions.FillRoundedRectangle(g, brush, rect, radius);
      }

      // Draw border
      using (var pen = new Pen(Color.FromArgb(230, 236, 240), 1))
      {
        GraphicsExtensions.DrawRoundedRectangle(g, pen, rect, radius);
      }

      // Draw accent line on left
      using (var brush = new SolidBrush(accentColor))
      {
        g.FillRectangle(brush, new Rectangle(rect.X, rect.Y + 10, 4, 30));
      }
    }

    private void DrawSimpleBarChart(object sender, PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;

      var chartPanel = sender as Panel;
      if (chartPanel == null) return;

      int width = chartPanel.Width;
      int height = chartPanel.Height;

      // Get last 7 days data
      var posts = postService?.GetAllPosts();
      if (posts == null) return;

      var last7Days = Enumerable.Range(0, 7)
        .Select(i => DateTime.Now.Date.AddDays(-6 + i))
        .ToList();

      var dailyPosts = last7Days.Select(day => new
      {
        Day = day,
        Count = posts.Count(p => p.CreatedAt.Date == day)
      }).ToList();

      int maxCount = dailyPosts.Max(d => d.Count);
      if (maxCount == 0) maxCount = 1;

      int barWidth = (width - 80) / 7;
      int chartHeight = height - 50;

      // Draw bars
      for (int i = 0; i < dailyPosts.Count; i++)
      {
        var data = dailyPosts[i];
        int barHeight = (int)((double)data.Count / maxCount * chartHeight);
        int x = 40 + i * barWidth;
        
        // Skip if barHeight is 0 or negative
        if (barHeight <= 0)
        {
          // Still draw the day label even if no data
          using (var font = new Font("Segoe UI", 7F))
          using (var brush = new SolidBrush(Color.FromArgb(127, 140, 141)))
          {
            var text = data.Day.ToString("dd/MM");
            var size = g.MeasureString(text, font);
            g.DrawString(text, font, brush, x + (barWidth - 10 - size.Width) / 2, chartHeight + 15);
          }
          continue;
        }
        
        int y = chartHeight - barHeight + 10;

        // Draw bar with gradient - only if barHeight > 0
        using (var brush = new LinearGradientBrush(
          new Rectangle(x, y, barWidth - 10, barHeight),
          Color.FromArgb(52, 152, 219),
          Color.FromArgb(41, 128, 185),
          LinearGradientMode.Vertical))
        {
          g.FillRectangle(brush, x, y, barWidth - 10, barHeight);
        }

        // Draw value on top
        using (var font = new Font("Segoe UI", 8F, FontStyle.Bold))
        using (var brush = new SolidBrush(Color.FromArgb(52, 73, 94)))
        {
          var text = data.Count.ToString();
          var size = g.MeasureString(text, font);
          g.DrawString(text, font, brush, x + (barWidth - 10 - size.Width) / 2, y - 20);
        }

        // Draw day label
        using (var font = new Font("Segoe UI", 7F))
        using (var brush = new SolidBrush(Color.FromArgb(127, 140, 141)))
        {
          var text = data.Day.ToString("dd/MM");
          var size = g.MeasureString(text, font);
          g.DrawString(text, font, brush, x + (barWidth - 10 - size.Width) / 2, chartHeight + 15);
        }
      }

      // Draw axis
      using (var pen = new Pen(Color.FromArgb(189, 195, 199), 1))
      {
        g.DrawLine(pen, 40, chartHeight + 10, width - 20, chartHeight + 10); // X axis
        g.DrawLine(pen, 40, 10, 40, chartHeight + 10); // Y axis
      }
    }
    #endregion

    #region Event Handlers
    private void ExportReport_Click(object sender, EventArgs e)
    {
      try
      {
        var saveDialog = new SaveFileDialog
        {
          Filter = "CSV Files (*.csv)|*.csv|Text Files (*.txt)|*.txt",
          FileName = $"Dashboard_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
          Title = "Xuất báo cáo Dashboard"
        };

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          var stats = dashboardService?.GetDashboardStatistics();
          if (stats != null)
          {
            var report = new StringBuilder();
            report.AppendLine("Dashboard Report - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            report.AppendLine("================================================");
            report.AppendLine($"Tổng số bài viết:,{stats.TotalPosts}");
            report.AppendLine($"Tổng người dùng:,{stats.TotalUsers}");
            report.AppendLine($"Người dùng hoạt động:,{stats.ActiveUsers}");
            report.AppendLine($"Tổng tương tác:,{stats.TotalInteractions}");
            report.AppendLine($"Tăng trưởng:,{stats.GrowthPercentage:F2}%");
            report.AppendLine($"Bài viết hôm nay:,{stats.PostsToday}");
            report.AppendLine($"TB tương tác/bài:,{stats.AverageInteractionsPerPost:F1}");
            report.AppendLine($"Ngày hoạt động cao nhất:,{stats.MostActiveDay}");
            report.AppendLine($"Số bài ngày đó:,{stats.MostActiveDayPosts}");

            File.WriteAllText(saveDialog.FileName, report.ToString());
            MessageBox.Show("Xuất báo cáo thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xuất báo cáo: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    protected override void OnVisibleChanged(EventArgs e)
    {
      base.OnVisibleChanged(e);
      if (this.Visible)
      {
        LoadDashboardDataAsync();
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        refreshTimer?.Stop();
        refreshTimer?.Dispose();
        components?.Dispose();
      }
      base.Dispose(disposing);
    }
    #endregion
  }
}

