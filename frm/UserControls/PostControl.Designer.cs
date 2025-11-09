using SocialManager.controls;

namespace SocialManager.controls
{
  partial class PostControl
  {
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    private void InitializeComponent()
    {
      gbPostContainer = new GroupBox();
      lblPostInfo = new Label();
      lblContent = new Label();
      btnLike = new RoundedButton();
      btnComment = new RoundedButton();
      btnDelete = new RoundedButton();
      btnReport = new RoundedButton();
      gbPostContainer.SuspendLayout();
      SuspendLayout();
      //
      // gbPostContainer
      //
      gbPostContainer.Controls.Add(btnDelete);
      gbPostContainer.Controls.Add(btnReport);
      gbPostContainer.Controls.Add(btnComment);
      gbPostContainer.Controls.Add(btnLike);
      gbPostContainer.Controls.Add(lblContent);
      gbPostContainer.Controls.Add(lblPostInfo);
      gbPostContainer.Dock = DockStyle.Fill;
      gbPostContainer.Location = new Point(5, 5);
      gbPostContainer.Name = "gbPostContainer";
      gbPostContainer.Padding = new Padding(10);
      gbPostContainer.Size = new Size(690, 260);
      gbPostContainer.TabIndex = 0;
      gbPostContainer.TabStop = false;
      //
      // lblPostInfo
      //
      lblPostInfo.AutoSize = true;
      lblPostInfo.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      lblPostInfo.ForeColor = SystemColors.ControlDarkDark;
      lblPostInfo.Location = new Point(13, 25);
      lblPostInfo.Name = "lblPostInfo";
      lblPostInfo.Size = new Size(181, 20);
      lblPostInfo.TabIndex = 0;
      lblPostInfo.Text = "userName - 20/10/2025";
      //
      // lblContent
      //
      lblContent.AutoSize = true;
      lblContent.Font = new Font("Segoe UI", 10F);
      lblContent.ForeColor = Color.FromArgb(64, 64, 64);
      lblContent.Location = new Point(13, 55);
      lblContent.MaximumSize = new Size(660, 0);
      lblContent.Name = "lblContent";
      lblContent.Size = new Size(235, 23);
      lblContent.TabIndex = 1;
      lblContent.Text = "Đây là nội dung của bài viết...";
      //
      // btnLike
      //
      btnLike.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      btnLike.BackColor = Color.WhiteSmoke;
      btnLike.BackgroundColor = Color.WhiteSmoke;
      btnLike.BorderColor = Color.PaleVioletRed;
      btnLike.BorderRadius = 32;
      btnLike.BorderSize = 0;
      btnLike.FlatAppearance.BorderSize = 0;
      btnLike.FlatStyle = FlatStyle.Flat;
      btnLike.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      btnLike.ForeColor = Color.FromArgb(64, 64, 64);
      btnLike.Location = new Point(13, 215);
      btnLike.Name = "btnLike";
      btnLike.Size = new Size(120, 32);
      btnLike.TabIndex = 2;
      btnLike.Text = "Thích";
      btnLike.TextColor = Color.FromArgb(64, 64, 64);
      btnLike.UseVisualStyleBackColor = false;
      //
      // btnComment
      //
      btnComment.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      btnComment.BackColor = Color.WhiteSmoke;
      btnComment.BackgroundColor = Color.WhiteSmoke;
      btnComment.BorderColor = Color.PaleVioletRed;
      btnComment.BorderRadius = 32;
      btnComment.BorderSize = 0;
      btnComment.FlatAppearance.BorderSize = 0;
      btnComment.FlatStyle = FlatStyle.Flat;
      btnComment.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      btnComment.ForeColor = Color.FromArgb(64, 64, 64);
      btnComment.Location = new Point(139, 215);
      btnComment.Name = "btnComment";
      btnComment.Size = new Size(120, 32);
      btnComment.TabIndex = 3;
      btnComment.Text = "Bình luận";
      btnComment.TextColor = Color.FromArgb(64, 64, 64);
      btnComment.UseVisualStyleBackColor = false;
      //
      // btnDelete
      //
      btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btnDelete.BackColor = Color.FromArgb(255, 83, 73);
      btnDelete.BackgroundColor = Color.FromArgb(255, 83, 73);
      btnDelete.BorderColor = Color.PaleVioletRed;
      btnDelete.BorderRadius = 27;
      btnDelete.BorderSize = 0;
      btnDelete.FlatAppearance.BorderSize = 0;
      btnDelete.FlatStyle = FlatStyle.Flat;
      btnDelete.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      btnDelete.ForeColor = Color.White;
      btnDelete.Location = new Point(644, 18);
      btnDelete.Name = "btnDelete";
      btnDelete.Size = new Size(33, 33);
      btnDelete.TabIndex = 5;
      btnDelete.Text = "X";
      btnDelete.TextColor = Color.White;
      btnDelete.UseVisualStyleBackColor = false;
      btnDelete.Visible = false;
      //
      // btnReport
      //
      btnReport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      btnReport.BackColor = Color.FromArgb(255, 117, 24);
      btnReport.BackgroundColor = Color.FromArgb(255, 117, 24);
      btnReport.BorderColor = Color.PaleVioletRed;
      btnReport.BorderRadius = 33;
      btnReport.BorderSize = 0;
      btnReport.FlatAppearance.BorderSize = 0;
      btnReport.FlatStyle = FlatStyle.Flat;
      btnReport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
      btnReport.ForeColor = Color.White;
      btnReport.Location = new Point(605, 18);
      btnReport.Name = "btnReport";
      btnReport.Size = new Size(33, 33);
      btnReport.TabIndex = 6;
      btnReport.Text = "!";
      btnReport.TextColor = Color.White;
      btnReport.UseVisualStyleBackColor = false;
      btnReport.Visible = false;
      btnReport.Click += BtnReport_Click;
      //
      // PostControl
      //
      AutoScaleDimensions = new SizeF(8F, 20F);
      AutoScaleMode = AutoScaleMode.Font;
      BackColor = Color.White;
      Controls.Add(gbPostContainer);
      Margin = new Padding(3, 3, 3, 10);
      Name = "PostControl";
      Padding = new Padding(5);
      Size = new Size(700, 270);
      gbPostContainer.ResumeLayout(false);
      gbPostContainer.PerformLayout();
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.GroupBox gbPostContainer;
    private System.Windows.Forms.Label lblPostInfo;
    private System.Windows.Forms.Label lblContent;
    private RoundedButton btnLike;
    private RoundedButton btnComment;
    private RoundedButton btnDelete;
    private RoundedButton btnReport;
  }
}
