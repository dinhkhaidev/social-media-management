using SocialManager.constants;
using SocialManager.services;
using SocialManager.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SocialManager.frm.UserControls
{
  public partial class ucSocialAccounts : UserControl
  {
    private readonly UserService _userService;
    private List<User> _allUsers;
    private System.Windows.Forms.Timer _refreshTimer;
    private DateTime _lastRefresh;

    public ucSocialAccounts()
    {
      InitializeComponent();
      _userService = new UserService();
      _lastRefresh = DateTime.Now;
      InitializeUserManagement();
      SetupRefreshTimer();
    }

    private void InitializeUserManagement()
    {
      this.BackColor = Color.FromArgb(240, 242, 245);
      SetupDataGridView();
      LoadAllUsers();
      LoadUserStatistics();
      LoadRecentActivities();
    }

    private void SetupRefreshTimer()
    {
      _refreshTimer = new System.Windows.Forms.Timer();
      _refreshTimer.Interval = 30000; // 30 seconds for real-time feeling
      _refreshTimer.Tick += RefreshTimer_Tick;
      _refreshTimer.Start();
    }

    private void RefreshTimer_Tick(object sender, EventArgs e)
    {
      try
      {
        LoadAllUsers();
        LoadUserStatistics();
        LoadRecentActivities();
        _lastRefresh = DateTime.Now;

        // Update last refresh time
        if (lblLastRefresh != null)
        {
          lblLastRefresh.Text = $"Last updated: {_lastRefresh:HH:mm:ss}";
        }
      }
      catch (Exception ex)
      {
        // Silent error handling for auto-refresh
        System.Diagnostics.Debug.WriteLine($"Auto-refresh error: {ex.Message}");
      }
    }

    private void SetupDataGridView()
    {
      if (dgvUsers == null) return;

      dgvUsers.Columns.Clear();
      dgvUsers.AutoGenerateColumns = false;

      // Configure DataGridView appearance
      dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
      dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
      dgvUsers.DefaultCellStyle.BackColor = Color.White;
      dgvUsers.DefaultCellStyle.ForeColor = Color.FromArgb(44, 62, 80);
      dgvUsers.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
      dgvUsers.DefaultCellStyle.Padding = new Padding(10, 8, 10, 8);

      dgvUsers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 73, 94);
      dgvUsers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
      dgvUsers.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
      dgvUsers.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 12, 10, 12);
      dgvUsers.ColumnHeadersHeight = 45;
      dgvUsers.RowTemplate.Height = 50;
      dgvUsers.EnableHeadersVisualStyles = false;
      dgvUsers.GridColor = Color.FromArgb(234, 236, 238);

      // Add columns with detailed user information
      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "UserName",
        HeaderText = "👤 Username",
        Width = 120,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "FullName",
        HeaderText = "📝 Full Name",
        Width = 150,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Email",
        HeaderText = "📧 Email",
        Width = 180,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Phone",
        HeaderText = "📱 Phone",
        Width = 120,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Role",
        HeaderText = "👑 Role",
        Width = 100,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Status",
        HeaderText = "Status",
        Width = 100,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "Gender",
        HeaderText = "⚧ Gender",
        Width = 80,
        ReadOnly = true
      });

      dgvUsers.Columns.Add(new DataGridViewTextBoxColumn
      {
        Name = "CreatedAt",
        HeaderText = "📅 Joined",
        Width = 120,
        ReadOnly = true
      });
    }

    //query truc tiep trong form, khong su dung service
    private void LoadAllUsers()
    {
      try
      {
        _allUsers = User.GetList(GlobalSetting.UsersFilePath);
        LoadUsersToGrid();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Error loading users: {ex.Message}", "Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        _allUsers = new List<User>();
      }
    }

    private void LoadUsersToGrid()
    {
      if (dgvUsers == null || _allUsers == null) return;

      dgvUsers.Rows.Clear();

      foreach (var user in _allUsers.OrderByDescending(u => u.CreatedAt))
      {
        var roleText = UserConstant.GetRoleText(user.Role);
        var statusText = UserConstant.GetStatusText(user.StatusId);
        var genderText = user.Gender == 0 ? "👨 Male" : "👩 Female";
        var joinedDate = user.CreatedAt.ToString("MMM dd, yyyy");
        var phoneDisplay = string.IsNullOrEmpty(user.Phone) ? "N/A" : user.Phone;

        dgvUsers.Rows.Add(
            user.UserName,
            user.FullName,
            user.Email,
            phoneDisplay,
            roleText,
            statusText,
            genderText,
            joinedDate
        );

        // Color code rows based on status
        var row = dgvUsers.Rows[dgvUsers.Rows.Count - 1];
        switch (user.StatusId)
        {
          case 1: // Active
            row.DefaultCellStyle.BackColor = Color.White;
            break;
          case 0: // Inactive
            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 248);
            row.DefaultCellStyle.ForeColor = Color.FromArgb(149, 165, 166);
            break;
          case -1: // Banned
            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 235, 235);
            row.DefaultCellStyle.ForeColor = Color.FromArgb(231, 76, 60);
            break;
        }
      }
    }


    private void LoadUserStatistics()
    {
      if (_allUsers == null) return;

      var totalUsers = _allUsers.Count;
      var activeUsers = _allUsers.Count(u => u.StatusId == 1);
      var newUsersToday = _allUsers.Count(u => u.CreatedAt.Date == DateTime.Today);
      var newUsersThisWeek = _allUsers.Count(u => u.CreatedAt >= DateTime.Today.AddDays(-7));
      var adminUsers = _allUsers.Count(u => u.Role == 1);

      // Update statistics cards
      if (lblTotalUsers != null) lblTotalUsers.Text = totalUsers.ToString();
      if (lblActiveUsers != null) lblActiveUsers.Text = activeUsers.ToString();
      if (lblNewToday != null) lblNewToday.Text = newUsersToday.ToString();
      if (lblNewThisWeek != null) lblNewThisWeek.Text = newUsersThisWeek.ToString();
      if (lblAdminUsers != null) lblAdminUsers.Text = adminUsers.ToString();

      // Calculate and show percentage
      var activePercentage = totalUsers > 0 ? (activeUsers * 100.0 / totalUsers) : 0;
      if (lblActivePercentage != null) lblActivePercentage.Text = $"{activePercentage:F1}%";
    }

    private void LoadRecentActivities()
    {
      if (lstActivities == null || _allUsers == null) return;

      lstActivities.Items.Clear();

      var activities = new List<string>();

      // Recent user registrations
      var recentUsers = _allUsers.Where(u => u.CreatedAt >= DateTime.Now.AddDays(-7))
                               .OrderByDescending(u => u.CreatedAt)
                               .Take(8);

      foreach (var user in recentUsers)
      {
        var timeAgo = TimeUtil.GetTimeAgo(user.CreatedAt);
        var roleIcon = user.Role == 1 ? "👑" : "👤";
        activities.Add($"{roleIcon} {user.UserName} joined ({timeAgo})");
      }

      // System statistics
      activities.Add($"📊 Total users: {_allUsers.Count}");
      activities.Add($"✅ Active: {_allUsers.Count(u => u.StatusId == 1)}");
      activities.Add($"⏸️ Inactive: {_allUsers.Count(u => u.StatusId == 0)}");
      activities.Add($"🚫 Banned: {_allUsers.Count(u => u.StatusId == -1)}");

      // Gender distribution
      var maleCount = _allUsers.Count(u => u.Gender == 0);
      var femaleCount = _allUsers.Count(u => u.Gender == 1);
      activities.Add($"👨 Male users: {maleCount}");
      activities.Add($"👩 Female users: {femaleCount}");

      activities.Add($"🔄 Updated: {DateTime.Now:HH:mm:ss}");

      foreach (var activity in activities.Take(15))
      {
        lstActivities.Items.Add(activity);
      }
    }

    // Event handlers for user management actions
    private void btnUserDetails_Click(object sender, EventArgs e)
    {
      if (dgvUsers.SelectedRows.Count == 0)
      {
        MessageBox.Show("Vui lòng chọn người dùng để xem chi tiết.", "Chưa chọn người dùng",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
      }

      var selectedRow = dgvUsers.SelectedRows[0];
      var username = selectedRow.Cells["UserName"].Value?.ToString();

      if (!string.IsNullOrEmpty(username))
      {
        var user = _userService.GetUserByUsername(username);
        if (user != null)
        {
          ShowDetailedUserInfo(user);
        }
      }
    }

    private void ShowDetailedUserInfo(User user)
    {
      var details = $"👤 COMPLETE USER PROFILE\n" +
                   $"{'=' * 50}\n\n" +
                   $"🆔 User ID: {user.UserID}\n" +
                   $"👤 Username: {user.UserName}\n" +
                   $"📝 Full Name: {user.FullName}\n" +
                   $"📧 Email: {user.Email}\n" +
                   $"📱 Phone: {user.Phone ?? "Not provided"}\n" +
                   $"👑 Role: {UserConstant.GetRoleText(user.Role)}\n" +
                   $"Status: {UserConstant.GetStatusText(user.StatusId)}\n" +
                   $"⚧ Gender: {(user.Gender == 0 ? "👨 Male" : "👩 Female")}\n" +
                   $"🎂 Date of Birth: {user.DOB:yyyy-MM-dd}\n" +
                   $"🏠 Address: {user.Address ?? "Not provided"}\n" +
                   $"📝 Bio: {user.Bio ?? "No bio available"}\n" +
                   $"📅 Member since: {user.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" +
                   $"🔗 Avatar URL: {user.AvatarUrl ?? "No avatar"}";

      MessageBox.Show(details, "User Profile Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnExportUsers_Click(object sender, EventArgs e)
    {
      try
      {
        using (var saveDialog = new SaveFileDialog())
        {
          saveDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
          saveDialog.Title = "Export Users Data";
          saveDialog.FileName = $"SocialMedia_Users_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

          if (saveDialog.ShowDialog() == DialogResult.OK)
          {
            ExportUsersToCSV(saveDialog.FileName);
            MessageBox.Show($"✅ Users exported successfully!\n\nFile: {saveDialog.FileName}\nTotal users: {_allUsers?.Count ?? 0}",
                "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"❌ Error exporting users:\n{ex.Message}", "Export Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void ExportUsersToCSV(string filePath)
    {
      var csv = new StringBuilder();
      csv.AppendLine("UserID,Username,FullName,Email,Phone,Role,Status,Gender,DateOfBirth,Address,Bio,JoinedDate");

      foreach (var user in _allUsers)
      {
        var roleText = UserConstant.GetRoleText(user.Role).Replace("👤 ", "").Replace("👑 ", "").Replace("🛡️ ", "");
        var statusText = UserConstant.GetStatusText(user.StatusId).Replace("✅ ", "").Replace("⏸️ ", "").Replace("🚫 ", "");
        var genderText = user.Gender == 0 ? "Male" : "Female";

        csv.AppendLine($"\"{user.UserID}\",\"{user.UserName}\",\"{user.FullName}\"," +
                      $"\"{user.Email}\",\"{user.Phone ?? ""}\",\"{roleText}\"," +
                      $"\"{statusText}\",\"{genderText}\",\"{user.DOB:yyyy-MM-dd}\"," +
                      $"\"{user.Address ?? ""}\",\"{user.Bio ?? ""}\",\"{user.CreatedAt:yyyy-MM-dd HH:mm:ss}\"");
      }

      File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
    }

    private void btnRefreshData_Click(object sender, EventArgs e)
    {
      try
      {
        LoadAllUsers();
        LoadUserStatistics();
        LoadRecentActivities();
        _lastRefresh = DateTime.Now;

        if (lblLastRefresh != null)
        {
          lblLastRefresh.Text = $"Last updated: {_lastRefresh:HH:mm:ss}";
        }

        MessageBox.Show($"✅ Data refreshed successfully!\n\n" +
                      $"📊 Users loaded: {_allUsers?.Count ?? 0}\n" +
                      $"🕒 Time: {DateTime.Now:HH:mm:ss}",
                      "Refresh Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        MessageBox.Show($"❌ Error refreshing data:\n{ex.Message}", "Refresh Error",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    // Custom paint events for modern UI
    private void pnlCard_Paint(object sender, PaintEventArgs e)
    {
      Panel panel = sender as Panel;
      if (panel != null)
      {
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Draw shadow
        using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
        {
          FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 12);
        }

        // Draw main card
        using (var cardBrush = new SolidBrush(Color.White))
        {
          FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 12);
        }

        // Draw subtle border
        using (var borderPen = new Pen(Color.FromArgb(220, 221, 222), 1))
        {
          DrawRoundedRectangle(e.Graphics, borderPen, new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 12);
        }
      }
    }

    // Helper methods for drawing rounded rectangles
    private void FillRoundedRectangle(Graphics graphics, Brush brush, Rectangle rect, int radius)
    {
      using (GraphicsPath path = CreateRoundedRectPath(rect, radius))
      {
        graphics.FillPath(brush, path);
      }
    }

    private void DrawRoundedRectangle(Graphics graphics, Pen pen, Rectangle rect, int radius)
    {
      using (GraphicsPath path = CreateRoundedRectPath(rect, radius))
      {
        graphics.DrawPath(pen, path);
      }
    }

    private GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
    {
      GraphicsPath path = new GraphicsPath();
      int diameter = radius * 2;

      path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
      path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
      path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
      path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
      path.CloseFigure();

      return path;
    }

    private void dgvUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
