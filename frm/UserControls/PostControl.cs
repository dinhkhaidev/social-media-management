using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager.controls
{
  public partial class PostControl : UserControl
  {
    public string PostId { get; private set; } = "";
    public event EventHandler<string>? PostClicked;

    private Post? postData;
    private User? currentUser;
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
      SetupControl();

      this.postData = post;
      this.currentUser = AuthSessionService.CurrentUser;
      this.PostId = post.PostID.ToString();

      LoadPostData();
      LoadLikesAndComments();
      AdjustHeight();
    }

    private void LoadPostData()
    {
      if (postData == null) return;

      lblPostInfo.Text = $"ID: {postData.PostID} - {postData.CreatedAt:dd/MM/yyyy HH:mm}";
      lblContent.Text = postData.Content;
      lblStatus.Visible = postData.UpdatedAt.HasValue;
    }

    private void LoadLikesAndComments()
    {
      if (postData == null) return;

      try
      {
        // Load likes
        var likes = Like.GetLikesByPostId(GlobalSetting.LikesFilePath, postData.PostID);
        likesCount = likes.Count;

        // Kiểm tra user đã like chưa
        if (currentUser != null)
        {
          isLiked = likes.Any(l => l.UserID == currentUser.UserID);
        }

        // Load comments
        var comments = Comment.GetCommentsByPostId(GlobalSetting.CommentsFilePath, postData.PostID);
        commentsCount = comments.Count;

        // Cập nhật UI
        UpdateLikeButton();
        UpdateCommentButton();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error loading likes/comments: {ex.Message}");
      }
    }

    private void UpdateLikeButton()
    {
      if (isLiked)
      {
        btnLike.Text = $"❤️ {likesCount}";
        btnLike.BackColor = System.Drawing.Color.FromArgb(255, 224, 230);
        btnLike.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
      }
      else
      {
        btnLike.Text = $"🤍 {likesCount}";
        btnLike.BackColor = System.Drawing.Color.WhiteSmoke;
        btnLike.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
      }
    }

    private void UpdateCommentButton()
    {
      btnComment.Text = $"💬 {commentsCount}";
    }

    private void SetupControl()
    {
      this.DoubleBuffered = true;
      this.Click += (s, e) => OnPostClicked();
      this.gbPostContainer.Click += (s, e) => OnPostClicked();
      this.lblContent.Click += (s, e) => OnPostClicked();
      this.lblPostInfo.Click += (s, e) => OnPostClicked();

      // Đăng ký sự kiện cho nút Like và Comment
      this.btnLike.Click += BtnLike_Click;
      this.btnComment.Click += BtnComment_Click;
    }

    private void BtnLike_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      try
      {
        if (isLiked)
        {
          // Unlike
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
          // Like
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
        MessageBox.Show($"Lỗi khi like/unlike: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void BtnComment_Click(object? sender, EventArgs e)
    {
      if (postData == null || currentUser == null) return;

      // Hiển thị dialog để nhập comment
      string commentContent = Microsoft.VisualBasic.Interaction.InputBox(
          "Nhập bình luận của bạn:",
          "Thêm bình luận",
          "",
          -1, -1);

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
          MessageBox.Show("✅ Đã thêm bình luận!", "Thành công",
              MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Lỗi khi thêm bình luận: {ex.Message}", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void AdjustHeight()
    {
      int topPadding = gbPostContainer.Padding.Top;
      int bottomPadding = gbPostContainer.Padding.Bottom;
      int contentBottom = lblContent.Top + lblContent.Height;
      int buttonsTop = btnLike.Top;

      this.Height = contentBottom + (buttonsTop - contentBottom) + btnLike.Height + topPadding + bottomPadding;
    }

    private void OnPostClicked()
    {
      PostClicked?.Invoke(this, this.PostId);
    }
  }
}
