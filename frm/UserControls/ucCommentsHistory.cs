using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SocialManager.models;
using SocialManager.services;
using SocialManager.utils;
using SocialManager.controls;

namespace SocialManager.frm.UserControls
{
  // Clean single definition replacing corrupted duplicates
  public class ucCommentsHistory : UserControl
  {
    private readonly CommentService _commentService = new CommentService();
    private readonly PostService _postService = new PostService();
    private FlowLayoutPanel flpComments = null!;
    private Label lblTitle = null!;

    public Guid CurrentUserId { get; set; }
    public event EventHandler<int>? OnOpenPostRequested;

    public ucCommentsHistory(Guid currentUserId)
    {
      CurrentUserId = currentUserId;
      InitializeComponent();
      LoadHistory();
      ApplyTheme(GlobalSettings.DarkMode);
    }

    private void InitializeComponent()
    {
      Dock = DockStyle.Fill;
      BackColor = Color.Transparent;

      lblTitle = new Label
      {
        Text = "Your Comment History",
        AutoSize = true,
        Font = new Font("Segoe UI", 16, FontStyle.Bold),
        Location = new Point(20, 20)
      };

      flpComments = new FlowLayoutPanel
      {
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoScroll = true,
        Location = new Point(20, 70),
        Size = new Size(Width - 40, Height - 90),
        Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
        Padding = new Padding(5)
      };

      Controls.Add(lblTitle);
      Controls.Add(flpComments);
      Resize += (_, __) => { flpComments.Size = new Size(Width - 40, Height - 90); };
    }

    private void LoadHistory()
    {
      flpComments.Controls.Clear();
      var comments = _commentService.GetAllComments()
        .Where(c => c.UserID == CurrentUserId)
        .OrderByDescending(c => c.CreatedAt)
        .Take(500)
        .ToList();

      foreach (var c in comments)
      {
        var post = _postService.GetAllPosts(includeDeleted: true).FirstOrDefault(p => p.PostID == c.PostID);
        flpComments.Controls.Add(BuildCommentPanel(c, post));
      }
    }

    private Panel BuildCommentPanel(Comment comment, Post? post)
    {
      var p = new Panel
      {
        Width = flpComments.Width - 30,
        Height = 120,
        Margin = new Padding(5),
        Padding = new Padding(10),
        BackColor = Color.FromArgb(240, 244, 248)
      };
      p.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

      var lblPost = new Label
      {
        AutoSize = false,
        Text = post != null ? $"Post #{post.PostID}: {Truncate(post.Content, 60)}" : "(Original post deleted)",
        Dock = DockStyle.Top,
        Height = 30,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
      };

      var lblComment = new Label
      {
        AutoSize = false,
        Text = comment.Content,
        Dock = DockStyle.Top,
        Height = 50,
        Font = new Font("Segoe UI", 10, FontStyle.Regular)
      };

      var lblMeta = new Label
      {
        AutoSize = false,
        Text = $"At: {comment.CreatedAt:yyyy-MM-dd HH:mm}",
        Dock = DockStyle.Bottom,
        Height = 25,
        Font = new Font("Segoe UI", 9, FontStyle.Italic)
      };

      var btnOpen = new RoundedButton
      {
        Text = "Open Post",
        Width = 110,
        Height = 32,
        Location = new Point(p.Width - 130, 75),
        Anchor = AnchorStyles.Top | AnchorStyles.Right,
        BorderRadius = 18
      };
      btnOpen.Click += (_, __) => OnOpenPostRequested?.Invoke(this, post?.PostID ?? -1);

      p.Controls.Add(btnOpen);
      p.Controls.Add(lblMeta);
      p.Controls.Add(lblComment);
      p.Controls.Add(lblPost);
      p.Resize += (_, __) => btnOpen.Location = new Point(p.Width - 130, 75);

      StyleCommentPanel(p, lblPost, lblComment, lblMeta, btnOpen);
      return p;
    }

    private void StyleCommentPanel(Panel p, Label lblPost, Label lblComment, Label lblMeta, RoundedButton btnOpen)
    {
      bool dark = GlobalSettings.DarkMode;
      p.BackColor = dark ? Color.FromArgb(40, 45, 55) : Color.FromArgb(240, 244, 248);
      lblPost.ForeColor = dark ? Color.Cyan : Color.DodgerBlue;
      lblComment.ForeColor = dark ? Color.Gainsboro : Color.Black;
      lblMeta.ForeColor = dark ? Color.LightGray : Color.Gray;
      btnOpen.BackColor = dark ? Color.FromArgb(30, 120, 200) : Color.FromArgb(0, 120, 215);
      btnOpen.ForeColor = Color.White;
      UIHelper.ApplyRoundedCorners(p, 12);
    }

    private string Truncate(string? input, int max)
    {
      if (string.IsNullOrEmpty(input)) return string.Empty;
      return input.Length <= max ? input : input.Substring(0, max - 3) + "...";
    }

    public void ApplyTheme(bool dark)
    {
      BackColor = dark ? Color.FromArgb(28, 30, 34) : Color.White;
      lblTitle.ForeColor = dark ? Color.Cyan : Color.DodgerBlue;
      foreach (Control c in flpComments.Controls)
      {
        if (c is Panel p)
        {
          var btn = p.Controls.OfType<RoundedButton>().FirstOrDefault();
          var labels = p.Controls.OfType<Label>().ToList();
          if (btn != null && labels.Count >= 3)
          {
            var lblPost = labels.FirstOrDefault(l => l.Font.Bold) ?? labels.First();
            var lblMeta = labels.FirstOrDefault(l => l.Font.Italic) ?? labels.Last();
            var lblComment = labels.Except(new[] { lblPost, lblMeta }).FirstOrDefault() ?? labels.First();
            StyleCommentPanel(p, lblPost, lblComment, lblMeta, btn);
          }
        }
      }
    }
  }
}
