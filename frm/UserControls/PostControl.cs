using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;
using SocialManager.utils;
using SocialManager.controls;

namespace SocialManager.controls
{
  public partial class PostControl : UserControl
  {
    public string PostId { get; private set; } = "";
    public event EventHandler<string>? PostClicked;
    public event EventHandler? PostReported; // Event khi bài viết bị tố cáo

    private Post? postData;
    private User? currentUser;
    private User? postOwner;
    private bool isLiked = false;
    private int likesCount = 0;
    private int commentsCount = 0;
    private bool isDarkMode = false; // theme flag

    public PostControl()
    {
      InitializeComponent();
      SetupControl();
      ApplyCardStyle();
    }

    public PostControl(Post post)
    {
      InitializeComponent();

      this.postData = post;
      this.currentUser = AuthSessionService.CurrentUser;
      this.PostId = post.PostID.ToString();

      SetupControl();
      ApplyCardStyle();
      LoadPostData();
      LoadLikesAndComments();
      AdjustHeight();
    }

    /// <summary>
    /// Áp dụng theme cho PostControl (dark/light)
    /// </summary>
    public void ApplyTheme(bool isDark)
    {
      isDarkMode = isDark;

      if (gbPostContainer != null)
      {
        gbPostContainer.BackColor = isDark ? Color.FromArgb(36, 37, 38) : Color.FromArgb(250, 251, 252);
        gbPostContainer.ForeColor = isDark ? Color.White : Color.FromArgb(64, 64, 64);
      }

      this.BackColor = Color.Transparent; // Match composer transparent card host

      if (lblPostInfo != null)
      {
        lblPostInfo.ForeColor = isDark ? Color.FromArgb(176, 179, 184) : SystemColors.ControlDarkDark;
      }
      if (lblContent != null)
      {
        lblContent.ForeColor = isDark ? Color.FromArgb(242, 243, 245) : Color.FromArgb(64, 64, 64);
      }
      if (picAvatar != null && picAvatar.Image == null)
      {
        picAvatar.BackColor = isDark ? Color.FromArgb(58, 59, 60) : Color.LightGray;
      }

      UpdateLikeButton();
      UpdateCommentButton();
      Invalidate(); // redraw custom card border
    }

    private void LoadPostData()
    {
      if (postData == null) return;

      try
      {
        var userService = new UserService();
        postOwner = userService.GetUserById(postData.UserID);

        string displayName = postOwner?.FullName ?? "Người dùng không xác định";
        lblPostInfo.Text = $"{displayName} - {postData.CreatedAt:dd/MM/yyyy HH:mm}";

        // Load avatar
        if (picAvatar != null)
        {
          try
          {
            if (!string.IsNullOrEmpty(postOwner?.AvatarUrl) && System.IO.File.Exists(postOwner.AvatarUrl))
            {
              picAvatar.Image = Image.FromFile(postOwner.AvatarUrl);
            }
            else
            {
              // Fallback: draw initial
              picAvatar.Image = GenerateInitialAvatar(postOwner?.FullName ?? "?");
            }
            UIHelper.MakeCircular(picAvatar);
            picAvatar.Cursor = Cursors.Hand;
            picAvatar.Click += (s, e) => OnPostClicked();
          }
          catch { }
        }
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine($"Lỗi tải thông tin người đăng: {ex.Message}");
        lblPostInfo.Text = $"Người dùng - {postData.CreatedAt:dd/MM/yyyy HH:mm}";
      }

      lblContent.Text = postData.Content;
      lblContent.MaximumSize = new System.Drawing.Size(660, 0);
      lblContent.AutoSize = true;
    }

    private void LoadLikesAndComments()
    {
      if (postData == null) return;

      try
      {
        // Load likes
        var likes = Like.GetLikesByPostId(GlobalSetting.LikesFilePath, postData.PostID);
        likesCount = likes.Count;

        if (currentUser != null)
        {
          isLiked = likes.Any(l => l.UserID == currentUser.UserID);
        }
        var comments = Comment.GetCommentsByPostId(GlobalSetting.CommentsFilePath, postData.PostID);
        commentsCount = comments.Count;

        UpdateLikeButton();
        UpdateCommentButton();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Lỗi tải lượt thích/bình luận: {ex.Message}");
      }
    }

    private void UpdateLikeButton()
    {
      if (btnLike == null) return;

      if (isLiked)
      {
        btnLike.Text = $"♥ {likesCount}"; // Use heart symbol
        if (isDarkMode)
        {
          btnLike.BackColor = Color.FromArgb(64, 20, 24);
          btnLike.BackgroundColor = Color.FromArgb(64, 20, 24);
          btnLike.ForeColor = Color.FromArgb(255, 99, 132);
          btnLike.TextColor = Color.FromArgb(255, 99, 132);
        }
        else
        {
          btnLike.BackColor = Color.FromArgb(255, 245, 245);
          btnLike.BackgroundColor = Color.FromArgb(255, 245, 245);
          btnLike.ForeColor = Color.FromArgb(220, 53, 69);
          btnLike.TextColor = Color.FromArgb(220, 53, 69);
        }
      }
      else
      {
        btnLike.Text = $"♡ {likesCount}"; // Use outline heart symbol
        if (isDarkMode)
        {
          btnLike.BackColor = Color.FromArgb(58, 59, 60);
          btnLike.BackgroundColor = Color.FromArgb(58, 59, 60);
          btnLike.ForeColor = Color.FromArgb(176, 179, 184);
          btnLike.TextColor = Color.FromArgb(176, 179, 184);
        }
        else
        {
          btnLike.BackColor = Color.FromArgb(248, 249, 250);
          btnLike.BackgroundColor = Color.FromArgb(248, 249, 250);
          btnLike.ForeColor = Color.FromArgb(108, 117, 125);
          btnLike.TextColor = Color.FromArgb(108, 117, 125);
        }
      }
    }

    private void UpdateCommentButton()
    {
      if (btnComment == null) return;
      btnComment.Text = $"💬 {commentsCount}"; // Use speech bubble symbol
      if (isDarkMode)
      {
        btnComment.BackColor = Color.FromArgb(58, 59, 60);
        btnComment.BackgroundColor = Color.FromArgb(58, 59, 60);
        btnComment.ForeColor = Color.FromArgb(176, 179, 184);
        btnComment.TextColor = Color.FromArgb(176, 179, 184);
      }
      else
      {
        btnComment.BackColor = Color.FromArgb(248, 249, 250);
        btnComment.BackgroundColor = Color.FromArgb(248, 249, 250);
        btnComment.ForeColor = Color.FromArgb(108, 117, 125);
        btnComment.TextColor = Color.FromArgb(108, 117, 125);
      }
    }

    private void SetupControl()
    {
      this.DoubleBuffered = true;
      this.Click += (s, e) => OnPostClicked();

      if (gbPostContainer != null)
        this.gbPostContainer.Click += (s, e) => OnPostClicked();

      if (lblContent != null)
        this.lblContent.Click += (s, e) => OnPostClicked();

      if (lblPostInfo != null)
        this.lblPostInfo.Click += (s, e) => OnPostClicked();

      if (picAvatar != null)
        this.picAvatar.Click += (s, e) => OnPostClicked();

      if (btnLike != null)
        this.btnLike.Click += BtnLike_Click;

      if (btnComment != null)
        this.btnComment.Click += BtnComment_Click;

      if (postData != null && currentUser != null)
      {
        bool canDelete = (postData.UserID == currentUser.UserID) || (currentUser.Role == 1);
        if (canDelete && btnDelete != null)
        {
          btnDelete.Visible = true;
          btnDelete.Click += BtnDelete_Click;
        }

        if (postData.UserID != currentUser.UserID && btnReport != null)
        {
          btnReport.Visible = true;
        }
      }
    }

    /// <summary>
    /// Áp dụng style "card" giống phần composer: bo tròn, nền nhạt, viền mỏng.
    /// </summary>
    private void ApplyCardStyle()
    {
      if (gbPostContainer == null) return;
      gbPostContainer.Text = string.Empty; // bỏ tiêu đề groupbox
      gbPostContainer.Padding = new Padding(15, 15, 15, 20);
      gbPostContainer.BackColor = isDarkMode ? Color.FromArgb(36, 37, 38) : Color.FromArgb(250, 251, 252);

      // Gắn Paint event để tự vẽ viền bo tròn
      gbPostContainer.Paint -= GbPostContainer_Paint; // tránh đăng ký trùng
      gbPostContainer.Paint += GbPostContainer_Paint;

      // Chuẩn hóa style các nút hành động
      StyleActionButton(btnLike);
      StyleActionButton(btnComment);
      StyleIconButton(btnDelete, Color.FromArgb(255, 83, 73));
      StyleIconButton(btnReport, Color.FromArgb(255, 117, 24));
    }

    private void StyleActionButton(RoundedButton? btn)
    {
      if (btn == null) return;
      btn.BorderRadius = 18;
      btn.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      if (isDarkMode)
      {
        btn.BackColor = Color.FromArgb(58, 59, 60);
        btn.BackgroundColor = btn.BackColor;
        btn.ForeColor = Color.FromArgb(176, 179, 184);
        btn.TextColor = btn.ForeColor;
      }
      else
      {
        btn.BackColor = Color.White;
        btn.BackgroundColor = btn.BackColor;
        btn.ForeColor = Color.FromArgb(108, 117, 125);
        btn.TextColor = btn.ForeColor;
      }
      btn.FlatStyle = FlatStyle.Flat;
      btn.FlatAppearance.BorderSize = 0;
      // Hover
      btn.MouseEnter += (s, e) =>
      {
        if (btn == null) return;
        btn.BackColor = isDarkMode ? Color.FromArgb(76, 77, 78) : Color.FromArgb(245, 246, 247);
        btn.BackgroundColor = btn.BackColor;
      };
      btn.MouseLeave += (s, e) =>
      {
        if (btn == null) return;
        btn.BackColor = isDarkMode ? Color.FromArgb(58, 59, 60) : Color.White;
        btn.BackgroundColor = btn.BackColor;
      };
    }

    private void StyleIconButton(RoundedButton? btn, Color baseColor)
    {
      if (btn == null) return;
      btn.BorderRadius = 20;
      btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      btn.Size = new Size(34, 34);
      btn.FlatStyle = FlatStyle.Flat;
      btn.FlatAppearance.BorderSize = 0;
      btn.BackColor = baseColor;
      btn.BackgroundColor = baseColor;
      btn.ForeColor = Color.White;
      btn.TextColor = Color.White;
    }

    private void GbPostContainer_Paint(object? sender, PaintEventArgs e)
    {
      if (gbPostContainer == null) return;
      e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
      var rect = gbPostContainer.ClientRectangle;
      rect.Inflate(-2, -2);
      int radius = 18;
      using (var path = new System.Drawing.Drawing2D.GraphicsPath())
      {
        int d = radius * 2;
        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
        path.CloseFigure();
        using (var fill = new SolidBrush(gbPostContainer.BackColor))
          e.Graphics.FillPath(fill, path);
        var borderColor = isDarkMode ? Color.FromArgb(58, 59, 60) : Color.FromArgb(225, 226, 228);
        using (var pen = new Pen(borderColor, 1))
          e.Graphics.DrawPath(pen, path);
      }
    }

    private void BtnLike_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      try
      {
        if (isLiked)
        {
          var like = new Like
          {
            PostID = postData.PostID,
            UserID = currentUser.UserID
          };

          if (like.Delete(GlobalSetting.LikesFilePath))
          {
            isLiked = false;
            likesCount--;
            UpdateLikeButton();
          }
        }
        else
        {
          var like = new Like
          {
            PostID = postData.PostID,
            UserID = currentUser.UserID,
            CreatedAt = DateTime.Now
          };

          if (like.Save(GlobalSetting.LikesFilePath))
          {
            isLiked = true;
            likesCount++;
            UpdateLikeButton();
          }
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi thích/bỏ thích: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnComment_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      string commentContent = Microsoft.VisualBasic.Interaction.InputBox("Nhập bình luận của bạn:", "Thêm bình luận", "", -1, -1);

      if (string.IsNullOrWhiteSpace(commentContent))
        return;

      try
      {
        var comment = new Comment
        {
          PostID = postData.PostID,
          UserID = currentUser.UserID,
          Content = commentContent,
          ParentCommentID = null,
          CreatedAt = DateTime.Now,
          UpdatedAt = null
        };

        if (comment.Save(GlobalSetting.CommentsFilePath))
        {
          commentsCount++;
          UpdateCommentButton();
          MessageBox.Show("Đã bình luận!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi bình luận: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      var result = MessageBox.Show(
          "Bạn có chắc muốn xóa bài viết này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

      if (result != DialogResult.Yes)
        return;

      try
      {
        var postService = new PostService();
        if (postService.SoftDeletePost(postData.PostID))
        {
          MessageBox.Show("Đã xóa bài viết!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
          this.Visible = false;
        }
        else
        {
          MessageBox.Show("Không thể xóa bài viết!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi xóa bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnReport_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      var result = MessageBox.Show(
          "Bạn có chắc muốn tố cáo bài viết này?", "Xác nhận tố cáo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

      if (result != DialogResult.Yes)
        return;

      try
      {
        Guid? reportedUserId;
        bool success = PostService.ReportPost(postData.PostID, currentUser.UserID, out reportedUserId);

        if (success)
        {
          MessageBox.Show("Đã tố cáo bài viết thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

          // Ẩn bài viết
          this.Visible = false;

          // Fire event để parent refresh lại danh sách
          PostReported?.Invoke(this, EventArgs.Empty);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi tố cáo bài viết: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AdjustHeight()
    {
      if (lblContent == null || btnLike == null || gbPostContainer == null)
        return;

      try
      {
        // Adjust content max width to container width
        if (gbPostContainer != null && lblContent != null)
        {
          int innerPadding = 30; // left + right inside groupbox
          lblContent.MaximumSize = new System.Drawing.Size(Math.Max(200, gbPostContainer.Width - innerPadding), 0);
        }

        int padding = 20;
        int infoHeight = lblPostInfo.Bottom;
        int contentHeight = lblContent!.Height;
        int buttonHeight = 32;
        int spacing = 15;

        int totalHeight = infoHeight + spacing + contentHeight + spacing + buttonHeight + padding + 30;

        if (totalHeight < 150)
          totalHeight = 150;

        this.Height = totalHeight;
        gbPostContainer!.Height = totalHeight - 10;

        int buttonsY = gbPostContainer.Height - buttonHeight - 15;
        btnLike.Top = buttonsY;
        btnComment.Top = buttonsY;
      }
      catch (Exception ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        this.Height = 270;
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      AdjustHeight();
    }

    private void OnPostClicked()
    {
      PostClicked?.Invoke(this, this.PostId);
    }

    /// <summary>
    /// Generate a simple circular avatar bitmap with the first letter of the user's name
    /// </summary>
    private Bitmap GenerateInitialAvatar(string name)
    {
      var bmp = new Bitmap(64, 64);
      using (var g = Graphics.FromImage(bmp))
      {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        var bgColor = isDarkMode ? Color.FromArgb(58, 59, 60) : Color.FromArgb(230, 230, 230);
        using (var brush = new SolidBrush(bgColor))
        {
          g.FillEllipse(brush, 0, 0, 64, 64);
        }
        string initial = string.IsNullOrWhiteSpace(name) ? "?" : name.Trim()[0].ToString().ToUpper();
        using (var font = new Font("Segoe UI", 24, FontStyle.Bold, GraphicsUnit.Pixel))
        using (var textBrush = new SolidBrush(isDarkMode ? Color.White : Color.FromArgb(64, 64, 64)))
        {
          var size = g.MeasureString(initial, font);
          g.DrawString(initial, font, textBrush, (64 - size.Width) / 2, (64 - size.Height) / 2);
        }
      }
      return bmp;
    }
  }
}
