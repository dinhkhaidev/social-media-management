using SocialManager.utils;
using SocialManager.services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace SocialManager.frm.UserControls
{
  // Extended methods for ucDashboard - Best Practices Implementation
  public partial class ucDashboard
  {
    #region Top Activities Methods
    private void UpdateTopActivities(int tabIndex = 0)
    {
      var contentPanel = topActivitiesPanel?.Controls.Find("topActivitiesContent", false).FirstOrDefault() as Panel;
      if (contentPanel == null) return;

      contentPanel.Controls.Clear();

      switch (tabIndex)
      {
        case 0: // Top Users
          LoadTopUsers(contentPanel);
          break;
        case 1: // Hot Posts
          LoadHotPosts(contentPanel);
          break;
        case 2: // Most Reported
          LoadMostReported(contentPanel);
          break;
      }
    }

    private void LoadTopUsers(Panel container)
    {
      try
      {
        var allPosts = postService?.GetAllPosts() ?? new List<Post>();
        var topUsers = allPosts
          .GroupBy(p => p.UserID)
          .Select(g => new
          {
            UserID = g.Key,
            PostCount = g.Count(),
            TotalInteractions = g.Sum(p => p.LikesCount + p.CommentsCount)
          })
          .OrderByDescending(u => u.TotalInteractions)
          .Take(5)
          .ToList();

        int yPos = 0;
        int rank = 1;
        foreach (var userStat in topUsers)
        {
          var user = userService?.GetUserById(userStat.UserID);
          var item = CreateTopUserItem(rank, user?.FullName ?? "Unknown", userStat.PostCount, userStat.TotalInteractions, yPos);
          container.Controls.Add(item);
          yPos += 55;
          rank++;
        }

        if (!topUsers.Any())
        {
          container.Controls.Add(new Label
          {
            Text = "Chưa có dữ liệu",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.FromArgb(149, 165, 166),
            Location = new Point(10, 10)
          });
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading top users: {ex.Message}");
      }
    }

    private Panel CreateTopUserItem(int rank, string userName, int posts, int interactions, int yPos)
    {
      var panel = new Panel
      {
        Size = new Size(250, 50),
        Location = new Point(0, yPos),
        BackColor = Color.FromArgb(250, 252, 255),
        Cursor = Cursors.Hand
      };

      panel.Paint += (s, e) =>
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(Color.FromArgb(236, 240, 241), 1))
        {
          e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
        }
      };

      // Rank badge
      var lblRank = new Label
      {
        Text = rank.ToString(),
        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
        ForeColor = rank <= 3 ? Color.FromArgb(230, 126, 34) : Color.FromArgb(149, 165, 166),
        Location = new Point(10, 12),
        Size = new Size(30, 25),
        TextAlign = ContentAlignment.MiddleCenter
      };

      var lblName = new Label
      {
        Text = userName,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold),
        ForeColor = Color.FromArgb(52, 73, 94),
        Location = new Point(45, 8),
        Size = new Size(150, 18),
        AutoSize = false
      };

      var lblStats = new Label
      {
        Text = $"📝 {posts} bài  💬 {interactions} tương tác",
        Font = new Font("Segoe UI", 7F),
        ForeColor = Color.FromArgb(127, 140, 141),
        Location = new Point(45, 28),
        AutoSize = true
      };

      panel.Controls.Add(lblRank);
      panel.Controls.Add(lblName);
      panel.Controls.Add(lblStats);

      panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 247, 250);
      panel.MouseLeave += (s, e) => panel.BackColor = Color.FromArgb(250, 252, 255);

      return panel;
    }

    private void LoadHotPosts(Panel container)
    {
      try
      {
        var hotPosts = postService?.GetAllPosts()
          .OrderByDescending(p => p.LikesCount + p.CommentsCount)
          .Take(5)
          .ToList();

        int yPos = 0;
        int rank = 1;
        foreach (var post in hotPosts ?? new List<Post>())
        {
          var item = CreateHotPostItem(rank, post, yPos);
          container.Controls.Add(item);
          yPos += 55;
          rank++;
        }

        if (hotPosts == null || !hotPosts.Any())
        {
          container.Controls.Add(new Label
          {
            Text = "Chưa có bài viết",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.FromArgb(149, 165, 166),
            Location = new Point(10, 10)
          });
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading hot posts: {ex.Message}");
      }
    }

    private Panel CreateHotPostItem(int rank, Post post, int yPos)
    {
      var panel = new Panel
      {
        Size = new Size(250, 50),
        Location = new Point(0, yPos),
        BackColor = Color.FromArgb(250, 252, 255),
        Cursor = Cursors.Hand
      };

      panel.Paint += (s, e) =>
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(Color.FromArgb(236, 240, 241), 1))
        {
          e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
        }
      };

      var lblRank = new Label
      {
        Text = rank <= 3 ? new[] { "🥇", "🥈", "🥉" }[rank - 1] : rank.ToString(),
        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
        Location = new Point(10, 15),
        Size = new Size(30, 20),
        TextAlign = ContentAlignment.MiddleCenter
      };

      var lblContent = new Label
      {
        Text = post.Content?.Length > 25 ? post.Content.Substring(0, 25) + "..." : post.Content,
        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
        ForeColor = Color.FromArgb(52, 73, 94),
        Location = new Point(45, 8),
        Size = new Size(195, 18),
        AutoSize = false
      };

      var lblStats = new Label
      {
        Text = $"❤️ {post.LikesCount}  💬 {post.CommentsCount}",
        Font = new Font("Segoe UI", 7F),
        ForeColor = Color.FromArgb(127, 140, 141),
        Location = new Point(45, 28),
        AutoSize = true
      };

      panel.Controls.Add(lblRank);
      panel.Controls.Add(lblContent);
      panel.Controls.Add(lblStats);

      panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(245, 247, 250);
      panel.MouseLeave += (s, e) => panel.BackColor = Color.FromArgb(250, 252, 255);

      return panel;
    }

    private void LoadMostReported(Panel container)
    {
      try
      {
        var reports = reportService?.GetAllReports() ?? new List<SocialManager.models.Report>();
        var mostReportedPosts = reports
          .GroupBy(r => r.ContentID)
          .Select(g => new
          {
            PostID = g.Key,
            ReportCount = g.Count()
          })
          .OrderByDescending(x => x.ReportCount)
          .Take(5)
          .ToList();

        int yPos = 0;
        int rank = 1;
        foreach (var item in mostReportedPosts)
        {
          var allPosts = postService?.GetAllPosts();
          var post = allPosts?.FirstOrDefault(p => p.PostID == item.PostID);
          if (post != null)
          {
            var reportItem = CreateReportedItem(rank, post, item.ReportCount, yPos);
            container.Controls.Add(reportItem);
            yPos += 55;
            rank++;
          }
        }

        if (!mostReportedPosts.Any())
        {
          container.Controls.Add(new Label
          {
            Text = "✅ Không có báo cáo",
            Font = new Font("Segoe UI", 9F, FontStyle.Italic),
            ForeColor = Color.FromArgb(46, 204, 113),
            Location = new Point(10, 10)
          });
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading reported posts: {ex.Message}");
      }
    }

    private Panel CreateReportedItem(int rank, Post post, int reportCount, int yPos)
    {
      var panel = new Panel
      {
        Size = new Size(250, 50),
        Location = new Point(0, yPos),
        BackColor = Color.FromArgb(255, 245, 245),
        Cursor = Cursors.Hand
      };

      panel.Paint += (s, e) =>
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        using (var pen = new Pen(Color.FromArgb(231, 76, 60), 1))
        {
          e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
        }
      };

      var lblRank = new Label
      {
        Text = rank.ToString(),
        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
        ForeColor = Color.FromArgb(231, 76, 60),
        Location = new Point(10, 12),
        Size = new Size(30, 25),
        TextAlign = ContentAlignment.MiddleCenter
      };

      var lblContent = new Label
      {
        Text = post.Content?.Length > 25 ? post.Content.Substring(0, 25) + "..." : post.Content,
        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
        ForeColor = Color.FromArgb(52, 73, 94),
        Location = new Point(45, 8),
        Size = new Size(195, 18),
        AutoSize = false
      };

      var lblReports = new Label
      {
        Text = $"⚠️ {reportCount} báo cáo",
        Font = new Font("Segoe UI", 7F, FontStyle.Bold),
        ForeColor = Color.FromArgb(231, 76, 60),
        Location = new Point(45, 28),
        AutoSize = true
      };

      panel.Controls.Add(lblRank);
      panel.Controls.Add(lblContent);
      panel.Controls.Add(lblReports);

      panel.MouseEnter += (s, e) => panel.BackColor = Color.FromArgb(255, 235, 235);
      panel.MouseLeave += (s, e) => panel.BackColor = Color.FromArgb(255, 245, 245);

      return panel;
    }
    #endregion

    #region Enhanced Chart Drawing
    private void DrawEnhancedBarChart(object sender, PaintEventArgs e)
    {
      var g = e.Graphics;
      g.SmoothingMode = SmoothingMode.AntiAlias;

      var chartPanel = sender as Panel;
      if (chartPanel == null) return;

      int width = chartPanel.Width;
      int height = chartPanel.Height;

      var posts = postService?.GetAllPosts();
      if (posts == null || !posts.Any())
      {
        DrawNoDataMessage(g, width, height);
        return;
      }

      // Get data based on selected time filter
      var dataPoints = GetChartDataPoints(posts);
      if (!dataPoints.Any()) return;

      int maxCount = dataPoints.Max(d => d.Count);
      if (maxCount == 0) maxCount = 1;

      int barWidth = Math.Max(20, (width - 80) / dataPoints.Count);
      int chartHeight = height - 60;

      // Draw grid lines
      DrawGridLines(g, width, chartHeight, maxCount);

      // Draw bars with gradient and animation effect
      for (int i = 0; i < dataPoints.Count; i++)
      {
        var data = dataPoints[i];
        int barHeight = (int)((double)data.Count / maxCount * chartHeight);
        int x = 50 + i * barWidth;
        int y = chartHeight - barHeight + 20;

        // Draw bar with gradient
        using (var brush = new LinearGradientBrush(
          new Rectangle(x, y, barWidth - 15, barHeight),
          Color.FromArgb(52, 152, 219),
          Color.FromArgb(41, 128, 185),
          LinearGradientMode.Vertical))
        {
          g.FillRectangle(brush, x, y, barWidth - 15, barHeight);
        }

        // Draw border
        using (var pen = new Pen(Color.FromArgb(41, 128, 185), 2))
        {
          g.DrawRectangle(pen, x, y, barWidth - 15, barHeight);
        }

        // Draw value on top
        using (var font = new Font("Segoe UI", 8F, FontStyle.Bold))
        using (var brush = new SolidBrush(Color.FromArgb(52, 73, 94)))
        {
          var text = data.Count.ToString();
          var size = g.MeasureString(text, font);
          g.DrawString(text, font, brush, x + (barWidth - 15 - size.Width) / 2, Math.Max(5, y - 20));
        }

        // Draw label
        using (var font = new Font("Segoe UI", 7F))
        using (var brush = new SolidBrush(Color.FromArgb(127, 140, 141)))
        {
          var text = data.Label;
          var size = g.MeasureString(text, font);
          g.DrawString(text, font, brush, x + (barWidth - 15 - size.Width) / 2, chartHeight + 25);
        }
      }

      // Draw axes
      using (var pen = new Pen(Color.FromArgb(189, 195, 199), 2))
      {
        g.DrawLine(pen, 50, chartHeight + 20, width - 20, chartHeight + 20); // X axis
        g.DrawLine(pen, 50, 20, 50, chartHeight + 20); // Y axis
      }
    }

    private void DrawGridLines(Graphics g, int width, int height, int maxValue)
    {
      using (var pen = new Pen(Color.FromArgb(240, 243, 245), 1))
      {
        int steps = 5;
        for (int i = 0; i <= steps; i++)
        {
          int y = height + 20 - (height * i / steps);
          g.DrawLine(pen, 50, y, width - 20, y);

          // Draw value label
          using (var font = new Font("Segoe UI", 7F))
          using (var brush = new SolidBrush(Color.FromArgb(149, 165, 166)))
          {
            var value = (maxValue * i / steps).ToString();
            g.DrawString(value, font, brush, 15, y - 7);
          }
        }
      }
    }

    private void DrawNoDataMessage(Graphics g, int width, int height)
    {
      using (var font = new Font("Segoe UI", 12F, FontStyle.Italic))
      using (var brush = new SolidBrush(Color.FromArgb(149, 165, 166)))
      {
        var text = "Không có dữ liệu";
        var size = g.MeasureString(text, font);
        g.DrawString(text, font, brush, (width - size.Width) / 2, (height - size.Height) / 2);
      }
    }

    private List<(string Label, int Count)> GetChartDataPoints(List<Post> posts)
    {
      var dataPoints = new List<(string, int)>();
      var now = DateTime.Now.Date;

      switch (selectedTimeFilter)
      {
        case "24h":
          for (int i = 23; i >= 0; i--)
          {
            var hour = now.AddHours(-i);
            var count = posts.Count(p => p.CreatedAt >= hour && p.CreatedAt < hour.AddHours(1));
            dataPoints.Add(($"{hour:HH}h", count));
          }
          break;

        case "7days":
          for (int i = 6; i >= 0; i--)
          {
            var day = now.AddDays(-i);
            var count = posts.Count(p => p.CreatedAt.Date == day);
            dataPoints.Add((day.ToString("dd/MM"), count));
          }
          break;

        case "30days":
          for (int i = 29; i >= 0; i--)
          {
            var day = now.AddDays(-i);
            var count = posts.Count(p => p.CreatedAt.Date == day);
            dataPoints.Add((day.ToString("dd/MM"), count));
          }
          break;

        default:
          // Default to 7 days
          for (int i = 6; i >= 0; i--)
          {
            var day = now.AddDays(-i);
            var count = posts.Count(p => p.CreatedAt.Date == day);
            dataPoints.Add((day.ToString("dd/MM"), count));
          }
          break;
      }

      return dataPoints;
    }
    #endregion
  }
}
