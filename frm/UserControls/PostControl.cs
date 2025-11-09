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

    public PostControl()
    {
      InitializeComponent();
      SetupControl();
    }

    public PostControl(Post post)
    {
      InitializeComponent();

      this.postData = post;
      this.currentUser = AuthSessionService.CurrentUser;
      this.PostId = post.PostID.ToString();

      SetupControl();
      LoadPostData();
      LoadLikesAndComments();
      AdjustHeight();
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
        btnLike.BackColor = System.Drawing.Color.FromArgb(255, 245, 245);
        btnLike.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
        btnLike.BackgroundColor = System.Drawing.Color.FromArgb(255, 245, 245);
        btnLike.TextColor = System.Drawing.Color.FromArgb(220, 53, 69);
      }
      else
      {
        btnLike.Text = $"♡ {likesCount}"; // Use outline heart symbol
        btnLike.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
        btnLike.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
        btnLike.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
        btnLike.TextColor = System.Drawing.Color.FromArgb(108, 117, 125);
      }
    }

    private void UpdateCommentButton()
    {
      if (btnComment == null) return;
      btnComment.Text = $"💬 {commentsCount}"; // Use speech bubble symbol
      btnComment.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
      btnComment.ForeColor = System.Drawing.Color.FromArgb(108, 117, 125);
      btnComment.BackgroundColor = System.Drawing.Color.FromArgb(248, 249, 250);
      btnComment.TextColor = System.Drawing.Color.FromArgb(108, 117, 125);
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
        int padding = 20;
        int infoHeight = lblPostInfo.Bottom;
        int contentHeight = lblContent.Height;
        int buttonHeight = 32;
        int spacing = 15;

        int totalHeight = infoHeight + spacing + contentHeight + spacing + buttonHeight + padding + 30;

        if (totalHeight < 150)
          totalHeight = 150;

        this.Height = totalHeight;
        gbPostContainer.Height = totalHeight - 10;

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

    private void OnPostClicked()
    {
      PostClicked?.Invoke(this, this.PostId);
    }
  }
}
