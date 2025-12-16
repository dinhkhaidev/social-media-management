using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.utils;

namespace SocialManager.controls
{
  public partial class ucPostDetail : UserControl
  {
    private int postId;
    private Post? post;
    private User? currentUser;
    private List<Comment>? comments;
    private List<Like>? likes;
    private bool isLiked = false;
    private UserService userService;
    public event EventHandler? CloseRequested;

    private Panel? pnlHeader;
    private Label? lblAuthor;
    private Label? lblDate;
    private Panel? pnlContent;
    private Label? lblContent;
    private Panel? pnlStats;
    private RoundedButton? btnLike;
    private Label? lblLikeCount;
    private Label? lblCommentCount;
    private FlowLayoutPanel? flpComments;
    private Panel? pnlAddComment;
    private TextBox? txtComment;
    private RoundedButton? btnAddComment;
    private RoundedButton? btnClose;
    private RoundedButton? btnReportPost;
    private RoundedButton? btnDeletePost;


    public ucPostDetail(int postId)
    {
      this.postId = postId;
      currentUser = AuthSessionService.CurrentUser;
      userService = new UserService();
      comments = new List<Comment>();
      likes = new List<Like>();
      InitializeComponent();
      LoadPostData();

      // Apply theme from GlobalSettings
      DashboardTheme.ApplyThemeToUserControl(this);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.BackColor = Color.White;
      this.Size = new Size(700, 600);
      this.AutoScroll = true;

      pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.FromArgb(245, 245, 245), Padding = new Padding(15) };
      lblAuthor = new Label { Location = new Point(15, 15), Font = new Font("Segoe UI", 12F, FontStyle.Bold), AutoSize = true, Text = "Đang tải..." };
      lblDate = new Label { Location = new Point(15, 40), Font = new Font("Segoe UI", 9F), ForeColor = Color.Gray, AutoSize = true };

      // Nút đóng sát phải nhất
      btnClose = new RoundedButton { Size = new Size(80, 35), Text = "✕ Đóng", BackColor = Color.FromArgb(108, 117, 125), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);

      // Nút xóa (chỉ hiện nếu là chủ bài viết hoặc admin)
      btnDeletePost = new RoundedButton { Size = new Size(90, 35), Text = "🗑 Xóa", BackColor = Color.FromArgb(220, 53, 69), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
      btnDeletePost.FlatAppearance.BorderSize = 0;
      btnDeletePost.Click += BtnDeletePost_Click;
      btnDeletePost.Visible = false;

      // Nút tố cáo (chỉ hiện nếu không phải chủ bài viết)
      btnReportPost = new RoundedButton { Size = new Size(100, 35), Text = "⚠ Tố cáo", BackColor = Color.FromArgb(255, 117, 24), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
      btnReportPost.FlatAppearance.BorderSize = 0;
      btnReportPost.Click += BtnReportPost_Click;
      btnReportPost.Visible = false;

      // Thêm event resize để điều chỉnh vị trí nút
      pnlHeader.Resize += (s, e) => RepositionHeaderButtons();

      pnlHeader.Controls.AddRange(new Control[] { lblAuthor, lblDate, btnReportPost, btnDeletePost, btnClose });

      pnlContent = new Panel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(15, 10, 15, 15), BackColor = Color.White };
      lblContent = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), AutoSize = true, MaximumSize = new Size(650, 0) };
      pnlContent.Controls.Add(lblContent);

      pnlStats = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(15, 5, 15, 5) };
      btnLike = new RoundedButton { Location = new Point(15, 10), Size = new Size(90, 30), Text = " Thích", FlatStyle = FlatStyle.Flat, BackColor = Color.White, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F) };
      btnLike.FlatAppearance.BorderSize = 0;
      btnLike.Click += BtnLike_Click;
      lblLikeCount = new Label { Location = new Point(110, 15), Font = new Font("Segoe UI", 9F), AutoSize = true };
      lblCommentCount = new Label { Location = new Point(220, 15), Font = new Font("Segoe UI", 9F), AutoSize = true };
      pnlStats.Controls.AddRange(new Control[] { btnLike, lblLikeCount, lblCommentCount });

      Label lblCommentsTitle = new Label { Dock = DockStyle.Top, Text = " Bình luận", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Height = 35, Padding = new Padding(15, 10, 0, 0), BackColor = Color.FromArgb(250, 250, 250) };
      flpComments = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(15), BackColor = Color.FromArgb(250, 250, 250) };

      pnlAddComment = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10) };
      txtComment = new TextBox { Location = new Point(15, 15), Size = new Size(540, 50), Multiline = true, Font = new Font("Segoe UI", 9F) };
      btnAddComment = new RoundedButton { Size = new Size(110, 50), Text = "Gửi", BackColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
      btnAddComment.FlatAppearance.BorderSize = 0;
      btnAddComment.Click += BtnAddComment_Click;

      // Event resize để điều chỉnh vị trí nút Gửi
      pnlAddComment.Resize += (s, e) =>
      {
        if (btnAddComment != null && txtComment != null && pnlAddComment != null)
        {
          btnAddComment.Location = new Point(pnlAddComment.Width - btnAddComment.Width - 15, 15);
          txtComment.Width = pnlAddComment.Width - btnAddComment.Width - 40;
        }
      };

      pnlAddComment.Controls.AddRange(new Control[] { txtComment, btnAddComment });

      this.Controls.Add(flpComments);
      this.Controls.Add(lblCommentsTitle);
      this.Controls.Add(pnlAddComment);
      this.Controls.Add(pnlStats);
      this.Controls.Add(pnlContent);
      this.Controls.Add(pnlHeader);
      this.ResumeLayout(false);
    }

    private void LoadPostData()
    {
      try
      {
        var allPosts = Post.GetList("datas\\Post.csv");
        post = allPosts.FirstOrDefault(p => p.PostID == postId);
        if (post == null)
        {
          MessageBox.Show("Không tìm thấy bài viết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
          CloseRequested?.Invoke(this, EventArgs.Empty);
          return;
        }
        var users = userService.GetAllUsers();
        var author = users.FirstOrDefault(u => u.UserID == post.UserID);
        if (lblAuthor != null) lblAuthor.Text = author?.FullName ?? "Người dùng";
        if (lblDate != null) lblDate.Text = post.CreatedAt.ToString("dd/MM/yyyy HH:mm");
        if (lblContent != null) lblContent.Text = post.Content;

        // Hiển thị nút xóa nếu là chủ bài viết hoặc admin
        if (currentUser != null && btnDeletePost != null)
        {
          bool isOwner = post.UserID == currentUser.UserID;
          bool isAdmin = currentUser.Role == 1;
          btnDeletePost.Visible = isOwner || isAdmin;
        }

        // Hiển thị nút tố cáo nếu không phải chủ bài viết
        if (currentUser != null && post.UserID != currentUser.UserID && btnReportPost != null)
        {
          btnReportPost.Visible = true;
        }

        // Trigger resize để đặt đúng vị trí nút
        RepositionHeaderButtons();

        if (System.IO.File.Exists("datas\\Likes.csv"))
        {
          likes = Like.GetList("datas\\Likes.csv").Where(l => l.PostID == postId).ToList();
          if (currentUser != null) isLiked = likes.Any(l => l.UserID == currentUser.UserID);
        }
        if (System.IO.File.Exists("datas\\Comment.csv"))
        {
          comments = Comment.GetList("datas\\Comment.csv").Where(c => c.PostID == postId).OrderBy(c => c.CreatedAt).ToList();
        }
        UpdateUI();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void UpdateUI()
    {
      if (lblLikeCount != null) lblLikeCount.Text = $"{likes?.Count ?? 0} lượt thích";
      if (lblCommentCount != null) lblCommentCount.Text = $"{comments?.Count ?? 0} bình luận";
      if (btnLike != null)
      {
        if (isLiked)
        {
          btnLike.Text = " Đã thích";
          btnLike.BackColor = Color.FromArgb(255, 220, 220);
          btnLike.BackgroundColor = Color.FromArgb(255, 220, 220);
          btnLike.ForeColor = Color.FromArgb(220, 53, 69);
          btnLike.TextColor = Color.FromArgb(220, 53, 69);
        }
        else
        {
          btnLike.Text = " Thích";
          btnLike.BackColor = Color.White;
          btnLike.BackgroundColor = Color.White;
          btnLike.ForeColor = Color.FromArgb(64, 64, 64);
          btnLike.TextColor = Color.FromArgb(64, 64, 64);
        }

        // btnLike.Visible = true;
        // btnLike.Invalidate();
        // btnLike.Refresh();
      }
      DisplayComments();
    }

    private void LoadCommentsData()
    {
      try
      {
        if (post != null && System.IO.File.Exists("datas\\Comment.csv"))
        {
          comments = Comment.GetList("datas\\Comment.csv")
            .Where(c => c.PostID == post.PostID)
            .OrderBy(c => c.CreatedAt)
            .ToList();
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error loading comments: {ex.Message}");
        comments = new List<Comment>();
      }
    }

    private void DisplayComments()
    {
      if (flpComments == null) return;

      flpComments.Controls.Clear();
      if (comments == null || comments.Count == 0)
      {
        flpComments.Controls.Add(new Label { Text = "Chưa có bình luận nào", ForeColor = Color.Gray, Font = new Font("Segoe UI", 9F, FontStyle.Italic), AutoSize = true, Padding = new Padding(5) });
        return;
      }
      var users = userService.GetAllUsers();
      foreach (var comment in comments)
      {
        var commenter = users.FirstOrDefault(u => u.UserID == comment.UserID);
        bool isDark = GlobalSettings.DarkMode;
        Panel commentPanel = new Panel
        {
          Width = flpComments.ClientSize.Width - 10, // Tràn ra hết khung
          AutoSize = true,
          Padding = new Padding(10),
          Margin = new Padding(0, 5, 0, 5),
          BackColor = isDark ? Color.FromArgb(36, 37, 38) : Color.White,
          BorderStyle = BorderStyle.None
        };
        Label lblCommenter = new Label { Text = commenter?.FullName ?? "Người dùng", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
        Label lblCommentDate = new Label { Text = comment.CreatedAt.ToString("dd/MM/yyyy HH:mm"), Font = new Font("Segoe UI", 8F), ForeColor = isDark ? Color.FromArgb(176, 179, 184) : Color.Gray, AutoSize = true, Location = new Point(10, 30) };
        Label lblCommentContent = new Label { Text = comment.Content, Font = new Font("Segoe UI", 9F), ForeColor = isDark ? Color.FromArgb(242, 243, 245) : Color.FromArgb(33, 33, 33), AutoSize = true, MaximumSize = new Size(commentPanel.Width - 30, 0), Location = new Point(10, 50) };
        
        // Add report button for comments from other users
        if (currentUser != null && comment.UserID != currentUser.UserID)
        {
          RoundedButton btnReportComment = new RoundedButton
          {
            Text = "⚠",
            Size = new Size(30, 25),
            BackColor = Color.FromArgb(255, 193, 7),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand,
            Font = new Font("Segoe UI", 10F, FontStyle.Bold),
            Anchor = AnchorStyles.Top | AnchorStyles.Right
          };
          btnReportComment.FlatAppearance.BorderSize = 0;
          btnReportComment.Location = new Point(commentPanel.Width - 45, 10);
          btnReportComment.Click += (s, ev) => BtnReportComment_Click(comment);
          
          commentPanel.Controls.Add(btnReportComment);
        }
        
        commentPanel.Controls.AddRange(new Control[] { lblCommenter, lblCommentDate, lblCommentContent });
        commentPanel.Height = lblCommentContent.Bottom + 15;
        UIHelper.ApplyRoundedCorners(commentPanel, 12);
        flpComments.Controls.Add(commentPanel);
      }
    }

    private void BtnLike_Click(object? sender, EventArgs e)
    {
      try
      {
        if (currentUser == null)
        {
          MessageBox.Show("Bạn cần đăng nhập để thích bài viết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        if (post == null) return;
        string likeFilePath = "datas\\Likes.csv";
        if (isLiked)
        {
          var myLike = likes?.FirstOrDefault(l => l.UserID == currentUser.UserID);
          if (myLike != null && myLike.Delete(likeFilePath))
          {
            likes?.Remove(myLike);
            isLiked = false;
            if (post.LikesCount > 0)
            {
              post.LikesCount--;
              UpdatePostCounts(post);
            }
          }
        }
        else
        {
          var newLike = new Like { LikeID = 0, PostID = postId, UserID = currentUser.UserID, CreatedAt = DateTime.Now };
          if (newLike.Save(likeFilePath))
          {
            likes?.Add(newLike);
            isLiked = true;
            post.LikesCount++;
            UpdatePostCounts(post);
          }
        }
        UpdateUI();
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi like: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnAddComment_Click(object? sender, EventArgs e)
    {
      try
      {
        if (currentUser == null)
        {
          MessageBox.Show("Bạn cần đăng nhập để bình luận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        if (txtComment == null || string.IsNullOrWhiteSpace(txtComment.Text))
        {
          MessageBox.Show("Vui lòng nhập nội dung bình luận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          txtComment?.Focus();
          return;
        }
        if (post == null) return;
        var newComment = new Comment { PostID = postId, UserID = currentUser.UserID, Content = txtComment.Text.Trim(), CreatedAt = DateTime.Now };
        if (newComment.Save("datas\\Comment.csv"))
        {
          comments?.Add(newComment);
          post.CommentsCount++;
          UpdatePostCounts(post);
          txtComment.Clear();
          UpdateUI();
          MessageBox.Show("Đã thêm bình luận!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi thêm bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void UpdatePostCounts(Post post)
    {
      try
      {
        string filePath = "datas\\Post.csv";
        if (!System.IO.File.Exists(filePath))
          return;

        var lines = System.IO.File.ReadAllLines(filePath).ToList();

        for (int i = 1; i < lines.Count; i++)
        {
          if (string.IsNullOrWhiteSpace(lines[i]))
            continue;

          var parts = lines[i].Split(',');
          if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == post.PostID)
          {
            parts[5] = post.LikesCount.ToString();
            parts[6] = post.CommentsCount.ToString();
            parts[8] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lines[i] = string.Join(",", parts);
            break;
          }
        }

        System.IO.File.WriteAllLines(filePath, lines);
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Error updating post counts: {ex.Message}");
      }
    }

    private void BtnReportPost_Click(object? sender, EventArgs e)
    {
      if (post == null || currentUser == null) return;

      // Tạo form dialog để chọn lý do tố cáo
      Form reportDialog = new Form
      {
        Text = "Tố cáo bài viết",
        Size = new Size(400, 220),
        StartPosition = FormStartPosition.CenterParent,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        MaximizeBox = false,
        MinimizeBox = false
      };

      Label lblReason = new Label
      {
        Text = "Vui lòng chọn lý do tố cáo:",
        Location = new Point(20, 20),
        AutoSize = true,
        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
      };

      ComboBox cmbReason = new ComboBox
      {
        Location = new Point(20, 50),
        Size = new Size(340, 25),
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = new Font("Segoe UI", 9F)
      };

      cmbReason.Items.AddRange(new string[]
      {
        "Nội dung spam hoặc quảng cáo",
        "Ngôn từ thù ghét hoặc bạo lực",
        "Thông tin sai lệch hoặc lừa đảo",
        "Nội dung khiêu dâm hoặc nhạy cảm",
        "Vi phạm bản quyền"
      });
      cmbReason.SelectedIndex = 0;

      RoundedButton btnConfirm = new RoundedButton
      {
        Text = "Xác nhận",
        Location = new Point(180, 100),
        Size = new Size(90, 35),
        BackColor = Color.FromArgb(220, 53, 69),
        ForeColor = Color.White,
        Cursor = Cursors.Hand,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
      };

      RoundedButton btnCancelReport = new RoundedButton
      {
        Text = "Hủy",
        Location = new Point(280, 100),
        Size = new Size(80, 35),
        BackColor = Color.FromArgb(108, 117, 125),
        ForeColor = Color.White,
        Cursor = Cursors.Hand,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
      };

      btnConfirm.Click += (s, ev) =>
      {
        try
        {
          string reason = cmbReason.SelectedItem?.ToString() ?? "Không xác định";
          Guid? reportedUserId;
          bool success = PostService.ReportPost(post.PostID, currentUser.UserID, out reportedUserId, reason);

          if (success)
          {
            MessageBox.Show($"Đã tố cáo bài viết thành công!\nLý do: {reason}\n\nBài viết đã bị ẩn.",
              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reportDialog.DialogResult = DialogResult.OK;
            reportDialog.Close();
            CloseRequested?.Invoke(this, EventArgs.Empty);
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Lỗi khi tố cáo bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      };

      btnCancelReport.Click += (s, ev) =>
      {
        reportDialog.DialogResult = DialogResult.Cancel;
        reportDialog.Close();
      };

      reportDialog.Controls.AddRange(new Control[] { lblReason, cmbReason, btnConfirm, btnCancelReport });
      reportDialog.ShowDialog();
    }

    private void BtnReportComment_Click(Comment comment)
    {
      if (comment == null || currentUser == null) return;

      // Tạo form dialog để chọn lý do tố cáo comment
      Form reportDialog = new Form
      {
        Text = "Tố cáo bình luận",
        Size = new Size(400, 220),
        StartPosition = FormStartPosition.CenterParent,
        FormBorderStyle = FormBorderStyle.FixedDialog,
        MaximizeBox = false,
        MinimizeBox = false
      };

      Label lblReason = new Label
      {
        Text = "Vui lòng chọn lý do tố cáo:",
        Location = new Point(20, 20),
        AutoSize = true,
        Font = new Font("Segoe UI", 10F, FontStyle.Bold)
      };

      ComboBox cmbReason = new ComboBox
      {
        Location = new Point(20, 50),
        Size = new Size(340, 25),
        DropDownStyle = ComboBoxStyle.DropDownList,
        Font = new Font("Segoe UI", 9F)
      };

      cmbReason.Items.AddRange(new string[]
      {
        "Nội dung spam hoặc quảng cáo",
        "Ngôn từ thù ghét hoặc bạo lực",
        "Thông tin sai lệch hoặc lừa đảo",
        "Nội dung khiêu dâm hoặc nhạy cảm",
        "Quấy rối hoặc bắt nạt"
      });
      cmbReason.SelectedIndex = 0;

      RoundedButton btnConfirm = new RoundedButton
      {
        Text = "Xác nhận",
        Location = new Point(180, 100),
        Size = new Size(90, 35),
        BackColor = Color.FromArgb(220, 53, 69),
        ForeColor = Color.White,
        Cursor = Cursors.Hand,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
      };

      RoundedButton btnCancelReport = new RoundedButton
      {
        Text = "Hủy",
        Location = new Point(280, 100),
        Size = new Size(80, 35),
        BackColor = Color.FromArgb(108, 117, 125),
        ForeColor = Color.White,
        Cursor = Cursors.Hand,
        Font = new Font("Segoe UI", 9F, FontStyle.Bold)
      };

      btnConfirm.Click += (s, ev) =>
      {
        try
        {
          string reason = cmbReason.SelectedItem?.ToString() ?? "Không xác định";
          Guid? reportedUserId;
          
          var commentService = new CommentService();
          bool success = commentService.ReportComment(comment.CommentID, currentUser.UserID, out reportedUserId, reason);

          if (success)
          {
            MessageBox.Show($"Đã tố cáo bình luận thành công!\nLý do: {reason}\n\nBình luận đã bị ẩn.",
              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            reportDialog.DialogResult = DialogResult.OK;
            reportDialog.Close();
            
            // Reload comments to reflect changes
            LoadCommentsData();
            DisplayComments();
          }
        }
        catch (Exception ex)
        {
          MessageBox.Show($"Lỗi khi tố cáo bình luận: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      };

      btnCancelReport.Click += (s, ev) =>
      {
        reportDialog.DialogResult = DialogResult.Cancel;
        reportDialog.Close();
      };

      reportDialog.Controls.AddRange(new Control[] { lblReason, cmbReason, btnConfirm, btnCancelReport });
      reportDialog.ShowDialog();
    }

    private void BtnDeletePost_Click(object? sender, EventArgs e)
    {
      if (post == null || currentUser == null) return;

      var result = MessageBox.Show(
          "Bạn có chắc muốn xóa bài viết này?\nHành động này không thể hoàn tác!",
          "Xác nhận xóa",
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Warning);

      if (result != DialogResult.Yes)
        return;

      try
      {
        var postService = new PostService();
        if (postService.SoftDeletePost(post.PostID))
        {
          MessageBox.Show("Đã xóa bài viết thành công!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
          CloseRequested?.Invoke(this, EventArgs.Empty);
        }
        else
        {
          MessageBox.Show("Không thể xóa bài viết!", "Lỗi",
              MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xóa bài viết: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void RepositionHeaderButtons()
    {
      if (pnlHeader == null) return;

      int rightMargin = 15;
      int currentX = pnlHeader.Width - rightMargin;

      // Nút đóng sát phải nhất
      if (btnClose != null)
      {
        btnClose.Location = new Point(currentX - btnClose.Width, 18);
        currentX -= btnClose.Width + 10;
      }

      // Nút xóa (nếu visible)
      if (btnDeletePost != null && btnDeletePost.Visible)
      {
        btnDeletePost.Location = new Point(currentX - btnDeletePost.Width, 18);
        currentX -= btnDeletePost.Width + 10;
      }

      // Nút tố cáo (nếu visible)
      if (btnReportPost != null && btnReportPost.Visible)
      {
        btnReportPost.Location = new Point(currentX - btnReportPost.Width, 18);
      }
    }
  }
}
