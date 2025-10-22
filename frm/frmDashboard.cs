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
        public frmDashboard()
        {
            InitializeComponent();
            this.picAvatar.Paint += new PaintEventHandler(picAvatar_Paint);
            this.btnNewPost.Paint += new PaintEventHandler(btnNewPost_Paint);
            this.flowLayoutPanelPosts.Resize += new EventHandler(flowLayoutPanelPosts_Resize);

            LoadPosts();
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
            var posts = new List<Post>();
            // The Post constructor requires at least these properties based on the signature:
            // PostID (int), UserID (Guid), Content (string), MediaUrl (string), Visibility (string), LikesCount (int), CommentsCount (int), CreatedAt (DateTime), UpdatedAt (DateTime?)
            // You must use object initializer syntax since no matching constructor exists.

            Guid currentUserId = AuthSessionService.CurrentUser.UserID; // Assuming this is available

            posts.Add(new Post
            {
                PostID = 12345,
                UserID = currentUserId,
                Content = "Đây là bài viết đầu tiên của tôi trên nền tảng này. Thật tuyệt vời! Nội dung này đủ dài để kiểm tra xem label có tự động xuống dòng và control có tự động giãn chiều cao hay không.",
                MediaUrl = "",
                Visibility = "Public",
                LikesCount = 0,
                CommentsCount = 0,
                CreatedAt = DateTime.Now.AddDays(-5),
                UpdatedAt = null
            });
            posts.Add(new Post
            {
                PostID = 12346,
                UserID = currentUserId,
                Content = "Một ngày đẹp trời để lập trình WinForms.",
                MediaUrl = "",
                Visibility = "Public",
                LikesCount = 0,
                CommentsCount = 0,
                CreatedAt = DateTime.Now.AddDays(-3),
                UpdatedAt = null
            });
            posts.Add(new Post
            {
                PostID = 12347,
                UserID = currentUserId,
                Content = "Vừa hoàn thành xong giao diện Dashboard. Cảm giác thật tuyệt!",
                MediaUrl = "",
                Visibility = "Public",
                LikesCount = 0,
                CommentsCount = 0,
                CreatedAt = DateTime.Now.AddDays(-1),
                UpdatedAt = null
            });
            posts.Add(new Post
            {
                PostID = 12348,
                UserID = currentUserId,
                Content = "Chuẩn bị cho các chức năng tiếp theo. #coding #winforms",
                MediaUrl = "",
                Visibility = "Public",
                LikesCount = 0,
                CommentsCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
            posts.Add(new Post
            {
                PostID = 12349,
                UserID = currentUserId,
                Content = "Nội dung của bài viết thứ năm.",
                MediaUrl = "",
                Visibility = "Public",
                LikesCount = 0,
                CommentsCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
            return posts;
        }

        private void LoadPosts()
        {
            flowLayoutPanelPosts.Controls.Clear();
            List<Post> userPosts = GetPostsForCurrentUser();

            foreach (var postData in userPosts)
            {
                var postControl = new PostControl(postData);
                //postControl.PostClicked += PostControl_PostClicked;
                flowLayoutPanelPosts.Controls.Add(postControl);
            }

            flowLayoutPanelPosts_Resize(this, EventArgs.Empty);
        }

        //private void PostControl_PostClicked(object? sender, string postId)
        //{
        //    frmPost postForm = new frmPost(postId);
        //    postForm.ShowDialog();
        //}

        private void flowLayoutPanelPosts_Resize(object? sender, EventArgs e)
        {
            foreach (Control c in flowLayoutPanelPosts.Controls)
            {
                c.Width = flowLayoutPanelPosts.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            }
            lblUsername.Text = AuthSessionService.CurrentUser.UserName;
        }

        private void lblUsername_Click(object sender, EventArgs e)
        {

        }

        private void btnNewPost_Click(object sender, EventArgs e)
        {
            frmPost frmPost = new frmPost();
            frmPost.ShowDialog();
        }
    }
}