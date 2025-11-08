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

    public event EventHandler? CloseRequested;

    private Panel? pnlHeader;
    private Label? lblAuthor;
    private Label? lblDate;
    private Panel? pnlContent;
    private Label? lblContent;
    private Panel? pnlStats;
    private Button? btnLike;
    private Label? lblLikeCount;
    private Label? lblCommentCount;
    private FlowLayoutPanel? flpComments;
    private Panel? pnlAddComment;
    private TextBox? txtComment;
    private Button? btnAddComment;
    private Button? btnClose;

    public ucPostDetail(int postId)
    {
      this.postId = postId;
      currentUser = AuthSessionService.CurrentUser;
      comments = new List<Comment>();
      likes = new List<Like>();
      InitializeComponent();
      LoadPostData();
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
      btnClose = new Button { Location = new Point(600, 15), Size = new Size(80, 35), Text = "Đóng", BackColor = Color.Gray, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
      btnClose.FlatAppearance.BorderSize = 0;
      btnClose.Click += (s, e) => CloseRequested?.Invoke(this, EventArgs.Empty);
      UIHelper.ApplyRoundedCorners(btnClose, 8);
      pnlHeader.Controls.AddRange(new Control[] { lblAuthor, lblDate, btnClose });

      pnlContent = new Panel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(15, 10, 15, 15), BackColor = Color.White };
      lblContent = new Label { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 10F), AutoSize = true, MaximumSize = new Size(650, 0) };
      pnlContent.Controls.Add(lblContent);

      pnlStats = new Panel { Dock = DockStyle.Top, Height = 50, BackColor = Color.White, Padding = new Padding(15, 5, 15, 5) };
      btnLike = new Button { Location = new Point(15, 10), Size = new Size(90, 30), Text = "Thích", FlatStyle = FlatStyle.Flat, BackColor = Color.White, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 9F) };
      btnLike.FlatAppearance.BorderSize = 1;
      btnLike.Click += BtnLike_Click;
      UIHelper.ApplyRoundedCorners(btnLike, 8);
      lblLikeCount = new Label { Location = new Point(110, 15), Font = new Font("Segoe UI", 9F), AutoSize = true };
      lblCommentCount = new Label { Location = new Point(220, 15), Font = new Font("Segoe UI", 9F), AutoSize = true };
      pnlStats.Controls.AddRange(new Control[] { btnLike, lblLikeCount, lblCommentCount });

      Label lblCommentsTitle = new Label { Dock = DockStyle.Top, Text = "Bình luận", Font = new Font("Segoe UI", 11F, FontStyle.Bold), Height = 35, Padding = new Padding(15, 10, 0, 0), BackColor = Color.FromArgb(250, 250, 250) };
      flpComments = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false, AutoScroll = true, Padding = new Padding(15), BackColor = Color.FromArgb(250, 250, 250) };

      pnlAddComment = new Panel { Dock = DockStyle.Bottom, Height = 80, BackColor = Color.White, Padding = new Padding(15, 10, 15, 10) };
      txtComment = new TextBox { Location = new Point(15, 15), Size = new Size(540, 50), Multiline = true, Font = new Font("Segoe UI", 9F), BorderStyle = BorderStyle.None };
      UIHelper.ApplyRoundedCorners(txtComment, 10);
      btnAddComment = new Button { Location = new Point(565, 15), Size = new Size(110, 50), Text = "Gửi", BackColor = Color.FromArgb(0, 123, 255), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
      btnAddComment.FlatAppearance.BorderSize = 0;
      btnAddComment.Click += BtnAddComment_Click;
      UIHelper.ApplyRoundedCorners(btnAddComment, 10);
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
        var userService = new UserService();
        var users = userService.GetAllUsers();
        var author = users.FirstOrDefault(u => u.UserID == post.UserID);
        if (lblAuthor != null) lblAuthor.Text = author?.FullName ?? "Người dùng";
        if (lblDate != null) lblDate.Text = post.CreatedAt.ToString("dd/MM/yyyy HH:mm");
        if (lblContent != null) lblContent.Text = post.Content;
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
          btnLike.Text = "Đã thích";
          btnLike.BackColor = Color.FromArgb(255, 220, 220);
        }
        else
        {
          btnLike.Text = "Thích";
          btnLike.BackColor = Color.White;
        }
      }
      DisplayComments();
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
      var userService = new UserService();
      var users = userService.GetAllUsers();

      var visibleComments = comments.Where(c => !c.IsDeleted).ToList();

      foreach (var comment in visibleComments)
      {
        var commenter = users.FirstOrDefault(u => u.UserID == comment.UserID);
        Panel commentPanel = new Panel { Width = flpComments.ClientSize.Width - 40, AutoSize = true, Padding = new Padding(10), Margin = new Padding(0, 5, 0, 5), BackColor = Color.White, BorderStyle = BorderStyle.None };
        UIHelper.ApplyRoundedCorners(commentPanel, 12);

        Label lblCommenter = new Label { Text = commenter?.FullName ?? "Người dùng", Font = new Font("Segoe UI", 9F, FontStyle.Bold), AutoSize = true, Location = new Point(10, 10) };
        Label lblCommentDate = new Label { Text = comment.CreatedAt.ToString("dd/MM/yyyy HH:mm"), Font = new Font("Segoe UI", 8F), ForeColor = Color.Gray, AutoSize = true, Location = new Point(10, 30) };
        Label lblCommentContent = new Label { Text = comment.Content, Font = new Font("Segoe UI", 9F), AutoSize = true, MaximumSize = new Size(commentPanel.Width - 100, 0), Location = new Point(10, 50) };

        commentPanel.Controls.AddRange(new Control[] { lblCommenter, lblCommentDate, lblCommentContent });

        int buttonX = commentPanel.Width - 45;

        if (currentUser != null && (comment.UserID == currentUser.UserID || currentUser.Role == 1))
        {
          Button btnDeleteComment = new Button
          {
            Text = "Xóa",
            Font = new Font("Segoe UI", 9F, FontStyle.Bold),
            Size = new Size(40, 30),
            Location = new Point(buttonX, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BackColor = Color.FromArgb(220, 53, 69),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
          };
          btnDeleteComment.FlatAppearance.BorderSize = 0;

          btnDeleteComment.Tag = comment.CommentID;
          btnDeleteComment.Click += BtnDeleteComment_Click;
          UIHelper.ApplyRoundedCorners(btnDeleteComment, 8);

          commentPanel.Controls.Add(btnDeleteComment);
          buttonX -= 45;
        }

        if (currentUser != null && comment.UserID != currentUser.UserID && post != null && comment.UserID != post.UserID)
        {
          Button btnReportComment = new Button
          {
            Text = "Tố cáo",
            Font = new Font("Segoe UI", 8F, FontStyle.Bold),
            Size = new Size(50, 30),
            Location = new Point(buttonX, 10),
            Anchor = AnchorStyles.Top | AnchorStyles.Right,
            BackColor = Color.FromArgb(255, 152, 0),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Cursor = Cursors.Hand
          };
          btnReportComment.FlatAppearance.BorderSize = 0;

          btnReportComment.Tag = comment.CommentID;
          btnReportComment.Click += BtnReportComment_Click;
          UIHelper.ApplyRoundedCorners(btnReportComment, 8);

          commentPanel.Controls.Add(btnReportComment);
        }

        commentPanel.Height = Math.Max(lblCommentContent.Bottom + 15, 70);
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
        MessageBox.Show($"Lỗi khi thích: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    private void BtnDeleteComment_Click(object? sender, EventArgs e)
    {
      try
      {
        if (sender is Button btn && btn.Tag is int commentId)
        {
          var result = MessageBox.Show(
              "Bạn có chắc muốn xóa bình luận này?\n(Bình luận sẽ bị ẩn)",
              "Xác nhận xóa",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

          if (result != DialogResult.Yes)
            return;

          var commentService = new CommentService();
          if (commentService.SoftDeleteComment(commentId))
          {
            MessageBox.Show("Đã xóa bình luận!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload comments
            LoadPostData();
          }
          else
          {
            MessageBox.Show("Không thể xóa bình luận!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xóa bình luận: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnReportComment_Click(object? sender, EventArgs e)
    {
      try
      {
        if (currentUser == null)
        {
          MessageBox.Show("Bạn cần đăng nhập để tố cáo!", "Thông báo",
              MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }

        if (sender is Button btn && btn.Tag is int commentId)
        {
          var result = MessageBox.Show(
              "Bạn có chắc muốn tố cáo bình luận này?\n" +
              "(Bình luận sẽ bị ẩn và tài khoản người dùng sẽ được ghi nhận vi phạm)",
              "Xác nhận tố cáo",
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Warning);

          if (result != DialogResult.Yes)
            return;

          var commentService = new CommentService();
          Guid? reportedUserId;
          if (commentService.ReportComment(commentId, currentUser.UserID, out reportedUserId))
          {
            // Lấy thông tin user bị tố cáo để hiển thị trạng thái
            string statusMessage = "Đã tố cáo bình luận!";
            if (reportedUserId.HasValue)
            {
              var userService = new UserService();
              var reportedUser = userService.GetUserById(reportedUserId.Value);
              if (reportedUser != null)
              {
                // Cập nhật ReportCount từ ReportService
                int reportCount = userService.GetReportCount(reportedUser.UserID);
                reportedUser.ReportCount = reportCount;

                statusMessage += $"\n\nTài khoản: {reportedUser.UserName}" +
                                $"\nSố lần vi phạm: {reportedUser.ReportCount}" +
                                $"\nTrạng thái: {reportedUser.AccountStatus}";
              }
            }

            MessageBox.Show(statusMessage, "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Reload comments
            LoadPostData();
          }
          else
          {
            MessageBox.Show("Không thể tố cáo bình luận!\n(Có thể bạn đã tố cáo rồi hoặc đây là comment của bạn)", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tố cáo bình luận: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
  }
}
