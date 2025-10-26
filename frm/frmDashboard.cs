using SocialManager.services;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SocialManager.controls;

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
    private PostService postService;
    private User? currentUser;

    public frmDashboard()
    {
      InitializeComponent();

      // Khởi tạo service
      postService = new PostService();
      currentUser = AuthSessionService.CurrentUser;

      // Kiểm tra user đã đăng nhập chưa
      if (currentUser == null)
      {
        MessageBox.Show("Bạn cần đăng nhập để sử dụng Dashboard!", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.Close();
        return;
      }

      this.picAvatar.Paint += new PaintEventHandler(picAvatar_Paint);
      this.btnNewPost.Paint += new PaintEventHandler(btnNewPost_Paint);
      this.btnLogout.Click += new EventHandler(btnLogout_Click);
      this.flowLayoutPanelPosts.Resize += new EventHandler(flowLayoutPanelPosts_Resize);

      // Load dữ liệu user
      LoadUserInfo();

      // Load bài viết
      LoadPosts();
    }

    private void LoadUserInfo()
    {
      if (currentUser != null)
      {
        // Hiển thị tên user
        lblUsername.Text = currentUser.UserName;

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
          picAvatar.Image = System.Drawing.Image.FromFile(currentUser.AvatarUrl);
          picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
        }
        catch
        {
          // Nếu load lỗi, để avatar mặc định
          picAvatar.BackColor = System.Drawing.Color.LightGray;
        }
      }
      else
      {
        // Avatar mặc định
        picAvatar.BackColor = System.Drawing.Color.LightGray;
      }
    }

    private void picAvatar_Paint(object? sender, PaintEventArgs e) => MakeControlCircular(sender, e);
    private void btnNewPost_Paint(object? sender, PaintEventArgs e) => MakeControlCircular(sender, e);

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
      if (currentUser == null)
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
        List<Post> userPosts = GetPostsForCurrentUser();

        if (userPosts == null || userPosts.Count == 0)
        {
          // Hiển thị thông báo nếu không có bài viết
          Label lblNoPost = new Label
          {
            Text = "📭 Chưa có bài viết nào.\nNhấn nút '+' để tạo bài viết đầu tiên!",
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

      // Cập nhật tên user
      if (currentUser != null)
      {
        lblUsername.Text = currentUser.UserName;
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

          // Đóng Dashboard và hiển thị lại Login
          this.Hide();
          frmLogin frmLogin = new frmLogin();
          frmLogin.ShowDialog();
          this.Close();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi đăng xuất: {ex.Message}", "Lỗi",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }
  }
}
