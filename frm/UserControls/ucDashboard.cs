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
      LoadDashboardData();
    }

    private void LoadDashboardData()
    {
      try
      {
        // Safely initialize controls with null checks
        if (lblTotalPostsValue != null)
          lblTotalPostsValue.Text = "1.2K";

        if (lblGrowthValue != null)
          lblGrowthValue.Text = "+12%";

        if (lblInteractionsValue != null)
          lblInteractionsValue.Text = "85K";

        // lblActiveAccountsValue control không tồn tại trong Designer
        // if (lblActiveAccountsValue != null)
        //     lblActiveAccountsValue.Text = "5";
      }
      catch (Exception ex)
      {
        // Ghi log và hiển thị thông báo thân thiện
        System.Diagnostics.Debug.WriteLine($"Error loading dashboard data: {ex}");
        MessageBox.Show($"Lỗi khi tải dữ liệu bảng điều khiển: {ex.Message}", "Lỗi bảng điều khiển",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void btnPostNow_Click(object sender, EventArgs e)
    {
      try
      {
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
        MessageBox.Show($"Lỗi khi đăng: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnSchedule_Click(object sender, EventArgs e)
    {
      try
      {

      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
        MessageBox.Show($"Lỗi khi lên lịch: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void btnViewAllActivity_Click(object sender, EventArgs e)
    {
      MessageBox.Show("Tính năng lịch sử hoạt động sẽ sớm có!", "Thông tin",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void btnAddPhoto_Click(object sender, EventArgs e)
    {
      try
      {
        using (OpenFileDialog openFileDialog = new OpenFileDialog())
        {
          openFileDialog.Filter = "Tệp hình ảnh (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
          openFileDialog.Title = "Chọn hình ảnh";

          if (openFileDialog.ShowDialog() == DialogResult.OK)
          {
            var selected = openFileDialog.FileName;
            if (!string.IsNullOrEmpty(selected))
            {
              MessageBox.Show($"Đã chọn hình: {Path.GetFileName(selected)}", "Thành công",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
              MessageBox.Show("Không có tệp nào được chọn.", "Thông tin",
                  MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
        MessageBox.Show($"Lỗi khi chọn hình ảnh: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    // Custom paint events for modern UI
    private void pnlCard_Paint(object sender, PaintEventArgs e)
    {
      if (sender is Panel panel)
      {
        try
        {
          e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

          // Draw shadow
          using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
          {
            GraphicsExtensions.FillRoundedRectangle(e.Graphics, shadowBrush, new Rectangle(3, 3, panel.Width - 3, panel.Height - 3), 12);
          }

          // Draw main card
          using (var cardBrush = new SolidBrush(Color.White))
          {
            GraphicsExtensions.FillRoundedRectangle(e.Graphics, cardBrush, new Rectangle(0, 0, panel.Width - 3, panel.Height - 3), 12);
          }

          // Draw subtle border
          using (var borderPen = new Pen(Color.FromArgb(230, 230, 230), 1))
          {
            GraphicsExtensions.DrawRoundedRectangle(e.Graphics, borderPen, new Rectangle(0, 0, panel.Width - 4, panel.Height - 4), 12);
          }
        }
        catch (Exception ex)
        {
          // Fallback to simple rectangle if drawing fails
          System.Diagnostics.Debug.WriteLine(ex.ToString());
          e.Graphics.FillRectangle(Brushes.White, panel.ClientRectangle);
          e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, panel.Width - 1, panel.Height - 1);
        }
      }
    }

    private void picTotalPosts_Paint(object sender, PaintEventArgs e)
    {
      DrawStatIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(52, 152, 219), "posts");
    }

    private void picGrowth_Paint(object sender, PaintEventArgs e)
    {
      DrawStatIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(39, 174, 96), "growth");
    }

    private void picInteractions_Paint(object sender, PaintEventArgs e)
    {
      DrawStatIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(231, 76, 60), "heart");
    }

    private void picActiveAccounts_Paint(object sender, PaintEventArgs e)
    {
      DrawStatIcon(e.Graphics, new Rectangle(5, 5, 30, 30), Color.FromArgb(46, 204, 113), "users");
    }

    private void DrawStatIcon(Graphics g, Rectangle rect, Color color, string iconType)
    {
      try
      {
        g.SmoothingMode = SmoothingMode.AntiAlias;
        using (var brush = new SolidBrush(color))
        {
          int centerX = rect.X + rect.Width / 2;
          int centerY = rect.Y + rect.Height / 2;

          switch (iconType)
          {
            case "posts":
              g.FillRectangle(brush, centerX - 10, centerY - 12, 20, 24);
              g.FillRectangle(Brushes.White, centerX - 7, centerY - 6, 14, 2);
              g.FillRectangle(Brushes.White, centerX - 7, centerY - 2, 14, 2);
              g.FillRectangle(Brushes.White, centerX - 7, centerY + 2, 10, 2);
              break;

            case "heart":
              using (var path = new GraphicsPath())
              {
                path.AddEllipse(centerX - 8, centerY - 5, 8, 8);
                path.AddEllipse(centerX, centerY - 5, 8, 8);
                path.AddPolygon(new Point[] {
                                    new Point(centerX - 4, centerY + 3),
                                    new Point(centerX, centerY + 12),
                                    new Point(centerX + 4, centerY + 3)
                                });
                g.FillPath(brush, path);
              }
              break;

            case "users":
              g.FillEllipse(brush, centerX - 12, centerY - 8, 10, 10);
              g.FillEllipse(brush, centerX + 2, centerY - 8, 10, 10);
              g.FillRectangle(brush, centerX - 14, centerY + 4, 14, 8);
              g.FillRectangle(brush, centerX, centerY + 4, 14, 8);
              break;

            case "growth":
              using (var pen = new Pen(brush, 3))
              {
                Point[] points = {
                                    new Point(centerX - 12, centerY + 8),
                                    new Point(centerX - 6, centerY + 2),
                                    new Point(centerX, centerY - 2),
                                    new Point(centerX + 6, centerY - 6),
                                    new Point(centerX + 12, centerY - 10)
                                };
                g.DrawLines(pen, points);

                // Arrow head
                g.DrawLine(pen, centerX + 8, centerY - 6, centerX + 12, centerY - 10);
                g.DrawLine(pen, centerX + 12, centerY - 6, centerX + 12, centerY - 10);
              }
              break;
          }
        }
      }
      catch (Exception ex)
      {
        // Fallback to simple rectangle if icon drawing fails
        System.Diagnostics.Debug.WriteLine(ex.ToString());
        g.FillRectangle(new SolidBrush(color), rect);
      }
    }

    private void picTotalPosts_Click(object sender, EventArgs e)
    {

    }

    private void label2_Click(object sender, EventArgs e)
    {

    }

    private void lblComposerTitle_Click(object sender, EventArgs e)
    {

    }
  }
}
