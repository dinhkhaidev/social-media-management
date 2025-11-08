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

namespace SocialManager.frm.UserControls
{
  public partial class ucDashboard : UserControl
  {
    private DashboardService dashboardService;
    private UserService userService;
    private PostService postService;

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

        // If InitializeComponent fails, create a simple fallback
        this.Size = new Size(800, 600);
        this.BackColor = Color.FromArgb(247, 249, 252);

        var lblError = new Label
        {
          Text = $"Lỗi tải Bảng điều khiển\n\nChi tiết: {ex.Message}\n\nĐây là giao diện dự phòng. Một số chức năng có thể bị hạn chế.",
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
    }

    private void InitializeDashboard()
    {
      this.BackColor = Color.FromArgb(247, 249, 252);
      
      // Initialize services
      try
      {
        dashboardService = new DashboardService();
        userService = new UserService();
        postService = new PostService();
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error initializing services: {ex.Message}");
      }
      
      CreateModernDashboardLayout();
      LoadDashboardData();
    }

    private void CreateModernDashboardLayout()
    {
      this.Controls.Clear();
      
      var mainPanel = new Panel
      {
        Dock = DockStyle.Fill,
        BackColor = Color.FromArgb(247, 249, 252),
        Padding = new Padding(30)
      };

      // Header section
      var headerPanel = new Panel
      {
        Height = 60,
        Dock = DockStyle.Top,
        BackColor = Color.Transparent,
        Margin = new Padding(0, 0, 0, 20)
      };

      var lblTitle = new Label
      {
        Text = "📊 Bảng Điều Khiển Quản Trị",
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        AutoSize = true,
        Location = new Point(0, 0)
      };

      headerPanel.Controls.Add(lblTitle);

      // Metrics row
      var metricsPanel = new Panel
      {
        Height = 150,
        Dock = DockStyle.Top,
        BackColor = Color.Transparent,
        Margin = new Padding(0, 0, 0, 20)
      };

      // Create metric cards
      var totalPostsCard = CreateMetricCard("Tổng Bài Viết", "1,200", Color.FromArgb(52, 152, 219), 0);
      var growthCard = CreateMetricCard("Tăng Trưởng", "+12%", Color.FromArgb(46, 204, 113), 1);
      var interactionsCard = CreateMetricCard("Tương Tác", "85K", Color.FromArgb(230, 126, 34), 2);
      var reachCard = CreateMetricCard("Tiếp Cận", "125.4K", Color.FromArgb(155, 89, 182), 3);

      metricsPanel.Controls.Add(totalPostsCard);
      metricsPanel.Controls.Add(growthCard);
      metricsPanel.Controls.Add(interactionsCard);
      metricsPanel.Controls.Add(reachCard);

      // Summary section
      var summaryPanel = CreateSummaryPanel();
      summaryPanel.Height = 250;
      summaryPanel.Dock = DockStyle.Top;
      summaryPanel.Margin = new Padding(0, 0, 0, 20);

      // Action buttons
      var actionsPanel = CreateActionsPanel();
      actionsPanel.Height = 60;
      actionsPanel.Dock = DockStyle.Top;

      mainPanel.Controls.Add(headerPanel);
      mainPanel.Controls.Add(metricsPanel);
      mainPanel.Controls.Add(summaryPanel);
      mainPanel.Controls.Add(actionsPanel);

      this.Controls.Add(mainPanel);
    }

    private Panel CreateMetricCard(string title, string value, Color accentColor, int position)
    {
      var card = new Panel
      {
        Size = new Size(240, 120),
        Location = new Point(position * 260, 10),
        BackColor = Color.White,
        Name = $"card_{position}"
      };

      card.Paint += (sender, e) => DrawMetricCard(e.Graphics, card.ClientRectangle, accentColor);

      var lblTitle = new Label
      {
        Text = title,
        Font = new Font("Segoe UI", 10F, FontStyle.Regular),
        ForeColor = Color.FromArgb(127, 140, 141),
        Location = new Point(20, 20),
        Size = new Size(200, 25),
        AutoSize = false
      };

      var lblValue = new Label
      {
        Text = value,
        Font = new Font("Segoe UI", 20F, FontStyle.Bold),
        ForeColor = Color.FromArgb(52, 73, 94),
        Location = new Point(20, 50),
        Size = new Size(200, 35),
        AutoSize = false,
        Name = $"value_{position}"
      };

      card.Controls.Add(lblTitle);
      card.Controls.Add(lblValue);

      return card;
    }

    private void DrawMetricCard(Graphics g, Rectangle rect, Color accentColor)
    {
      g.SmoothingMode = SmoothingMode.AntiAlias;

      // Draw rounded rectangle for card
      int radius = 10;
      using (var brush = new SolidBrush(Color.White))
      {
        GraphicsExtensions.FillRoundedRectangle(g, brush, rect, radius);
      }

      using (var pen = new Pen(Color.FromArgb(220, 221, 222), 1))
      {
        GraphicsExtensions.DrawRoundedRectangle(g, pen, rect, radius);
      }

      // Draw accent bar on top
      using (var brush = new SolidBrush(accentColor))
      {
        g.FillRectangle(brush, new Rectangle(rect.X, rect.Y, rect.Width, 4));
      }
    }

    private Panel CreateSummaryPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.White,
        BorderStyle = BorderStyle.None
      };

      panel.Paint += (sender, e) =>
      {
        int radius = 10;
        GraphicsExtensions.DrawRoundedRectangle(e.Graphics, new Pen(Color.FromArgb(220, 221, 222), 1), e.ClipRectangle, radius);
      };

      var lblSummaryTitle = new Label
      {
        Text = "📈 Tóm Tắt Hoạt Động Gần Đây",
        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 15),
        Size = new Size(300, 30),
        AutoSize = false
      };

      var summaryText = new Label
      {
        Text = "• Bài viết mới: 5 bài trong ngày hôm nay\n" +
               "• Lượt tương tác: 245 (tăng 15% so với hôm qua)\n" +
               "• Người theo dõi mới: 12 người\n" +
               "• Bình luận chưa trả lời: 8 bình luận",
        Font = new Font("Segoe UI", 10F),
        ForeColor = Color.FromArgb(84, 102, 115),
        Location = new Point(20, 50),
        Size = new Size(400, 150),
        AutoSize = false
      };

      panel.Controls.Add(lblSummaryTitle);
      panel.Controls.Add(summaryText);

      return panel;
    }

    private Panel CreateActionsPanel()
    {
      var panel = new Panel
      {
        BackColor = Color.Transparent
      };

      var btnRefresh = new Button
      {
        Text = "🔄 Làm Mới",
        Location = new Point(0, 10),
        Size = new Size(120, 40),
        BackColor = Color.FromArgb(52, 152, 219),
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand
      };

      btnRefresh.Click += (s, e) => LoadDashboardData();
      btnRefresh.FlatAppearance.BorderSize = 0;

      var btnExport = new Button
      {
        Text = "📥 Xuất Báo Cáo",
        Location = new Point(130, 10),
        Size = new Size(130, 40),
        BackColor = Color.FromArgb(46, 204, 113),
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
        FlatStyle = FlatStyle.Flat,
        Cursor = Cursors.Hand
      };

      btnExport.FlatAppearance.BorderSize = 0;

      panel.Controls.Add(btnRefresh);
      panel.Controls.Add(btnExport);

      return panel;
    }

    private void LoadDashboardData()
    {
      try
      {
        // Update metric card values
        var cards = this.Controls.OfType<Panel>()
          .SelectMany(p => p.Controls.OfType<Panel>())
          .Where(c => c.Name?.StartsWith("card_") == true)
          .ToList();

        if (cards.Count >= 4)
        {
          UpdateCardValue(cards[0], "1,200");
          UpdateCardValue(cards[1], "+12%");
          UpdateCardValue(cards[2], "85,000");
          UpdateCardValue(cards[3], "125.4K");
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading dashboard data: {ex}");
        MessageBox.Show($"Lỗi khi tải dữ liệu bảng điều khiển: {ex.Message}", "Lỗi bảng điều khiển",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void UpdateCardValue(Panel card, string value)
    {
      var valueLabel = card.Controls.OfType<Label>().FirstOrDefault(l => l.Name?.StartsWith("value_") == true);
      if (valueLabel != null)
        valueLabel.Text = value;
    }
  }
}
