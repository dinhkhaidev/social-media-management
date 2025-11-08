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
      this.gbPostContainer = new System.Windows.Forms.GroupBox();
      this.btnDelete = new System.Windows.Forms.Button();
      this.lblStatus = new System.Windows.Forms.Label();
      this.btnComment = new System.Windows.Forms.Button();
      this.btnLike = new System.Windows.Forms.Button();
      this.lblContent = new System.Windows.Forms.Label();
      this.lblPostInfo = new System.Windows.Forms.Label();
      this.gbPostContainer.SuspendLayout();
      this.SuspendLayout();
      //
      // gbPostContainer
      //
      this.gbPostContainer.Controls.Add(this.btnDelete);
      this.gbPostContainer.Controls.Add(this.lblStatus);
      this.gbPostContainer.Controls.Add(this.btnComment);
      this.gbPostContainer.Controls.Add(this.btnLike);
      this.gbPostContainer.Controls.Add(this.lblContent);
      this.gbPostContainer.Controls.Add(this.lblPostInfo);
      this.gbPostContainer.Dock = System.Windows.Forms.DockStyle.Fill;
      this.gbPostContainer.Location = new System.Drawing.Point(5, 5);
      this.gbPostContainer.Name = "gbPostContainer";
      this.gbPostContainer.Padding = new System.Windows.Forms.Padding(10);
      this.gbPostContainer.Size = new System.Drawing.Size(690, 160);
      this.gbPostContainer.TabIndex = 0;
      this.gbPostContainer.TabStop = false;
      //
      // lblStatus
      //
      this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.lblStatus.AutoSize = true;
      this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
      this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblStatus.Location = new System.Drawing.Point(582, 129);
      this.lblStatus.Name = "lblStatus";
      this.lblStatus.Size = new System.Drawing.Size(95, 19);
      this.lblStatus.TabIndex = 4;
      this.lblStatus.Text = "Đã chỉnh sửa";
      this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      //
      // btnComment
      //
      this.btnComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.btnComment.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btnComment.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
      this.btnComment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnComment.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.btnComment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.btnComment.Location = new System.Drawing.Point(450, 121);
      this.btnComment.Name = "btnComment";
      this.btnComment.Size = new System.Drawing.Size(120, 32);
      this.btnComment.TabIndex = 3;
      this.btnComment.Text = "Bình luận";
      this.btnComment.UseVisualStyleBackColor = false;
      //
      // btnDelete
      //
      this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
      this.btnDelete.FlatAppearance.BorderSize = 0;
      this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
      this.btnDelete.ForeColor = System.Drawing.Color.White;
      this.btnDelete.Location = new System.Drawing.Point(139, 121);
      this.btnDelete.Name = "btnDelete";
      this.btnDelete.Size = new System.Drawing.Size(80, 32);
      this.btnDelete.TabIndex = 5;
      this.btnDelete.Text = "✕ Xóa";
      this.btnDelete.UseVisualStyleBackColor = false;
      this.btnDelete.Visible = false;
      //
      // btnLike
      //
      this.btnLike.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.btnLike.BackColor = System.Drawing.Color.WhiteSmoke;
      this.btnLike.FlatAppearance.BorderColor = System.Drawing.Color.Gainsboro;
      this.btnLike.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.btnLike.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.btnLike.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.btnLike.Location = new System.Drawing.Point(13, 121);
      this.btnLike.Name = "btnLike";
      this.btnLike.Size = new System.Drawing.Size(120, 32);
      this.btnLike.TabIndex = 2;
      this.btnLike.Text = "Thích";
      this.btnLike.UseVisualStyleBackColor = false;
      //
      // lblContent
      //
      this.lblContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
      | System.Windows.Forms.AnchorStyles.Right)));
      this.lblContent.AutoSize = true;
      this.lblContent.Font = new System.Drawing.Font("Segoe UI", 10F);
      this.lblContent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
      this.lblContent.Location = new System.Drawing.Point(13, 55);
      this.lblContent.MaximumSize = new System.Drawing.Size(660, 0);
      this.lblContent.Name = "lblContent";
      this.lblContent.Size = new System.Drawing.Size(232, 23);
      this.lblContent.TabIndex = 1;
      this.lblContent.Text = "Đây là nội dung của bài viết...";
      //
      // lblPostInfo
      //
      this.lblPostInfo.AutoSize = true;
      this.lblPostInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
      this.lblPostInfo.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
      this.lblPostInfo.Location = new System.Drawing.Point(13, 25);
      this.lblPostInfo.Name = "lblPostInfo";
      this.lblPostInfo.Size = new System.Drawing.Size(201, 20);
      this.lblPostInfo.TabIndex = 0;
      this.lblPostInfo.Text = "ID: 12345 - 20/10/2025";
      //
      // PostControl
      //
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.gbPostContainer);
      this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 10);
      this.Name = "PostControl";
      this.Padding = new System.Windows.Forms.Padding(5);
      this.Size = new System.Drawing.Size(700, 170);
      this.gbPostContainer.ResumeLayout(false);
      this.gbPostContainer.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbPostContainer;
    private System.Windows.Forms.Label lblPostInfo;
    private System.Windows.Forms.Label lblContent;
    private System.Windows.Forms.Button btnLike;
    private System.Windows.Forms.Button btnComment;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Button btnDelete;
  }
}
