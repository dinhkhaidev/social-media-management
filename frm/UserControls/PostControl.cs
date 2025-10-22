using System;
using System.Windows.Forms;
using SocialManager.services;

namespace SocialManager.controls
{
    public partial class PostControl : UserControl
    {
        public string PostId { get; private set; } = "";
        public event EventHandler<string>? PostClicked;

        public PostControl()
        {
            InitializeComponent();
            SetupControl();
        }

        public PostControl(Post postData)
        {
            InitializeComponent();
            SetupControl();

            this.PostId = postData.PostID.ToString(); // CS0029: Convert int to string
            lblPostInfo.Text = $"ID: {postData.PostID} - {postData.CreatedAt:dd/MM/yyyy}"; // IDE0071: Simplify interpolation
            lblContent.Text = postData.Content;
            lblStatus.Visible = postData.UpdatedAt.HasValue; // CS1061: Use UpdatedAt to infer "edited" status

            AdjustHeight();
        }

        private void SetupControl()
        {
            this.DoubleBuffered = true;
            this.Click += (s, e) => OnPostClicked();
            this.gbPostContainer.Click += (s, e) => OnPostClicked();
            this.lblContent.Click += (s, e) => OnPostClicked();
            this.lblPostInfo.Click += (s, e) => OnPostClicked();
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