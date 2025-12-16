using System;
using System.Drawing;
using System.Windows.Forms;

namespace SocialManager.frm.UserControls
{
  public partial class ucSimpleDashboard : UserControl
  {
    public ucSimpleDashboard()
    {
      InitializeControl();
    }

    private void InitializeControl()
    {
      // Set basic properties
      this.Size = new Size(800, 600);
      this.BackColor = Color.FromArgb(247, 249, 252);
      this.Padding = new Padding(20);

      // Create title label
      var lblTitle = new Label
      {
        Text = "Dashboard",
        Font = new Font("Segoe UI", 24F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 20),
        Size = new Size(300, 50),
        BackColor = Color.Transparent
      };
      this.Controls.Add(lblTitle);

      // Create stats panel
      var pnlStats = new Panel
      {
        Location = new Point(20, 80),
        Size = new Size(760, 150),
        BackColor = Color.White
      };

      // Add some sample stats
      var lblStats = new Label
      {
        Text = "?? Total Posts: 1.2K\n?? Growth: +12%\n?? Interactions: 85K\n?? Active Accounts: 5",
        Font = new Font("Segoe UI", 12F),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 20),
        Size = new Size(720, 110),
        BackColor = Color.Transparent
      };
      pnlStats.Controls.Add(lblStats);
      this.Controls.Add(pnlStats);

      // Create post composer
      var pnlComposer = new Panel
      {
        Location = new Point(20, 250),
        Size = new Size(760, 300),
        BackColor = Color.White
      };

      var lblComposer = new Label
      {
        Text = "Create New Post",
        Font = new Font("Segoe UI", 16F, FontStyle.Bold),
        ForeColor = Color.FromArgb(44, 62, 80),
        Location = new Point(20, 20),
        Size = new Size(200, 30),
        BackColor = Color.Transparent
      };
      pnlComposer.Controls.Add(lblComposer);

      var txtContent = new TextBox
      {
        Location = new Point(20, 60),
        Size = new Size(720, 150),
        Multiline = true,
        PlaceholderText = "Bạn đang nghĩ gì?",
        Font = new Font("Segoe UI", 11F)
      };
      pnlComposer.Controls.Add(txtContent);

      var btnPost = new Button
      {
        Text = "Post Now",
        Location = new Point(20, 230),
        Size = new Size(100, 40),
        BackColor = Color.FromArgb(52, 152, 219),
        ForeColor = Color.White,
        FlatStyle = FlatStyle.Flat,
        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
      };
      btnPost.FlatAppearance.BorderSize = 0;
      btnPost.Click += (s, e) =>
      {
        if (string.IsNullOrWhiteSpace(txtContent.Text))
        {
          MessageBox.Show("Vui lòng nhập nội dung bài viết.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        MessageBox.Show("Đã đăng bài thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        txtContent.Clear();
      };
      pnlComposer.Controls.Add(btnPost);

      this.Controls.Add(pnlComposer);
    }
  }
}
